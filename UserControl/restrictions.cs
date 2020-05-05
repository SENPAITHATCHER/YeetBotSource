
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

namespace yeetbot.UserControl
{
    public class restrictions
    {
        private IVoiceChannel channel;
        private int level;

        public restrictions(IVoiceChannel channel, int level)
        {
            this.channel = channel;
            this.level = level;
        }

        public IVoiceChannel Channel { get => channel; }
        public int Level { get => level; }
    }
}