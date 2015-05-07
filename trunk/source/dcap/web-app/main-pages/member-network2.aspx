<%@ Page Language="C#" MasterPageFile="~/Site.master" CodeBehind="member-network2.aspx.cs" Inherits="web_app.main_pages.member_network2" %>

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
            <asp:Label ID="ParentInfo" runat="server"></asp:Label>
		    </div>
		    <div class="dTree">
                <asp:TreeView ID="TreeThanhVien" runat="server" >
                <DataBindings>
                    <asp:TreeNodeBinding DataMember="System.Data.DataRowView" TextField="Description" ValueField="AccountId" />
                </DataBindings>
                </asp:TreeView>
		    </div>
        </div><!-- /.box-body -->
        </div><!-- /.box -->
    </div><!-- /.col -->
    </div><!-- /.row -->
</asp:Content>
