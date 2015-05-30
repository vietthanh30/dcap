<%@ Page Language="C#" MasterPageFile="~/Site.master" CodeBehind="member-network.aspx.cs" Inherits="web_app.main_pages.member_network" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <h1>
    Mạng lưới thành viên
    </h1>
    <ol class="breadcrumb">
    <li><a href="#"><i class="fa fa-dashboard"></i> Trang chủ</a></li>
    <li class="active">Bảng kê</li>
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
				<label for="IdMember">ID thành viên</label>
				<input type="text" class="form-control" id="IdMember" runat="server" placeholder="Nhập ID thành viên">
				</div>
			</div>
			<div class="row">
				<div class="col-xs-4">
				<label for="SoCmnd">Số CMND</label>
				<input type="text" class="form-control" id="SoCmnd" runat="server" placeholder="Nhập số CMND">
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
								<ul>
									<li><a href="#">Phạm Hoàng Long [0000001]</a>
										<ul>
											<li><a href="#">Phạm Trung Linh [0000002]</a></li>
											<li><a href="#">Nguyễn Lê Thắng [0000003]</a>
												<ul>
													<li><a href="#">Hà Hải Yến [0000004]</a>
														<ul>
															<li><a href="#">Đỗ Văn Cường [0000041]</a>
																<ul>
																	<li><a href="#">Nguyễn Văn Tuyền [0000081]</a></li>
																	<li><a href="#">Trần Ngọc Huyền Như [0000091]</a></li>
																	<li><a href="#">Nguyễn Ngọc Thắng [0000100]</a></li>
																</ul>
															</li>
															<li><a href="#">Nguyễn Ngọc Dương [0000101]</a>
																<ul>
																	<li><a href="#">Doãn Chí Bình [0000102]</a></li>
																	<li><a href="#">Trịnh Minh Cương [0000113]</a></li>
																	<li><a href="#">Nguyễn Ngọc Ninh [0000114]</a>
																		<ul>
																			<li><a href="#">Nguyễn Thị Hợi [0000115]</a></li>
																			<li><a href="#">Nguyễn Ngọc Quỳnh [0000116]</a></li>
																			<li><a href="#">Trịnh Kim Thành [0000117]</a></li>
																		</ul>
																	</li>
																</ul>
															</li>
														</ul>
													</li>
													<li><a href="#">Hà Thanh Sơn [0000005]</a></li>
													<li><a href="#">Nguyễn Thùy Dung [0000006]</a>
														<ul>
															<li><a href="#">Đỗ Đình Cường [0000007]</a>
																<ul>
																	<li><a href="#">Nguyễn Ngọc Tuyền [0000008]</a></li>
																	<li><a href="#">Trần Ngọc Linh [0000009]</a></li>
																	<li><a href="#">Nguyễn Lê Thắng [0000010]</a></li>
																</ul>
															</li>
															<li><a href="#">Nguyễn Đắc Dương [0000011]</a>
																<ul>
																	<li><a href="#">Lương Thanh Hoài [0000012]</a></li>
																	<li><a href="#">Trịnh Hoàng Cương [0000013]</a></li>
																	<li><a href="#">Nguyễn Ngọc Minh [0000014]</a>
																		<ul>
																			<li><a href="#">Nguyễn Thị Thái Ngân [0000015]</a></li>
																			<li><a href="#">Nguyễn Quỳnh Diệp [0000016]</a></li>
																			<li><a href="#">Trần Thị Thành [0000017]</a></li>
																		</ul>
																	</li>
																</ul>
															</li>
														</ul>
													</li>
												</ul>
											</li>
											<li><a href="#">Mã Ngọc Khang [0000018]</a>
												<ul>
													<li><a href="#">Đường Quốc Cường [0000019]</a>
														<ul>
															<li><a href="#">Hồ Ngọc Hà [0000020]</a></li>
															<li><a href="#">Nguyễn Hồng Nhung [0000021]</a></li>
														</ul>
													</li>
													<li><a href="#">Phạm Mỹ Linh [0000022]</a></li>
													<li><a href="#">Hồ Quỳnh Hương [0000023]</a>
														<ul>
															<li><a href="#">Đàm Vĩnh Hưng [0000024]</a></li>
															<li><a href="#">Nguyễn Tuấn Hưng [0000025]</a></li>
															<li><a href="#">Phạm Bằng Kiều [0000026]</a></li>
														</ul>
													</li>
												</ul>
											</li>
										</ul>
									</li>
								</ul>
							</div>
        </div><!-- /.box-body -->
        </div><!-- /.box -->
    </div><!-- /.col -->
    </div><!-- /.row -->
</asp:Content>
