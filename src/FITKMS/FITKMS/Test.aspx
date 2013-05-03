<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="FITKMS.Test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.5.1/jquery.min.js"></script>
    <script type="text/javascript" src="Content/js/jquery.tokeninput.js"></script>

    <link rel="stylesheet" href="Content/css/token-input-facebook.css" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <h2 id="theme">Facebook Theme</h2>
    <div>
        <input type="text" id="demo-input-facebook-theme" name="blah2" />
        <input type="button" value="Submit" />
        <script type="text/javascript">
            $(document).ready(function () {
                $("#demo-input-facebook-theme").tokenInput("http://shell.loopj.com/tokeninput/tvshows.php", {
                    theme: "facebook"
                });
            });
        </script>
    </div>
    </div>
    </form>
</body>
</html>
