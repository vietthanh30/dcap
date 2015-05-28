<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Register.aspx.cs" Inherits="web_app.admin.Register" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <h1>
    Đăng ký thành viên
    </h1>
    <ol class="breadcrumb">
    <li><a href="#"><i class="fa fa-dashboard"></i> Trang chủ</a></li>
    <li class="active">Đăng ký thành viên</li>
    </ol>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:HiddenField ID="ContinueDestinationPageUrl" runat="server" />
    <span class="failureNotification">
        <asp:Literal ID="ErrorMessage" runat="server"></asp:Literal>
    </span>
    <asp:ValidationSummary ID="RegisterUserValidationSummary" runat="server" CssClass="failureNotification" 
            ValidationGroup="RegisterUserValidationGroup"/>
    <div class="row">
    <!-- left column -->
        <div class="col-xs-12">
        <!-- general form elements -->
            <div class="box box-primary">
                <div class="box-body">
				<div class="row">
					<div class="col-xs-4">
					<label for="ParentId">Người giới thiệu</label>
					<input type="text" class="form-control" id="ParentId" runat="server" placeholder="Nhập ID người giới thiệu">
					</div>
					<div class="col-xs-4">
					<label for="DirectParentId">Tuyến trên</label>
					<input type="text" class="form-control" id="DirectParentId" runat="server" placeholder="Nhập ID thành viên tuyến trên">
					</div>
				</div>
				<div class="row">
					<div class="col-xs-4">
					<label for="HoTen">Họ tên</label>
					<input type="text" class="form-control" id="HoTen" runat="server" placeholder="Nhập họ tên">
                    <asp:RequiredFieldValidator ID="HoTenRequired" runat="server" ControlToValidate="HoTen" 
                        CssClass="failureNotification" ErrorMessage="Họ tên bắt buộc nhập." ToolTip="Họ tên bắt buộc nhập." 
                        ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
					</div>
				</div>
				<div class="row">
					<div class="col-xs-4">
					<label for="NgaySinh">Ngày sinh</label>
					<input type="text" class="form-control" id="NgaySinh" runat="server" placeholder="Nhập ngày sinh">
					</div>
					<div class="col-xs-4">
					<label for="SoCmnd">Số CMND</label>
					<input type="text" class="form-control" id="SoCmnd" runat="server" placeholder="Nhập số CMND">
                    <asp:RequiredFieldValidator ID="SoCmndRequired" runat="server" ControlToValidate="SoCmnd" 
                        CssClass="failureNotification" ErrorMessage="Số CMND bắt buộc nhập." ToolTip="Số CMND bắt buộc nhập." 
                        ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
					</div>
				</div>
				<div class="row">
					<div class="col-xs-8">
					<label for="DiaChi">Địa chỉ</label>
					<input type="text" class="form-control" id="DiaChi" runat="server" placeholder="Nhập địa chỉ">
					</div>
				</div>
				<div class="row">
					<div class="col-xs-2">
					<label for="SoTaiKhoan">Số tài khoản</label>
					<input type="text" class="form-control" id="SoTaiKhoan" runat="server" placeholder="Nhập số TK">
					</div>
					<div class="col-xs-6">
					<label for="ChiNhanhNH">Ngân hàng</label>
					<input type="text" class="form-control" id="ChiNhanhNH" runat="server" placeholder="Nhập thông tin ngân hàng">
					</div>
				</div>
					
				<div class="row">
					<div class="col-xs-6">
					<label for="exampleInputFile">Ảnh</label>
					<input type="file" runat="server" id="filePhotoUpload">
					<p class="help-block">Chọn ảnh chân dung</p>
					</div>
				</div>
                    
                </div><!-- /.box-body -->
				  
                <div class="box-footer">
                <asp:Button ID="SearchUserButton" runat="server" Text="Tra cứu" 
                        ValidationGroup="RegisterUserValidationGroup" class="btn btn-primary" 
                    onclick="RegisterUser_SearchUser"/>
                <asp:Button ID="CreateUserButton" runat="server" Text="Cập nhật" 
                        ValidationGroup="RegisterUserValidationGroup" class="btn btn-primary" 
                    onclick="RegisterUser_CreatingUser"/>
                </div>
            </div><!-- /.box -->
        </div><!-- /.box -->
    </div><!--/.col (left) -->

</asp:Content>
