﻿using SOCVR.Chatbot.Database;
using System;
using TCL.Extensions;

namespace SOCVR.Chatbot.ChatbotActions.Commands.Stats
{
    internal class Stats : UserCommand
    {
        public override string ActionDescription =>
            "Shows the stats at the top of the /review/close/stats page.";

        public override string ActionName => "Stats";

        public override string ActionUsage => "stats";

        public override PermissionGroup? RequiredPermissionGroup => PermissionGroup.Reviewer;

        protected override string RegexMatchingPattern => "^(close vote )?stats( (please|pl[sz]))?$";

        public override void RunAction(ChatExchangeDotNet.Message incomingChatMessage, ChatExchangeDotNet.Room chatRoom)
        {
            var sa = new CloseQueueStatsAccessor();
            var stats = sa.GetOverallQueueStats();

            var message = new[]
            {
                $"{stats.NeedReview} need review",
                $"{stats.ReviewsToday} reviews today",
                $"{stats.ReviewsAllTime} reviews all-time",
            }
            .ToCSV(Environment.NewLine);

            chatRoom.PostMessageOrThrow(message);
        }
    }
}
