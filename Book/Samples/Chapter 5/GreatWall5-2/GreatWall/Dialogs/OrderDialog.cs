using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Threading.Tasks;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Dialogs;

using System.Net.Http;

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
                await context.PostAsync(activity.Text + "를 주문하셨습니다.");
            }
            else
                await context.PostAsync("메뉴를 선택해 주십시오");

            //자장면 출력

            //이미지 객체의 생성
            List<CardImage> menu1images = new List<CardImage>();
            menu1images.Add(new CardImage() { Url = this.ServerUrl + "menu1.JPG" });

            //버튼의 생성
            List<CardAction> menu1Buttons = new List<CardAction>();
            menu1Buttons.Add(new CardAction() { Title = "자장면", Value = "자장면" });

            HeroCard menu1Card = new HeroCard()
            {
                Title = "자장면",
                Subtitle = "전통적인 자장면 입니다.",
                Images = menu1images,
                Buttons = menu1Buttons
            };

            //짬뽕 출력

            //이미지 객체의 생성
            List<CardImage> menu2images = new List<CardImage>();
            menu2images.Add(new CardImage() { Url = this.ServerUrl + "menu2.JPG" });

            //버튼의 생성
            List<CardAction> menu2Buttons = new List<CardAction>();
            menu2Buttons.Add(new CardAction() { Title = "짬뽕", Value = "짬뽕" });

            HeroCard menu2Card = new HeroCard()
            {
                Title = "짬뽕",
                Subtitle = "시원한 국물의 짬뽕입니다.",
                Images = menu2images,
                Buttons = menu2Buttons
            };

            //탕수육 출력

            //이미지 객체의 생성
            List<CardImage> menu3images = new List<CardImage>();
            menu3images.Add(new CardImage() { Url = this.ServerUrl + "menu3.JPG" });

            //버튼의 생성
            List<CardAction> menu3Buttons = new List<CardAction>();
            menu3Buttons.Add(new CardAction() { Title = "탕수육", Value = "탕수육" });

            HeroCard menu3Card = new HeroCard()
            {
                Title = "탕수육",
                Subtitle = "부먹찍먹 모두 맛있는 탕수육 입니다.",
                Images = menu3images,
                Buttons = menu3Buttons
            };

            var message = context.MakeMessage();
            message.Attachments.Add(menu1Card.ToAttachment());
            message.Attachments.Add(menu2Card.ToAttachment());
            message.Attachments.Add(menu3Card.ToAttachment());

            await context.PostAsync(message);

            context.Wait(this.MessageReceivedAsync);
        }
    }
}