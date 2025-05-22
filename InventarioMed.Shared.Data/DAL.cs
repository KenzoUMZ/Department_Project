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
        public void Create(T value)
        {
            context.Set<T>().Add(value);
            context.SaveChanges();
        }
        public IEnumerable<T> Read()
        {
            return context.Set<T>().ToList();
        }
        public void Update(T value)
        {
            context.Set<T>().Update(value);
            context.SaveChanges();
        }
        public void Delete(T value)
        {
            context.Set<T>().Remove(value);
            context.SaveChanges();
        }
        public T? ReadBy(Func<T, bool> predicate)
        {
            return context.Set<T>().FirstOrDefault(predicate);
        }
    }
}
