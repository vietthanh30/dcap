using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using web_app.common;
using web_app.DcapServiceReference;

namespace web_app
{
    public partial class TreeDemo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                OnSearchNetwork();
            }
        }

        private void OnSearchNetwork()
        {
            var idMember = "-1";
            var allMemberNodeDto = DcapServiceUtil.SearchMemberNodeDto(idMember);
            ltrTree.Text += "<div id='" + idMember + "' class='selected'>" + idMember + "</div><ul>";
            DrawTree(allMemberNodeDto.ToList(), long.Parse(idMember));
            ltrTree.Text += "</ul>";
        }
        public void DrawTree(List<MemberNodeDto> lstDto, long noteID)
        {
            foreach(MemberNodeDto note in lstDto)
            {
                if (note.ParentId == noteID)
                {
                    ltrTree.Text += "<li><div id='" + noteID + "'>" + note.AccountId;
                    if (HasChild(lstDto, note.AccountId))
                    {
                        ltrTree.Text += "</div><ul>";
                        DrawTree(lstDto, note.AccountId);
                        ltrTree.Text += "</ul></li>";
                    }
                }
           }
        }

        public bool HasChild(List<MemberNodeDto> lstDto, long noteID)
        {
            int Count = (from p in lstDto
                         where p.ParentId == noteID
                         select p).Count();
            return Count > 0 ? true : false;
        }
    }
}