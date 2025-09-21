using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.Models.Products
{
    public class ProductDto
    {
        public required string Name { get; set; }
        public required Guid CategoryId { get; set; }
        public required string Model { get; set; }
        public required string MadeIn { get; set; }
        public required string Price { get; set; }
        public required string Color { get; set; }
        public required int Number { get; set; }
        public required List<IFormFile> ProductPictures { get; set; }
        [HiddenInput]
        public string ProductPicturesStr { get; set; }
        public required string ReleaseDate { get; set; }
        public required string MainDescription { get; set; }
        public required string AllDescription { get; set; }
    }
}
