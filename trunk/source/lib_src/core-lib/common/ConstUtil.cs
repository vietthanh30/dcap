using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace core_lib.common
{
    public class ConstUtil
    {
        public const string COLUMN_NAMES = "COLUMN_NAMES";
        public const string SOURCE_TABLE = "SOURCE_TABLE";
        public const string TABLE_INDEX = "TABLE_INDEX";
        public const string IS_HEADER = "IS_HEADER";
        public const string INCLUDE_HEADER = "INCLUDE_HEADER";

        public const string DEFAULT_PASSWORD = "12345";
        public const string ACTIVE_STATUS = "Y";
        public const string INACTIVE_STATUS = "N";

        public const string QTHT = "QTHT";
        public const string QLKT = "QLKT";
        public const string QLTV = "QLTV";

        public const string MONTH_FORMAT = "yyyyMM";

		public const string MA_GT_NAM = "M";
        public const string MA_GT_NU = "F";

		public const string TEN_GT_NAM = "Nam";
        public const string TEN_GT_NU = "Nữ";

        public static BonusType BONUS_TYPE_CAN_CAP = new BonusType
                                                          {
                                                              Type = "CC",
                                                              Amount = 0.2
                                                          };

        public static BonusType BONUS_TYPE_HE_THONG = new BonusType
        {
            Type = "HT",
            Amount = 0.2
        };

        public static BonusType BONUS_TYPE_MA_ROI = new BonusType
        {
            Type = "MR",
            Amount = 0.2

        };

        public static BonusType BONUS_TYPE_QL1 = new BonusType
                                                            {
                                                                Type = "QL1",
                                                                Amount = 1

                                                            };
        public static BonusType BONUS_TYPE_CCL1 = new BonusType
        {
            Type = "CC1",
            Amount = 1

        };

        public static BonusType BONUS_TYPE_QL2 = new BonusType
                                                          {
                                                              Type = "QL2",
                                                              Amount = 1

                                                          };

        public static BonusType BONUS_TYPE_CCL2 = new BonusType
        {
            Type = "CC2",
            Amount = 1
        };

        public static BonusType BONUS_TYPE_QL3 = new BonusType
                                                          {
                                                              Type = "QL3",
                                                              Amount = 1
                                                          };

        public static BonusType BONUS_TYPE_CCL3 = new BonusType
        {
            Type = "CC3",
            Amount = 1
        };

        public static BonusType BONUS_TYPE_QL4 = new BonusType
                                                          {
                                                              Type = "QL4",
                                                              Amount = 1
                                                          };
        public static BonusType BONUS_TYPE_CCL4 = new BonusType
        {
            Type = "CC4",
            Amount = 1
        };

        public static BonusType BONUS_TYPE_QL5 = new BonusType
                                                          {
                                                              Type = "QL5",
                                                              Amount = 1
                                                          };

        public static BonusType BONUS_TYPE_CCL5 = new BonusType
        {
            Type = "CC5",
            Amount = 1
        };

        public static BonusType BONUS_TYPE_QL6 = new BonusType
                                                          {
                                                              Type = "QL6",
                                                              Amount = 1
                                                          };

        public static BonusType BONUS_TYPE_CCL6 = new BonusType
        {
            Type = "CC6",
            Amount = 1
        };

    }
}
