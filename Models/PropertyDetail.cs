// Models/PropertyDetail.cs
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstateAuction.Models
{
    [Table("PropertyDetails")]
    public class PropertyDetail
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [StringLength(500)]
        public string Location { get; set; }

        [StringLength(2000)]
        public string Description { get; set; }

        public double Area { get; set; }

        public int Bedrooms { get; set; }

        public int Bathrooms { get; set; }

        [StringLength(500)]
        public string ImageUrl { get; set; }

        [StringLength(50)]
        public string PropertyType { get; set; }

        public bool IsHot { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? StartingBid { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? CurrentBid { get; set; }

        public DateTime? AuctionStartDate { get; set; }
        public DateTime? AuctionEndDate { get; set; }

        public string? ProjectName { get; set; }
        public string? Investor { get; set; }
        public string? Status { get; set; }
        public string? Building { get; set; }
        public string? Floor { get; set; }

        public int? AuctionId { get; set; }
        public int? UserId { get; set; }

        // Navigation properties
        [ForeignKey("AuctionId")]
        public virtual Auction? Auction { get; set; }

        [ForeignKey("UserId")]
        public virtual User? User { get; set; }

        // Collections - PHẢI CÓ 2 DÒNG NÀY
        public virtual ICollection<PropertyImage> PropertyImages { get; set; } = new List<PropertyImage>();
        public virtual ICollection<PropertyAmenity> PropertyAmenities { get; set; } = new List<PropertyAmenity>();
    }
}