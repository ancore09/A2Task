using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using A2Task;

namespace A2_test_task
{
    public class DBManager
    {
        private static string connectionString = "Server=localhost,1433;Database=A2TestDB;User Id=sa;Password=Out@ofstyle;";

        private static DBManager instance = null;

        public static DBManager GetInstance()
        {
            if (instance == null)
            {
                instance = new DBManager();
            }

            return instance;
        }

        public void InsertRow(Model obj)
        {
            var con = new SqlConnection(connectionString);
            con.Open();
            string req = $"insert into deals (sellerName, sellerInn, buyerName, buyerInn, woodVolumeBuyer, woodVolumeSeller, dealDate, dealNumber) " +
                         $"values (@sellerName, @sellerInn, @buyerName, @buyerInn, @woodVolumeBuyer, @woodVolumeSeller, @dealDate, @dealNumber)";
            SqlCommand command = new SqlCommand(req, con);
            command.Parameters.Add(new SqlParameter("sellerName", obj.SellerName));
            command.Parameters.Add(new SqlParameter("sellerInn", obj.SellerInn));
            command.Parameters.Add(new SqlParameter("buyerName", obj.BuyerName));
            command.Parameters.Add(new SqlParameter("buyerInn", obj.BuyerInn));
            command.Parameters.Add(new SqlParameter("woodVolumeBuyer", obj.WoodVolumeBuyer));
            command.Parameters.Add(new SqlParameter("woodVolumeSeller", obj.WoodVolumeSeller));
            command.Parameters.Add(new SqlParameter("dealDate", obj.DealDate));
            command.Parameters.Add(new SqlParameter("dealNumber", obj.DealNumber));
            try
            {
                command.ExecuteNonQuery();
                Console.WriteLine("Row was inserted");
            }
            catch (Exception e)
            {
                Console.WriteLine(obj);
            }
            con.Close();
        }
        
        public List<Model> GetRows(int amount)
        {
            List<Model> list = new List<Model>();

            var con = new SqlConnection(connectionString);
            con.Open();
            string req = $"select top {amount} * from deals order by dealDate desc";
            SqlCommand command = new SqlCommand(req, con);
            try
            {
                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string sellerName = reader.GetString(1);
                        string sellerInn = reader.GetString(2);
                        string buyerName = reader.GetString(3);
                        string buyerInn = reader.GetString(4);
                        double woodVolumeBuyer = Double.Parse(reader.GetValue(5).ToString());
                        double woodVolumeSeller = Double.Parse(reader.GetValue(6).ToString());
                        DateTime dealDate = reader.GetDateTime(7);
                        string dealNumber = reader.GetString(8);
                        
                        list.Add(new Model(sellerName, sellerInn, buyerName, buyerInn, woodVolumeBuyer, woodVolumeSeller, dealDate, dealNumber));
                    }
                }
                
                Console.WriteLine("Rows were fetched");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            con.Close();
            return list;
        }
    }
}