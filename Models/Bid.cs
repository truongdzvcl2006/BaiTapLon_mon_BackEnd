// Models/Bid.cs
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstateAuction.Models
{
    [Table("Bids")]
    public class Bid
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        public DateTime BidTime { get; set; } = DateTime.Now;

        [Required]
        public int AuctionId { get; set; }

        [Required]
        public int UserId { get; set; }

        // Navigation properties
        [ForeignKey("AuctionId")]
        public virtual Auction Auction { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}