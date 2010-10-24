<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Layout.master" Inherits="ViewPage<AuthenticationController.LoginModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Login</h2>

    <div>
        Message: <%= Model.Message %>
    </div>

    <% using(Html.BeginForm("OpenId", "Authentication", new { ReturnUrl = Model.ReturnUrl })) { %>
        <%= Html.Label("identifier")%>:
        <%= Html.TextBox("identifier")%>
        <input type="submit" value="Sign In" />
    <% } %>

</asp:Content>