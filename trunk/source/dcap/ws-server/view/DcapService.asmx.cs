﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using ws_server.controller;
using ws_server.model;

namespace ws_server.view
{
    /// <summary>
    /// Summary description for DcapService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class DcapService : System.Web.Services.WebService
    {
        private Controller controller = new Controller();

        [WebMethod]
        public string login(String userName, String password)
        {

            return controller.login(userName, password);
        }

        [WebMethod]
        public string changePassword(String userName, String oldPassword, String newPassword, String confirmPassword)
        {
            return controller.changePassword(userName, oldPassword, newPassword, confirmPassword);
        }

        [WebMethod]
        public string CreateUser(String parentId, String directParentId, String userName, String ngaySinh, String soCmnd, String diaChi, String soTaiKhoan,
            String chiNhanhNH, String photoUrl)
        {
            return controller.CreateUser(parentId, directParentId, userName, ngaySinh, soCmnd, diaChi, soTaiKhoan, chiNhanhNH, photoUrl);
        }

        [WebMethod]
        public string SearchUser(String parentId, String directParentId, String userName, String ngaySinh, String soCmnd, String diaChi, String soTaiKhoan,
            String chiNhanhNH, String photoUrl)
        {
            return controller.SearchUser(parentId, directParentId, userName, ngaySinh, soCmnd, diaChi, soTaiKhoan, chiNhanhNH, photoUrl);
        }

        [WebMethod]
        public AccountLog[] CalculateAccountLog()
        {
            return controller.CalculateAccountLog().ToArray();
        }

        
    }
}