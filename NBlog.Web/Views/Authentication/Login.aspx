<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Layout.master" Inherits="ViewPage<Authentication.LoginModel>" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<div id="main">

<h1>Sign In</h1>

<% using (Html.BeginForm("OpenId", "Authentication", new { ReturnUrl = Model.ReturnUrl }, FormMethod.Post, new { @class = "openid" })) { %>
    
    <%= Html.AntiForgeryToken()%>

    <div>
        <ul class="providers"> 
            <li class="openid" title="OpenID"><img src="/resources/scripts/jQueryOpenIdPlugin/images/openidB.png" alt="icon" /> 
                <span><strong>http://{your-openid-url}</strong></span>
            </li>
            <li class="direct" title="Google"> 
                <img src="/resources/scripts/jQueryOpenIdPlugin/images/googleB.png" alt="icon" /><span>https://www.google.com/accounts/o8/id</span>
            </li> 
            <li class="direct" title="Yahoo"> 
                <img src="/resources/scripts/jQueryOpenIdPlugin/images/yahooB.png" alt="icon" /><span>http://yahoo.com/</span>
            </li> 
            <li class="username" title="MyOpenID user name"> 
                <img src="/resources/scripts/jQueryOpenIdPlugin/images/myopenidB.png" alt="icon" /><span>http://<strong>username</strong>.myopenid.com/</span>
            </li> 
        </ul>
    </div> 
    <fieldset> 
        <label for="openid_username">Enter your <span>Provider user name</span></label> 
        <div>
            <span></span><input type="text" name="openid_username" /><span></span> 
            <input type="submit" value="Login" />
        </div> 
    </fieldset> 
    <fieldset> 
        <label for="openid_identifier">Enter your <a class="openid_logo" href="http://openid.net">OpenID</a></label> 
        <div>
            <input type="text" name="openid_identifier" /> 
            <input type="submit" value="Sign In" />
        </div> 
    </fieldset> 

<% } %>

    <div class="message">
        <%= Model.Message %>
    </div>

</div>
</asp:Content>

<asp:Content ContentPlaceHolderID="Scripts" runat="server">
<script>
    $(function () { $("form.openid:eq(0)").openid(); });
</script>
</asp:Content>