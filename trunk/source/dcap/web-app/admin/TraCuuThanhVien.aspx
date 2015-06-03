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
				<div class="col-xs-4">
					<label for="SoCmndSearch">Số CMND</label>
					<input type="text" class="form-control" runat="server" id="SoCmndSearch" placeholder="Nhập Số CMND">
				</div>
				<div class="col-xs-4">
					<label for="IdThanhVienSearch">Id thành viên</label>
					<input type="text" class="form-control" runat="server" id="IdThanhVienSearch" placeholder="Nhập Id thành viên">
				</div>
			</div>
			<div class="row">
				<div class="col-xs-8">
					<label for="HoTenSearch">Họ tên</label>
					<input type="text" class="form-control" runat="server" id="HoTenSearch" placeholder="Nhập Họ tên">
				</div>
			</div>
            </div><!-- /.box-body -->
				  
            <div class="box-footer">        
                <asp:Button ID="SearchButton" runat="server" Text="Tra cứu" 
                    class="btn btn-primary" onclick="TraCuuThanhVien_Search" 
                    />
            </div>
        </div><!-- /.box -->

    </div><!--/.col (left) -->
    </div>
		  
    <asp:HiddenField ID="ContinueDestinationPageUrl" runat="server" />  
    <div class="row">
		<div class="col-xs-8">
        <asp:Label ID="InvalidCredentialsMessage" runat="server" class="failureNotification"
            Text="" Visible="False"></asp:Label>
            </div>
    </div>   
    <div class="row">
    <div class="col-xs-12">
        <div class="box">
                
        <div class="box-body">
            <asp:GridView ID="gvMemberInfo" runat="server" AutoGenerateColumns="false" 
                EnableModelValidation="true" class="table table-bordered table-striped" 
                BorderColor="#CCCCCC" >
                <Columns>
                <asp:BoundField DataField="FullName" HeaderText="Họ tên" >
                <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="SoCmnd" HeaderText="Số CMND" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="AccountNumber" HeaderText="Id Thành viên" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="UserName" HeaderText="Tên đăng nhập" >
                <ItemStyle HorizontalAlign="Left" />
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
    <asp:Button ID="BtnExportExcel" runat="server" class="btn btn-primary" 
            Text="Xuất Excel file" onclick="TraCuuThanhVien_ExportToExcel" 
            />
    <asp:Button ID="BtnExportDoc" runat="server" class="btn btn-primary" 
            Text="Xuất Word file" onclick="TraCuuThanhVien_ExportToWord" 
            />
    </div>
</asp:Content>
