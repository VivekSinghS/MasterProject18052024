using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
namespace MasterProject18052024
{
    public partial class UserForm : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connection1"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCountry();
                BindUserGrid();
            }
        }


        public void Clear()
        {
            txtName.Text = string.Empty;
            ddlCountry.SelectedValue = "0";
            ddlState.SelectedValue = "0";
            ddlCity.SelectedValue = "0";
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (btnSubmit.Text == "Submit")
            {
                string ImgName = Path.GetFileName(ImgFile.PostedFile.FileName);
                ImgFile.SaveAs(Server.MapPath("MyImage" +"\\"+ ImgName));
                con.Open();
                SqlCommand cmd = new SqlCommand("spUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Action","Insert");
                cmd.Parameters.AddWithValue("@uName",txtName.Text);
                cmd.Parameters.AddWithValue("@uCountry",ddlCountry.SelectedValue);
                cmd.Parameters.AddWithValue("@uState",ddlState.SelectedValue);
                cmd.Parameters.AddWithValue("@uCity", ddlCity.SelectedValue);
                cmd.Parameters.AddWithValue("@uImage",ImgName);
                cmd.ExecuteNonQuery();
                con.Close();
                BindUserGrid();
                Clear();
            } 
              else if(btnSubmit.Text == "Update")
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("spUser", con);
                cmd.CommandType= CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action","Update");
                cmd.Parameters.AddWithValue("@uId", ViewState["Idd"]);
                cmd.Parameters.AddWithValue("@uName",txtName.Text);
                cmd.Parameters.AddWithValue("@uCountry",ddlCountry.SelectedValue);
                cmd.Parameters.AddWithValue("@uState",ddlState.SelectedValue);
                cmd.Parameters.AddWithValue("@uCity",ddlCity.SelectedValue);
                //cmd.Parameters.AddWithValue("@uImage",ImgFile);
                cmd.ExecuteNonQuery ();
                con.Close() ;
                btnSubmit.Text = "Submit";
                Clear();
                BindUserGrid();
            }

        }
            public void BindCountry()
            {
             con.Open() ;
             SqlCommand cmd = new SqlCommand("Select * from tblCountry",con);
             SqlDataAdapter adp = new SqlDataAdapter(cmd);
             DataTable dt = new DataTable();
             adp.Fill(dt);
             ddlCountry.DataValueField = "CountryId";
             ddlCountry.DataTextField = "CountryName";
             ddlCountry.DataSource = dt;
             ddlCountry.DataBind();
             ddlCountry.Items.Insert(0, new ListItem("--Select--", "0"));
            }


            public void BindState()
            {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from tblState where CountryId = '"+ddlCountry.SelectedValue+"'",con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            ddlState.DataTextField = "StateName";
            ddlState.DataValueField = "StId";
            ddlState.DataSource = dt;
            ddlState.DataBind();
            ddlState.Items.Insert(0, new ListItem("--Select--", "0"));

        } 

        public void BindCity()
        {
            
            SqlCommand cmd = new SqlCommand("Select * from tblCity where StId = '" + ddlState.SelectedValue + "' ", con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);
           
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

        public void BindUserGrid()
        {
           
            SqlCommand cmd = new SqlCommand("spUser", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "Select");
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);
           
            grdUser.DataSource = dt;
            grdUser.DataBind();

        }


        protected void grdUser_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Dlt")
            {
                con.Open() ;
                SqlCommand cmd = new SqlCommand("spUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "Delete");
                cmd.Parameters.AddWithValue("@uId",e.CommandArgument);
                cmd.ExecuteNonQuery();
                con.Close() ;
                BindUserGrid();
            }
              else if (e.CommandName == "Edt")
            {
                con.Open() ;
                SqlCommand cmd = new SqlCommand("spUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "Edit");
                cmd.Parameters.AddWithValue("@uId", e.CommandArgument);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                con.Close();
                txtName.Text = dt.Rows[0]["uName"].ToString();
                ddlCountry.SelectedValue = dt.Rows[0]["uCountry"].ToString();
                BindState();
                ddlState.SelectedValue = dt.Rows[0]["uState"].ToString() ;
                BindCity();
                ddlCity.SelectedValue = dt.Rows[0]["uCity"].ToString();
                btnSubmit.Text = "Update";
                ViewState["Idd"] = e.CommandArgument;
                
                BindUserGrid();
            }
        }
    }
}