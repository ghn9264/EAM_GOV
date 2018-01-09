using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Web;
using System.ServiceProcess;
using System.Text;

namespace Eam.PrintService
{
    public partial class WinPrintService : ServiceBase
    {
        public WinPrintService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            RunService();
        }

        private static WebServiceHost _serviceHost;

        private void RunService()
        {
            try
            {
                //
                // 创建服务站点
                // IP地址自由设定
                //
                _serviceHost = new WebServiceHost(typeof(PrintService), new Uri("http://127.0.0.1:7080"));
                ContractDescription con = ContractDescription.GetContract(typeof (IPrintService));
                var webHttpBinding = new WebHttpBinding {CrossDomainScriptAccessEnabled = true};
                var endpointAddress = new EndpointAddress("http://127.0.0.1:7080/PrintService");
                WebHttpEndpoint webHttpEndpoint = new WebHttpEndpoint(con, endpointAddress);
                webHttpEndpoint.Binding = webHttpBinding;
                webHttpEndpoint.CrossDomainScriptAccessEnabled = true;
                //webHttpEndpoint.Behaviors.Add(new EnableCrossOriginResourceSharingBehavior());
                 _serviceHost.AddServiceEndpoint(webHttpEndpoint);

                //
                // 写服务日志
                //
                _serviceHost.Opened += (o, e) => Logger.Log("服务已开启");
                _serviceHost.Closed += (o, e) => Logger.Log("服务已关闭");
                _serviceHost.Open();
                GC.KeepAlive(_serviceHost);
            }
            catch (Exception ex)
            {
                Logger.Log("服务开启异常:" + ex);
            }
        }

        protected override void OnStop()
        {
        }
    }
}