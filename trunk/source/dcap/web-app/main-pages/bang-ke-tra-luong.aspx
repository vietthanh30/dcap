<%@ Page Title="Bảng lương tháng" Language="C#" MasterPageFile="~/Site.master" CodeBehind="bang-ke-tra-luong.aspx.cs" Inherits="web_app.main_pages.bang_ke_tra_luong" %>
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
    <script src="../plugins/datepicker/locales/bootstrap-datepicker.vi.js" type="text/javascript"></script>
    <script type="text/javascript">
        function invokeMeMaster() {
            $('[id$=ReportMonth]').datepicker({
                todayBtn: "linked",
                language: "vi",
                autoclose: true,
                todayHighlight: true,
                format: 'mm/yyyy'
            });
        }
    </script>
</asp:Content>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <h1>
    Bảng lương tháng
    </h1>
    <ol class="breadcrumb">
    <li><a href="#"><i class="fa fa-dashboard"></i> Trang chủ</a></li>
    <li class="active">Bảng lương tháng</li>
    </ol>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="row">
    <!-- left column -->
    <div class="col-xs-12">
        <!-- general form elements -->
        <div class="box box-primary">
            <div class="box-body">
			<div class="row">
				<div class="col-xs-4">
					<label for="BeginDate">Tháng kê khai</label>
                    <div class="input-group">
                      <div class="input-group-addon">
                        <i id="i1" class="fa fa-calendar" runat="server"></i>
                      </div>
					  <input type="text" class="form-control" runat="server" id="ReportMonth" placeholder="mm/yyyy">
                    </div><!-- /.input group -->
				</div>
			</div>
            </div><!-- /.box-body -->
				  
            <div class="box-footer">        
                <asp:Button ID="SearchButton" runat="server" Text="Tra cứu" class="btn btn-primary" 
                    onclick="BangKeTraLuong_SearchBangKe"/>
                <asp:Button ID="CapNhatButton" runat="server" Text="Cập nhật trả lương" class="btn btn-primary" 
                    onclick="BangKeTraLuong_PreUpdatePaid"/>
            </div>
        </div><!-- /.box -->

    </div><!--/.col (left) -->
    </div>

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
            <asp:GridView ID="gvBangKe" runat="server" AutoGenerateColumns="false" 
                EnableModelValidation="true" class="table table-bordered table-striped" 
                BorderColor="#CCCCCC" AllowPaging="True" 
                onpageindexchanging="gvBangKe_PageIndexChanging" PageSize="15" >
                <Columns>
                <asp:TemplateField HeaderText="Đã trả">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkRow" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="STT">
                    <ItemTemplate><%#GetStt() %></ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="HoTen" HeaderText="Họ tên" >
                <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="UserName" HeaderText="Tên đăng nhập" >
                <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="SoCmnd" HeaderText="Số CMND" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="DiaChi" HeaderText="Địa chỉ" >
                <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="SoTaiKhoan" HeaderText="Số TK" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="ChiNhanhNH" HeaderText="Ngân hàng" >
                <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="SoDienThoai" HeaderText="Số ĐT" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="HeThong" HeaderText="Hệ thống" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="QuanLy" HeaderText="Quản lý" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="ThuongThem" HeaderText="Thưởng thêm" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="SoTien" HeaderText="Tổng điểm" >
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="Thang" HeaderText="Tháng" >
                <ItemStyle HorizontalAlign="Center" />
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
	<asp:Button runat="server" class="btn btn-primary" Text="Xuất Excel file" 
            onclick="BangKeTraLuong_ExportExcel" />
    <asp:Button runat="server" class="btn btn-primary" Text="Xuất Word file" 
            onclick="BangKeTraLuong_ExportDOC" />
    </div>
    <ASPP:PopupPanel HeaderText="Cập nhật trả lương" ID="UpdatePaidPopup" runat="server" OnCloseWindowClick="OnClosePopupWindow">
    <PopupWindow>
        <ASPP:PopupWindow ID="UpdatePaidWindow" runat="server">
        <asp:Panel ID="UpdatePaidPanel" runat="server" BorderStyle="Ridge" style="width: 500px;">
        <div class="box-body">   
        <div class="row">
			<div class="col-xs-12">
            <asp:Label ID="UpdatePaidLabel" runat="server"></asp:Label>
				</div>
		</div>
                    
        </div><!-- /.box-body -->
				  
        <div class="box-footer">
            <asp:Button ID="AcceptUpdateButton" runat="server" Text="Đồng ý" 
                        class="btn btn-primary" 
                    onclick="BangKeTraLuong_UpdatePaid" />
            <asp:Button ID="CancelUpdateButton" runat="server" Text="Hủy bỏ" 
                        class="btn btn-primary" 
                    onclick="BangKeTraLuong_CancelUpdatePaid" />
        </div> 
        </asp:Panel>
        </ASPP:PopupWindow>
        </PopupWindow>
    </ASPP:PopupPanel>
</asp:Content>