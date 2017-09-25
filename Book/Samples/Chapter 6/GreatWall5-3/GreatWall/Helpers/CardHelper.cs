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
            buttons.Add(new CardAction() { Title = buttonText, Value = buttonValue });

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
            buttons.Add(new CardAction() { Title = buttonText, Value = buttonValue });

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
            buttons.Add(new CardAction() { Title = buttonText, Value = buttonValue });

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