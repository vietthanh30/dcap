<%@ Page Title="Thưởng thêm" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewBonusAdd.aspx.cs" Inherits="web_app.admin.NewBonusAdd" %>
<%@ Register Assembly="ASP.Web.UI.PopupControl" Namespace="ASP.Web.UI.PopupControl"
    TagPrefix="ASPP" %>
<asp:Content ID="Content5" ContentPlaceHolderID="HeadContent" runat="server">
    <h1>
    Thưởng thêm
    </h1>
    <ol class="breadcrumb">
    <li><a href="#"><i class="fa fa-dashboard"></i> Trang chủ</a></li>
    <li class="active">Thưởng thêm</li>
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
				<div class="col-xs-4">
					<label for="IsApprovedSearch">Trạng thái</label>
					<asp:DropDownList ID="IsApprovedSearch" runat="server" class="form-control">
						<asp:ListItem Value="" Text="- Tất cả -" Selected="True"></asp:ListItem>
						<asp:ListItem Value="N" Text="Chưa duyệt"></asp:ListItem>
						<asp:ListItem Value="Y" Text="Đã duyệt"></asp:ListItem>
					</asp:DropDownList>
				</div>
			</div>
            </div><!-- /.box-body -->
				  
            <div class="box-footer">        
                <asp:Button ID="SearchButton" runat="server" Text="Tra cứu" 
                    class="btn btn-primary" onclick="NewBonusAdd_Search" 
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
	<div class="box-footer">
    <asp:Button ID="BtnAddNewBonus" runat="server" class="btn btn-primary" 
            Text="Thêm mới" onclick="NewBonusAdd_PreAddNewBonus" 
            />
    </div>
    <ASPP:PopupPanel HeaderText="Thêm mới Thưởng thêm" ID="NewBonusAddPopup" runat="server" OnCloseWindowClick="OnClosePopupWindow">
    <PopupWindow>
    <ASPP:PopupWindow ID="NewBonusAddWindow" runat="server">
            <asp:Panel runat="server" BorderStyle="Ridge" style="width: 600px; height: 300px">
                <div class="box-body">   
                <div class="row">
					<div class="col-xs-8">
                    <asp:Label ID="InvalidCredentialsMessage2" runat="server" class="failureNotification" ForeColor="Red"
                     Text="" Visible="False"></asp:Label>
                     </div>
                </div>          
                <div class="row">
					<div class="col-xs-8">
                    <span class="failureNotification">
                        <asp:Literal ID="ErrorMessage" runat="server"></asp:Literal>
                    </span>
                    <asp:ValidationSummary ID="NewBonusAddValidationSummary" runat="server" CssClass="failureNotification" 
                            ValidationGroup="NewBonusAddValidationGroup"/>  
                    </div>
                </div>          
                <div class="row">
					<div class="col-xs-8">
                    <asp:Label ID="AccountCode" runat="server" Text="" ForeColor="Blue" Visible="False"></asp:Label>
                    </div>
                </div>
				<div class="row">
					<div class="col-xs-4">
					<label for="AccountNumber">Id thành viên</label>
					<input type="text" class="form-control" maxlength="10" id="AccountNumber" runat="server" placeholder="Nhập ID thành viên">
                    <asp:RequiredFieldValidator ID="AccountNumberRequired" runat="server" ControlToValidate="AccountNumber" 
                        CssClass="failureNotification" ErrorMessage="Id thành viên bắt buộc nhập." ToolTip="Id thành viên bắt buộc nhập." 
                        ValidationGroup="NewBonusAddValidationGroup">*</asp:RequiredFieldValidator>
					</div>
					<div class="col-xs-4">
					<label for="BonusAmount">Điểm thưởng</label>
					<input type="text" class="form-control" maxlength="10" id="BonusAmount" runat="server" placeholder="Nhập Điểm thưởng">
                    <asp:RequiredFieldValidator ID="BonusAmountRequired" runat="server" ControlToValidate="BonusAmount" 
                        CssClass="failureNotification" ErrorMessage="Điểm thưởng bắt buộc nhập." ToolTip="Điểm thưởng bắt buộc nhập." 
                        ValidationGroup="NewBonusAddValidationGroup">*</asp:RequiredFieldValidator>
					</div>
				</div>                    
                </div><!-- /.box-body -->
				  
                <div class="box-footer">
                <asp:Button ID="AddNewBonusButton" runat="server" Text="Cập nhật" 
                        ValidationGroup="NewBonusAddValidationGroup" class="btn btn-primary" 
                    onclick="NewBonusAdd_AddNewBonus" />
                <asp:Button ID="CancelNewBonusButton" runat="server" Text="Hủy bỏ" 
                        class="btn btn-primary" 
                    onclick="OnClosePopupWindow" />
                </div> 
        </asp:Panel>
        </ASPP:PopupWindow>
        </PopupWindow>
    </ASPP:PopupPanel>
</asp:Content>
