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
        public static async Task drawTeam(IVoiceChannel channel, int seed, int playerNum, SocketCommandContext context)
        {
            List<user> allUsers = userControl.UsersInChannel(channel);
            List<user> teamUsers = new List<user>();
            Random rnd = new Random(seed);
            int randomNum = 0;
            for (int i = 0; i < playerNum; i++)
            {
                randomNum = rnd.Next(0, allUsers.Count);
                teamUsers.Add(allUsers[randomNum]);
                allUsers.RemoveAt(randomNum);
            }
            allUsers.Clear();
            string teamUsersString = "";
            foreach (var item in teamUsers)
            {
                teamUsersString += "\n" + "\t" + item.Iuser.Mention;
            }
            await context.Channel.SendMessageAsync("Members of the team: " + teamUsersString);
        }
    }
}