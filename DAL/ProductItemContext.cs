using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CoreWebsiteTest1.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace CoreWebsiteTest1.DAL
{
    public class ProductItemContext : DbContext
    {
        //base("ProductItemContext")
        public ProductItemContext() : base()
        {

        }

        public DbSet<ProductItemModel> ProductItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
