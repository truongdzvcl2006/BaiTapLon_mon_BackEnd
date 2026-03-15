using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstateAuction.Data;
using RealEstateAuction.Models;
using System;
using System.Linq;
using System.Collections.Generic;

namespace RealEstateAuction.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        // ================ KIỂM TRA QUYỀN ADMIN ================
        private bool IsAdmin()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var userRole = HttpContext.Session.GetInt32("UserRoleId");
            return userId != null && userRole == 1;
        }

        // ================ DASHBOARD ================
        public IActionResult Index()
        {
            // DEBUG: Kiểm tra session
            var userId = HttpContext.Session.GetInt32("UserId");
            var userRole = HttpContext.Session.GetInt32("UserRoleId");
            var userEmail = HttpContext.Session.GetString("UserEmail");
            var userName = HttpContext.Session.GetString("UserName");

            Console.WriteLine($"========== ADMIN INDEX ==========");
            Console.WriteLine($"UserId from session: {userId}");
            Console.WriteLine($"UserRole from session: {userRole}");
            Console.WriteLine($"UserEmail from session: {userEmail}");
            Console.WriteLine($"UserName from session: {userName}");
            Console.WriteLine($"=================================");

            // Nếu chưa đăng nhập
            if (userId == null)
            {
                Console.WriteLine("Chưa đăng nhập, chuyển về Login");
                return RedirectToAction("Login", "Account");
            }

            // Nếu không phải admin
            if (userRole != 1)
            {
                Console.WriteLine($"Không phải admin (Role = {userRole}), chuyển về Home");
                return RedirectToAction("Index", "Home");
            }

            // Lấy thông tin user từ database
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                Console.WriteLine("Không tìm thấy user trong database");
                HttpContext.Session.Clear();
                return RedirectToAction("Login", "Account");
            }

            ViewBag.AdminName = user.FullName;
            ViewBag.AdminEmail = user.Email;

            // Thống kê cho dashboard
            try
            {
                ViewBag.TotalUsers = _context.Users.Count();
                ViewBag.TotalProperties = _context.Properties?.Count() ?? 0;
                ViewBag.TotalAuctions = _context.Auctions?.Count() ?? 0;
                ViewBag.TotalBids = _context.Bids?.Count() ?? 0;
                ViewBag.TotalDeposits = _context.Deposits?.Count() ?? 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi thống kê: {ex.Message}");
                ViewBag.TotalUsers = 0;
                ViewBag.TotalProperties = 0;
                ViewBag.TotalAuctions = 0;
                ViewBag.TotalBids = 0;
                ViewBag.TotalDeposits = 0;
            }

            Console.WriteLine($"Load Admin Index thành công cho: {user.FullName}");
            return View();
        }

        // ================ QUẢN LÝ NGƯỜI DÙNG ================
        public IActionResult Users()
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");

            var users = _context.Users.ToList();
            return View(users);
        }

        [HttpGet]
        public IActionResult GetUser(int id)
        {
            if (!IsAdmin()) return Unauthorized();

            var user = _context.Users.Find(id);
            if (user == null)
                return NotFound();

            return Json(user);
        }

        [HttpPost]
        public IActionResult SaveUser([FromForm] User user)
        {
            if (!IsAdmin()) return Unauthorized();

            try
            {
                if (user.Id == 0)
                {
                    // Thêm mới
                    user.CreatedAt = DateTime.Now;
                    _context.Users.Add(user);
                }
                else
                {
                    // Cập nhật
                    var existingUser = _context.Users.Find(user.Id);
                    if (existingUser == null)
                        return NotFound();

                    existingUser.FullName = user.FullName;
                    existingUser.Email = user.Email;
                    existingUser.Phone = user.Phone;
                    existingUser.Address = user.Address;
                    existingUser.RoleId = user.RoleId;
                    existingUser.IsActive = user.IsActive;

                    if (!string.IsNullOrEmpty(user.Password))
                    {
                        existingUser.Password = user.Password;
                    }
                }

                _context.SaveChanges();
                return Ok(new { success = true, message = "Lưu thành công!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult DeleteUser(int id)
        {
            if (!IsAdmin()) return Unauthorized();

            try
            {
                var user = _context.Users.Find(id);
                if (user == null)
                    return NotFound();

                _context.Users.Remove(user);
                _context.SaveChanges();

                return Ok(new { success = true, message = "Xóa thành công!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult ToggleUserStatus(int id)
        {
            if (!IsAdmin()) return Unauthorized();

            var user = _context.Users.Find(id);
            if (user == null)
                return NotFound();

            user.IsActive = !user.IsActive;
            _context.SaveChanges();

            return Ok(new { success = true, isActive = user.IsActive });
        }

        // ================ QUẢN LÝ ĐẤU GIÁ ================
        public IActionResult Auctions()
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");
            return View();
        }

        [HttpGet]
        public IActionResult GetAuctions()
        {
            if (!IsAdmin()) return Unauthorized();

            var auctions = _context.Auctions
                .Include(a => a.Property)
                .Include(a => a.Bids)
                .Select(a => new
                {
                    a.Id,
                    a.PropertyId,
                    PropertyName = a.Property != null ? a.Property.Title : "",
                    a.StartingPrice,
                    a.CurrentPrice,
                    a.ReservePrice,
                    a.StartTime,
                    a.EndTime,
                    a.Status,
                    BidCount = a.Bids != null ? a.Bids.Count : 0
                })
                .ToList();

            return Json(auctions);
        }

        [HttpPost]
        public IActionResult CreateAuction([FromBody] Auction auction)
        {
            if (!IsAdmin()) return Unauthorized();

            try
            {
                auction.Status = "Sắp diễn ra";
                _context.Auctions.Add(auction);
                _context.SaveChanges();

                return Ok(new { success = true, message = "Tạo phiên đấu giá thành công!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult UpdateAuctionStatus(int id, string status)
        {
            if (!IsAdmin()) return Unauthorized();

            var auction = _context.Auctions.Find(id);
            if (auction == null)
                return NotFound();

            auction.Status = status;
            _context.SaveChanges();

            return Ok(new { success = true, message = "Cập nhật trạng thái thành công!" });
        }

        // ================ QUẢN LÝ ĐẶT CỌC ================
        public IActionResult Deposits()
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");
            return View();
        }

        [HttpGet]
        public IActionResult GetDeposits()
        {
            if (!IsAdmin()) return Unauthorized();

            var deposits = _context.Deposits
                .Include(d => d.User)
                .Include(d => d.Auction)
                    .ThenInclude(a => a.Property)
                .Select(d => new
                {
                    d.Id,
                    d.Amount,
                    d.DepositDate,
                    d.Status,
                    d.ExpiryDate,
                    UserName = d.User != null ? d.User.FullName : "",
                    PropertyName = d.Auction != null && d.Auction.Property != null ? d.Auction.Property.Title : ""
                })
                .ToList();

            return Json(deposits);
        }

        [HttpGet]
        public IActionResult GetDeposit(int id)
        {
            if (!IsAdmin()) return Unauthorized();

            var deposit = _context.Deposits
                .Include(d => d.User)
                .Include(d => d.Auction)
                .FirstOrDefault(d => d.Id == id);

            if (deposit == null)
                return NotFound();

            return Json(deposit);
        }

        [HttpPost]
        public IActionResult ConfirmDeposit(int id)
        {
            if (!IsAdmin()) return Unauthorized();

            var deposit = _context.Deposits.Find(id);
            if (deposit == null)
                return NotFound();

            deposit.Status = "Đã xác nhận";
            _context.SaveChanges();

            return Ok(new { success = true, message = "Xác nhận đặt cọc thành công!" });
        }

        [HttpPost]
        public IActionResult CancelDeposit(int id)
        {
            if (!IsAdmin()) return Unauthorized();

            var deposit = _context.Deposits.Find(id);
            if (deposit == null)
                return NotFound();

            deposit.Status = "Đã hủy";
            _context.SaveChanges();

            return Ok(new { success = true, message = "Hủy đặt cọc thành công!" });
        }

        // ================ BÁO CÁO ================
        public IActionResult Reports()
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");
            return View();
        }

        [HttpGet]
        public IActionResult GetReportData(DateTime fromDate, DateTime toDate)
        {
            if (!IsAdmin()) return Unauthorized();

            try
            {
                var reportData = new
                {
                    // Thống kê giao dịch
                    Transactions = _context.Auctions
                        .Where(a => a.StartTime >= fromDate && a.EndTime <= toDate)
                        .Select(a => new
                        {
                            a.Id,
                            a.StartTime,
                            a.EndTime,
                            a.CurrentPrice,
                            a.Status
                        })
                        .ToList(),

                    // Thống kê doanh thu
                    Revenue = new
                    {
                        Total = _context.Auctions
                            .Where(a => a.EndTime <= toDate)
                            .Sum(a => (decimal?)a.CurrentPrice) ?? 0,
                        Commission = _context.Auctions
                            .Where(a => a.EndTime <= toDate)
                            .Sum(a => (decimal?)a.CurrentPrice * 0.05m) ?? 0
                    },

                    // Thống kê người dùng mới
                    NewUsers = _context.Users
                        .Where(u => u.CreatedAt >= fromDate && u.CreatedAt <= toDate)
                        .Count(),

                    // Thống kê đấu giá theo trạng thái
                    AuctionStats = _context.Auctions
                        .GroupBy(a => a.Status)
                        .Select(g => new { Status = g.Key, Count = g.Count() })
                        .ToList(),

                    // Thống kê đặt cọc
                    DepositStats = _context.Deposits
                        .Where(d => d.DepositDate >= fromDate && d.DepositDate <= toDate)
                        .GroupBy(d => d.Status)
                        .Select(g => new { Status = g.Key, Count = g.Count(), Total = g.Sum(d => d.Amount) })
                        .ToList()
                };

                return Json(reportData);
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        // ================ CÀI ĐẶT ================
        public IActionResult Settings()
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");
            return View();
        }

        [HttpPost]
        public IActionResult SaveSettings([FromBody] Dictionary<string, object> settings)
        {
            if (!IsAdmin()) return Unauthorized();

            try
            {
                // TODO: Lưu cài đặt vào database hoặc file config
                return Ok(new { success = true, message = "Lưu cài đặt thành công!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        // ================ KEEP SESSION ================
        [HttpGet]
        public IActionResult KeepSession()
        {
            return Ok(new { time = DateTime.Now });
        }
    }
}