<%@ Page Language="C#" MasterPageFile="~/Site.master" CodeBehind="bang-ke-tra-luong.aspx.cs" Inherits="web_app.main_pages.bang_ke_tra_luong" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <h1>
    Bảng kê trả lương
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
					<label for="BeginDate">Từ ngày</label>
					<input type="text" class="form-control" runat="server" id="BeginDate" placeholder="Nhập ngày bắt đầu">
				</div>
				<div class="col-xs-4">
					<label for="exampleInputPassword1">Đến ngày</label>
					<input type="text" class="form-control" runat="server" id="EndDate" placeholder="Nhập kết thúc">
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
    <div class="col-xs-12">
        <div class="box">
                
        <div class="box-body">
            <table id="example1" class="table table-bordered table-striped">
            <thead>
                <tr>
                <th>STT</th>
                <th>Họ tên</th>
                <th>Số CMND</th>
                <th>Địa chỉ</th>
                <th>Số TK</th>
				<th>Ngân hàng</th>
				<th>Số ĐT</th>
				<th>Số tiền</th>
				<th>Tháng</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                <td>1</td>
                <td>Phạm Hoàng Long</td>
                <td>012018990</td>
                <td>Hà Nội</td>
                <td>011567283939</td>
				<td>Agribank - Nam Hà Nội</td>
				<td>0915678345</td>
				<td>10.000.000</td>
				<td>Tháng 5/2015</td>
                </tr>
                <tr>
                <td>2</td>
                <td>Đường Ngọc Lan</td>
                <td>012018991</td>
                <td>Hà Nội</td>
                <td>011567283940</td>
				<td>Agribank - Nam Hà Nội</td>
				<td>0915678345</td>
				<td>10.000.000</td>
				<td>Tháng 5/2015</td>
                </tr>
				<tr>
                <td>3</td>
                <td>Nguyễn Lê Thắng</td>
                <td>012018992</td>
                <td>Hà Nội</td>
                <td>011567283941</td>
				<td>Agribank - Nam Hà Nội</td>
				<td>0915678345</td>
				<td>10.000.000</td>
				<td>Tháng 5/2015</td>
                </tr>
				<tr>
                <td>4</td>
                <td>Trần Ngọc Linh</td>
                <td>012018990</td>
                <td>Hà Nội</td>
                <td>011567283939</td>
				<td>Agribank - Nam Hà Nội</td>
				<td>0915678345</td>
				<td>10.000.000</td>
				<td>Tháng 5/2015</td>
                </tr>
				<tr>
                <td>5</td>
                <td>Đỗ Đình Cường</td>
                <td>012018990</td>
                <td>Hà Nội</td>
                <td>011567283939</td>
				<td>Agribank - Nam Hà Nội</td>
				<td>0915678345</td>
				<td>10.000.000</td>
				<td>Tháng 5/2015</td>
                </tr>
				<tr>
                <td>6</td>
                <td>Đường Quốc Cường</td>
                <td>012018990</td>
                <td>Hà Nội</td>
                <td>011567283939</td>
				<td>Agribank - Nam Hà Nội</td>
				<td>0915678345</td>
				<td>10.000.000</td>
				<td>Tháng 5/2015</td>
                </tr>
				<tr>
                <td>7</td>
                <td>Hồ Ngọc Hà</td>
                <td>012018990</td>
                <td>Hà Nội</td>
                <td>011567283939</td>
				<td>Agribank - Nam Hà Nội</td>
				<td>0915678345</td>
				<td>10.000.000</td>
				<td>Tháng 5/2015</td>
                </tr>
				<tr>
                <td>8</td>
                <td>Phạm Mỹ Linh</td>
                <td>012018990</td>
                <td>Hà Nội</td>
                <td>011567283939</td>
				<td>Agribank - Nam Hà Nội</td>
				<td>0915678345</td>
				<td>10.000.000</td>
				<td>Tháng 5/2015</td>
                </tr>
				<tr>
                <td>9</td>
                <td>Hà Thanh Sơn</td>
                <td>012018990</td>
                <td>Hà Nội</td>
                <td>011567283939</td>
				<td>Agribank - Nam Hà Nội</td>
				<td>0915678345</td>
				<td>10.000.000</td>
				<td>Tháng 5/2015</td>
                </tr>
				<tr>
                <td>10</td>
                <td>Hà Hải Yến</td>
                <td>012018990</td>
                <td>Hà Nội</td>
                <td>011567283939</td>
				<td>Agribank - Nam Hà Nội</td>
				<td>0915678345</td>
				<td>10.000.000</td>
				<td>Tháng 5/2015</td>
                </tr>
				<tr>
                <td>11</td>
                <td>Phạm Hoàng Long</td>
                <td>012018990</td>
                <td>Hà Nội</td>
                <td>011567283939</td>
				<td>Agribank - Nam Hà Nội</td>
				<td>0915678345</td>
				<td>10.000.000</td>
				<td>Tháng 5/2015</td>
                </tr>
				<tr>
                <td>12</td>
                <td>Phạm Hoàng Long</td>
                <td>012018990</td>
                <td>Hà Nội</td>
                <td>011567283939</td>
				<td>Agribank - Nam Hà Nội</td>
				<td>0915678345</td>
				<td>10.000.000</td>
				<td>Tháng 5/2015</td>
                </tr>
				<tr>
                <td>13</td>
                <td>Phạm Hoàng Long</td>
                <td>012018990</td>
                <td>Hà Nội</td>
                <td>011567283939</td>
				<td>Agribank - Nam Hà Nội</td>
				<td>0915678345</td>
				<td>10.000.000</td>
				<td>Tháng 5/2015</td>
                </tr>
				<tr>
                <td>14</td>
                <td>Phạm Hoàng Long</td>
                <td>012018990</td>
                <td>Hà Nội</td>
                <td>011567283939</td>
				<td>Agribank - Nam Hà Nội</td>
				<td>0915678345</td>
				<td>10.000.000</td>
				<td>Tháng 5/2015</td>
                </tr>
				<tr>
                <td>15</td>
                <td>Phạm Hoàng Long</td>
                <td>012018990</td>
                <td>Hà Nội</td>
                <td>011567283939</td>
				<td>Agribank - Nam Hà Nội</td>
				<td>0915678345</td>
				<td>10.000.000</td>
				<td>Tháng 5/2015</td>
                </tr>
				<tr>
                <td>16</td>
                <td>Phạm Hoàng Long</td>
                <td>012018990</td>
                <td>Hà Nội</td>
                <td>011567283939</td>
				<td>Agribank - Nam Hà Nội</td>
				<td>0915678345</td>
				<td>10.000.000</td>
				<td>Tháng 5/2015</td>
                </tr>
				<tr>
                <td>17</td>
                <td>Phạm Hoàng Long</td>
                <td>012018990</td>
                <td>Hà Nội</td>
                <td>011567283939</td>
				<td>Agribank - Nam Hà Nội</td>
				<td>0915678345</td>
				<td>10.000.000</td>
				<td>Tháng 5/2015</td>
                </tr>
				<tr>
                <td>18</td>
                <td>Phạm Hoàng Long</td>
                <td>012018990</td>
                <td>Hà Nội</td>
                <td>011567283939</td>
				<td>Agribank - Nam Hà Nội</td>
				<td>0915678345</td>
				<td>10.000.000</td>
				<td>Tháng 5/2015</td>
                </tr>
				<tr>
                <td>19</td>
                <td>Phạm Hoàng Long</td>
                <td>012018990</td>
                <td>Hà Nội</td>
                <td>011567283939</td>
				<td>Agribank - Nam Hà Nội</td>
				<td>0915678345</td>
				<td>10.000.000</td>
				<td>Tháng 5/2015</td>
                </tr>
				<tr>
                <td>20</td>
                <td>Phạm Hoàng Long</td>
                <td>012018990</td>
                <td>Hà Nội</td>
                <td>011567283939</td>
				<td>Agribank - Nam Hà Nội</td>
				<td>0915678345</td>
				<td>10.000.000</td>
				<td>Tháng 5/2015</td>
                </tr>
				<tr>
                <td>21</td>
                <td>Phạm Hoàng Long</td>
                <td>012018990</td>
                <td>Hà Nội</td>
                <td>011567283939</td>
				<td>Agribank - Nam Hà Nội</td>
				<td>0915678345</td>
				<td>10.000.000</td>
				<td>Tháng 5/2015</td>
                </tr>
				<tr>
                <td>22</td>
                <td>Phạm Hoàng Long</td>
                <td>012018990</td>
                <td>Hà Nội</td>
                <td>011567283939</td>
				<td>Agribank - Nam Hà Nội</td>
				<td>0915678345</td>
				<td>10.000.000</td>
				<td>Tháng 5/2015</td>
                </tr>
				<tr>
                <td>23</td>
                <td>Phạm Hoàng Long</td>
                <td>012018990</td>
                <td>Hà Nội</td>
                <td>011567283939</td>
				<td>Agribank - Nam Hà Nội</td>
				<td>0915678345</td>
				<td>10.000.000</td>
				<td>Tháng 5/2015</td>
                </tr>
				<tr>
                <td>24</td>
                <td>Phạm Hoàng Long</td>
                <td>012018990</td>
                <td>Hà Nội</td>
                <td>011567283939</td>
				<td>Agribank - Nam Hà Nội</td>
				<td>0915678345</td>
				<td>10.000.000</td>
				<td>Tháng 5/2015</td>
                </tr>
				<tr>
                <td>25</td>
                <td>Phạm Hoàng Long</td>
                <td>012018990</td>
                <td>Hà Nội</td>
                <td>011567283939</td>
				<td>Agribank - Nam Hà Nội</td>
				<td>0915678345</td>
				<td>10.000.000</td>
				<td>Tháng 5/2015</td>
                </tr>
				<tr>
                <td>26</td>
                <td>Phạm Hoàng Long</td>
                <td>012018990</td>
                <td>Hà Nội</td>
                <td>011567283939</td>
				<td>Agribank - Nam Hà Nội</td>
				<td>0915678345</td>
				<td>10.000.000</td>
				<td>Tháng 5/2015</td>
                </tr>
				<tr>
                <td>27</td>
                <td>Phạm Hoàng Long</td>
                <td>012018990</td>
                <td>Hà Nội</td>
                <td>011567283939</td>
				<td>Agribank - Nam Hà Nội</td>
				<td>0915678345</td>
				<td>10.000.000</td>
				<td>Tháng 5/2015</td>
                </tr>
				<tr>
                <td>28</td>
                <td>Phạm Hoàng Long</td>
                <td>012018990</td>
                <td>Hà Nội</td>
                <td>011567283939</td>
				<td>Agribank - Nam Hà Nội</td>
				<td>0915678345</td>
				<td>10.000.000</td>
				<td>Tháng 5/2015</td>
                </tr>
				<tr>
                <td>29</td>
                <td>Phạm Hoàng Long</td>
                <td>012018990</td>
                <td>Hà Nội</td>
                <td>011567283939</td>
				<td>Agribank - Nam Hà Nội</td>
				<td>0915678345</td>
				<td>10.000.000</td>
				<td>Tháng 5/2015</td>
                </tr>
				<tr>
                <td>30</td>
                <td>Phạm Hoàng Long</td>
                <td>012018990</td>
                <td>Hà Nội</td>
                <td>011567283939</td>
				<td>Agribank - Nam Hà Nội</td>
				<td>0915678345</td>
				<td>10.000.000</td>
				<td>Tháng 5/2015</td>
                </tr>
				<tr>
                <td>31</td>
                <td>Phạm Hoàng Long</td>
                <td>012018990</td>
                <td>Hà Nội</td>
                <td>011567283939</td>
				<td>Agribank - Nam Hà Nội</td>
				<td>0915678345</td>
				<td>10.000.000</td>
				<td>Tháng 5/2015</td>
                </tr>
				<tr>
                <td>32</td>
                <td>Phạm Hoàng Long</td>
                <td>012018990</td>
                <td>Hà Nội</td>
                <td>011567283939</td>
				<td>Agribank - Nam Hà Nội</td>
				<td>0915678345</td>
				<td>10.000.000</td>
				<td>Tháng 5/2015</td>
                </tr>
            </tbody>
            <tfoot>
                <tr>
                <th>STT</th>
                <th>Họ tên</th>
                <th>Số CMND</th>
                <th>Địa chỉ</th>
                <th>Số TK</th>
				<th>Ngân hàng</th>
				<th>Số ĐT</th>
				<th>Số tiền</th>
				<th>Tháng</th>
                </tr>
            </tfoot>
            </table>
        </div><!-- /.box-body -->
        </div><!-- /.box -->
    </div><!-- /.col -->
    </div><!-- /.row -->
	<div class="box-footer">
	<asp:Button runat="server" class="btn btn-primary" Text="Xuất Excel file" 
            onclick="BangKeTraLuong_ExportExcel" />
    <asp:Button runat="server" class="btn btn-primary" Text="Xuất PDF file" 
            onclick="BangKeTraLuong_ExportPDF" />
    </div>
</asp:Content>