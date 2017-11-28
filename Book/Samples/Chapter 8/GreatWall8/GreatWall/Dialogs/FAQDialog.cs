using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Threading.Tasks;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Dialogs;
using QnaMakerApi;

namespace GreatWall.Dialogs
{
    [Serializable]
    public class FAQDialog : IDialog<string>
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
                string message = "";
                using (var client = new QnaMakerClient("633be6fd99a64f87b5cfd7d8486b568c"))
                {
                    var answer = await client.GenerateAnswer(new Guid("5b4c08ae-5535-4757-88a1-99014d48b483"), activity.Text);
                    message = answer.Answers[0].Answer;
                }

                await context.PostAsync(message);

                context.Wait(MessageReceivedAsync);
            }
        }
    }
}