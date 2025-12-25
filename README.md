# 机房助手 - ITClassHelper

# 简介
这是一款帮助你对抗老师的"电子教室"而开发的软件，拥有关闭、挂起、恢复、读写密码、控制广播等功能。\
同时它还可以向机房里的任何设备远程发送命令，让你成为机房里最靓的仔！\
虽然本软件面向极域电子教室开发，但从 3.2.0 版本后开始逐渐支持红蜘蛛电子教室，以后将会支持更多电子教室。

# 功能
* 实时显示教室状态
* 关闭、挂起、恢复教室
* 支持 4 种强杀进程方式：
	* Process.Kill
	* NtTerminateProcess
	* Ntsd
	* EndTask
* 读取/修改教室密码^
* 一键补全并更新组件
* 自动读取教室路径^
* 破解教室各种挂钩与限制^
* 简单的局域网聊天
* 设备管理器：
	* 扫描在线的设备，获取它们的 MAC 与 IP
	* 发送命令、消息、脚本
	* 远程关机、重启
	* 内置脚本处理器：将BAT/VBS转换为可攻击脚本\
^：指该功能暂时仅支持极域电子教室。

# 安装
## 初次安装
直接下载仓库中 Release 文件夹中的 ITCHLauncher.exe 文件，打开后将自动下载必要组件并打开主程序。\
[点我立即下载](https://gitee.com/ujhhgtg/ITClassHelper/raw/master/bin/x86/Release/ITCHLauncher.exe)
## 更新软件
打开主程序，再点击[更新软件]。

# 致谢
[极域数据包重放攻击：Jiyu_udp_attack](https://github.com/ht0Ruial/Jiyu_udp_attack)\
[极域功能参考：JiyuTrainer](https://github.com/imengyu/JiYuTrainer)\
[红蜘蛛数据包重放攻击：M家的蜘蛛不可能那么可爱](https://cvnet.lanzoui.com/irv7pr7fn7e)
