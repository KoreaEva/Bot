using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Threading.Tasks;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Dialogs;
using GreatWall.Helpers;
using System.Data;
using System.Data.SqlClient;
using GreatWall.Model;

namespace GreatWall.Dialogs
{
    [Serializable]
    public class OrderDialog : IDialog<string>
    {
        string ServerUrl = "http://greatwallweb.azurewebsites.net/Images/";
        List<OrderItem> MenuItems = new List<OrderItem>();

        public async Task StartAsync(IDialogContext context)
        {
            await this.MessageReceivedAsync(context, null);
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            if (result != null)
            {
                var activity = await result as Activity;

                //주문 완료를 선택하면 주문을 완료한다. 
                if (activity.Text == "주문")
                {
                    await context.PostAsync("주문이 완료 되었습니다. 감사합니다.");
                    context.Done("");
                    return;
                }
                else //메뉴를 선택하면 해당 메뉴를 리스트에 추가한다. 
                {
                    //넘겨받은 메뉴코드로 메뉴 정보를 조회하는 코드
                    DataSet sds = SQLHelper.RunSQL("SELECT * FROM Menus WHERE MenuID=" + activity.Text);

                    DataRow row = sds.Tables[0].Rows[0];

                    //조회가 끝나면 관련 내용을 가져와서 리스트에 추가한다.
                    MenuItems.Add(new OrderItem
                    {
                        ItemID = (int)row["MenuID"],
                        ItemName = row["Title"].ToString(),
                        price = (Decimal)row["Price"],
                        Quantity = 1
                    });

                    //현재까지 추가된 내용을 보여준다. 
                    string orderMenus = "";
                    foreach(OrderItem orderItem in MenuItems)
                    {
                        orderMenus += orderItem.ItemName + "/" + orderItem.price + "\n\n";
                    }

                    await context.PostAsync(orderMenus);
                }
            }
            else
                await context.PostAsync("메뉴를 선택해 주십시오");

            //메뉴 출력

            //SQLHelper를 사용하는 것으로 수정된 부분.
            DataSet ds = SQLHelper.RunSQL("SELECT * FROM Menus");

            var message = context.MakeMessage();
            message.Attachments.Add(CardHelper.GetHeroCard("지금 주문", "지금 주문합니다.", this.ServerUrl + "order.jpg", "바로 주문", "주문"));

            foreach(DataRow row in ds.Tables[0].Rows)
            {
                message.Attachments.Add(CardHelper.GetHeroCard(row["Title"].ToString(), row["Subtitle"].ToString(), this.ServerUrl + row["Images"].ToString(), row["Title"].ToString(), row["MenuID"].ToString()));
            }

            message.AttachmentLayout = "carousel";

            await context.PostAsync(message);

            context.Wait(this.MessageReceivedAsync);
        }
    }
}