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

        // GET api/values
        [HttpGet]
        public IEnumerable<ProductDto> Get()
        {
            return _productApplicationService.GetAll();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ProductDto Get(int id)
        {
            return _productApplicationService.FindByName("Teste 2");
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]ProductVm productVm)
        {
            var product = new Product(productVm.Name, productVm.LastPrice);
            _productApplicationService.AddProduct(product);
            Created("", product);
        }

        // PUT api/values/5
        [HttpPut]
        public void Put([FromBody]ProductVm productVm)
        {
            var product = _productApplicationService.FindByName(productVm.Name);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
