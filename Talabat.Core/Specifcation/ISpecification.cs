using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifcation
{
    public interface ISpecification<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Criteria { get; set; }//Where
        public List<Expression<Func<T, object>>> Includes { get; set; }
        public List<string> IncludeString { get; set; }
        //Ascending
        public Expression<Func<T, object>> OrderBy { get; set; }
        public Expression<Func<T, object>> OrderByDescending { get; set; }




        int Take { get; set; }
        int Skip { get; set; }
        bool IsPagingEnabled { get; }





    }
}
