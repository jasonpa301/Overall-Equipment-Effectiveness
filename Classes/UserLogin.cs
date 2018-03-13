using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace oeeps
{
    public class UserLogin
    {
        private string userName;
        private string userPassword;
        private SqlDbConnection con = new SqlDbConnection();

        public UserLogin(string _userName, string _userPassword)
        {
            userName = _userName;
            userPassword = _userPassword;
        }

        public bool CheckLogin()
        {
            con.SetCommand("Select userID from Workers where userID=@un and PasswordPass=@pw");
            con.com.Parameters.AddWithValue("@un", userName);
            con.com.Parameters.AddWithValue("@pw", userPassword);


            string result = con.StringScalar();

            if (string.IsNullOrEmpty(result))       //if login successful string will be userID and return false else it will be null and return true           
                return false;
            else
                return true;
        }

        public string GetAccessLevel()
        {
            con.SetCommand("Select AccessLevel FROM Workers WHERE userID= @un");
            con.com.Parameters.AddWithValue("@un", userName);
            string tempAccessLevel = con.StringScalar();
            return tempAccessLevel;
        }

        public void InsertLoginRecord()
        {
            con.SetCommand("Select Worker_ID from Workers where userID=@un and PasswordPass=@pw");
            con.com.Parameters.AddWithValue("@un", userName); 
            con.com.Parameters.AddWithValue("@pw", userPassword);
            int workerId = con.IntScalar();  //get workerID

            //insert login record table with worker id of worker and todays date/time
            con.SetCommand("INSERT INTO LoginRecord (Worker_ID,Login_Time) VALUES (@wid,@dt)");

            con.com.Parameters.AddWithValue("@wid", workerId);
            con.com.Parameters.AddWithValue("@dt", DateTime.Now);

            con.NonQueryEx();
        }

        public void CloseConnection()
        {
            con.CloseCon();
        }
    }
}