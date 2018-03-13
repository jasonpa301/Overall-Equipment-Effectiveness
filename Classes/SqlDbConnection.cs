using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;

namespace oeeps
{
    public class SqlDbConnection  //class to connect to database
    {
        private SqlConnection _con;
        public SqlCommand com { get; private set; }
        private SqlDataAdapter daAd;
        private DataTable dt;
        private SqlDataReader reader;
        
        public SqlDbConnection(string connectionString=null)
        {
            _con = new SqlConnection(connectionString??@"Data Source = XXXXXXXXXXXX; Initial Catalog = oee; Integrated Security = False; User ID = psf; Password = XXXXXX; Connect Timeout = 60; Encrypt = False; TrustServerCertificate = True; ApplicationIntent = ReadWrite; MultiSubnetFailover = False");
            _con.Open();
			//database connection hidden as its public on github
        }
        
        public void SetCommand(string query)
        {
            com = new SqlCommand(query, _con);
        }
        
        public int IntScalar()
        {
            try
            {
                return (int)com.ExecuteScalar();
            }
            catch
            {
                return 0;
            }
            //return (int)com.ExecuteScalar();
        }

        public double DoubleScalar()
        {            
            return Convert.ToDouble(com.ExecuteScalar());
        }
        public string StringScalar()
        {
            return Convert.ToString(com.ExecuteScalar());
        }

        public string StringScalarAsync()
        {
            
            return Convert.ToString(com.ExecuteScalarAsync());
        }

        public void NonQueryEx()
        {
           com.ExecuteNonQuery();
                       
        }

        public void NonQueryEx(SqlCommand _com)
        {
            _com.ExecuteNonQuery();
        }

        public SqlDataReader ExecuteDataReader(SqlCommand _com)
        {
            reader = _com.ExecuteReader();
            return reader;
        }

        public SqlDataReader ExecuteDataReader()
        {
            return com.ExecuteReader();
            
        }

        public void CloseCon()
        {
            if (_con != null)
                _con.Close();
        }

        public void CloseReader()
        {
            if (reader != null)
            {
                reader.Close();
            }
        }

        public DataTable FillDataTable()
        {
            daAd = new SqlDataAdapter(com);
            dt = new DataTable();
            daAd.Fill(dt);
            return dt;
        }

        public DataTable Hae(string sql)  //from visual basic
        {

            DataTable dataTbl = new DataTable();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(sql, _con);
                DataSet myDS = new DataSet();
                adapter.Fill(dataTbl);
                adapter.Dispose();
                return dataTbl;
            }
            catch (Exception ex)
            {
                
                SEH("Error Information", sql + "\n" + ex.ToString());
                return null;
            }

        }


        public void SEH(string title, string info) //adds error info and time occurred to database
        {
            string error = "oee. " + title;
            SetCommand("Insert Into ErrorLog Values(@info, @dt)");
            com.Parameters.AddWithValue("@info", info);
            com.Parameters.AddWithValue("@dt", DateTime.Now);
            NonQueryEx();
            
        }

        public void ErrorAcknowledged(int errorID, DateTime timeAck) //used to add time error is acknowledged by admin/worker
        {
            SetCommand("Update ErrorLog set Acknowledged_Time = @time WHERE Error_ID = @id");
            com.Parameters.AddWithValue("@time", timeAck);
            com.Parameters.AddWithValue("@id", errorID);
            NonQueryEx();
        }

        public void ErrorSolved(int errorID, DateTime timeSol) //used to add time error is solved
        {
            SetCommand("Update ErrorLog set Solved_Time = @time WHERE Error_ID = @id");
            com.Parameters.AddWithValue("@time", timeSol);
            com.Parameters.AddWithValue("@id", errorID);
            NonQueryEx();
        }
    }
}