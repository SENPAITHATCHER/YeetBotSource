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
    public class teamsplit
    {
        public static async Task splitTeam(IVoiceChannel channel, int SizeofTeams, int seed)
        {
            List<user> allUsers = userControl.UsersInChannel(channel);
            Random rnd = new Random(seed);
            for (int i = 0; i < SizeofTeams; i++)
            {
                int random = rnd.Next(0, allUsers.Count);
                user userToMove = allUsers[random];
                allUsers.RemoveAt(random);
                await userToMove.MoveUser();
            }

        }
    }
}