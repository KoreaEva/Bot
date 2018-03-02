using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Bot.Connector.DirectLine;
using Newtonsoft.Json;

namespace DirectLine
{
    class Program
    {
        private static string directLineSecret = "bqIyd2EGIkM.cwA.LOs.yHVfRz6OVDCmXayxuiVd_bHJ0EMupVV--DHp1g5NKSE";
        private static string botId = "GreatWall";
        private static string fromUser = "DirectLineSampleClientUser";


        static void Main(string[] args)
        {
            StartBotConversation().Wait();
        }

        private static async Task StartBotConversation()
        {
            DirectLineClient client = new DirectLineClient(directLineSecret);

            var conversation = await client.Conversations.StartConversationAsync();

            new System.Threading.Thread(async () => await ReadBotMessagesAsync(client, conversation.ConversationId)).Start();

            Console.Write("Command> ");

            while (true)
            {
                string input = Console.ReadLine().Trim();

                if (input.ToLower() == "exit")
                {
                    break;
                }
                else
                {
                    if (input.Length > 0)
                    {
                        Activity userMessage = new Activity
                        {
                            From = new ChannelAccount(fromUser),
                            Text = input,
                            Type = ActivityTypes.Message,
                        };

                        await client.Conversations.PostActivityAsync(conversation.ConversationId, userMessage);
                    }
                }
            }
        }

        private static async Task ReadBotMessagesAsync(DirectLineClient client, string conversationId)
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
                    byte[] temp = Encoding.UTF8.GetBytes(activity.Text);
                    string message = Encoding.UTF8.GetString(temp);

                    Console.WriteLine(message);

                    Console.Write("Command> ");
                }

                await Task.Delay(TimeSpan.FromSeconds(0.1)).ConfigureAwait(false);
            }
        }
    }
}
