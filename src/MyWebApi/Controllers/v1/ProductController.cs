using Application.Commands;
using Application.Models.Products;
using Application.Queries;
using Application.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWebApi.Filters;

namespace MyWebApi.Controllers.v1
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiVersion("1.0")]
    [CustomActionResultFilter]
    [ExceptionFilter]
    public class ProductController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IWebHostEnvironment environment;
        private const string ImagePath = "images/product";
        private string uploadRootpath = "";
        public ProductController(IMediator mediator, IWebHostEnvironment environment)
        {
            this.mediator = mediator;
            this.environment = environment;

            var uploadRootpath = Path.Combine(environment.WebRootPath, ImagePath);
            if (!Directory.Exists(uploadRootpath))
            {
                Directory.CreateDirectory(uploadRootpath);
            }

        }
         

        [HttpGet]
        public async Task<IActionResult> AllProducts()
        {

            var result = await mediator.Send(new AllProductsQuery());
            if (result.Count()==0)
            {
                return Ok(new CustomActionResult<object>(true, "هیچ محصولی موجود نیست", result));

            }
            return Ok(new CustomActionResult<object>(true, "", result));

        }

        [HttpPost]
        [Authorize(Roles = "Admin")] 
        public async Task<IActionResult> CreateAsync([FromQuery]ProductDto dto)
        {
             uploadRootpath = Path.Combine(environment.WebRootPath, ImagePath);
            if (dto.ProductPictures is not null && dto.ProductPictures.Count > 0)
            {
                List<string> fileNames = new List<string>(); 
                foreach (var picture in dto.ProductPictures)
                {
                    string fileExtension = Path.GetExtension(Path.GetFileName(picture.FileName));
                    string newFileName = $"Product_{Guid.NewGuid().ToString().Replace("-", "")}{fileExtension}";
                    var filePath = Path.Combine(uploadRootpath, newFileName);

                    using var fileStream = new FileStream(filePath, FileMode.Create);
                    await picture.CopyToAsync(fileStream).ConfigureAwait(false); 

                    fileNames.Add(newFileName); 
                }

                dto.ProductPicturesStr = string.Join(",", fileNames);

            }
            var result = mediator.Send(new CreateProductCommand(dto));
            if (result.IsCompletedSuccessfully == true)
            {
                return Ok(new CustomActionResult<object>(true,"",result.Result));
            }
            return Ok(new CustomActionResult<object>(true, "", result.Exception.InnerExceptions.Select(x => x.Message).ToList()));
           
        }

        [HttpGet]
        public async Task<IActionResult> GetProductById([FromQuery] string Id)
        {
            var result = await mediator.Send(new GetProductByIdQuery(Id));

            return Ok(result);
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")] 
        public Task<IActionResult> DeleteProduct([FromQuery] string Id)
        {
            var result =  mediator.Send(new DeleteProductCommand(Id));

            return Task.FromResult<IActionResult>(Ok(result));
        }

        [HttpPut]
        [Authorize(Roles = "Admin")] 
        public async Task<IActionResult> UpdateAsync([FromQuery]string id,[FromQuery] ProductDto dto)
        {
            uploadRootpath = Path.Combine(environment.WebRootPath, ImagePath);
            if (dto.ProductPictures is not null && dto.ProductPictures.Count > 0)
            {
                List<string> fileNames = new List<string>(); 
                foreach (var picture in dto.ProductPictures)
                {
                    string fileExtension = Path.GetExtension(Path.GetFileName(picture.FileName));
                    string newFileName = $"Product_{Guid.NewGuid().ToString().Replace("-", "")}{fileExtension}";
                    var filePath = Path.Combine(uploadRootpath, newFileName);

                    using var fileStream = new FileStream(filePath, FileMode.Create);
                    await picture.CopyToAsync(fileStream).ConfigureAwait(false);

                    fileNames.Add(newFileName);
                }

                dto.ProductPicturesStr = string.Join(",", fileNames);

            }

            var result = mediator.Send(new UpdateProductCommand(id,dto));
            if (result.IsCompletedSuccessfully == true)
            {
                return Ok(new CustomActionResult<object>(true, "", result.Result));
            }
            return Ok(new CustomActionResult<object>(true, "", result.Exception.InnerExceptions.Select(x => x.Message).ToList()));

        }

    }
}
