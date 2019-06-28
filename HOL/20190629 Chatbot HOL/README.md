# 숭실대학교  챗봇 Hands on Lab (2019s년 6월 29일)

## 1. 개발환경의 구성

1. Microsoft Teams 다운로드 [https://products.office.com/ko-kr/microsoft-teams/group-chat-software] (https://products.office.com/ko-kr/microsoft-teams/group-chat-software) <br>
2. Visual Studio 2019 community edition의 설치 [https://visualstudio.microsoft.com/ko/](https://visualstudio.microsoft.com/ko/)<br>
3. Bot Emulator [https://github.com/Microsoft/BotFramework-Emulator/releases/tag/v4.4.2](https://github.com/Microsoft/BotFramework-Emulator/releases/tag/v4.4.2)<br>
4. 시작 프로젝트 파일의 다운로드 [GreatWall_Start.zip](./GreatWall_Start.zip)<br>
5. Azure 계정의 준비 [http://portal.azure.com](http://portal.azure.com)<br>


## 2. Code

1.------------------------------------

```C#
        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await argument as Activity;

            string message = string.Format("{0}을 주문하셨습니다. ", activity.Text);

            await context.PostAsync(message);

            context.Wait(MessageReceivedAsync);
        }
```

2.------ 인사 기능의 구현 

```C#
        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            await context.PostAsync("안녕하세요 신속배달 만리장성 봇 입니다. 주문하시려는 음식을 입력해 주세요");

            context.Wait(SendWelcomeMessageAsync);
        }
```


3.------ 인사 기능의 구현(2)

```C#
        private async Task SendWelcomeMessageAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            string message = string.Format("{0}을 주문하셨습니다. 감사합니다.", activity.Text);
            await context.PostAsync(message);

            context.Wait(SendWelcomeMessageAsync);
        }
```

4.------- Dialog의 구현

```C#
	string WelcomeMessage = "안녕하세요 만리장석 봇입니다. 1.주문 2.FAQ 중에 선택하세요";


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
```

5.--------- Order Dialog

```C#
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
```

6.---------- DialogResumeAfer( )의 추가

```C#
        private async Task DialogResumeAfter(IDialogContext context, IAwaitable<string> result)
        {
            try
            {
                string message = await result;

		await this.MessageReceivedAsync(context, result);
            }
            catch (TooManyAttemptsException)
            {
                await context.PostAsync("오류가 생겼습니다. 죄송합니다.");
            }
        }
```

7.----------------------------------------------------------------

```C#
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
                await context.PostAsync("FAQ Dialog 입니다.");

                context.Wait(MessageReceivedAsync);
            }
        }
    }
}
```

8.-------- Card

RootDialog.cs

```C#
	using System.Collections.Generic;


        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            await context.PostAsync(WelcomeMessage);

            var message = context.MakeMessage();

            var actions = new List<CardAction>();

            actions.Add(new CardAction() { Title = "1.주문", Value = "1" , Type = ActionTypes.ImBack });
            actions.Add(new CardAction() { Title = "2.FAQ", Value = "2" , Type = ActionTypes.ImBack });


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
```

9.-----------------------------------------------------------------

```C#
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
```

10.------------------------------------------------------------------
OrderDialog.cs

```C#
        string ServerUrl = "https://greatwallkcd.azurewebsites.net/images/";
```

11.--------------------------------------------------------------------

```C#
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
            menu1Buttons.Add(new CardAction() { Title = "자장면", Value = "자장면", Type = ActionTypes.ImBack });

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
            menu2Buttons.Add(new CardAction() { Title = "짬뽕", Value = "짬뽕", Type = ActionTypes.ImBack });

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
            menu3Buttons.Add(new CardAction() { Title = "탕수육", Value = "탕수육", Type = ActionTypes.ImBack });

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
```

12.--------------------------------------------------------------------
RootDialog.cs

```C#
        private async Task SendWelcomeMessageAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;
            string selected = activity.Text.Trim();

            if (selected == "1")
            {
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
```

13.----------------
OrderDialog.cs

```C#
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
```

13-1 ------ /Helpers/CardHelper의 추가

```C#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Microsoft.Bot.Builder;
using Microsoft.Bot.Connector;

namespace GreatWall.Helpers
{
    public static class CardHelper
    {
        public static Attachment GetHeroCard(string title, string subTitle, string image, string buttonText, string buttonValue)
        {
            //이미지 객체의 생성
            List<CardImage> images = new List<CardImage>();
            images.Add(new CardImage() { Url = image });

            //버튼의 생성
            List<CardAction> buttons = new List<CardAction>();
            buttons.Add(new CardAction() { Title = buttonText, Value = buttonValue, Type = ActionTypes.ImBack });

            HeroCard card = new HeroCard()
            {
                Title = title,
                Subtitle = subTitle,
                Images = images,
                Buttons = buttons
            };

            return card.ToAttachment();
        }

        public static Attachment GetThumbnailCard(string title, string subTitle, string image, string buttonText, string buttonValue)
        {
            //이미지 객체의 생성
            List<CardImage> images = new List<CardImage>();
            images.Add(new CardImage() { Url = image });

            //버튼의 생성
            List<CardAction> buttons = new List<CardAction>();
            buttons.Add(new CardAction() { Title = buttonText, Value = buttonValue, Type = ActionTypes.ImBack });

            ThumbnailCard card = new ThumbnailCard()
            {
                Title = title,
                Subtitle = subTitle,
                Images = images,
                Buttons = buttons
            };

            return card.ToAttachment();
        }

        public static Attachment GetHeroCard(string title, List<ReceiptItem> items, string total, string tax, string vat, string buttonText, string buttonValue)
        {
            //버튼의 생성
            List<CardAction> buttons = new List<CardAction>();
            buttons.Add(new CardAction() { Title = buttonText, Value = buttonValue, Type = ActionTypes.ImBack });

            ReceiptCard card = new ReceiptCard
            {
                Title = title,
                Items = items,
                Tax = tax,
                Total = total,
                Vat = vat,
                Buttons = buttons
            };

            return card.ToAttachment();
        }
    }
}
```

14.------- QnAMaker

```C#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Threading.Tasks;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Dialogs;
using System.Threading;
using QnAMakerDialog.Models;
using QnAMakerDialog;


namespace GreatWall.Dialogs
{
    [Serializable]
    [QnAMakerService("https://greatwallqna.azurewebsites.net/qnamaker", "be9a25bc-715b-4ec7-a50f-4be894748f90", "9936be9a-840a-470c-a182-7735c78cbf27", MaxAnswers = 5)]
    public class FAQDialog : QnAMakerDialog<string>
    {
        public override async Task NoMatchHandler(IDialogContext context, string originalQueryText)
        {
            await context.PostAsync($"Sorry, I couldn't find an answer for '{originalQueryText}'.");
            context.Wait(MessageReceived);
        }

        public override async Task DefaultMatchHandler(IDialogContext context, string originalQueryText, QnAMakerResult result)
        {
            if (originalQueryText == "그만")
            {
                context.Done("");
                return;
            }

            await context.PostAsync(result.Answers.First().Answer);

            context.Wait(MessageReceived);
        }

        [QnAMakerResponseHandler(0.5)]
        public async Task LowScoreHandler(IDialogContext context, string originalQueryText, QnAMakerResult result)
        {
            var messageActivity = ProcessResultAndCreateMessageActivity(context, ref result);
            messageActivity.Text = $"I found an answer that might help...{result.Answers.First().Answer}.";
            await context.PostAsync(messageActivity);

            context.Wait(MessageReceived);
        }
    }
}
```

15. LUIS ---------------------------------------------

```C#
//RootDialog.cs

actions.Add(new CardAction() { Title = "3.LUIS", Value = "3", Type = ActionTypes.ImBack });

else if (selected == "3")
{
    await context.PostAsync("LUIS 서비스 입니다. 질문을 입력해 주십시오.");
    context.Call(new LDialog(), DialogResumeAfter);

}

//LuisDialog.cs

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Threading.Tasks;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Dialogs;
using System.Net.Http;
using Newtonsoft.Json;

namespace GreatWall.Dialogs
{
    [Serializable]
    public class LDialog : IDialog<string>
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
                //var parseResult = ParseUserInput(activity.Text);

                string uri = "https://koreacentral.api.cognitive.microsoft.com/luis/v2.0/apps/1de493ba-8172-42d3-b0d6-3e9fe749ac92?verbose=true&timezoneOffset=-360&subscription-key=3b6d9ac05c194f4592e5fe71a2a8161c&q=" + activity.Text;

                var client = new HttpClient();
                HttpResponseMessage msg = await client.GetAsync(uri);
                var jsonResponse = await msg.Content.ReadAsStringAsync();

                //string parseText = "Testing..."; // parseResult.ToString();

                await context.PostAsync(jsonResponse);

                context.Wait(MessageReceivedAsync);
            }
        }
    }

    public class StockLUIS
    {
        public string query { get; set; }
        public lIntent[] intents { get; set; }
        public lEntity[] entities { get; set; }
    }

    public class lIntent
    {
        public string intent { get; set; }
        public float score { get; set; }
    }

    public class lEntity
    {
        public string entity { get; set; }
        public string type { get; set; }
        public int startIndex { get; set; }
        public int endIndex { get; set; }
        public float score { get; set; }
    }

}
```