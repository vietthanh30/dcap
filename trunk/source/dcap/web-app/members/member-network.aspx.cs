using System;
using System.Data;
using web_app.common;
using web_app.DcapServiceReference;

namespace web_app.members
{
    public partial class member_network_ext : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var userDto = (UserDto)Session["UserDto"];
            if (userDto == null)
            {
                Response.Redirect("~/admin/Login.aspx");
            }
        }

        protected void MemberNetwork_SearchNetwork(object sender, EventArgs e)
        {
            OnSearchNetwork();
        }

        private void OnSearchNetwork()
        {
            var userDto = (UserDto)Session["UserDto"];
            if (userDto == null)
            {
                Response.Redirect("~/admin/Login.aspx");
                return;
            }
            var rootNumber = userDto.AccountNumber;
            var idMember = IdMember.Value.Trim();
            MemberNodeDto[] allMemberNodeDto;
            if (!DcapServiceUtil.IsContainMemberNode(rootNumber, idMember))
            {
                allMemberNodeDto = new MemberNodeDto[0];
                InvalidCredentialsMessage.Text = "Id không thuộc quản lý của thành viên.";
                InvalidCredentialsMessage.Visible = true;
            }
            else
            {
                allMemberNodeDto = DcapServiceUtil.SearchMemberNodeDto(idMember);
                if (allMemberNodeDto.Length == 0)
                {
                    InvalidCredentialsMessage.Text = "Không tồn tại cây thành viên " + idMember;
                    InvalidCredentialsMessage.Visible = true;
                }
                else
                {
                    InvalidCredentialsMessage.Visible = false;
                }
            }
            var headerNames = new[] { "AccountId", "ParentId", "Description" };
            var columnTypes = new[] {typeof (long), typeof (long), typeof (string)};
            var ds = CreateMemberNodeDataSet(allMemberNodeDto, headerNames, columnTypes);
            var parentDirectNodeDto = DcapServiceUtil.GetParentDirectNodeByChildNo(idMember);
            if (parentDirectNodeDto == null)
            {
                ParentDirectInfo.Text = "";
            }
            else
            {
                ParentDirectInfo.Text = "Người giới thiệu: " + parentDirectNodeDto.Description;
            }
            var parentNodeDto = DcapServiceUtil.GetParentNodeByChildNo(idMember);
            long parentId;
            if (parentNodeDto == null)
            {
                parentId = -1;
                ParentInfo.Text = "";
            } else
            {
                parentId = parentNodeDto.AccountId;
                ParentInfo.Text = "Tuyến trên: " + parentNodeDto.Description;
            }
            TreeThanhVien.DataSource = new HierarchicalDataSet(ds, "AccountId", "ParentId", parentId);
            TreeThanhVien.DataBind();
            TreeThanhVien.CollapseAll();
        }

        private DataSet CreateMemberNodeDataSet(MemberNodeDto[] allMemberNodeDto, string[] headerNames, Type[] columnTypes)
        {
            var dataSet = new DataSet();
            dataSet.Tables.Add("MEMBER_NODES");
            for (int i = 0; i < headerNames.Length; i++)
            {
                dataSet.Tables[0].Columns.Add(headerNames[i], columnTypes[i]);
            }
            foreach (var bangKeDto in allMemberNodeDto)
            {
                var dataRow = dataSet.Tables[0].NewRow();
                int index = 0;
                dataRow[headerNames[index++]] = bangKeDto.AccountId;
                dataRow[headerNames[index++]] = bangKeDto.ParentId;
                dataRow[headerNames[index]] = bangKeDto.Description;
                dataSet.Tables[0].Rows.Add(dataRow);
            }
            return dataSet;
        }
    }
}