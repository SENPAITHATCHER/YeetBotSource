using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

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

    public async Task punish(IVoiceChannel where, int lvl)
    {
        res.Add(new restrictions(where, lvl));
        if (lvl == 1)
        {
            await iuser.ModifyAsync(x => x.Mute = false);
        }
        if (lvl == 2)
        {
            await iuser.ModifyAsync(x => x.Channel = null);
        }


        return;
    }
    }
}