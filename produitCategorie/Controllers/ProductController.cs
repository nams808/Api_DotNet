using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using produitCategorie.Data;
using produitCategorie.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace produitCategorie.Controllers
{
    
    [Route("api")]    
    public class ProductController : ControllerBase
    {
        private readonly DataContext _context;

        public ProductController(DataContext context)
        {
            _context = context;
        }

        //localhost:44382/api/GetProducts
        //Récupérer tout les produits
        [HttpGet("GetProducts")]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            //ToList();
            return await _context.Products.ToListAsync();
        }

        //localhost:44382/api/GetProduct/1
        //Récupérer un produit en fonction de sa category
        [HttpGet("GetProduct/{Id}")]
        public async Task<ActionResult<Product>> GetOneProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        //localhost:44382/api/GetProductsByPage?page=1&size=2
        // l'exemple ci-dessus indique qu'il affichera 2 produits sur la page 1
        //Récupérer les produits par page        
        [HttpGet("GetProductsByPage")]
        public IEnumerable<Product> GetProductsByPage(int page=0, int size=1)
        { 

            int skipValue = (page - 1) * size;
            return _context.Products
                .Include(p => p.Category)
                .Skip(skipValue)
                .Take(size);
        }

        //localhost:44382/api/ProductSearch?keyword=  "la lettre recherché"
        //Recherche tout les produits dont le nom incluera le motclé
        [HttpGet("ProductSearch")]
        public IEnumerable<Product> Search(string keyword)
        {
            //ToList();
            return _context.Products
                .Include(p => p.Category)
                .Where(p => p.NameProduct.Contains(keyword));
        }

        //localhost:44382/api/AddProduct
        //Ajouter un Produit
        [HttpPost("AddProduct")]
        public Product AddProduct([FromBody] Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return product;
        }

        //localhost:44382/api/UpdateProduct/1
        //Modifier un produit
        [HttpPut("UpdateProduct/{Id}")]
        public Product UpdateProduct([FromBody] Product product, int Id)
        {
            product.ProductID = Id;
            _context.Products.Update(product);
            _context.SaveChanges();
            return product;
        }

        //localhost:44382/api/RemoveProduct/1
        //Supprimer un produit
        [HttpDelete("RemoveProduct/{Id}")]
        public void DeleteProduct(int Id)
        {
            Product product = _context.Products.FirstOrDefault(p => p.ProductID == Id);
            _context.Remove(product);
            _context.SaveChanges();
        }
    }
}
