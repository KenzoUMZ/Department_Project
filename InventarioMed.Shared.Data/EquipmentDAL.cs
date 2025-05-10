using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using InventarioMed.Shared.Model;
using InventarioMed.Shared.Data;

namespace InventarioMed.Shared.Data
{
    public class EquipmentDAL
    {
        private readonly InventarioMedContext context;

        public EquipmentDAL()
        {
            context = new InventarioMedContext();
        }

        public void Create(Equipment eqp)
        {

            context.Equipment.Add(eqp);
            context.SaveChanges();
        }
        public IEnumerable<Equipment> Read()
        {
            return context.Equipment.ToList();
        }
        public Equipment? ReadByName(string name)
        {

            return context.Equipment.FirstOrDefault(x => x.Name == name);
        }

        public void Update(Equipment eqp)
        {
            context.Equipment.Update(eqp);
            context.SaveChanges();
        }
        public void Delete(Equipment epq)
        {
            context.Equipment.Remove(epq);
            context.SaveChanges();
        }
    }
}
