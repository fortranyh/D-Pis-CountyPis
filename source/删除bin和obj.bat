@echo off

echo ���������ʼɾ��obj��binĿ¼

pause>nul

@echo off

cd %FolderName%
 %dis%

@echo on
::ɾ��obj��binĿ¼

for /f "tokens=*" %%a in ('dir obj /b /ad /s ^|sort') do rd "%%a" /s/q

for /f "tokens=*" %%a in ('dir bin /b /ad /s ^|sort') do rd "%%a" /s/q

del *.sln.cache

@echo off

echo ��������˳�

pause>nul

