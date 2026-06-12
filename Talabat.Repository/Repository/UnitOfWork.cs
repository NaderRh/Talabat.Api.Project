using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repository.Contract;
using Talabat.Repository.Data;

namespace Talabat.Repository.Repository
{
   public class UnitOfWork:IUnitOfWork
    {
        // ده اللي هيكلم الداتابيز
        private readonly StoreDbContext _dbcontext;

        // زي الكاش علشان ما استهلكش ميموري علي الفاضي Repositories علشان يشيل ال Hashtable هتعرف 
        private Hashtable _repositories;

        public UnitOfWork(StoreDbContext dbcontext)
        {
            _dbcontext = dbcontext;
            _repositories = new Hashtable();
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            var key=typeof(TEntity).Name;
            // هتروح يحجز مكان جديد علشان هخزن فيه ريبو فيما بعد null ب  Hashtable هتروح تشيك الاول لو ال 
            if (!_repositories.ContainsKey(key))
            {
                var repository = new GenericRepository<TEntity>(_dbcontext);
                _repositories.Add(key, repository);
            }
               return _repositories[key] as IGenericRepository<TEntity>;

            //   Key اللي جايلك وتخزنه علشان تستخدمه ك  Class هتروح تجيب اسم ال 
           
        }

        public async Task<int> CompleteAsync()
        {
            // هتروح تنادي على علشان تسيف كل حاجة مرة واحدة
            return await _dbcontext.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {

            await _dbcontext.DisposeAsync();
        }
    }
}
