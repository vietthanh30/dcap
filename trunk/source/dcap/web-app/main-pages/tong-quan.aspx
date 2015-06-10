<%@ Page Language="C#" MasterPageFile="~/Site.master" CodeBehind="tong-quan.aspx.cs" Inherits="web_app.main_pages.tong_quan" %>

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
    <div class="col-md-3 col-sm-6 col-xs-12">
        <div class="info-box">
        <span class="info-box-icon bg-aqua"><i class="ion ion-ios-people-outline"></i></span>
        <div class="info-box-content">
            <span class="info-box-text">Thành viên</span>
            <span class="info-box-number"><asp:Label ID="MemberAmount" runat="server"></asp:Label></span>
        </div><!-- /.info-box-content -->
        </div><!-- /.info-box -->
    </div><!-- /.col -->
    <!-- fix for small devices only -->
    <div class="clearfix visible-sm-block"></div>

    <div class="col-md-3 col-sm-6 col-xs-12">
        <div class="info-box">
        <span class="info-box-icon bg-green"><i class="ion ion-ios-cart-outline"></i></span>
        <div class="info-box-content">
            <span class="info-box-text">Tổng số ID</span>
            <span class="info-box-number"><asp:Label ID="AccountAmount" runat="server"></asp:Label></span>
        </div><!-- /.info-box-content -->
        </div><!-- /.info-box -->
    </div><!-- /.col -->
			
    <div class="col-md-3 col-sm-6 col-xs-12">
        <div class="info-box">
        <span class="info-box-icon bg-yellow"><i class="ion ion-ios-people-outline"></i></span>
        <div class="info-box-content">
            <span class="info-box-text">Quản lý</span>
            <span class="info-box-number"><asp:Label ID="ManagerAmount" runat="server"></asp:Label></span>
        </div><!-- /.info-box-content -->
        </div><!-- /.info-box -->
    </div><!-- /.col -->
			
	<div class="col-md-3 col-sm-6 col-xs-12">
        <div class="info-box">
        <span class="info-box-icon bg-yellow"><i class="ion ion-ios-people-outline"></i></span>
        <div class="info-box-content">
            <span class="info-box-text">Quản lý 6</span>
            <span class="info-box-number"><asp:Label ID="ManagerL6Amount" runat="server"></asp:Label></span>
        </div><!-- /.info-box-content -->
        </div><!-- /.info-box -->
    </div><!-- /.col -->
			
</div><!-- /.row -->

<!-- Main row -->
<div class="row">
	<div class="col-md-6">
		<div class="box box-danger">
			<div class="box-header with-border">
			<h3 class="box-title">Thành viên mới</h3>
			<div class="box-tools pull-right">
				<span class="label label-danger"><asp:Label ID="NewMemberAmount" runat="server"></asp:Label></span>
				<button class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
				<button class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
			</div>
			</div><!-- /.box-header -->
			<div class="box-body no-padding">
                <asp:Literal ID="NewMemberList" runat="server"></asp:Literal>
            </div><!-- /.box-body -->
		</div>
	</div>
                

    <div class="col-md-6">
        <!-- USERS LIST -->
        <div class="box box-danger">
            <div class="box-header with-border">
                <h3 class="box-title">Quản lý mới</h3>
                <div class="box-tools pull-right">
                <span class="label label-danger"><asp:Label ID="NewManagerAmount" runat="server"></asp:Label></span>
                <button class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                <button class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
                </div>
            </div><!-- /.box-header -->
            <div class="box-body no-padding">
                <asp:Literal ID="NewManagerList" runat="server"></asp:Literal>
            </div><!-- /.box-body -->
        </div><!--/.box -->
    </div><!-- /.col -->
</div><!-- /.row -->

<!-- TABLE: LATEST ORDERS -->
<div class="box box-info">
    <div class="box-header with-border">
        <h3 class="box-title">Bảng kê điểm thưởng hoa hồng năm <asp:Label ID="ReportYearLabel" runat="server"></asp:Label></h3>
        <div class="box-tools pull-right">
        <button class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
        <button class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
        </div>
    </div><!-- /.box-header -->
    <div class="box-body">
        <div class="table-responsive">
            <asp:Label ID="AccountBonusTable" runat="server"></asp:Label>
        </div><!-- /.table-responsive -->
    </div><!-- /.box-body -->
</div><!-- /.box -->
</asp:Content>
