using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using core_lib.common;
using web_app.common;
using web_app.DcapServiceReference;

namespace web_app
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
            UpdateUserInfo(userDto);
            if (!IsPostBack)
            {
                OnSearchNetwork();
            }
        }

        private void UpdateUserInfo(UserDto userDto)
        {
            var imageUrl = userDto.ImageUrl;
            if (String.IsNullOrEmpty(imageUrl))
            {
                imageUrl = "~/dist/img/avatar5.png";
            }
            var headLoginName = userDto.FullName;
            string headLoginNameAdmin;
            var roleCode = GetRoleCode(userDto);
            if (String.IsNullOrEmpty(roleCode))
            {
                headLoginNameAdmin = userDto.FullName;
            }
            else
            {
                headLoginNameAdmin = userDto.FullName + " - " + roleCode;
            }
            HeadLoginImg1.Src = imageUrl;
            HeadLoginImg2.Src = imageUrl;
            HeadLoginImg3.Src = imageUrl;
            HeadLoginName1.Text = headLoginName;
            HeadLoginName2.Text = headLoginNameAdmin;
            HeadLoginName3.Text = headLoginName;
            UpdateLeftPanel(roleCode);
        }

        private void UpdateLeftPanel(string roleCode)
        {
            if (string.Compare(ConstUtil.QTHT, roleCode, true) == 0)
            {
                LeftContentAdmin.Visible = true;
                LeftContentKT.Visible = false;
                LeftContentTV.Visible = false;
            }
            if (string.Compare(ConstUtil.QLKT, roleCode, true) == 0)
            {
                LeftContentAdmin.Visible = false;
                LeftContentKT.Visible = true;
                LeftContentTV.Visible = false;
            }
            if (string.Compare(ConstUtil.QLTV, roleCode, true) == 0)
            {
                LeftContentAdmin.Visible = false;
                LeftContentKT.Visible = false;
                LeftContentTV.Visible = true;
            }
        }

        private string GetRoleCode(UserDto userDto)
        {
            var allRoles = userDto.AllRoles;
            if (allRoles.Length == 0)
            {
                return "QLTV";
            }
            return allRoles[0].RoleCode;
        }

        protected void MemberNetwork_SearchNetwork(object sender, EventArgs e)
        {
            OnSearchNetwork();
        }

        private void OnSearchNetwork()
        {
            var idMember = IdMember.Value.Trim();
            var allMemberNodeDto = DcapServiceUtil.SearchMemberNodeDto(idMember);
            var parentNodeDto = DcapServiceUtil.GetParentNodeByChildNo(idMember);
            long parentId;
            string description;
            if (parentNodeDto == null)
            {
                parentId = -1;
                description = "Tuyến gốc";
            }
            else
            {
                parentId = parentNodeDto.AccountId;
                description = GetNodeDescription(parentNodeDto.Description);
            }
            long count = 0;
            ltrTree.Text = "<div id='sptree" + parentId + "'>" + description + "</div><ul>";
            DrawTree(allMemberNodeDto.ToList(), parentId, count);
            ltrTree.Text += "</ul>";
        }

        private string GetNodeDescription(string description)
        {
            if (String.IsNullOrEmpty(description))
            {
                return description;
            }
            var arr1 = description.Split(new [] {'|'});
            var fullName = arr1[0];
            var accountInfo = arr1[1].Substring(arr1[1].IndexOf(" ") + 1);
            return fullName + accountInfo;
        }

        public void DrawTree(List<MemberNodeDto> lstDto, long noteId, long count)
        {
            foreach(MemberNodeDto note in lstDto)
            {
                count++;
                string selected = "";
                if (count == 1)
                {
                    selected = " class='selected'";
                }
                if (note.ParentId == noteId)
                {
                    ltrTree.Text += "<li><div id='sptree" + note.AccountId + "'" + selected + ">" + GetNodeDescription(note.Description);
                    if (HasChild(lstDto, note.AccountId))
                    {
                        ltrTree.Text += "</div><ul>";
                        DrawTree(lstDto, note.AccountId, count);
                        ltrTree.Text += "</ul></li>";
                    }
                }
           }
        }

        public bool HasChild(List<MemberNodeDto> lstDto, long noteId)
        {
            int count = (from p in lstDto
                         where p.ParentId == noteId
                         select p).Count();
            return count > 0 ? true : false;
        }
    }
}