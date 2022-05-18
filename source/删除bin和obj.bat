@echo off

echo 按任意键开始删除obj和bin目录

pause>nul

@echo off

cd %FolderName%
 %dis%

@echo on
::删除obj和bin目录

for /f "tokens=*" %%a in ('dir obj /b /ad /s ^|sort') do rd "%%a" /s/q

for /f "tokens=*" %%a in ('dir bin /b /ad /s ^|sort') do rd "%%a" /s/q

del *.sln.cache

@echo off

echo 按任意键退出

pause>nul

