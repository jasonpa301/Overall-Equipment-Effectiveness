<%@ Page Title="Admin" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="oeeps.Login"%>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class ="adminP">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <p>Log in</p>
        <Span>User ID:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </Span>
        
            
        <asp:DropDownList ID="DropDownListUserNames" runat="server" Height="25px" Width="250px" DataSourceID="SqlDataSourceUserID" DataTextField="userID" DataValueField="Worker_ID" AppendDataBoundItems="True">
            <asp:ListItem>---Select User ID---</asp:ListItem>
              </asp:DropDownList>
        <asp:SqlDataSource ID="SqlDataSourceUserID" runat="server" ConnectionString="<%$ ConnectionStrings:oeeConnectionString %>" SelectCommand="SELECT [Worker_ID], [userID] FROM [Workers] ORDER BY [userID]"></asp:SqlDataSource>
        <br />
        <span>Password:</span>&nbsp;&nbsp;
        <asp:TextBox ID="tbxPassword" runat="server" Height="25px" Width="250px" TextMode="Password"></asp:TextBox>  
        <br />
        <asp:Button ID="ButtonLogIn" runat="server" Text="Log In" Height="50px" OnClick="ButtonLogIn_Click" Width="119px" />
        <asp:Label ID="LabelLoginFail" runat="server" ForeColor="Red"></asp:Label>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ButtonLogIn" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    </div>

   
    
</asp:Content>
