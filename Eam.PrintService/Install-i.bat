@echo off
::服务程序文件
set Service=Eam.PrintService.exe
::服务端口
set port=7080
::服务显示名字
set Name="Eam.PrintService"

::echo 正在配置本地端口
netsh http add urlacl url=http://+:%port%/  sddl="D:(A;;GX;;;LS)"
netsh http add iplisten ipaddress=0.0.0.0:%port%

::echo 正在设置防火墙
netsh advfirewall firewall add rule name=%Name% dir=in action=allow protocol=TCP localport=%port%

echo 正在安装服务 %Name%  
%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\InstallUtil.exe -i %Service%

echo 正在设置服务参数
sc config %Name% start= auto