using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace PowerPlantWebSockets
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            var webSocketOptions = new WebSocketOptions { KeepAliveInterval = TimeSpan.FromSeconds(120) };
            app.UseWebSockets(webSocketOptions);
            app.Use(async (context, next) =>
            {
                if (context.Request.Path == "/send")
                {
                    if (context.WebSockets.IsWebSocketRequest)
                    {
                        using (WebSocket websocket = await context.WebSockets.AcceptWebSocketAsync())
                        {
                            await Send( websocket);
                        }

                    }
                    else
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    }

                }
            });

            
        }

        private async Task Send ( WebSocket webSocket)
        {
            var buffer = new byte[1024 * 4];
            WebSocketReceiveResult res = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), System.Threading.CancellationToken.None);
            if (res !=null)
            {
                while (!res.CloseStatus.HasValue)
                {
                    string msg = Encoding.UTF8.GetString(new ArraySegment<byte>(buffer, 0, res.Count));
                    Console.WriteLine($"client says :{msg}");
                    await webSocket.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes($"Server said :{DateTime.Now}")), res.MessageType, res.EndOfMessage, System.Threading.CancellationToken.None);
                    res = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), System.Threading.CancellationToken.None);
                }

            }
            await webSocket.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, string.Empty, System.Threading.CancellationToken.None);


        }
    }
}
