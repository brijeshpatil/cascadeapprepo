using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
public partial class _Default : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=D:\Brijesh\22Sep\CascadingDropDown\App_Data\Database.mdf;Integrated Security=True;User Instance=True");
    SqlDataAdapter da;
    DataTable dt;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillCountries();
        }
    }

    private void FillCountries()
    {
        da = new SqlDataAdapter("select * from countrytbl", con);
        dt = new DataTable();
        da.Fill(dt);

        drpCountry.DataSource = dt;
        drpCountry.DataTextField = "countryname";
        drpCountry.DataValueField = "countryid";
        drpCountry.DataBind();
     
        ListItem li = new ListItem();
        li.Text = "Select Country";
        li.Value = "0";
        drpCountry.Items.Insert(0, li); FillStates();
    }

    private void FillStates()
    {
        da = new SqlDataAdapter("select stateid,statename from statetbl where fkcountryid=@CID", con);
        da.SelectCommand.Parameters.AddWithValue("@CID", drpCountry.SelectedItem.Value);
        dt = new DataTable();
        da.Fill(dt);
        drpState.DataSource = dt;
        drpState.DataTextField = "statename";
        drpState.DataValueField = "stateid";
        drpState.DataBind();
        ListItem li = new ListItem();
        li.Text = "Select State";
        li.Value = "0";
        drpState.Items.Insert(0, li);
        FIllCities();
    }

    private void FIllCities()
    {
        da = new SqlDataAdapter("select cityid,cityname from citytbl where fkstateid=@SID", con);
        da.SelectCommand.Parameters.AddWithValue("@SID", drpState.SelectedItem.Value);
        dt = new DataTable();
        da.Fill(dt);

        drpCity.DataSource = dt;
        drpCity.DataTextField = "cityname";
        drpCity.DataValueField = "cityid";
        drpCity.DataBind();
        ListItem li = new ListItem();
        li.Text = "Select City";
        li.Value = "0";
        drpCity.Items.Insert(0, li);
    }
    protected void drpCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillStates();   
    }
    protected void drpState_SelectedIndexChanged(object sender, EventArgs e)
    {
        FIllCities();
    }
}