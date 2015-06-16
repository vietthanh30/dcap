<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="web_app._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <asp:Panel ID="MemberHeaderPanel" runat="server">
        <h1>
            Trang chủ
        </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Trang chủ</a></li>
        </ol>
    </asp:Panel>
    <asp:Panel ID="AdminHeaderPanel" runat="server">
        <h1>
            Tổng quan hệ thống
        </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Trang chủ</a></li>
            <li class="active">Tổng quan</li>
        </ol>
    </asp:Panel>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:Panel ID="MemberContentPanel" runat="server">
        <div class="row">
            <div class="col-md-6">
                <div class="box box-solid">
                    <div class="box-header with-border">
                        <h3 class="box-title">
                            Giới thiệu về công ty</h3>
                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
                        <div class="box-group" id="accordion">
                            <!-- we are adding the .panel class so bootstrap.js collapse plugin detects it -->
                            <div class="panel box box-primary">
                                <div id="collapseOne" class="panel-collapse collapse in">
                                    <div class="box-body">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- /.box-body -->
                </div>
                <!-- /.box -->
            </div>
            <!-- /.col -->
            <div class="col-md-6">
                <div class="box box-solid">
                    <div class="box-header with-border">
                        <h3 class="box-title">
                            Hoạt động</h3>
                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
                        <div id="carousel-example-generic" class="carousel slide" data-ride="carousel">
                            <ol class="carousel-indicators">
                                <li data-target="#carousel-example-generic" data-slide-to="0" class="active"></li>
                                <li data-target="#carousel-example-generic" data-slide-to="1" class=""></li>
                                <li data-target="#carousel-example-generic" data-slide-to="2" class=""></li>
                                <li data-target="#carousel-example-generic" data-slide-to="3" class=""></li>
                                <li data-target="#carousel-example-generic" data-slide-to="4" class=""></li>
                                <li data-target="#carousel-example-generic" data-slide-to="5" class=""></li>
                                <li data-target="#carousel-example-generic" data-slide-to="6" class=""></li>
                                <li data-target="#carousel-example-generic" data-slide-to="7" class=""></li>
                                <li data-target="#carousel-example-generic" data-slide-to="8" class=""></li>
                                <li data-target="#carousel-example-generic" data-slide-to="9" class=""></li>
                            </ol>
                            <div class="carousel-inner">
                                <div class="item active">
                                    <img src="~/dist/img/event/1.JPG" runat="server" alt="">
<%--                                    <div class="carousel-caption">--%>
<%--                                        First Slide--%>
<%--                                    </div>--%>
                                </div>
                                <div class="item">
                                    <img src="~/dist/img/event/2.JPG" runat="server" alt="">
<%--                                    <div class="carousel-caption">--%>
<%--                                        Second Slide--%>
<%--                                    </div>--%>
                                </div>
                                <div class="item">
                                    <img src="~/dist/img/event/3.JPG" runat="server" alt="">
<%--                                    <div class="carousel-caption">--%>
<%--                                        Third Slide--%>
<%--                                    </div>--%>
                                </div>
                                <div class="item">
                                    <img id="Img3" src="~/dist/img/event/4.JPG" runat="server" alt="">
                                </div>
                                <div class="item">
                                    <img id="Img4" src="~/dist/img/event/5.JPG" runat="server" alt="">
                                </div>
                                <div class="item">
                                    <img id="Img5" src="~/dist/img/event/6.JPG" runat="server" alt="">
                                </div>
                                <div class="item">
                                    <img id="Img6" src="~/dist/img/event/7.JPG" runat="server" alt="">
                                </div>
                                <div class="item">
                                    <img id="Img7" src="~/dist/img/event/8.JPG" runat="server" alt="">
                                </div>
                                <div class="item">
                                    <img id="Img8" src="~/dist/img/event/9.JPG" runat="server" alt="">
                                </div>
                                <div class="item">
                                    <img id="Img9" src="~/dist/img/event/10.JPG" runat="server" alt="">
                                </div>
                            </div>
                            <a class="left carousel-control" href="#carousel-example-generic" data-slide="prev">
                                <span class="fa fa-angle-left"></span></a><a class="right carousel-control" href="#carousel-example-generic"
                                    data-slide="next"><span class="fa fa-angle-right"></span></a>
                        </div>
                    </div>
                    <!-- /.box-body -->
                </div>
                <!-- /.box -->
            </div>
            <!-- /.col -->
        </div>
        <!-- /.row -->
        <!-- END ACCORDION & CAROUSEL-->
        <h2 class="page-header">
            SẢN PHẨM</h2>
        <div class="row">
            <div class="col-md-12">
                <div class="box box-solid">
                    <div class="box-header with-border">
                        <h3 class="box-title">
                            MÁY KHỬ ĐỘC OZONE HT401</h3>
                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
                        <p>
                            <img id="Img1" width="360" src="~/dist/img/may-ozone-ht401.jpg" runat="server"
                                align="left" hspace="12">
                            <span ><font color=red>Tính năng tác dụng:</font></span>
                            <br />
                            - Diệt trừ vi khuẩn virus, vi trùng, nấm mốc.v.v...
                            <br />
                            - Loại bỏ dư lượng thuốc trừ sâu, thuốc bảo vệ thực vật, thuốc diệt cỏ độc hại.
                            <br />
                            - Làm sạch nước và không khí, khử mùi hôi tanh
                            <br />
                            <span ><font color=red>Thông số kỹ thuật:</font></span>
                            <br />
                            - Công suất Ozone: 450mg/h x 2cửa ra
                            <br />
                            - Điện áp: 220VAC~50Hz. Công suất tiêu thụ: 30W
                            <br />
                            - Kích thước: D290; d260 x R10
                            <br />
                            - Trọng lượng: 1.4Kg
                            <br />
                            <font color=red>Phụ kiện:</font> 02 dây sủi, 03 quả sủi
                            <br />
                            <font color=red>Bảo hành:</font>
                            <br />
                            - Đổi mới trong vòng 9 tháng
                            <br />
                            - Bảo hành miễn phí trong vòng 24 tháng
                            <br />
                            - Sửa chữa ưu đãi trong vòng 36 tháng
                            <br />
                        </p>
                    </div>
                </div>
                <!-- /.box -->
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="box box-solid">
                    <div class="box-header with-border">
                        <h3 class="box-title">
                            ĐÈN BẮT MUỖI QUANG PHONG</h3>
                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
                        <p style='text-align: justify'>
                            <img id="Img2" width="360" height="265" src="~/dist/img/den-bat-muoi.jpg" runat="server"
                                align="left" hspace="12">
                            Cuộc sống của bạn có đang bị ảnh hưởng bởi các loại côn trùng, ruồi, muỗi gây hại
                            không? Nếu có thì bạn cần tìm ngay 1 giải pháp ngăn chặn ảnh hưởng của những loại
                            côn trùng này. Đặc biệt nếu trong gia đình bạn có trẻ nhỏ thì việc ngăn chặn các
                            loại côn trùng nguy hiểm càng trở nên cần thiết.</p>
                        <p style='text-align: justify'>
                            Có rất nhiều biện pháp diệt côn trùng, nhưng đi kèm với những biện pháp đó là những
                            ảnh hưởng không tốt tới sức khỏe của chúng ta. Ví dụ như hương muỗi, thuốc diệt
                            côn trùng,...</p>
                        <p style='text-align: justify'>
                            Vậy giải pháp nào sẽ giúp bạn vừa diệt được côn trùng mà không gây ảnh hưởng đền
                            sức khỏe của chúng ta?</p>
                        <h4>
                            <span>Đèn diệt côn trùng, giải pháp diệt côn trùng an toàn nhất</span></h4>
                        <p style='text-align: justify'>
                            Với thiết kế nhỏ gọn, có thể treo lên hoặc đặt trên bàn hay trên sàn, hơn nữa đèn
                            diệt côn trùng hoàn toàn không sử dụng hóa chất và dòng điện của đèn không gây hại
                            tới con người.</p>
                    </div>
                </div>
                <!-- /.box -->
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="AdminContentPanel" runat="server">
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
    </asp:Panel>
</asp:Content>
