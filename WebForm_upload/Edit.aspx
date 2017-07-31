<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="WebForm_upload.Edit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <th>EmployeeName</th>
                    <td>
                        <asp:TextBox ID="EmployeeName" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>Title</th>
                    <td>
                        <asp:TextBox ID="Title" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>TitleOfCourtesy</th>
                    <td>
                        <asp:TextBox ID="TitleOfCourtesy" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>BirthDate</th>
                    <td>
                        <asp:TextBox ID="BirthDate" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>HireDate</th>
                    <td>
                        <asp:TextBox ID="HireDate" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>Address</th>
                    <td>
                        <asp:TextBox ID="Address" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>HomePhone</th>
                    <td>
                        <asp:TextBox ID="HomePhone" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>Extension</th>
                    <td>
                        <asp:TextBox ID="Extension" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>PhotoPath</th>
                    <td>
                        <asp:TextBox ID="PhotoPath" runat="server" ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>UploadPhoto</th>
                    <td>
                        <asp:FileUpload ID="Photo" runat="server" />
                        </td>
                    </tr>
                <tr>
                    <th>Notes</th>
                    <td>
                        <asp:TextBox ID="Notes" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>ManagerID</th>
                    <td>
                        <asp:TextBox ID="ManagerID" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>Salary</th>
                    <td>
                        <asp:TextBox ID="Salary" runat="server"></asp:TextBox>
                    </td>
                </tr>

                <tr>
                    <th colspan="2" style="text-align: center">
                        <asp:Button ID="save" runat="server" Text="存檔" onclick="Save_Click"/>
                        <asp:Button ID="cancel" runat="server" Text="取消" onclick="Cancel_Click"/>
                    </th>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
