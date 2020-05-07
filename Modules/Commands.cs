using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using Discord;
using Discord.Audio;
using System.Diagnostics;
using yeetbot.UserControl;

namespace yeetbot.Modules
{
    public class Commands : ModuleBase<SocketCommandContext>
    {
        //[Command("Prefix"), RequireUserPermission(GuildPermission.Administrator)] public Task Prefix([Remainder] string Newprefix)
        //{ if (!(Newprefix == "" || Newprefix == " ")) { if (Newprefix.Length > 1) { ReplyAsync("Invalid prefix: \"" + Newprefix + "\""); } else { config.NewPref(Newprefix[0]); ReplyAsync("The new Prefix is: \"" + Newprefix + "\""); } } else { ReplyAsync("Missing parameter(s) from command!"); } return null; }

        //----------------------------------------------------------------------------------------------------//
        /*
        [Command("VoteKick")]public Task VoteKick([Remainder] string target) // + ha valaki kickelve 3szor/5ször próbál joinolni akkor olyan rolet kap hogy sehova se tud bemenni
        {
            
            IGuildUser caster = Context.Message.Author as IGuildUser;
            //ulong targetId = userControl.IdByName(target);
            if (target.Substring(0, 3) == "<@!")
            {
               targetId = Convert.ToUInt64(target.Substring(3, target.Length - 4));

            }
            if (targetId !=0)
            {
                IGuildUser targetUser = Context.Guild.GetUser(targetId);
                if (caster.VoiceChannel == targetUser.VoiceChannel && caster != targetUser)
                {
                    //kicklista.Kickszavazas(targetUser, caster, Context);
                    
                }
                else
                {
                    Context.Channel.SendMessageAsync("You can only kick users who are in the same channel as you!");
                }
            }
            else
            {
                Context.Message.Channel.SendMessageAsync("User not found! ");
            }
            return null;
        }
*/
        //----------------------------------------------------------------------------------------------------//

        //[Command("VoteMute")] szavazás arról akit muteolni akarunk x embernek a 60% kell pl.  Admin szavazás nélkül teheti. (esetleg x időre szól a mute)
        //[Command("Unmute") komplementer parancs az előzőre]
        //[Command("teams") a szobába lévő játékosokat két csapatra osztja és kiírja a channel-be. Ha elfogadják a kiosztást akkor két külön szobába rakja őket. (??????ha 8 bol csak 6 embert kell osztani akkor azt a 2-t?????? \{aember, bember})]
        //Számláló ami számolja hogy czkrisz2 hányszor mute-olta magát és kiírja óránként.
        
        [Command("Ping")] public async Task Ping() { await ReplyAsync("Pong!"); }
        [Command("VoteKick")]public Task VoteKick([Remainder] string target)
        {
            user targetUser;
            if (target.Substring(0, 3) == "<@!")
            {
                targetUser = userControl.GetUser(Convert.ToUInt64(target.Substring(3, target.Length - 4)));
            }
            else
            {
                targetUser = userControl.GetUser(target);
            }
            if (targetUser == null || targetUser.Channel == null)
            {
                Context.Channel.SendMessageAsync("Target not found!");
            }
            else
            {
                if (targetUser.Channel == null)
                {
                    Context.Channel.SendMessageAsync("The target is not in a voice channel!");
                }
                else if ((Context.Message.Author as IGuildUser).VoiceChannel == null)
                {
                    Context.Channel.SendMessageAsync("You are not in a voice channel!");
                }
                else if (targetUser.Channel != (Context.Message.Author as IGuildUser).VoiceChannel)
                {
                    Context.Channel.SendMessageAsync("You can only vote against users who are in the same channel as you!");
                }
                else
                {
                    userControl.VoteKick(targetUser.Channel, Context, targetUser, Context.Message.Author.Id);
                }
            }
            return null;
        }
        [Command("Teams")]public async Task TeamSplit([Remainder] int SizeofTeams)
        {
            IVoiceChannel channel = (Context.Message.Author as IGuildUser).VoiceChannel;
            int connectedUsers = userControl.UserCountInChannel(channel);
            var sw = Stopwatch.StartNew();
            await ReplyAsync("Forming teams.");
            sw.Stop();
            int seed = Convert.ToInt32(Math.Floor(sw.Elapsed.TotalMilliseconds));
            await teamsplit.splitTeam(channel, SizeofTeams, seed, );
        }
        [Command("DrawTeam")][Alias("dt")] public async Task DrawnTeam([Remainder] int playerNum )
        {
            
            IVoiceChannel channel = (Context.Message.Author as IGuildUser).VoiceChannel;
            if (playerNum <= userControl.UserCountInChannel(channel) && playerNum > 0)
            {
                var sw = Stopwatch.StartNew();
                await ReplyAsync("Drawing Team."); 
                sw.Stop();
                int seed = Convert.ToInt32(Math.Floor(sw.Elapsed.TotalMilliseconds));
                await drawteam.drawTeam(channel, seed, playerNum, Context);
            }
            else if (playerNum <= 0)
            {
                await ReplyAsync(":exclamation: The size of the team can't be negative or zero :exclamation:");
            }
            else
            {
                await ReplyAsync(":exclamation: The size of the team can't be bigger than the number of actually connected users :exclamation:");
            }
        }
        [Command("Teszt"), RequireUserPermission(GuildPermission.Administrator)] public async Task Teszt([Remainder] string target) 
        { 
            if (target.Substring(0, 3) == "<@!")
            {
                await Context.Channel.SendMessageAsync(Convert.ToUInt64(target.Substring(3, target.Length - 4)).ToString());
            }
        }
        [Command("ping2")][Alias("latency")][Summary("Shows the websocket connection's latency and time it takes for me send a message.")]public async Task PingAsync()
        {
            // start a new stopwatch to measure the time it takes for us to send a message
            var sw = Stopwatch.StartNew();

            // send the message and store it for later modification
            var msg = await ReplyAsync($"**Websocket latency**: {Context.Client.Latency}ms\n" +
                                       "**Response**: ...");
            // pause the stopwatch
            sw.Stop();

            // modify the message we sent earlier to display measured time
            await msg.ModifyAsync(x => x.Content = $"**Websocket latency**: {Context.Client.Latency}ms\n" +
                                                   $"**Response**: {sw.Elapsed.TotalMilliseconds}ms");

         
        }
    }
}
