using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace WebForm_upload
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) //第一次載入頁面pageIndex=1,判別是否有切換頁面過(viewIndex)
            {
                //pageIndex:現在讀取頁數,pageNum:總共資料頁數,viewIndex:儲存跳頁時的頁數,queryString:條件搜尋式

                if (Session["viewIndex"] == null)
                {
                    Session["pageIndex"] = 1;
                    BtnFirstPage.Enabled = false;
                    BtnPPage.Enabled = false;
                    Session["queryString"] = "SELECT EmployeeID,PhotoPath,EmployeeName,Title,BirthDate,Address,Salary FROM Employees ORDER BY EmployeeID ASC OFFSET @OFFSET ROWS FETCH FIRST 5 ROWS ONLY";
                    string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ToString();
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        string queryString = "SELECT COUNT (EmployeeID) FROM Employees";
                        SqlCommand command = new SqlCommand(queryString, connection);
                        connection.Open();
                        int numrows = (int)command.ExecuteScalar();
                        int pagenum = numrows / 5;
                        if (numrows % 5 != 0)
                        {
                            pagenum++;
                        }
                        Session["pageNum"] = pagenum;
                    }
                    GetData();
                }
                else
                {
                    //以viewIndex回復切換前的頁數
                    Session["pageIndex"] = (int)Session["viewIndex"];

                    //處理各種返回情況下按鈕是否enable
                    if ((int)Session["pageIndex"] == 1 && (int)Session["pageNum"] != 1)
                    {
                        BtnFirstPage.Enabled = false;
                        BtnPPage.Enabled = false;
                    }
                    else if ((int)Session["pageIndex"] == (int)Session["pageNum"] && (int)Session["pageNum"] != 1)
                    {
                        BtnLastPage.Enabled = false;
                        BtnNPage.Enabled = false;
                    }
                    else if ((int)Session["pageNum"] == 1)
                    {
                        BtnFirstPage.Enabled = false;
                        BtnPPage.Enabled = false;
                        BtnLastPage.Enabled = false;
                        BtnNPage.Enabled = false;
                    }
                    else if ((int)Session["pageIndex"] > 1 && (int)Session["pageIndex"] < (int)Session["pageNum"])
                    {
                        BtnFirstPage.Enabled = true;
                        BtnPPage.Enabled = true;
                        BtnLastPage.Enabled = true;
                        BtnNPage.Enabled = true;
                    }
                    GetData();
                }
            }
            else
            {
                //處理按鈕click的各種情況
                if (!string.IsNullOrEmpty(Request.Form["BtnFirstPage"]))
                {
                    Session["pageIndex"] = 1;
                    BtnFirstPage.Enabled = false;
                    BtnPPage.Enabled = false;
                    BtnNPage.Enabled = true;
                    BtnLastPage.Enabled = true;
                    GetData();
                }
                else if (!string.IsNullOrEmpty(Request.Form["BtnPPage"]))
                {
                    int page = (int)Session["pageIndex"];
                    page--;
                    Session["pageIndex"] = page;
                    if ((int)Session["pageIndex"] < (int)Session["pageNum"])
                    {
                        BtnNPage.Enabled = true;
                        BtnLastPage.Enabled = true;
                    }
                    if ((int)Session["pageIndex"] == 1)
                    {
                        BtnFirstPage.Enabled = false;
                        BtnPPage.Enabled = false;
                    }
                    GetData();
                }
                else if (!string.IsNullOrEmpty(Request.Form["BtnNPage"]))
                {
                    int page = (int)Session["pageIndex"];
                    page++;
                    Session["pageIndex"] = page;
                    if ((int)Session["pageIndex"] > 1)
                    {
                        BtnFirstPage.Enabled = true;
                        BtnPPage.Enabled = true;
                    }
                    if ((int)Session["pageIndex"] == (int)Session["pageNum"])
                    {
                        BtnNPage.Enabled = false;
                        BtnLastPage.Enabled = false;
                    }
                    GetData();
                }
                else if (!string.IsNullOrEmpty(Request.Form["BtnLastPage"]))
                {
                    Session["pageIndex"] = (int)Session["pageNum"];
                    BtnFirstPage.Enabled = true;
                    BtnPPage.Enabled = true;
                    BtnNPage.Enabled = false;
                    BtnLastPage.Enabled = false;
                    GetData();
                }

                if (!string.IsNullOrEmpty(Request.Form["BtnQuery"]))
                {
                    Session["pageIndex"] = 1;
                    BtnFirstPage.Enabled = false;
                    BtnPPage.Enabled = false;
                    Session["queryString"] = "SELECT EmployeeID,PhotoPath,EmployeeName,Title,BirthDate,Address,Salary FROM Employees WHERE 1=1";
                    String countString = "SELECT COUNT (EmployeeID) FROM Employees WHERE 1=1";

                    if (!string.IsNullOrEmpty(Request.Form["query_name"]))
                    {
                        Session["queryString"] += "AND EmployeeName LIKE '%'+@EmployeeName+'%'";
                        countString += "AND EmployeeName LIKE '%'+@EmployeeName+'%'";
                    }

                    if (!string.IsNullOrEmpty(Request.Form["query_title"]))
                    {
                        Session["queryString"] += "AND Title LIKE '%'+@Title+'%'";
                        countString += "AND Title LIKE '%'+@Title+'%'";
                    }

                    Session["queryString"] += "ORDER BY EmployeeID ASC OFFSET @OFFSET ROWS FETCH FIRST 5 ROWS ONLY";

                    string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ToString();
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlCommand command = new SqlCommand(countString, connection);
                        if (!string.IsNullOrEmpty(Request.Form["query_name"]))
                        {
                            command.Parameters.AddWithValue("@EmployeeName", (String)Request.Form["query_name"]);
                            Session["query_name"] = (String)Request.Form["query_name"];
                        }
                        if (!string.IsNullOrEmpty(Request.Form["query_title"]))
                        {
                            command.Parameters.AddWithValue("@Title", (String)Request.Form["query_title"]);
                            Session["query_title"] = (String)Request.Form["query_title"];
                        }
                        connection.Open();
                        int numrows = (int)command.ExecuteScalar();
                        int pagenum = numrows / 5;
                        if (numrows % 5 != 0)
                        {
                            pagenum++;
                        }
                        Session["pageNum"] = pagenum;
                        //若資料只有一頁則無法換頁
                        if (pagenum == 1)
                        {
                            BtnLastPage.Enabled = false;
                            BtnNPage.Enabled = false;
                        }
                        else
                        {
                            BtnLastPage.Enabled = true;
                            BtnNPage.Enabled = true;
                        }
                    }
                    GetData();
                }
            }
        }

        //按鈕觸發row command
        protected void GridView1_RowCommand(Object sender, GridViewCommandEventArgs e)
        {
            Button btn = (Button)e.CommandSource;
            GridViewRow rowNum = (GridViewRow)btn.NamingContainer;

            switch (e.CommandName)
            {
                case "editbyID":
                    //以rowcommand得到資料行數,再用行數得到該行資料的ID
                    Session["ID"] = rowNum.Cells[0].Text;

                    Session["actionType"] = "edit";
                    Session["viewIndex"] = (int)Session["pageIndex"];
                    Response.Redirect("Edit.aspx");
                    break;

                case "deletebyID":
                    //以rowcommand得到資料行數,再用行數得到該行資料的ID
                    Session["ID"] = Convert.ToInt32(rowNum.Cells[0].Text);

                    //刪除指定ID資料
                    string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ToString();
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        string queryString = "SELECT PhotoPath FROM Employees WHERE EmployeeID = @EmployeeID";
                        SqlCommand command = new SqlCommand(queryString, connection);
                        command.Parameters.AddWithValue("@EmployeeID", Convert.ToInt32(Session["ID"]));
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        string delPhotoPath = "";
                        while (reader.Read())
                        {
                            delPhotoPath = reader["PhotoPath"].ToString();
                        }
                        if (!string.IsNullOrEmpty(delPhotoPath))
                        {
                            File.Delete(delPhotoPath);
                        }
                    }

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        string queryString = "DELETE FROM Employees WHERE EmployeeID = @EmployeeID";
                        SqlCommand command = new SqlCommand(queryString, connection);
                        command.Parameters.AddWithValue("@EmployeeID", Convert.ToInt32(Session["ID"]));
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    GetData();
                    break;

                default:
                    break;
            }
        }

        //在data bind後隱藏ID欄位
        protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Visible = false;
            }
        }

        //新增按鈕click函式
        protected void New(object sender, EventArgs e)
        {
            Session["actionType"] = "new";
            Session["viewIndex"] = (int)Session["pageIndex"];
            Response.Redirect("Edit.aspx");
        }

        //將資料存進dataset後顯示在GridView1
        protected void GetData()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ToString();
            DataSet ds = new DataSet();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand((string)Session["queryString"], connection);
                if (!string.IsNullOrEmpty((String)Session["query_name"]))
                {
                    command.Parameters.AddWithValue("@EmployeeName", (String)Session["query_name"]);
                }
                if (!string.IsNullOrEmpty((String)Session["query_title"]))
                {
                    command.Parameters.AddWithValue("@Title", (String)Session["query_title"]);
                }
                command.Parameters.AddWithValue("@OFFSET", ((int)Session["pageIndex"] - 1) * 5);
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
        }
    }
}