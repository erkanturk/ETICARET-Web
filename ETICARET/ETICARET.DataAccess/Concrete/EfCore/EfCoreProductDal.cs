using ETICARET.DataAccess.Abstract;
using ETICARET.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ETICARET.DataAccess.Concrete.EfCore
{
    public class EfCoreProductDal : EfCoreGenericRepository<Product, DataContext>, IProductDal
    {
        public int GetCountByCategory(string category)
        {
            using (var context = new DataContext())
            {
                var products = context.Products.AsQueryable();//AsQueryable(): LINQ sorgularını daha esnek hale getirmek için kullanılır.
                if (!string.IsNullOrEmpty(category)&&category!="all")
                {
                    products=products.Include(i => i.ProductCategories)
                        .ThenInclude(i => i.Category)
                        .Where(i => i.ProductCategories.Any(a => a.Category.Name.ToLower()==category.ToLower()));
                    return products.Count();//filtrelenmiş ürün sayısını döner
                    //Any(): Belirli bir koşulu sağlayan en az bir öğe olup olmadığını kontrol eder.
                    //Where(): Belirli bir koşulu sağlayan öğeleri filtreler.

                }
                else
                {
                    return products.Include(i=>i.ProductCategories)
                        .ThenInclude(i=>i.Category)
                        .Where(i=>i.ProductCategories.Any())
                        .Count();//koşula bağlı olmadan tüm ürün sayısını döner
                }
            }
        }

        public Product GetProductDetails(int id)
        {
            using (var context = new DataContext())
            {
                return context.Products//Products tablosundan
                    .Where(i=>i.Id==id)//belirli bir id ye sahip ürünü filtreler
                    .Include("Images")//ilişkili Images tablosunu dahil eder
                    .Include("Comments")//ilişkili Comments tablosunu dahil eder
                    .Include(i=>i.ProductCategories)//ilişkili ProductCategories tablosunu dahil eder
                    .FirstOrDefault();//ilk bulunan ürünü döner
            }
        }

        public List<Product> GetProductsByCategory(string category, int page, int pageSize)
        {
            using (var context = new DataContext())
            {
                //Ürünlerin ve resimlerini yükleyerek sorguyu başlatır
                var products = context.Products.Include("Images").AsQueryable();
                if (!string.IsNullOrEmpty(category)&&category!="all")
                {
                    products=products.Include(i => i.ProductCategories)
                        .ThenInclude(i => i.Category)
                        .Where(i => i.ProductCategories.Any(a => a.Category.Name.ToLower()==category.ToLower()));
                }
                return products.Skip((page-1)*pageSize).Take(pageSize).ToList();
                //Skip(): Belirli bir sayıda öğeyi atlar.
                //Take(): Belirli bir sayıda öğeyi alır.
                //ToList(): Sorgu sonucunu listeye dönüştürür ve veritabanından verileri getirir.
                //page-1*pageSize: Atlanacak öğe sayısını hesaplar

            }
        }

        public void Update(Product entity, int[] categoryIds)
        {
            using (var context = new DataContext())
            {
                //Product Tablosuna git ProductCategories ile ilişkilendirilmiş kategorileri yükle
                //ve güncellenecek ürünün Id'si ile eşleşen ürünü bul

                var products = context.Products
                    .Include(i => i.ProductCategories)
                    .FirstOrDefault(i => i.Id == entity.Id);

                if (products is not null)
                {
                    products.Price = entity.Price;
                    products.Name = entity.Name;
                    products.Description = entity.Description;
                    products.ProductCategories=categoryIds.Select(catId => new ProductCategory()
                    {
                        ProductId=entity.Id,
                        CategoryId=catId
                    }).ToList();
                    products.Images = entity.Images;

                }
                //products nesnesi null değilse  Entity den gelen yeni değerlerle günceller

                context.SaveChanges();
            }

        }
        public override void Delete(Product entity)
        {
            using (var context = new DataContext())
            {
                context.Images.RemoveRange(entity.Images);
                context.Products.Remove(entity);
                context.SaveChanges();
            }
        }
        public override List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            using (var context = new DataContext())
            {
                return filter==null?context.Products.Include("Images").ToList()//Eğer filter null ise tüm ürünleri getir
                    : context.Products.Include("Images").Where(filter).ToList();//Değilse filtreye göre ürünleri getir
            }
        }
    }
}
