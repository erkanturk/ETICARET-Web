using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ETICARET.DataAccess.Abstract
{
    public interface IRepository<T>
    {
        T GetById(int id);//geriye T tipinde bir nesne dönderen int parametreli gövdesiz metot
        T GetOne(Expression<Func<T, bool>> filter = null);//Belirtilen filtreye göre tek bir nesne dönderen metot
        List<T> GetAll(Expression<Func<T, bool>> filter = null);//Belirtilen filtreye göre veya tüm nesneleri dönderen metot
        void Create(T entity);//Yeni bir nesne ekleyen metot
        void Update(T entity);//Var olan bir nesneyi güncelleyen metot
        void Delete(T entity);//Var olan bir nesneyi silen metot

    }
    /*
        Bu yapı, veri erişim katmanında kullanılmak üzere genel bir depo (repository) arayüzü tanımlar.
        Crud (Create, Read, Update, Delete) 
    işlemlerini standartlaştırır ve farklı varlık türleri için tekrar kullanılabilirliği artırır.
    Generic olması sayesinde Product , Category, Order, Comment gibi farklı varlıklar için aynı arayüzü kullanabilirsiniz.
    bu yapıya Generic Repository Pattern denir. 
    */
}
