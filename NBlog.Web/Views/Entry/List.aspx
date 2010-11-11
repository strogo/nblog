<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Layout.master" Inherits="ViewPage<EntryController.ListModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>List entries</h2>

<% foreach (var entry in Model.Entries) { %>
  <%= entry.Title %>
<% } %>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
