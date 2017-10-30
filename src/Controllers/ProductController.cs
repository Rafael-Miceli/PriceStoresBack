using System;
using Api.ViewModels;
using Api.ApplicationServices.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Api.Models;
using Microsoft.Extensions.Configuration;

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
        public async Task<IEnumerable<ProductResumeGroupedByCategoryVm>> Get()
        {
            try
            {
                var products = await _productApplicationService.GetAll();
                var productsResume = products
                                    .GroupBy(g => g.ProductsOfThePast.Last().Category)
                                    .Select(p => new ProductResumeGroupedByCategoryVm
                                    {
                                        CategoryName = p.Key == null ? "Sem Categoria" : p.Key.Name,
                                        Products = p.Select(s => new ProductResumeVm {
                                            Name = s.ProductsOfThePast.Last().Name,
                                            LastPrice = s.ProductsOfThePast.Last().Price,
                                            HigherPrice = s.ExpensiverPrice,
                                            LowerPrice = s.CheaperPrice
                                        })
                                    });

                return productsResume;
            }
            catch (Exception ex)
            {
                throw ex;
            }            
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
                return Ok(productVm);
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
