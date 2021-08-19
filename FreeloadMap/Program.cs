using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using FreeloadMap.Data;

namespace FreeloadMap
{
#warning 设定端口
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseKestrel(options =>
                    {
                        // https://docs.microsoft.com/zh-cn/dotnet/api/microsoft.aspnetcore.server.kestrel.https.httpsconnectionadapteroptions?view=aspnetcore-3.1
                        options.ConfigureHttpsDefaults(configureOptions =>
                        {
                            configureOptions.AllowAnyClientCertificate();
                            configureOptions.ClientCertificateMode = Microsoft.AspNetCore.Server.Kestrel.Https.ClientCertificateMode.NoCertificate;
                            configureOptions.HandshakeTimeout = new TimeSpan(0, 0, 0, 10, 0);
                            configureOptions.ServerCertificate = new System.Security.Cryptography.X509Certificates.X509Certificate2(
                                SecretValues.X509CertificatePath,
                                SecretValues.X509CertificatePassword
                                );
                            configureOptions.SslProtocols = System.Security.Authentication.SslProtocols.Tls12 | System.Security.Authentication.SslProtocols.Tls13;
                        });
                    });

                    //// https://www.cnblogs.com/wanggang2016/p/12320808.html
                    webBuilder.UseUrls("https://*:5001"); // dotnet默认端口
                    //webBuilder.UseUrls(new string[] { "http://*:5000", "https://*:5001" }); // dotnet默认端口

                    webBuilder.UseContentRoot(Directory.GetCurrentDirectory());
                    webBuilder.UseStartup<Startup>();
                });
    }
}
