首先在系统环境变量中添加Mariadb的安装目录如：C:\Program Files (x86)\MySQL\MySQL Server 5.5\bin
之后在硬盘上建立备份目录D:\db_backup\data后，命令行执行如下备份命令：
==============================================
@echo off
D:\MariaDB103\bin\mysqldump.exe --opt -u root --password=125353Ct --databases pathology > D:\db_backup\data\pis.sql
exit
