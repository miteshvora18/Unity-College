using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using unitycollegeModel;

public partial class OtherPages_RegisterExtraCourseF : System.Web.UI.Page
{
    unitycollegeEntities1 ue = new unitycollegeEntities1();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //Check Valid Login..
            var currentUser = (string)Session["username"];

            var checkUser = (from u in ue.Users
                             where u.uvalid == true && u.Roles.rid == 3 && u.username == currentUser
                             select u).FirstOrDefault();
            if (checkUser == null)
                Response.Redirect("../Login.aspx?error=invalid");

            if (!IsPostBack)
            {

                ddlExtraCourse.Items.Clear();
                ddlExtraCourse.Items.Add("--Select--");

                txtDuration.Text = txtStartDate.Text = "";

                string username = (string)Session["username"];
                var user = (from u in ue.Users
                            where u.username == username
                            select u).FirstOrDefault();

                var extraCoursesNew = (from ec in ue.ExtraCourses
                                       where ec.ecvalid == true
                                       select ec).ToList();

                if (user != null)
                {
                    foreach (var data in extraCoursesNew)
                        ddlExtraCourse.Items.Add(data.ecname);
                }
            }
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:" + e1.Message;
        }
    }

    protected void ddlECourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txtDuration.Text = txtStartDate.Text = "";

            string username = (string)Session["username"];
            var user = (from u in ue.Users
                        where u.username == username
                        select u).FirstOrDefault();

            txtDuration.Text = "";
            if (ddlExtraCourse.SelectedIndex != 0)
            {
                var selectedCourse = (from ec in ue.ExtraCourses
                                      where ec.ecname == ddlExtraCourse.Text
                                      select ec).FirstOrDefault();

                if (selectedCourse != null)
                {
                    txtDuration.Text = selectedCourse.ecduration.ToString();
                }
            }
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:" + e1.Message;
        }
    }
    /// <summary>
    /// Registration for selected extra course
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string username = (string)Session["username"];
            var user = (from u in ue.Users
                        where u.username == username
                        select u).FirstOrDefault();

            var extraCourse = (from ec in ue.ExtraCourses
                               where ec.ecname == ddlExtraCourse.Text
                               select ec).FirstOrDefault();

            var courseExists = (from rec in ue.ExtraCourseRegister
                                join ec in ue.ExtraCourses
                                on rec.ExtraCourses.ecid equals ec.ecid
                                join u in ue.Users
                                on rec.Users.uid equals u.uid
                                where u.username == username && ec.ecname == ddlExtraCourse.Text
                                select rec).FirstOrDefault();

            if (user != null)
            {
                if (ddlExtraCourse.SelectedIndex != 0)
                {
                    if (courseExists == null)
                    {
                        ExtraCourseRegister newExtraCourseRegistration = new ExtraCourseRegister();
                        newExtraCourseRegistration.ExtraCoursesReference.EntityKey = new System.Data.EntityKey("unitycollegeEntities1.ExtraCourses", "ecid", extraCourse.ecid);
                        newExtraCourseRegistration.UsersReference.EntityKey = new System.Data.EntityKey("unitycollegeEntities1.Users", "uid", user.uid);
                        newExtraCourseRegistration.recdate = DateTime.Now.Date;
                        newExtraCourseRegistration.recstartdate = Convert.ToDateTime(txtStartDate.Text);
                        newExtraCourseRegistration.recenddate = Convert.ToDateTime(txtStartDate.Text).AddMonths(Convert.ToInt32(txtDuration.Text));
                        ue.AddToExtraCourseRegister(newExtraCourseRegistration);
                        ue.SaveChanges();

                        lblMsg.Text = "Success!!!You are registered for course!";

                        ddlExtraCourse.Items.Clear();
                        ddlExtraCourse.Items.Add("--Select--");
                        txtDuration.Text = txtStartDate.Text = "";
                    }
                    else
                        lblMsg.Text = "You are already registered for this course!";
                }
                else
                    lblMsg.Text = "No Course selected!";
            }
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:" + e1.Message;
        }
    }
}
