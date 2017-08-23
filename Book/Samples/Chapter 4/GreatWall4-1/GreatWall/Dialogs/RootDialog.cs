using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace GreatWall.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            await context.PostAsync("안녕하세요 신속배달 만리장성 봇 입니다. 주문하시려는 음식을 입력해 주세요");

            context.Wait(MessageReceivedAsync);
        }

        private async Task SendWelcomeMessageAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            string message = string.Format("{0}을 주문하셨습니다. 감사합니다.", activity.Text);
            await context.PostAsync(message);

            //context.Call(new NameDialog(), this.NameDialogResumeAfter);
            context.Wait(SendWelcomeMessageAsync);
        }
    }
}