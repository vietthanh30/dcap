USE [dcap]
GO

/****** Object:  Table [dbo].[BONUS_APPROVAL]    Script Date: 6/15/2015 10:09:23 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[BONUS_APPROVAL](
	[ID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[BONUS_TYPE] [nvarchar](3) NULL,
	[BONUS_AMOUNT] [numeric](10, 3) NULL,
	[ACCOUNT_ID] [numeric](10, 0) NULL,
	[IS_APPROVED] [varchar](1) NULL,
	[CREATED_BY] [nvarchar](50) NULL,
	[CREATED_DATE] [date] NULL,
	[APPROVED_BY] [nvarchar](50) NULL,
	[APPROVED_DATE] [date] NULL,
 CONSTRAINT [PK_BONUS_APPROVAL] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


