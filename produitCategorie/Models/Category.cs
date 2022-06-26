using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace produitCategorie.Models
{
    public class Category
    {
        
        public int CategoryID { get; set; }
        public string NameCategory { get; set; }        

        //[JsonIgnore]
        public List<Product> Products { get; set; }
    }
}
