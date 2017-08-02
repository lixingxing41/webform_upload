using System;
using System.Data.SqlClient;
using System.IO;

namespace WebForm_upload
{
    public partial class Edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) //第一次載入頁面
            {
                if ((String)Session["actionType"] == "edit") //編輯
                {
                    int updateID = Convert.ToInt32(Session["ID"]);
                    string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ToString();
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        string queryString = "SELECT EmployeeName,Title,TitleOfCourtesy,BirthDate,HireDate,Address,HomePhone,Extension,PhotoPath,Notes,ManagerID,Salary FROM Employees WHERE EmployeeID = @EmployeeID";
                        SqlCommand command = new SqlCommand(queryString, connection);
                        command.Parameters.AddWithValue("@EmployeeID", updateID);
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            EmployeeName.Text = (String)reader["EmployeeName"];
                            Title.Text = (String)reader["Title"];
                            TitleOfCourtesy.Text = (String)reader["TitleOfCourtesy"];
                            BirthDate.Text = reader["BirthDate"].ToString();
                            HireDate.Text = reader["HireDate"].ToString();
                            Address.Text = (String)reader["Address"];
                            HomePhone.Text = (String)reader["HomePhone"];
                            Extension.Text = (String)reader["Extension"];
                            PhotoPath.Text = (String)reader["PhotoPath"];
                            Notes.Text = (String)reader["Notes"];
                            ManagerID.Text = reader["ManagerID"].ToString();
                            Salary.Text = reader["Salary"].ToString();
                        }
                    }
                }
                else if ((String)Session["actionType"] == "new") //新增
                {
                    EmployeeName.Text = "";
                    Title.Text = "";
                    TitleOfCourtesy.Text = "";
                    BirthDate.Text = "";
                    HireDate.Text = "";
                    Address.Text = "";
                    HomePhone.Text = "";
                    Extension.Text = "";
                    PhotoPath.Text = "";
                    Notes.Text = "";
                    ManagerID.Text = "";
                    Salary.Text = "";
                }
            }
            else
            {
                EmployeeName.Text = Request.Form["EmployeeName"];
                Title.Text = Request.Form["Title"];
                TitleOfCourtesy.Text = Request.Form["TitleOfCourtesy"];
                BirthDate.Text = Request.Form["BirthDate"];
                HireDate.Text = Request.Form["HireDate"];
                Address.Text = Request.Form["Address"];
                HomePhone.Text = Request.Form["HomePhone"];
                Extension.Text = Request.Form["Extension"];
                PhotoPath.Text = Request.Form["PhotoPath"];
                Notes.Text = Request.Form["Notes"];
                ManagerID.Text = Request.Form["ManagerID"];
                Salary.Text = Request.Form["Salary"];
            }
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }

        protected void Save_Click(object sender, EventArgs e)
        {
            string photopath = PhotoPath.Text;
            string filename = Photo.FileName;
            Boolean fileOK = false;
            String path = Server.MapPath("~/UploadedImages/"); //路徑指定方法1 ex."C:\\Test.txt"
            String fileExtension = Path.GetExtension(filename).ToLower();
            if (Photo.HasFile)
            {
                //判斷是否為允許上傳的檔案類型
                String[] allowedExtensions = {".gif", ".png", ".jpeg", ".jpg"};
                for (int i = 0; i < allowedExtensions.Length; i++)
                {
                    if (fileExtension == allowedExtensions[i])
                    {
                        fileOK = true;
                    }
                }
            }
            if (fileOK)
            {
                FileInfo file = new FileInfo(path + filename);
                string fileNameOnly = Path.GetFileNameWithoutExtension(filename); //去除副檔名，只取得檔名
                int fileCount = 1;

                try
                {
                    //檢查 Server 上該資料夾是否存在，不存在就自動建立
                    if (!Directory.Exists(@"~\UploadedImages\")) //路徑指定方法2 ex.@"C:\Test.txt"
                    {
                        Directory.CreateDirectory(@"~\UploadedImages\");
                    }
                    
                    while (file.Exists)
                    {
                        //若欲上傳圖片檔名與serverfile內重複，重新命名： 檔名_1、檔名_2...
                        filename = string.Concat(fileNameOnly, "_", fileCount, fileExtension); //檔名_num.副檔名
                        file = new FileInfo(path + filename); //更新FileInfo
                        fileCount++;
                    }
                    //上傳圖片
                    Photo.PostedFile.SaveAs(path + filename);
                    photopath = "UploadedImages/" + filename;
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
            }

            //驗證部分
            DateTime date = new DateTime();
            int num;
            if (string.IsNullOrEmpty(Request.Form["EmployeeName"]))
            {
                Response.Write("<font color=FF0000>請確認是否已輸入EmployeeName!</font>");
            }
            else if (string.IsNullOrEmpty(Request.Form["Title"]))
            {
                Response.Write("<font color=FF0000>請確認是否已輸入Title!</font>");
            }
            else if (string.IsNullOrEmpty(Request.Form["TitleOfCourtesy"]))
            {
                Response.Write("<font color=FF0000>請確認是否已輸入TitleOfCourtesy!</font>");
            }
            else if (DateTime.TryParse(Request.Form["BirthDate"], out date) == false)
            {
                Response.Write("<font color=FF0000>請確認是否已輸入BirthDate(必須是日期格式)!</font>");
            }
            else if (DateTime.TryParse(Request.Form["HireDate"], out date) == false)
            {
                Response.Write("<font color=FF0000>請確認是否已輸入HireDate(必須是日期格式)!</font>");
            }
            else if (string.IsNullOrEmpty(Request.Form["Address"]))
            {
                Response.Write("<font color=FF0000>請確認是否已輸入Address!</font>");
            }
            else if (string.IsNullOrEmpty(Request.Form["HomePhone"]))
            {
                Response.Write("<font color=FF0000>請確認是否已輸入HomePhone!</font>");
            }
            else if (string.IsNullOrEmpty(Request.Form["Extension"]))
            {
                Response.Write("<font color=FF0000>請確認是否已輸入Extension!</font>");
            }
            else if (string.IsNullOrEmpty(Request.Form["Notes"]))
            {
                Response.Write("<font color=FF0000>請確認是否已輸入Notes!</font>");
            }
            else if (int.TryParse(Request.Form["ManagerID"], out num) == false)
            {
                Response.Write("<font color=FF0000>請確認是否已輸入ManagerID(必須是數字)!</font>");
            }
            else if (int.TryParse(Request.Form["Salary"], out num) == false)
            {
                Response.Write("<font color=FF0000>請確認是否已輸入Salary(必須是數字)!</font>");
            }
            //更改已有資料
            else
            {
                if ((String)Session["actionType"] == "edit")
                {
                    string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ToString();
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        string queryString = "UPDATE Employees SET EmployeeName=@EmployeeName,Title=@Title,TitleOfCourtesy=@TitleOfCourtesy" +
                                    ",BirthDate=@BirthDate,HireDate=@HireDate,Address=@Address,HomePhone=@HomePhone,Extension=@Extension" +
                                    ",PhotoPath=@PhotoPath,Notes=@Notes,ManagerID=@ManagerID,Salary=@Salary WHERE EmployeeID=@EmployeeID";
                        SqlCommand command = new SqlCommand(queryString, connection);
                        command.Parameters.AddWithValue("@EmployeeName", (String)Request.Form["EmployeeName"]);
                        command.Parameters.AddWithValue("@Title", (String)Request.Form["Title"]);
                        command.Parameters.AddWithValue("@TitleOfCourtesy", (String)Request.Form["TitleOfCourtesy"]);
                        command.Parameters.AddWithValue("@BirthDate", DateTime.Parse(Request.Form["BirthDate"]).ToShortDateString());
                        command.Parameters.AddWithValue("@HireDate", DateTime.Parse(Request.Form["HireDate"]).ToShortDateString());
                        command.Parameters.AddWithValue("@Address", (String)Request.Form["Address"]);
                        command.Parameters.AddWithValue("@HomePhone", (String)Request.Form["HomePhone"]);
                        command.Parameters.AddWithValue("@Extension", (String)Request.Form["Extension"]);
                        command.Parameters.AddWithValue("@PhotoPath", photopath);
                        command.Parameters.AddWithValue("@Notes", (String)Request.Form["Notes"]);
                        command.Parameters.AddWithValue("@ManagerID", Convert.ToInt32(Request.Form["ManagerID"]));
                        command.Parameters.AddWithValue("@Salary", Convert.ToInt32(Request.Form["Salary"]));
                        command.Parameters.AddWithValue("@EmployeeID", Convert.ToInt32(Session["ID"]));
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    Response.Redirect("Default.aspx");
                }
                //新增新的資料
                else
                {
                    string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ToString();
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        string queryString = "INSERT INTO Employees (EmployeeName,Title,TitleOfCourtesy,BirthDate,HireDate,Address,HomePhone,Extension,PhotoPath,Notes,ManagerID,Salary) " +
                            "VALUES (@EmployeeName,@Title,@TitleOfCourtesy,@BirthDate,@HireDate,@Address,@HomePhone,@Extension,@PhotoPath,@Notes,@ManagerID,@Salary)";
                        SqlCommand command = new SqlCommand(queryString, connection);
                        command.Parameters.AddWithValue("@EmployeeName", (String)Request.Form["EmployeeName"]);
                        command.Parameters.AddWithValue("@Title", (String)Request.Form["Title"]);
                        command.Parameters.AddWithValue("@TitleOfCourtesy", (String)Request.Form["TitleOfCourtesy"]);
                        command.Parameters.AddWithValue("@BirthDate", DateTime.Parse(Request.Form["BirthDate"]).ToShortDateString());
                        command.Parameters.AddWithValue("@HireDate", DateTime.Parse(Request.Form["HireDate"]).ToShortDateString());
                        command.Parameters.AddWithValue("@Address", (String)Request.Form["Address"]);
                        command.Parameters.AddWithValue("@HomePhone", (String)Request.Form["HomePhone"]);
                        command.Parameters.AddWithValue("@Extension", (String)Request.Form["Extension"]);
                        command.Parameters.AddWithValue("@PhotoPath", photopath);
                        command.Parameters.AddWithValue("@Notes", (String)Request.Form["Notes"]);
                        command.Parameters.AddWithValue("@ManagerID", Convert.ToInt32(Request.Form["ManagerID"]));
                        command.Parameters.AddWithValue("@Salary", Convert.ToInt32(Request.Form["Salary"]));
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    Response.Redirect("Default.aspx");
                }
            }
        }
    }
}
