<%@ Page Title="Tổng quan hệ thống" Language="C#" MasterPageFile="~/Site.master" CodeBehind="tong-quan.aspx.cs" Inherits="web_app.main_pages.tong_quan" %>

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
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <h1>
    Tổng quan hệ thống
    </h1>
    <ol class="breadcrumb">
    <li><a href="#"><i class="fa fa-dashboard"></i> Trang chủ</a></li>
    <li class="active">Tổng quan</li>
    </ol>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
        <!-- Info boxes -->
        <div class="row">
            <div class="col-md-6 col-sm-6 col-xs-12">
                <div class="info-box">
                    <span class="info-box-icon bg-green"><i class="ion ion-ios-cart-outline"></i></span>
                    <div class="info-box-content">
                        <span class="info-box-text">Tổng số ID</span> <span class="info-box-number">
                            <asp:Label ID="AccountAmount" runat="server"></asp:Label></span>
                    </div>
                    <!-- /.info-box-content -->
                </div>
                <!-- /.info-box -->
            </div>
            <!-- /.col -->
            <div class="col-md-6 col-sm-6 col-xs-12">
                <div class="info-box">
                    <span class="info-box-icon bg-yellow"><i class="ion ion-ios-people-outline"></i>
                    </span>
                    <div class="info-box-content">
                        <span class="info-box-text">Quản lý</span> <span class="info-box-number">
                            <asp:Label ID="ManagerAmount" runat="server"></asp:Label></span>
                    </div>
                    <!-- /.info-box-content -->
                </div>
                <!-- /.info-box -->
            </div>
        </div>
        <!-- /.row -->
        <!-- Main row -->
        <div class="row">
            <div class="col-md-12">
                <div class="box box-danger">
                    <div class="box-header with-border">
                        <h3 class="box-title">
                            Thành viên mới</h3>
                        <div class="box-tools pull-right">
                            <span class="label label-danger">
                                <asp:Label ID="NewMemberAmount" runat="server"></asp:Label></span>
                            <button class="btn btn-box-tool" data-widget="collapse">
                                <i class="fa fa-minus"></i>
                            </button>
                            <button class="btn btn-box-tool" data-widget="remove">
                                <i class="fa fa-times"></i>
                            </button>
                        </div>
                    </div>
                    <!-- /.box-header -->
                    <div class="box-body no-padding">
                        <asp:Literal ID="NewMemberList" runat="server"></asp:Literal>
                    </div>
                    <!-- /.box-body -->
                </div>
            </div>
        </div>
        <!-- /.row -->
        <!-- TABLE: LATEST ORDERS -->
        <%--<div class="box box-info">--%>
        <%--    <div class="box-header with-border">--%>
        <%--        <h3 class="box-title">Bảng kê điểm thưởng hoa hồng năm <asp:Label ID="ReportYearLabel" runat="server"></asp:Label></h3>--%>
        <%--        <div class="box-tools pull-right">--%>
        <%--        <button class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>--%>
        <%--        <button class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>--%>
        <%--        </div>--%>
        <%--    </div><!-- /.box-header -->--%>
        <%--    <div class="box-body">--%>
        <%--        <div class="table-responsive">--%>
        <%--            <asp:Label ID="AccountBonusTable" runat="server"></asp:Label>--%>
        <%--        </div><!-- /.table-responsive -->--%>
        <%--    </div><!-- /.box-body -->--%>
        <%--</div><!-- /.box -->--%>
</asp:Content>
