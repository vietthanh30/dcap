<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TreeDemo.aspx.cs" Inherits="web_app.TreeDemo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <script src="plugins/jQuery/jQuery-2.1.4.min.js"></script>
        <script type="text/javascript" src="Scripts/excanvas.js"></script>
        <script type="text/javascript" src="Scripts/jit.js"></script>
        <script type="text/javascript" src="Scripts/jquery.spacetree.js"></script>
        <script type="text/javascript">
            $(document).ready(function () {
                $('#tree').spacetree('#spacetree').hide();
            });
        </script>
        <link type="text/css" rel="stylesheet" href="Styles/jquery.spacetree.css" media="screen" />
</head>
<body>
    <form id="form1" runat="server">
        <div id="tree">
        <asp:Literal ID="ltrTree" runat="server" />
        </div>
        <div id="spacetree">
        </div>
    </form>
</body>
</html>
