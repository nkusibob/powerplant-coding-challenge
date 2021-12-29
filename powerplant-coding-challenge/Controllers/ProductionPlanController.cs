using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PowerChallengeBusiness;
using PowerChallengeBusiness.Models;
using PowerChallengeBusiness.Operations;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace powerplant_coding_challenge.Controllers
{
    public class ProductionPlanController : Controller
    {
        private readonly FuelCostOperation _fuelOp;
        private readonly PowerOperation _powerOperation;

        public ProductionPlanController(FuelCostOperation fuelOp, PowerOperation powerOperation)
        {
            _fuelOp = fuelOp;
            _powerOperation = powerOperation;
        }

        /// <summary>
        /// specify how much power each powerplant should deliver 
        /// </summary>
        /// <param name="payload"> The payload contains 3 types of data : load , fuels and powerplants</param>
        /// <returns>list power produced by powerplants </returns>

        [HttpPost("productionplan")]
        [SwaggerResponse(200, "productionplan", typeof(Payload))]
        public async Task<List<Power>> Addpayload([FromBody] Payload payload)
        {
            var payloadJson = JsonConvert.SerializeObject(payload);
            List<Power> powerList = _powerOperation.GetPower(payload, _fuelOp);
            var response = JsonConvert.SerializeObject(powerList);
            await WebSocket("payload request   :" + payloadJson);
            await WebSocket("payload response  :" + response);
            return powerList ;
        }
        private async Task WebSocket(string message)
        {
            using (ClientWebSocket client = new ClientWebSocket())
            {
                if (message != null)
                {
                    Uri servUri = new Uri("ws://localhost:5000/send");
                    var cancelTokenSource = new CancellationTokenSource();
                    cancelTokenSource.CancelAfter(TimeSpan.FromSeconds(120));
                    try
                    {
                        await client.ConnectAsync(servUri, cancelTokenSource.Token);
                        while (client.State == WebSocketState.Open)
                        {
                            if (!string.IsNullOrEmpty(message))
                            {
                                ArraySegment<byte> messageByte = new ArraySegment<byte>(Encoding.UTF8.GetBytes(message));
                                await client.SendAsync(messageByte, WebSocketMessageType.Text, true, cancelTokenSource.Token);
                                var responseBuff = new Byte[1024];
                                var offset = 0;
                                var packet = 1024;
                                WebSocketReceiveResult resp;
                                while (true)
                                {
                                    ArraySegment<byte> recByte = new ArraySegment<byte>(responseBuff, offset, packet);
                                     resp = await client.ReceiveAsync(recByte, cancelTokenSource.Token);
                                    var responseMessage = Encoding.UTF8.GetString(responseBuff, offset, resp.Count);
                                    Console.WriteLine(responseMessage);
                                    if (resp.MessageType == WebSocketMessageType.Text)
                                    {
                                        await client.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
                                        break;
                                    };

                                }
                                
                            }

                        }

                    }
                    catch (WebSocketException e)
                    {

                        Console.WriteLine(e.Message);
                    }
                }
            }
        }
       
       
    }
}
