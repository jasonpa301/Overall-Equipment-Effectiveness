using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace oeeps
{
    public partial class Login : Page
    {
        //private string strcon = WebConfigurationManager.ConnectionStrings["oeeConnectionString"].ConnectionString;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            DropDownListUserNames.DataSourceID = "SqlDataSourceUserID";
        }
        //private bool UserLogin (string un, string pw)
        //{
        //    SqlDbConnection con = new SqlDbConnection();  //creates SqlDbConnect class object to connect to DB
            
        //    con.SetCommand("Select userID from Workers where userID=@un and PasswordPass=@pw");
        //    con.com.Parameters.AddWithValue("@un", un);
        //    con.com.Parameters.AddWithValue("@pw", pw);

            
        //    string result = con.StringScalar();
        //    con.CloseCon(); 
            
        //    if (string.IsNullOrEmpty(result))       //if login successful string will be userID and return false else it will be null and return true           
        //    return false;
        //    else   
        //    return true;

        //}

        protected void ButtonLogIn_Click(object sender, EventArgs e)
        {
            if (DropDownListUserNames.SelectedIndex == 0)
            {
                LabelLoginFail.Text = "Login failed! You must select a user ID";
            }
            else
            {
                UserLogin login = new UserLogin(DropDownListUserNames.SelectedItem.Text, tbxPassword.Text);

                if (login.CheckLogin())
                {
                    Session["username"] = DropDownListUserNames.SelectedItem.Text;
                    Session["accessLevel"] = login.GetAccessLevel();
                    login.InsertLoginRecord();
                    login.CloseConnection();
                    Response.Redirect("/default");
                }
                else
                {
                    LabelLoginFail.Text = "Password did not match user ID. Login Failed!";
                    //tbxPassword.Text = "";
                    if (IsPostBack)
                    {
                        tbxPassword.Text = "";
                    }
                }

            }

            //string un = DropDownListUserNames.SelectedItem.ToString();
            //string pw = tbxPassword.Text;
            //bool result = UserLogin(un, pw);
            //if (result)
            //{
            //    //e.Authenticated = true;
            //    Session["username"] = un;
                
            //    //below code populates loginrecord table everytime user successfully logs in



            //    SqlDbConnection con = new SqlDbConnection();
            //    con.SetCommand("Select AccessLevel FROM Workers WHERE userID= @un");
            //    con.com.Parameters.AddWithValue("@un", un);
            //    int tempAccessLevel = con.IntScalar();
            //    Session["accessLevel"] = tempAccessLevel.ToString(); //creates session for access level
            //    con.SetCommand("Select Worker_ID from Workers where userID=@un and PasswordPass=@pw");
            //    con.com.Parameters.AddWithValue("@un", un); //uses public attribute com
            //    con.com.Parameters.AddWithValue("@pw", pw);
            //    int uid = con.IntScalar();
            //    string test = (string)(Session["accessLevel"]);
                



            //    con.SetCommand("INSERT INTO LoginRecord (Worker_ID,Login_Time) VALUES (@uid,@dt)");

            //    con.com.Parameters.AddWithValue("@uid", uid);
            //    con.com.Parameters.AddWithValue("@dt", DateTime.Now); //same format as sql
            //    con.NonQueryEx(); //non query must be executed to update DB

            //    //con.SetCommand("SELECT PlannedProductionTime FROM Workers WHERE userID=@un"); //gets planned production of worker that logged in
            //    //con.com.Parameters.AddWithValue("@un", un);
            //    //Session["PassValue"] = con.StringScalar(); //saves value to be used in session
            //    con.CloseCon(); //closes connection to DB
            //    Response.Redirect("/default");
            //}
            //else
            //{
            //    LabelLoginFail.Text = "Password did not match user ID. Login Failed!";
            //    //tbxPassword.Text = "";
            //    if (IsPostBack)
            //    {
            //        tbxPassword.Text = "";
            //    }
            //}
            
        }
    
        //protected void Login2_Authenticate(object sender, AuthenticateEventArgs e)
        //{
        //    string un = Login2.UserName;
        //    string pw = Login2.Password;
        //    bool result = UserLogin(un, pw);
        //    if (result)
        //    {
        //        e.Authenticated = true;
        //        Session["username"] = un;

        //        //below code populates loginrecord table everytime user successfully logs in



        //        SqlDbConnection con = new SqlDbConnection();
        //        con.SetCommand("Select Worker_ID from Workers where userID=@un and PasswordPass=@pw");
        //        con.com.Parameters.AddWithValue("@un", un); //uses public attribute com
        //        con.com.Parameters.AddWithValue("@pw", pw);





        //        int uid = con.IntScalar();

        //        con.SetCommand("Select COUNT(*) FROM LoginRecord");


        //        //int id = con.IntScalar();
        //        //id += 1; //sets primary key as amount of login records plus 1

        //        //con.SetCommand("INSERT INTO LoginRecord (Login_ID,Worker_ID,Login_Time) VALUES (@id,@uid,@dt)");
        //        con.SetCommand("INSERT INTO LoginRecord (Worker_ID,Login_Time) VALUES (@uid,@dt)");
        //        //con.com.Parameters.AddWithValue("@id", id);
        //        con.com.Parameters.AddWithValue("@uid", uid);
        //        con.com.Parameters.AddWithValue("@dt", DateTime.Now); //same format as sql
        //        con.NonQueryEx(); //non query must be executed to update DB

        //        //con.SetCommand("SELECT PlannedProductionTime FROM Workers WHERE userID=@un"); //gets planned production of worker that logged in
        //        //con.com.Parameters.AddWithValue("@un", un);
        //        //Session["PassValue"] = con.StringScalar(); //saves value to be used in session
        //        con.CloseCon(); //closes connection to DB
        //    }
        //    else e.Authenticated = false;
        //}


    }
}