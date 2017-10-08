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
            SqlConnection con = new SqlConnection("Server=tcp:greatwalldbserver.database.windows.net,1433;Initial Catalog=greatwalldb;Persist Security Info=False;User ID=winkey;Password=!greatwall1004;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            SqlCommand cmd = new SqlCommand("SELECT * FROM Menus", con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            adapter.Fill(ds);

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