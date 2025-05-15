using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Department.Shared.Data
{
    public class DAL<T> where T : class
    {
        private readonly DepartmentContext context;
        public DAL()
        {
            context = new DepartmentContext();
        }

        public void Create(T entity) => context.Set<T>().Add(entity);

        public IEnumerable<T> Read() => context.Set<T>().ToList();


        public void Update(T entity)
        {
            context.Set<T>().Update(entity);
            context.SaveChanges();
        }

        public T? ReadBy(Func<T, bool> predicate) => context.Set<T>().FirstOrDefault(predicate);
    }
}
