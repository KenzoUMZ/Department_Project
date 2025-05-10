using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventarioMed.Shared.Model;

namespace InventarioMed.Shared.Data
{
    public class CategoryDAL
    {
        private readonly InventarioMedContext context;

        public CategoryDAL()
        {
            context = new InventarioMedContext();
        }

        public void Create(Category category)
        {
            context.Category.Add(category);
            context.SaveChanges();
        }

        public IEnumerable<Category> Read()
        {
            return context.Category.ToList();
        }

        public Category? ReadByName(string name)
        {
            return context.Category.FirstOrDefault(x => x.Name == name);
        }

        public void Update(Category category)
        {
            context.Category.Update(category);
            context.SaveChanges();
        }

        public void Delete(Category category)
        {
            context.Category.Remove(category);
            context.SaveChanges();
        }
    }
}
