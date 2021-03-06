USE [dcap]
GO
/****** Object:  Table [dbo].[ACCOUNT]    Script Date: 5/26/2015 2:32:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ACCOUNT](
	[ACCOUNT_ID] [numeric](10, 0) IDENTITY(1,1) NOT NULL,
	[PARENT_ID] [numeric](10, 0) NULL,
	[PARENT_DIRECT_ID] [numeric](10, 0) NULL,
	[ACCOUNT_NUMBER] [numeric](7, 0) NULL,
	[CHILD_INDEX] [numeric](5, 0) NULL,
	[IS_ACTIVE] [nvarchar](1) NULL,
	[MEMBER_ID] [numeric](10, 0) NULL,
	[CREATED_DATE] [date] NULL,
	[CREATED_BY] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[ACCOUNT_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ACCOUNT_BONUS]    Script Date: 5/26/2015 2:32:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ACCOUNT_BONUS](
	[ACCOUNT_ID] [numeric](10, 0) NULL,
	[BONUS_TYPE] [nvarchar](3) NULL,
	[BONUS_AMOUNT] [numeric](10, 3) NULL,
	[MONTH] [nvarchar](6) NULL,
	[IS_PAID] [numeric](1, 0) NULL,
	[CREATED_DATE] [date] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ACCOUNT_LOG]    Script Date: 5/26/2015 2:32:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ACCOUNT_LOG](
	[ACCOUNT_ID] [numeric](10, 0) NULL,
	[DML] [varchar](1) NULL,
	[CREATED_DATE] [date] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ACCOUNT_PRE_CALC]    Script Date: 5/26/2015 2:32:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ACCOUNT_PRE_CALC](
	[ACCOUNT_ID] [numeric](10, 0) NULL,
	[CALC_ACCOUNT_ID] [numeric](10, 0) NULL,
	[ACCOUNT_LEVEL] [int] NULL,
	[LOCATION] [numeric](10, 0) NULL,
	[IS_CALCULATED] [varchar](1) NULL,
	[CALCULATED_DATE] [date] NULL,
	[CREATED_DATE] [date] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MANAGER_APPROVE]    Script Date: 5/26/2015 2:32:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MANAGER_APPROVE](
	[ACCOUNT_ID] [numeric](10, 0) NULL,
	[MANAGER_LEVEL] [numeric](2, 0) NULL,
	[IS_APPROVED] [nvarchar](1) NULL,
	[APPROVED_DATE] [date] NULL,
	[APPROVED_BY] [numeric](10, 0) NULL,
	[CREATED_DATE] [date] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MANAGER_L1]    Script Date: 5/26/2015 2:32:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MANAGER_L1](
	[ACCOUNT_ID] [numeric](10, 0) IDENTITY(1,1) NOT NULL,
	[PARENT_ID] [numeric](10, 0) NULL,
	[CHILD_INDEX] [numeric](5, 0) NULL,
	[ALL_CHILD] [nvarchar](30) NULL,
	[IS_ACTIVE] [nvarchar](1) NULL,
	[CREATED_DATE] [date] NULL,
	[CREATED_BY] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[ACCOUNT_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MANAGER_L1_PRE_CALC]    Script Date: 5/26/2015 2:32:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MANAGER_L1_PRE_CALC](
	[ACCOUNT_ID] [numeric](10, 0) NULL,
	[CALC_ACCOUNT_ID] [numeric](10, 0) NULL,
	[ACCOUNT_LEVEL] [int] NULL,
	[LOCATION] [numeric](10, 0) NULL,
	[IS_CALCULATED] [nvarchar](1) NULL,
	[CALCULATED_DATE] [date] NULL,
	[CREATED_DATE] [date] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MANAGER_L2]    Script Date: 5/26/2015 2:32:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MANAGER_L2](
	[ACCOUNT_ID] [numeric](10, 0) IDENTITY(1,1) NOT NULL,
	[PARENT_ID] [numeric](10, 0) NULL,
	[CHILD_INDEX] [numeric](5, 0) NULL,
	[ALL_CHILD] [nvarchar](30) NULL,
	[IS_ACTIVE] [nvarchar](1) NULL,
	[CREATED_DATE] [date] NULL,
	[CREATED_BY] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[ACCOUNT_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MANAGER_L2_PRE_CALC]    Script Date: 5/26/2015 2:32:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MANAGER_L2_PRE_CALC](
	[ACCOUNT_ID] [numeric](10, 0) NULL,
	[CALC_ACCOUNT_ID] [numeric](10, 0) NULL,
	[ACCOUNT_LEVEL] [int] NULL,
	[LOCATION] [numeric](10, 0) NULL,
	[IS_CALCULATED] [nvarchar](1) NULL,
	[CALCULATED_DATE] [date] NULL,
	[CREATED_DATE] [date] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MANAGER_L3]    Script Date: 5/26/2015 2:32:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MANAGER_L3](
	[ACCOUNT_ID] [numeric](10, 0) IDENTITY(1,1) NOT NULL,
	[PARENT_ID] [numeric](10, 0) NULL,
	[CHILD_INDEX] [numeric](5, 0) NULL,
	[ALL_CHILD] [nvarchar](30) NULL,
	[IS_ACTIVE] [nvarchar](1) NULL,
	[CREATED_DATE] [date] NULL,
	[CREATED_BY] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[ACCOUNT_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MANAGER_L3_PRE_CALC]    Script Date: 5/26/2015 2:32:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MANAGER_L3_PRE_CALC](
	[ACCOUNT_ID] [numeric](10, 0) NULL,
	[CALC_ACCOUNT_ID] [numeric](10, 0) NULL,
	[ACCOUNT_LEVEL] [int] NULL,
	[LOCATION] [numeric](10, 0) NULL,
	[IS_CALCULATED] [nvarchar](1) NULL,
	[CALCULATED_DATE] [date] NULL,
	[CREATED_DATE] [date] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MANAGER_L4]    Script Date: 5/26/2015 2:32:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MANAGER_L4](
	[ACCOUNT_ID] [numeric](10, 0) IDENTITY(1,1) NOT NULL,
	[PARENT_ID] [numeric](10, 0) NULL,
	[CHILD_INDEX] [numeric](5, 0) NULL,
	[ALL_CHILD] [nvarchar](30) NULL,
	[IS_ACTIVE] [nvarchar](1) NULL,
	[CREATED_DATE] [date] NULL,
	[CREATED_BY] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[ACCOUNT_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MANAGER_L4_PRE_CALC]    Script Date: 5/26/2015 2:32:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MANAGER_L4_PRE_CALC](
	[ACCOUNT_ID] [numeric](10, 0) NULL,
	[CALC_ACCOUNT_ID] [numeric](10, 0) NULL,
	[ACCOUNT_LEVEL] [int] NULL,
	[LOCATION] [numeric](10, 0) NULL,
	[IS_CALCULATED] [nvarchar](1) NULL,
	[CALCULATED_DATE] [date] NULL,
	[CREATED_DATE] [date] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MANAGER_L5]    Script Date: 5/26/2015 2:32:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MANAGER_L5](
	[ACCOUNT_ID] [numeric](10, 0) IDENTITY(1,1) NOT NULL,
	[PARENT_ID] [numeric](10, 0) NULL,
	[CHILD_INDEX] [numeric](5, 0) NULL,
	[ALL_CHILD] [nvarchar](30) NULL,
	[IS_ACTIVE] [nvarchar](1) NULL,
	[CREATED_DATE] [date] NULL,
	[CREATED_BY] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[ACCOUNT_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MANAGER_L5_PRE_CALC]    Script Date: 5/26/2015 2:32:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MANAGER_L5_PRE_CALC](
	[ACCOUNT_ID] [numeric](10, 0) NULL,
	[CALC_ACCOUNT_ID] [numeric](10, 0) NULL,
	[ACCOUNT_LEVEL] [int] NULL,
	[LOCATION] [numeric](10, 0) NULL,
	[IS_CALCULATED] [nvarchar](1) NULL,
	[CALCULATED_DATE] [date] NULL,
	[CREATED_DATE] [date] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MANAGER_L6]    Script Date: 5/26/2015 2:32:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MANAGER_L6](
	[ACCOUNT_ID] [numeric](10, 0) IDENTITY(1,1) NOT NULL,
	[PARENT_ID] [numeric](10, 0) NULL,
	[CHILD_INDEX] [numeric](5, 0) NULL,
	[ALL_CHILD] [nvarchar](30) NULL,
	[IS_ACTIVE] [nvarchar](1) NULL,
	[CREATED_DATE] [date] NULL,
	[CREATED_BY] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[ACCOUNT_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MANAGER_L6_PRE_CALC]    Script Date: 5/26/2015 2:32:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MANAGER_L6_PRE_CALC](
	[ACCOUNT_ID] [numeric](10, 0) NULL,
	[CALC_ACCOUNT_ID] [numeric](10, 0) NULL,
	[ACCOUNT_LEVEL] [int] NULL,
	[LOCATION] [numeric](10, 0) NULL,
	[IS_CALCULATED] [nvarchar](1) NULL,
	[CALCULATED_DATE] [date] NULL,
	[CREATED_DATE] [date] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MANAGER_LOG]    Script Date: 5/26/2015 2:32:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MANAGER_LOG](
	[ACCOUNT_ID] [numeric](10, 0) NULL,
	[DML] [nvarchar](1) NULL,
	[MANAGER_LEVEL] [numeric](2, 0) NULL,
	[CREATED_DATE] [date] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MEMBER_INFO]    Script Date: 5/26/2015 2:32:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MEMBER_INFO](
	[MEMBER_ID] [numeric](10, 0) NOT NULL,
	[HO_TEN] [nvarchar](100) NOT NULL,
	[NGAY_SINH] [date] NULL,
	[SO_CMND] [varchar](15) NOT NULL,
	[NGAY_CAP] [date] NULL,
	[SO_DIEN_THOAI] [varchar](15) NULL,
	[DIA_CHI] [nvarchar](500) NULL,
	[GIOI_TINH] [nvarchar](1) NULL,
	[SO_TAI_KHOAN] [varchar](15) NULL,
	[CHI_NHANH_NH] [nvarchar](100) NULL,
	[USER_NAME] [varchar](30) NOT NULL,
	[CREATED_DATE] [date] NULL,
	[CREATED_BY] [nvarchar](50) NULL,
 CONSTRAINT [PK__MEMBER_I__B1223B70DE783716] PRIMARY KEY CLUSTERED 
(
	[MEMBER_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UQ__MEMBER_I__1AA3A82453F97A4D] UNIQUE NONCLUSTERED 
(
	[SO_CMND] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UQ__MEMBER_I__8005F5FBA291CC53] UNIQUE NONCLUSTERED 
(
	[USER_NAME] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
