using System;

namespace ws_server.model
{
    public class MemberInfo
    {
        #region Declarations

        // Property variables
        private int member_ID = -1;

        // Member variables
        private string ho_Ten = String.Empty;

        private DateTime ngay_Sinh = default(DateTime);

        private String so_Cmnd = String.Empty;

        private String so_Dien_Thoai = String.Empty;

        private String so_Tai_Khoan = String.Empty;

        private String chi_Nhanh_NH = String.Empty;

        private String member_Code = String.Empty;

        #endregion

    	#region Constructor

        public MemberInfo()
        {
        }

    	#endregion

        #region Properties

        /// <summary>
        /// Member ID
        /// </summary>
        public virtual int MemberID
        {
            get { return member_ID; }
            set { member_ID = value; }
        }

        /// <summary>
        /// Ho Ten.
        /// </summary>
        public virtual string HoTen
        {
            get { return ho_Ten; }
            set { ho_Ten = value; }
        }

        /// <summary>
        /// Ngay Sinh
        /// </summary>
        public virtual DateTime NgaySinh
        {
            get { return ngay_Sinh; }
            set { ngay_Sinh = value; }
        }

        /// <summary>
        /// So Cmnd
        /// </summary>
        public virtual String SoCmnd
        {
            get { return so_Cmnd; }
            set { so_Cmnd = value; }
        }

        /// <summary>
        /// So Dien Thoai
        /// </summary>
        public virtual String SoDienThoai
        {
            get { return so_Dien_Thoai; }
            set { so_Dien_Thoai = value; }
        }

        /// <summary>
        /// So Tai Khoan
        /// </summary>
        public virtual String SoTaiKhoan
        {
            get { return so_Tai_Khoan; }
            set { so_Tai_Khoan = value; }
        }

        /// <summary>
        /// Chi Nhanh NH
        /// </summary>
        public virtual String ChiNhanhNH
        {
            get { return chi_Nhanh_NH; }
            set { chi_Nhanh_NH = value; }
        }

        /// <summary>
        /// Member Code
        /// </summary>
        public virtual String MemberCode
        {
            get { return member_Code; }
            set { member_Code = value; }
        }

        #endregion

        #region Method Overrides

        public override string ToString()
        {
            return member_Code;
        }

        #endregion
    }
}