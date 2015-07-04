<%@ Page Title="Thưởng thêm" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewBonusAdd.aspx.cs" Inherits="web_app.admin.NewBonusAdd" %>
<%@ Register Assembly="ASP.Web.UI.PopupControl" Namespace="ASP.Web.UI.PopupControl"
    TagPrefix="ASPP" %>

<asp:Content ID="HeaderContent1" runat="server" ContentPlaceHolderID="HeadContent1">
	<!-- jQuery 2.1.4 -->
	<script src="../plugins/jQuery/jQuery-2.1.4.min.js"></script>
	<!-- Bootstrap 3.3.2 JS -->
	<script src="../bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
	<!-- FastClick -->
	<script src='../plugins/fastclick/fastclick.min.js'></script>
	<!-- AdminLTE App -->
	<script src="../dist/js/app.min.js" type="text/javascript"></script>
	<!-- Sparkline -->
	<script src="../plugins/sparkline/jquery.sparkline.min.js" type="text/javascript"></script>
	<!-- jvectormap -->
	<script src="../plugins/jvectormap/jquery-jvectormap-1.2.2.min.js" type="text/javascript"></script>
	<script src="../plugins/jvectormap/jquery-jvectormap-world-mill-en.js" type="text/javascript"></script>
	<!-- SlimScroll 1.3.0 -->
	<script src="../plugins/slimScroll/jquery.slimscroll.min.js" type="text/javascript"></script>
	<!-- ChartJS 1.0.1 -->
	<script src="../plugins/chartjs/Chart.min.js" type="text/javascript"></script>

	<!-- AdminLTE dashboard demo (This is only for demo purposes) -->
	<script src="../dist/js/pages/dashboard2.js" type="text/javascript"></script>

	<!-- AdminLTE for demo purposes -->
	<script src="../dist/js/demo.js" type="text/javascript"></script>
	
	<!-- date-picker -->
	<script src="../plugins/datepicker/bootstrap-datepicker.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="HeadContent" runat="server">
    <h1>
    Thưởng thêm
    </h1>
    <ol class="breadcrumb">
    <li><a href="#"><i class="fa fa-dashboard"></i> Trang chủ</a></li>
    <li class="active">Thưởng thêm</li>
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
					<label for="IdThanhVienSearch">Id thành viên</label>
					<input type="text" class="form-control" maxlength="7" runat="server" id="IdThanhVienSearch" placeholder="Nhập Id thành viên">
				</div>
				<div class="col-xs-4">
					<label for="UserNameSearch">Tên đăng nhập</label>
					<input type="text" class="form-control" runat="server" maxlength="50" id="UserNameSearch" placeholder="Nhập Tên đăng nhập">
				</div>
				<div class="col-xs-4">
					<label for="IsApprovedSearch">Trạng thái</label>
					<asp:DropDownList ID="IsApprovedSearch" runat="server" class="form-control">
						<asp:ListItem Value="" Text="- Tất cả -" Selected="True"></asp:ListItem>
						<asp:ListItem Value="N" Text="Chưa duyệt"></asp:ListItem>
						<asp:ListItem Value="Y" Text="Đã duyệt"></asp:ListItem>
					</asp:DropDownList>
				</div>
			</div>
            </div><!-- /.box-body -->
				  
            <div class="box-footer">        
                <asp:Button ID="SearchButton" runat="server" Text="Tra cứu" 
                    class="btn btn-primary" onclick="NewBonusAdd_Search" 
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
            <asp:GridView ID="gvBonusApproval" runat="server" AutoGenerateColumns="false" 
                EnableModelValidation="true" class="table table-bordered table-striped" 
                BorderColor="#CCCCCC" AllowPaging="True" 
                onpageindexchanging="gvBonusApproval_PageIndexChanging" PageSize="15" >
                <Columns>
                <asp:TemplateField HeaderText="STT">
                <ItemTemplate><%#GetStt() %></ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="AccountNumber" HeaderText="Id Thành viên" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="UserName" HeaderText="Tên đăng nhập" >
                <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="BonusAmount" HeaderText="Điểm thưởng" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="IsApproved" HeaderText="Trạng thái" >
                <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                </Columns>
                <HeaderStyle BackColor="#CCCCCC" Font-Names="Arial" Font-Size="Small" HorizontalAlign="Center" />
                <PagerSettings Mode="NumericFirstLast" 
                    NextPageText="" PageButtonCount="5" 
                    PreviousPageText="" FirstPageText="Đầu" LastPageText="Cuối" />
                <PagerStyle CssClass="GridPager" HorizontalAlign="Left" />
                <RowStyle Font-Names="Arial" Font-Size="Small" HorizontalAlign="Left" VerticalAlign="Middle" />
            </asp:GridView>
        </div><!-- /.box-body -->
        </div><!-- /.box -->
    </div><!-- /.col -->
    </div><!-- /.row -->
	<div class="box-footer">
    <asp:Button ID="BtnAddNewBonus" runat="server" class="btn btn-primary" 
            Text="Thêm mới" onclick="NewBonusAdd_PreAddNewBonus" 
            />
    </div>
    <ASPP:PopupPanel HeaderText="Thêm mới Thưởng thêm" ID="NewBonusAddPopup" runat="server" OnCloseWindowClick="OnClosePopupWindow">
    <PopupWindow>
    <ASPP:PopupWindow ID="NewBonusAddWindow" runat="server">
            <asp:Panel runat="server" BorderStyle="Ridge" style="width: 600px; height: 300px">
            <div class="row">
            <!-- left column -->
            <div class="col-xs-12">
            <!-- general form elements -->
            <div class="box box-primary">
                <div class="box-body">   
                <div class="row">
					<div class="col-xs-10">
                    <asp:Label ID="InvalidCredentialsMessage2" runat="server" class="failureNotification" ForeColor="Red"
                     Text="" Visible="False"></asp:Label>
                     </div>
                </div>          
                <div class="row">
					<div class="col-xs-10">
                    <span class="failureNotification">
                        <asp:Literal ID="ErrorMessage" runat="server"></asp:Literal>
                    </span>
                    <asp:ValidationSummary ID="NewBonusAddValidationSummary" runat="server" CssClass="failureNotification" 
                            ValidationGroup="NewBonusAddValidationGroup"/>  
                    </div>
                </div>          
                <div class="row">
					<div class="col-xs-10">
                    <asp:Label ID="AccountCode" runat="server" Text="" ForeColor="Blue" Visible="False"></asp:Label>
                    </div>
                </div>
				<div class="row">
					<div class="col-xs-5">
					<label for="AccountNumber">Id thành viên</label>
					<input type="text" class="form-control" maxlength="7" id="AccountNumber" runat="server" placeholder="Nhập ID thành viên">
                    <asp:RequiredFieldValidator ID="AccountNumberRequired" runat="server" ControlToValidate="AccountNumber" 
                        CssClass="failureNotification" ErrorMessage="Id thành viên bắt buộc nhập." ToolTip="Id thành viên bắt buộc nhập." 
                        ValidationGroup="NewBonusAddValidationGroup">*</asp:RequiredFieldValidator>
					</div>
					<div class="col-xs-5">
					<label for="BonusAmount">Điểm thưởng</label>
					<input type="text" class="form-control" maxlength="10" id="BonusAmount" runat="server" placeholder="Nhập Điểm thưởng">
                    <asp:RequiredFieldValidator ID="BonusAmountRequired" runat="server" ControlToValidate="BonusAmount" 
                        CssClass="failureNotification" ErrorMessage="Điểm thưởng bắt buộc nhập." ToolTip="Điểm thưởng bắt buộc nhập." 
                        ValidationGroup="NewBonusAddValidationGroup">*</asp:RequiredFieldValidator>
					</div>
				</div>                    
                </div><!-- /.box-body -->
				  
                <div class="box-footer">
                <asp:Button ID="AddNewBonusButton" runat="server" Text="Cập nhật" 
                        ValidationGroup="NewBonusAddValidationGroup" class="btn btn-primary" 
                    onclick="NewBonusAdd_AddNewBonus" />
                <asp:Button ID="CancelNewBonusButton" runat="server" Text="Hủy bỏ" 
                        class="btn btn-primary" 
                    onclick="OnClosePopupWindow" />
                </div> 
            </div><!-- /.box -->
            </div><!-- /.box -->
            </div><!--/.col (left) -->
        </asp:Panel>
        </ASPP:PopupWindow>
        </PopupWindow>
    </ASPP:PopupPanel>
</asp:Content>
