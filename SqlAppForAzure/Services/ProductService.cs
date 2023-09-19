using SqlAppForAzure.Models;
using System.Data.SqlClient;

namespace SqlAppForAzure.Services
{
    public class ProductService
    {
        private static string db_source = "azuresqlserver030199.database.windows.net";
        private static string db_user = "hemant123";
        private static string db_password = "6!GX-rrd";
        private static string db_database = "azuresqldb";

        private SqlConnection GetConnection() {
            var _builder = new SqlConnectionStringBuilder();
            _builder.DataSource = db_source;
            _builder.UserID = db_user;
            _builder.Password = db_password;
            _builder.InitialCatalog = db_database;
            return new SqlConnection(_builder.ConnectionString);
        }

        public List<Product> GetProducts()
        {
            SqlConnection conn = GetConnection();
            var _product_List = new List<Product>();
            string statement = "select productID,ProductName,Quantity from Products";
            conn.Open();
            SqlCommand cmd = new SqlCommand(statement, conn);
            using (SqlDataReader reader = cmd.ExecuteReader()) {
                while (reader.Read()) {
                    Product product = new Product()
                    {
                        ProductID = reader.GetInt32(0),
                        ProductName = reader.GetString(1),
                        Quantity = reader.GetInt32(2)
                    };

                    _product_List.Add(product);
                }
               
            }
            conn.Close();
            return _product_List;
        }
    }
}
