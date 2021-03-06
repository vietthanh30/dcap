﻿<%@ Page Title="Bảng lương nâng cao" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BangLuongExt.aspx.cs" Inherits="web_app.main_pages.BangLuongExt" %>

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
            $('[id$=BeginDate]').datepicker({
                todayBtn: "linked",
                language: "vi",
                autoclose: true,
                todayHighlight: true,
                format: 'dd/mm/yyyy'
            });
            $('[id$=EndDate]').datepicker({
                todayBtn: "linked",
                language: "vi",
                autoclose: true,
                todayHighlight: true,
                format: 'dd/mm/yyyy'
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="HeadContent" runat="server">
    <h1>
    Bảng lương nâng cao
    </h1>
    <ol class="breadcrumb">
    <li><a href="#"><i class="fa fa-dashboard"></i> Trang chủ</a></li>
    <li class="active">Bảng lương nâng cao</li>
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
					<label for="IdMember">Id thành viên</label>
					<input type="text" class="form-control" maxlength="7" runat="server" id="IdMember" placeholder="Nhập Id thành viên">
				</div>
				<div class="col-xs-4">
					<label for="UserName">Tên đăng nhập</label>
					<input type="text" class="form-control" maxlength="50" runat="server" id="UserName" placeholder="Nhập tên đăng nhập">
				</div>
			</div>
			<div class="row">
				<div class="col-xs-4">
					<label for="BeginDate">Từ ngày</label>
                    <div class="input-group">
                      <div class="input-group-addon">
                        <i id="i2" class="fa fa-calendar" runat="server"></i>
                      </div>
					    <input type="text" class="form-control" runat="server" id="BeginDate" placeholder="dd/MM/yyyy">
                    </div><!-- /.input group -->
				</div>
				<div class="col-xs-4">
					<label for="EndDate">Đến ngày</label>
                    <div class="input-group">
                      <div class="input-group-addon">
                        <i id="i3" class="fa fa-calendar" runat="server"></i>
                      </div>
					  <input type="text" class="form-control" runat="server" id="EndDate" placeholder="dd/MM/yyyy">
                    </div><!-- /.input group -->
				</div>
			</div>
            </div><!-- /.box-body -->
				  
            <div class="box-footer">        
                <asp:Button ID="SearchButton" runat="server" Text="Tra cứu" class="btn btn-primary" 
                    onclick="BangKeTraLuong_SearchBangKe"/>
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
	<asp:Button ID="Button1" runat="server" class="btn btn-primary" Text="Xuất Excel file" 
            onclick="BangKeTraLuong_ExportExcel" />
    <asp:Button ID="Button2" runat="server" class="btn btn-primary" Text="Xuất Word file" 
            onclick="BangKeTraLuong_ExportDOC" />
    </div>
</asp:Content>
