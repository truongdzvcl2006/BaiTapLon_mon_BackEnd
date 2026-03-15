using Microsoft.AspNetCore.Mvc;
using RealEstateAuction.Models;
using System.Collections.Generic;

namespace RealEstateAuction.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Projects()
        {
            var properties = new List<Property>
            {
                new Property
                {
                    Id = 1,
                    Title = "Căn hộ VinHome Grand Park Q9",
                    Price = 3800000000,
                    Location = "Quận 9, TP.HCM",
                    Area = 75,
                    Bedrooms = 2,
                    Bathrooms = 1,
                    ImageUrl = "/images/House1.jpg",
                    IsHot = true
                },
                new Property
                {
                    Id = 2,
                    Title = "Biệt thự ven sông",
                    Price = 15000000000,
                    Location = "Quận 2, TP.HCM",
                    Area = 200,
                    Bedrooms = 4,
                    Bathrooms = 5,
                    ImageUrl = "/images/House2.jpg",
                    IsHot = true
                }
            };

            return View(properties);
        }

        public IActionResult PropertyDetail(int id)
        {
            // Tạo dữ liệu mẫu dựa vào id
            var property = new PropertyDetailViewModel
            {
                Id = id,
                Title = id == 1 ? "Căn hộ VinHome Grand Park Q9" : "Biệt thự ven sông",
                Price = id == 1 ? 3800000000 : 15000000000,
                Location = id == 1 ? "Quận 9, TP.HCM" : "Quận 2, TP.HCM",
                Description = id == 1
                    ? "Căn hộ cao cấp tại khu đô thị VinHome Grand Park, view đẹp, tiện ích đầy đủ."
                    : "Biệt thự sang trọng ven sông Sài Gòn, không gian yên tĩnh, thoáng mát.",
                Area = id == 1 ? 75 : 200,
                Bedrooms = id == 1 ? 2 : 4,
                Bathrooms = id == 1 ? 1 : 5,
                MainImageUrl = id == 1 ? "/images/House1.jpg" : "/images/House2.jpg",
                PropertyType = "Bán",
                IsHot = true
            };

            return View(property);
        }

        public IActionResult DauGia()
        {
            return View();
        }
    }
}