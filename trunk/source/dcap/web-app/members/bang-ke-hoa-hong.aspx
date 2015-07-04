<%@ Page Title="Tra cứu bảng lương" Language="C#" MasterPageFile="~/Site.master" CodeBehind="bang-ke-hoa-hong.aspx.cs" Inherits="web_app.members.bang_ke_hoa_hong" %>

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
    Tra cứu bảng lương
    </h1>
    <ol class="breadcrumb">
    <li><a href="#"><i class="fa fa-dashboard"></i> Trang chủ</a></li>
    <li class="active">Bảng lương</li>
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
                    onclick="BangKeHoaHong_SearchBangKe"/>
            </div>
        </div><!-- /.box -->

    </div><!--/.col (left) -->
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
                <asp:TemplateField HeaderText="STT">
                <ItemTemplate><%#GetStt() %></ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="AccountId" HeaderText="Id thành viên" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="TrucTiep" HeaderText="Trực tiếp" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="CanCap" HeaderText="Cân cặp" >
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
                <asp:BoundField DataField="Thang" HeaderText="Tháng" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Tong" HeaderText="Tổng" >
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
</asp:Content>