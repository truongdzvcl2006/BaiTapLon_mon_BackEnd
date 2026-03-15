// Models/Deposit.cs
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstateAuction.Models
{
    [Table("Deposits")]
    public class Deposit
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int AuctionId { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        public DateTime DepositDate { get; set; } = DateTime.Now;

        public DateTime? ExpiryDate { get; set; }  // Cho phép null

        [StringLength(50)]
        public string Status { get; set; } = "Chờ xác nhận";

        // Navigation properties
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("AuctionId")]
        public virtual Auction Auction { get; set; }
    }
}