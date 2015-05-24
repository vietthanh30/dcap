insert into roles(role_code, description) values ('QTHT',N'Quản trị hệ thống')
GO
insert into roles(role_code, description) values ('QLTV',N'Quản lý thông tin thành viên')
GO
insert into roles(role_code, description) values ('QTML',N'Quản lý thông tin mạng lưới')
GO
insert into roles(role_code, description) values ('QTHH',N'Quản lý thông tin hoa hồng/trả thưởng')
GO
insert into users(user_name, password) values ('NGUYENBH','123456')
GO
insert into user_role (user_id, role_id)
select user_id, role_id from users, roles order by user_id, role_id
GO