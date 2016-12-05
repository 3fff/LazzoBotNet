using System;
using Discord;
using Discord.Commands;

namespace LazzoBotNet
{
    class Program
    {
        static void Main(string[] args) => new Program().Initialize();

        private DiscordClient client;

        public void Initialize()
        {
            string token = "MjUyMDc4ODI3NDgyNzc1NTUy.CyVSHg.X_YGRUkiXVLxKwd6uSQTeUrYY3Y";
            client = new DiscordClient(x =>
            {
                x.AppName = "LazzoBotNet";
                x.AppUrl = "http://routepoint.xyz";
                x.AppVersion = "Version 0.1 Beta";
                x.LogLevel = LogSeverity.Info;
                x.LogHandler = LogHandler;
            });

            client.ChannelCreated += OnChannelCreated;
            client.UserUpdated += OnUserUpdated;

            client.ExecuteAndWait(async () =>
            {
                await client.Connect(token, TokenType.Bot);
            });

            client.UsingCommands(x =>
            {
                x.PrefixChar = '!';
                x.HelpMode = HelpMode.Public;
            });
        }

        private static async void OnChannelCreated(object sender, ChannelEventArgs e)
        {
            DateTime time = DateTime.Now;
            var logChannel = e.Server.GetChannel(254990946536652801);
            await logChannel.SendMessage($"{time.ToString("h:mm:ss tt")} Lev has created {e.Channel.Name} channels.");
            Console.WriteLine("A new channel has been created.\n--");
        }

        private static async void OnUserUpdated(object sender, UserUpdatedEventArgs e)
        {
            DateTime time = DateTime.Now;
            var logChannel = e.Server.GetChannel(254990946536652801);
            if (e.After.VoiceChannel == null) return;
            if (e.Before.VoiceChannel == e.After.VoiceChannel) return;

            await logChannel.SendMessage($"{time.ToString("h:mm:ss tt")} User {e.After.Name} has joined {e.After.VoiceChannel} channels.");
            Console.WriteLine($"User {e.After.Name} was moved to another channel.\n--");
        }

        private static void LogHandler(object sender, LogMessageEventArgs x)
        {
            Console.WriteLine(x.Message);
        }
    }
}
