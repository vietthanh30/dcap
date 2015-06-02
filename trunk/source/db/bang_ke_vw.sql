USE [dcap]
GO

/****** Object:  View [dbo].[BANG_KE_VW]    Script Date: 06/02/2015 17:55:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[BANG_KE_VW]
AS
SELECT     ROW_NUMBER() over(order by SUM(ab.BONUS_AMOUNT)) as STT, child.HO_TEN AS TEN_NHAN_VIEN, child.GIOI_TINH, child.SO_CMND, child.NGAY_CAP, child.DIA_CHI, child.SO_TAI_KHOAN, child.CHI_NHANH_NH, 
                      child.SO_DIEN_THOAI, child.CREATED_DATE AS NGAY_DANG_KY, parent.HO_TEN AS NGUOI_BAO_TRO, SUM(ab.BONUS_AMOUNT) * 1000000 AS TONG, 
                      ab.MONTH AS THANG, ab.ACCOUNT_ID
FROM         dbo.ACCOUNT_BONUS AS ab INNER JOIN
                      dbo.ACCOUNT AS ca ON ab.ACCOUNT_ID = ca.ACCOUNT_ID INNER JOIN
                      dbo.MEMBER_INFO AS child ON ca.MEMBER_ID = child.MEMBER_ID LEFT OUTER JOIN
                      dbo.ACCOUNT AS pa ON ca.PARENT_ID = pa.ACCOUNT_ID LEFT OUTER JOIN
                      dbo.MEMBER_INFO AS parent ON pa.MEMBER_ID = parent.MEMBER_ID
GROUP BY child.HO_TEN, child.GIOI_TINH, child.SO_CMND, child.NGAY_CAP, child.DIA_CHI, child.SO_TAI_KHOAN, child.CHI_NHANH_NH, child.SO_DIEN_THOAI, 
                      child.CREATED_DATE, parent.HO_TEN, ab.MONTH, ab.ACCOUNT_ID

GO
