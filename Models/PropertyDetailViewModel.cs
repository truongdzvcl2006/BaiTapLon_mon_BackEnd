// Models/PropertyDetailViewModel.cs
using System;
using System.Collections.Generic;

namespace RealEstateAuction.Models
{
    public class PropertyDetailViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public double Area { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public string PropertyType { get; set; }
        public string MainImageUrl { get; set; }
        public List<string> GalleryImages { get; set; }
        public List<string> Amenities { get; set; }
        public string ProjectName { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Investor { get; set; }
        public string Building { get; set; }
        public string Floor { get; set; }
        public string ContactPerson { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
        public int ViewCount { get; set; }
        public bool IsHot { get; set; }
    }
}