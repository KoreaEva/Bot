using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Microsoft.Bot.Connector.DirectLine;
using Newtonsoft.Json;
using System.Configuration;

namespace DirectLineClient_WPF
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        private string directLineSecret = ConfigurationManager.AppSettings["DirectLineSecret"];
        private string botId = ConfigurationManager.AppSettings["BotId"];
        private string fromUser = "DirectLineSampleClientUser";
        private Conversation Conversation = null;
        

        DirectLineClient Client = null;

        public MainWindow()
        {
            InitializeComponent();
            Client = new DirectLineClient(directLineSecret);
            this.Conversation = Client.Conversations.StartConversation();

            new System.Threading.Thread(async () => await ReadBotMessagesAsync(Client, this.Conversation.ConversationId)).Start();
        }

        private async void btnSend_Click(object sender, RoutedEventArgs e)
        {
            await StartBotConversation();
        }

        private async Task StartBotConversation()
        {
            string input = txtMessage.Text; // Console.ReadLine().Trim();

            if (input.Length > 0)
            {
                Activity userMessage = new Activity
                {
                    From = new ChannelAccount(fromUser),
                    Text = input,
                    Type = ActivityTypes.Message
                };

                lstChatroom.Items.Add(new Controls.userUserMessage(input));
                txtMessage.Focus();
                txtMessage.Text = "";

                lstChatroom.SelectedIndex = lstChatroom.Items.Count - 1;
                lstChatroom.ScrollIntoView(lstChatroom.SelectedItem);

                await Client.Conversations.PostActivityAsync(this.Conversation.ConversationId, userMessage);
            }
        }

        private async Task ReadBotMessagesAsync(DirectLineClient client, string conversationId)
        {
            string watermark = null;

            while (true)
            {
                var activitySet = await client.Conversations.GetActivitiesAsync(conversationId, watermark);
                watermark = activitySet?.Watermark;

                var activities = from x in activitySet.Activities
                                 where x.From.Id == botId
                                 select x;

                foreach (Activity activity in activities)
                {
                    //Console.WriteLine(activity.Text);
                    //MessageBox.Show(activity.Text);

                    Action action = () => {
                        lstChatroom.Items.Add(new Controls.userBotMessage(activity.Text));

                        lstChatroom.SelectedIndex = lstChatroom.Items.Count - 1;
                        lstChatroom.ScrollIntoView(lstChatroom.SelectedItem);
                    };
                    Dispatcher.BeginInvoke(action);
                    //Dispatcher.Invoke(DispatcherPri)

                    //if (activity.Attachments != null)
                    //{
                    //    foreach (Attachment attachment in activity.Attachments)
                    //    {
                    //        switch (attachment.ContentType)
                    //        {
                    //            case "application/vnd.microsoft.card.hero":
                    //                RenderHeroCard(attachment);
                    //                break;

                    //            case "image/png":
                    //                Console.WriteLine($"Opening the requested image '{attachment.ContentUrl}'");

                    //                Process.Start(attachment.ContentUrl);
                    //                break;
                    //        }
                    //    }
                    //}

                    Console.Write("Command> ");
                }

                await Task.Delay(TimeSpan.FromSeconds(1)).ConfigureAwait(false);
            }
        }
    }
}
