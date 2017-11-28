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
                    List<ReceiptItem> receiptItems = new List<ReceiptItem>();

                    Decimal totalPrice = 0;

                    foreach(OrderItem orderItem in MenuItems)
                    {
                        receiptItems.Add(new ReceiptItem()
                        {
                            Title = orderItem.Title,
                            Price = orderItem.Price.ToString("##########"),
                            Quantity = orderItem.Quantity.ToString(),
                        });

                        totalPrice += orderItem.Price;
                    }

                    //주문 내역을 Database에 입력한다.
                    SqlParameter[] para =
                    {
                        new SqlParameter("@TotalPrice", SqlDbType.SmallMoney),
                        new SqlParameter("@UserID", SqlDbType.NVarChar, 50)
                    };

                    para[0].Value = totalPrice;
                    para[1].Value = activity.Id;

                    SQLHelper.ExecuteNonQuery("INSERT INTO Orders(TotalPrice, UserID, OrderDate) VALUES(@TotalPrice, @UserID, GETDATE())", para);

                    DataSet orderNumber = SQLHelper.RunSQL("SELECT MAX(OrderID) FROM Orders WHERE UserID = '" + activity.Id + "'");

                    DataRow row = orderNumber.Tables[0].Rows[0];
                    int orderID = (int)row[0];

                    foreach (OrderItem orderItem in MenuItems)
                    {
                        SqlParameter[] para2 =
                        {
                            new SqlParameter("@OrderID", SqlDbType.Int),
                            new SqlParameter("@ItemName", SqlDbType.NVarChar),
                            new SqlParameter("@ItemPrice", SqlDbType.SmallMoney),
                            new SqlParameter("@Qunatity", SqlDbType.Int)
                        };

                        para2[0].Value = orderID;
                        para2[1].Value = orderItem.Title;
                        para2[2].Value = orderItem.Price;
                        para2[3].Value = orderItem.Quantity;

                        SQLHelper.ExecuteNonQuery("INSERT INTO Items(OrderID, ItemName, ItemPrice, Qunatity) VALUES(@OrderID, @ItemName, @ItemPrice, @Qunatity)", para2);
                    }

                    var cardMessage = context.MakeMessage();
                    cardMessage.Attachments.Add(CardHelper.GetReceiptCard("주문내역", receiptItems, totalPrice.ToString(), "2%", "10%"));

                    MenuItems.Clear();

                    await context.PostAsync(cardMessage);
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
                        Title = row["Title"].ToString(),
                        Price = (Decimal)row["Price"],
                        Quantity = 1
                    });

                    //현재까지 추가된 내용을 보여준다. 
                    string orderMenus = "";
                    foreach(OrderItem orderItem in MenuItems)
                    {
                        orderMenus += orderItem.Title + "/" + orderItem.Price.ToString("########") + "\n\n";
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