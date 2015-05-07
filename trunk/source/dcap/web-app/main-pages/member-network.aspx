<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="member-network.aspx.cs" Inherits="web_app.member_network" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>HITECH | Dashboard</title>
    <meta content='width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no' name='viewport'>
    <!-- Bootstrap 3.3.4 -->
    <link href="../bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <!-- Font Awesome Icons -->
    <link href="https://maxcdn.bootstrapcdn.com/font-awesome/4.3.0/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <!-- Ionicons -->
    <link href="https://code.ionicframework.com/ionicons/2.0.1/css/ionicons.min.css" rel="stylesheet" type="text/css" />
    <!-- jvectormap -->
    <link href="../plugins/jvectormap/jquery-jvectormap-1.2.2.css" rel="stylesheet" type="text/css" />
    <!-- Theme style -->
    <link href="../dist/css/AdminLTE.min.css" rel="stylesheet" type="text/css" />
    <!-- AdminLTE Skins. Choose a skin from the css/skins
         folder instead of downloading all of them to reduce the load. -->
    <link href="../dist/css/skins/_all-skins.min.css" rel="stylesheet" type="text/css" />
    
    <!-- date picker -->
    <link href="../plugins/datepicker/datepicker3.css" rel="stylesheet" type="text/css" />

    <!-- GridPager -->
    <link href="../Styles/GridPager.css" rel="stylesheet" type="text/css" />

    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/html5shiv/3.7.2/html5shiv.min.js"></script>
        <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
    <![endif]-->
	
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

        <script src="../plugins/jQuery/jQuery-2.1.4.min.js"></script>
        <script type="text/javascript" src="../Scripts/excanvas.js"></script>
        <script type="text/javascript" src="../Scripts/jit.js"></script>
        <script type="text/javascript" src="../Scripts/jquery.spacetree.js"></script>
        <script type="text/javascript">
            $(document).ready(function () {
                $('#tree').spacetree('#spacetree').hide();
            });
        </script>
        <link type="text/css" rel="stylesheet" href="../Styles/jquery.spacetree.css" media="screen" />
</head>
<body class="skin-blue sidebar-mini">
    <form id="form1" runat="server">
    <div class="wrapper">
        <header class="main-header">
			<!-- Logo -->
			<a id="A1" href="#" runat="server" class="logo">
			  <!-- mini logo for sidebar mini 50x50 pixels -->
			  <span class="logo-mini"><b>A</b>LT</span>
			  <!-- logo for regular state and mobile devices -->
			  <span class="logo-lg"><b>HITECH</b></span>
			</a>
            
			<!-- Header Navbar: style can be found in header.less -->
			<nav class="navbar navbar-static-top" role="navigation">
			  <!-- Sidebar toggle button-->
			  <a id="A2" href="#" runat="server" class="sidebar-toggle" data-toggle="offcanvas" role="button">
				<span class="sr-only">Toggle navigation</span>
			  </a>		  			
			      <!-- Navbar Right Menu -->
			      <div class="navbar-custom-menu">	
                        <ul class="nav navbar-nav">
	                        <!-- User Account: style can be found in dropdown.less -->
	                        <li class="dropdown user user-menu">
	                        <a id="A3" href="#" runat="server" class="dropdown-toggle" data-toggle="dropdown">
		                        <img ID="HeadLoginImg1" src="" runat="server" class="user-image" alt="User Image"/>
		                        <span class="hidden-xs"><asp:Label ID="HeadLoginName1" runat="server" /></span>
	                        </a>
                            <ul class="dropdown-menu">
                                <!-- User image -->
                                <li class="user-header">
                                    <img ID="HeadLoginImg2" src="" runat="server" class="img-circle" alt="User Image" />
                                    <p>
	                                    <asp:Label ID="HeadLoginName2" runat="server" />
                                    </p>
                                </li>
					  
                                <asp:LoginView ID="HeadLoginView" runat="server" EnableViewState="false">
                                <LoggedInTemplate>
                                <!-- Menu Footer-->
                                <li class="user-footer">
                                    <div class="pull-left">
	                                    <a href="~/admin/ChangePassword.aspx" runat="server" ID="HeadChangePassword" class="btn btn-default btn-flat">Đổi mật khẩu</a>
                                    </div>
                                    <div class="pull-right">
	                                    <asp:LoginStatus ID="HeadLoginStatus" runat="server" class="btn btn-default btn-flat" LogoutAction="Redirect" LogoutText="Đăng xuất" LogoutPageUrl="~/"/>
                                    </div>
                                </li>
                                </LoggedInTemplate>
                                </asp:LoginView>
                            </ul>
                        </li>
                        </ul>
                        		
			     </div>

			</nav>
        </header> <!-- /.content-main-header -->

             <div class="left-side">
                <!-- sidebar: style can be found in sidebar.less -->
                <section class="sidebar">
                <div class="user-panel">
                    <div class="pull-left image">
                        <img ID="HeadLoginImg3" src="" runat="server" class="img-circle" alt="User Image" />
                    </div>
                    <div class="pull-left info">
                        <p><asp:Label ID="HeadLoginName3" runat="server" /></p>
                    </div>
                </div>
                
            <asp:Panel ID="LeftContentAdmin" runat="server">
              <ul class="sidebar-menu">
                <li class="header"></li>
                <li>
                  <a id="A4" href="~/main-pages/tong-quan.aspx" runat="server">
			        <i class="fa fa-files-o"></i>
                    <span>Tổng quan</span>
                  </a>
                </li>
                <li>
                  <a id="A5" href="~/admin/Register.aspx" runat="server">
                    <i class="fa fa-edit"></i> <span>Đăng ký thành viên</span>
                  </a>
                </li>
                <li>
                  <a id="A6" href="~/admin/TraCuuThanhVien.aspx" runat="server">
                    <i class="fa fa-edit"></i> <span>Tra cứu thành viên</span>
                  </a>
                </li>
                <li>
                  <a id="A7" href="~/admin/Register2.aspx" runat="server">
                    <i class="fa fa-edit"></i> <span>Đăng ký quản lý</span>
                  </a>
                </li>
			    <li>
                  <a id="A8" href="~/main-pages/member-network.aspx" runat="server">
                    <i class="fa fa-calendar"></i> <span>Mạng lưới thành viên</span>
                  </a>
                </li>
                <li>
                  <a id="A9" href="~/main-pages/bang-ke-tra-luong.aspx" runat="server">
                    <i class="fa fa-table"></i> <span>Bảng kê trả lương</span>
                  </a>
                </li>
              </ul>	 
            </asp:Panel>

            <asp:Panel ID="LeftContentKT" runat="server">
              <ul class="sidebar-menu">
                <li class="header"></li>
			    <li>
                  <a id="A10" href="~/main-pages/member-network.aspx" runat="server">
                    <i class="fa fa-calendar"></i> <span>Mạng lưới thành viên</span>
                  </a>
                </li>
                <li>
                  <a id="A11" href="~/main-pages/bang-ke-tra-luong.aspx" runat="server">
                    <i class="fa fa-table"></i> <span>Bảng kê trả lương</span>
                  </a>
                </li>
              </ul>	  
            </asp:Panel>
       
            <asp:Panel ID="LeftContentTV" runat="server">
              <ul class="sidebar-menu">
                <li class="header"></li>
                <li>
                  <a id="A12" href="~/main-pages/tong-quan.aspx" runat="server">
			        <i class="fa fa-files-o"></i>
                    <span>Tổng quan</span>
                  </a>
                </li>
                <li>
                  <a id="A13" href="~/members/EditMemberInfo.aspx" runat="server">
			        <i class="fa fa-files-o"></i>
                    <span>Thông tin cá nhân</span>
                  </a>
                </li>
			    <li>
                  <a id="A14" href="~/main-pages/member-network.aspx" runat="server">
                    <i class="fa fa-calendar"></i> <span>Mạng lưới thành viên</span>
                  </a>
                </li>
              </ul>	 
            </asp:Panel>
       
            </section>
            <!-- /.sidebar --> 
             </div>  
       
      <!-- Content Wrapper. Contains page content -->
      <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
        </section>

        <!-- Main content -->
        <section class="content"> 
            <div class="row">
                <!-- left column -->
                <div class="col-xs-12">
                    <!-- general form elements -->
                    <div class="box box-primary">
                        <div class="box-body">
			            <div class="row">
				            <div class="col-xs-4">
				            <label for="IdMember">ID thành viên</label>
				            <input type="text" class="form-control" id="IdMember" runat="server" placeholder="Nhập ID thành viên">
				            </div>
			            </div>
                        </div><!-- /.box-body -->
				  
                        <div class="box-footer">            
                            <asp:Button ID="SearchButton" runat="server" Text="Tra cứu" class="btn btn-primary" 
                                onclick="MemberNetwork_SearchNetwork"/>
                        </div>
                    </div><!-- /.box -->

                </div><!--/.col (left) -->
         
	            <!-- right column -->
            
                </div>   <!-- /.row -->
		
                <div class="row">
                <div class="col-xs-12">
                    <div class="box">
                    <div class="box-body">
		                <div class="dTree">                                       
                            <div id="tree">
                            <asp:Literal ID="ltrTree" runat="server" />
                            </div>
                            <div id="spacetree">
                            </div>
		                </div>
                    </div><!-- /.box-body -->
                    </div><!-- /.box -->
                </div><!-- /.col -->
                </div><!-- /.row -->
        </section><!-- /.content -->

    </div> <!-- /.content-wrapper -->

    <footer class="main-footer">
    <div class="pull-right hidden-xs">
        <b>Version</b> 1.0
    </div>
    <strong>Copyright &copy; 2014-2015 <a href="#">VNI</a>.</strong> All rights reserved.
    </footer>    
    </form>
</body>
</html>
