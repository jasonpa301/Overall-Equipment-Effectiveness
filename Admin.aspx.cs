using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;

namespace oeeps
{
    public partial class Admin : Page
    {
        //SqlDbConnection con;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"] == null) //redirects to login page if not logged in
            {
                Response.Redirect("/Login");
                //Server.Transfer("admin.aspx", true);
            }

          
            if (Session["username"] != null)
            {
                 if ((string)(Session["accessLevel"]) != "1")
                            {
                                //Server.Transfer("admin.aspx", true);
                                Response.Redirect("/Default");
                            }
              
            }
                
            
            
               // LabelErrorManager.Text = "";
        }

        protected void Button1_Click(object sender, EventArgs e) //add machines
        {
            //con = new SqlDbConnection();

            //con.SetCommand("Insert into MachineInfo (MachineName) Values(@MachineName)");
            //con.com.Parameters.AddWithValue("@MachineName", txbMachineNameInput.Text);

            //try
            //{ 
            //    con.NonQueryEx();
            //    LabelErrorManager.Text = "";
            //}
            //catch (Exception)
            //{
            //    LabelErrorManager.Text = "Data input failed! Check format of data input is correct!";
            //}
            try
            {
                Machine machine = new Machine(txbMachineNameInput.Text);
                machine.AddMachine();
                machine.CloseConnection();
                LabelErrorManager.Text = "Machine added successfully";
                LabelErrorManager.ForeColor = Color.Green;
            }

            catch (SqlException)
            {
                LabelErrorManager.Text = "Database Error!";
                LabelErrorManager.ForeColor = Color.Red;
            }

            

            if (IsPostBack)
            {
                LabelErrorWorker.Text = "";
                txbMachineNameInput.Text = "";
                DropDownListMachine1.Items.Clear();
                DropDownListMachine1.Items.Add("---Select Machine---");
                DropDownListMachine1.DataSourceID = "SqlDataSourceMachine1";
                //refreshes dropdownlist and retains default item ---select---
                DropDownListMachines2.Items.Clear();
                DropDownListMachines2.Items.Add("---Select Machine---");
                DropDownListMachines2.DataSourceID = "SqlDataSourceMachine1";
                
                
            }

            
        }

        protected void Button5_Click(object sender, EventArgs e) //add cause description
        {
            //con = new SqlDbConnection();           
            //con.SetCommand("Insert into CauseDescription(CauseDescription) Values(@CauseDescription)");
            //con.com.Parameters.AddWithValue("@CauseDescription", txbCauseDescription.Text);
            //con.NonQueryEx();
            //con.CloseCon();
            try
            {
                FaultCause fault = new FaultCause(txbCauseDescription.Text);
                fault.AddFaultCause();
                fault.CloseConnection();
                LabelErrorManager.Text = "Fault added successfully";
                LabelErrorManager.ForeColor = Color.Green;

            }

            catch (SqlException)
            {
                LabelErrorManager.Text = "Database error";
                LabelErrorManager.ForeColor = Color.Red;
            }

            if (IsPostBack)
            {
                LabelErrorWorker.Text = "";
                txbCauseDescription.Text = "";
                DropDownListMachineFault.Items.Clear();
                DropDownListMachineFault.Items.Add("---Select Cause---");
                DropDownListMachineFault.DataSourceID = "SqlDataSourceCauseList";
            }
            

        }

        protected void btnAddBreak_Click(object sender, EventArgs e) //add break
        {
            try
            {
                Breaks empBreak = new Breaks(txbBreakType.Text);
                empBreak.AddBreak();
                empBreak.CloseConnection();
                LabelErrorWorker.Text = "Break added successfully";
                LabelErrorWorker.ForeColor = Color.Green;
            }

            catch (SqlException)
            {
                LabelErrorWorker.Text = "Database error";
                LabelErrorWorker.ForeColor = Color.Red;
            }

            //con = new SqlDbConnection();
            //con.SetCommand("Insert into BreakType (BreakType) values (@BreakType)");
            
            //con.com.Parameters.AddWithValue("@BreakType", txbBreakType.Text);
            //con.NonQueryEx();
            //con.CloseCon();

            if (IsPostBack)
            {
                txbBreakType.Text = "";
                DropDownListBreakType.DataSourceID = "SqlDataSourceBreakType";
                LabelErrorManager.Text = "";
            }
            
        }

        protected void Button2_Click(object sender, EventArgs e) //add worker
        {

            try
            {
                Employee emp = new Employee(txbWorkerName.Text, txbShiftLength.Text, txbBreakHours.Text, txbUserId.Text, txbPassword.Text, int.Parse(DropDownListAccessLevel.SelectedValue));
                if (emp.CheckUserIDUnique(txbUserId.Text)) //if userID is not in database
                {
                    emp.AddWorker();
                    LabelErrorWorker.Text = "Worker added successfully";
                    LabelErrorWorker.ForeColor = Color.Green;
                }
                else
                {
                    LabelErrorWorker.Text = "UserID already exists";
                    LabelErrorWorker.ForeColor = Color.Red;
                }

                emp.CloseConnection();

                if (IsPostBack)
                {
                    LabelErrorWorker.Text = "";
                    txbWorkerName.Text = "";
                    txbShiftLength.Text = "";
                    txbBreakHours.Text = "";
                    txbUserId.Text = "";
                    txbPassword.Text = "";
                    DropDownWorkerList.Items.Clear();
                    DropDownWorkerList.Items.Add("---Select Worker---");
                    DropDownWorkerList.DataSourceID = "SqlDataWorkers1";

                }

                

            }
            
            catch (FormatException)
            {
                LabelErrorWorker.Text = "Format error! Check form is filled in correctly";
            }

            //try
            //{
            //    double ShiftLength = double.Parse(txbShiftLength.Text);
            //    double BreakHours = double.Parse(txbBreakHours.Text);
            //    double PlanProductionTime = ShiftLength - BreakHours;

            //    con = new SqlDbConnection();

            //    con.SetCommand("Select userID from Workers WHERE userID = '" + txbUserId.Text + "'");
            //    if (con.StringScalar() != txbUserId.Text)
            //    {
            //        //Manager can add worker into database
            //        con.SetCommand("Insert into Workers (WorkerName, ShiftLength, BreaksLength, PlannedProductionTime, userID, PasswordPass, AccessLevel) Values(@WorkerName, @ShiftLength, @BreaksLength, @PlannedProductionTime,@userID, @Pw, @Ac)");


            //        con.com.Parameters.AddWithValue("@WorkerName", txbWorkerName.Text);
            //        con.com.Parameters.AddWithValue("@ShiftLength", txbShiftLength.Text);
            //        con.com.Parameters.AddWithValue("@BreaksLength", txbBreakHours.Text);
            //        con.com.Parameters.AddWithValue("@PlannedProductionTime", PlanProductionTime);
            //        con.com.Parameters.AddWithValue("@userID", txbUserId.Text);
            //        con.com.Parameters.AddWithValue("@Pw", txbPassword.Text);
            //        con.com.Parameters.AddWithValue("@Ac", DropDownListAccessLevel.SelectedValue);
            //        con.NonQueryEx(); //executes the non query sql command
            //        con.CloseCon();
            //    }
            //    else
            //    {
            //        con.CloseCon();
            //        LabelErrorWorker.Text = "UserID already exists";
            //    }
            //}
            //catch (FormatException)
            //{
            //    LabelErrorWorker.Text = "Data input failed! Check format of data input is correct!";
            //}

           

        }

        protected void ButtonUpdateWorker_Click(object sender, EventArgs e) //update worker
        {
            if (DropDownWorkerList.SelectedIndex == 0)
            {
                LabelErrorWorker.Text = "You must select a worker to delete";
            }

            else
            {
                try
                {
                    Employee emp = new Employee();
                    emp.RetrieveWorkerData(int.Parse(DropDownWorkerList.SelectedValue));
                    if (emp.CheckUserIDUnique(txbUserId.Text))
                    {
                        emp.UpdateWorkerValues(txbWorkerName.Text, txbShiftLength.Text, txbBreakHours.Text, txbUserId.Text, txbPassword.Text, DropDownListAccessLevel.SelectedValue);
                        //if (CheckBoxUpdateAccess.Checked)
                        //{
                        //    emp.UpdateAccessLevel(int.Parse(DropDownListAccessLevel.SelectedValue));
                        //}

                        emp.UpdateWorker(int.Parse(DropDownWorkerList.SelectedValue));

                        emp.CloseConnection();
                        LabelErrorWorker.Text = "Worker updated successfully";
                        LabelErrorWorker.ForeColor = Color.Green;

                        if (IsPostBack)
                        {
                            LabelErrorManager.Text = ""; 
                            txbWorkerName.Text = "";
                            txbShiftLength.Text = "";
                            txbBreakHours.Text = "";
                            txbUserId.Text = "";
                            txbPassword.Text = "";
                            DropDownWorkerList.Items.Clear();
                            DropDownWorkerList.Items.Add("---Select Worker---");
                            DropDownWorkerList.DataSourceID = "SqlDataWorkers1";
                            //CheckBoxUpdateAccess.Checked = false;
                        }
                    }

                    else
                    {
                        LabelErrorWorker.Text = "UserID already exists! Choose another";
                    }
                }

                catch (SqlException)
                {
                    LabelErrorWorker.Text = "Database error";
                    LabelErrorWorker.ForeColor = Color.Red;
                }

                catch (FormatException)
                {
                    LabelErrorWorker.Text = "Format error! Check data is entered correctly";
                    LabelErrorWorker.ForeColor = Color.Red;
                }
             
                
            }
            
            
            //con = new SqlDbConnection();
            //con.SetCommand("Select WorkerName FROM Workers WHERE Worker_ID = "+ DropDownWorkerList.SelectedValue);
            //string tempName = con.StringScalar();
            //con.SetCommand("Select ShiftLength FROM Workers WHERE Worker_ID = " + DropDownWorkerList.SelectedValue);
            //double tempShiftLength = con.DoubleScalar();
            //con.SetCommand("Select BreaksLength FROM Workers WHERE Worker_ID = " + DropDownWorkerList.SelectedValue);
            //double tempBreaksLength = con.DoubleScalar();
            //con.SetCommand("Select UserID FROM Workers WHERE Worker_ID = " + DropDownWorkerList.SelectedValue);
            //string tempUserID = con.StringScalar();
            //con.SetCommand("Select PasswordPass FROM Workers WHERE Worker_ID = " + DropDownWorkerList.SelectedValue);
            //string tempPass = con.StringScalar();
            //con.SetCommand("Select AccessLevel FROM Workers WHERE Worker_ID = " + DropDownWorkerList.SelectedValue);
            //int tempAccessLevel = con.IntScalar();
            ////gets values from database so that fields left blank will update DB with same data
            //if (txbWorkerName.Text != "")
            //    tempName = txbWorkerName.Text;
            //if (txbShiftLength.Text != "")
            //    tempShiftLength = double.Parse(txbShiftLength.Text);
            //if (txbBreakHours.Text != "")
            //    tempBreaksLength = double.Parse(txbBreakHours.Text);
            //if (txbUserId.Text != "")
            //    tempUserID = txbUserId.Text;
            //if (txbPassword.Text != "")
            //    tempPass = txbPassword.Text;
            //if (CheckBoxUpdateAccess.Checked)
            //    tempAccessLevel = int.Parse(DropDownListAccessLevel.SelectedValue);
            //double tempPlanProd = tempShiftLength - tempBreaksLength;

            //con.SetCommand("Update Workers SET WorkerName = @Wn, ShiftLength = @Sl, PlannedProductionTime = @Ppt, BreaksLength = @Bl, UserID = @Uid, PasswordPass = @pw, AccessLevel = @al WHERE Worker_ID = " + DropDownWorkerList.SelectedValue);
            //con.com.Parameters.AddWithValue("@Wn", tempName);
            //con.com.Parameters.AddWithValue("@Sl", tempShiftLength);
            //con.com.Parameters.AddWithValue("@Ppt", tempPlanProd);
            //con.com.Parameters.AddWithValue("@Bl", tempBreaksLength);
            //con.com.Parameters.AddWithValue("@Uid", tempUserID);
            //con.com.Parameters.AddWithValue("@Pw", tempPass);
            //con.com.Parameters.AddWithValue("@al", tempAccessLevel);
            //con.NonQueryEx();
            //con.CloseCon();

        }

        protected void ButtonDeleteWorker_Click(object sender, EventArgs e)
        {
            //con = new SqlDbConnection();
            //con.SetCommand("DELETE FROM Workers WHERE Worker_ID = @id");
            //con.com.Parameters.AddWithValue("@id", DropDownWorkerList.SelectedValue);
            ////Deletes selected worker from DB
            //con.NonQueryEx();
            //con.CloseCon();

            if (DropDownWorkerList.SelectedIndex == 0)
            {
                LabelErrorWorker.Text = "You must select a worker to delete";
                LabelErrorWorker.ForeColor = Color.Red;
            }
            
            else
            {
                try
                {


                    Employee emp = new Employee();
                    emp.DeleteWorker(int.Parse(DropDownWorkerList.SelectedValue));
                    emp.CloseConnection();
                    LabelErrorWorker.Text = "Worker Deleted Successfully";
                    LabelErrorWorker.ForeColor = Color.Green;

                    if (IsPostBack)
                    {

                        txbWorkerName.Text = "";
                        txbShiftLength.Text = "";
                        txbBreakHours.Text = "";
                        txbUserId.Text = "";
                        txbPassword.Text = "";
                        DropDownWorkerList.DataSourceID = "SqlDataWorkers1";
                        LabelErrorManager.Text = "";
                    }
                }

                catch (SqlException)
                {
                    LabelErrorWorker.Text = "Database Error";
                    LabelErrorWorker.ForeColor = Color.Red;
                }
            }
            
        }

        protected void ButtonUpdateMachine_Click(object sender, EventArgs e)  
        {
            //con = new SqlDbConnection();
            //con.SetCommand("Select MachineName FROM MachineInfo WHERE Machine_ID = " + DropDownListMachine1.SelectedValue);
            //string tempMachineName = con.StringScalar();


            //if (txbMachineNameInput.Text != "")
            //    tempMachineName = txbMachineNameInput.Text;

            //con.SetCommand("UPDATE MachineInfo SET MachineName = @Mn WHERE Machine_ID = " + DropDownListMachine1.SelectedValue);
            //con.com.Parameters.AddWithValue("@Mn", tempMachineName);
            //con.NonQueryEx();
            //con.CloseCon();
            if (DropDownListMachine1.SelectedIndex == 0)
            {
                LabelErrorManager.Text = "You must select a machine to update";
            }
            else
            {
                try
                {

                    Machine machine = new Machine();
                    machine.RetrieveMachineData(int.Parse(DropDownListMachine1.SelectedValue));
                    machine.updateMachineValues(txbMachineNameInput.Text);
                    machine.updateMachine(int.Parse(DropDownListMachine1.SelectedValue));
                    machine.CloseConnection();
                    LabelErrorManager.Text = "Machine updated successfully";
                    LabelErrorManager.ForeColor = Color.Green;

                    if (IsPostBack)
                    {
                        txbMachineNameInput.Text = "";
                        DropDownListMachine1.Items.Clear();
                        DropDownListMachine1.Items.Add("---Select Machine---");
                        DropDownListMachine1.DataSourceID = "SqlDataSourceMachine1";
                        //refreshes dropdownlist and retains default item ---select---
                        DropDownListMachines2.Items.Clear();
                        DropDownListMachines2.Items.Add("---Select Machine---");
                        DropDownListMachines2.DataSourceID = "SqlDataSourceMachine1";
                        LabelErrorWorker.Text = "";

                    }

                }

                catch (FormatException)
                {
                    LabelErrorManager.Text = "Format error! Check data is entered correctly";
                    LabelErrorManager.ForeColor = Color.Red;
                }

                catch (SqlException)
                {
                    LabelErrorManager.Text = "Database Error";
                    LabelErrorManager.ForeColor = Color.Red;
                }
            }
            
        }

        protected void ButtonDeleteMachine_Click(object sender, EventArgs e) //deletes machine from list/db
        {
            //con = new SqlDbConnection();
            //con.SetCommand("Delete FROM MachineInfo WHERE Machine_ID = @id");
            //con.com.Parameters.AddWithValue("@id", DropDownListMachine1.SelectedValue);
            ////deletes selected machine from DB
            //con.NonQueryEx();
            //con.CloseCon();
            try
            {
                if (DropDownListMachine1.SelectedIndex == 0) //if no machine is selected error message is shown
                {
                    LabelErrorManager.Text = "You must select a machine to delete";
                    LabelErrorManager.ForeColor = Color.Red;
                }
                else
                {
                    Machine machine = new Machine();
                    machine.DeleteMachine(int.Parse(DropDownListMachine1.SelectedValue));
                    machine.CloseConnection();
                    LabelErrorManager.Text = "Machine deleted successfully";
                    LabelErrorManager.ForeColor = Color.Green;

                    if (IsPostBack)
                    {
                        txbMachineNameInput.Text = "";
                        DropDownListMachine1.Items.Clear();
                        DropDownListMachine1.Items.Add("---Select Machine---");
                        DropDownListMachine1.DataSourceID = "SqlDataSourceMachine1";
                        //refreshes dropdownlist and retains default item ---select---
                        DropDownListMachines2.Items.Clear();
                        DropDownListMachines2.Items.Add("---Select Machine---");
                        DropDownListMachines2.DataSourceID = "SqlDataSourceMachine1";
                        LabelErrorWorker.Text = "";
                    }
                }
            }
            
            catch (SqlException)
            {
                LabelErrorManager.Text = "Database Error";
                LabelErrorManager.ForeColor = Color.Red;
            }
            
        }

        protected void ButtonUpdateCause_Click(object sender, EventArgs e)
        {
            //con = new SqlDbConnection();
            //con.SetCommand("Select CauseDescription FROM CauseDescription Where CauseID = " + DropDownListMachineFault.SelectedValue);
            //string tempCause = con.StringScalar();

            //if (txbCauseDescription.Text != "")
            //    tempCause = txbCauseDescription.Text;

            //con.SetCommand("Update CauseDescription SET CauseDescription = @cd WHERE CauseID = " + DropDownListMachineFault.SelectedValue);
            //con.com.Parameters.AddWithValue("@cd", tempCause);
            //con.NonQueryEx();
            //con.CloseCon();
            try
            {
                if (DropDownListMachineFault.SelectedIndex == 0)
                {
                    LabelErrorManager.Text = "You must select a Cause to update";
                    LabelErrorManager.ForeColor = Color.Red;
                }

                else
                {
                    FaultCause fault = new FaultCause();
                    fault.RetrieveCauseData(int.Parse(DropDownListMachineFault.SelectedValue));
                    fault.UpdateFaultValues(txbCauseDescription.Text);
                    fault.UpdateFaultCause(int.Parse(DropDownListMachineFault.SelectedValue));
                    fault.CloseConnection();
                    LabelErrorManager.Text = "Fault Cause Updated Successfully";
                    LabelErrorManager.ForeColor = Color.Green;

                    if (IsPostBack)
                    {
                        LabelErrorWorker.Text = "";
                        txbCauseDescription.Text = "";
                        DropDownListMachineFault.Items.Clear();
                        DropDownListMachineFault.Items.Add("---Select Cause---");
                        DropDownListMachineFault.DataSourceID = "SqlDataSourceCauseList";
                    }
                }

              }
            catch (SqlException)
            {
                LabelErrorManager.Text = "Database Error!";
                LabelErrorManager.ForeColor = Color.Red;
            }
            
            

        }

        protected void ButtonDeleteCause_Click(object sender, EventArgs e)
        {
            //con = new SqlDbConnection();
            //con.SetCommand("Delete FROM CauseDescription WHERE CauseID = @Cid");
            //con.com.Parameters.AddWithValue("@Cid", DropDownListMachineFault.SelectedValue);
            //con.NonQueryEx();
            //con.CloseCon();
            try
            {
                if (DropDownListMachineFault.SelectedIndex == 0)
                {
                    LabelErrorManager.Text = "You must select a Cause to delete";
                    LabelErrorManager.ForeColor = Color.Red;
                }

                else
                {
                    FaultCause fault = new FaultCause();
                    fault.DeleteFaultCause(int.Parse(DropDownListMachineFault.SelectedValue));
                    fault.CloseConnection();
                    LabelErrorManager.Text = "Fault Cause Deleted Successfully";
                    LabelErrorManager.ForeColor = Color.Green;

                    if (IsPostBack)
                    {

                        txbCauseDescription.Text = "";
                        LabelErrorWorker.Text = "";
                        DropDownListMachineFault.Items.Clear();
                        DropDownListMachineFault.Items.Add("---Select Cause---");
                        DropDownListMachineFault.DataSourceID = "SqlDataSourceCauseList";
                    }
                }
            }

            catch (SqlException)
            {
                LabelErrorManager.Text = "Database error";
                LabelErrorManager.ForeColor = Color.Red;
            }
           

           

        }

        protected void ButtonUpdateBreak_Click(object sender, EventArgs e)
        {
            try
            {
                if (DropDownListBreakType.SelectedIndex == 0)
                {
                    LabelErrorWorker.Text = "You must select a Break to update";
                    LabelErrorWorker.ForeColor = Color.Red;
                }

                else
                {
                    Breaks brk = new Breaks();
                    brk.RetrieveBreakData(int.Parse(DropDownListBreakType.SelectedValue));
                    brk.updateBreakValues(txbBreakType.Text);
                    brk.updateBreak(int.Parse(DropDownListBreakType.SelectedValue));
                    brk.CloseConnection();
                    LabelErrorWorker.Text = "Break Updated Successfully";
                    LabelErrorWorker.ForeColor = Color.Green;

                    if (IsPostBack)
                    {
                        LabelErrorManager.Text = "";
                        txbBreakType.Text = "";
                        DropDownListBreakType.Items.Clear();
                        DropDownListBreakType.Items.Add("---Select Break---");
                        DropDownListBreakType.DataSourceID = "SqlDataSourceBreakType";
                    }
                }
            }

            catch (SqlException)
            {
                LabelErrorWorker.Text = "Database Error";
                LabelErrorWorker.ForeColor = Color.Red;
            }


            //con = new SqlDbConnection();
            //con.SetCommand("SELECT BreakType FROM BreakType WHERE BreakID = " + DropDownListBreakType.SelectedValue);
            //string tempBreakType = con.StringScalar();

            //if (txbBreakType.Text != "")
            //    tempBreakType = txbBreakType.Text;

            //con.SetCommand("UPDATE BreakType SET BreakType = @bt WHERE BreakID = " + DropDownListBreakType.SelectedValue);
            //con.com.Parameters.AddWithValue("@bt", tempBreakType);
            //con.NonQueryEx();
            //con.CloseCon();

            }

        protected void ButtonDeleteBreak_Click(object sender, EventArgs e)
        {
            //con = new SqlDbConnection();
            //con.SetCommand("Delete FROM BreakType WHERE BreakID = " + DropDownListBreakType.SelectedValue);
            //con.NonQueryEx();
            //con.CloseCon();
            try
            {
                if (DropDownListBreakType.SelectedIndex == 0)
                {
                    LabelErrorWorker.Text = "You must select a Break to delete";
                    LabelErrorWorker.ForeColor = Color.Red;
                }

                else
                {
                    Breaks brk = new Breaks();
                    brk.DeleteBreak(int.Parse(DropDownListBreakType.SelectedValue));
                    LabelErrorWorker.Text = "Break Deleted Successfully";
                    LabelErrorWorker.ForeColor = Color.Green;

                    if (IsPostBack)
                    {
                        txbBreakType.Text = "";
                        LabelErrorManager.Text = "";
                        DropDownListBreakType.Items.Clear();
                        DropDownListBreakType.Items.Add("---Select Break---");
                        DropDownListBreakType.DataSourceID = "SqlDataSourceBreakType";
                    }
                }
            }

            catch (SqlException)
            {
                LabelErrorWorker.Text = "Database Error";
                LabelErrorWorker.ForeColor = Color.Red;
            }
           
            
        }
        
        protected void ButtonAddProduct_Click(object sender, EventArgs e) //add product to db
        {
            //con = new SqlDbConnection();
            //con.SetCommand("Insert into Products (ProductName) Values (@Pn)");
            //con.com.Parameters.AddWithValue("@Pn", TxbProductNames.Text);
            //con.NonQueryEx();
            //con.CloseCon();
            try
            {
                Products product = new Products(TxbProductNames.Text);
                product.AddProduct();
                product.CloseConnection();
                LabelErrorManager.Text = "Product added successfully";
                LabelErrorManager.ForeColor = Color.Green;

                if (IsPostBack)
                {
                    LabelErrorWorker.Text = "";
                    TxbProductNames.Text = "";
                    DropDownListProducts.Items.Clear();
                    DropDownListProducts.Items.Add("---Select Product---");
                    DropDownListProducts.DataSourceID = "SqlDataSourceProducts";
                    DropDownListProducts2.Items.Clear();
                    DropDownListProducts2.Items.Add("---Select Product---");
                    DropDownListProducts2.DataSourceID = "SqlDataSourceProducts";
                }
            }

            catch (SqlException)
            {
                LabelErrorManager.Text = "Database Error!";
                LabelErrorManager.ForeColor = Color.Red;
            }

            catch (FormatException)
            {
                LabelErrorManager.Text = "Format Error! Check data is entered correctly";
                LabelErrorManager.ForeColor = Color.Red;
            }


        }

        protected void ButtonUpdateProduct_Click(object sender, EventArgs e)
        {
            //con = new SqlDbConnection();
            //con.SetCommand("SELECT ProductName FROM Products WHERE Product_ID = " + DropDownListProducts.SelectedValue);
            //string tempProductName = con.StringScalar();

            //if (TxbProductNames.Text != "")
            //    tempProductName = TxbProductNames.Text;



            //con.SetCommand("UPDATE Products SET ProductName = @pn WHERE Product_ID = " + DropDownListProducts.SelectedValue);
            //con.com.Parameters.AddWithValue("@pn", tempProductName);

            //con.NonQueryEx();
            //con.CloseCon();
            try
            {
                if (DropDownListProducts.SelectedIndex == 0)
                {
                    LabelErrorManager.Text = "You must select a Product to update";
                    LabelErrorManager.ForeColor = Color.Red;
                }

                else
                {
                    Products product = new Products();
                    product.RetrieveProductData(int.Parse(DropDownListProducts.SelectedValue));
                    product.UpdateProductValues(TxbProductNames.Text);
                    product.UpdateProduct(int.Parse(DropDownListProducts.SelectedValue));
                    product.CloseConnection();
                    LabelErrorManager.Text = "Product updated successfully";
                    LabelErrorManager.ForeColor = Color.Green;

                    if (IsPostBack)
                    {
                        LabelErrorWorker.Text = "";
                        TxbProductNames.Text = "";
                        DropDownListProducts.Items.Clear();
                        DropDownListProducts.Items.Add("---Select Product---");
                        DropDownListProducts.DataSourceID = "SqlDataSourceProducts";
                        DropDownListProducts2.Items.Clear();
                        DropDownListProducts2.Items.Add("---Select Product---");
                        DropDownListProducts2.DataSourceID = "SqlDataSourceProducts";

                    }
                }
            }

            catch (SqlException)
            {
                LabelErrorManager.Text = "Database Error!";
                LabelErrorManager.ForeColor = Color.Red;
            }
           
            
        }

        protected void ButtonDeleteProducts_Click(object sender, EventArgs e)
        {
            //con = new SqlDbConnection();
            //con.SetCommand("Delete FROM Products WHERE Product_ID = " + DropDownListProducts.SelectedValue);
            //con.NonQueryEx();
            //con.CloseCon();

            try
            {
                if (DropDownListProducts.SelectedIndex == 0)
                {
                    LabelErrorManager.Text = "You must select a product to delete";
                    LabelErrorManager.ForeColor = Color.Red;
                }

                else
                {
                    Products product = new Products();
                    product.DeleteProduct(int.Parse(DropDownListProducts.SelectedValue));
                    product.CloseConnection();
                    LabelErrorManager.Text = "Product Deleted Successfully";
                    LabelErrorManager.ForeColor = Color.Green;
                    if (IsPostBack)
                    {
                        TxbProductNames.Text = "";
                        LabelErrorWorker.Text = "";
                        DropDownListProducts.Items.Clear();
                        DropDownListProducts.Items.Add("---Select Product---");
                        DropDownListProducts.DataSourceID = "SqlDataSourceProducts";
                        DropDownListProducts2.Items.Clear();
                        DropDownListProducts2.Items.Add("---Select Product---");
                        DropDownListProducts2.DataSourceID = "SqlDataSourceProducts";
                    }
                }

            }

            catch (SqlException)
            {
                LabelErrorManager.Text = "Database Error!";
                LabelErrorManager.ForeColor = Color.Red;
            }

        }

       

        protected void ButtonAssignProduct_Click(object sender, EventArgs e)
        {
            try
            {
                if (DropDownListProducts2.SelectedIndex == 0 || DropDownListMachines2.SelectedIndex == 0)
                {
                    LabelErrorManager.Text = "You must select a Product and Machine to assign";
                    LabelErrorManager.ForeColor = Color.Red;
                }
                else
                {
                    IdealCycleTime ict = new IdealCycleTime(DropDownListMachines2.SelectedValue, DropDownListProducts2.SelectedValue);
                    if (ict.CheckAssignment())
                    {
                        LabelErrorManager.Text = "Product is already assigned to that machine!";
                        LabelErrorManager.ForeColor = Color.Red;
                    }
                    else
                    {
                        if (txbIdealCycleTime.Text != "")
                        {
                            ict.AddAssignment(double.Parse(txbIdealCycleTime.Text));
                            ict.CloseConnection();
                            LabelErrorManager.Text = "Product Assigned to Machine Successfully";
                            LabelErrorManager.ForeColor = Color.Green;
                            if (IsPostBack)
                            {
                                LabelErrorWorker.Text = "";
                                txbIdealCycleTime.Text = "";
                                DropDownListProducts2.Items.Clear();
                                DropDownListProducts2.Items.Add("---Select Product---");
                                DropDownListProducts2.DataSourceID = "SqlDataSourceProducts";
                                DropDownListMachines2.Items.Clear();
                                DropDownListMachines2.Items.Add("---Select Machine---");
                                DropDownListMachines2.DataSourceID = "SqlDataSourceMachine1";
                            }
                        }

                        else
                        {
                            LabelErrorManager.Text = "Data input failed! Ideal Cycle Time Required";
                        }
                    }

                    ict.CloseConnection();

                    if (IsPostBack)
                    {
                        txbIdealCycleTime.Text = "";
                        DropDownListProducts2.Items.Clear();
                        DropDownListProducts2.Items.Add("---Select Product---");
                        DropDownListProducts2.DataSourceID = "SqlDataSourceProducts";
                        DropDownListMachines2.Items.Clear();
                        DropDownListMachines2.Items.Add("---Select Machine---");
                        DropDownListMachines2.DataSourceID = "SqlDataSourceMachine1";
                    }
                }
            }

            catch (SqlException)
            {
                LabelErrorManager.Text = "Database Error!";
                LabelErrorManager.ForeColor = Color.Red;
            }


            //con = new SqlDbConnection(); //checks if product is already assigned to chosen machine
            //con.SetCommand("Select * FROM IdealCycleTimes WHERE Machine_ID = @mid AND Product_ID = @pid");
            //con.com.Parameters.AddWithValue("@mid", DropDownListMachines2.SelectedValue);
            //con.com.Parameters.AddWithValue("@pid", DropDownListProducts2.SelectedValue);
            //DataTable dt = con.FillDataTable();
            //if (dt.Rows.Count == 0)
            //{
            //    con.SetCommand("INSERT INTO IdealCycleTimes (Machine_ID, Product_ID, IdealCycleTime) VALUES (@mid, @pid, @ict)");
            //    con.com.Parameters.AddWithValue("@mid", DropDownListMachines2.SelectedValue);
            //    con.com.Parameters.AddWithValue("@pid", DropDownListProducts2.SelectedValue);
            //    con.com.Parameters.AddWithValue("@ict", txbIdealCycleTime.Text);
            //    con.NonQueryEx();
            //    LabelErrorManager.Text = "";
            //}
            //else
            //{
            //    LabelErrorManager.Text = "Product is already assigned to that machine!";

            //}
            //if (txbIdealCycleTime.Text == "")
            //    LabelErrorManager.Text = "Data input failed! Ideal Cycle Time Required";

            //con.CloseCon();
        }

        protected void ButtonUnassignProduct_Click(object sender, EventArgs e) //unassigns product from machine
        {

            //con = new SqlDbConnection(); //checks if product is already assigned to chosen machine
            //con.SetCommand("Select * FROM IdealCycleTimes WHERE Machine_ID = @mid AND Product_ID = @pid");
            //con.com.Parameters.AddWithValue("@mid", DropDownListMachines2.SelectedValue);
            //con.com.Parameters.AddWithValue("@pid", DropDownListProducts2.SelectedValue);
            //DataTable dt = con.FillDataTable();
            //if (dt.Rows.Count == 0)
            //{
            //    LabelErrorManager.Text = "The product and machine are already unassigned";
            //}
            //else
            //{
            //    con.SetCommand("DELETE FROM IdealCycleTimes WHERE Machine_ID = @mid AND Product_ID = @pid");
            //    con.com.Parameters.AddWithValue("@mid", DropDownListMachines2.SelectedValue);
            //    con.com.Parameters.AddWithValue("@pid", DropDownListProducts2.SelectedValue);
            //    con.NonQueryEx();
            //    LabelErrorManager.Text = "";
            //}

            try
            {
                if (DropDownListProducts2.SelectedIndex == 0 || DropDownListMachines2.SelectedIndex == 0)
                {
                    LabelErrorManager.Text = "You must select a Product and Machine to unassign";
                    LabelErrorManager.ForeColor = Color.Red;
                }

                else
                {
                    IdealCycleTime ict = new IdealCycleTime(DropDownListMachines2.SelectedValue, DropDownListProducts2.SelectedValue);
                    if (ict.CheckAssignment() == false)
                    {
                        LabelErrorManager.Text = "The product and machine are already unassigned";
                        LabelErrorManager.ForeColor = Color.Red;
                    }
                    else
                    {
                        ict.RemoveAssignment();
                        LabelErrorManager.Text = "";
                        LabelErrorManager.Text = "Product and machine are unassigned successfully";
                        LabelErrorManager.ForeColor = Color.Green;

                        ict.CloseConnection();

                        if (IsPostBack)
                        {
                            LabelErrorWorker.Text = "";
                            txbIdealCycleTime.Text = "";
                            DropDownListProducts2.Items.Clear();
                            DropDownListProducts2.Items.Add("---Select Product---");
                            DropDownListProducts2.DataSourceID = "SqlDataSourceProducts";
                            DropDownListMachines2.Items.Clear();
                            DropDownListMachines2.Items.Add("---Select Machine---");
                            DropDownListMachines2.DataSourceID = "SqlDataSourceMachine1";
                        }
                    }
                }
            }

            catch (SqlException)
            {
                LabelErrorManager.Text = "Database Error!";
                LabelErrorManager.ForeColor = Color.Red;
            }


        }

        protected void ButtonUpdateIct_Click(object sender, EventArgs e)
        {
            try
            {
                if (DropDownListProducts2.SelectedIndex == 0 || DropDownListMachines2.SelectedIndex == 0)
                {
                    LabelErrorManager.Text = "You must select a Product and Machine to update";
                    LabelErrorManager.ForeColor = Color.Red;
                }

                else
                {
                    IdealCycleTime ict = new IdealCycleTime(DropDownListMachines2.SelectedValue, DropDownListProducts2.SelectedValue);
                    if (ict.CheckAssignment())
                    {
                        try
                        {
                            ict.UpdateCycleTime(double.Parse(txbIdealCycleTime.Text));
                            LabelErrorManager.Text = "Ideal Cycle Time Updated Successfully";
                            LabelErrorManager.ForeColor = Color.Green;
                        }

                        catch (FormatException)
                        {
                            LabelErrorManager.Text = "Data input failed! Ideal Cycle Time must not be blank or invalid characters";
                            LabelErrorManager.ForeColor = Color.Red;
                        }
                    }
                    else
                    {
                        LabelErrorManager.Text = "Product must be assigned to machine before you can update";
                        LabelErrorManager.ForeColor = Color.Red;
                    }

                    if (IsPostBack)
                    {
                        LabelErrorWorker.Text = "";
                        txbIdealCycleTime.Text = "";
                        DropDownListProducts2.Items.Clear();
                        DropDownListProducts2.Items.Add("---Select Product---");
                        DropDownListProducts2.DataSourceID = "SqlDataSourceProducts";
                        DropDownListMachines2.Items.Clear();
                        DropDownListMachines2.Items.Add("---Select Machine---");
                        DropDownListMachines2.DataSourceID = "SqlDataSourceMachine1";
                    }
                }

            }

            catch (SqlException)
            {
                LabelErrorManager.Text = "Database Error!";
                LabelErrorManager.ForeColor = Color.Red;
            }





            //con = new SqlDbConnection(); //checks if product is already assigned to chosen machine
            //con.SetCommand("Select * FROM IdealCycleTimes WHERE Machine_ID = @mid AND Product_ID = @pid");
            //con.com.Parameters.AddWithValue("@mid", DropDownListMachines2.SelectedValue);
            //con.com.Parameters.AddWithValue("@pid", DropDownListProducts2.SelectedValue);
            //DataTable dt = con.FillDataTable();
            //if (dt.Rows.Count == 0)
            //{
            //    LabelErrorManager.Text = "Product must be assigned to machine before you can update";
            //}
            //else
            //{
            //    con.SetCommand("Update IdealCycleTimes SET IdealCycleTime = @ict WHERE Machine_ID = @mid AND Product_ID = @pid ");
            //    con.com.Parameters.AddWithValue("@mid", DropDownListMachines2.SelectedValue);
            //    con.com.Parameters.AddWithValue("@pid", DropDownListProducts2.SelectedValue);
            //    con.com.Parameters.AddWithValue("@ict", txbIdealCycleTime.Text);
            //    con.NonQueryEx();

            //    LabelErrorManager.Text = "";
            //}
            //con.CloseCon();
            //if (txbIdealCycleTime.Text == "")
            //{
            //    LabelErrorManager.Text = "Data update failed! Ideal Cycle Time is required!";
            //}

        }

        protected void DropDownWorkerList_SelectedIndexChanged(object sender, EventArgs e)
        {
            LabelErrorWorker.Text = "";
            LabelErrorManager.Text = "";

            if (DropDownWorkerList.SelectedIndex != 0)
            {
                Employee emp = new Employee();
                emp.RetrieveWorkerData(int.Parse(DropDownWorkerList.SelectedValue));
                string[] employeeInfo = emp.GetWorkerInfoArray();
                txbWorkerName.Text = employeeInfo[0];
                txbShiftLength.Text = employeeInfo[1];
                txbBreakHours.Text = employeeInfo[2];
                txbUserId.Text = employeeInfo[3];
                txbPassword.Text = employeeInfo[4];
                DropDownListAccessLevel.SelectedIndex = DropDownListAccessLevel.Items.IndexOf(DropDownListAccessLevel.Items.FindByValue(employeeInfo[5]));


                emp.CloseConnection();
            }
            

        }

      

        protected void DropDownListProducts2_SelectedIndexChanged(object sender, EventArgs e)
        {
            LabelErrorManager.Text = "";
            LabelErrorWorker.Text = "";
            if (DropDownListProducts2.SelectedIndex != 0 && DropDownListMachines2.SelectedIndex != 0 )
            {
                IdealCycleTime ict = new IdealCycleTime(DropDownListMachines2.SelectedValue, DropDownListProducts2.SelectedValue);
                txbIdealCycleTime.Text = ict.GetIdealCycleTime();
                ict.CloseConnection();
            }
            
        }

        protected void DropDownListBreakType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void DropDownListMachines2_SelectedIndexChanged(object sender, EventArgs e)
        {
            LabelErrorManager.Text = "";
            LabelErrorWorker.Text = "";
            if (DropDownListProducts2.SelectedIndex != 0 && DropDownListMachines2.SelectedIndex != 0)
            {
                IdealCycleTime ict = new IdealCycleTime(DropDownListMachines2.SelectedValue, DropDownListProducts2.SelectedValue);
                txbIdealCycleTime.Text = ict.GetIdealCycleTime();
                ict.CloseConnection();
            }
        }
    }
}