<%@ Page Title="Admin" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="oeeps.Admin"%>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanelAdmin" runat="server">
        <ContentTemplate>
    <br /><br /><br />
    <h2 class ="pageTitle">&nbsp;Administration</h2>
    <div class ="wrapper1">
    <div class = "MachineInfoManager">
    <h3 style="font-size: x-large">Machine Info
    </h3>
    <p class ="InputDataWorker1"> 
        <asp:Label ID="LabelErrorManager" runat="server" Font-Size="Medium" ForeColor="Red"></asp:Label>
        <br />
        <span style="font-size: medium">Machine Name:</span>
        <br />
        <asp:TextBox ID="txbMachineNameInput" runat="server" Height="20px" Width="300px"></asp:TextBox>
        <br />
       
        <asp:Button ID="btnAddDescription" runat="server" Text="Add" Height="28px" Width="69px" OnClick="Button1_Click" />
        <br />
        <span style="font-size: medium">Select Machine to update/delete:</span>
        <br />
        <asp:DropDownList ID="DropDownListMachine1" runat="server" DataSourceID="SqlDataSourceMachine1" DataTextField="MachineName" DataValueField="Machine_ID" Height="22px" Width="200px" AppendDataBoundItems="true">
            <asp:ListItem>---Select Machine---</asp:ListItem>
        </asp:DropDownList>
        <asp:SqlDataSource ID="SqlDataSourceMachine1" runat="server" ConnectionString="<%$ ConnectionStrings:oeeConnectionString %>" SelectCommand="SELECT [Machine_ID], [MachineName] FROM [MachineInfo]"></asp:SqlDataSource>
        <br />
        <asp:Button ID="ButtonUpdateMachine" runat="server" Text="Update" OnClick="ButtonUpdateMachine_Click" />
        <asp:Button ID="ButtonDeleteMachine" runat="server" Text="Delete" OnClick="ButtonDeleteMachine_Click" />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </p>
    <br />
    <%--<p class ="InputDataWorker1">--%> 
        
        <h4>Downtime Causes</h4>
       
        <span style="font-size: medium">Possible Cause Description for Machine:</span>
        <br />
        <asp:TextBox ID="txbCauseDescription" runat="server" Height="20px" Width="300px"></asp:TextBox>
        <br />
        <asp:Button ID="Button5" runat="server" Text="Add" OnClick="Button5_Click" Height="28px" Width="69px" />
        <br />
        <span style="font-size: medium">Select Cause to Edit/Delete:</span><br />
        <asp:DropDownList ID="DropDownListMachineFault" runat="server" DataSourceID="SqlDataSourceCauseList" DataTextField="CauseDescription" DataValueField="CauseID" Height="21px" Width="200px" AppendDataBoundItems="True">
            <asp:ListItem>---Select Cause---</asp:ListItem>
        </asp:DropDownList>
        <asp:SqlDataSource ID="SqlDataSourceCauseList" runat="server" ConnectionString="<%$ ConnectionStrings:oeeConnectionString %>" SelectCommand="SELECT [CauseID], [CauseDescription] FROM [CauseDescription]"></asp:SqlDataSource>
        <br />
            <asp:Button ID="ButtonUpdateCause" runat="server" Text="Update" OnClick="ButtonUpdateCause_Click" />
            <asp:Button ID="ButtonDeleteCause" runat="server" Text="Delete" OnClick="ButtonDeleteCause_Click" />
        
        
        <br />
    
        <br />
         <br /><h4>Products</h4>
        <span style="font-size: medium">Product Name:</span><br />
        <asp:TextBox ID="TxbProductNames" runat="server" Width="300px"></asp:TextBox><br />
        <asp:Button ID="ButtonAddProduct" runat="server" Text="Add" OnClick="ButtonAddProduct_Click" />
        <br />
        <span style="font-size: medium">Select Product to Update/Delete:</span>
        <br />
        <asp:DropDownList ID="DropDownListProducts" runat="server" DataSourceID="SqlDataSourceProducts" DataTextField="ProductName" DataValueField="Product_ID" Width="200px" AppendDataBoundItems="True" Height="20px">
            <asp:ListItem>---Select Products---</asp:ListItem>
        </asp:DropDownList>
        <asp:SqlDataSource ID="SqlDataSourceProducts" runat="server" ConnectionString="<%$ ConnectionStrings:oeeConnectionString %>" SelectCommand="SELECT [Product_ID], [ProductName] FROM [Products]"></asp:SqlDataSource>
        <br />
        <asp:Button ID="ButtonUpdateProduct" runat="server" Text="Update" OnClick="ButtonUpdateProduct_Click" />
        <asp:Button ID="ButtonDeleteProducts" runat="server" Text="Delete" OnClick="ButtonDeleteProducts_Click" />
        <br /> <br />
        <h4>Assign Products to Machine </h4>
        <span style= "font-size: medium">Machine: </span>
        &nbsp;<asp:DropDownList ID="DropDownListMachines2" runat="server" DataSourceID="SqlDataSourceMachine1" DataTextField="MachineName" DataValueField="Machine_ID" AutoPostBack="True" OnSelectedIndexChanged="DropDownListMachines2_SelectedIndexChanged" AppendDataBoundItems="True" Height="20px" Width="200px">
        <asp:ListItem>---Select Machine---</asp:ListItem>
        </asp:DropDownList><br />
        <span style= "font-size: medium">Product:&nbsp;&nbsp; </span>
        <asp:DropDownList ID="DropDownListProducts2" runat="server" DataSourceID="SqlDataSourceProducts" DataTextField="ProductName" DataValueField="Product_ID" AutoPostBack="True" OnSelectedIndexChanged="DropDownListProducts2_SelectedIndexChanged" AppendDataBoundItems="True" Height="20px" Width="200px">
        <asp:ListItem>---Select Product---</asp:ListItem>
        </asp:DropDownList><br />
        <span style= "font-size: medium">Ideal Cycle Time:</span><br />
        <asp:TextBox ID="txbIdealCycleTime" runat="server" Height="20px" Width="300px"></asp:TextBox>
        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txbIdealCycleTime" ErrorMessage="Must be a number" ForeColor="Red" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
        <br />
        <asp:Button ID="ButtonAssignProduct" runat="server" Text="Assign" OnClick="ButtonAssignProduct_Click" />
        <asp:Button ID="ButtonUnassignProduct" runat="server" Text="Unassign" OnClick="ButtonUnassignProduct_Click" />
        <asp:Button ID="ButtonUpdateIct" runat="server" Text="Update" OnClick="ButtonUpdateIct_Click" />
    </div>
    <div class ="WorkerInfoManager">
    <h3 style="font-size: x-large">Worker Info</h3>
    <%--<p class ="InputDataWorker1">--%>
        <asp:Label ID="LabelErrorWorker" runat="server" Font-Size="Medium" ForeColor="Red"></asp:Label>
        <br />
       
        <span style="font-size: medium">Worker Name:
        </span>
        <br />
        <asp:TextBox ID="txbWorkerName" runat="server" Height="20px" Width="300px"></asp:TextBox>
        <br />
        <span style="font-size: medium">Shift Length:</span>
        <br />
        <asp:TextBox ID="txbShiftLength" runat="server" Height="20px" Width="300px"></asp:TextBox>
        <br />
        <span style="font-size: medium">Break Hours:
        </span>
        <br />
        <asp:TextBox ID="txbBreakHours" runat="server" Height="20px" Width="300px"></asp:TextBox>
        <br />
        <span style="font-size: medium">User ID:
        </span>
        <br />
        <asp:TextBox ID="txbUserId" runat="server" Height="20px" Width="300px"></asp:TextBox>
        <br />
        <span style="font-size: medium">Password:
        </span>
        <br />
        <asp:TextBox ID="txbPassword" runat="server" Height="20px" Width="300px"></asp:TextBox>
        <br />
        <span style="font-size: medium">Access Level:
        </span>
        <br />
        <asp:DropDownList ID="DropDownListAccessLevel" runat="server" DataSourceID="SqlDataSourceAccessLevel" DataTextField="AccessLevel" DataValueField="Id" Height="20px" Width="200px"></asp:DropDownList>
        <asp:SqlDataSource ID="SqlDataSourceAccessLevel" runat="server" ConnectionString="<%$ ConnectionStrings:oeeConnectionString %>" SelectCommand="SELECT [Id], [AccessLevel] FROM [AccessLevel]"></asp:SqlDataSource>
        <br />
        <asp:Button ID="Button2" runat="server" Text="Add" Height="28px" Width="69px" OnClick="Button2_Click" />
        <br />
        <span style="font-size: medium">Select Worker to Update/delete:
        </span>
        <br />
        <asp:DropDownList ID="DropDownWorkerList" runat="server" DataSourceID="SqlDataWorkers1" DataTextField="WorkerName" DataValueField="Worker_ID" Height="20px" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="DropDownWorkerList_SelectedIndexChanged" AppendDataBoundItems="True">
        <asp:ListItem>---Select Worker---</asp:ListItem>
        </asp:DropDownList>
        <asp:SqlDataSource ID="SqlDataWorkers1" runat="server" ConnectionString="<%$ ConnectionStrings:oeeConnectionString %>" SelectCommand="SELECT [Worker_ID], [WorkerName] FROM [Workers]"></asp:SqlDataSource>
        <br />
        <asp:Button ID="ButtonUpdateWorker" runat="server" Text="Update" OnClick="ButtonUpdateWorker_Click" />
        <asp:Button ID="ButtonDeleteWorker" runat="server" Text="Delete" OnClick="ButtonDeleteWorker_Click" />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <h4>Breaks</h4>
        <span style="font-size: medium">Break Type:</span>
        <br />
        <asp:TextBox ID="txbBreakType" runat="server" Height="20px" Width="300px"></asp:TextBox>
        <br />
        <asp:Button ID="btnAddBreak" runat="server" Text="Add" OnClick="btnAddBreak_Click" Height="28px" Width="69px" />
        <br />
        <span style="font-size: medium">Select Break Type to Edit/Delete:</span>
        <br />
        <asp:DropDownList ID="DropDownListBreakType" runat="server" DataSourceID="SqlDataSourceBreakType" DataTextField="BreakType" DataValueField="BreakID" Width="236px" OnSelectedIndexChanged="DropDownListBreakType_SelectedIndexChanged" AppendDataBoundItems="True">
            <asp:ListItem>---Select Break---</asp:ListItem>
        </asp:DropDownList>
        <asp:SqlDataSource ID="SqlDataSourceBreakType" runat="server" ConnectionString="<%$ ConnectionStrings:oeeConnectionString %>" SelectCommand="SELECT [BreakID], [BreakType] FROM [BreakType]"></asp:SqlDataSource>
        <br />
        <asp:Button ID="ButtonUpdateBreak" runat="server" Text="Update" OnClick="ButtonUpdateBreak_Click" />
        <asp:Button ID="ButtonDeleteBreak" runat="server" Text="Delete" OnClick="ButtonDeleteBreak_Click" />
        <br />
        <br />
        <%--</p>--%>
    </div>
    </div>
    </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnAddDescription" />
            <asp:PostBackTrigger ControlID="ButtonUpdateMachine" />
            <asp:PostBackTrigger ControlID="ButtonDeleteMachine" />
            <asp:PostBackTrigger ControlID="Button5" />
            <asp:PostBackTrigger ControlID="ButtonUpdateCause" />
            <asp:PostBackTrigger ControlID="ButtonAddProduct" />
            <asp:PostBackTrigger ControlID="ButtonUpdateProduct" />
            <asp:PostBackTrigger ControlID="ButtonAssignProduct" />
            <asp:PostBackTrigger ControlID="ButtonUnassignProduct" />
            <asp:PostBackTrigger ControlID="ButtonUpdateIct" />
            <asp:PostBackTrigger ControlID="Button2" />
            <asp:PostBackTrigger ControlID="ButtonUpdateWorker" />
            <asp:PostBackTrigger ControlID="btnAddBreak" />
            <asp:PostBackTrigger ControlID="ButtonUpdateBreak" />

        </Triggers>
       
   </asp:UpdatePanel>
    
</asp:Content>
