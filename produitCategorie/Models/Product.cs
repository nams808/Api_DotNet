using System.ComponentModel.DataAnnotations.Schema;

namespace produitCategorie.Models
{
    public class Product
    {
        
        public int ProductID { get; set; }
        public string NameProduct { get; set; }
        public string DescriptionProduct { get; set; }     
        //[ForeignKey("CategoryId")]
        public int CategoryId { get; set; }
        
        public Category Category { get; set; }
    }
}
