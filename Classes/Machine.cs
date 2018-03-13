using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace oeeps
{
    public class Machine
    {
        private string machineName;
        SqlDbConnection con = new SqlDbConnection();
        

        public Machine(string _machineName)
        {
            machineName = _machineName;
        }

        public Machine()
        {

        }

        public void AddMachine() //adds new machine type to database
        {
            con.SetCommand("Insert into MachineInfo (MachineName) Values(@MachineName)");
            con.com.Parameters.AddWithValue("@MachineName", machineName);
            con.NonQueryEx();
        }

        public void RetrieveMachineData(int machineID)
        {
            con.SetCommand("Select MachineName FROM MachineInfo WHERE Machine_ID = " + machineID);
            machineName = con.StringScalar();

        }

        public void updateMachineValues(string _machineName)
        {
            if (_machineName != "")
                machineName = _machineName;
        }

        public void updateMachine(int machineID)
        {
            con.SetCommand("UPDATE MachineInfo SET MachineName = @Mn WHERE Machine_ID = " + machineID);
            con.com.Parameters.AddWithValue("@Mn", machineName);
            con.NonQueryEx();
        }

        public void DeleteMachine(int machineId)
        {
            con.SetCommand("Delete FROM MachineInfo WHERE Machine_ID = @id");
            con.com.Parameters.AddWithValue("@id", machineId);
            //deletes selected machine from DB
            con.NonQueryEx();


        }

        public void CloseConnection()
        {
            con.CloseCon();

        }
    }
}


