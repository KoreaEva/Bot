using System;
using System.Threading.Tasks;

using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Dialogs;
using System.Net.Http;


namespace CFE
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        private const string VER = "0.1alpha";

        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("안녕하세요 CFE 봇 입니다. 궁금하신 내용을 질문하세요");
            //context.Wait(MessageReceivedAsync);
            context.Call(new FAQDialog(), DialogResumeAfter);
        }

        public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            var message = await argument;

            if (message.Text == "ver?")
            {
                await context.PostAsync("현재 사용중인 버전은 " + VER + "입니다.");
            }

            context.Call(new FAQDialog(), DialogResumeAfter);
            //context.Wait(MessageReceivedAsync);
        }

        private async Task DialogResumeAfter(IDialogContext context, IAwaitable<string> result)
        {
            try
            {
                string message = await result;

                //await context.PostAsync(WelcomeMessage); ;
                await this.MessageReceivedAsync(context, null);
            }
            catch (TooManyAttemptsException)
            {
                await context.PostAsync("오류가 생겼습니다. 죄송합니다.");
            }
        }
    }
}