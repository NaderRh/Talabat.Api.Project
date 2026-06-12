using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifcation
{
    public class BaseSpecefication<T> : ISpecification<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Criteria { get; set; }

        public List<Expression<Func<T, object>>> Includes { get; set; } = /*السطر ده بدل اللي تحت علشان امنع تكرار الكود*/ new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>> OrderBy { get; set; }

        public Expression<Func<T, object>> OrderByDescending { get; set; }
        public int Take { get; set; }
        public int Skip { get; set; }
        public bool IsPaginationEnabled { get; set; }
        public List<string> IncludeString { get; set; } = new List<string>();

        public bool IsPagingEnabled { get; set; }


        public BaseSpecefication()
        {
            //Includes = new List<Expression<Func<T,object>>>();]
        }
        public BaseSpecefication(Expression<Func<T, bool>> CriteriaExprsssion)
        {
            Criteria = CriteriaExprsssion;
            //Includes = new List<Expression<Func<T, object>>>();
        }
        public void AddOrderBy(Expression<Func<T, object>> OrderByExprssion)
        {
            OrderBy = OrderByExprssion;
        }
        public void AddOrderByDescending(Expression<Func<T, object>> OrderByDescendingExprssion)
        {
            OrderByDescending = OrderByDescendingExprssion;
        }
        public void AddInclude(Expression<Func<T, object>> IncludeExpression)
        {
            Includes.Add(IncludeExpression);
        }
        public void AddInclude(string IncludeString1)
        {
            IncludeString.Add(IncludeString1);
        }
        public void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPagingEnabled = true;
        }
    }
}
