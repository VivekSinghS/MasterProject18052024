using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MasterProject18052024
{
    public partial class EmployeeForm : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connection1"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGender();
                BindCountry();
                BindGridEmployee();
                
            }
        }

          

        public void Clear()
        {
            txtName.Text = string.Empty;
            rblGender.ClearSelection();
            ddlCountry.SelectedValue = "0";
            ddlState.SelectedValue = "0";
            ddlCity.SelectedValue = "0";
        }

          public void BindGender()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from tblGender", con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            con.Close();
            rblGender.DataValueField = "Id";
            rblGender.DataTextField = "gName";
            rblGender.DataSource = dt;
            rblGender.DataBind();

        }

        public void BindCountry()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from tblCountry", con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            con.Close();
            ddlCountry.DataValueField = "CountryId";
            ddlCountry.DataTextField = "CountryName";
            ddlCountry.DataSource = dt;
            ddlCountry.DataBind();
            ddlCountry.Items.Insert(0, new ListItem("--Select--", "0"));
        }


        public void BindGridEmployee()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("spSelectEmployee", con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);   
            DataTable dt = new DataTable();
            adp.Fill(dt);
            con.Close();
            grdEmployee.DataSource = dt;
            grdEmployee.DataBind();
            
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (btnSubmit.Text == "Submit")
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("spInsertEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@empName", txtName.Text);
                cmd.Parameters.AddWithValue("@empGender", rblGender.SelectedValue);
                cmd.Parameters.AddWithValue("@empCountry", ddlCountry.SelectedValue);
                cmd.Parameters.AddWithValue("@empState", ddlState.SelectedValue);
                cmd.Parameters.AddWithValue("@empCity", ddlCity.SelectedValue);
                cmd.ExecuteNonQuery();
                con.Close();
                Clear();
                BindGridEmployee();

            }
              else if(btnSubmit.Text == "Update")
              {

                con.Open();
                SqlCommand cmd = new SqlCommand("spUpdateEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@eId", ViewState["vs"]);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@empName", txtName.Text);
                cmd.Parameters.AddWithValue("@empGender", rblGender.SelectedValue);
                cmd.Parameters.AddWithValue("@empCountry", ddlCountry.SelectedValue);
                cmd.Parameters.AddWithValue("@empState", ddlState.SelectedValue);
                cmd.Parameters.AddWithValue("@empCity", ddlCity.SelectedValue);
                cmd.ExecuteNonQuery();
                con.Close();
                btnSubmit.Text = "Submit";
                Clear();
                BindGridEmployee();

            }
        }


        public void BindState()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from tblState where CountryId = '" + ddlCountry.SelectedValue + "'", con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            con.Close();
            ddlState.DataValueField = "StId";
            ddlState.DataTextField = "StateName";
            ddlState.DataSource = dt;
            ddlState.DataBind();
            ddlState.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        public void BindCity()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from tblCity where StId = '" + ddlState.SelectedValue + "' ", con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            con.Close();
            ddlCity.DataValueField = "CityId";
            ddlCity.DataTextField = "CityName";
            ddlCity.DataSource = dt;
            ddlCity.DataBind();
            ddlCity.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            BindState();
                
            
        }

        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {

            BindCity();  
            
        }

        protected void grdEmployee_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Dlt")
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("spDeleteEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EId", e.CommandArgument);
                cmd.ExecuteNonQuery();
                con.Close();
                BindGridEmployee();
            }
               else if(e.CommandName == "Edt")
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("spEditEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@eId", e.CommandArgument);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                con.Close();
                txtName.Text = dt.Rows[0]["EmpName"].ToString();
                rblGender.SelectedValue = dt.Rows[0]["EmpGender"].ToString() ;
                ddlCountry.SelectedValue = dt.Rows[0]["EmpCountry"].ToString();
                BindState();
                ddlState.SelectedValue = dt.Rows[0]["EmpState"].ToString();
                BindCity();
                ddlCity.SelectedValue = dt.Rows[0]["EmpCity"].ToString();
                btnSubmit.Text = "Update";
                ViewState["vs"] = e.CommandArgument;
                BindGridEmployee();


            }

        }
    }
}