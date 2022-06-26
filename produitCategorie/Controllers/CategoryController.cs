using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using produitCategorie.Data;
using produitCategorie.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace produitCategorie.Controllers
{
    
    [Route("api")]
    //[ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly DataContext _context;

        public CategoryController(DataContext context)
        {
            _context = context;
        }

        //localhost:44382/api/GetCategorys
        [HttpGet("GetCategorys")]
        public async Task<ActionResult<IEnumerable<Category>>> GetAllCategorys()
        {
            //ToList();
            return await _context.Categorys.ToListAsync();
        }

        //localhost:44382/api/GetCategory/1
        [HttpGet("GetCategory/{Id}")]
        public async Task<ActionResult<Category>> GetOneCategory(int id)
        {
            var category = await  _context.Categorys.FindAsync(id);

            if(category == null)
            {
                return NotFound();
            }

            return category;
        }
        

        //localhost:44382/api/GetProductByCategory/1
        //Récupérer les produits par catégorie ou récupérer une categorie avec tout ses produits
        [HttpGet("GetProductsByCategory/{Id}")]
        public   IEnumerable<Product> GetProductsByCategory(int Id)
        {
            Category category =  _context.Categorys
                .Include(c => c.Products)
                .FirstOrDefault(c => c.CategoryID == Id);
            return  category.Products;
        }

        //localhost:44382/api/AddCategory/
        [HttpPost("AddCategory")]
        public Category AddCategory([FromBody] Category category)
        {
            _context.Categorys.Add(category);
            _context.SaveChanges();
            return category;
        }

        //localhost:44382/api/UpdateCategory/1
        [HttpPut("UpdateCategory/{Id}")]
        public Category UpdateCategory([FromBody] Category category, int Id)
        {
            category.CategoryID = Id;
            _context.Categorys.Update(category);
            _context.SaveChanges();
            return category;
        }

        //localhost:44382/api/RemoveCategory/1
        [HttpDelete("RemoveCategory/{Id}")]
        public void DeleteCategory(int Id)
        {
            Category category = _context.Categorys.FirstOrDefault(c => c.CategoryID == Id);
            _context.Remove(category);
            _context.SaveChanges();            
        }
    }
}
