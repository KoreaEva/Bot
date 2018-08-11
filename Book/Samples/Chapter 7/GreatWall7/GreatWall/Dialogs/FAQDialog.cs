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
    //[QnAMakerService("https://westus.api.cognitive.microsoft.com/qnamaker/v4.0","7a8abeed-12d4-4513-90f8-48cab30a42ee","592fd964-0e7b-4d9b-b343-6a15f27e429d",MaxAnswers = 5)]
    [QnAMakerService("https://greatwallqna.azurewebsites.net/qnamaker","7a8abeed-12d4-4513-90f8-48cab30a42ee", "592fd964-0e7b-4d9b-b343-6a15f27e429d", MaxAnswers = 5)]
    public class FAQDialog : QnAMakerDialog<string>
    {
        /// <summary>
        /// Handler used when the QnAMaker finds no appropriate answer
        /// </summary>
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

            // ProcessResultAndCreateMessageActivity will remove any attachment markup from the results answer
            // and add any attachments to a new message activity with the message activity text set by default
            // to the answer property from the result
            //var messageActivity = ProcessResultAndCreateMessageActivity(context, ref result);
            //messageActivity.Text = $"I found {result.Answers.Length} answer(s) that might help...here is the first, which returned a score of {result.Answers.First().Score}...{result.Answers.First().Answer}";

            await context.PostAsync(result.Answers.First().Answer);

            context.Wait(MessageReceived);
        }

        /// <summary>
        /// Handler to respond when QnAMakerResult score is a maximum of 0.5
        /// </summary>
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