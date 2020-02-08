using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using unitycollegeModel;

public partial class OtherPages_AddExtraCourse : System.Web.UI.Page
{
    unitycollegeEntities1 ue = new unitycollegeEntities1();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";

            //Checks valid login..
            var currentUser = (string)Session["username"];

            var checkUser = (from u in ue.Users
                             where u.uvalid == true && u.Roles.rid == 1 && u.username == currentUser
                             select u).FirstOrDefault();
            if (checkUser == null)
                Response.Redirect("../Login.aspx?error=invalid");

            if (!IsPostBack)
            {
                //Adding duration in month for extra course in drop down list..
                ddlDuration.Items.Clear();
                ddlDuration.Items.Add("--Select--");
                for (int i = 1; i < 13; i++)
                    ddlDuration.Items.Add(i.ToString());
            }
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:" + e1.Message;
        }
    }
    /// <summary>
    /// Adds Extra Course to the database on Submit
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            var courseExists = (from ec in ue.ExtraCourses
                                where ec.ecname == txtName.Text
                                select ec).FirstOrDefault();

            //Check if course already exists..
            if (courseExists == null)
            {
                if (ddlDuration.SelectedIndex != 0)
                {
                    unitycollegeModel.ExtraCourses newExtraCourse = new unitycollegeModel.ExtraCourses();
                    newExtraCourse.ecname = txtName.Text;
                    newExtraCourse.ecduration = Convert.ToInt32(ddlDuration.Text);
                    newExtraCourse.ecdescription = txtDesc.Text;
                    newExtraCourse.ecbenefits = txtBenefits.Text;
                    if (txtSeats.Text != "")
                        newExtraCourse.ecseats = Convert.ToInt32(txtSeats.Text);
                    if (ddlValid.SelectedIndex == 0)
                        newExtraCourse.ecvalid = true;
                    else
                        newExtraCourse.ecvalid = false;
                    ue.AddToExtraCourses(newExtraCourse);
                    ue.SaveChanges();
                    lblMsg.Text = "Success!!!Record Saved!";
                    txtSeats.Text = txtName.Text = txtDesc.Text = txtBenefits.Text = "";
                    ddlDuration.SelectedIndex = ddlValid.SelectedIndex = 0;
                }
                else
                    lblMsg.Text = "Course Duration is required!";
            }
            else
                lblMsg.Text = "Course already exists!";
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:"+e1.Message;
        }
    }
}
