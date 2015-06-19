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
					<label for="DirectParentId">Người giới thiệu</label>
					<input type="text" class="form-control" id="DirectParentId" runat="server" placeholder="Nhập ID người giới thiệu">
					</div>
					<div class="col-xs-4">
					<input type="text" class="form-control" id="DirectParentName" runat="server" readonly="true" onchange="RegisterUser_OnDirectParentChange">
					</div>
				</div>
				<div class="row">
					<div class="col-xs-4">
					<label for="ParentId">Tuyến trên</label>
					<input type="text" class="form-control" id="ParentId" runat="server" placeholder="Nhập ID thành viên tuyến trên">
					</div>
					<div class="col-xs-4">
					<input type="text" class="form-control" id="ParentName" runat="server" readonly="true" onchange="RegisterUser_OnParentChange">
					</div>
				</div>
				<div class="row">
					<div class="col-xs-4">
					<label for="HoTen">Họ tên</label>
					<input type="text" class="form-control" id="HoTen" maxlength="100" runat="server" placeholder="Nhập họ tên">
                    <asp:RequiredFieldValidator ID="HoTenRequired" runat="server" ControlToValidate="HoTen" 
                        CssClass="failureNotification" ErrorMessage="Họ tên bắt buộc nhập." ToolTip="Họ tên bắt buộc nhập." 
                        ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
					</div>
				</div>
				<div class="row">
					<div class="col-xs-4">
					<label for="NgaySinh">Ngày sinh</label>
                    <div class="input-group">
                      <div class="input-group-addon">
                        <i id="imgNgaySinh" class="fa fa-calendar" runat="server"></i>
                      </div>
                      <input type="text" id="NgaySinh" runat="server" maxlength="10" class="form-control" placeholder="dd/mm/yyyy" >
                    </div><!-- /.input group -->
					</div>
					<div class="col-xs-4">
					<label for="SoCmnd">Số CMND</label>
					<input type="text" class="form-control" id="SoCmnd" maxlength="15" runat="server" placeholder="Nhập số CMND">
                    <asp:RequiredFieldValidator ID="SoCmndRequired" runat="server" ControlToValidate="SoCmnd" 
                        CssClass="failureNotification" ErrorMessage="Số CMND bắt buộc nhập." ToolTip="Số CMND bắt buộc nhập." 
                        ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
					</div>
				</div>
				<div class="row">
                    <div class="col-xs-4">
					<label for="NgayCap">Ngày cấp</label>
                    <div class="input-group">
                      <div class="input-group-addon">
                        <i id="i1" class="fa fa-calendar" runat="server"></i>
                      </div>
                        <input type="text" ID="NgayCap" runat="server" maxlength="10" class="form-control" placeholder="dd/mm/yyyy" >
                    </div><!-- /.input group -->
					</div>
					<div class="col-xs-4">
					<label for="SoDienThoai">Số điện thoại</label>
					<input type="text" class="form-control" id="SoDienThoai" maxlength="15" runat="server" placeholder="Nhập số điện thoại">
					</div>
				</div>
				<div class="row">
					<div class="col-xs-2">
					<label for="GioiTinh">Giới tính</label>
                    <asp:RadioButtonList ID="GioiTinh" runat="server" class="form-control" RepeatDirection="Horizontal" RepeatLayout="Table" ToolTip="Nhập giới tính">
                        <asp:ListItem Text="Nam" Value="M" />
                        <asp:ListItem Text="Nữ" Value="F" />
                    </asp:RadioButtonList>
					</div>
					<div class="col-xs-6">
					<label for="DiaChi">Địa chỉ</label>
					<input type="text" class="form-control" id="DiaChi" maxlength="500" runat="server" placeholder="Nhập địa chỉ">
					</div>
				</div>
				<div class="row">
					<div class="col-xs-2">
					<label for="SoTaiKhoan">Số tài khoản</label>
					<input type="text" class="form-control" id="SoTaiKhoan" maxlength="50" runat="server" placeholder="Nhập số TK">
					</div>
					<div class="col-xs-6">
					<label for="ChiNhanhNH">Ngân hàng</label>
					<input type="text" class="form-control" id="ChiNhanhNH" maxlength="100" runat="server" placeholder="Nhập thông tin ngân hàng">
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
<%--                <asp:Button ID="SearchUserButton" runat="server" Text="Tra cứu" --%>
<%--                        ValidationGroup="RegisterUserValidationGroup" class="btn btn-primary" --%>
<%--                    onclick="RegisterUser_SearchUser"/>--%>
                <asp:Button ID="CreateUserButton" runat="server" Text="Cập nhật" 
                        ValidationGroup="RegisterUserValidationGroup" class="btn btn-primary" 
                    onclick="RegisterUser_CreatingUser"/>
                </div> 
            </div><!-- /.box -->
        </div><!-- /.box -->
    </div><!--/.col (left) -->

</asp:Content>
