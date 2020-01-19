using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using prkym.Models;

namespace prkym.Services
{
    public class MockDataStore : IDataStore<Item>
    {
        List<Item> items;

        const string connString = "Server=remotemysql.com;Port=3306;Uid=F3B1HGDzLU;Pwd=FEbHnRWLzi;Database=F3B1HGDzLU";
        readonly static MySqlConnection conn = new MySqlConnection(connString);
        MySqlCommand cmd = new MySqlCommand(null, conn);

        public void ListServices()
        {
            try
            {
                items = new List<Item>();
                cmd.CommandText = "select * from services";
                conn.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    items.Add(new Item { Id = int.Parse(rdr["id"].ToString()), Text = rdr["service"].ToString(), Description = rdr["desc_se"].ToString() });
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            try
            {
                cmd.CommandText = "INSERT INTO services (service, desc_se) VALUES ('" + item.Text + "', '" + item.Description + "'); SELECT LAST_INSERT_ID()";
                conn.Open();
                var res = cmd.ExecuteScalar();
                item.Id = int.Parse(res.ToString());
                items.Add(item);
                conn.Close();
                return res != null ? await Task.FromResult(true) : await Task.FromResult(false);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return await Task.FromResult(false);
            }
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            try
            {
                bool res;
                cmd.CommandText = "UPDATE services SET service = '" + item.Text + "', desc_se = '" + item.Description + "' WHERE services.id = " + item.Id;
                conn.Open();
                if (cmd.ExecuteNonQuery() == 0) res = false;
                else res = true;
                conn.Close();
                return await Task.FromResult(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return await Task.FromResult(false);
            }
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            try
            {
                bool res;
                cmd.CommandText = "DELETE FROM services WHERE services.id = " + id;
                conn.Open();
                if (cmd.ExecuteNonQuery() == 0) res = false;
                else res = true;
                conn.Close();
                var oldItem = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
                if (res == true) items.Remove(oldItem);
                return await Task.FromResult(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return await Task.FromResult(false);
            }
        }

        public async Task<Item> GetItemAsync(int id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            ListServices();
            return await Task.FromResult(items);
        }
    }
}