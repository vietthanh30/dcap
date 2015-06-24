<%@ Page Title="Cây quản lý" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManagerNetwork.aspx.cs" Inherits="web_app.main_pages.ManagerNetwork" %>
<asp:Content ID="Content5" ContentPlaceHolderID="HeadContent" runat="server">
    <h1>
    Cây quản lý
    </h1>
    <ol class="breadcrumb">
    <li><a href="#"><i class="fa fa-dashboard"></i> Trang chủ</a></li>
    <li class="active">Cây quản lý</li>
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
                <span class="failureNotification">
                    <asp:Literal ID="ErrorMessage" runat="server"></asp:Literal>
                </span>
                <asp:ValidationSummary ID="ManagerNetworkValidationSummary" runat="server" CssClass="failureNotification" 
                        ValidationGroup="ManagerNetworkValidationGroup"/>  
                </div>
            </div>
			<div class="row">
				<div class="col-xs-4">
				<label for="CapQuanLy">Cấp quản lý</label>
				<asp:DropDownList ID="CapQuanLy" runat="server" class="form-control">
                    <asp:ListItem Value="1" Text="Cấp 1" Selected="True"></asp:ListItem>
                    <asp:ListItem Value="2" Text="Cấp 2"></asp:ListItem>
                    <asp:ListItem Value="3" Text="Cấp 3"></asp:ListItem>
                    <asp:ListItem Value="4" Text="Cấp 4"></asp:ListItem>
                    <asp:ListItem Value="5" Text="Cấp 5"></asp:ListItem>
                    <asp:ListItem Value="6" Text="Cấp 6"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="CapQuanLyRequired" runat="server" ControlToValidate="CapQuanLy" 
                    CssClass="failureNotification" ErrorMessage="Cấp quản lý bắt buộc nhập." ToolTip="Cấp quản lý bắt buộc nhập." 
                    ValidationGroup="ManagerNetworkValidationGroup">*</asp:RequiredFieldValidator>
				</div>
				<div class="col-xs-4">
				<label for="IdMember">ID thành viên</label>
				<input type="text" class="form-control" maxlength="7" id="IdMember" runat="server" placeholder="Nhập ID thành viên">
				</div>
			</div>
            </div><!-- /.box-body -->
				  
            <div class="box-footer">            
                <asp:Button ID="SearchButton" runat="server" Text="Tra cứu" class="btn btn-primary" 
                    ValidationGroup="ManagerNetworkValidationGroup" onclick="ManagerNetwork_SearchNetwork"/>
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
                <asp:Label ID="InvalidCredentialsMessage" runat="server" ForeColor="Red" class="failureNotification"
                    Text="" Visible="False"></asp:Label>
            </div>
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
