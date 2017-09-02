using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Threading.Tasks;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Dialogs;

namespace GreatWall.Dialogs
{
    [Serializable]
    public class OrderDialog : IDialog<string>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            if (activity.Text.Trim() == "그만")
            {
                context.Done("주문완료");
            }
            else
            {
                string message = string.Format("{0}을 주문하셨습니다. 감사합니다.", activity.Text);

                await context.PostAsync(message);

                context.Wait(MessageReceivedAsync);
            }
        }
    }
}