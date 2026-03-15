// Models/Auction.cs
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstateAuction.Models
{
    [Table("Auctions")]
    public class Auction
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PropertyId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal StartingPrice { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal CurrentPrice { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? ReservePrice { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        [StringLength(50)]
        public string Status { get; set; } = "Sắp diễn ra";

        // Navigation properties
        [ForeignKey("PropertyId")]
        public virtual Property Property { get; set; }

        public virtual ICollection<Bid> Bids { get; set; } = new List<Bid>();
        public virtual ICollection<Deposit> Deposits { get; set; } = new List<Deposit>();
    }
}