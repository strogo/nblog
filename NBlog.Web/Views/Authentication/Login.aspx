<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Layout.master" Inherits="ViewPage<Authentication.LoginModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div id="main">

    <h1>Sign In</h1>

    <% using(Html.BeginForm("OpenId", "Authentication", new { ReturnUrl = Model.ReturnUrl })) { %>
        <%= Html.AntiForgeryToken() %>
        <%= Html.LabelFor(m => m.OpenIdIdentifier)%>:
        <%= Html.TextBoxFor(m => m.OpenIdIdentifier)%>
        <input type="submit" value="Sign In" />
    <% } %>

    <div>
        Message: <%= Model.Message %>
    </div>

</div>
</asp:Content>