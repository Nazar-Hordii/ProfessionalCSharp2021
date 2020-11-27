﻿using ChatServer.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Text;

namespace ChatServer
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR();
        }

        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseFileServer();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<ChatHub>("/chat");
                endpoints.MapHub<GroupChatHub>("7groupchat");
                endpoints.Map("/", async context =>
                {
                    var sb = new StringBuilder();
                    sb.Append("<h1>SignalR Sample</h1>");
                    sb.Append("<div>Open <a href='/ChatWindow.html'>ChatWindow</a> for communication</div>");
                    await context.Response.WriteAsync(sb.ToString());
                });
            });

            //app.UseAzureSignalR(routes =>
            //{
            //    routes.MapHub<ChatHub>("/chat");
            //    routes.MapHub<GroupChatHub>("/groupchat");
            //});

        }
    }
}
