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
        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("음식 주문 메뉴 입니다. 원하시는 음식을 입력해 주십시오. 주문을 완료하려면 그만 이라고 입력하세요.");
            context.Wait(MessageReceivedAsync);
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