<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ApprovalBonusAdd.aspx.cs" Inherits="web_app.admin.ApprovalBonusAdd" %>
<%@ Register Assembly="ASP.Web.UI.PopupControl" Namespace="ASP.Web.UI.PopupControl"
    TagPrefix="ASPP" %>
<asp:Content ID="Content5" ContentPlaceHolderID="HeadContent" runat="server">
    <h1>
    Duyệt thưởng thêm
    </h1>
    <ol class="breadcrumb">
    <li><a href="#"><i class="fa fa-dashboard"></i> Trang chủ</a></li>
    <li class="active">Duyệt thưởng thêm</li>
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
					<label for="IdThanhVienSearch">Id thành viên</label>
					<input type="text" class="form-control" runat="server" id="IdThanhVienSearch" placeholder="Nhập Id thành viên">
				</div>
				<div class="col-xs-4">
					<label for="UserNameSearch">Tên đăng nhập</label>
					<input type="text" class="form-control" runat="server" id="UserNameSearch" placeholder="Nhập Tên đăng nhập">
				</div>
			</div>
            </div><!-- /.box-body -->
				  
            <div class="box-footer">        
                <asp:Button ID="SearchButton" runat="server" Text="Tra cứu" 
                    class="btn btn-primary" onclick="ApprovalBonusAdd_Search" 
                    />
            </div>
        </div><!-- /.box -->

    </div><!--/.col (left) -->
    </div>
		  
    <asp:HiddenField ID="ContinueDestinationPageUrl" runat="server" /> 
    <div class="row">
		<div class="col-xs-8">
        <asp:Label ID="InvalidCredentialsMessage" runat="server" class="failureNotification" ForeColor="Blue"
            Text="" Visible="False"></asp:Label>
            </div>
    </div>
    <div class="row">
    <div class="col-xs-12">
        <div class="box">
                
        <div class="box-body">
            <asp:GridView ID="gvBonusApproval" runat="server" AutoGenerateColumns="false" 
                EnableModelValidation="true" class="table table-bordered table-striped" 
                BorderColor="#CCCCCC" AllowPaging="True" 
                onpageindexchanging="gvBonusApproval_PageIndexChanging" PageSize="15" >
                <Columns>
                <asp:TemplateField HeaderText="STT">
                <ItemTemplate><%#GetStt() %></ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="AccountNumber" HeaderText="Id Thành viên" DataFormatString="{0:0000000}" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="UserName" HeaderText="Tên đăng nhập" >
                <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="BonusAmount" HeaderText="Điểm thưởng" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="IsApproved" HeaderText="Trạng thái" >
                <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Duyệt">
                    <ItemTemplate>
                        <asp:ImageButton ID="imgBtnApprove" runat="server" ImageUrl="~/dist/img/approve-icon.png"
                            OnClick="imgBtnApprove_Click" CausesValidation="False" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Width="20px" />
                </asp:TemplateField>
                </Columns>
                <HeaderStyle BackColor="#CCCCCC" Font-Names="Arial" Font-Size="Small" HorizontalAlign="Center" />
                <PagerSettings Mode="NumericFirstLast" 
                    NextPageText="" PageButtonCount="5" 
                    PreviousPageText="" FirstPageText="Đầu" LastPageText="Cuối" />
                <PagerStyle CssClass="GridPager" HorizontalAlign="Right" />
                <RowStyle Font-Names="Arial" Font-Size="Small" HorizontalAlign="Left" VerticalAlign="Middle" />
            </asp:GridView>
        </div><!-- /.box-body -->
        </div><!-- /.box -->
    </div><!-- /.col -->
    </div><!-- /.row -->
    <ASPP:PopupPanel HeaderText="Duyệt thưởng thêm" ID="ApprovalBonusPopup" runat="server" OnCloseWindowClick="OnClosePopupWindow">
        <PopupWindow>
        <ASPP:PopupWindow ID="ApprovalPopupWindow" runat="server">
        <asp:Panel ID="ApprovalBonusPanel" runat="server" BorderStyle="Ridge" style="width: 500px;">
        <div class="box-body">   
        <div class="row">
			<div class="col-xs-8">
            <asp:Label ID="ApprovalBonusLabel" runat="server"></asp:Label>
			<asp:HiddenField ID="AccountNumberApproval" runat="server" />
			</div>
		</div>
                    
        </div><!-- /.box-body -->
				  
        <div class="box-footer">
            <asp:Button ID="AcceptApprovalButton" runat="server" Text="Đồng ý" 
                        class="btn btn-primary" 
                    onclick="ApprovalBonusAdd_ApproveBonusAdd" />
            <asp:Button ID="CancelApprovalButton" runat="server" Text="Hủy bỏ" 
                        class="btn btn-primary" 
                    onclick="OnClosePopupWindow" />
        </div> 
        </asp:Panel>
        </ASPP:PopupWindow>
        </PopupWindow>
    </ASPP:PopupPanel>
</asp:Content>
