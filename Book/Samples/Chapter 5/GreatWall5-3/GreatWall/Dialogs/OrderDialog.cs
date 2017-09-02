using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Threading.Tasks;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Dialogs;
using GreatWall.Helpers;

namespace GreatWall.Dialogs
{
    [Serializable]
    public class OrderDialog : IDialog<string>
    {
        string ServerUrl = "http://greatwallweb.azurewebsites.net/Images/";

        public async Task StartAsync(IDialogContext context)
        {
            await this.MessageReceivedAsync(context, null);
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            if (result != null)
            {
                var activity = await result as Activity;

                if(activity.Text == "주문")
                {
                    await context.PostAsync("주문이 완료 되었습니다. 감사합니다.");
                    context.Done("");
                    return;
                }
                else
                    await context.PostAsync(activity.Text + "를 주문하셨습니다.");
            }
            else
                await context.PostAsync("메뉴를 선택해 주십시오");

            //메뉴 출력
            var message = context.MakeMessage();
            message.Attachments.Add(CardHelper.GetHeroCard("지금 주문", "지금 주문합니다.", this.ServerUrl + "order.jpg", "바로 주문", "주문"));
            message.Attachments.Add(CardHelper.GetHeroCard("자장면 \\2,500", "전통적인 자장면 입니다.", this.ServerUrl + "menu1.JPG", "자장면", "자장면"));
            message.Attachments.Add(CardHelper.GetHeroCard("짬뽕 \\3,000", "시원한 국물의 짬뽕입니다.", this.ServerUrl + "menu2.JPG", "짬뽕", "짬뽕"));
            message.Attachments.Add(CardHelper.GetHeroCard("탕수육 \\5,000", "부먹찍먹 모두 맛있는 탕수육 입니다.", this.ServerUrl + "menu3.JPG", "탕수육", "탕수육"));

            message.AttachmentLayout = "carousel";

            await context.PostAsync(message);

            context.Wait(this.MessageReceivedAsync);
        }
    }
}