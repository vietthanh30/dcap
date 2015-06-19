<%@ Page Title="Đăng ký quản lý" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Register2.aspx.cs" Inherits="web_app.admin.Register2" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <h1>
    Đăng ký quản lý
    </h1>
    <ol class="breadcrumb">
    <li><a href="#"><i class="fa fa-dashboard"></i> Trang chủ</a></li>
    <li class="active">Đăng ký quản lý</li>
    </ol>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:HiddenField ID="ContinueDestinationPageUrl" runat="server" />
    <div class="row">
    <!-- left column -->
        <div class="col-xs-12">
        <!-- general form elements -->
            <div class="box box-primary">
                <div class="box-body">   
                <div class="row">
					<div class="col-xs-8">
                    <asp:Label ID="InvalidCredentialsMessage" runat="server" ForeColor="Red" class="failureNotification"
                     Text="" Visible="False"></asp:Label>
                     </div>
                </div>          
                <div class="row">
					<div class="col-xs-8">
                    <span class="failureNotification">
                        <asp:Literal ID="ErrorMessage" runat="server"></asp:Literal>
                    </span>
                    <asp:ValidationSummary ID="RegisterUserValidationSummary" runat="server" CssClass="failureNotification" 
                            ValidationGroup="RegisterUserValidationGroup"/>  
                    </div>
                </div>          
                <div class="row">
					<div class="col-xs-8">
                    <asp:Label ID="AccountCode" runat="server" Text="" ForeColor="Blue" Visible="False"></asp:Label>
                    </div>
                </div>
				<div class="row">
					<div class="col-xs-4">
					<label for="UserRole">Quyền người dùng</label>
                    <asp:RadioButtonList ID="UserRole" runat="server" class="form-control" RepeatDirection="Horizontal" RepeatLayout="Table" ToolTip="Nhập quyền người dùng">
                        <asp:ListItem Text="Quản trị hệ thống" Value="QTHT" />
                        <asp:ListItem Text="Quản lý kế toán" Value="QLKT" />
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="UserRole" 
                        CssClass="failureNotification" ErrorMessage="Quyền người dùng bắt buộc nhập." ToolTip="Quyền người dùng bắt buộc nhập." 
                        ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
					</div>
				</div>
				<div class="row">
					<div class="col-xs-4">
					<label for="HoTen">Họ tên</label>
					<input type="text" class="form-control" maxlength="100" id="HoTen" runat="server" placeholder="Nhập họ tên">
                    <asp:RequiredFieldValidator ID="HoTenRequired" runat="server" ControlToValidate="HoTen" 
                        CssClass="failureNotification" ErrorMessage="Họ tên bắt buộc nhập." ToolTip="Họ tên bắt buộc nhập." 
                        ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
					</div>
				</div>
				<div class="row">
					<div class="col-xs-4">
					<label for="TenDangNhap">Tên đăng nhập</label>
					<input type="text" class="form-control" maxlength="50" id="TenDangNhap" runat="server" placeholder="Nhập tên đăng nhập">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TenDangNhap" 
                        CssClass="failureNotification" ErrorMessage="Tên đăng nhập bắt buộc nhập." ToolTip="Tên đăng nhập bắt buộc nhập." 
                        ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
					</div>
				</div>
					
                </div><!-- /.box-body -->
				  
                <div class="box-footer">
                <asp:Button ID="CreateUserButton" runat="server" Text="Cập nhật" 
                        ValidationGroup="RegisterUserValidationGroup" class="btn btn-primary" 
                    onclick="RegisterUser_CreatingUser"/>
                </div> 
            </div><!-- /.box -->
        </div><!-- /.box -->
    </div><!--/.col (left) -->

</asp:Content>
