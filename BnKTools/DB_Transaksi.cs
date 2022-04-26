using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace BnKTools
{
    class DB_Transaksi
    {
        public static MySqlConnection Connect()
        {
            string sql = "Server = localhost; uid = root; password = ; database = bnktools;";
            MySqlConnection conn = new MySqlConnection(sql);
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database tidak terhubung : " + ex.Message);
            }

            return conn;
        }

        public static void Add_Transaksi(Transaksi transaksi)
        {
            string sql = "INSERT INTO transaksi VALUES (NULL, @kode_transaksi, @total_transaksi, " +
                "@currency_transaksi, @buy_transaksi, @sell_transaksi, @date_transaksi, @profit_transaksi);";

            MySqlConnection conn = Connect();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.Add("@kode_transaksi", MySqlDbType.VarChar).Value = transaksi.kode;
            cmd.Parameters.Add("@total_transaksi", MySqlDbType.Double).Value = transaksi.total;
            cmd.Parameters.Add("@currency_transaksi", MySqlDbType.VarChar).Value = transaksi.currency;
            cmd.Parameters.Add("@buy_transaksi", MySqlDbType.Double).Value = transaksi.buy;
            cmd.Parameters.Add("@sell_transaksi", MySqlDbType.Double).Value = transaksi.sell;
            cmd.Parameters.Add("@date_transaksi", MySqlDbType.DateTime).Value = transaksi.date;
            cmd.Parameters.Add("@profit_transaksi", MySqlDbType.Double).Value = transaksi.profit;

            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Transaksi berhasil ditambahkan.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("MySQL Error : " + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            conn.Close();
        }

        public static Transaksi Read_Transaksi(int id)
        {
            string sql = "SELECT * FROM transaksi WHERE id = @id_transaksi;";

            MySqlConnection conn = Connect();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.Add("@id_transaksi", MySqlDbType.Int32).Value = id;

            try
            {
                Transaksi transaksi = new Transaksi();

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        transaksi.id = int.Parse(reader[0].ToString());
                        transaksi.kode = reader[1].ToString();
                        transaksi.total = double.Parse(reader[2].ToString());
                        transaksi.currency = reader[3].ToString();
                        transaksi.buy = double.Parse(reader[4].ToString());
                        transaksi.sell = double.Parse(reader[5].ToString());
                        transaksi.date = DateTime.Parse(reader[6].ToString());
                        transaksi.profit = double.Parse(reader[7].ToString());
                    }
                    reader.Close();
                }

                return transaksi;
            }
            catch (Exception ex)
            {
                MessageBox.Show("MySQL Error : " + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            conn.Close();
            return null;
        }

        public static void Update_Transaksi(Transaksi transaksi, int id)
        {
            string sql = "UPDATE transaksi SET  kode = @kode_transaksi, total = @total_transaksi, currency = @currency_transaksi," + 
                " buy = @buy_transaksi, sell = @sell_transaksi, date = @date_transaksi, profit = @profit_transaksi" + 
                " WHERE id = @id_transaksi;";

            MySqlConnection conn = Connect();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.Add("@id_transaksi", MySqlDbType.Int32).Value = id;
            cmd.Parameters.Add("@kode_transaksi", MySqlDbType.VarChar).Value = transaksi.kode;
            cmd.Parameters.Add("@total_transaksi", MySqlDbType.Double).Value = transaksi.total;
            cmd.Parameters.Add("@currency_transaksi", MySqlDbType.VarChar).Value = transaksi.currency;
            cmd.Parameters.Add("@buy_transaksi", MySqlDbType.Double).Value = transaksi.buy;
            cmd.Parameters.Add("@sell_transaksi", MySqlDbType.Double).Value = transaksi.sell;
            cmd.Parameters.Add("@date_transaksi", MySqlDbType.DateTime).Value = transaksi.date;
            cmd.Parameters.Add("@profit_transaksi", MySqlDbType.Double).Value = transaksi.profit;

            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Transaksi berhasil diubah.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("MySQL Error : " + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            conn.Close();
        }

        public static void Delete_Transaksi(int id)
        {
            string sql = "DELETE FROM transaksi WHERE id = @id_transaksi;";

            MySqlConnection conn = Connect();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.Add("@id_transaksi", MySqlDbType.Int32).Value = id;

            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Transaksi berhasil dihapus.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("MySQL Error : " + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static List<Transaksi> Read_All_Transaksi()
        {
            string sql = "SELECT * FROM transaksi;";

            MySqlConnection conn = Connect();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;

            List<Transaksi> list_transaksi = new List<Transaksi>();

            try
            {
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Transaksi transaksi = new Transaksi();

                        transaksi.id = int.Parse(reader[0].ToString());
                        transaksi.kode = reader[1].ToString();
                        transaksi.total = double.Parse(reader[2].ToString());
                        transaksi.currency = reader[3].ToString();
                        transaksi.buy = double.Parse(reader[4].ToString());
                        transaksi.sell = double.Parse(reader[5].ToString());
                        transaksi.date = DateTime.Parse(reader[6].ToString());
                        transaksi.profit = double.Parse(reader[7].ToString());

                        list_transaksi.Add(transaksi);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("MySQL Error : " + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            conn.Close();
            return list_transaksi;
        }

        public static List<Transaksi> Query(string keyword, DateTime startDateTime, DateTime endDateTime)
        {
            string sql = "SELECT * FROM transaksi WHERE kode LIKE '%" + keyword + "%' AND date >= '" +
                startDateTime.ToString("yyyy-MM-dd") + " 00:00:00' AND date < '" + 
                endDateTime.ToString("yyyy-MM-dd") + " 23:59:59';";

            MySqlConnection conn = Connect();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;

            List<Transaksi> list_transaksi = new List<Transaksi>();

            try
            {
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Transaksi transaksi = new Transaksi();

                        transaksi.id = int.Parse(reader[0].ToString());
                        transaksi.kode = reader[1].ToString();
                        transaksi.total = double.Parse(reader[2].ToString());
                        transaksi.currency = reader[3].ToString();
                        transaksi.buy = double.Parse(reader[4].ToString());
                        transaksi.sell = double.Parse(reader[5].ToString());
                        transaksi.date = DateTime.Parse(reader[6].ToString());
                        transaksi.profit = double.Parse(reader[7].ToString());

                        list_transaksi.Add(transaksi);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Query Error : " + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            conn.Close();
            return list_transaksi;
        }

        public static double Read_Value(string s, bool average, DateTime startDateTime, DateTime endDateTime)
        {
            string sql = "";

            if (average) sql = "SELECT AVG(profit) ";
            else sql = "SELECT SUM(profit) ";

            sql += "FROM transaksi WHERE date >= '" +
                startDateTime.ToString("yyyy-MM-dd") + " 00:00:00' AND date < '" +
                endDateTime.ToString("yyyy-MM-dd") + " 23:59:59'";

            if (s == "profit")
            {
                sql += " AND profit > 0";
            }
            else if(s == "loss")
            {
                sql += " AND profit < 0";
            }

            sql += ";";

            MySqlConnection conn = Connect();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;

            double value = 0;
            try
            {
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader[0] == DBNull.Value) return (double)0.00;
                        value = double.Parse(reader[0].ToString());
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("MySQL Error : " + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            conn.Close();
            return value;
        }
    }
}
