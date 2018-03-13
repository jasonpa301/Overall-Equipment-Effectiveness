using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace oeeps
{
    public class IdealCycleTime
    {
        private string machineId;
        private string productId;
        
        private SqlDbConnection con = new SqlDbConnection();

        public IdealCycleTime()
        {

        }

        public IdealCycleTime(string _machineId, string _productId)
        {
            machineId = _machineId;
            productId = _productId;
        }


        public bool CheckAssignment() //if assignment exists returns true
        {
            con.SetCommand("Select * FROM IdealCycleTimes WHERE Machine_ID = @mid AND Product_ID = @pid");
            con.com.Parameters.AddWithValue("@mid", machineId);
            con.com.Parameters.AddWithValue("@pid", productId);
            DataTable dt = con.FillDataTable();
            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }

        public void AddAssignment(double iCT)
        {

        con.SetCommand("INSERT INTO IdealCycleTimes (Machine_ID, Product_ID, IdealCycleTime) VALUES (@mid, @pid, @ict)");
                con.com.Parameters.AddWithValue("@mid", machineId);
                con.com.Parameters.AddWithValue("@pid", productId);
                con.com.Parameters.AddWithValue("@ict", iCT);
                con.NonQueryEx();
        }

        public void RemoveAssignment()  //deletes ideal cycle time and assignment of product to machine
        {
            con.SetCommand("DELETE FROM IdealCycleTimes WHERE Machine_ID = @mid AND Product_ID = @pid");
            con.com.Parameters.AddWithValue("@mid", machineId);
            con.com.Parameters.AddWithValue("@pid", productId);
            con.NonQueryEx();
        }
         //edit cycle time in any machine pairing
        public void UpdateCycleTime(double iCT)
        {
            con.SetCommand("Update IdealCycleTimes SET IdealCycleTime = @ict WHERE Machine_ID = @mid AND Product_ID = @pid ");
            con.com.Parameters.AddWithValue("@mid", machineId);
            con.com.Parameters.AddWithValue("@pid", productId);
            con.com.Parameters.AddWithValue("@ict", iCT);
            con.NonQueryEx();
        }

        public string GetIdealCycleTime()
        {
            con.SetCommand("Select IdealCycleTime FROM IdealCycleTimes WHERE Machine_ID = @mid AND Product_ID = @pid");
            con.com.Parameters.AddWithValue("@mid", machineId);
            con.com.Parameters.AddWithValue("@pid", productId);
            return con.StringScalar();
        }

        public void CloseConnection()
        {
            con.CloseCon();
        }

        

    }
}