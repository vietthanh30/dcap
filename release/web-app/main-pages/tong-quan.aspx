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
            <span class="info-box-number">10.568</span>
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
            <span class="info-box-number">12.856</span>
        </div><!-- /.info-box-content -->
        </div><!-- /.info-box -->
    </div><!-- /.col -->
			
    <div class="col-md-3 col-sm-6 col-xs-12">
        <div class="info-box">
        <span class="info-box-icon bg-yellow"><i class="ion ion-ios-people-outline"></i></span>
        <div class="info-box-content">
            <span class="info-box-text">Quản lý</span>
            <span class="info-box-number">600</span>
        </div><!-- /.info-box-content -->
        </div><!-- /.info-box -->
    </div><!-- /.col -->
			
	<div class="col-md-3 col-sm-6 col-xs-12">
        <div class="info-box">
        <span class="info-box-icon bg-yellow"><i class="ion ion-ios-people-outline"></i></span>
        <div class="info-box-content">
            <span class="info-box-text">Quản lý 6</span>
            <span class="info-box-number">10</span>
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
				<span class="label label-danger">8 Thành viên</span>
				<button class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
				<button class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
			</div>
			</div><!-- /.box-header -->
			<div class="box-body no-padding">
                <ul class="users-list clearfix">
                <li>
                    <img src="~/dist/img/user1-128x128.jpg" runat="server" alt="User Image"/>
                    <a class="users-list-name" href="#">Alexander Pierce</a>
                    <span class="users-list-date">Today</span>
                </li>
                <li>
                    <img src="~/dist/img/user8-128x128.jpg" runat="server" alt="User Image"/>
                    <a class="users-list-name" href="#">Norman</a>
                    <span class="users-list-date">Yesterday</span>
                </li>
                <li>
                    <img src="~/dist/img/user7-128x128.jpg" runat="server" alt="User Image"/>
                    <a class="users-list-name" href="#">Jane</a>
                    <span class="users-list-date">12 Jan</span>
                </li>
                <li>
                    <img src="~/dist/img/user6-128x128.jpg" runat="server" alt="User Image"/>
                    <a class="users-list-name" href="#">John</a>
                    <span class="users-list-date">12 Jan</span>
                </li>
                <li>
                    <img src="~/dist/img/user2-160x160.jpg" runat="server" alt="User Image"/>
                    <a class="users-list-name" href="#">Alexander</a>
                    <span class="users-list-date">13 Jan</span>
                </li>
                <li>
                    <img src="~/dist/img/user5-128x128.jpg" runat="server" alt="User Image"/>
                    <a class="users-list-name" href="#">Sarah</a>
                    <span class="users-list-date">14 Jan</span>
                </li>
                <li>
                    <img src="~/dist/img/user4-128x128.jpg" runat="server" alt="User Image"/>
                    <a class="users-list-name" href="#">Nora</a>
                    <span class="users-list-date">15 Jan</span>
                </li>
                <li>
                    <img src="~/dist/img/user3-128x128.jpg" runat="server" alt="User Image"/>
                    <a class="users-list-name" href="#">Nadia</a>
                    <span class="users-list-date">15 Jan</span>
                </li>
                </ul><!-- /.users-list -->
            </div><!-- /.box-body -->
		</div>
	</div>
                

    <div class="col-md-6">
        <!-- USERS LIST -->
        <div class="box box-danger">
            <div class="box-header with-border">
                <h3 class="box-title">Quản lý mới</h3>
                <div class="box-tools pull-right">
                <span class="label label-danger">8 Quản lý</span>
                <button class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                <button class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
                </div>
            </div><!-- /.box-header -->
            <div class="box-body no-padding">
                <ul class="users-list clearfix">
                <li>
                    <img src="~/dist/img/user1-128x128.jpg" runat="server" alt="User Image"/>
                    <a class="users-list-name" href="#">Alexander Pierce</a>
                    <span class="users-list-date">Today</span>
                </li>
                <li>
                    <img src="~/dist/img/user8-128x128.jpg" runat="server" alt="User Image"/>
                    <a class="users-list-name" href="#">Norman</a>
                    <span class="users-list-date">Yesterday</span>
                </li>
                <li>
                    <img src="~/dist/img/user7-128x128.jpg" runat="server" alt="User Image"/>
                    <a class="users-list-name" href="#">Jane</a>
                    <span class="users-list-date">12 Jan</span>
                </li>
                <li>
                    <img src="~/dist/img/user6-128x128.jpg" runat="server" alt="User Image"/>
                    <a class="users-list-name" href="#">John</a>
                    <span class="users-list-date">12 Jan</span>
                </li>
                <li>
                    <img src="~/dist/img/user2-160x160.jpg" runat="server" alt="User Image"/>
                    <a class="users-list-name" href="#">Alexander</a>
                    <span class="users-list-date">13 Jan</span>
                </li>
                <li>
                    <img src="~/dist/img/user5-128x128.jpg" runat="server" alt="User Image"/>
                    <a class="users-list-name" href="#">Sarah</a>
                    <span class="users-list-date">14 Jan</span>
                </li>
                <li>
                    <img src="~/dist/img/user4-128x128.jpg" runat="server" alt="User Image"/>
                    <a class="users-list-name" href="#">Nora</a>
                    <span class="users-list-date">15 Jan</span>
                </li>
                <li>
                    <img src="~/dist/img/user3-128x128.jpg" runat="server" alt="User Image"/>
                    <a class="users-list-name" href="#">Nadia</a>
                    <span class="users-list-date">15 Jan</span>
                </li>
                </ul><!-- /.users-list -->
            </div><!-- /.box-body -->
        </div><!--/.box -->
    </div><!-- /.col -->
</div><!-- /.row -->

<!-- TABLE: LATEST ORDERS -->
<div class="box box-info">
    <div class="box-header with-border">
        <h3 class="box-title">Bảng kê điểm thưởng hoa hồng năm 2015</h3>
        <div class="box-tools pull-right">
        <button class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
        <button class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
        </div>
    </div><!-- /.box-header -->
    <div class="box-body">
        <div class="table-responsive">
        <table class="table no-margin">
            <thead>
            <tr>
                <th>Tháng</th>
                <th>Tổng số</th>
                <th>Trạng Thái</th> 
            </tr>
            </thead>
            <tbody>
            <tr>
                <td><a href="#">Tháng 6</a></td>
                <td>10.000</td>
                <td><span class="label label-warning">Chưa trả thưởng</span></td>
            </tr>
            <tr>
                <td><a href="#">Tháng 5</a></td>
                <td>8000</td>
                <td><span class="label label-warning">Đang xét duyệt</span></td>
            </tr>
            <tr>
                <td><a href="#">Tháng 4</a></td>
                <td>5000</td>
                <td><span class="label label-success">Đã trả thưởng</span></td>
            </tr>
            <tr>
                <td><a href="#">Tháng 3</a></td>
                <td>5000</td>
                <td><span class="label label-success">Đã trả thưởng</span></td>
            </tr>
            <tr>
                <td><a href="#">Tháng 2</a></td>
                <td>2000</td>
                <td><span class="label label-success">Đã trả thưởng</span></td>
            </tr>
            <tr>
                <td><a href="#">Tháng 1</a></td>
                <td>1000</td>
                <td><span class="label label-success">Đã trả thưởng</span></td>
            </tr>
                        
            </tbody>
        </table>
        </div><!-- /.table-responsive -->
    </div><!-- /.box-body -->
</div><!-- /.box -->
</asp:Content>
