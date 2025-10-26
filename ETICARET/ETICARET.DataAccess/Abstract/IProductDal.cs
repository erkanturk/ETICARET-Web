using ETICARET.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETICARET.DataAccess.Abstract
{
    public interface IProductDal:IRepository<Product>
    {
        int GetCountByCategory(string category);
        Product GetProductDetails(int id);
        List<Product> GetProductsByCategory(string category, int page, int pageSize);//sayfalama yapısı için bir kategoriye göre ürünleri dönderen metot
        void Update(Product entity, int[] categoryIds);//ürün güncelleme işlemi için kategori id lerini de alan metot
    }
}
