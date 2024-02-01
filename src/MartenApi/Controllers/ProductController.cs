using MartenApi.Data;
using Microsoft.AspNetCore.Mvc;

namespace MartenApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _repository;

    public ProductController(IProductRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public IActionResult GetProducts()
    {
        return Ok(_repository.GetProducts());
    }


    [HttpGet("better")]
    public IActionResult GetProductsBetter(int page)
    {
        return Ok(_repository.GetProductsBetter(page));
    }

}
