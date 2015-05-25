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

CREATE TRIGGER [dbo].[TR_MANAGER_L1] 
   ON  [dbo].[MANAGER_L1]
   AFTER INSERT,DELETE
AS 
DECLARE @ACCOUNT_ID numeric(10,0);
BEGIN
	if exists(select * from inserted)
	begin
		select @ACCOUNT_ID = account_id from inserted;
		insert into MANAGER_LOG(account_id, dml, manager_level, created_date) values (@ACCOUNT_ID,'I',1,GETDATE());
	end
	
	if exists(select * from deleted)
	begin
		select @ACCOUNT_ID = account_id from deleted;
		insert into MANAGER_LOG(account_id, dml, manager_level, created_date) values (@ACCOUNT_ID,'D',1,GETDATE());
	end

END

GO

CREATE TRIGGER [dbo].[TR_MANAGER_L2] 
   ON  [dbo].[MANAGER_L2]
   AFTER INSERT,DELETE
AS 
DECLARE @ACCOUNT_ID numeric(10,0);
BEGIN
	if exists(select * from inserted)
	begin
		select @ACCOUNT_ID = account_id from inserted;
		insert into MANAGER_LOG(account_id, dml, manager_level, created_date) values (@ACCOUNT_ID,'I',2,GETDATE());
	end
	
	if exists(select * from deleted)
	begin
		select @ACCOUNT_ID = account_id from deleted;
		insert into MANAGER_LOG(account_id, dml, manager_level, created_date) values (@ACCOUNT_ID,'D',2,GETDATE());
	end

END

GO

CREATE TRIGGER [dbo].[TR_MANAGER_L3] 
   ON  [dbo].[MANAGER_L3]
   AFTER INSERT,DELETE
AS 
DECLARE @ACCOUNT_ID numeric(10,0);
BEGIN
	if exists(select * from inserted)
	begin
		select @ACCOUNT_ID = account_id from inserted;
		insert into MANAGER_LOG(account_id, dml, manager_level, created_date) values (@ACCOUNT_ID,'I',3,GETDATE());
	end
	
	if exists(select * from deleted)
	begin
		select @ACCOUNT_ID = account_id from deleted;
		insert into MANAGER_LOG(account_id, dml, manager_level, created_date) values (@ACCOUNT_ID,'D',3,GETDATE());
	end

END

GO

CREATE TRIGGER [dbo].[TR_MANAGER_L4] 
   ON  [dbo].[MANAGER_L4]
   AFTER INSERT,DELETE
AS 
DECLARE @ACCOUNT_ID numeric(10,0);
BEGIN
	if exists(select * from inserted)
	begin
		select @ACCOUNT_ID = account_id from inserted;
		insert into MANAGER_LOG(account_id, dml, manager_level, created_date) values (@ACCOUNT_ID,'I',4,GETDATE());
	end
	
	if exists(select * from deleted)
	begin
		select @ACCOUNT_ID = account_id from deleted;
		insert into MANAGER_LOG(account_id, dml, manager_level, created_date) values (@ACCOUNT_ID,'D',4,GETDATE());
	end

END

GO

CREATE TRIGGER [dbo].[TR_MANAGER_L5] 
   ON  [dbo].[MANAGER_L5]
   AFTER INSERT,DELETE
AS 
DECLARE @ACCOUNT_ID numeric(10,0);
BEGIN
	if exists(select * from inserted)
	begin
		select @ACCOUNT_ID = account_id from inserted;
		insert into MANAGER_LOG(account_id, dml, manager_level, created_date) values (@ACCOUNT_ID,'I',5,GETDATE());
	end
	
	if exists(select * from deleted)
	begin
		select @ACCOUNT_ID = account_id from deleted;
		insert into MANAGER_LOG(account_id, dml, manager_level, created_date) values (@ACCOUNT_ID,'D',5,GETDATE());
	end

END

GO

CREATE TRIGGER [dbo].[TR_MANAGER_L6] 
   ON  [dbo].[MANAGER_L6]
   AFTER INSERT,DELETE
AS 
DECLARE @ACCOUNT_ID numeric(10,0);
BEGIN
	if exists(select * from inserted)
	begin
		select @ACCOUNT_ID = account_id from inserted;
		insert into MANAGER_LOG(account_id, dml, manager_level, created_date) values (@ACCOUNT_ID,'I',6,GETDATE());
	end
	
	if exists(select * from deleted)
	begin
		select @ACCOUNT_ID = account_id from deleted;
		insert into MANAGER_LOG(account_id, dml, manager_level, created_date) values (@ACCOUNT_ID,'D',6,GETDATE());
	end

END

GO

