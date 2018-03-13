using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

namespace oeeps
{
    public partial class dataInput : Page
    {
        //Finnish format for the datetime
        IFormatProvider culture = new System.Globalization.CultureInfo("fi-FI", true);
        SqlDbConnection con; //initialises con so don't have to keep writing SqlDbConnection can just use con
        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"] == null) //redirects to login page if not logged in
            {
                Response.Redirect("/Login");
                //Server.Transfer("admin.aspx", true);
            }

            //if ((string)Session["accessLevel"] != "1")
            //LabelDataInput.Text = "";
           
        }

 

        protected void ddlMachine_ID_SelectedIndexChanged(object sender, EventArgs e)
        {
            //will only change dropdown list if machines are selected
            LabelDataInput.Text = "";
            if (ddlMachine_ID.SelectedIndex != 0)
            {
                DropDownListProductsM.Items.Clear();
                DropDownListProductsM.Items.Add("---Select Product---");
                
                con = new SqlDbConnection();
                con.SetCommand("Select Products.ProductName, Products.Product_ID FROM Products INNER JOIN IdealCycleTimes ON Products.Product_ID = IdealCycleTimes.Product_ID INNER JOIN MachineInfo ON IdealCycleTimes.Machine_ID = MachineInfo.Machine_ID WHERE MachineInfo.Machine_ID = " + ddlMachine_ID.SelectedValue);
                DataTable dt = con.FillDataTable();
                if (dt.Rows.Count > 0)  //changes dropdown list of products to only products associated with selected machine
                {
                    DropDownListProductsM.DataSource = dt;
                    DropDownListProductsM.DataTextField = "ProductName";
                    DropDownListProductsM.DataValueField = "Product_ID";
                    DropDownListProductsM.DataBind();
                }
                con.CloseCon();

            }

        }



        protected void Button2_Click(object sender, EventArgs e)  //adds production info and calculates oee
        {

            try
            {
                OOEResult oee = new OOEResult(DateTime.Parse(txbRunTimeStart.Text, culture, System.Globalization.DateTimeStyles.AssumeLocal), DateTime.Parse(txbRunTimeEnd.Text, culture, System.Globalization.DateTimeStyles.AssumeLocal));
                oee.CalculateAvailability(int.Parse(ddlMachine_ID.SelectedValue));
                oee.CalculateQuality(double.Parse(txbTotalCount.Text), double.Parse(txbRejectedCount.Text));
                oee.CalculatePerformance(int.Parse(ddlMachine_ID.SelectedValue), int.Parse(DropDownListProductsM.SelectedValue), double.Parse(txbTotalCount.Text));
                oee.InsertMachineStatus(int.Parse(ddlMachine_ID.SelectedValue), ddlMachine_ID.SelectedItem.Text, int.Parse(DropDownListProductsM.SelectedValue));
                oee.InsertOutputData(int.Parse(ddlMachine_ID.SelectedValue), double.Parse(txbTotalCount.Text), double.Parse(txbRejectedCount.Text), int.Parse(DropDownListProductsM.SelectedValue));
                int dailyCount = oee.GetDailyCount();
                if (dailyCount != 0)
                {
                    oee.UpdateOEE(dailyCount);
                }
                else
                {
                    oee.InsertOeeData();
                }
                LabelDataInput.Text = "Data updated successfully";
                LabelDataInput.ForeColor = Color.Green;

                oee.CloseConnection();

                if (IsPostBack)
                {

                    //txbMachineName.Text = "";
                    txbRunTimeStart.Text = "";
                    txbRunTimeEnd.Text = "";

                    txbDownTimeStart.Text = "";
                    txbDownTimeEnd.Text = "";

                    txbTotalCount.Text = "";
                    txbRejectedCount.Text = "";
                
                }
            }

            catch (FormatException)
            {
                LabelDataInput.Text = "Data input failed! Check data entered is in correct format!";
                LabelDataInput.ForeColor = Color.Red;
            }

            catch (SqlException)
            {
                LabelDataInput.Text = "Database Error!";
                LabelDataInput.ForeColor = Color.Red;
            }
            //SqlDataReader rdr = null;


            // string PassPlanProductionTime = Session["PassValue"] as string; //is NULL
            //planned production time is now taken from user when first logged in and saved as a Session


            //Save and add all the data input from users (workers)
            
            //con = new SqlDbConnection();
            //double DownTime;
            //try
            //{


            //    //Set datetime for the machine running start & end, down time start & end
            //    DateTime TimeForRunStart = DateTime.Parse(txbRunTimeStart.Text, culture, System.Globalization.DateTimeStyles.AssumeLocal);
            //    DateTime TimeForRunEnd = DateTime.Parse(txbRunTimeEnd.Text, culture, System.Globalization.DateTimeStyles.AssumeLocal);
            //    //DateTime TimeForDownStart = DateTime.Parse(txbDownTimeStart.Text, culture, System.Globalization.DateTimeStyles.AssumeLocal);
            //    //DateTime TimeForDownEnd = DateTime.Parse(txbDownTimeEnd.Text, culture, System.Globalization.DateTimeStyles.AssumeLocal);

            //    //Calculate Runtime and downtime (hours)
            //    double RunTime = (TimeForRunEnd - TimeForRunStart).TotalMinutes;  //all calculations must have time in same format such as all minutes
                
            //    con.SetCommand("SELECT SUM(DownTime) FROM DownTime WHERE DownTimeStarting > @rs AND DownTimeStarting < @re AND Machine_ID = @id");
            //    con.com.Parameters.AddWithValue("@rs", TimeForRunStart);
            //    con.com.Parameters.AddWithValue("@re", TimeForRunEnd);
            //    con.com.Parameters.AddWithValue("@id", ddlMachine_ID.SelectedValue);
            //    //double DownTime = (TimeForDownEnd - TimeForDownStart).TotalMinutes;
            //    try
            //    {
            //       DownTime = con.DoubleScalar();
            //    }
                
            //    catch (InvalidCastException)
            //    {
            //        DownTime = 0; //sets downtime as 0 if unable to retrieve any values from DB
            //    }
            //    //double PPD = double.Parse(PassPlanProductionTime);
            //    //double Availabilty = (RunTime - DownTime) / PPD;
            //    double Availability = (RunTime - DownTime) / RunTime;


            //    //Convert the input data for total count & rejected count, and calculate good count
            //    double TotalCount = double.Parse(txbTotalCount.Text);
            //    double RejectedCount = double.Parse(txbRejectedCount.Text);
            //    double GoodCount = TotalCount - RejectedCount;

            //    //Calculate Quality
            //    double Quality = GoodCount / TotalCount;

            //    //Calculate Performance 
            //    con.SetCommand("Select IdealCycleTime From IdealCycleTimes Where Machine_ID = @mid AND Product_ID = @pid");
            //    con.com.Parameters.AddWithValue("@mid", ddlMachine_ID.SelectedValue);
            //    con.com.Parameters.AddWithValue("@pid", DropDownListProductsM.SelectedValue);
            //    //SqlDataReader rdr = con.ExecuteDataReader(); //not sure if this line is right //for single data use scalar
            //    double ICT = con.DoubleScalar(); //made new method to return single double value from db
            //                                     //con.CloseReader();
            //                                     //double ICT = (double)rdr["IdealCycleTime"];
            //    double Performance = (ICT * TotalCount) / RunTime;

            //    double OEE = Quality * Performance * Availability;



            //    //Save input data into MachineStatus table 
            //    con.SetCommand("Insert into MachineStatus (Machine_ID, MachineName, RunTimeStarting, RunTimeEnding, Product_ID, RunTimeLength) Values(@MachineID, @MachineName, @RunTimeStarting, @RunTimeEnding, @pid, @RunTimeLength)");
            //    con.com.Parameters.AddWithValue("@MachineID", ddlMachine_ID.SelectedItem.Value); //Save MachineID
            //    con.com.Parameters.AddWithValue("@MachineName", ddlMachine_ID.SelectedItem.Text); //Save MachineName
            //    con.com.Parameters.AddWithValue("@RunTimeStarting", SqlDbType.DateTime).Value = TimeForRunStart;//Save the date and time that machine start working
            //    con.com.Parameters.AddWithValue("@RunTimeEnding", SqlDbType.DateTime).Value = TimeForRunEnd; //Save the date and time that machine end working
            //    con.com.Parameters.AddWithValue("@RunTimeLength", RunTime); //Save the runtime (hours, calculated before) in to the table
            //    con.com.Parameters.AddWithValue("@pid", DropDownListProductsM.SelectedValue);
            //    con.NonQueryEx();

            //    //Save the data into DownTime table
            //    //con.SetCommand("Insert into DownTime (MachineID, DownTimeStarting, DownTimeEnding, CauseDescription, DownTime) Values (@MachineID, @DownTimeStarting, @DownTimeEnding, @CauseDescription, @DownTime)");
            //    //con.com.Parameters.AddWithValue("@MachineID", ddlMachine_ID.SelectedItem.Value); //Save MachineID
            //    //con.com.Parameters.AddWithValue("@DownTimeStarting", SqlDbType.DateTime).Value = TimeForDownStart; //Save the date and time that machine stop suddenly
            //    //con.com.Parameters.AddWithValue("@DownTimeEnding", SqlDbType.DateTime).Value = TimeForDownEnd; //Save the date and time that machine work again (after repairing)
            //    //con.com.Parameters.AddWithValue("@CauseDescription", ddlCauseDescription.SelectedItem.Value); //Save the reason of the accident (which should be input by managers and will be saved into the CauseDescription table), the dropdownlist show from the database 
            //    //con.com.Parameters.AddWithValue("@DownTime", DownTime); //Save the time length of the downtime which was calculate before
            //    //con.NonQueryEx();


            //    //Save the data into Output table, which include the elements to calculate quality result & performance result
            //    con.SetCommand("Insert into OutputData (Machine_ID, TotalCount, RejectedCount, GoodCount, Product_ID) Values (@Machine_ID, @TotalCount, @RejectedCount, @GoodCount, @pid)");
            //    con.com.Parameters.AddWithValue("@Machine_ID", ddlMachine_ID.SelectedItem.Value); //Save machineID
            //    con.com.Parameters.AddWithValue("@TotalCount", txbTotalCount.Text); //Save the total count
            //    con.com.Parameters.AddWithValue("@RejectedCount", txbRejectedCount.Text);
            //    con.com.Parameters.AddWithValue("@GoodCount", GoodCount);
            //    con.com.Parameters.AddWithValue("@pid", DropDownListProductsM.SelectedValue);
            //    con.NonQueryEx();

            //    int dailyCount = 0;
            //    con.SetCommand("Select DailyCount From OEEResult WHERE day = @today");
            //    con.com.Parameters.AddWithValue("@today", DateTime.Today.Date);
            //    dailyCount = con.IntScalar(); //value of how many machines entered so far in day

            //    if (dailyCount != 0)
            //    {
            //        //con.SetCommand("Select DailyCount From OEEResult WHERE day = @today");
            //        //con.com.Parameters.AddWithValue("@today", DateTime.Today.Date);
            //        //int dailyCount = con.IntScalar(); //value of how many machines entered so far in day
            //        dailyCount++; //increases dailycount by 1 to be used to calculate daily average results
            //        con.SetCommand("Select AvailabilityResult from OEEResult WHERE day = @today");
            //        con.com.Parameters.AddWithValue("@today", DateTime.Today.Date);
            //        double todaysAvailability = con.DoubleScalar();
            //        con.SetCommand("Select PerformanceResult from OEEResult WHERE day = @today");
            //        con.com.Parameters.AddWithValue("@today", DateTime.Today.Date);
            //        double todaysPerformance = con.DoubleScalar();
            //        con.SetCommand("Select QualityResult from OEEResult WHERE day = @today");
            //        con.com.Parameters.AddWithValue("@today", DateTime.Today.Date);
            //        double todaysQuality = con.DoubleScalar();
            //        con.SetCommand("Select OEEResult from OEEResult WHERE day = @today");
            //        con.com.Parameters.AddWithValue("@today", DateTime.Today.Date);
            //        //double todaysOEEResult = todaysAvailability * todaysPerformance * todaysQuality;
            //        //gets the current values from OEEresult table
            //        double avgAvailability = (todaysAvailability + Availability) / dailyCount;
            //        double avgPerformance = (todaysPerformance + Performance) / dailyCount;
            //        double avgQuality = (todaysQuality + Quality) / dailyCount;
            //        double avgOEEResult = avgAvailability * avgPerformance * avgQuality;

            //        // con.SetCommand("Update OEEResult Set AvailabilityResult = @avgA, PerformanceResult = @avgP, QualityResult = @avgQ, OEEResult = @avgOEE, DailyCount = @dc WHERE Day = @today");
            //        con.SetCommand("Update OEEResult Set AvailabilityResult = @avgA WHERE Day = @today");
            //        con.com.Parameters.AddWithValue("@avgA", avgAvailability);
            //        con.com.Parameters.AddWithValue("@today", DateTime.Today);
            //        con.NonQueryEx();
            //        con.SetCommand("Update OEEResult Set PerformanceResult = @avgP WHERE Day = @today");
            //        con.com.Parameters.AddWithValue("@avgP", avgPerformance);
            //        con.com.Parameters.AddWithValue("@today", DateTime.Today);
            //        con.NonQueryEx();
            //        con.SetCommand("Update OEEResult Set QualityResult = @avgQ WHERE Day = @today");
            //        con.com.Parameters.AddWithValue("@avgQ", avgQuality);
            //        con.com.Parameters.AddWithValue("@today", DateTime.Today);
            //        con.NonQueryEx();
            //        con.SetCommand("Update OEEResult Set OEEResult = @avgOEE WHERE Day = @today");
            //        con.com.Parameters.AddWithValue("@avgOEE", avgOEEResult);
            //        con.com.Parameters.AddWithValue("@today", DateTime.Today);
            //        con.NonQueryEx();
            //        con.SetCommand("Update OEEResult Set dailyCount = @dc WHERE Day = @today");
            //        con.com.Parameters.AddWithValue("@dc", dailyCount);
            //        con.com.Parameters.AddWithValue("@today", DateTime.Today);



            //        con.NonQueryEx();
            //        //updates table with average results for all machines of the day
            //    }

            //    else
            //    {

            //        //Save data into OEEResult table
            //        con.SetCommand("Insert into OEEResult(QualityResult, PerformanceResult, AvailabilityResult, OEEResult, Day, DailyCount) Values (@QualityResult, @PerformanceResult, @AvailabilityResult, @OEEResult, @Date, @Dc)");
            //        con.com.Parameters.AddWithValue("@QualityResult", Quality);
            //        con.com.Parameters.AddWithValue("@PerformanceResult", Performance);
            //        con.com.Parameters.AddWithValue("@AvailabilityResult", Availability);
            //        con.com.Parameters.AddWithValue("@OEEResult", OEE);
            //        con.com.Parameters.AddWithValue("@Date", DateTime.Today);
            //        con.com.Parameters.AddWithValue("@Dc", 1);
            //        con.NonQueryEx();
            //    }
            //    con.CloseReader();
            //    con.CloseCon();
            //}
            //catch (FormatException)
            //{
            //    LabelDataInput.Text = "Data input failed! Check data entered is in correct format!";
            //}

        }


        protected void ddlWorkerID_SelectedIndexChanged(object sender, EventArgs e)
        {
        //    //ddlMachine_ID.ClearSelection(); //changes machine list to default as the machine name changes on page load
        //    //Show the Worker name from selecting worker ID
        //    string selectedWorkerID = ddlWorkerID.SelectedItem.Value;
        //    con = new SqlDbConnection();
        //    con.SetCommand("Select * From Workers Where Worker_ID= @Worker_ID");
        //    con.com.Parameters.AddWithValue("@Worker_ID", selectedWorkerID);
        //    con.com.CommandType = CommandType.Text;
        //    SqlDataReader reader = con.ExecuteDataReader();
        //    if (reader.HasRows)
        //    {
        //        reader.Read();
        //        txbWorkerName.Text = reader.GetString(1);
        //    }
        //    con.CloseReader();
        //    con.CloseCon();
        }




        protected void Button1_Click(object sender, EventArgs e)  //add employee shift
        {


            try
            {
                EmployeeShift empShift = new EmployeeShift(DateTime.Parse(txbWorkerStartingTime.Text, culture, System.Globalization.DateTimeStyles.AssumeLocal), DateTime.Parse(txbWorkerEndingTime.Text, culture, System.Globalization.DateTimeStyles.AssumeLocal), DateTime.Parse(txbBreakStart.Text, culture, System.Globalization.DateTimeStyles.AssumeLocal), DateTime.Parse(txbBreakEnd.Text, culture, System.Globalization.DateTimeStyles.AssumeLocal));
                empShift.InsertRealWorkingTime(int.Parse(ddlWorkerID.SelectedItem.Value), ddlWorkerID.SelectedItem.Text);
                empShift.InsertRealBreak(int.Parse(ddlWorkerID.SelectedItem.Value), int.Parse(ddlBreakType.SelectedItem.Value), ddlWorkerID.SelectedItem.Text);
                empShift.CloseConnection();

                LabelDataInput.Text = "Employee data successfully added";
                LabelDataInput.ForeColor = Color.Green;


                if (IsPostBack)
                {


                    txbWorkerStartingTime.Text = "";
                    txbWorkerEndingTime.Text = "";
                    txbBreakStart.Text = "";
                    txbBreakEnd.Text = "";

                }
            }

            catch (FormatException)
            {
                LabelDataInput.Text = "Data input failed! Check data entered is in correct format!";
            }

            catch (SqlException)
            {
                LabelDataInput.Text = "Database Error!";
                LabelDataInput.ForeColor = Color.Red;
            }

            //try
            //{
            //    //Set datetime for wokers real working time and break time
            //    DateTime WorkingStartTime = DateTime.Parse(txbWorkerStartingTime.Text, culture, System.Globalization.DateTimeStyles.AssumeLocal);
            //    DateTime WorkingEndTime = DateTime.Parse(txbWorkerEndingTime.Text, culture, System.Globalization.DateTimeStyles.AssumeLocal);
            //    DateTime BreakStartingTime = DateTime.Parse(txbBreakStart.Text, culture, System.Globalization.DateTimeStyles.AssumeLocal);
            //    DateTime BreakEndTime = DateTime.Parse(txbBreakEnd.Text, culture, System.Globalization.DateTimeStyles.AssumeLocal);

            //    //Calculate the working length of workers and breaklength
            //    double WorkingLength = (WorkingEndTime - WorkingStartTime).TotalHours;
            //    double BreakLength = (BreakEndTime - BreakStartingTime).TotalHours;

            //    con = new SqlDbConnection();

            //    //Save needed data into RealWorkingTime table
            //    con.SetCommand("Insert into RealWorkingTime (Worker_ID, WorkingStartingTime, WorkingEndingTime, RealWorkingLength, WorkerName) Values(@Worker_ID, @WorkingStartingTime, @WorkingEndingTime, @RealWorkingLength, @WorkerName)");
            //    con.com.Parameters.AddWithValue("@Worker_ID", ddlWorkerID.SelectedItem.Value); //Save WorkerID
            //    con.com.Parameters.AddWithValue("@WorkingStartingTime", SqlDbType.DateTime).Value = WorkingStartTime; //Save the time of start to work
            //    con.com.Parameters.AddWithValue("@WorkingEndingTime", SqlDbType.DateTime).Value = WorkingEndTime; //Save the time that worker go off
            //    con.com.Parameters.AddWithValue("@RealWorkingLength", WorkingLength); //Save worker's working length (calculte before)
            //    con.com.Parameters.AddWithValue("@WorkerName", ddlWorkerID.SelectedItem.Text); //Save the worker name
            //    con.NonQueryEx();

            //    //Save the data into RealBreak table
            //    con.SetCommand("Insert into RealBreak (WorkerID, WorkerName, StartingTime, EndingTime, BreakType, RealBreakLength) Values(@WorkerID, @WorkerName, @StartingTime, @EndingTime, @BreakType, @RealBreakLength)");
            //    con.com.Parameters.AddWithValue("@WorkerID", ddlWorkerID.SelectedItem.Value); //Save worker ID
            //    con.com.Parameters.AddWithValue("@WorkerName", ddlWorkerID.SelectedItem.Text); //Save Worker name
            //    con.com.Parameters.AddWithValue("@StartingTime", SqlDbType.DateTime).Value = BreakStartingTime; //Save the time of break starting
            //    con.com.Parameters.AddWithValue("@EndingTime", SqlDbType.DateTime).Value = BreakEndTime; //Save the time that worker go back to work
            //    con.com.Parameters.AddWithValue("@BreakType", ddlBreakType.SelectedItem.Value); //Save break type, coffee lunch whatever
            //    con.com.Parameters.AddWithValue("@RealBreakLength", BreakLength); //Save the break length
            //    con.NonQueryEx();

            //    con.CloseCon();
            //}
            //catch (FormatException)
            //{
            //    LabelDataInput.Text = "Data input failed! Check data entered is in correct format!";
            //}

        }

        protected void ButtonTimeStartMachine_Click(object sender, EventArgs e)
        {
            LabelDataInput.Text = "";
            txbRunTimeStart.Text = DateTime.Now.ToString();
        }

        protected void ButtonMachineEndTime_Click(object sender, EventArgs e)
        {
            txbRunTimeEnd.Text = DateTime.Now.ToString();
            LabelDataInput.Text = "";
        }

        protected void ButtonWorkerStartTime_Click(object sender, EventArgs e)
        {
            LabelDataInput.Text = "";
            txbWorkerStartingTime.Text = DateTime.Now.ToString();
        }
      

        protected void ButtonWorkingEndTime_Click(object sender, EventArgs e)
        {
            txbWorkerEndingTime.Text = DateTime.Now.ToString();
            LabelDataInput.Text = "";
        }

        protected void ButtonBreakStartTime_Click(object sender, EventArgs e)
        {
            txbBreakStart.Text = DateTime.Now.ToString();
            LabelDataInput.Text = "";
        }

        protected void ButtonBreakEndTime_Click(object sender, EventArgs e)
        {
            txbBreakEnd.Text = DateTime.Now.ToString();
            LabelDataInput.Text = "";
        }

        protected void ButtonAddDownTime_Click(object sender, EventArgs e)
        {
            
            if (ddlCauseDescription.SelectedIndex != 0 && ddlMachine_ID.SelectedIndex != 0)
            {
                try
                {
                    DownTime downT = new oeeps.DownTime(DateTime.Parse(txbDownTimeStart.Text, culture, System.Globalization.DateTimeStyles.AssumeLocal), DateTime.Parse(txbDownTimeEnd.Text, culture, System.Globalization.DateTimeStyles.AssumeLocal));
                    downT.InsertDownTime(int.Parse(ddlMachine_ID.SelectedItem.Value), int.Parse(DropDownListProductsM.SelectedItem.Value), ddlCauseDescription.SelectedItem.Text);
                    downT.CloseConnection();

                    LabelDataInput.Text = "DownTime added Successfully";
                    LabelDataInput.ForeColor = Color.Green;

                    if (IsPostBack)
                    {
                        txbDownTimeStart.Text = "";
                        txbDownTimeEnd.Text = "";
                        LabelDataInput.Text = "";

                    }
                }

                catch (FormatException)
                {
                    LabelDataInput.Text = "Data input failed! Check data entered is in correct format!";
                    LabelDataInput.ForeColor = Color.Red;


                }

                catch (SqlException)
                {
                    LabelDataInput.Text = "Data input failed! Check data entered is in correct format!";
                    LabelDataInput.ForeColor = Color.Red;
                }
            }
            else
            {
                LabelDataInput.Text = "You must select a Product and Machine";
                LabelDataInput.ForeColor = Color.Red;
            }

            //try
            //{
            //    //add all downtime before adding full machine cycle
            //    con = new SqlDbConnection();
            //    DateTime TimeForDownStart = DateTime.Parse(txbDownTimeStart.Text, culture, System.Globalization.DateTimeStyles.AssumeLocal);
            //    DateTime TimeForDownEnd = DateTime.Parse(txbDownTimeEnd.Text, culture, System.Globalization.DateTimeStyles.AssumeLocal);
            //    double DownTime = (TimeForDownEnd - TimeForDownStart).TotalMinutes;

            //    con.SetCommand("Insert into DownTime (Machine_ID, DownTimeStarting, DownTimeEnding, CauseDescription, DownTime, Product_ID) Values (@MachineID, @DownTimeStarting, @DownTimeEnding, @CauseDescription, @DownTime, @pid)");
            //    con.com.Parameters.AddWithValue("@MachineID", ddlMachine_ID.SelectedItem.Value); //Save MachineID
            //    con.com.Parameters.AddWithValue("@DownTimeStarting", SqlDbType.DateTime).Value = TimeForDownStart; //Save the date and time that machine stop suddenly
            //    con.com.Parameters.AddWithValue("@DownTimeEnding", SqlDbType.DateTime).Value = TimeForDownEnd; //Save the date and time that machine work again (after repairing)
            //    con.com.Parameters.AddWithValue("@CauseDescription", ddlCauseDescription.SelectedItem.Text); //Save the reason of the accident (which should be input by managers and will be saved into the CauseDescription table), the dropdownlist show from the database 
            //    con.com.Parameters.AddWithValue("@DownTime", DownTime);
            //    con.com.Parameters.AddWithValue("@pid", DropDownListProductsM.SelectedValue);//Save the time length of the downtime which was calculate before
            //    con.NonQueryEx();
            //    con.CloseCon();


            //}

            //catch (FormatException)
            //{
            //    LabelDataInput.Text = "Data input failed! Check data entered is in correct format!";
            //}

        }

        protected void ButtonDownTimeStart_Click(object sender, EventArgs e)
        {
            txbDownTimeStart.Text = DateTime.Now.ToString();
            LabelDataInput.Text = "";
        }

        protected void ButtonDownTimeEnd_Click(object sender, EventArgs e)
        {
            txbDownTimeEnd.Text = DateTime.Now.ToString();
            LabelDataInput.Text = "";
        }
    }
}


