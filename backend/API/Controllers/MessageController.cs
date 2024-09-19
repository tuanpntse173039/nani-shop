using Core.Specification;
using FirebaseAdmin.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class MessageController : BaseApiController
{
    [HttpPost]
    public async Task<IActionResult> SendMessageAsync([FromBody] MessageParams messageParams)
    {
        var message = new Message()
        {
            Notification = new Notification
            {
                Title = messageParams.Title,
                Body = messageParams.Body,
            },
            Data = new Dictionary<string, string>()
            {
                ["FirstName"] = "John",
                ["LastName"] = "Doe"
            },
            Token = messageParams.Token
        };

        var messaging = FirebaseMessaging.DefaultInstance;
        var result = await messaging.SendAsync(message);

        if (!string.IsNullOrEmpty(result))
        {
            // Message was sent successfully
            Console.WriteLine("Hello");
            return Ok("Message sent successfully!");
        }
        else
        {
            // There was an error sending the message
            throw new Exception("Error sending the message.");
        }
    }
}
