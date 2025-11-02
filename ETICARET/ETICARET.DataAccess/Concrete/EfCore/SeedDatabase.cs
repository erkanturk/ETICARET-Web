using ETICARET.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETICARET.DataAccess.Concrete.EfCore
{
    public class SeedDatabase
    {
        public static void Seed()
        {
            var context = new DataContext();
            if (context.Database.GetPendingMigrations().Count()==0)
            {
                if (context.Categories.Count()==0)
                {
                    context.AddRange(Categories);
                }
                if (context.Products.Count()==0)
                {
                    context.AddRange(Products);
                    context.AddRange(ProductCategories);
                }
                context.SaveChanges();
            }
        }
        //Her Kategory için 5 ürün
        private static Category[] Categories =
        {
            new Category(){Name="Telefon"},
            new Category(){Name="Bilgisayar"},
            new Category(){Name="Elektronik"},
            new Category(){Name="Ev Gereçleri"},
        };
        private static Product[] Products =
        {
            new Product(){ Name = "Monster notebook" , Price = 22000, Images = { new Image() {ImageUrl = "monster2.jpg" },  new Image() {ImageUrl = "monster.jpg" }, new Image() {ImageUrl = "monster2.jpg" }, new Image() {ImageUrl = "monster.jpg" } },Description ="<p>Güzel laptop</p>" },
            new Product(){ Name = "Monster notebook" , Price = 22000, Images = { new Image() {ImageUrl = "monster2.jpg" },  new Image() {ImageUrl = "monster.jpg" }, new Image() {ImageUrl = "monster2.jpg" }, new Image() {ImageUrl = "monster.jpg" } },Description ="<p>Güzel laptop</p>" },
            new Product(){ Name = "Monster notebook" , Price = 22000, Images = { new Image() {ImageUrl = "monster2.jpg" },  new Image() {ImageUrl = "monster.jpg" }, new Image() {ImageUrl = "monster2.jpg" }, new Image() {ImageUrl = "monster.jpg" } },Description ="<p>Güzel laptop</p>" },
            new Product(){ Name = "Monster notebook" , Price = 22000, Images = { new Image() {ImageUrl = "monster2.jpg" },  new Image() {ImageUrl = "monster.jpg" }, new Image() {ImageUrl = "monster2.jpg" }, new Image() {ImageUrl = "monster.jpg" } },Description ="<p>Güzel laptop</p>" }
        };

        private static ProductCategory[] ProductCategories =
        {
            new ProductCategory(){Product=Products[0],Category=Categories[1]},
            new ProductCategory(){Product=Products[1],Category=Categories[0]},
            new ProductCategory(){Product=Products[2],Category=Categories[3]},
        };
    }
}
