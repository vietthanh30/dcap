USE [dcap]
GO
/****** Object:  Trigger [TR_ACCOUNT]    Script Date: 5/28/2015 5:29:37 PM ******/
DROP TRIGGER [dbo].[TR_ACCOUNT]
GO
/****** Object:  Table [dbo].[MEMBER_INFO]    Script Date: 5/28/2015 5:29:37 PM ******/
DROP TABLE [dbo].[MEMBER_INFO]
GO
/****** Object:  Table [dbo].[MANAGER_L6]    Script Date: 5/28/2015 5:29:37 PM ******/
DROP TABLE [dbo].[MANAGER_L6]
GO
/****** Object:  Table [dbo].[MANAGER_L5]    Script Date: 5/28/2015 5:29:37 PM ******/
DROP TABLE [dbo].[MANAGER_L5]
GO
/****** Object:  Table [dbo].[MANAGER_L4]    Script Date: 5/28/2015 5:29:37 PM ******/
DROP TABLE [dbo].[MANAGER_L4]
GO
/****** Object:  Table [dbo].[MANAGER_L3]    Script Date: 5/28/2015 5:29:37 PM ******/
DROP TABLE [dbo].[MANAGER_L3]
GO
/****** Object:  Table [dbo].[MANAGER_L2]    Script Date: 5/28/2015 5:29:37 PM ******/
DROP TABLE [dbo].[MANAGER_L2]
GO
/****** Object:  Table [dbo].[MANAGER_L1]    Script Date: 5/28/2015 5:29:37 PM ******/
DROP TABLE [dbo].[MANAGER_L1]
GO
/****** Object:  Table [dbo].[MANAGER_APPROVAL]    Script Date: 5/28/2015 5:29:37 PM ******/
DROP TABLE [dbo].[MANAGER_APPROVAL]
GO
/****** Object:  Table [dbo].[ACCOUNT_PRE_CALC]    Script Date: 5/28/2015 5:29:37 PM ******/
DROP TABLE [dbo].[ACCOUNT_PRE_CALC]
GO
/****** Object:  Table [dbo].[ACCOUNT_LOG]    Script Date: 5/28/2015 5:29:37 PM ******/
DROP TABLE [dbo].[ACCOUNT_LOG]
GO
/****** Object:  Table [dbo].[ACCOUNT_BONUS]    Script Date: 5/28/2015 5:29:37 PM ******/
DROP TABLE [dbo].[ACCOUNT_BONUS]
GO
/****** Object:  Table [dbo].[ACCOUNT]    Script Date: 5/28/2015 5:29:37 PM ******/
DROP TABLE [dbo].[ACCOUNT]
GO
/****** Object:  Table [dbo].[ACCOUNT]    Script Date: 5/28/2015 5:29:37 PM ******/
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
	[USER_ID] [numeric](10, 0) NULL,
	[CREATED_DATE] [date] NULL,
	[CREATED_BY] [nvarchar](50) NULL,
 CONSTRAINT [PK__ACCOUNT__05B22F600EAF5E43] PRIMARY KEY CLUSTERED 
(
	[ACCOUNT_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ACCOUNT_BONUS]    Script Date: 5/28/2015 5:29:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ACCOUNT_BONUS](
	[ID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[ACCOUNT_ID] [numeric](10, 0) NULL,
	[BONUS_TYPE] [nvarchar](3) NULL,
	[BONUS_AMOUNT] [numeric](10, 3) NULL,
	[MONTH] [nvarchar](6) NULL,
	[IS_PAID] [numeric](1, 0) NULL,
	[CREATED_DATE] [date] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ACCOUNT_LOG]    Script Date: 5/28/2015 5:29:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ACCOUNT_LOG](
	[ID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[ACCOUNT_ID] [numeric](10, 0) NULL,
	[DML] [varchar](1) NULL,
	[CREATED_DATE] [date] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ACCOUNT_PRE_CALC]    Script Date: 5/28/2015 5:29:37 PM ******/
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
	[LEVEL_INDEX] [numeric](10, 0) NULL,
	[IS_CALCULATED] [varchar](1) NULL,
	[CALCULATED_DATE] [date] NULL,
	[CREATED_DATE] [date] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MANAGER_APPROVAL]    Script Date: 5/28/2015 5:29:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MANAGER_APPROVAL](
	[ACCOUNT_ID] [numeric](10, 0) NULL,
	[MANAGER_LEVEL] [numeric](2, 0) NULL,
	[IS_APPROVED] [nvarchar](1) NULL,
	[APPROVED_DATE] [date] NULL,
	[APPROVED_BY] [numeric](10, 0) NULL,
	[CREATED_DATE] [date] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MANAGER_L1]    Script Date: 5/28/2015 5:29:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MANAGER_L1](
	[ACCOUNT_ID] [numeric](10, 0) NOT NULL,
	[PARENT_ID] [numeric](10, 0) NULL,
	[CHILD_INDEX] [numeric](5, 0) NULL,
	[ALL_CHILD] [nvarchar](30) NULL,
	[LEVEL] [numeric](10, 0) NULL,
	[LEVEL_INDEX] [numeric](10, 0) NULL,
	[IS_ACTIVE] [nvarchar](1) NULL,
	[CREATED_DATE] [date] NULL,
	[CREATED_BY] [nvarchar](50) NULL,
 CONSTRAINT [PK__MANAGER___05B22F605C850053] PRIMARY KEY CLUSTERED 
(
	[ACCOUNT_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MANAGER_L2]    Script Date: 5/28/2015 5:29:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MANAGER_L2](
	[ACCOUNT_ID] [numeric](10, 0) NOT NULL,
	[PARENT_ID] [numeric](10, 0) NULL,
	[CHILD_INDEX] [numeric](5, 0) NULL,
	[ALL_CHILD] [nvarchar](30) NULL,
	[LEVEL] [numeric](10, 0) NULL,
	[LEVEL_INDEX] [numeric](10, 0) NULL,
	[IS_ACTIVE] [nvarchar](1) NULL,
	[CREATED_DATE] [date] NULL,
	[CREATED_BY] [nvarchar](50) NULL,
 CONSTRAINT [PK__MANAGER___05B22F6050AB58C9] PRIMARY KEY CLUSTERED 
(
	[ACCOUNT_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MANAGER_L3]    Script Date: 5/28/2015 5:29:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MANAGER_L3](
	[ACCOUNT_ID] [numeric](10, 0) NOT NULL,
	[PARENT_ID] [numeric](10, 0) NULL,
	[CHILD_INDEX] [numeric](5, 0) NULL,
	[ALL_CHILD] [nvarchar](30) NULL,
	[LEVEL] [numeric](10, 0) NULL,
	[LEVEL_INDEX] [numeric](10, 0) NULL,
	[IS_ACTIVE] [nvarchar](1) NULL,
	[CREATED_DATE] [date] NULL,
	[CREATED_BY] [nvarchar](50) NULL,
 CONSTRAINT [PK__MANAGER___05B22F6059F3E2D9] PRIMARY KEY CLUSTERED 
(
	[ACCOUNT_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MANAGER_L4]    Script Date: 5/28/2015 5:29:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MANAGER_L4](
	[ACCOUNT_ID] [numeric](10, 0) NOT NULL,
	[PARENT_ID] [numeric](10, 0) NULL,
	[CHILD_INDEX] [numeric](5, 0) NULL,
	[ALL_CHILD] [nvarchar](30) NULL,
	[LEVEL] [numeric](10, 0) NULL,
	[LEVEL_INDEX] [numeric](10, 0) NULL,
	[IS_ACTIVE] [nvarchar](1) NULL,
	[CREATED_DATE] [date] NULL,
	[CREATED_BY] [nvarchar](50) NULL,
 CONSTRAINT [PK__MANAGER___05B22F6095B8197C] PRIMARY KEY CLUSTERED 
(
	[ACCOUNT_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MANAGER_L5]    Script Date: 5/28/2015 5:29:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MANAGER_L5](
	[ACCOUNT_ID] [numeric](10, 0) NOT NULL,
	[PARENT_ID] [numeric](10, 0) NULL,
	[CHILD_INDEX] [numeric](5, 0) NULL,
	[ALL_CHILD] [nvarchar](30) NULL,
	[LEVEL] [numeric](10, 0) NULL,
	[LEVEL_INDEX] [numeric](10, 0) NULL,
	[IS_ACTIVE] [nvarchar](1) NULL,
	[CREATED_DATE] [date] NULL,
	[CREATED_BY] [nvarchar](50) NULL,
 CONSTRAINT [PK__MANAGER___05B22F60B48155CE] PRIMARY KEY CLUSTERED 
(
	[ACCOUNT_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MANAGER_L6]    Script Date: 5/28/2015 5:29:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MANAGER_L6](
	[ACCOUNT_ID] [numeric](10, 0) NOT NULL,
	[PARENT_ID] [numeric](10, 0) NULL,
	[CHILD_INDEX] [numeric](5, 0) NULL,
	[ALL_CHILD] [nvarchar](30) NULL,
	[LEVEL] [numeric](10, 0) NULL,
	[LEVEL_INDEX] [numeric](10, 0) NULL,
	[IS_ACTIVE] [nvarchar](1) NULL,
	[CREATED_DATE] [date] NULL,
	[CREATED_BY] [nvarchar](50) NULL,
 CONSTRAINT [PK__MANAGER___05B22F60DDE33673] PRIMARY KEY CLUSTERED 
(
	[ACCOUNT_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MEMBER_INFO]    Script Date: 5/28/2015 5:29:37 PM ******/
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
	[IMAGE_URL] [varchar](200) NULL,
	[CREATED_DATE] [date] NULL,
	[CREATED_BY] [nvarchar](50) NULL,
 CONSTRAINT [PK__MEMBER_I__B1223B70DE783716] PRIMARY KEY CLUSTERED 
(
	[MEMBER_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UQ__MEMBER_I__1AA3A82453F97A4D] UNIQUE NONCLUSTERED 
(
	[SO_CMND] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Trigger [dbo].[TR_ACCOUNT]    Script Date: 5/28/2015 5:29:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE TRIGGER [dbo].[TR_ACCOUNT] 
   ON  [dbo].[ACCOUNT]
   AFTER INSERT,DELETE
AS 
DECLARE @ACCOUNT_ID numeric(10,0);
BEGIN
	if exists(select * from inserted)
	begin
		select @ACCOUNT_ID = account_id from inserted;
		insert into ACCOUNT_LOG(account_id, dml, created_date) values (@ACCOUNT_ID,'I',GETDATE());
	end
	
	if exists(select * from deleted)
	begin
		select @ACCOUNT_ID = account_id from deleted;
		insert into ACCOUNT_LOG(account_id, dml, created_date) values (@ACCOUNT_ID,'D',GETDATE());
	end

END

GO
