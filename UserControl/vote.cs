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
    public class vote
    {
        private byte lvl;
        private IVoiceChannel where;

        private HashSet<ulong> voters = new HashSet<ulong>();

        public vote(IVoiceChannel where, byte lvl, ulong voter)
        {
            this.lvl = lvl;
            this.where = where;
            voters.Add(voter);
        }

        public byte Lvl { get => lvl; }
        public IVoiceChannel Where { get => where; }

        public int Vote(ulong id)
        {
            voters.Add(id);
            return voters.Count;
        }
    }
}