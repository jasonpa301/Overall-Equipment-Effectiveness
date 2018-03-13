<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="oeeps._Default" %>

<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanelCharts" runat="server">
        <ContentTemplate>
    <br /><br /><br />
    <div class="oee">
        <h1>OEE:</h1>
        <p class="oeer">
            <asp:Chart ID="Chart1" runat="server" DataSourceID="SqlDataSourceOEE" Height="323px" Width="623px"  PaletteCustomColors="RoyalBlue; ; Red" BackGradientStyle="LeftRight">
                <Series>
                    <asp:Series Name="Series1" XValueMember="Day" YValueMembers="OEEResult" BackGradientStyle="VerticalCenter" Color="Navy">
                    </asp:Series>
                </Series>
                <ChartAreas>
                    <asp:ChartArea Name="ChartArea1">
                    </asp:ChartArea>
                </ChartAreas>
            </asp:Chart>
            <br />
            <asp:SqlDataSource ID="SqlDataSourceYearly" runat="server" ConnectionString="<%$ ConnectionStrings:oeeConnectionString %>" SelectCommand="SELECT AVG(OEEResult), resultYear FROM OEEResult GROUP BY resultYear"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSourceMonthly" runat="server" ConnectionString="<%$ ConnectionStrings:oeeConnectionString %>" SelectCommand="SELECT AVG(OEEResult) AS OEE, CONCAT(resultMonth,' ', resultYear) AS month From OEEResult WHERE Day &gt; DATEADD(mm, -12,getDate()) GROUP BY CONCAT(resultMonth ,' ',resultYear)"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSourceWeekly" runat="server" ConnectionString="<%$ ConnectionStrings:oeeConnectionString %>" SelectCommand="SELECT AVG(OEEResult) AS OEE, CONCAT(ResultWeek,' ',ResultYear) AS Week FROM OEEResult WHERE Day &gt; DATEADD(wk, -20,getDate()) GROUP BY CONCAT(ResultWeek,' ',ResultYear)"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSourceOEE" runat="server" ConnectionString="<%$ ConnectionStrings:oeeConnectionString %>" SelectCommand="SELECT Day, OEEResult FROM OEEResult WHERE Day &gt; (getDate() - 30) ORDER BY Day"></asp:SqlDataSource>
            <asp:RadioButton ID="rbtDaily" runat="server" Text="Daily" GroupName="Results" OnCheckedChanged="rbtDaily_CheckedChanged" AutoPostBack="True" />
            <asp:RadioButton ID="rbtWeekly" runat="server" Text="Weekly" GroupName="Results" AutoPostBack="True" OnCheckedChanged="rbtWeekly_CheckedChanged" />
            <asp:RadioButton ID="rbtMonthly" runat="server" Text="Monthly" GroupName="Results" AutoPostBack="True" OnCheckedChanged="rbtMonthly_CheckedChanged" />
            <asp:RadioButton ID="rbtYearly" runat="server" Text="Yearly" GroupName="Results" AutoPostBack="True" OnCheckedChanged="rbtYearly_CheckedChanged" />
        </p>
    </div>

    <div class="availability">
               <h3>Availability:</h3>
        <p class="availabilityr">
            <asp:Chart ID="Chart2" runat="server" DataSourceID="SqlDataSourceAvailability" Height="149px" Width="326px" Palette="Bright">
                <Series>
                    <asp:Series Name="Series2" XValueMember="Day" YValueMembers="AvailabilityResult" BackGradientStyle="VerticalCenter" Color="Lime">
                    </asp:Series>
                </Series>
                <ChartAreas>
                    <asp:ChartArea Name="ChartArea1">
                    </asp:ChartArea>
                </ChartAreas>
            </asp:Chart>
            <asp:SqlDataSource ID="SqlDataSourceAvailabilityYearly" runat="server" ConnectionString="<%$ ConnectionStrings:oeeConnectionString %>" SelectCommand="SELECT AVG(AvailabilityResult), resultYear FROM OEEResult GROUP BY resultYear"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSourceAvailabilityMonthly" runat="server" ConnectionString="<%$ ConnectionStrings:oeeConnectionString %>" SelectCommand="SELECT AVG(AvailabilityResult) AS Availability, CONCAT(resultMonth,' ', resultYear) AS month From OEEResult WHERE Day &gt; DATEADD(mm, -12,getDate()) GROUP BY CONCAT(resultMonth ,' ',resultYear)"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSourceAvailabilityWeekly" runat="server" ConnectionString="<%$ ConnectionStrings:oeeConnectionString %>" SelectCommand="SELECT AVG(AvailabilityResult) AS Availability, CONCAT(ResultWeek,' ',ResultYear) AS Week FROM OEEResult WHERE Day &gt; DATEADD(wk, -20,getDate()) GROUP BY CONCAT(ResultWeek,' ',ResultYear)"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSourceAvailability" runat="server" ConnectionString="<%$ ConnectionStrings:oeeConnectionString %>" SelectCommand="SELECT Day, AvailabilityResult FROM OEEResult WHERE Day &gt; (getDate() - 30) ORDER BY Day"></asp:SqlDataSource>
        </p>     
          <h3>Quality:</h3>
        <p class="qualityr">
            <asp:Chart ID="Chart3" runat="server" DataSourceID="SqlDataSourceQuality" Height="149px" Width="326px">
                <Series>
                    <asp:Series Name="Series3" XValueMember="Day" YValueMembers="QualityResult" BackGradientStyle="VerticalCenter" >
                    </asp:Series>
                </Series>
                <ChartAreas>
                    <asp:ChartArea Name="ChartArea1">
                    </asp:ChartArea>
                </ChartAreas>
            </asp:Chart>
            <asp:SqlDataSource ID="SqlDataSourceQualityYearly" runat="server" ConnectionString="<%$ ConnectionStrings:oeeConnectionString %>" SelectCommand="SELECT AVG(QualityResult), resultYear FROM OEEResult GROUP BY resultYear"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSourceQualityMonthly" runat="server" ConnectionString="<%$ ConnectionStrings:oeeConnectionString %>" SelectCommand="SELECT AVG(QualityResult) AS Quality, CONCAT(resultMonth,' ', resultYear) AS month From OEEResult WHERE Day &gt; DATEADD(mm, -12,getDate()) GROUP BY CONCAT(resultMonth ,' ',resultYear)"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSourceQualityWeekly" runat="server" ConnectionString="<%$ ConnectionStrings:oeeConnectionString %>" SelectCommand="SELECT AVG(QualityResult) AS Quality, CONCAT(ResultWeek,' ',ResultYear) AS Week FROM OEEResult WHERE Day &gt; DATEADD(wk, -20,getDate()) GROUP BY CONCAT(ResultWeek,' ',ResultYear)"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSourceQuality" runat="server" ConnectionString="<%$ ConnectionStrings:oeeConnectionString %>" SelectCommand="SELECT Day, QualityResult FROM OEEResult WHERE Day &gt; (getDate() - 30) ORDER BY Day" OnSelecting="SqlDataSourceQuality_Selecting"></asp:SqlDataSource>
        </p>       
        <h3>Performance:</h3>
        <p class="performancer">
            <asp:Chart ID="Chart4" runat="server" DataSourceID="SqlDataSourcePerformance" Height="149px" Width="326px" Palette="Berry" >
                <Series>
                    <asp:Series Name="Series4" XValueMember="Day" YValueMembers="PerformanceResult" BackGradientStyle="VerticalCenter">
                    </asp:Series>
                </Series>
                <ChartAreas>
                    <asp:ChartArea Name="ChartArea1">
                    </asp:ChartArea>
                </ChartAreas>
            </asp:Chart>
            <asp:SqlDataSource ID="SqlDataSourcePerformanceYearly" runat="server" ConnectionString="<%$ ConnectionStrings:oeeConnectionString %>" SelectCommand="SELECT AVG(PerformanceResult), resultYear FROM OEEResult GROUP BY resultYear"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSourcePerformanceMonthly" runat="server" ConnectionString="<%$ ConnectionStrings:oeeConnectionString %>" SelectCommand="SELECT AVG(PerformanceResult) AS Performance, CONCAT(resultMonth,' ', resultYear) AS month From OEEResult WHERE Day &gt; DATEADD(mm, -12,getDate()) GROUP BY CONCAT(resultMonth ,' ',resultYear)"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSourcePerformanceWeekly" runat="server" ConnectionString="<%$ ConnectionStrings:oeeConnectionString %>" SelectCommand="SELECT AVG(PerformanceResult) AS Performance, CONCAT(ResultWeek,' ',ResultYear) AS Week FROM OEEResult WHERE Day &gt; DATEADD(wk, -20,getDate()) GROUP BY CONCAT(ResultWeek,' ',ResultYear)"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSourcePerformance" runat="server" ConnectionString="<%$ ConnectionStrings:oeeConnectionString %>" SelectCommand="SELECT Day, PerformanceResult FROM OEEResult WHERE Day &gt; (getDate() - 30) ORDER BY Day"></asp:SqlDataSource>
        </p>
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
