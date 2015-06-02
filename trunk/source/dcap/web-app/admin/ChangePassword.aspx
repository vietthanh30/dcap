<%@ Page Title="Change Password" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="ChangePassword.aspx.cs" Inherits="web_app.admin.ChangePassword" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Đổi mật khẩu
    </h2>
    <p>
        Sử dụng mẫu dưới đây để thay đổi mật khẩu của bạn.
    </p>
    <p>
        Mật khẩu mới cần phải được tối thiểu là <%= Membership.MinRequiredPasswordLength %> ký tự.
    </p>
    <asp:ChangePassword ID="ChangeUserPassword" runat="server" CancelDestinationPageUrl="~/" EnableViewState="false" RenderOuterTable="false" 
         SuccessPageUrl="ChangePasswordSuccess.aspx">
        <ChangePasswordTemplate>
            <span class="failureNotification">
                <asp:Literal ID="FailureText" runat="server"></asp:Literal>
            </span>
            <asp:ValidationSummary ID="ChangeUserPasswordValidationSummary" runat="server" CssClass="failureNotification" 
                 ValidationGroup="ChangeUserPasswordValidationGroup"/>
            <div class="accountInfo">
                <fieldset class="changePassword">
                    <legend>Thông tin người dùng</legend>
            <div class="box box-primary">
                <div class="box-body">   
                <div class="row">
					<div class="col-xs-8">
                        <asp:Label ID="CurrentPasswordLabel" runat="server" AssociatedControlID="CurrentPassword">Mật khẩu cũ:</asp:Label>
                        <asp:TextBox ID="CurrentPassword" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="CurrentPasswordRequired" runat="server" ControlToValidate="CurrentPassword" 
                             CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="Old Password is required." 
                             ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:RequiredFieldValidator>
                    </div>
                </div>          
                <div class="row">
					<div class="col-xs-8">
                        <asp:Label ID="NewPasswordLabel" runat="server" AssociatedControlID="NewPassword">Mật khẩu mới:</asp:Label>
                        <asp:TextBox ID="NewPassword" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="NewPasswordRequired" runat="server" ControlToValidate="NewPassword" 
                             CssClass="failureNotification" ErrorMessage="New Password is required." ToolTip="New Password is required." 
                             ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:RequiredFieldValidator>
                    </div>
                </div>          
                <div class="row">
					<div class="col-xs-8">
                        <asp:Label ID="ConfirmNewPasswordLabel" runat="server" AssociatedControlID="ConfirmNewPassword">Xác nhận mật khẩu Mới:</asp:Label>
                        <asp:TextBox ID="ConfirmNewPassword" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="ConfirmNewPasswordRequired" runat="server" ControlToValidate="ConfirmNewPassword" 
                             CssClass="failureNotification" Display="Dynamic" ErrorMessage="Confirm New Password is required."
                             ToolTip="Confirm New Password is required." ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="NewPasswordCompare" runat="server" ControlToCompare="NewPassword" ControlToValidate="ConfirmNewPassword" 
                             CssClass="failureNotification" Display="Dynamic" ErrorMessage="The Confirm New Password must match the New Password entry."
                             ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:CompareValidator>
                    </div>
                </div>    
                </div><!-- /.box-body -->
            </div><!-- /.box -->
                </fieldset>
                <p class="submitButton">
                    <asp:Button ID="CancelPushButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel"/>&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="ChangePasswordPushButton" runat="server" 
                        Text="Change Password" ValidationGroup="ChangeUserPasswordValidationGroup" 
                        onclick="ChangeUserPassword_ChangingPassword"/>
                </p>
            </div>
        </ChangePasswordTemplate>
    </asp:ChangePassword>
</asp:Content>
