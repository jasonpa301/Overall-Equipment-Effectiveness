using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace oeeps
{
    public class Breaks
    {
        SqlDbConnection con = new SqlDbConnection();
        private string breakType;

        public Breaks(string _breakType)
        {
            breakType = _breakType;
        }

        public Breaks()
        {

        }

        public void AddBreak() //adds new break type to database
        {
            con.SetCommand("Insert into BreakType (BreakType) values (@BreakType)");

            con.com.Parameters.AddWithValue("@BreakType", breakType);
            con.NonQueryEx();
        }

        public void RetrieveBreakData(int breakID)
        {
            con.SetCommand("SELECT BreakType FROM BreakType WHERE BreakID = " + breakID);
            breakType = con.StringScalar();
        }

        public void updateBreakValues(string _breakType)
        {
            if (_breakType != "")
                breakType = _breakType;
        }

        public void updateBreak(int breakID)
        {
            con.SetCommand("UPDATE BreakType SET BreakType = @bt WHERE BreakID = " + breakID);
            con.com.Parameters.AddWithValue("@bt", breakType);
            con.NonQueryEx();
        }

        public void DeleteBreak(int breakId)
        {
            con.SetCommand("Delete FROM BreakType WHERE BreakID = " + breakId);
            con.NonQueryEx();
            


        }

        public void CloseConnection()
        {
            con.CloseCon();

        }
    }
}