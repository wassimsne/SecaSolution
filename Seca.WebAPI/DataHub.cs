using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.AspNetCore.SignalR;
using System.Diagnostics;
using System.Net;
using System.Security.Claims;

namespace Seca.WebAPI.Hubs
{

    public class DataHub : Hub
    {
        public DataHub()
        {
         
        }

        public override System.Threading.Tasks.Task OnDisconnectedAsync(Exception? stopCalled)
        {
           
                Console.WriteLine("Client  disconnected");


            return base.OnDisconnectedAsync(stopCalled);
        }
        public override Task OnConnectedAsync()
        {
            
            Debug.WriteLine("new user ");
            //  var userid = Context.User.Claims.FirstOrDefault(c => c.Type == "UserId").Value;
           
            return base.OnConnectedAsync();
        }


        public void test(string text)
        {
            Console.WriteLine(text);
      }
       
    }
}
