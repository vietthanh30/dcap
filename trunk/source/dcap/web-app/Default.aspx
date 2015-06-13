<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="web_app._Default" %>
    
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <asp:Panel ID="MemberHeaderPanel" runat="server">
    <h1>
    Trang chủ
    </h1>
    <ol class="breadcrumb">
    <li><a href="#"><i class="fa fa-dashboard"></i> Trang chủ</a></li>
    </ol>
</asp:Panel>
<asp:Panel ID="AdminHeaderPanel" runat="server">
    <h1>
    Tổng quan hệ thống
    </h1>
    <ol class="breadcrumb">
    <li><a href="#"><i class="fa fa-dashboard"></i> Trang chủ</a></li>
    <li class="active">Tổng quan</li>
    </ol>
</asp:Panel>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:Panel ID="MemberContentPanel" runat="server">
<div class="row">
<div class="row">
<div class="col-md-12">
<h2>I. MÁY KHỬ ĐỘC OZONE HT401</h2>
<p><img width=360 height=240 src="~/dist/img/may-ozone-ht401.jpg" runat="server" align=left
hspace=12>Ngày nay khi công nghệ ngày càng hiện đại, việc sản xuất các chất bảo quản và các chất bảo vệ thực vật càng dễ dàng. Nhưng người sản xuất các giống rau, củ, quả, thịt, cá.. đang và đã lạm dụng thuốc trừ sâu và thuốc kính thích cũng như thuốc bảo vệ thực vật quá yêu cầu cho phép. Hơn ai hết, chúng ta phải tự bảo vệ bản thân mình trước khi người sản xuất ý thức được lòi giống chúng ta đa bị đầu độc bởi thức ăn, không khí và nước uống. Sau bao ngày nghiên cứu và đưa thử nghiệm thực tế, công ty Hitech VietNam chúng tôi cho ra đời sản phẩm máy khử độc rau, củ, quả, thịt, cá… dùng công nghệ tạo khí O3 ( ozon ) có tác dụng phân hủy và khử 99, 9% lượng độc tố trên thực phẩm.</p>
<p><span>
- Khả năng phân huỷ hoàn toàn thuốc trừ sâu và diệt 99, 99% những vi khuẩn có hại trong rau quả thực phẩm.
<br/>
- Khử mùi hôi tanh, các chất ướp trong thịt, cá.
<br/>
- Bảo quản rau quả thực phẩm tươi ngon lâu hơn.
<br/>
- Sản xuất nước ngậm Ozone dùng rửa sạch bát đĩa, chai lọ sữa cho em bé.
<br/>
- Sử dụng đơn giản, quá trình làm sạch rau quả hoàn toàn tự động.
</span></p>
</div>
</div>
<div class="row">
<div class="col-md-12">
<h2>II. ĐÈN BẮT MUỖI QUANG PHONG</h2>
<p style='text-align:justify'><img width=360 height=265
src="~/dist/img/den-bat-muoi.jpg" runat="server" align=left hspace=12>
Cuộc sống của bạn có đang bị ảnh hưởng bởi các loại côn trùng, ruồi, muỗi gây hại không? Nếu có thì bạn cần tìm ngay 1 giải pháp ngăn chặn ảnh hưởng của những loại côn trùng này. Đặc biệt nếu trong gia đình bạn có trẻ nhỏ thì việc ngăn chặn các loại côn trùng nguy hiểm càng trở nên cần thiết.</p>
<p style='text-align:justify'>Có rất nhiều biện pháp diệt côn trùng, nhưng đi kèm với những biện pháp đó là những ảnh hưởng không tốt tới sức khỏe của chúng ta. Ví dụ như hương muỗi, thuốc diệt côn trùng,...</p>
<p style='text-align:justify'>Vậy giải pháp nào sẽ giúp bạn vừa diệt được côn trùng mà không gây ảnh hưởng đền sức khỏe của chúng ta?</p>
<h3><span>Đèn diệt côn trùng, giải pháp diệt côn trùng an toàn nhất</span></h3>
<p style='text-align:justify'>Với thiết kế nhỏ gọn, có thể treo lên hoặc đặt trên bàn hay trên sàn, hơn nữa đèn diệt côn trùng hoàn toàn không sử dụng hóa chất và dòng điện của đèn không gây hại tới con người.</p>
</div>
</div>
</div>
</asp:Panel>
<asp:Panel ID="AdminContentPanel" runat="server">
<!-- Info boxes -->
<div class="row">
<%--    <div class="col-md-3 col-sm-6 col-xs-12">--%>
<%--        <div class="info-box">--%>
<%--        <span class="info-box-icon bg-aqua"><i class="ion ion-ios-people-outline"></i></span>--%>
<%--        <div class="info-box-content">--%>
<%--            <span class="info-box-text">Thành viên</span>--%>
<%--            <span class="info-box-number"><asp:Label ID="MemberAmount" runat="server"></asp:Label></span>--%>
<%--        </div><!-- /.info-box-content -->--%>
<%--        </div><!-- /.info-box -->--%>
<%--    </div><!-- /.col -->--%>
<%--    <!-- fix for small devices only -->--%>
<%--    <div class="clearfix visible-sm-block"></div>--%>

    <div class="col-md-6 col-sm-6 col-xs-12">
        <div class="info-box">
        <span class="info-box-icon bg-green"><i class="ion ion-ios-cart-outline"></i></span>
        <div class="info-box-content">
            <span class="info-box-text">Tổng số ID</span>
            <span class="info-box-number"><asp:Label ID="AccountAmount" runat="server"></asp:Label></span>
        </div><!-- /.info-box-content -->
        </div><!-- /.info-box -->
    </div><!-- /.col -->
			
    <div class="col-md-6 col-sm-6 col-xs-12">
        <div class="info-box">
        <span class="info-box-icon bg-yellow"><i class="ion ion-ios-people-outline"></i></span>
        <div class="info-box-content">
            <span class="info-box-text">Quản lý</span>
            <span class="info-box-number"><asp:Label ID="ManagerAmount" runat="server"></asp:Label></span>
        </div><!-- /.info-box-content -->
        </div><!-- /.info-box -->
    </div><!-- /.col -->
			
<%--	<div class="col-md-3 col-sm-6 col-xs-12">--%>
<%--        <div class="info-box">--%>
<%--        <span class="info-box-icon bg-yellow"><i class="ion ion-ios-people-outline"></i></span>--%>
<%--        <div class="info-box-content">--%>
<%--            <span class="info-box-text">Quản lý 6</span>--%>
<%--            <span class="info-box-number"><asp:Label ID="ManagerL6Amount" runat="server"></asp:Label></span>--%>
<%--        </div><!-- /.info-box-content -->--%>
<%--        </div><!-- /.info-box -->--%>
<%--    </div><!-- /.col -->--%>
			
</div><!-- /.row -->

<!-- Main row -->
<div class="row">
	<div class="col-md-12">
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
                

<%--    <div class="col-md-6">--%>
<%--        <!-- USERS LIST -->--%>
<%--        <div class="box box-danger">--%>
<%--            <div class="box-header with-border">--%>
<%--                <h3 class="box-title">Quản lý mới</h3>--%>
<%--                <div class="box-tools pull-right">--%>
<%--                <span class="label label-danger"><asp:Label ID="NewManagerAmount" runat="server"></asp:Label></span>--%>
<%--                <button class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>--%>
<%--                <button class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>--%>
<%--                </div>--%>
<%--            </div><!-- /.box-header -->--%>
<%--            <div class="box-body no-padding">--%>
<%--                <asp:Literal ID="NewManagerList" runat="server"></asp:Literal>--%>
<%--            </div><!-- /.box-body -->--%>
<%--        </div><!--/.box -->--%>
<%--    </div><!-- /.col -->--%>
</div><!-- /.row -->

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
