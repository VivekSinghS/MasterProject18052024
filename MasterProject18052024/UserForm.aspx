<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="UserForm.aspx.cs" Inherits="MasterProject18052024.UserForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
    <tr>
        <td>User Name:</td>
        <td><asp:TextBox ID="txtName" runat="server"></asp:TextBox></td>
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
                               <td>Image:</td>
                               <td><asp:FileUpload ID="ImgFile" runat="server" /></td>
                           </tr>
                <tr>
                    <td></td>
                    <td><asp:Button ID="btnSubmit" runat="server" Text="Submit" BackColor="Blue" ForeColor="White" OnClick="btnSubmit_Click"/></td>
                </tr>
                     <tr>
                         <td></td>
                         <td style = "color:black">
                             <asp:GridView ID="grdUser" runat="server" AutoGenerateColumns="false" OnRowCommand="grdUser_RowCommand">
                             <Columns>
                                 <asp:TemplateField HeaderText="User ID">
                                     <ItemTemplate>
                                         <%#Eval("uId") %>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="User Name">
                                     <ItemTemplate>
                                         <%#Eval("uName") %>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="User Country">
                                     <ItemTemplate>
                                         <%#Eval("CountryName") %>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="User State">
                                     <ItemTemplate>
                                         <%#Eval("StateName") %>
                                     </ItemTemplate> 
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="User City">
                                     <ItemTemplate>
                                         <%#Eval("CityName")%>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Image">
                                     <ItemTemplate>
                                        <asp:Image ID="Imgload" runat="server" Width="55px" Height="55px" ImageUrl='<%#Eval("uImage","~/MyImage/{0}") %>' />
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField>
                                     <ItemTemplate>
                                        <asp:Button ID ="btnDelete" runat="server" Text="Delete" CommandName="Dlt" CommandArgument='<%#Eval("uId")%>'/>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField>
                                     <ItemTemplate>
                                         <asp:Button ID="btnEdit" runat="server" Text="Edit" CommandName="Edt" CommandArgument='<%#Eval("uId")%>'/>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                             </Columns>
                        </asp:GridView></td>
                     </tr>

</table>
</asp:Content>
