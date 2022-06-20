using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using avows_02.Models;

namespace avows_02.DAL
{
    public class ProdukDAL
    {
        string conString = ConfigurationManager.ConnectionStrings["constr"].ToString();

        //tampilkan data
        public List<Produk> Tampil_all()
        {
            List<Produk> list = new List<Produk>();
            using (SqlConnection con = new SqlConnection(conString))
            { 
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Show_data";
                SqlDataAdapter da=new SqlDataAdapter(cmd);
                DataTable dt=new DataTable();

                con.Open();
                da.Fill(dt);
                con.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    list.Add(new Produk
                    {
                        Kode_Barang = Convert.ToInt32(dr["Kode_Barang"]),
                        Nama_Barang = dr["Nama_Barang"].ToString(),
                        Harga = Convert.ToInt32(dr["Harga"]),
                        Keterangan = dr["Keterangan"].ToString()
                    });
                }

            }
                return list;
        }

        //insert data
        public bool Simpan(Produk Produk)
        {
            int id = 0;
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("simpan",con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Nama_Barang",Produk.Nama_Barang);
                cmd.Parameters.AddWithValue("Harga", Produk.Harga);
                cmd.Parameters.AddWithValue("Keterangan", Produk.Keterangan);

                con.Open();
                id=cmd.ExecuteNonQuery();
                con.Close();

            }

            if (id > 0)
            {
                return true;
            }
            else { 
            return false;
            }
        }

        //get id
        public List<Produk> Get_id( int  ProdukID)
        {
            List<Produk> list = new List<Produk>();
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "tampil";
                cmd.Parameters.AddWithValue("Kode_Barang", ProdukID);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                con.Open();
                da.Fill(dt);
                con.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    list.Add(new Produk
                    {
                        Kode_Barang = Convert.ToInt32(dr["Kode_Barang"]),
                        Nama_Barang = dr["Nama_Barang"].ToString(),
                        Harga = Convert.ToInt32(dr["Harga"]),
                        Keterangan = dr["Keterangan"].ToString()
                    });
                }

            }
            return list;
        }

        //update
        public bool Ubah(Produk Produk)
        {
            int i= 0;
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("ubah", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Kode_Barang", Produk.Kode_Barang);
                cmd.Parameters.AddWithValue("Nama_Barang", Produk.Nama_Barang);
                cmd.Parameters.AddWithValue("Harga", Produk.Harga);
                cmd.Parameters.AddWithValue("Keterangan", Produk.Keterangan);

                con.Open();
                i= cmd.ExecuteNonQuery();
                con.Close();

            }

            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //hapus
        public string HapusProduk(int ProdukID)
        {
            string result = "";

            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("hapus", con);
                cmd.CommandType= CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@kode_barang",ProdukID);
                cmd.Parameters.Add("@OUTPUTMESSAGE", SqlDbType.VarChar,50).Direction=ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                result = cmd.Parameters["@OUTPUTMESSAGE"].Value.ToString();
                con.Close();
            }
            return result;
        }
    }
}