using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Autok
{
    internal class Adatbazis
    {
        MySqlConnection conn = null;
        MySqlCommand sql = null;

        // Konstruktok, kapcsolat beállítása
        public Adatbazis()
        {
            MySqlConnectionStringBuilder sb = new MySqlConnectionStringBuilder();
            sb.Server = "localhost";
            sb.UserID = "root";
            sb.Password = "";
            sb.Database = "autok";
            sb.CharacterSet = "utf8";
            conn = new MySqlConnection(sb.ConnectionString);
            sql = conn.CreateCommand();
            try
            {
                kapcsolatNyit();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                Environment.Exit(0);
            }
            finally
            {
                kapcsolatZar();
            }
        }

        private void kapcsolatZar()
        {
            if (conn.State != System.Data.ConnectionState.Closed)
            {
                conn.Close();
            }
        }

        private void kapcsolatNyit()
        {
            if (conn.State != System.Data.ConnectionState.Open)
            {
                conn.Open();
            }
        }
        
        // Adatbázisból betöltés
        internal List<Auto> getAllAuto()
        {
            List<Auto> autok = new List<Auto>();
            sql.CommandText = "SELECT * FROM auto ORDER BY `marka`";

            try
            {
                kapcsolatNyit();
                using (MySqlDataReader dr = sql.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        string rendszam = dr.GetString("rendszam");
                        string marka = dr.GetString("marka");
                        string modell = dr.GetString("modell");
                        int gyartasiEv = dr.GetInt32("gyartasiev");
                        DateTime forgalmiErvenyesseg = dr.GetDateTime("forgalmiErvenyesseg");
                        int vetelAr = dr.GetInt32("vetelar");
                        int kmAllas = dr.GetInt32("kmallas");
                        int hengerurtartalom = dr.GetInt32("hengerűrtartalom");
                        int tomeg = dr.GetInt32("tomeg");
                        int teljesitmeny = dr.GetInt32("teljesitmeny");
                        autok.Add(new Auto(rendszam, marka, modell, gyartasiEv, forgalmiErvenyesseg, vetelAr, kmAllas, hengerurtartalom, tomeg, teljesitmeny));
                    }
                }
            }
            catch (MySqlException ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                kapcsolatZar();

            }
            return autok;
        }

        internal void autoModositas(Auto auto)
        {
            try
            {
                kapcsolatNyit();
                sql.CommandText = "UPDATE `auto` SET `rendszam`=@rendszam,`marka`=@marka,`modell`=@modell,`gyartasiev`=@gyartasiev,`forgalmiErvenyesseg`=@forgalmiErvenyesseg,`vetelar`=@vetelar,`kmallas`=@kmallas,`hengerűrtartalom`=@hengerűrtartalom,`tomeg`=@tomeg,`teljesitmeny`=@teljesitmeny WHERE `rendszam`=@Rendszam";
                sql.Parameters.Clear();
                sql.Parameters.AddWithValue("@Rendszam", auto.Rendszam);
                sql.Parameters.AddWithValue("@Marka", auto.Marka);
                sql.Parameters.AddWithValue("@Modell", auto.Modell);
                sql.Parameters.AddWithValue("@GyartasiEv", auto.GyartasiEv);
                sql.Parameters.AddWithValue("@ForgalmiErvenyesseg", auto.ForgalmiErvenyesseg);
                sql.Parameters.AddWithValue("@VetelAr", auto.VetelAr);
                sql.Parameters.AddWithValue("@KmAllas", auto.KmAllas);
                sql.Parameters.AddWithValue("@Hengerűrtartalom", auto.Hengerurtartalom);
                sql.Parameters.AddWithValue("@Tomeg", auto.Tomeg);
                sql.Parameters.AddWithValue("@Teljesitmeny", auto.Teljesitmeny);
                sql.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {

                MessageBox.Show("Hiba történt a módosítás során: " + ex.Message);
            }
            finally { kapcsolatZar(); }

        }

        internal void autoHozzaadas(Auto auto)
        {
            try
            {
                kapcsolatNyit();
                sql.CommandText = "INSERT INTO `auto`(`rendszam`, `marka`, `modell`, `gyartasiev`, `forgalmiErvenyesseg`, `vetelar`, `kmallas`, `hengerűrtartalom`, `tomeg`, `teljesitmeny`) VALUES (@Rendszam, @Marka, @Modell, @GyartasiEv, @ForgalmiErvenyesseg, @VetelAr, @KmAllas, @Hengerurtartalom, @Tomeg, @Teljesitmeny)";
                sql.Parameters.AddWithValue("@Rendszam", auto.Rendszam);
                sql.Parameters.AddWithValue("@Marka", auto.Marka);
                sql.Parameters.AddWithValue("@Modell", auto.Modell);
                sql.Parameters.AddWithValue("@GyartasiEv", auto.GyartasiEv);
                sql.Parameters.AddWithValue("@ForgalmiErvenyesseg", auto.ForgalmiErvenyesseg);
                sql.Parameters.AddWithValue("@VetelAr", auto.VetelAr);
                sql.Parameters.AddWithValue("@KmAllas", auto.KmAllas);
                sql.Parameters.AddWithValue("@Hengerurtartalom", auto.Hengerurtartalom);
                sql.Parameters.AddWithValue("@Tomeg", auto.Tomeg);
                sql.Parameters.AddWithValue("@Teljesitmeny", auto.Teljesitmeny);
                sql.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                kapcsolatZar();
            }
           
        }

        internal void autoTorles(Auto auto)
        {
            try
            {
                sql.CommandText = "DELETE FROM `auto` WHERE `rendszam` = @rendszam";
                sql.Parameters.Clear();
                sql.Parameters.AddWithValue("@rendszam", auto.Rendszam);
                kapcsolatNyit();
                sql.ExecuteNonQuery();
            }
            catch (MySqlException)
            {

                MessageBox.Show("Hiba történt a törlés során!");
            }
            finally { kapcsolatZar(); }
        }
    }
}
