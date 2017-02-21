using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;

namespace GreatWall
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));
            StateClient stateClient = activity.GetStateClient();

            if (activity != null)
            {
                string message;
                Activity reply;

                // one of these will have an interface and process it
                switch (activity.GetActivityType())
                {
                    case ActivityTypes.Message:
                        message = string.Format("{0}을 주문 받았습니다. 감사합니다.", activity.Text);

                        // return our reply to the user
                        reply = activity.CreateReply(message);
                        await connector.Conversations.ReplyToActivityAsync(reply);

                        BotData userData = await stateClient.BotState.GetUserDataAsync(activity.ChannelId, activity.From.Id);

                        string menu = activity.Text;
                        menu += "," + userData.GetProperty<string>("MENU");

                        userData.SetProperty<string>("MENU", menu);
                        await stateClient.BotState.SetUserDataAsync(activity.ChannelId, activity.From.Id, userData);

                        reply = activity.CreateReply("주문내역:" + menu);
                        await connector.Conversations.ReplyToActivityAsync(reply);

                        break;

                    case ActivityTypes.ConversationUpdate:
                        message = string.Format("안녕하세요 만리장성 봇 입니다. 주문하실 메뉴를 입력해 주세요", activity.Text);

                        reply = activity.CreateReply(message);
                        await connector.Conversations.ReplyToActivityAsync(reply);

                        break;

                    case ActivityTypes.ContactRelationUpdate:
                    case ActivityTypes.Typing:
                    case ActivityTypes.DeleteUserData:
                    default:
                        break;
                }
            }
            return new HttpResponseMessage(System.Net.HttpStatusCode.Accepted);
        }
    }
}