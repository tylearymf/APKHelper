using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml.Linq;
using Iteedee.ApkReader;


namespace APKHelper
{
    public partial class ApkInfoForm : Form
    {
        private string strPath;
        public ApkInfoForm()
        {
            strPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            strPath = Path.Combine(strPath, "toolsForm/apkInfo");

            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterScreen;

            if (!Directory.Exists(strPath))
            {
                Directory.CreateDirectory(strPath);
            }
        }

        private void ApkInfoForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //删除这个目录下的所有子目录
            if (Directory.GetDirectories(strPath).Length > 0)
            {
                foreach (string var in Directory.GetDirectories(strPath))
                {
                    //DeleteDirectory(var);
                    Directory.Delete(var, true);
                    //DeleteDirectory(var);
                }
            }
            //删除这个目录下的所有文件
            if (Directory.GetFiles(strPath).Length > 0)
            {
                foreach (string var in Directory.GetFiles(strPath))
                {
                    File.Delete(var);
                }
            }
        }


        //拖放完成时
        private void ApkInfoForm_DragDrop(object sender, DragEventArgs e)
        {

            string apkPath = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            if (apkPath.IndexOf(".apk") == -1 && apkPath.IndexOf(".zip") == -1)
            {
                MessageBox.Show("请拖入apk");
                return;
            }

            ApkInfo apk = ReadApk.ReadApkFromPath(apkPath);

            //获取权限
            List<string> Permissions = new List<string>();
            Permissions = apk.Permissions;
            //Permissions = getPermissions(manifestData);



            //读取APK大小等信息
            List<string> listInfo = new List<string>();
            listInfo = getInfo(apkPath);

            if (listInfo.Count != 3)
                return;

            string iconPath = null;
            ////显示应用的LOGO
            Directory.CreateDirectory(strPath);
            for (var i = 0; i < apk.iconFileName.Count; i++)
            {
                iconPath = ExtractApkFile.ExtractFileAndSave(apkPath, apk.iconFileName[i], strPath, i);
            }
            if (iconPath.Length > 0)
            {
                pictureBox.ImageLocation = iconPath;
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }


            //名称
            apkName.Text = apk.label;
            //包名
            packageName.Text = apk.packageName;
            //versionCode
            versionCode.Text = apk.versionCode;
            //versionName
            versionCode1.Text = apk.versionName;

            #region MyRegion
            //Permissions 
            for (int i = 0; i < Permissions.Count; i++)
            {
                if (Permissions[i] == "android.permission.ACCESS_CHECKIN_PROPERTIES")
                    Permission.Items.Add("允许读写访问数据库");
                if (Permissions[i] == "android.permission.ACCESS_COARSE_LOCATION")
                    Permission.Items.Add("允许访问CellID或WiFi获取粗略的位置");
                if (Permissions[i] == "android.permission.ACCESS_FINE_LOCATION")
                    Permission.Items.Add("允许访问精良位置(如GPS)");
                if (Permissions[i] == "android.permission.ACCESS_LOCATION_EXTRA_COMMANDS")
                    Permission.Items.Add("允许访问额外的位置提供命令");
                if (Permissions[i] == "android.permission.ACCESS_MOCK_LOCATION")
                    Permission.Items.Add("允许创建模拟位置提供用于测试");
                if (Permissions[i] == "android.permission.ACCESS_NETWORK_STATE")
                    Permission.Items.Add("允许访问有关GSM网络信息");
                if (Permissions[i] == "android.permission.ACCESS_SURFACE_FLINGER")
                    Permission.Items.Add("允许使用SurfaceFlinger底层特性");
                if (Permissions[i] == "android.permission.ACCESS_WIFI_STATE")
                    Permission.Items.Add("允许访问Wi-Fi网络状态信息");
                if (Permissions[i] == "android.permission.ADD_SYSTEM_SERVICE")
                    Permission.Items.Add("允许发布系统级服务");
                if (Permissions[i] == "android.permission.BATTERY_STATS")
                    Permission.Items.Add("允许更新手机电池统计信息");
                if (Permissions[i] == "android.permission.BLUETOOTH")
                    Permission.Items.Add("允许连接到已配对的蓝牙设备");
                if (Permissions[i] == "android.permission.BLUETOOTH_ADMIN")
                    Permission.Items.Add("允许发现和配对蓝牙设备");
                if (Permissions[i] == "android.permission.BRICK")
                    Permission.Items.Add("请求能够禁用设备(非常危险)");
                if (Permissions[i] == "android.permission.BROADCAST_PACKAGE_REMOVED")
                    Permission.Items.Add("允许广播消息在程序包移除后");
                if (Permissions[i] == "android.permission.BROADCAST_STICKY")
                    Permission.Items.Add("允许广播常用intents");
                if (Permissions[i] == "android.permission.CALL_PHONE")
                    Permission.Items.Add("允许初始化拨号无需确认");
                if (Permissions[i] == "android.permission.CALL_PRIVILEGED")
                    Permission.Items.Add("允许拨打任何号码");
                if (Permissions[i] == "android.permission.CAMERA")
                    Permission.Items.Add("请求访问使用照相设备");
                if (Permissions[i] == "android.permission.CHANGE_COMPONENT_ENABLED_STATE")
                    Permission.Items.Add("允许是否改变一个组件或其他的启用或禁用");
                if (Permissions[i] == "android.permission.CHANGE_CONFIGURATION")
                    Permission.Items.Add("允许修改当前设置");
                if (Permissions[i] == "android.permission.CHANGE_NETWORK_STATE")
                    Permission.Items.Add("允许改变网络连接状态");
                if (Permissions[i] == "android.permission.CHANGE_WIFI_STATE")
                    Permission.Items.Add("允许改变Wi-Fi连接状态");
                if (Permissions[i] == "android.permission.CLEAR_APP_CACHE")
                    Permission.Items.Add("允许清楚缓存从所有安装的程序在设备中");
                if (Permissions[i] == "android.permission.CLEAR_APP_USER_DATA")
                    Permission.Items.Add("允许清除用户设置");
                if (Permissions[i] == "android.permission.CONTROL_LOCATION_UPDATES")
                    Permission.Items.Add("允许启用禁止位置更新提示从无线模块");
                if (Permissions[i] == "android.permission.DELETE_CACHE_FILES")
                    Permission.Items.Add("允许删除缓存文件");
                if (Permissions[i] == "android.permission.DELETE_PACKAGES")
                    Permission.Items.Add("允许程序删除包");
                if (Permissions[i] == "android.permission.DEVICE_POWER")
                    Permission.Items.Add("允许访问底层电源管理");
                if (Permissions[i] == "android.permission.DIAGNOSTIC")
                    Permission.Items.Add("允许程序RW诊断资源");
                if (Permissions[i] == "android.permission.DISABLE_KEYGUARD")
                    Permission.Items.Add("允许程序禁用键盘锁");
                if (Permissions[i] == "android.permission.DUMP")
                    Permission.Items.Add("允许程序返回状态抓取信息从系统服务");
                if (Permissions[i] == "android.permission.EXPAND_STATUS_BAR")
                    Permission.Items.Add("允许扩展收缩在状态栏");
                if (Permissions[i] == "android.permission.FACTORY_TEST")
                    Permission.Items.Add("作为一个测试程序，运行在root用户");
                if (Permissions[i] == "android.permission.FLASHLIGHT")
                    Permission.Items.Add("访问闪光灯");
                if (Permissions[i] == "android.permission.FORCE_BACK")
                    Permission.Items.Add("允许程序强行一个后退操作");
                if (Permissions[i] == "android.permission.FOTA_UPDATE")
                    Permission.Items.Add("预留权限");
                if (Permissions[i] == "android.permission.GET_ACCOUNTS")
                    Permission.Items.Add("访问帐户列表在Accounts Service中");
                if (Permissions[i] == "android.permission.GET_PACKAGE_SIZE")
                    Permission.Items.Add("允许获取任何package占用空间容量");
                if (Permissions[i] == "android.permission.GET_TASKS")
                    Permission.Items.Add("允许程序获取信息");
                if (Permissions[i] == "android.permission.HARDWARE_TEST")
                    Permission.Items.Add("允许访问硬件");
                if (Permissions[i] == "android.permission.INJECT_EVENTS")
                    Permission.Items.Add("允许截获用户事件");
                if (Permissions[i] == "android.permission.INSTALL_PACKAGES")
                    Permission.Items.Add("允许安装packages");
                if (Permissions[i] == "android.permission.INTERNAL_SYSTEM_WINDOW")
                    Permission.Items.Add("允许打开窗口使用系统用户界面");
                if (Permissions[i] == "android.permission.INTERNET")
                    Permission.Items.Add("允许打开网络套接字");
                if (Permissions[i] == "android.permission.MANAGE_APP_TOKENS")
                    Permission.Items.Add("允许程序管理");
                if (Permissions[i] == "android.permission.MODIFY_AUDIO_SETTINGS")
                    Permission.Items.Add("允许修改全局音频设置");
                if (Permissions[i] == "android.permission.MODIFY_PHONE_STATE")
                    Permission.Items.Add("允许修改话机状态");
                if (Permissions[i] == "android.permission.MOUNT_UNMOUNT_FILESYSTEMS")
                    Permission.Items.Add("允许挂载和反挂载系统可移动存储");
                if (Permissions[i] == "android.permission.READ_CALENDAR")
                    Permission.Items.Add("允许读取用户日历数据");
                if (Permissions[i] == "android.permission.READ_CONTACTS")
                    Permission.Items.Add("允许读取用户联系人数据");
                if (Permissions[i] == "android.permission.READ_INPUT_STATE")
                    Permission.Items.Add("允许返回当前按键状态");
                if (Permissions[i] == "android.permission.READ_LOGS")
                    Permission.Items.Add("允许读取底层系统日志文件");
                if (Permissions[i] == "android.permission.READ_OWNER_DATA")
                    Permission.Items.Add("允许程序读取所有者数据");
                if (Permissions[i] == "android.permission.READ_SMS")
                    Permission.Items.Add("允许读取短信息");
                if (Permissions[i] == "android.permission.READ_SYNC_SETTINGS")
                    Permission.Items.Add("允许读取同步设置");
                if (Permissions[i] == "android.permission.READ_SYNC_STATS")
                    Permission.Items.Add("允许读取同步状态");
                if (Permissions[i] == "android.permission.REBOOT")
                    Permission.Items.Add("请求能够重新启动设备");
                if (Permissions[i] == "android.permission.RECEIVE_MMS")
                    Permission.Items.Add("允许监控将收到MMS彩信");
                if (Permissions[i] == "android.permission.RECEIVE_SMS")
                    Permission.Items.Add("允许监控短信息");
                if (Permissions[i] == "android.permission.RECORD_AUDIO")
                    Permission.Items.Add("允许录制音频");
                if (Permissions[i] == "android.permission.RESTART_PACKAGES")
                    Permission.Items.Add("允许重新启动其他程序");
                if (Permissions[i] == "android.permission.SEND_SMS")
                    Permission.Items.Add("允许发送SMS短信");
                if (Permissions[i] == "android.permission.SET_DEBUG_APP")
                    Permission.Items.Add("配置用于调试");
                if (Permissions[i] == "android.permission.SET_PROCESS_FOREGROUND")
                    Permission.Items.Add("允许当前运行程序强行到前台");
                if (Permissions[i] == "android.permission.SET_TIME_ZONE")
                    Permission.Items.Add("允许设置时间区域");
                if (Permissions[i] == "android.permission.SET_WALLPAPER")
                    Permission.Items.Add("允许设置壁纸");
                if (Permissions[i] == "android.permission.STATUS_BAR")
                    Permission.Items.Add("允许打开、关闭或禁用状态栏及图标");
                if (Permissions[i] == "android.permission.SYSTEM_ALERT_WINDOW")
                    Permission.Items.Add("允许显示在顶层");
                if (Permissions[i] == "android.permission.VIBRATE")
                    Permission.Items.Add("允许访问振动设备");
                if (Permissions[i] == "android.permission.WAKE_LOCK")
                    Permission.Items.Add("保持进程在休眠时从屏幕消失");
                if (Permissions[i] == "android.permission.WRITE_APN_SETTINGS")
                    Permission.Items.Add("允许写入API设置");
                if (Permissions[i] == "android.permission.WRITE_CALENDAR")
                    Permission.Items.Add("允许写入但不读取用户日历数据");
                if (Permissions[i] == "android.permission.WRITE_CONTACTS")
                    Permission.Items.Add("允许写入但不读取用户联系人数据");
                if (Permissions[i] == "android.permission.WRITE_GSERVICES")
                    Permission.Items.Add("允许修改Google服务地图");
                if (Permissions[i] == "android.permission.WRITE_SETTINGS")
                    Permission.Items.Add("允许程序读取或写入系统设置");
                if (Permissions[i] == "android.permission.INTERNET")
                    Permission.Items.Add("允许程序访问网络连接");
                else
                    Permission.Items.Add(Permissions[i]);
            }
            #endregion
            //文件名
            fileName.Text = apkPath;

            //md5 
            textMd5.Text = listInfo[2];

            //文件大小
            textSize.Text = listInfo[0] + "字节" + "(" + listInfo[1] + "MB)";


            this.Refresh();


        }
        //拖放时发生
        private void ApkInfoForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.All;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        /// <summary>
        /// 获取大小等基本信息,通过FileStream
        /// </summary>
        /// <param name="apk">apk为文件的完整目录</param>
        /// <returns></returns>

        private List<string> getInfo(string apk)
        {
            List<string> info = new List<string>();

            FileInfo myfile = new FileInfo(apk);
            string fileSizeFloat = (myfile.Length).ToString("N0");
            string fileSizeFloat1 = ((myfile.Length) / (1024f * 1024)).ToString("F2");

            if (fileSizeFloat.Length != 0)
                info.Add(fileSizeFloat);

            if (fileSizeFloat.Length != 0)
                info.Add(fileSizeFloat1);
            try
            {
                FileStream file = new FileStream(apk, FileMode.Open);
                System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] retVal = md5.ComputeHash(file);
                file.Close();

                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                //MessageBox.Show(sb.ToString());
                info.Add(sb.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("GetMD5HashFromFile() fail,error:" + ex.Message);
            }

            return info;
        }


    }
}
