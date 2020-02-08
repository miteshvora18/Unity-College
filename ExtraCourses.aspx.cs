using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using unitycollegeModel;

public partial class ExtraCourses : System.Web.UI.Page
{
    unitycollegeEntities1 ue = new unitycollegeEntities1();
    /// <summary>
    /// Gets list of Extra Course available on Load..
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            gvExtraCourse.DataSource = "";
            gvExtraCourse.DataBind();

            if (!IsPostBack)
            {
                var getExtraCourses = (from ec in ue.ExtraCourses
                                       where ec.ecvalid == true
                                       select new { Name = ec.ecname, Duration = ec.ecduration, Description = ec.ecdescription, Benefits = ec.ecbenefits }).ToList();

                //foreach (var data in getexcourse)
                //{
                //    if (data.ecbenefits == null || data.ecbenefits=="")
                //    {
                //        lblExCourse.Text += "<div class='eventheader'><b>" + data.ecname + "</b></div><div style='font-size:large;'><br/>Duration:" + data.ecduration + " months<br/>Description:" + data.ecdescription + "</div><br/>";
                //    }
                //    else
                //    lblExCourse.Text += "<div class='eventheader'><b>" + data.ecname + "</b></div><div style='font-size:large;'><br/>Duration:" + data.ecduration + " months<br/>Description:" + data.ecdescription + "<br/>Benefits:" + data.ecbenefits + "</div><br/>";      
                //}

                //Loads Extra Courses in grid view..
                if (getExtraCourses.Count != 0)
                {
                    gvExtraCourse.DataSource = getExtraCourses;
                    gvExtraCourse.DataBind();
                }
            }
        }
        catch (Exception e1)
        {
            lblMsg.Text = "Error:" + e1.Message;
        }
    }
}
