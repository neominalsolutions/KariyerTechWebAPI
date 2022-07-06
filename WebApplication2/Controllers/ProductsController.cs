using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ProductsController : ControllerBase
  {

    [HttpGet("list")] // api/products/list
    public IActionResult List()
    {
      return Ok(new List<Product>()
      {
        new Product{ Id = 1, Name = "A"},
        new Product{ Id = 2, Name = "B"},
      }) ;
    }


    [HttpPost("create")]
    public IActionResult Create([FromBody] Product model)
    {
      return Created($"api/products/{model.Id}", model);
    }


    // Het işlemlerinde 200 statuscode kullanırız
    [HttpGet("detail/{id?}")]
    public IActionResult Detail(int? id)
    {
      // 200 status code
      return Ok(new Product { Id = (int)id, Name = "Test" });
    }

    [Authorize] // login olmadıysak 401 döndürsün
    [HttpDelete("delete/{id?}")] // api/products/delete/1
    public IActionResult Delete(int? id)
    {


      return NoContent();
    }

    [HttpPut("update/{id?}")]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.UnprocessableEntity)]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public IActionResult Update(int? id, [FromBody] Product model)
    {

      if(model.Name == "Test")
      {
        //throw new Exception("Test adında kayıttan mevcut");
        return StatusCode(500); // 500 status code
      }

      if (string.IsNullOrEmpty(model.Name))
      {
        return UnprocessableEntity();
      }


      if(id == null)
      {
        return NotFound();
      }

      return NoContent();
    }
  }
}
