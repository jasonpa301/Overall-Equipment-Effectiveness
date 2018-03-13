using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace oeeps
{
    public class EmployeeShift
    {
        private SqlDbConnection con = new SqlDbConnection();
        private IFormatProvider culture = new System.Globalization.CultureInfo("fi-FI", true);
        private DateTime workingStartTime;
        private DateTime workingEndTime;
        private DateTime breakStartingTime;
        private DateTime breakEndTime;
        



        public EmployeeShift(DateTime _workingStartTime, DateTime _workingEndTime, DateTime _breakStartingTime, DateTime _breakEndTime)
        {
            workingStartTime = _workingStartTime;
            workingEndTime = _workingEndTime;
            breakStartingTime = _breakStartingTime;
            breakEndTime = _breakEndTime;


        }

        public void InsertRealWorkingTime(int workerId, string workerName)
        {
            double workingLength = (workingEndTime - workingStartTime).TotalHours;

            //Save needed data into RealWorkingTime table
            con.SetCommand("Insert into RealWorkingTime (Worker_ID, WorkingStartingTime, WorkingEndingTime, RealWorkingLength, WorkerName) Values(@Worker_ID, @WorkingStartingTime, @WorkingEndingTime, @RealWorkingLength, @WorkerName)");
            con.com.Parameters.AddWithValue("@Worker_ID", workerId); //Save WorkerID
            con.com.Parameters.AddWithValue("@WorkingStartingTime", SqlDbType.DateTime).Value = workingStartTime; //Save the time of start to work
            con.com.Parameters.AddWithValue("@WorkingEndingTime", SqlDbType.DateTime).Value = workingEndTime; //Save the time that worker go off
            con.com.Parameters.AddWithValue("@RealWorkingLength", workingLength); //Save worker's working length (calculte before)
            con.com.Parameters.AddWithValue("@WorkerName", workerName); //Save the worker name
            con.NonQueryEx();
        }

        public void InsertRealBreak(int workerId, int breakId, string workerName)
        {
            double breakLength = (breakEndTime - breakStartingTime).TotalHours;

            //Save the data into RealBreak table
            con.SetCommand("Insert into RealBreak (WorkerID, WorkerName, StartingTime, EndingTime, BreakType, RealBreakLength) Values(@WorkerID, @WorkerName, @StartingTime, @EndingTime, @BreakType, @RealBreakLength)");
            con.com.Parameters.AddWithValue("@WorkerID", workerId); //Save worker ID
            con.com.Parameters.AddWithValue("@WorkerName", workerName); //Save Worker name
            con.com.Parameters.AddWithValue("@StartingTime", SqlDbType.DateTime).Value = breakStartingTime; //Save the time of break starting
            con.com.Parameters.AddWithValue("@EndingTime", SqlDbType.DateTime).Value = breakEndTime; //Save the time that worker go back to work
            con.com.Parameters.AddWithValue("@BreakType", breakId); //Save break type, coffee lunch whatever
            con.com.Parameters.AddWithValue("@RealBreakLength", breakLength); //Save the break length
            con.NonQueryEx();
        }


        public void CloseConnection()
        {
            con.CloseCon();
        }

    }
}