using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace oeeps
{
    public class OOEResult
    {
        private SqlDbConnection con = new SqlDbConnection();
        private double availability;
        private double performance;
        private double quality;
        private double oeeR;
        DateTime startTime;
        DateTime endTime;
        double runTime;

        public OOEResult(DateTime _startTime, DateTime _endTime)
        {
            startTime = _startTime;
            endTime = _endTime;
        }

        public void CalculateAvailability(int machineId)
        {
            con.SetCommand("SELECT SUM(DownTime) FROM DownTime WHERE DownTimeStarting > @rs AND DownTimeStarting < @re AND Machine_ID = @id");
            con.com.Parameters.AddWithValue("@rs", startTime);
            con.com.Parameters.AddWithValue("@re", endTime);
            con.com.Parameters.AddWithValue("@id", machineId);
            string tempString = con.StringScalar();
            double downTime;
            if (tempString == "")
            {
                downTime = 0;
            }
            else
            {
                downTime = double.Parse(tempString);
            }
            runTime = (endTime - startTime).TotalMinutes;
            availability = (runTime - downTime) / runTime;
        }

        public void CalculateQuality(double totalCount, double rejectedCount)
        {
            double goodCount = totalCount - rejectedCount;
            quality = goodCount / totalCount;
        }

        public void CalculatePerformance(int machineId, int productId, double totalCount)
        {
            con.SetCommand("Select IdealCycleTime From IdealCycleTimes Where Machine_ID = @mid AND Product_ID = @pid");
            con.com.Parameters.AddWithValue("@mid", machineId);
            con.com.Parameters.AddWithValue("@pid", productId);
           
            double ICT = con.DoubleScalar();

            performance = (ICT * totalCount) / runTime;
        }

        public void InsertMachineStatus(int machineId, string machineName, int productId)
        {
            con.SetCommand("Insert into MachineStatus (Machine_ID, MachineName, RunTimeStarting, RunTimeEnding, Product_ID, RunTimeLength) Values(@MachineID, @MachineName, @RunTimeStarting, @RunTimeEnding, @pid, @RunTimeLength)");
            con.com.Parameters.AddWithValue("@MachineID", machineId); //Save MachineID
            con.com.Parameters.AddWithValue("@MachineName", machineName); //Save MachineName
            con.com.Parameters.AddWithValue("@RunTimeStarting", SqlDbType.DateTime).Value = startTime;//Save the date and time that machine start working
            con.com.Parameters.AddWithValue("@RunTimeEnding", SqlDbType.DateTime).Value = endTime; //Save the date and time that machine end working
            con.com.Parameters.AddWithValue("@RunTimeLength", runTime); //Save the runtime (hours, calculated before) in to the table
            con.com.Parameters.AddWithValue("@pid", productId);
            con.NonQueryEx();
        }

        public void InsertOutputData(int machineId, double totalCount, double rejectedCount, int productId)
        {
            //Save the data into Output table, which include the elements to calculate quality result & performance result
            con.SetCommand("Insert into OutputData (Machine_ID, TotalCount, RejectedCount, GoodCount, Product_ID) Values (@Machine_ID, @TotalCount, @RejectedCount, @GoodCount, @pid)");
            con.com.Parameters.AddWithValue("@Machine_ID", machineId); //Save machineID
            con.com.Parameters.AddWithValue("@TotalCount", totalCount); //Save the total count
            con.com.Parameters.AddWithValue("@RejectedCount", rejectedCount);
            con.com.Parameters.AddWithValue("@GoodCount", (totalCount - rejectedCount));
            con.com.Parameters.AddWithValue("@pid", productId);
            con.NonQueryEx();
        }

        public int GetDailyCount()
        {

            int dailyCount;
            con.SetCommand("Select DailyCount From OEEResult WHERE day = @today");
            con.com.Parameters.AddWithValue("@today", DateTime.Today.Date);
            string tempString = con.StringScalar(); //value of how many machines entered so far in day
            if (string.IsNullOrEmpty(tempString))
            {
                dailyCount = 0;
            }
            else
            {
                dailyCount = int.Parse(tempString);
            }

            return dailyCount;
        }

        public void UpdateOEE(int dailyCount)
        {
            dailyCount++; //increases dailycount by 1 to be used to calculate daily average results
            con.SetCommand("Select AvailabilityResult from OEEResult WHERE day = @today");
            con.com.Parameters.AddWithValue("@today", DateTime.Today.Date);
            double todaysAvailability = con.DoubleScalar();
            con.SetCommand("Select PerformanceResult from OEEResult WHERE day = @today");
            con.com.Parameters.AddWithValue("@today", DateTime.Today.Date);
            double todaysPerformance = con.DoubleScalar();
            con.SetCommand("Select QualityResult from OEEResult WHERE day = @today");
            con.com.Parameters.AddWithValue("@today", DateTime.Today.Date);
            double todaysQuality = con.DoubleScalar();
            con.SetCommand("Select OEEResult from OEEResult WHERE day = @today");
            con.com.Parameters.AddWithValue("@today", DateTime.Today.Date);
            //double todaysOEEResult = todaysAvailability * todaysPerformance * todaysQuality;
            //gets the current values from OEEresult table
            double avgAvailability = (todaysAvailability + availability) / dailyCount;
            double avgPerformance = (todaysPerformance + performance) / dailyCount;
            double avgQuality = (todaysQuality + quality) / dailyCount;
            double avgOEEResult = avgAvailability * avgPerformance * avgQuality;

            // con.SetCommand("Update OEEResult Set AvailabilityResult = @avgA, PerformanceResult = @avgP, QualityResult = @avgQ, OEEResult = @avgOEE, DailyCount = @dc WHERE Day = @today");
            con.SetCommand("Update OEEResult Set AvailabilityResult = @avgA WHERE Day = @today");
            con.com.Parameters.AddWithValue("@avgA", avgAvailability);
            con.com.Parameters.AddWithValue("@today", DateTime.Today);
            con.NonQueryEx();
            con.SetCommand("Update OEEResult Set PerformanceResult = @avgP WHERE Day = @today");
            con.com.Parameters.AddWithValue("@avgP", avgPerformance);
            con.com.Parameters.AddWithValue("@today", DateTime.Today);
            con.NonQueryEx();
            con.SetCommand("Update OEEResult Set QualityResult = @avgQ WHERE Day = @today");
            con.com.Parameters.AddWithValue("@avgQ", avgQuality);
            con.com.Parameters.AddWithValue("@today", DateTime.Today);
            con.NonQueryEx();
            con.SetCommand("Update OEEResult Set OEEResult = @avgOEE WHERE Day = @today");
            con.com.Parameters.AddWithValue("@avgOEE", avgOEEResult);
            con.com.Parameters.AddWithValue("@today", DateTime.Today);
            con.NonQueryEx();
            con.SetCommand("Update OEEResult Set dailyCount = @dc WHERE Day = @today");
            con.com.Parameters.AddWithValue("@dc", dailyCount);
            con.com.Parameters.AddWithValue("@today", DateTime.Today);



            con.NonQueryEx();
        }

        public void InsertOeeData()
        {
            oeeR = quality * performance * availability;
            //Save data into OEEResult table
            con.SetCommand("Insert into OEEResult(QualityResult, PerformanceResult, AvailabilityResult, OEEResult, Day, DailyCount) Values (@QualityResult, @PerformanceResult, @AvailabilityResult, @OEEResult, @Date, @Dc)");
            con.com.Parameters.AddWithValue("@QualityResult", quality);
            con.com.Parameters.AddWithValue("@PerformanceResult", performance);
            con.com.Parameters.AddWithValue("@AvailabilityResult", availability);
            con.com.Parameters.AddWithValue("@OEEResult", oeeR);
            con.com.Parameters.AddWithValue("@Date", DateTime.Today);
            con.com.Parameters.AddWithValue("@Dc", 1);
            con.NonQueryEx();
        }

        public void CloseConnection()
        {
            con.CloseCon();
        }
    }
}