﻿
using IdCardReader_WinService.Helper;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.ServiceProcess;
using System.Web.Http;
using System.Web.Http.SelfHost;

namespace FingerPrint_WinService
{
    public partial class MrzPassportReaderReaderService : ServiceBase
    {
        public MrzPassportReaderReaderService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                string ServiceURLPath = ConfigurationManager.AppSettings["UrlSelfHosting"];


                var config = new HttpSelfHostConfiguration(ServiceURLPath);
                config.EnableCors();
                config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

                config.MaxReceivedMessageSize = 2147483647; // use config for this value


                config.Routes.MapHttpRoute(
                    name: "API",
                    routeTemplate: "api/{controller}/{action}/{id}",
                    defaults: new { controller = "PassportApi", id = RouteParameter.Optional }
                    );

                Logger.WriteLog(" تم تشغيل خدمة قارئ  مرز الجوازات بنجاح ");

                HttpSelfHostServer server = new HttpSelfHostServer(config);
                server.OpenAsync().Wait();

                //WriteToFile("api http://localhost:7077 in done to calling" + DateTime.Now);

            }
            catch (Exception ex)
            {
                var stackTrace = new StackTrace(ex, true);
                var frame = stackTrace.GetFrame(0);
                var line = frame.GetFileLineNumber();
                Logger.WriteLog("ErrorMessage" + Environment.NewLine + ex.Message + Environment.NewLine + stackTrace + "Line" + line);

            }
            

        }

        protected override void OnStop()
        {
            Logger.WriteLog(" تم ايقاف خدمة قارئ مرز الجوازات بنجاح ");
        }

    }
}
