using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace oeeps
{
    public class FaultCause
    {
        private string faultName;
        private SqlDbConnection con = new SqlDbConnection();

        public FaultCause()
        {

        }

        public FaultCause(string _faultName)
        {
            faultName = _faultName;
        }

        public void AddFaultCause()
        {
            con.SetCommand("Insert into CauseDescription(CauseDescription) Values(@CauseDescription)");
            con.com.Parameters.AddWithValue("@CauseDescription", faultName);
            con.NonQueryEx();
        }

        public void RetrieveCauseData(int causeId)
        {
            con = new SqlDbConnection();
            con.SetCommand("Select CauseDescription FROM CauseDescription Where CauseID = " + causeId);
            faultName = con.StringScalar();
        }

        public void UpdateFaultValues(string _faultName)
        {
            if (_faultName != "")
                faultName = _faultName;
        }

        public void UpdateFaultCause(int causeId)
        {
            con.SetCommand("Update CauseDescription SET CauseDescription = @cd WHERE CauseID = " + causeId);
            con.com.Parameters.AddWithValue("@cd", faultName);
            con.NonQueryEx();
            
        }

        public void DeleteFaultCause(int causeId)
        {
            con.SetCommand("Update CauseDescription SET CauseDescription = @cd WHERE CauseID = " + causeId);
            con.com.Parameters.AddWithValue("@cd", faultName);
            con.NonQueryEx();
        }

        public void CloseConnection()
        {
            con.CloseCon();

        }

    }
}