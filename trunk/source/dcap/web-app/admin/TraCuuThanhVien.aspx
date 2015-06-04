<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TraCuuThanhVien.aspx.cs" Inherits="web_app.admin.TraCuuThanhVien" %>
<%@ Register Assembly="ASP.Web.UI.PopupControl" Namespace="ASP.Web.UI.PopupControl"
    TagPrefix="ASPP" %>

<asp:Content ID="Content5" ContentPlaceHolderID="HeadContent" runat="server">
    <h1>
    Tra cứu thành viên
    </h1>
    <ol class="breadcrumb">
    <li><a href="#"><i class="fa fa-dashboard"></i> Trang chủ</a></li>
    <li class="active">Tra cứu thành viên</li>
    </ol>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
    <!-- left column -->
    <div class="col-xs-12">
        <!-- general form elements -->
        <div class="box box-primary">
            <div class="box-body">
			<div class="row">
				<div class="col-xs-4">
					<label for="SoCmndSearch">Số CMND</label>
					<input type="text" class="form-control" runat="server" id="SoCmndSearch" placeholder="Nhập Số CMND">
				</div>
				<div class="col-xs-4">
					<label for="IdThanhVienSearch">Id thành viên</label>
					<input type="text" class="form-control" runat="server" id="IdThanhVienSearch" placeholder="Nhập Id thành viên">
				</div>
			</div>
			<div class="row">
				<div class="col-xs-8">
					<label for="HoTenSearch">Họ tên</label>
					<input type="text" class="form-control" runat="server" id="HoTenSearch" placeholder="Nhập Họ tên">
				</div>
			</div>
            </div><!-- /.box-body -->
				  
            <div class="box-footer">        
                <asp:Button ID="SearchButton" runat="server" Text="Tra cứu" 
                    class="btn btn-primary" onclick="TraCuuThanhVien_Search" 
                    />
            </div>
        </div><!-- /.box -->

    </div><!--/.col (left) -->
    </div>
		  
    <asp:HiddenField ID="ContinueDestinationPageUrl" runat="server" />  
    <div class="row">
		<div class="col-xs-8">
        <asp:Label ID="InvalidCredentialsMessage" runat="server" class="failureNotification" ForeColor="Blue"
            Text="" Visible="False"></asp:Label>
            </div>
    </div>   
    <div class="row">
    <div class="col-xs-12">
        <div class="box">
                
        <div class="box-body">
            <asp:GridView ID="gvMemberInfo" runat="server" AutoGenerateColumns="false" 
                EnableModelValidation="true" class="table table-bordered table-striped" 
                BorderColor="#CCCCCC" AllowPaging="True" 
                onpageindexchanging="gvMemberInfo_PageIndexChanging" PageSize="15" >
                <Columns>
                <asp:TemplateField HeaderText="STT">
                <ItemTemplate><%#GetStt() %></ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="FullName" HeaderText="Họ tên" >
                <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="SoCmnd" HeaderText="Số CMND" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="AccountNumber" HeaderText="Id Thành viên" DataFormatString="{0:0000000}" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="UserName" HeaderText="Tên đăng nhập" >
                <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Sửa">
                    <ItemTemplate>
                        <asp:ImageButton ID="imgBtnEditUser" runat="server" ImageUrl="~/dist/img/edit-icon.png"
                            OnClick="imgBtnEditUser_Click" CausesValidation="False" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Width="20px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Xóa">
                    <ItemTemplate>
                        <asp:ImageButton ID="imgBtnDeleteUser" runat="server" ImageUrl="~/dist/img/delete-icon.png"
                            OnClick="imgBtnDeleteUser_Click" CausesValidation="False" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Width="20px" />
                </asp:TemplateField>
                </Columns>
                <HeaderStyle BackColor="#CCCCCC" Font-Names="Arial" Font-Size="Small" HorizontalAlign="Center" />
                <PagerSettings Mode="NextPrevious" 
                    NextPageText="&amp;nbsp;&amp;nbsp;&amp;nbsp;Tiếp theo" PageButtonCount="6" 
                    PreviousPageText="Quay lại" />
                <RowStyle Font-Names="Arial" Font-Size="Small" HorizontalAlign="Left" VerticalAlign="Middle" />
            </asp:GridView>
        </div><!-- /.box-body -->
        </div><!-- /.box -->
    </div><!-- /.col -->
    </div><!-- /.row -->
	<div class="box-footer">
    <asp:Button ID="BtnExportExcel" runat="server" class="btn btn-primary" 
            Text="Xuất Excel file" onclick="TraCuuThanhVien_ExportToExcel" 
            />
    <asp:Button ID="BtnExportDoc" runat="server" class="btn btn-primary" 
            Text="Xuất Word file" onclick="TraCuuThanhVien_ExportToWord" 
            />
    </div>
    <ASPP:PopupPanel HeaderText="Sửa thông tin thành viên" ID="EditMemberPopup" runat="server" OnCloseWindowClick="OnClosePopupWindow">
    <PopupWindow>
    <ASPP:PopupWindow ID="EditPopupWindow" runat="server">
            <asp:Panel runat="server" BorderStyle="Ridge" style="width: 800px; height: 600px">
                <div class="box-body">   
                <div class="row">
					<div class="col-xs-8">
                    <asp:Label ID="Label1" runat="server" class="failureNotification"
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
					<label for="HoTen">Họ tên</label>
					<input type="text" class="form-control" readonly="true" id="HoTen" runat="server" placeholder="Nhập họ tên">
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
                      <input type="text" id="NgaySinh" runat="server" class="form-control" placeholder="dd/mm/yyyy" >
                    </div><!-- /.input group -->
					</div>
					<div class="col-xs-4">
					<label for="SoCmnd">Số CMND</label>
					<input type="text" class="form-control" readonly="true" id="SoCmnd" runat="server" placeholder="Nhập số CMND">
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
                        <input type="text" ID="NgayCap" runat="server" class="form-control" placeholder="dd/mm/yyyy" >
                    </div><!-- /.input group -->
					</div>
					<div class="col-xs-4">
					<label for="SoDienThoai">Số điện thoại</label>
					<input type="text" class="form-control" id="SoDienThoai" runat="server" placeholder="Nhập số điện thoại">
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
                <asp:Button ID="CreateUserButton" runat="server" Text="Cập nhật" 
                        ValidationGroup="RegisterUserValidationGroup" class="btn btn-primary" 
                    onclick="TraCuuThanhVien_EditUser" />
                <asp:Button ID="CancelEditButton" runat="server" Text="Hủy bỏ" 
                        class="btn btn-primary" 
                    onclick="OnClosePopupWindow" />
                </div> 
        </asp:Panel>
        </ASPP:PopupWindow>
        </PopupWindow>
    </ASPP:PopupPanel>
    
    <ASPP:PopupPanel HeaderText="Xóa thành viên" ID="DeleteMemberPopup" runat="server" OnCloseWindowClick="OnClosePopupWindow">
        <PopupWindow>
        <ASPP:PopupWindow ID="DeletePopupWindow" runat="server">
        <asp:Panel ID="DeleteMemberPanel" runat="server" BorderStyle="Ridge" style="width: 500px;">
        <div class="box-body">   
        <div class="row">
			<div class="col-xs-8">
            <asp:Label ID="DeleteMemberLabel" runat="server"></asp:Label>
				</div>
		</div>
                    
        </div><!-- /.box-body -->
				  
        <div class="box-footer">
            <asp:Button ID="AcceptDelButton" runat="server" Text="Đồng ý" 
                        class="btn btn-primary" 
                    onclick="TraCuuThanhVien_DeleteUser" />
            <asp:Button ID="CancelDelButton" runat="server" Text="Hủy bỏ" 
                        class="btn btn-primary" 
                    onclick="OnClosePopupWindow" />
        </div> 
        </asp:Panel>
        </ASPP:PopupWindow>
        </PopupWindow>
    </ASPP:PopupPanel>
    <asp:PlaceHolder ID="EditPlaceHolder" runat="server" Visible="false">
    <asp:Panel runat="server" style="height: 300px"></asp:Panel>
    </asp:PlaceHolder>
</asp:Content>
