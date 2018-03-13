using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace oeeps
{
    public class Employee
    {
        SqlDbConnection con = new SqlDbConnection();
        private string employeeName;
        private double shiftLength;
        private double breakLength;
        private double planProdTime;
        private string userId;
        private string userPass;
        private int accessLevel;

        public Employee(string _empName, string _shiftLength, string _breakLength, string _userId, string _userPass, int _accessLevel)
        {
            employeeName = _empName;
            shiftLength = double.Parse(_shiftLength);
            breakLength = double.Parse(_breakLength);
            planProdTime = shiftLength - breakLength;
            userId = _userId;
            userPass = _userPass;
            accessLevel = _accessLevel;
            

        }

        public Employee()
        {

        }
        //checks if userId already exists in database
        public bool CheckUserIDUnique(string userText)
        {
            con.SetCommand("Select userID from Workers WHERE userID = '" + userText + "'");
            DataTable dt = con.FillDataTable();
            if (dt.Rows.Count == 1)
                return false;  //returns false if found in DB
            else
                return true;  //returns true if not
        }

        //adds worker to database
        public void AddWorker()
        {
            con.SetCommand("Insert into Workers (WorkerName, ShiftLength, BreaksLength, PlannedProductionTime, userID, PasswordPass, AccessLevel) Values(@WorkerName, @ShiftLength, @BreaksLength, @PlannedProductionTime,@userID, @Pw, @Ac)");


            con.com.Parameters.AddWithValue("@WorkerName", employeeName);
            con.com.Parameters.AddWithValue("@ShiftLength", shiftLength);
            con.com.Parameters.AddWithValue("@BreaksLength", breakLength);
            con.com.Parameters.AddWithValue("@PlannedProductionTime", (planProdTime));
            con.com.Parameters.AddWithValue("@userID", userId);
            con.com.Parameters.AddWithValue("@Pw", userPass);
            con.com.Parameters.AddWithValue("@Ac", accessLevel);
            con.NonQueryEx(); //executes the non query sql command
            
        }

        public void RetrieveWorkerData(int workerID) //sets values to data from DB of selected worker ID (used for updating where fields are left blank)
        {
            con.SetCommand("Select WorkerName FROM Workers WHERE Worker_ID = " + workerID);
            employeeName = con.StringScalar();
            con.SetCommand("Select ShiftLength FROM Workers WHERE Worker_ID = " + workerID);
            shiftLength = con.DoubleScalar();
            con.SetCommand("Select BreaksLength FROM Workers WHERE Worker_ID = " + workerID);
            breakLength = con.DoubleScalar();
            con.SetCommand("Select UserID FROM Workers WHERE Worker_ID = " + workerID);
            userId = con.StringScalar();
            con.SetCommand("Select PasswordPass FROM Workers WHERE Worker_ID = " + workerID);
            userPass = con.StringScalar();
            con.SetCommand("Select AccessLevel FROM Workers WHERE Worker_ID = " + workerID);
            accessLevel = con.IntScalar();
            planProdTime = shiftLength - breakLength;
        }

        public void UpdateWorkerValues(string _empName, string _shiftLength, string _breakLength, string _userId, string _userPass)
        {
            if (_empName != "")
                employeeName = _empName;
            if (_shiftLength != "")
                shiftLength = double.Parse(_shiftLength);
            if (_breakLength != "")
                breakLength = double.Parse(_breakLength);
            if (_userId != "")
                userId = _userId;
            if (_userPass != "")
                userPass = _userPass;

            planProdTime = shiftLength - breakLength;
        }

        public void UpdateWorkerValues(string _empName, string _shiftLength, string _breakLength, string _userId, string _userPass, string _accessLevel )
        {
            if (_empName != "")
                employeeName = _empName;
            if (_shiftLength != "")
                shiftLength = double.Parse(_shiftLength);
            if (_breakLength != "")
                breakLength = double.Parse(_breakLength);
            if (_userId != "")
                userId = _userId;
            if (_userPass != "")
                userPass = _userPass;
            accessLevel = int.Parse(_accessLevel);

            planProdTime = shiftLength - breakLength;
        }

        public void UpdateAccessLevel(int _accessLevel)
        {
            accessLevel = _accessLevel;
        }

        public void UpdateWorker(int workerId)
        {
            con.SetCommand("Update Workers SET WorkerName = @Wn, ShiftLength = @Sl, PlannedProductionTime = @Ppt, BreaksLength = @Bl, UserID = @Uid, PasswordPass = @pw, AccessLevel = @al WHERE Worker_ID = " + workerId);
            con.com.Parameters.AddWithValue("@Wn", employeeName);
            con.com.Parameters.AddWithValue("@Sl", shiftLength);
            con.com.Parameters.AddWithValue("@Ppt", planProdTime);
            con.com.Parameters.AddWithValue("@Bl", breakLength);
            con.com.Parameters.AddWithValue("@Uid", userId);
            con.com.Parameters.AddWithValue("@Pw", userPass);
            con.com.Parameters.AddWithValue("@al", accessLevel);
            con.NonQueryEx();
        }

        public void DeleteWorker(int workerId)
        {
            con.SetCommand("DELETE FROM Workers WHERE Worker_ID = @id");
            con.com.Parameters.AddWithValue("@id", workerId);
            //Deletes selected worker from DB
            con.NonQueryEx();
            
        }

        public string[] GetWorkerInfoArray()
        {
            string[] workerInfo = {employeeName, shiftLength.ToString(), breakLength.ToString(), userId, userPass, accessLevel.ToString()};

            return workerInfo;
        }

        public void CloseConnection()
        {
            con.CloseCon();

        }

    }
}