using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.DataVisualization.Charting;


namespace oeeps
{
    public partial class _Default : Page
    {
        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"] == null) //redirects to login page if not logged in
            {
                Response.Redirect("/Login");
                //Server.Transfer("admin.aspx", true);
            }

            if (Session["username"] != null) 
            {
                if ((string)(Session["accessLevel"]) == "3") //worker redirected to datainput
                {
                    //Server.Transfer("admin.aspx", true);
                    Response.Redirect("/dataInput");
                }
                
            }

            Chart1.ChartAreas[0].AxisY.Minimum = 0;  //sets chart max min on page load
            Chart1.ChartAreas[0].AxisY.Maximum = 1;
            Chart2.ChartAreas[0].AxisY.Minimum = 0;
            Chart2.ChartAreas[0].AxisY.Maximum = 1;
            Chart3.ChartAreas[0].AxisY.Minimum = 0;
            Chart3.ChartAreas[0].AxisY.Maximum = 1;
            Chart4.ChartAreas[0].AxisY.Minimum = 0;
            Chart4.ChartAreas[0].AxisY.Maximum = 1;
        }

        protected void SqlDataSourceQuality_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {

        }

        protected void rbtDaily_CheckedChanged(object sender, EventArgs e)
        {
            Chart1.DataSourceID = "SqlDataSourceOEE";   //uses datasource created in designer to get data from database for chart
            Chart1.Series["Series1"].XValueMember = "Day";
            Chart1.Series["Series1"].YValueMembers = "OEEResult";
            Chart1.ChartAreas[0].AxisY.Minimum = 0;
            Chart1.ChartAreas[0].AxisY.Maximum = 1;  //sets y axis to max one so charts are same scale if values vary
            Chart1.DataBind();

            Chart2.DataSourceID = "SqlDataSourceAvailability";
            Chart2.Series["Series2"].XValueMember = "Day";
            Chart2.Series["Series2"].YValueMembers = "AvailabilityResult";
            Chart2.ChartAreas[0].AxisY.Minimum = 0;
            Chart2.ChartAreas[0].AxisY.Maximum = 1;
            Chart2.DataBind();

            Chart3.DataSourceID = "SqlDataSourceQuality";
            Chart3.Series["Series3"].XValueMember = "Day";
            Chart3.Series["Series3"].YValueMembers = "QualityResult";
            Chart3.ChartAreas[0].AxisY.Minimum = 0;
            Chart3.ChartAreas[0].AxisY.Maximum = 1;
            Chart3.DataBind();

            Chart4.DataSourceID = "SqlDataSourcePerformance";
            Chart4.Series["Series4"].XValueMember = "Day";
            Chart4.Series["Series4"].YValueMembers = "PerformanceResult";
            Chart4.ChartAreas[0].AxisY.Minimum = 0;
            Chart4.ChartAreas[0].AxisY.Maximum = 1;
            Chart4.DataBind();

        }



        protected void rbtWeekly_CheckedChanged(object sender, EventArgs e)
        {
            Chart1.DataSourceID = "SqlDataSourceWeekly";
            Chart1.Series["Series1"].XValueMember = "Week";
            Chart1.Series["Series1"].YValueMembers = "OEE";
            Chart1.ChartAreas[0].AxisY.Minimum = 0;
            Chart1.ChartAreas[0].AxisY.Maximum = 1;
            Chart1.DataBind();

            Chart2.DataSourceID = "SqlDataSourceAvailabilityWeekly";
            Chart2.Series["Series2"].XValueMember = "Week";
            Chart2.Series["Series2"].YValueMembers = "Availability";
            Chart2.ChartAreas[0].AxisY.Minimum = 0;
            Chart2.ChartAreas[0].AxisY.Maximum = 1;
            Chart2.DataBind();

            Chart3.DataSourceID = "SqlDataSourceQualityWeekly";
            Chart3.Series["Series3"].XValueMember = "Week";
            Chart3.Series["Series3"].YValueMembers = "Quality";
            Chart3.ChartAreas[0].AxisY.Minimum = 0;
            Chart3.ChartAreas[0].AxisY.Maximum = 1;
            Chart3.DataBind();

            Chart4.DataSourceID = "SqlDataSourcePerformanceWeekly";
            Chart4.Series["Series4"].XValueMember = "Week";
            Chart4.Series["Series4"].YValueMembers = "Performance";
            Chart4.ChartAreas[0].AxisY.Minimum = 0;
            Chart4.ChartAreas[0].AxisY.Maximum = 1;
            Chart4.DataBind();

        }

        protected void rbtMonthly_CheckedChanged(object sender, EventArgs e)  //custom sql statement to get monthly values from daily results
        {
            Chart1.DataSourceID = "SqlDataSourceMonthly";
            Chart1.Series["Series1"].XValueMember = "month";
            Chart1.Series["Series1"].YValueMembers = "OEE";
            Chart1.ChartAreas[0].AxisY.Minimum = 0;
            Chart1.ChartAreas[0].AxisY.Maximum = 1;
            Chart1.DataBind();

            Chart2.DataSourceID = "SqlDataSourceAvailabilityMonthly";
            Chart2.Series["Series2"].XValueMember = "month";
            Chart2.Series["Series2"].YValueMembers = "Availability";
            Chart2.ChartAreas[0].AxisY.Minimum = 0;
            Chart2.ChartAreas[0].AxisY.Maximum = 1;
            Chart2.DataBind();

            Chart3.DataSourceID = "SqlDataSourceQualityMonthly";
            Chart3.Series["Series3"].XValueMember = "month";
            Chart3.Series["Series3"].YValueMembers = "Quality";
            Chart3.ChartAreas[0].AxisY.Minimum = 0;
            Chart3.ChartAreas[0].AxisY.Maximum = 1;
            Chart3.DataBind();

            Chart4.DataSourceID = "SqlDataSourcePerformanceMonthly";
            Chart4.Series["Series4"].XValueMember = "month";
            Chart4.Series["Series4"].YValueMembers = "Performance";
            Chart4.ChartAreas[0].AxisY.Minimum = 0;
            Chart4.ChartAreas[0].AxisY.Maximum = 1;
            Chart4.DataBind();
        }

        protected void rbtYearly_CheckedChanged(object sender, EventArgs e)
        {
            Chart1.DataSourceID = "SqlDataSourceYearly";
            Chart1.Series["Series1"].XValueMember = "ResultYear";
            Chart1.Series["Series1"].YValueMembers = "Column1";
            Chart1.ChartAreas[0].AxisY.Minimum = 0;
            Chart1.ChartAreas[0].AxisY.Maximum = 1;
            Chart1.DataBind();

            Chart2.DataSourceID = "SqlDataSourceAvailabilityYearly";
            Chart2.Series["Series2"].XValueMember = "ResultYear";
            Chart2.Series["Series2"].YValueMembers = "Column1";
            Chart2.ChartAreas[0].AxisY.Minimum = 0;
            Chart2.ChartAreas[0].AxisY.Maximum = 1;
            Chart2.DataBind();

            Chart3.DataSourceID = "SqlDataSourceQualityYearly";
            Chart3.Series["Series3"].XValueMember = "ResultYear";
            Chart3.Series["Series3"].YValueMembers = "Column1";
            Chart3.ChartAreas[0].AxisY.Minimum = 0;
            Chart3.ChartAreas[0].AxisY.Maximum = 1;
            Chart3.DataBind();

            Chart4.DataSourceID = "SqlDataSourcePerformanceYearly";
            Chart4.Series["Series4"].XValueMember = "ResultYear";
            Chart4.Series["Series4"].YValueMembers = "Column1";
            Chart4.ChartAreas[0].AxisY.Minimum = 0;
            Chart4.ChartAreas[0].AxisY.Maximum = 1;
            Chart4.DataBind();
        }
    }
}