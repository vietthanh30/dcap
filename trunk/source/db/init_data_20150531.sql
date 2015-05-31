alter table users add FULL_NAME nvarchar(100) null
GO
update users set full_name = (select m.ho_ten from member_info m, account a where m.member_id = a.member_id and a.user_id = users.user_id)
GO
update roles set description = N'Quyền quản trị hệ thống', role_code = 'QTHT' where role_id = 1
GO
update roles set description = N'Quyền quản lý kế toán', role_code = 'QLKT' where role_id = 2
GO
update roles set description = N'Quyền quản lý thành viên', role_code = 'QLTV' where role_id = 3
GO
delete from user_role
GO
delete from roles where role_id > 3
GO
insert into user_role (user_id, role_id)
select user_id, 1 from users where user_name in ('NGUYENBH', 'THANHNV') order by user_id
GO
insert into user_role (user_id, role_id)
select user_id, 3 from users where user_name not in ('NGUYENBH', 'THANHNV') order by user_id
GO