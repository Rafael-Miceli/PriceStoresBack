using System;
using Api.ViewModels;
using Api.ApplicationServices.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Api.Models;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private IProductApplicationService _productApplicationService;

        public ProductController(IProductApplicationService productApplicationService)
        {            
            _productApplicationService = productApplicationService;
        }

        [HttpGet]
        public async Task<IEnumerable<ProductHistory>> Get()
        {
            return await _productApplicationService.GetAll();
        }

        [HttpPost]
        [Route("api/GetByName")]
        public async Task<Product> GetbyName([FromBody]string productName)
        {
            return await _productApplicationService.FindByName(productName);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]ProductVm productVm)
        {
            var product = new Product(productVm.Name, productVm.Price);
            
            try
            {
                await _productApplicationService.AddProduct(product);            
                return Created("location", product);
            } 
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest(ex.Message);
            }            
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody]ProductUpdateVm productVm)
        {            
            try
            {
                await _productApplicationService.UpdateProduct(productVm.OldName, productVm.NewName, productVm.Price);            
                return Ok();
            } 
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest(ex.Message);
            }                        
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
