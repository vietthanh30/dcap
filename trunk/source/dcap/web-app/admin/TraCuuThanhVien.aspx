<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TraCuuThanhVien.aspx.cs" Inherits="web_app.admin.TraCuuThanhVien" %>
<asp:Content ID="Content5" ContentPlaceHolderID="HeadContent" runat="server">
    <h1>
    Tra cứu thành viên
    </h1>
    <ol class="breadcrumb">
    <li><a href="#"><i class="fa fa-dashboard"></i> Trang chủ</a></li>
    <li class="active">Tra cứu thành viên</li>
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
				<div class="col-xs-8">
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
                    />
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
                 BorderColor="#CCCCCC" >
                <Columns>
                <asp:BoundField DataField="STT" HeaderText="STT" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="HoTen" HeaderText="Họ tên" >
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
                <asp:BoundField DataField="SoTien" HeaderText="Tổng tiền" >
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="Thang" HeaderText="Tháng" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                </Columns>
                <HeaderStyle BackColor="#CCCCCC" Font-Names="Arial" Font-Size="Small" HorizontalAlign="Center" />
                <RowStyle Font-Names="Arial" Font-Size="Small" HorizontalAlign="Left" VerticalAlign="Middle" />
            </asp:GridView>
        </div><!-- /.box-body -->
        </div><!-- /.box -->
    </div><!-- /.col -->
    </div><!-- /.row -->
	<div class="box-footer">
	<asp:Button ID="Button1" runat="server" class="btn btn-primary" Text="Xuất Excel file" 
             />
    <asp:Button ID="Button2" runat="server" class="btn btn-primary" Text="Xuất Word file" 
             />
    </div>
</asp:Content>
