namespace GreatWall.Dialogs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;
    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Builder.FormFlow;
    using Microsoft.Bot.Builder.Luis;
    using Microsoft.Bot.Builder.Luis.Models;
    using Microsoft.Bot.Connector;

    [LuisModel("79cee8ff-ee05-4d9f-a3c4-8061c67be195", "45b9b62a11a04220a79d948a7a7893b4")]
    [Serializable]
    public class LUISDialog : LuisDialog<string>
    {
        [LuisIntent("")]
        [LuisIntent("None")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            string message = $"죄송합니다. 말씀을 이해하지 못했습니다.";

            await context.PostAsync(message);

            context.Wait(this.MessageReceived);
        }

        [LuisIntent("Order")]
        public async Task Order(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            var message = await activity;

            EntityRecommendation menuEntityRecommendation;
            EntityRecommendation sizeEntityRecommendation;
            EntityRecommendation quantityEntityRecommendation;

            string menu = "";
            string size = "보통";
            string quantity = "한그릇";

            if(result.TryFindEntity("Menu", out menuEntityRecommendation))
            {
                menu = menuEntityRecommendation.Entity.Replace(" ", "");
            }
            else
            {
                await context.PostAsync("없는 메뉴를 선택했습니다.");
                context.Wait(this.MessageReceived);
                return;
            }

            if (result.TryFindEntity("Size", out sizeEntityRecommendation))
            {
                size = sizeEntityRecommendation.Entity.Replace(" ", "");
            }

            if (result.TryFindEntity("Quantity", out quantityEntityRecommendation))
            {
                quantity = quantityEntityRecommendation.Entity.Replace(" ", "");
            }


            await context.PostAsync($"{menu} {size} {quantity}를 주문하셨습니다.");

            context.Wait(this.MessageReceived);
        }

        [LuisIntent("Delivery")]
        public async Task Delivery(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            await context.PostAsync("출발 했습니다. 잠시만 기다려 주세요.");

            context.Wait(this.MessageReceived);
        }

        [LuisIntent("Finish")]
        public async Task Finish(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            await context.PostAsync("주문 완료 되었습니다. 감사합니다.");

            context.Done("주문완료");
        }
    }
}