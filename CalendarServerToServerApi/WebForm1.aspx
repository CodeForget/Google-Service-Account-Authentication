<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="CalendarServerToServerApi.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Event Set on google Calendar using service Account Authentication </h1>
            <h2>This code run on page load</h2>
            <h3>Please change required credencial from your's Account and replace JSON private key in key folder from Your JSON private key</h3>
            <h1>Thanks I hope You enjoy It</h1>
        </div>
        <asp:Button runat="server" Text="Set Event(Insert event to google calender using service Account Authentication)" OnClick="Authenticate" />
    </form>
</body>

</html>
