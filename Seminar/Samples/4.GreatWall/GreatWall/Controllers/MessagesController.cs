using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;

using Microsoft.Bot.Builder.Dialogs;

namespace GreatWall
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        internal static IDialog<FoodOrder> MakeRootDialog()
        {
            return Chain.From(() => FormDialog.FromForm(FoodOrder.BuildForm));
        }

        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            if (activity != null)
            {
                // one of these will have an interface and process it
                switch (activity.GetActivityType())
                {
                    case ActivityTypes.Message:
                        await Conversation.SendAsync(activity, () => new FoodDialog());
                        //await Conversation.SendAsync(activity, MakeRootDialog);

                        break;

                    case ActivityTypes.ConversationUpdate:
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