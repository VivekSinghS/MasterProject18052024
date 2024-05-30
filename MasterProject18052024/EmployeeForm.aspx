  <%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="EmployeeForm.aspx.cs" Inherits="MasterProject18052024.EmployeeForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<table>
    <tr>
        <td>Name:</td>
        <td><asp:TextBox ID="txtName" runat="server"></asp:TextBox></td>
    </tr>
            <tr>
                <td>Gender:</td>
                <td><asp:RadioButtonList ID="rblGender" runat="server" RepeatColumns="3"></asp:RadioButtonList></td>
            </tr>
            <tr>
              <td>Country:</td>
              <td><asp:DropDownList ID="ddlCountry" runat="server" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td>
          </tr>
                 <tr>
                     <td>State:</td>
                     <td><asp:DropDownList ID="ddlState" runat="server" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td>
                 </tr>
                        <tr>
                            <td>City:</td>
                            <td><asp:DropDownList ID="ddlCity" runat="server"></asp:DropDownList></td>
                        </tr>
                <tr>
                    <td></td>
                    <td><asp:Button ID="btnSubmit" runat="server" Text="Submit" BackColor="Blue" ForeColor="White" OnClick="btnSubmit_Click" /></td>
                </tr>
                     <tr>
                         <td></td>
                         <td style = "color:black">
                             <asp:GridView ID="grdEmployee" runat="server" AutoGenerateColumns="false" OnRowCommand="grdEmployee_RowCommand">
                             <Columns>
                                 <asp:TemplateField HeaderText="ID">
                                     <ItemTemplate>
                                         <%#Eval("EmpId") %>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Employee Name">
                                     <ItemTemplate>
                                         <%#Eval("EmpName") %>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Employee Gender">
                                     <ItemTemplate>
                                         <%#Eval("gName") %>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Employee Country">
                                     <ItemTemplate>
                                         <%#Eval("CountryName") %>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Employee State">
                                     <ItemTemplate>
                                         <%#Eval("StateName") %>
                                     </ItemTemplate> 
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Employee City">
                                     <ItemTemplate>
                                         <%#Eval("CityName")%>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField>
                                     <ItemTemplate>
                                        <asp:Button ID ="btnDelete" runat="server" Text="Delete" CommandName="Dlt" CommandArgument='<%#Eval("EmpId")%>'/>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField>
                                     <ItemTemplate>
                                         <asp:Button ID="btnEdit" runat="server" Text="Edit" CommandName="Edt" CommandArgument='<%#Eval("EmpId")%>'/>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                             </Columns>
                        </asp:GridView></td>
                     </tr>

</table>
</asp:Content>
