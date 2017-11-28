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

    [LuisModel("d550355f-3389-46d7-ae3c-8a610a718438", "45b9b62a11a04220a79d948a7a7893b4")]
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

        [LuisIntent("order")]
        public async Task Order(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            var message = await activity;

            EntityRecommendation menuEntityRecommendation;
            EntityRecommendation sizeEntityRecommendation;
            EntityRecommendation quantityEntityRecommendation;

            string menu = "";
            string size = "보통";
            string quantity = "한그릇";

            if(result.TryFindEntity("menu", out menuEntityRecommendation))
            {
                menu = menuEntityRecommendation.Entity.Replace(" ", "");
            }
            else
            {
                await context.PostAsync("없는 메뉴를 선택했습니다.");
            }

            if (result.TryFindEntity("size", out sizeEntityRecommendation))
            {
                size = sizeEntityRecommendation.Entity.Replace(" ", "");
            }

            if (result.TryFindEntity("quantity", out quantityEntityRecommendation))
            {
                quantity = quantityEntityRecommendation.Entity.Replace(" ", "");
            }


            await context.PostAsync($"{menu} {size} {quantity}를 주문하셨습니다.");

            context.Wait(this.MessageReceived);
        }
    }
}