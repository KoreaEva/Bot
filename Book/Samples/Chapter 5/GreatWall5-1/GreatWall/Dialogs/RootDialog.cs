using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

using System.Collections.Generic;

namespace GreatWall.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        private string WelcomeMessage = "신속배달 만리장성 봇입니다";

        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            await context.PostAsync(WelcomeMessage);

            var message = context.MakeMessage();

            var actions = new List<CardAction>();

            actions.Add(new CardAction() { Title = "1.주문", Value = "1" });
            actions.Add(new CardAction() { Title = "2.FAQ", Value = "2" });


            message.Attachments.Add(
                new HeroCard
                {
                    Title = "원하는 기능을 선택하세요",
                    Buttons = actions
                }.ToAttachment()
            );

            await context.PostAsync(message);

            context.Wait(SendWelcomeMessageAsync);
        }

        private async Task SendWelcomeMessageAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;
            string selected = activity.Text.Trim();

            if (selected == "1")
            {
                await context.PostAsync("음식 주문 메뉴 입니다. 원하시는 음식을 입력해 주십시오.");
                context.Call(new OrderDialog(), DialogResumeAfter);
            }
            else if (selected == "2")
            {
                await context.PostAsync("FAQ 서비스 입니다. 질문을 입력해 주십시오.");
                context.Call(new FAQDialog(), DialogResumeAfter);
                
            }
            else
            {
                await context.PostAsync("잘못 선택하셨습니다. 다시 선택해 주십시오");
                context.Wait(SendWelcomeMessageAsync);
            }
            
        }

        private async Task DialogResumeAfter(IDialogContext context, IAwaitable<string> result)
        {
            try
            {
                string message = await result;

                //await context.PostAsync(WelcomeMessage); ;
                await this.MessageReceivedAsync(context, result);
            }
            catch (TooManyAttemptsException)
            {
                await context.PostAsync("오류가 생겼습니다. 죄송합니다.");
            }
        }
    }
}