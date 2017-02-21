using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Threading.Tasks;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Dialogs;

namespace GreatWall
{
    [Serializable]
    public class FoodDialog : IDialog<object>
    {
        private string MENU;

        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
        }

        public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            var activity = await argument;

            string message;

            message = string.Format("{0}을 주문 받았습니다. 감사합니다.", activity.Text);

            // return our reply to the user
            await context.PostAsync(message);

            MENU += activity.Text + ",";

            await context.PostAsync("주문내역:" + MENU);

            context.Wait(MessageReceivedAsync);
        }
    }
}