USE [dcap]
GO
/****** Object:  Table [dbo].[ACCOUNT_PRE_CALC]    Script Date: 5/30/2015 8:48:25 AM ******/
DROP TABLE [dbo].[ACCOUNT_PRE_CALC]
GO
/****** Object:  Table [dbo].[ACCOUNT_LOG_HIST]    Script Date: 5/30/2015 8:48:25 AM ******/
DROP TABLE [dbo].[ACCOUNT_LOG_HIST]
GO
/****** Object:  Table [dbo].[ACCOUNT_LOG_HIST]    Script Date: 5/30/2015 8:48:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ACCOUNT_LOG_HIST](
	[ID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[ACCOUNT_ID] [numeric](10, 0) NULL,
	[DML] [varchar](1) NULL,
	[ERROR] [text] NULL,
	[CREATED_DATE] [datetime] NULL,
 CONSTRAINT [PK_ACCOUNT_LOG_HIST] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ACCOUNT_PRE_CALC]    Script Date: 5/30/2015 8:48:25 AM ******/
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
	[BONUS_TYPE] [nvarchar](2) NULL,
	[IS_CALCULATED] [varchar](1) NULL,
	[CALCULATED_DATE] [date] NULL,
	[CREATED_DATE] [date] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
