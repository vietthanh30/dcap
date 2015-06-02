<%@ Page Title="Log In" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Login.aspx.cs" Inherits="web_app.admin.Login" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Đăng nhập
    </h2>
    <p>
        Vui lòng nhập tên và mật khẩu.
    </p>
    <p>
        <asp:Label ID="InvalidCredentialsMessage" runat="server" class="failureNotification"
         Text="" Visible="False"></asp:Label>
    </p>
    <span class="failureNotification">
        <asp:Literal ID="FailureText" runat="server"></asp:Literal>
    </span>
    <asp:ValidationSummary ID="LoginUserValidationSummary" runat="server" CssClass="failureNotification" 
            ValidationGroup="LoginUserValidationGroup"/>
    <div class="accountInfo">
        <fieldset class="login">
            <legend>Thông tin người dùng</legend>
            <div class="box box-primary">
                <div class="box-body">   
                <div class="row">
					<div class="col-xs-3">
                <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Tên đăng nhập:</asp:Label>
                <asp:TextBox ID="UserName" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" 
                        CssClass="failureNotification" ErrorMessage="User Name is required." ToolTip="User Name is required." 
                        ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                    </div>
                </div>          
                <div class="row">
					<div class="col-xs-3">
                <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Mật khẩu:</asp:Label>
                <asp:TextBox ID="Password" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" 
                        CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="Password is required." 
                        ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                    </div>
                </div>          
                <div class="row">
					<div class="col-xs-3">
                <asp:Image ID="lblCaptchaImage" ImageUrl="~/admin/CImage.aspx" runat="server"></asp:Image>
                    </div>
                </div>          
                <div class="row">
					<div class="col-xs-3">
                <asp:TextBox ID="CaptchaImage" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>          
                <div class="row">
					<div class="col-xs-3">
                <asp:CheckBox ID="RememberMe" runat="server"/>
                <asp:Label ID="RememberMeLabel" runat="server" AssociatedControlID="RememberMe" CssClass="inline">Giữ luôn đăng nhập</asp:Label>
                    </div>
                </div> 
                </div><!-- /.box-body -->
            </div><!-- /.box -->
        </fieldset>
        <p class="submitButton">
            <asp:Button ID="LoginButton" runat="server" Text="Log In" 
                ValidationGroup="LoginUserValidationGroup" onclick="ValidateUser"/>
        </p>
    </div>
</asp:Content>
