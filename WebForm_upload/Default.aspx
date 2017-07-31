<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebForm_upload._Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        查詢條件:<br />
        名字:
        <asp:TextBox ID="query_name" runat="server"></asp:TextBox>
        職稱:<asp:TextBox ID="query_title" runat="server"></asp:TextBox>
        <asp:Button ID="BtnQuery" runat="server" Text="查詢" />
        <br />
        <asp:Button ID="BtnAdd" runat="server" Text="新增" Onclick="New"/>
        <div>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="800px" OnRowCommand="GridView1_RowCommand" OnRowCreated="GridView1_RowCreated">
                <Columns>
                    <asp:BoundField DataField="EmployeeID" HeaderText="ID">
                    </asp:BoundField>
                    <asp:ImageField DataImageUrlField="PhotoPath" HeaderText="Photo">
                        <ControlStyle Height="60px" Width="60px" />
                    </asp:ImageField>
                    <asp:BoundField DataField="EmployeeName" HeaderText="Name">
                    </asp:BoundField>
                    <asp:BoundField DataField="Title" HeaderText="Title">
                    </asp:BoundField>
                    <asp:BoundField DataField="BirthDate" DataFormatString="{0:d}" HeaderText="BirthDate">
                    </asp:BoundField>
                    <asp:BoundField DataField="Address" HeaderText="Address">
                    </asp:BoundField>
                    <asp:BoundField DataField="Salary" HeaderText="Salary">
                    </asp:BoundField>
                    <asp:TemplateField>
                        <ItemTemplate>
                        <asp:Button ID="BtnEdit" runat="server" Text="編輯" CommandName="editbyID"/>
                        <asp:Button ID="BtnDelete" runat="server" Text="刪除"  CommandName="deletebyID" />
                    </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <asp:Button ID="BtnFirstPage" runat="server" Text="第一頁" />
        <asp:Button ID="BtnPPage" runat="server" Text="上一頁" />
        <asp:Button ID="BtnNPage" runat="server" Text="下一頁" />
        <asp:Button ID="BtnLastPage" runat="server" Text="最後頁" />
    </form>

</body>
</html>