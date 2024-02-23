using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DemoUserManagement.Model;
using DemoUserManagement.BussinessLogicLayer;
using System.Drawing;
using System.Web.Services.Description;
using static System.Net.Mime.MediaTypeNames;
using DemoUserManagement.Utils;

namespace DemoUserManagement
{
    public partial class UserList : BasePage
    {
        public int test = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] != null)
            {
                    if (!IsPostBack)
                    {
                        ViewState["SortExpression"] = "UserId";
                        ViewState["SortDirection"] = "ASC";
                        BindGridView();
                    }
                }
            }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Response.Redirect("UserDetails2.aspx?id=" + GridView1.DataKeys[e.NewEditIndex].Values["UserId"]);
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            BindGridView(); 
        }

        protected void SortingGridView(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;
            string sortDirection = ViewState["SortDirection"].ToString();
            ViewState["SortExpression"] = sortExpression;
            ViewState["SortDirection"] = sortDirection == "ASC" ? "DESC" : "ASC";

            BindGridView();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindGridView();
        }

        private void BindGridView()
        {
            int currentPageIndex = GridView1.PageIndex;
            int pageSize = GridView1.PageSize;
            string sortExpression = ViewState["SortExpression"].ToString();
            string sortDirection = ViewState["SortDirection"].ToString();
            int totalCount = GetTotalCount();

            GridView1.VirtualItemCount = totalCount;

            GridView1.DataSource = UserDetailsService.Allusers(sortExpression, sortDirection, currentPageIndex * pageSize, pageSize);
            GridView1.DataBind();
        }

        private int GetTotalCount()
        {
            int totalCount = 0;
            totalCount = UserDetailsService.Lenusers();
            return totalCount;

        }
    }
}