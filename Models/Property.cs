// Models/Property.cs
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstateAuction.Models
{
    [Table("Properties")]
    public class Property
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        [StringLength(500)]
        public string Location { get; set; }

        [StringLength(2000)]
        public string Description { get; set; }

        public double Area { get; set; }

        [Display(Name = "Số phòng ngủ")]
        public int Bedrooms { get; set; }

        [Display(Name = "Số phòng tắm")]
        public int Bathrooms { get; set; }

        [StringLength(500)]
        public string ImageUrl { get; set; }

        [StringLength(50)]
        public string PropertyType { get; set; } // Bán, Cho thuê

        public bool IsHot { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [StringLength(100)]
        public string ProjectName { get; set; }

        [StringLength(100)]
        public string Investor { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        [StringLength(50)]
        public string Building { get; set; }

        [StringLength(50)]
        public string Floor { get; set; }

        // Navigation property cho ảnh gallery
        public virtual ICollection<PropertyImage> PropertyImages { get; set; }

        // Navigation property cho tiện ích
        public virtual ICollection<PropertyAmenity> PropertyAmenities { get; set; }
    }
    // Models/Property.cs - Sửa phần PropertyImage
    public class PropertyImage
    {
        public int Id { get; set; }
        public int PropertyId { get; set; }

        [Required]
        [StringLength(500)]
        public string ImageUrl { get; set; }

        [StringLength(200)]
        public string? Caption { get; set; } // Thêm ? để nullable

        public int SortOrder { get; set; }

        [ForeignKey("PropertyId")]
        public virtual Property Property { get; set; }
    }

    [Table("PropertyAmenities")]
    public class PropertyAmenity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PropertyId { get; set; }

        [Required]
        [StringLength(200)]
        public string AmenityName { get; set; }

        [ForeignKey("PropertyId")]
        public virtual Property Property { get; set; }
    }
}