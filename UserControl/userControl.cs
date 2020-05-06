using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using yeetbot.UserControl;

namespace yeetbot
{
    class userControl
    {
       static List<user> ActiveUsers = new List<user>();
        //Only ads users who connect to a voicechat or are already in one when you start up the bot, so inactive users don't take up memory

        public static async Task Initialize(DiscordSocketClient client)
        {
            foreach (var server in client.Guilds.ToList())
            {
                foreach (var uservar in server.Users.ToList())
                {
                    if (!uservar.IsBot  && uservar.VoiceChannel != null)
                    {
                        if (uservar.VoiceChannel.Guild == server)
                        {
                            ActiveUsers.Add(new user(uservar as IGuildUser, server as IGuild));
                            await Console.Out.WriteLineAsync("Már Online volt: " + uservar.Nickname + " (" + uservar.Username + ") a " + server.Name + " szerveren.");
                        }
                        
                    }
                }
            }
            return;
        }

        public static void Refresh(IGuildUser suser, SocketVoiceState oldc, SocketVoiceState newc)
        {
            /*
            IGuildUser user = suser as IGuildUser;
            if (oldc.VoiceChannel == null && newc.VoiceChannel != null)
            {
                if (kicklista.Kickelve(user as IGuildUser, newc.ToString()))
                {
                    IGuildUser a = user as IGuildUser;
                    a.ModifyAsync(x => { x.Channel = null; });
                    DateTime meddig = kicklista.Meddig(user as IGuildUser, newc.ToString());
                    TimeSpan maradt = meddig - DateTime.Now;

                    a.SendMessageAsync("You are temporarily banned from " + newc.ToString() + " until " + meddig + ". (" + maradt.ToString("mm\\:ss") + " remaining.)");
                }
                else
                {
                    kicklista.MarNemKickelve(user as IGuildUser, newc.ToString());
                    Console.WriteLine("Joined: " + user.Username +" " + user.Id + " " + newc.VoiceChannel.ToString());
                }
                ActiveUsers.Add(new users(user.Username, user.Id,user.Nickname, newc.VoiceChannel.ToString()));
            }
            else if (oldc.VoiceChannel != null && newc.VoiceChannel == null)
            {
                ActiveUsers.RemoveAt(FindInList(user.Username));
                Console.WriteLine("Left: " + user.Username + " " + user.Id + " " + oldc.VoiceChannel.ToString());
            }
            else
            {
                ActiveUsers[FindInList(user.Username)].channel = newc.ToString();
                if (kicklista.Kickelve(user as IGuildUser, newc.ToString()))
                {
                    IGuildUser a = user as IGuildUser;
                    a.ModifyAsync(x => { x.Channel = null; });
                    a.SendMessageAsync("You are temporarily banned from " + newc.ToString() + " until " + kicklista.Meddig(user as IGuildUser, newc.ToString()) + ".");
                }
                else
                {
                    kicklista.MarNemKickelve(user as IGuildUser, newc.ToString());
                }
            }
            */
        }
/*
        public static int FindInList(string nev)
        {
            for (int i = 0; i < ActiveUsers.Count; i++)
            {
                if (nev == ActiveUsers[i].username)
                {
                    return i;
                }
            }
            return -1;
        }

        public static void JoinedKiiras(DiscordSocketClient client)
        {
            foreach (var server in client.Guilds.ToList())
            {
                foreach (var user in server.Users.ToList())
                {
                    if (!user.IsBot && user.VoiceChannel != null)
                    {
                        ActiveUsers.Add(new users(user.Username, user.Id, user.Nickname, user.VoiceChannel.ToString()));
                        Console.WriteLine("Már online volt: " + user.Username + " " + user.Id + " " + user.VoiceChannel + " " + user.Nickname + " " + user.Guild);
                    }
                }
            }
            

        }

        public static void KiirasKonzolra()
        {
            foreach (var item in ActiveUsers)
            {
                Console.WriteLine(item.username + " #" + item.tag + " " + item.channel);
            }
        }
        */
        public static int UsersInChannel(IVoiceChannel a)
        {
            int db = 0;
            foreach (var item in ActiveUsers)
            {
                if (item.Channel == a)
                {
                    db++;
                }
            }
            return db;
        }
        /*
        public static bool InVoiceChannel(IGuildUser target)
        {
            foreach (var item in ActiveUsers)
            {
                if (item.tag == target.Id)
                {
                    return true;
                }
            }
            return false;
        }
        public static ulong IdByName(string target)
        {
            foreach (var item in ActiveUsers)
            {
                if (item.username == target || (item.nickname == target && item.nickname.Length > 0))
                {
                    return item.tag;
                }
            }
            return 0;
        }
        */
        public static async void VoteKick(IVoiceChannel where, SocketCommandContext context, user target, ulong caster)
        {
            await ActiveUsers[ActiveUsers.IndexOf(target)].Vote(where, 2, caster, context);
        }
        public static async Task UserUpdate(IGuildUser a, IGuildUser b)
        {
            user updateduser = userControl.GetUser(a.Id);
            if (updateduser != null)
            {
                //await updateduser.UserUpdate(b);
            }
        }

        public static user GetUser(ulong id)
        {
            foreach (var item in ActiveUsers)
            {
                if (item.Id == id)
                {
                    return item;
                }
            }
            return null;
        }
        public static user GetUser(string name)
        {
            foreach (var item in ActiveUsers)
            {
                if (item.Nick == name || item.Name == name)
                {
                    return item;
                }
            }
            return null;
        }
        
    }
    
}
