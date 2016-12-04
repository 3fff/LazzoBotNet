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

            client.ChannelCreated += async (s, e) => 
            {
                var logChannel = e.Server.GetChannel(254990946536652801);
                await logChannel.SendMessage($"Lev has created {e.Channel.Name} channels.");
                Console.WriteLine("A new channel has been created.\n--");
            };

            client.UserUpdated += async (s, e) =>
            {
                var logChannel = e.Server.GetChannel(254990946536652801);
                if (e.After.VoiceChannel == null) return;
                if (e.Before.VoiceChannel == e.After.VoiceChannel) return;

                await logChannel.SendMessage($"User {e.After.Name} has joined {e.After.VoiceChannel} channels.");
                Console.WriteLine("User was moved to another channel.\n--");
            };

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

        private static void LogHandler(object sender, LogMessageEventArgs x)
        {
            Console.WriteLine(x.Message);
        }
    }
}
