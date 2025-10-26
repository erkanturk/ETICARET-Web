using ETICARET.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETICARET.DataAccess.Abstract
{
    public interface ICategoryDal: IRepository<Category>
    {
        void DeleteFromCategory(int categoryId, int productId);//Bir ürünü bir kategoriden silmek için kullanılan metot
        Category GetByIdWithProducts(int id);//Kategori id sine göre kategoriyi ve o kategoriye ait ürünleri getiren metot
    }
}
