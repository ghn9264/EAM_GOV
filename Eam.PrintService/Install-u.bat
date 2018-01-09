@echo off
::服务程序文件
set Service=Eam.PrintService.exe
::服务端口
set port=7080
::服务显示名字
set Name="Eam.PrintService"

::echo 正在清理本地端口配置
netsh http delete urlacl url=http://+:%port%/ 
netsh http delete iplisten ipaddress=0.0.0.0

::echo 正在清理防火墙设置
netsh advfirewall firewall delete rule name=%Name%

echo 正在卸载服务 %Name%
net stop %Name% 
%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\InstallUtil.exe -u %Service%
echo 卸载完毕