using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace prkym.Controllers
{
    public class DBController
    {
        //String[] ime_servisa = new String[1389];
        //String[] opis = new String[1389];
        //public DBController()
        //{
        //    int i = 0, j = 0;
        //    var connString = "Server=192.168.0.42;Port=3306;Uid=mysql;Pwd=travian98;Database=site";
        //    MySqlConnection mySqlConnection = new MySqlConnection(connString);
        //    var sql = "select * from subcats left join services on subcats.service=services.service;";
        //    MySqlCommand mySqlCommand = new MySqlCommand(sql, mySqlConnection);
        //    mySqlConnection.Open();
        //    MySqlDataReader rdr = mySqlCommand.ExecuteReader();
        //    rdr.Read();
        //    String pr = rdr["service"].ToString();
        //    ime_servisa[i++] = rdr["desc_se"].ToString();
        //    opis[j++] = rdr["desc_sc"] + " " + rdr["price"] + " RSD";
        //    while (rdr.Read())
        //    {
        //        if (rdr["service"].ToString() != pr)
        //        {
        //            ime_servisa[i++] = rdr["desc_se"].ToString();
        //            opis[j++] = rdr["desc_sc"] + " " + rdr["price"] + " RSD";
        //            pr = rdr["service"].ToString();
        //        }
        //        else
        //            opis[j++] = rdr["desc_sc"] + " " + rdr["price"] + " RSD";
        //    }
        //}
        //public String[] Ime_Servisa()
        //{
        //    return ime_servisa;
        //}
        //public String[] Opis()
        //{
        //    return opis;
        //}
        readonly List<string> ime_servisa = new List<string>();
        readonly List<string> opis = new List<string>();
        public DBController()
        {
            try
            {
                var connString = "Server=remotemysql.com;Port=3306;Uid=638t2K8utb;Pwd=m6V2OxUk2y;Database=638t2K8utb";
                MySqlConnection mySqlConnection = new MySqlConnection(connString);
                var sql = "select * from subcats left join services on subcats.service=services.service;";
                MySqlCommand mySqlCommand = new MySqlCommand(sql, mySqlConnection);
                mySqlConnection.Open();
                MySqlDataReader rdr = mySqlCommand.ExecuteReader();
                rdr.Read();
                String pr = rdr["service"].ToString();
                ime_servisa.Add(rdr["desc_se"].ToString());
                opis.Add(rdr["desc_sc"] + " " + rdr["price"] + " RSD");
                while (rdr.Read())
                {
                    if (rdr["service"].ToString() != pr)
                    {
                        ime_servisa.Add(rdr["desc_se"].ToString());
                        opis.Add(rdr["desc_sc"] + " " + rdr["price"] + " RSD");
                        pr = rdr["service"].ToString();
                    }
                    else
                        opis.Add(rdr["desc_sc"] + " " + rdr["price"] + " RSD");
                }
                mySqlConnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("'" + ex.Message + "' JE NAS PROBLEM");
            }
        }

        public string[] Ime_Servisa { get { return ime_servisa.ToArray(); } }
        public string[] Opis { get { return opis.ToArray(); } }
    }
}
