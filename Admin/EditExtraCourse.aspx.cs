using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using unitycollegeModel;

public partial class OtherPages_EditExtraCourse : System.Web.UI.Page
{
    unitycollegeEntities1 ue = new unitycollegeEntities1();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            var currentUser = (string)Session["username"];

            var checkUser = (from u in ue.Users
                             where u.uvalid == true && u.Roles.rid == 1 && u.username == currentUser
                             select u).FirstOrDefault();
            if (checkUser == null)
                Response.Redirect("../Login.aspx?error=invalid");

            if (!IsPostBack)
            {
                ddlCourse.Items.Clear();
                ddlCourse.Items.Add("--Select--");
                var extraCourse = (from ec in ue.ExtraCourses
                                   select ec).ToList();
                foreach (var data in extraCourse)
                    ddlCourse.Items.Add(data.ecname);

                ddlDuration.Items.Clear();
                ddlDuration.Items.Add("--Select--");
            }
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:" + e1.Message;
        }
    }
    /// <summary>
    /// Populates list of course for selected course
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlDuration.Items.Clear();
            ddlDuration.Items.Add("--Select--");

            var extraCourse = (from ec in ue.ExtraCourses
                               where ec.ecname == ddlCourse.Text
                               select ec).FirstOrDefault();

            if (extraCourse != null)
            {
                for (int i = 1; i < 13; i++)
                    ddlDuration.Items.Add(i.ToString());

                ddlDuration.Text = extraCourse.ecduration.ToString();
                if (extraCourse.ecbenefits != null)
                    txtBenefits.Text = extraCourse.ecbenefits;
                txtDesc.Text = extraCourse.ecdescription;
                txtSeats.Text = extraCourse.ecseats.ToString();
                if (extraCourse.ecvalid == true)
                    ddlValid.SelectedIndex = 0;
                else
                    ddlValid.SelectedIndex = 1;
            }
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:" + e1.Message;
        }
    }
    /// <summary>
    /// New record for extra course is saved..
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            var course = (from ec in ue.ExtraCourses
                          where ec.ecname == ddlCourse.Text
                          select ec).FirstOrDefault();

            if (ddlCourse.SelectedIndex != 0)
            {
                if (ddlDuration.SelectedIndex != 0)
                {
                    course.ecduration = Convert.ToInt32(ddlDuration.Text);
                    course.ecdescription = txtDesc.Text;
                    course.ecbenefits = txtBenefits.Text;
                    if (txtSeats.Text != "")
                        course.ecseats = Convert.ToInt32(txtSeats.Text);
                    if (ddlValid.SelectedIndex == 0)
                        course.ecvalid = true;
                    else
                        course.ecvalid = false;
                    ue.SaveChanges();

                    lblMsg.Text = "Success!!!Record Updated!";

                    txtSeats.Text = txtDesc.Text = txtBenefits.Text = "";
                    ddlCourse.SelectedIndex = ddlDuration.SelectedIndex = ddlValid.SelectedIndex = 0;
                }
                else
                    lblMsg.Text = "Duration is required!";
            }
            else
                lblMsg.Text = "No course selected!";
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:" + e1.Message;
        }

    }
}
