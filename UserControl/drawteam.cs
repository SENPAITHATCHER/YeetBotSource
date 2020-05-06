using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using Discord;
using Discord.Audio;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;
using yeetbot;


namespace yeetbot.UserControl
{
    static public class drawteam
    {
        public static async Task drawTeam(IVoiceChannel channel, int seed)
        {
            List<user> allUsers = userControl.UsersInChannel(channel);
        }
        
    }
}