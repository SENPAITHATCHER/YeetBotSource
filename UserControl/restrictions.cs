
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
        private byte level;
        private DateTime expiry = new DateTime();

        public restrictions(IVoiceChannel channel, byte level)
        {
            this.channel = channel;
            this.level = level;
            expiry = DateTime.Now.AddMinutes(15);
        }

        public IVoiceChannel Channel { get => channel; }
        public int Level { get => level; }
    }
}