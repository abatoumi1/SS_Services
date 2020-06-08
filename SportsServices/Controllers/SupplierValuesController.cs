using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SportsServices.Models;
using SportsServices.Models.BindingTargets;
using System.Collections.Generic;

namespace SportsServices.Controllers {

    [Route("api/suppliers")]
    [EnableCors("AllowAnyOrigin")]
    public class SupplierValuesController : Controller {
        private DataContext context;

        public SupplierValuesController(DataContext ctx) {
            context = ctx;
        }

        [HttpGet]
        public IEnumerable<Supplier> GetSuppliers() {
            return context.Suppliers;
        }

        [HttpPost]
        public IActionResult CreateSupplier([FromBody]SupplierData sdata) {
            if (ModelState.IsValid) {
                Supplier s = sdata.Supplier;
                context.Add(s);
                context.SaveChanges();
                return Ok(s.SupplierId);
            } else {
                return BadRequest(ModelState);
            }
        }

        [HttpPut("{id}")]
        public IActionResult ReplaceSupplier(long id, 
                [FromBody] SupplierData sdata) {
            if (ModelState.IsValid) {
                Supplier s = sdata.Supplier;
                s.SupplierId = id;
                context.Update(s);
                context.SaveChanges();
                return Ok();
            } else {
                return BadRequest(ModelState);
            }
        }        

        [HttpDelete("{id}")]
        public void DeleteSupplier(long id) {
            context.Remove(new Supplier { SupplierId = id });
            context.SaveChanges();
        }        
    }
}
