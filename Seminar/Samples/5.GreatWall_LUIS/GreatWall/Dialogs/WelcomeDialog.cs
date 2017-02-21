using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace GreatWall.Dialogs
{
    [Serializable]
    public class WelcomeDialog: IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
        }

        public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            await context.PostAsync("안녕하세요 만리장성 봇 입니다. 무엇을 도와드릴까요?");
        }

    }
}