<%@ Page Title="Data Input for Workers" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="dataInput.aspx.cs" Inherits="oeeps.dataInput"%>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanelDataInput" runat="server">
    <ContentTemplate>
    <br /><br /><br />
    <h2>Data Input Page
        <asp:Label ID="LabelDataInput" runat="server" CssClass="errorMessage" Font-Overline="False" Font-Size="Medium" ForeColor="Red"></asp:Label>
    </h2>
    <div class ="InputMachineInfoDiv">
    <h3 style="font-size: x-large">Machine Info </h3>
    <span style="font-size: medium">Select Machine:</span>
    <br />
        <asp:DropDownList ID="ddlMachine_ID" runat="server" Height="20px" Width="300px" DataSourceID="SqlDataSourceM" DataTextField="MachineName" DataValueField="Machine_ID" OnSelectedIndexChanged="ddlMachine_ID_SelectedIndexChanged" AutoPostBack="True" AppendDataBoundItems="True">
        <asp:ListItem>---Select Machine---</asp:ListItem>
        </asp:DropDownList>
    
        <asp:SqlDataSource ID="SqlDataSourceM" runat="server" ConnectionString="<%$ ConnectionStrings:oeeConnectionString %>" SelectCommand="SELECT [MachineName], [Machine_ID] FROM [MachineInfo] ORDER BY [MachineName]">
        </asp:SqlDataSource>
    
        <br />
        <br />
        <span style="font-size: medium">Select Product:</span>
        <br />
        <asp:DropDownList ID="DropDownListProductsM" runat="server" Height="20px" Width="300px" AppendDataBoundItems="True">
            <asp:ListItem>---Select Product---</asp:ListItem>
        </asp:DropDownList><br />
        <br />
        <span style="font-size: medium">Starting Time:&nbsp;&nbsp;
        <asp:Button ID="ButtonTimeStartMachine" runat="server" Height="25px" OnClick="ButtonTimeStartMachine_Click" Text="Use Current Time" Width="172px" CausesValidation="False" />
        </span>
        <br />
        <asp:TextBox ID="txbRunTimeStart" runat="server" Height="20px" Width="300px"></asp:TextBox>
        <ajaxToolkit:CalendarExtender ID="txbRunTimeStart_CalendarExtender" runat="server" TargetControlID="txbRunTimeStart" Format="dd/MM/yyyy HH':'mm':'ss" />
        <br />
        <span style="font-size: medium">Ending Time:&nbsp;&nbsp;&nbsp;
        </span>
        <asp:Button ID="ButtonMachineEndTime" runat="server" Height="25px" OnClick="ButtonMachineEndTime_Click" Text="Use Current Time" Width="172px" CausesValidation="False" />
        <br />
        <asp:TextBox ID="txbRunTimeEnd" runat="server" Height="20px" Width="300px"></asp:TextBox>
        <ajaxToolkit:CalendarExtender ID="txbRunTimeEnd_CalendarExtender" runat="server" TargetControlID="txbRunTimeEnd" Format="dd/MM/yyyy HH':'mm':'ss" />
        <br />
        <asp:CompareValidator ID="CompareValidatorRunTime" runat="server" ErrorMessage="Ending time must be after Starting Time" ControlToCompare="txbRunTimeStart" ControlToValidate="txbRunTimeEnd" ForeColor="Red" Operator="GreaterThan"></asp:CompareValidator>
        <br />
        <span style="font-size: medium">Down Time Start:
        </span>
        <asp:Button ID="ButtonDownTimeStart" runat="server" Text="Use Current Time" OnClick="ButtonDownTimeStart_Click" Width="158px" CausesValidation="False" Height="25px" />
        <br />
        <asp:TextBox ID="txbDownTimeStart" runat="server" Height="20px" Width="300px"></asp:TextBox>
        <ajaxToolkit:CalendarExtender ID="txbDownTimeStart_CalendarExtender" runat="server" TargetControlID="txbDownTimeStart" Format="dd/MM/yyyy HH':'mm':'ss"/>
        <br />
        <span style="font-size: medium">Down Time End:</span>
        <asp:Button ID="ButtonDownTimeEnd" runat="server" Text="Use Current Time" Height="25px" OnClick="ButtonDownTimeEnd_Click" Width="162px" CausesValidation="False" />
        <br />
        <asp:TextBox ID="txbDownTimeEnd" runat="server" Height="20px" Width="300px"></asp:TextBox>
        <ajaxToolkit:CalendarExtender ID="txbDownTimeEnd_CalendarExtender" runat="server" TargetControlID="txbDownTimeEnd" Format="dd/MM/yyyy HH':'mm':'ss"/>
        <br />
        <asp:CompareValidator ID="CompareValidatorDownTime" runat="server" ErrorMessage="End time must be after Start Time" ControlToCompare="txbDownTimeStart" ControlToValidate="txbDownTimeEnd" ForeColor="Red" Operator="GreaterThan"></asp:CompareValidator>
        <br />
        <span style="font-size: medium">Cause Description:</span>
        <br />
        <asp:DropDownList ID="ddlCauseDescription" runat="server" Height="20px" Width="300px" DataSourceID="SqlDataSourceCause" DataTextField="CauseDescription" DataValueField="CauseID" AutoPostBack="True" AppendDataBoundItems="True">
            <asp:ListItem>---Select Cause---</asp:ListItem>
        </asp:DropDownList>
        <asp:SqlDataSource ID="SqlDataSourceCause" runat="server" ConnectionString="<%$ ConnectionStrings:oeeConnectionString %>" SelectCommand="SELECT [CauseID], [CauseDescription] FROM [CauseDescription] ORDER BY [CauseDescription]"></asp:SqlDataSource>
        <asp:Button ID="ButtonAddDownTime" runat="server" OnClick="ButtonAddDownTime_Click" Text="Add DownTime" />
        <br /><br />
        <span style="font-size: medium">Total Count:
        </span>
        <br />
        <asp:TextBox ID="txbTotalCount" runat="server" Height="20px" Width="300px"></asp:TextBox>
        <br />
        <span style="font-size: medium">Rejected Count: </span>
        <br />
        <asp:TextBox ID="txbRejectedCount" runat="server" Height="20px" Width="300px"></asp:TextBox>
        <br /><br />
        <asp:Button ID="Button2" runat="server" ForeColor="Black" Height="52px" Text="Submit" Width="131px" OnClick="Button2_Click" />
        <br />
        </div>

        <div class ="InputDataWorkerDiv">
        <h3 style="font-size: x-large">Worker Info</h3>
        <p class ="InputDataWorker1">
        <span style="font-size: medium">Select Worker:
        </span>
        <br />
        <span style="font-size: medium">
        <asp:DropDownList ID="ddlWorkerID" runat="server" Height="20px" Width="300px" DataSourceID="SqlDataSourceWorkerID" DataTextField="WorkerName" DataValueField="Worker_ID" AutoPostBack="True" OnSelectedIndexChanged="ddlWorkerID_SelectedIndexChanged" AppendDataBoundItems="True">
        <asp:ListItem>---Select Worker---</asp:ListItem>
        </asp:DropDownList>
        </span>
        <asp:SqlDataSource ID="SqlDataSourceWorkerID" runat="server" ConnectionString="<%$ ConnectionStrings:oeeConnectionString %>" SelectCommand="SELECT [Worker_ID], [WorkerName] FROM [Workers]"></asp:SqlDataSource>
        <br />
        <br />
        <span style="font-size: medium">Working Start Time:
        </span>
            <asp:Button ID="ButtonWorkerStartTime" runat="server" Height="25px" OnClick="ButtonWorkerStartTime_Click" Text="Use Current Time" Width="141px" CausesValidation="False" />
        <br />
        <asp:TextBox ID="txbWorkerStartingTime" runat="server" Height="20px" Width="300px"></asp:TextBox>
        <ajaxToolkit:CalendarExtender ID="txbWorkerStartingTime_CalendarExtender" runat="server" TargetControlID="txbWorkerStartingTime" Format="dd/MM/yyyy HH':'mm':'ss"/>
        <br />
        <span style="font-size: medium">Working End Time:</span>
            <asp:Button ID="ButtonWorkingEndTime" runat="server" Height="25px" OnClick="ButtonWorkingEndTime_Click" Text="Use Current Time" Width="146px" CausesValidation="False" />
        <br />
        <asp:TextBox ID="txbWorkerEndingTime" runat="server" Height="20px" Width="300px"></asp:TextBox>
        <ajaxToolkit:CalendarExtender ID="txbWorkerEndingTime_CalendarExtender" runat="server" TargetControlID="txbWorkerEndingTime" Format="dd/MM/yyyy HH':'mm':'ss"/>
        <br />
        <asp:CompareValidator ID="CompareValidatorWorkerTime" runat="server" ErrorMessage="Ending time must be after starting time" ControlToCompare="txbWorkerStartingTime" ControlToValidate="txbWorkerEndingTime" ForeColor="Red" Operator="GreaterThan"></asp:CompareValidator>
        <br />
        <span style="font-size: medium">Break Starting Time:</span>
            <asp:Button ID="ButtonBreakStartTime" runat="server" Height="25px" OnClick="ButtonBreakStartTime_Click" Text="Use Current Time" Width="136px" CausesValidation="False" />
        <br />
        <asp:TextBox ID="txbBreakStart" runat="server" Height="20px" Width="300px"></asp:TextBox>
        <ajaxToolkit:CalendarExtender ID="txbBreakStart_CalendarExtender" runat="server" TargetControlID="txbBreakStart" Format="dd/MM/yyyy HH':'mm':'ss"/>
        <br />
        <span style="font-size: medium">Break Ending Time:</span>
            <asp:Button ID="ButtonBreakEndTime" runat="server" Height="25px" OnClick="ButtonBreakEndTime_Click" Text="Use Current Time" Width="141px" CausesValidation="False" />
        <br />
        <asp:TextBox ID="txbBreakEnd" runat="server" Height="20px" Width="300px"></asp:TextBox>
        <ajaxToolkit:CalendarExtender ID="txbBreakEnd_CalendarExtender" runat="server" TargetControlID="txbBreakEnd" Format="dd/MM/yyyy HH':'mm':'ss"/>
        <br />
        <asp:CompareValidator ID="CompareValidatorBreakTime" runat="server" ErrorMessage="Ending Time must be after Starting Time" ControlToCompare="txbBreakStart" ControlToValidate="txbBreakEnd" ForeColor="Red" Operator="GreaterThan"></asp:CompareValidator>
        <br />
        <span style="font-size: medium">Break Type:
        </span>
        <br />
        <asp:DropDownList ID="ddlBreakType" runat="server" Height="20px" Width="300px" DataSourceID="SqlDataSourceBreakddl" DataTextField="BreakType" DataValueField="BreakID" AutoPostBack="True" AppendDataBoundItems="True">
        <asp:ListItem>---Select Break Type---</asp:ListItem>
        </asp:DropDownList>
        <asp:SqlDataSource ID="SqlDataSourceBreakddl" runat="server" ConnectionString="<%$ ConnectionStrings:oeeConnectionString %>" SelectCommand="SELECT [BreakType], [BreakID] FROM [BreakType]"></asp:SqlDataSource>
        <br />
        <br />

    </p>
    

    
    <asp:Button ID="Button1" runat="server" ForeColor="Black" Height="52px" Text="Submit" Width="131px" OnClick="Button1_Click" />
    </div>
  </ContentTemplate>
        <Triggers>
            <%--<asp:AsyncPostBackTrigger ControlID="ddlMachine_ID" EventName="SelectedIndexChanged" />--%>
            <asp:PostBackTrigger ControlID="Button2" />
            <asp:PostBackTrigger ControlID="Button1" />
        </Triggers>
  </asp:UpdatePanel>

    
</asp:Content>
