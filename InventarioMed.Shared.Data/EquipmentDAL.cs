using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using InventarioMed.Shared.Model;

namespace InventarioMed.Shared.Data
{
    public class EquipmentDAL
    {
        public void Create(Equipment equipment)
        {
            InventarioMedContext con = new InventarioMedContext();

            using var connection = con.Connect();
            connection.Open();
            string sqlQuery = "INSERT INTO Equipment (Name, Manufacturer) VALUES (@Name, @Manufacturer)";
            using var command = new SqlCommand(sqlQuery, connection);
            command.Parameters.AddWithValue("@Name", equipment.Name);
            command.Parameters.AddWithValue("@Manufacturer", equipment.Manufacturer);
            command.ExecuteNonQuery();
        }

        public IEnumerable<Equipment> Read()
        {
            var list = new List<Equipment>();
            InventarioMedContext con = new InventarioMedContext();

            using var connection = con.Connect();
            connection.Open();

            string sqlQuery = "SELECT * FROM Equipment";
            using var command = new SqlCommand(sqlQuery, connection);

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                // Use null-coalescing operator to handle possible null values
                string eqpName = Convert.ToString(reader["Name"]) ?? string.Empty;
                string eqpManufacturer = Convert.ToString(reader["Manufacturer"]) ?? string.Empty;
                Equipment equipment = new Equipment(eqpName, eqpManufacturer);
                int eqpId = Convert.ToInt32(reader["Id"]);

                equipment.Id = eqpId;

                list.Add(equipment);
                Console.WriteLine(equipment);
            }
            return list;
        }

        public void Update(Equipment equipment, int id)
        {
            using var connection = new InventarioMedContext().Connect();
            connection.Open();

            string sqlQuery = "UPDATE Equipment SET Name = @name," +
                "Manufacturer = @Manufacturer WHERE Id = @id";

            using var command = new SqlCommand(sqlQuery, connection);
            command.Parameters.AddWithValue("@Name", equipment.Name);
            command.Parameters.AddWithValue("@Manufacturer", equipment.Manufacturer);
            command.Parameters.AddWithValue("@Id", id);

            int response = command.ExecuteNonQuery();
            Console.WriteLine($"Equipamento atualizado com sucesso! {response} linhas afetadas");
        }
        public void Delete(int id)
        {

            using var connection = new InventarioMedContext().Connect();
            connection.Open();

            string sqlQuery = "DELETE FROM Equipment WHERE Id = @id";

            using var command = new SqlCommand(sqlQuery, connection);
            command.Parameters.AddWithValue("@id", id);
            int response = command.ExecuteNonQuery();

            if (response > 0)
            {
                Console.WriteLine($"Equipamento excluído com sucesso! {response} linhas afetadas");
            }
            else
            {
                Console.WriteLine("Nenhum equipamento encontrado com o ID fornecido.");
            }
        }
    }
}
