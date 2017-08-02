using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Microsoft.Bot.Connector.DirectLine;
using System.Threading.Tasks;
using System.Configuration;

namespace KakaoConnector.Controllers
{
    public class MessageController : Controller
    {
        private string directLineSecret = ConfigurationManager.AppSettings["DirectLineSecret"];
        private string botId = ConfigurationManager.AppSettings["BotId"];
        private string fromUser = "DirectLineSampleClientUser";
        private Conversation Conversation = null;
        DirectLineClient Client = null;

        // GET: Message
        public async Task<ActionResult> Index(string user_key, string type, string content)
        {
            Activity replyMessage = new Activity
            {
                From = new ChannelAccount(id: "test", name: "kakao"),
                Type = ActivityTypes.Message,
                Text = "hi"
            };

            Client = new DirectLineClient(directLineSecret);
            this.Conversation = Client.Conversations.StartConversation();

            Activity userMessage = new Activity
            {
                From = new ChannelAccount(fromUser),
                Type = ActivityTypes.Message,
                Text = content
            };

            await Client.Conversations.PostActivityAsync(this.Conversation.ConversationId, userMessage);

            //메시지를 받는 부분
            string watermark = null;

            while (true)
            {
                var activitySet = await Client.Conversations.GetActivitiesAsync(Conversation.ConversationId, watermark);
                watermark = activitySet?.Watermark;

                var activities = from x in activitySet.Activities
                                 where x.From.Id == botId
                                 select x;

                Models.Message message = new Models.Message();
                Models.MessageResponse messageResponse = new Models.MessageResponse();
                messageResponse.message = message;

                foreach (Activity activity in activities)
                {
                    message.text = activity.Text;
                }

                return Json(messageResponse, JsonRequestBehavior.AllowGet);            //return View();
            }
        }
    }
}