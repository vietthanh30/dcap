<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Register.aspx.cs" Inherits="web_app.admin.Register" %>
<%@ Register Assembly="SlimeeLibrary" Namespace="SlimeeLibrary" TagPrefix="cc1" %>

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
                    <cc1:datepicker ID="NgaySinh" runat="server" class="form-control" placeholder="Nhập ngày sinh" Width="270px" PaneWidth="250px" Height="20px"
                    BorderColor="#D3D3D3" EnableViewState="true" >
                                <PaneTableStyle BorderColor="#707070" BorderWidth="1px" BorderStyle="Solid" />
                                <PaneHeaderStyle BackColor="#0099FF" />
                                <TitleStyle ForeColor="White" Font-Bold="true" />
                                <NextPrevMonthStyle ForeColor="White" Font-Bold="true" />
                                <NextPrevYearStyle ForeColor="#E0E0E0" Font-Bold="true" />
                                <DayHeaderStyle BackColor="#E8E8E8" />
                                <TodayStyle BackColor="#FFFFCC" ForeColor="#000000" Font-Underline="false" BorderColor="#FFCC99"/>
                                <AlternateMonthStyle BackColor="#F0F0F0" ForeColor="#707070" Font-Underline="false"/>
                                <MonthStyle BackColor="" ForeColor="#000000" Font-Underline="false"/>
                            </cc1:datepicker>
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
                    <div class="col-xs-4">
					<label for="NgayCap">Ngày cấp</label>
                    <cc1:datepicker ID="NgayCap" runat="server" class="form-control" placeholder="Nhập ngày cấp" Width="270px" PaneWidth="250px" Height="20px"
                    BorderColor="#D3D3D3" EnableViewState="true" >
                                <PaneTableStyle BorderColor="#707070" BorderWidth="1px" BorderStyle="Solid" />
                                <PaneHeaderStyle BackColor="#0099FF" />
                                <TitleStyle ForeColor="White" Font-Bold="true" />
                                <NextPrevMonthStyle ForeColor="White" Font-Bold="true" />
                                <NextPrevYearStyle ForeColor="#E0E0E0" Font-Bold="true" />
                                <DayHeaderStyle BackColor="#E8E8E8" />
                                <TodayStyle BackColor="#FFFFCC" ForeColor="#000000" Font-Underline="false" BorderColor="#FFCC99"/>
                                <AlternateMonthStyle BackColor="#F0F0F0" ForeColor="#707070" Font-Underline="false"/>
                                <MonthStyle BackColor="" ForeColor="#000000" Font-Underline="false"/>
                            </cc1:datepicker>
					</div>
					<div class="col-xs-4">
					<label for="SoDienThoai">Số điện thoại</label>
					<input type="text" class="form-control" id="SoDienThoai" runat="server" placeholder="Nhập số điện thoại">
					</div>
				</div>
				<div class="row">
					<div class="col-xs-2">
					<label for="GioiTinh">Giới tính</label>
                    <asp:RadioButtonList ID="GioiTinh" runat="server" class="form-control" RepeatDirection="Horizontal" RepeatLayout="Table" placeholder="Nhập giới tính">
                        <asp:ListItem Text="Nam" Value="M" />
                        <asp:ListItem Text="Nữ" Value="F" />
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="GioiTinhRequired" runat="server" ControlToValidate="GioiTinh" 
                        CssClass="failureNotification" ErrorMessage="Giới tính bắt buộc nhập." ToolTip="Giới tính bắt buộc nhập." 
                        ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
					</div>
					<div class="col-xs-6">
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
