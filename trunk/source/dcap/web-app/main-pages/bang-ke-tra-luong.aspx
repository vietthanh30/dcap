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
                    onclick="BangKeTraLuong_SearchBangKe"/>
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
                BorderColor="#CCCCCC" AllowPaging="True" 
                onpageindexchanging="gvBangKe_PageIndexChanging" PageSize="15" >
                <Columns>
                <asp:TemplateField HeaderText="STT">
                <ItemTemplate><%#GetStt() %></ItemTemplate>
                </asp:TemplateField>
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
                <PagerSettings Mode="NextPrevious" 
                    NextPageText="&amp;nbsp;&amp;nbsp;&amp;nbsp;Next" PageButtonCount="6" 
                    PreviousPageText="Previous" />
                <RowStyle Font-Names="Arial" Font-Size="Small" HorizontalAlign="Left" VerticalAlign="Middle" />
            </asp:GridView>
        </div><!-- /.box-body -->
        </div><!-- /.box -->
    </div><!-- /.col -->
    </div><!-- /.row -->
	<div class="box-footer">
	<asp:Button runat="server" class="btn btn-primary" Text="Xuất Excel file" 
            onclick="BangKeTraLuong_ExportExcel" />
    <asp:Button runat="server" class="btn btn-primary" Text="Xuất Word file" 
            onclick="BangKeTraLuong_ExportDOC" />
    </div>
</asp:Content>