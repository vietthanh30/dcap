<%@ Page Language="C#" MasterPageFile="~/Site.master" CodeBehind="dropdown-menu.aspx.cs" Inherits="web_app.control.dropdown_menu" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="DropdownContent">
  		
<ul class="nav navbar-nav">
	<!-- User Account: style can be found in dropdown.less -->
	<li class="dropdown user user-menu">
	<a id="A1" href="~/control/dropdown-menu.aspx" runat="server" class="dropdown-toggle" data-toggle="dropdown">
		<img ID="Img1" src="~/dist/img/user2-160x160.jpg" runat="server" class="user-image" alt="User Image"/>
		<span class="hidden-xs"><asp:LoginName ID="LoginName1" runat="server" /></span>
	</a>
</li>
</ul> 
    <asp:LoginStatus ID="HeadLoginStatus" runat="server" LogoutAction="Redirect" LogoutText="Đăng xuất" LogoutPageUrl="~/"/>
<ul class="dropdown-menu">
<!-- User image -->
<li class="user-header">
<img ID="HeadLoginImg" src="~/dist/img/user2-160x160.jpg" runat="server" class="img-circle" alt="User Image" />
<p>
	<asp:LoginName ID="HeadLoginName" runat="server" /> - Admin
</p>
</li>
					  
<!-- Menu Footer-->
<li class="user-footer">
<div class="pull-left">
	<a href="~/admin/Register.aspx" runat="server" ID="HeadRegisterUser" class="btn btn-default btn-flat">Hồ sơ cá nhân</a>&nbsp;&nbsp;&nbsp;
</div>
<div class="pull-right">
	<a href="~/admin/ChangePassword.aspx" runat="server" ID="HeadChangePassword" class="btn btn-default btn-flat" data-toggle="dropdown">Đổi mật khẩu</a>
</div>
</li>
</ul>
</asp:Content>
 