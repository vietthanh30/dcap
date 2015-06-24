using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using web_app.common;
using web_app.DcapServiceReference;

namespace web_app.main_pages
{
    public partial class member_network : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var userDto = (UserDto)Session["UserDto"];
            if (userDto == null)
            {
                Response.Redirect("~/admin/Login.aspx");
                return;
            }
            if (!UserUtil.IsQthtRole(userDto) && !UserUtil.IsQlktRole(userDto))
            {
                Response.Redirect("~/Default.aspx");
                return;
            }
            if (!IsPostBack)
            {
                OnSearchNetwork();
            }
        }

        protected void MemberNetwork_SearchNetwork(object sender, EventArgs e)
        {
            OnSearchNetwork();
        }

		private void ResetMemberTreeView(out MemberNodeDto[] allMemberNodeDto)
		{
			allMemberNodeDto = new MemberNodeDto[0];
			ParentInfo.Text = "";
			ParentDirectInfo.Text = "";
		}

        private void OnSearchNetwork()
        {
			MemberNodeDto[] allMemberNodeDto;
            var idMember = IdMember.Value.Trim();
            long parentId = -1;
            if(!string.IsNullOrEmpty(idMember) && !DcapServiceUtil.IsValidAccountNumber(idMember))
            {
                InvalidCredentialsMessage.Text = "Id thành viên không đúng định dạng";
                InvalidCredentialsMessage.Visible = true;
                ResetMemberTreeView(out allMemberNodeDto);
            } else 
			{
				allMemberNodeDto = DcapServiceUtil.SearchMemberNodeDto(idMember);
				if (allMemberNodeDto.Length == 0)
				{
					InvalidCredentialsMessage.Text = "Không tồn tại cây thành viên " + idMember;
					InvalidCredentialsMessage.Visible = true;
					ResetMemberTreeView(out allMemberNodeDto);
				}
				else
				{
					InvalidCredentialsMessage.Visible = false;
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
					if (parentNodeDto == null)
					{
						ParentInfo.Text = "";
					} else
					{
						parentId = parentNodeDto.AccountId;
						ParentInfo.Text = "Tuyến trên: " + parentNodeDto.Description;
					}
				}
			}			
            var headerNames = new[] { "AccountId", "ParentId", "Description" };
            var columnTypes = new[] {typeof (long), typeof (long), typeof (string)};
            var ds = CreateMemberNodeDataSet(allMemberNodeDto, headerNames, columnTypes);
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