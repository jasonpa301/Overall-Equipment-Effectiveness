using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace oeeps
{
    public class DownTime
    {
        private SqlDbConnection con = new SqlDbConnection();
        private DateTime timeForDownStart;
        private DateTime timeForDownEnd;
        private double downTime;
   
        public DownTime(DateTime _timeDownStart, DateTime _timeDownEnd)
        {
            timeForDownStart = _timeDownStart;
            timeForDownEnd = _timeDownEnd;
            downTime = (timeForDownEnd - timeForDownStart).TotalMinutes;
        }

        public void InsertDownTime(int machineId, int productID, string dtCause)
        {
            con.SetCommand("Insert into DownTime (Machine_ID, DownTimeStarting, DownTimeEnding, CauseDescription, DownTime, Product_ID) Values (@MachineID, @DownTimeStarting, @DownTimeEnding, @CauseDescription, @DownTime, @pid)");
            con.com.Parameters.AddWithValue("@MachineID", machineId); //Save MachineID
            con.com.Parameters.AddWithValue("@DownTimeStarting", timeForDownStart); //Save the date and time that machine stop suddenly
            con.com.Parameters.AddWithValue("@DownTimeEnding", timeForDownEnd); //Save the date and time that machine work again (after repairing)
            con.com.Parameters.AddWithValue("@CauseDescription", dtCause); //Save the reason of the accident (which should be input by managers and will be saved into the CauseDescription table), the dropdownlist show from the database 
            con.com.Parameters.AddWithValue("@DownTime", downTime);
            con.com.Parameters.AddWithValue("@pid", productID);//Save the time length of the downtime which was calculate before
            con.NonQueryEx();
        }

        public void CloseConnection()
        {
            con.CloseCon();
        }
    }
}