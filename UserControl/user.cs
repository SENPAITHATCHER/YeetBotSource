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

namespace yeetbot.UserControl
{
    class user
    {
        private IGuildUser iuser;
        private ulong id;
        private string name;
        private string nick;
        private IVoiceChannel channel;
        private IGuild guild;
        private List<restrictions> res = new List<restrictions>();
        private List<vote> votes = new List<vote>();

        public IGuildUser Iuser { get => iuser;  }
        public ulong Id { get => id;  }
        public string Name { get => name;  }
        public string Nick { get => nick;  }
        public IVoiceChannel Channel { get => channel;  }
        public IGuild Guild { get => guild;  }

        public user(IGuildUser iuser, IGuild guild)
        {
            this.iuser = iuser;
            this.id = iuser.Id;
            this.name = iuser.Username;
            if (iuser.Nickname =="")
            {
                this.nick = iuser.Username;
            }
            else
                this.nick = iuser.Nickname;
            this.channel = iuser.VoiceChannel;
            this.guild = guild;
        }

    public Task UserUpdate(IGuildUser newu)
    {
        if (newu.Nickname != this.nick)
        {
            if (newu.Nickname == "")
            {
                this.nick = newu.Username;
            }
            else
                this.nick = newu.Nickname;
        }
        if (newu.Username != this.name)
        {
            this.name = newu.Username;
        }
        return null;
    }
    public int RestrictionLevel(IGuildChannel c)
    {
        foreach (var item in res)
        {
            if (item.Channel == c)
            {
                return item.Level;
            }
        }
        return 0;
    }

    public async Task Punish(IVoiceChannel where, byte lvl)
    {
        res.Add(new restrictions(where, lvl));
        if (lvl == 1)
        {
            await iuser.ModifyAsync(x => { x.Mute = false; });
        }
        if (lvl == 2)
        {
            Console.Write("Kickelve");
            await (iuser as IGuildUser).ModifyAsync(x => { x.Channel = null; });
        }
        await Task.Delay(15*1000*60);
        res.Remove(new restrictions(where, lvl));
        return;
    }

    public bool OngoingVote(IVoiceChannel where, byte lvl)
    {
        foreach (var item in votes)
        {
            if (item.Where == where && item.Lvl == lvl)
            {
                return true;
            }
        }
        return false;
    }


    public async Task Vote(IVoiceChannel where, byte lvl, ulong voter, SocketCommandContext context)
    {
        if (OngoingVote(where, lvl))
        {
            int votes = AddVote(where, lvl, voter, context);
            int maxvotes = Convert.ToInt32(Math.Ceiling(userControl.UserCountInChannel(where)*0.6));
            if (lvl == 1)
            {
                await context.Channel.SendMessageAsync("Vote for muting " + nick + "(" + where.ToString() + "): " + votes + "/" + maxvotes);
            }
            else
            {
                await context.Channel.SendMessageAsync("Vote for kicking " + nick + "(" + where.ToString() + "): " + votes + "/" + maxvotes);
            }
            if (votes >= maxvotes)
            {
                StopVote(where, lvl);
                res.Add(new restrictions(where, lvl));        
                if (lvl == 1)
                {
                    await context.Channel.SendMessageAsync("The vote for muting " + nick + " was successful! (" + where.ToString() + ")");
                }
                else
                {
                    await context.Channel.SendMessageAsync("The vote for kicking " + nick + " was successful! (" + where.ToString() + ")");
                    await Punish(where, 2);
                }
            }
        }
        else
        {
            await CreateVote(where, lvl, voter, context);
        }

    }

    private int AddVote(IVoiceChannel where, byte lvl, ulong voter, SocketCommandContext context)
    {
        foreach (var item in votes)
        {
            if (item.Lvl == lvl && item.Where == where)
            {
                return item.Vote(voter);
            }
        }
        return 1;
    }

    private async Task CreateVote(IVoiceChannel where, byte lvl, ulong voter, SocketCommandContext context)
    {
        votes.Add(new vote(where, lvl, voter));
        if (lvl == 1)
        {
            await context.Channel.SendMessageAsync("The vote for muting " + nick + " has started! (" + where.ToString() + ") Use $votemute [user] to vote!");
            await context.Channel.SendMessageAsync("Vote for muting " + nick + "(" + where.ToString() + "): " + 1 + "/" + Convert.ToInt32(Math.Ceiling(userControl.UserCountInChannel(where)*0.6)));
        }
        else
        {
            await context.Channel.SendMessageAsync("The vote for kicking " + nick + " has started! (" + where.ToString() + ") Use $votekick [user] to vote!");
            await context.Channel.SendMessageAsync("Vote for kicking " + nick + "(" + where.ToString() + "): " + 1 + "/" + Convert.ToInt32(Math.Ceiling(userControl.UserCountInChannel(where)*0.6)));
        }
        await Task.Delay(1*1000*60);
        if (OngoingVote(where, lvl))
        {
            StopVote(where, lvl);
            if (lvl == 1)
                {
                    await context.Channel.SendMessageAsync("The vote for muting " + nick + " was unsuccessful! (" + where.ToString() + ")");
                }
                else
                {
                    await context.Channel.SendMessageAsync("The vote for kicking " + nick + " was unsuccessful! (" + where.ToString() + ")");
                }
            
        }      
    }

    private void StopVote(IVoiceChannel where, byte lvl)
    {
        for (int i = votes.Count - 1; i >= 0; i--)
            {
                if (where == votes[i].Where && lvl == votes[i].Lvl)
                {
                    votes.RemoveAt(i);
                }
            }
    }
    

    public async Task MoveUser(IVoiceChannel channel)
    {
        if (channel == null)
        {
            return;
        }
        await (iuser as IGuildUser).ModifyAsync(x => { x.Channel = Optional.Create(channel);});

    }
}
}
