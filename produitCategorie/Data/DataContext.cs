﻿using Microsoft.EntityFrameworkCore;
using produitCategorie.Models;

namespace produitCategorie.Data
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

            
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categorys { get; set; }
    }
}
