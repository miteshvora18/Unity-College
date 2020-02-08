using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using unitycollegeModel;

public partial class OtherPages_RenewExtraCourseS : System.Web.UI.Page
{
    unitycollegeEntities1 ue = new unitycollegeEntities1();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //Check Valid Login..
            var currentUser = (string)Session["username"];

            var checkUser = (from u in ue.Users
                             where u.uvalid == true && u.Roles.rid == 2 && u.username == currentUser
                             select u).FirstOrDefault();
            if (checkUser == null)
                Response.Redirect("../Login.aspx?error=invalid");
            //Lists expired extra courses in the drop down list of courses..
            if (!IsPostBack)
            {
                ddlExtraCourse.Items.Clear();
                ddlExtraCourse.Items.Add("--Select--");

                txtDuration.Text = txtStartDate.Text = "";

                string username = (string)Session["username"];
                var user = (from u in ue.Users
                            where u.username == username
                            select u).FirstOrDefault();

                var currentDate = DateTime.Now.Date;
                var extraCoursesNew = (from ec in ue.ExtraCourses
                                       join rec in ue.ExtraCourseRegister
                                       on ec.ecid equals rec.ExtraCourses.ecid
                                       join u in ue.Users
                                       on rec.Users.uid equals u.uid
                                       where u.username == username && rec.recenddate < currentDate
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
                                      join rec in ue.ExtraCourseRegister
                                      on ec.ecid equals rec.ExtraCourses.ecid
                                      where ec.ecname == ddlExtraCourse.Text
                                      select new { ec, rec }).FirstOrDefault();

                if (selectedCourse != null)
                {
                    txtDuration.Text = selectedCourse.ec.ecduration.ToString();
                    txtStartDate.Text = selectedCourse.rec.recstartdate.Date.ToString("MM/dd/yyyy");
                }
            }
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:" + e1.Message;
        }
    }
    /// <summary>
    /// Renews extra course..
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string username = (string)Session["username"];
            var selectedCourse = (from rec in ue.ExtraCourseRegister
                                  join u in ue.Users
                                  on rec.Users.uid equals u.uid
                                  join ec in ue.ExtraCourses
                                  on rec.ExtraCourses.ecid equals ec.ecid
                                  where ec.ecname == ddlExtraCourse.Text && u.username == username
                                  select rec).FirstOrDefault();
            if (selectedCourse != null)
            {
                if (Convert.ToDateTime(txtStartDate.Text).Date > selectedCourse.recenddate)
                {
                    selectedCourse.recdate = DateTime.Now.Date;
                    selectedCourse.recstartdate = Convert.ToDateTime(txtStartDate.Text);
                    selectedCourse.recenddate = Convert.ToDateTime(txtStartDate.Text).AddMonths(Convert.ToInt32(txtDuration.Text));
                    ue.SaveChanges();

                    lblMsg.Text = "Success!!!Details Updated!";
                }
                else
                    lblMsg.Text = "Please select date greater than " + selectedCourse.recenddate.ToString("MMM. dd yyyy") + ", as you are already registered till then!";
            }
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:" + e1.Message;
        }
    }
}
