﻿<%@ Page Title="Thông tin cá nhân" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="EditMemberInfo.aspx.cs" Inherits="web_app.members.EditMemberInfo" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <h1>
    Thông tin thành viên
    </h1>
    <ol class="breadcrumb">
    <li><a href="#"><i class="fa fa-dashboard"></i> Trang chủ</a></li>
    <li class="active">Thông tin thành viên</li>
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
                    <asp:Label ID="InvalidCredentialsMessage" runat="server" class="failureNotification"
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
					<input type="text" class="form-control" readonly="true" id="DirectParentId" runat="server">
					</div>
					<div class="col-xs-4">
					<label for="ParentId">Tuyến trên</label>
					<input type="text" class="form-control" readonly="true" id="ParentId" runat="server">
					</div>
				</div>
				<div class="row">
					<div class="col-xs-4">
					<label for="HoTen">Họ tên</label>
					<input type="text" class="form-control" readonly="true" id="HoTen" runat="server">
					</div>
				</div>
				<div class="row">
					<div class="col-xs-4">
					<label for="NgaySinh">Ngày sinh</label>
                    <div class="input-group">
                      <div class="input-group-addon">
                        <i id="imgNgaySinh" class="fa fa-calendar" runat="server"></i>
                      </div>
                      <input type="text" id="NgaySinh" readonly="true" runat="server" class="form-control" >
                    </div><!-- /.input group -->
					</div>
					<div class="col-xs-4">
					<label for="SoCmnd">Số CMND</label>
					<input type="text" class="form-control" id="SoCmnd" readonly="true" runat="server">
					</div>
				</div>
				<div class="row">
                    <div class="col-xs-4">
					<label for="NgayCap">Ngày cấp</label>
                    <div class="input-group">
                      <div class="input-group-addon">
                        <i id="i1" class="fa fa-calendar" runat="server"></i>
                      </div>
                        <input type="text" ID="NgayCap" readonly="true" runat="server" class="form-control" >
                    </div><!-- /.input group -->
					</div>
					<div class="col-xs-4">
					<label for="SoDienThoai">Số điện thoại</label>
					<input type="text" class="form-control" id="SoDienThoai" readonly="true" runat="server">
					</div>
				</div>
				<div class="row">
					<div class="col-xs-2">
					<label for="GioiTinh">Giới tính</label>
                    <asp:RadioButtonList ID="GioiTinh" runat="server" Enabled="false" class="form-control" RepeatDirection="Horizontal" RepeatLayout="Table">
                        <asp:ListItem Text="Nam" Value="M" />
                        <asp:ListItem Text="Nữ" Value="F" />
                    </asp:RadioButtonList>
					</div>
					<div class="col-xs-6">
					<label for="DiaChi">Địa chỉ</label>
					<input type="text" class="form-control" readonly="true" id="DiaChi" runat="server">
					</div>
				</div>
				<div class="row">
					<div class="col-xs-2">
					<label for="SoTaiKhoan">Số tài khoản</label>
					<input type="text" class="form-control" readonly="true" id="SoTaiKhoan" runat="server">
					</div>
					<div class="col-xs-6">
					<label for="ChiNhanhNH">Ngân hàng</label>
					<input type="text" class="form-control" readonly="true" id="ChiNhanhNH" runat="server">
					</div>
				</div>
<%--					--%>
<%--				<div class="row">--%>
<%--					<div class="col-xs-6">--%>
<%--					<label for="exampleInputFile">Ảnh</label>--%>
<%--					<input type="file" runat="server" id="filePhotoUpload">--%>
<%--					<p class="help-block">Chọn ảnh chân dung</p>--%>
<%--					</div>--%>
<%--				</div>--%>
<%--                    --%>
<%--                </div><!-- /.box-body -->--%>
<%--				  --%>
<%--                <div class="box-footer">--%>
<%--                <asp:Button ID="CreateUserButton" runat="server" Text="Cập nhật" --%>
<%--                        ValidationGroup="RegisterUserValidationGroup" class="btn btn-primary" --%>
<%--                    onclick="RegisterUser_CreatingUser"/>--%>
<%--                </div> --%>
            </div><!-- /.box -->
        </div><!-- /.box -->
    </div><!--/.col (left) -->

</asp:Content>
