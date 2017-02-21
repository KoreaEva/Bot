using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Collections;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using GreatWall.Entities;

namespace GreatWall.Dialogs
{
    [Serializable]
    public class OrderDialog : IDialog<object>
    {
        //private ArrayList Menus = new ArrayList();
        private List<lMenu> Menus = new List<lMenu>();

        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
        }

        public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            var activity = await argument;

            Entities.GreatWallLUIS greatWallResult = await LuisClient.ParseUserInput(activity.Text);

            string intent = greatWallResult.intents[0].intent;

            if (activity.Text == "주문완료")
            {
                if (Menus.Count == 0)
                {
                    await context.PostAsync("메뉴를 먼저 주문해 주십시오");
                }
                else
                {
                    Menus.Clear();
                    await context.PostAsync("주문이 완료 되었습니다. 곧 배달해 드리겠습니다.");
                }
            }
            else
            {

                switch (intent)
                {
                    case "주문":
                        lMenu menu = new lMenu();
                        menu.Size = "보통";
                        menu.Quantity = 1;
                        Menus.Add(menu);

                        foreach (lEntity e in greatWallResult.entities)
                        {
                            if (e.type == "메뉴")
                            {
                                lMenu temp = Menus[Menus.Count - 1];
                                temp.Menu = e.entity.Replace(" ", "");

                                Menus[Menus.Count - 1] = temp;
                            }
                            else if (e.type == "수량")
                            {
                                lMenu temp = Menus[Menus.Count - 1];
                                temp.Quantity = this.TextToQuantity(e.entity.Replace(" ", ""));

                                Menus[Menus.Count - 1] = temp;
                            }
                            else if (e.type == "크기")
                            {
                                lMenu temp = Menus[Menus.Count - 1];
                                temp.Size = e.entity.Replace(" ", "");

                                Menus[Menus.Count - 1] = temp;
                            }
                        }

                        string order = this.MenuToString(this.Menus);
                        await context.PostAsync(order);


                        break;
                    case "배달":
                        await context.PostAsync("지금 출발했습니다. 조금만 기다려 주십시오");
                        break;
                    case "인사":
                        await context.PostAsync("안녕하세요 신속배달 만리장성 봇입니다. 무엇을 도와드릴까요?");
                        break;

                    default:
                        break;
                }
            }

            context.Wait(MessageReceivedAsync);
        }

        private int TextToQuantity(string message)
        {
            int quantity = 1;

            switch (message)
            {
                case "한그릇":
                    quantity = 1;
                    break;
                case "두그릇":
                    quantity = 2;
                    break;
                case "세그릇":
                    quantity = 3;
                    break;
            }

            return quantity;
        }

        private string QuantityToString(int quantity)
        {
            string message;

            switch (quantity)
            {
                case 1:
                    message = "한그릇";
                    break;
                case 2:
                    message = "두그릇";
                    break;
                case 3:
                    message = "세그릇";
                    break;
                default:
                    message = "한그릇";
                    break;
            }

            return message;
        }

        private string MenuToString(List<lMenu> menus)
        {
            string message = "";

            foreach(var m in menus)
            {
                lMenu menu = m;

                message += menu.Menu + "(" + menu.Size + ") " + this.QuantityToString(menu.Quantity) + "\n\n";
            }

            message += "\n\n메뉴를 추가하시려면 계속 말씀해 주시고 주문을 끝내려면 '주문완료'라고 말씀해 주세요";

            return message;
        }
    }
}