using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Discord.Audio;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace yeetbot
{
    class Program
    {
        static void Main(string[] args) => new Program().RunBotAsync().GetAwaiter().GetResult();
        private DiscordSocketClient _client;
        private CommandService _commands;
        private IServiceProvider _services;
        private AudioService _audio;

        private IConfiguration _config;



        public async Task RunBotAsync()
        {
            _client = new DiscordSocketClient();
            _commands = new CommandService();
            _audio = new AudioService();
            _services = new ServiceCollection().AddSingleton(new AudioService())
            .AddSingleton(_client)
            .AddSingleton(_commands)
            .BuildServiceProvider();

            var _builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile(path: "config.json");            
            _config = _builder.Build();
            _client.Log += _client_Log;
            
            //await SetGame();

            await _client.LoginAsync(TokenType.Bot, _config["Token"]);
            await _client.StartAsync();
            await RegisterCommandAsync();
            
            await Task.Delay(1500);
            
            await userControl.Initialize(_client);
            
            await Task.Delay(-1);

        }

        private Task _client_Log(LogMessage arg)
        {
            Console.WriteLine(arg);
            return Task.CompletedTask;
        }

        public async Task RegisterCommandAsync()
        {
            _client.MessageReceived += HandleCommandAsync;
            _client.UserVoiceStateUpdated += (user, before, after) =>
            {
                if (before.VoiceChannel != after.VoiceChannel)
                {
                    Console.WriteLine($"VoiceStateUpdate: {user} - {before.VoiceChannel?.Name ?? "null"} -> {after.VoiceChannel?.Name ?? "null"}");
                    //userControl.Refresh(user, before, after);
                }
                return Task.CompletedTask;
            };
            _client.GuildMemberUpdated +=  userControl.UserUpdate;
            
            
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
        }

        private async Task HandleCommandAsync(SocketMessage arg)
        {
            var message = arg as SocketUserMessage;
            var context = new SocketCommandContext(_client, message);
            if (message.Author.IsBot) return;
            int argPos = 0;
            
            string pref = _config["Prefix"];
            Console.WriteLine(pref);
            if (message.HasStringPrefix(pref, ref argPos))
            {
                var result = await _commands.ExecuteAsync(context, argPos, _services);
                if (!result.IsSuccess)
                {
                    if (result.Error == CommandError.UnknownCommand)
                    {
                        await message.Channel.SendMessageAsync("Unknown command! Type " + pref + "help to see all available commands!");
                    }
                    else if (result.Error == CommandError.BadArgCount)
                    {
                        await message.Channel.SendMessageAsync("The arguments for the command are invalid! Type " + pref + "help to see all available commands and their usage!");//$command visszaadja  a command beirando parametereit
                    }
                    else
                    {
                        Console.WriteLine(result.ErrorReason);
                    }
                }
                    

            }
        }
    }
}
