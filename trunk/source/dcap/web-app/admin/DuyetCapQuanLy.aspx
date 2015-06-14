<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DuyetCapQuanLy.aspx.cs" Inherits="web_app.admin.DuyetCapQuanLy" %>
<%@ Register Assembly="ASP.Web.UI.PopupControl" Namespace="ASP.Web.UI.PopupControl"
    TagPrefix="ASPP" %>
<asp:Content ID="Content5" ContentPlaceHolderID="HeadContent" runat="server">
    <h1>
    Duyệt cấp quản lý
    </h1>
    <ol class="breadcrumb">
    <li><a href="#"><i class="fa fa-dashboard"></i> Trang chủ</a></li>
    <li class="active">Duyệt cấp quản lý</li>
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
				<div class="col-xs-8">
                <span class="failureNotification">
                    <asp:Literal ID="ErrorMessage" runat="server"></asp:Literal>
                </span>
                <asp:ValidationSummary ID="ManagerApprovalValidationGroup" runat="server" CssClass="failureNotification" 
                        ValidationGroup="ManagerApprovalValidationGroup"/>  
                </div>
            </div>
			<div class="row">
				<div class="col-xs-4">
				<label for="CapQuanLySearch">Cấp quản lý</label>
				<asp:DropDownList ID="CapQuanLySearch" runat="server" class="form-control">
                    <asp:ListItem Value="2" Text="Cấp 2" Selected="True"></asp:ListItem>
                    <asp:ListItem Value="3" Text="Cấp 3"></asp:ListItem>
                    <asp:ListItem Value="4" Text="Cấp 4"></asp:ListItem>
                    <asp:ListItem Value="5" Text="Cấp 5"></asp:ListItem>
                    <asp:ListItem Value="6" Text="Cấp 6"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="CapQuanLySearchRequired" runat="server" ControlToValidate="CapQuanLySearch" 
                    CssClass="failureNotification" ErrorMessage="Cấp quản lý bắt buộc nhập." ToolTip="Cấp quản lý bắt buộc nhập." 
                    ValidationGroup="ManagerApprovalValidationGroup">*</asp:RequiredFieldValidator>
				</div>
				<div class="col-xs-4">
					<label for="IdThanhVienSearch">Id thành viên</label>
					<input type="text" class="form-control" runat="server" id="IdThanhVienSearch" placeholder="Nhập Id thành viên">
				</div>
			</div>
            </div><!-- /.box-body -->
				  
            <div class="box-footer">        
                <asp:Button ID="SearchButton" runat="server" Text="Tra cứu" 
                    class="btn btn-primary" onclick="DuyetCapQuanLy_Search" ValidationGroup="ManagerApprovalValidationGroup"
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
            <asp:GridView ID="gvManagerApproval" runat="server" AutoGenerateColumns="false" 
                EnableModelValidation="true" class="table table-bordered table-striped" 
                BorderColor="#CCCCCC" AllowPaging="True" 
                onpageindexchanging="gvManagerApproval_PageIndexChanging" PageSize="15" >
                <Columns>
                <asp:TemplateField HeaderText="STT">
                <ItemTemplate><%#GetStt() %></ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="ManagerLevel" HeaderText="Cấp quản lý" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="UserName" HeaderText="Tên đăng nhập" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="AccountNumber" HeaderText="Id Thành viên" DataFormatString="{0:0000000}" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Duyệt">
                    <ItemTemplate>
                        <asp:ImageButton ID="imgBtnApprove" runat="server" ImageUrl="~/dist/img/approve-icon.png"
                            OnClick="imgBtnApprove_Click" CausesValidation="False" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Width="20px" />
                </asp:TemplateField>
                </Columns>
                <HeaderStyle BackColor="#CCCCCC" Font-Names="Arial" Font-Size="Small" HorizontalAlign="Center" />
                <PagerSettings Mode="NumericFirstLast" 
                    NextPageText="" PageButtonCount="5" 
                    PreviousPageText="" FirstPageText="Đầu" LastPageText="Cuối" />
                <PagerStyle CssClass="GridPager" HorizontalAlign="Right" />
                <RowStyle Font-Names="Arial" Font-Size="Small" HorizontalAlign="Left" VerticalAlign="Middle" />
            </asp:GridView>
        </div><!-- /.box-body -->
        </div><!-- /.box -->
    </div><!-- /.col -->
    </div><!-- /.row -->
    <ASPP:PopupPanel HeaderText="Duyệt cấp quản lý" ID="ManagerApprovalPopup" runat="server" OnCloseWindowClick="OnClosePopupWindow">
        <PopupWindow>
        <ASPP:PopupWindow ID="ApprovalPopupWindow" runat="server">
        <asp:Panel ID="ManagerApprovalPanel" runat="server" BorderStyle="Ridge" style="width: 500px;">
        <div class="box-body">   
        <div class="row">
			<div class="col-xs-8">
            <asp:Label ID="ManagerApprovalLabel" runat="server"></asp:Label>
			<asp:HiddenField ID="ManagerLevelApproval" runat="server" />
			<asp:HiddenField ID="AccountNumberApproval" runat="server" />
			</div>
		</div>
                    
        </div><!-- /.box-body -->
				  
        <div class="box-footer">
            <asp:Button ID="AcceptApprovalButton" runat="server" Text="Đồng ý" 
                        class="btn btn-primary" 
                    onclick="DuyetCapQuanLy_ApproveManager" />
            <asp:Button ID="CancelApprovalButton" runat="server" Text="Hủy bỏ" 
                        class="btn btn-primary" 
                    onclick="OnClosePopupWindow" />
        </div> 
        </asp:Panel>
        </ASPP:PopupWindow>
        </PopupWindow>
    </ASPP:PopupPanel>
</asp:Content>
