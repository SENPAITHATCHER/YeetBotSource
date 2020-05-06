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

        //----------------------------------------------------------------------------------------------------//
/*
        static bool Firstplayer = true;//RPS
        static string[] rpsdraws = new string[3]
        {
            "ROCK",
            "PAPER",
            "SCISSORS"
        };//RPS
        public static List<ulong> vips = new List<ulong>()
        {
            290031898863075329
        };//RRPS
        IGuildUser Player2;//RPS
        string Player2draw;//RPS
        private string Kinyert(string p1, string p2)
        {
            switch (p1)
            {
                case "ROCK":
                    switch (p2)
                    {
                        case "ROCK": return Player2.Mention + " drawed with " + config.Player1.Mention;
                        case "PAPER": return Player2.Mention + " defeated " + config.Player1.Mention;
                        case "SCISSORS": return config.Player1.Mention + " defeated " + Player2.Mention;
                        default: break;
                    } break;
                case "PAPER":
                    switch (p2)
                    {
                        case "PAPER": return Player2.Mention + " drawed with " + config.Player1.Mention;
                        case "SCISSORS": return Player2.Mention + " defeated " + config.Player1.Mention;
                        case "ROCK": return config.Player1.Mention + " defeated " + Player2.Mention;
                        default: break;
                    } break;
                case "SCISSORS":
                    switch (p2)
                    {
                        case "SCISSORS": return Player2.Mention + " drawed with " + config.Player1.Mention;
                        case "ROCK": return Player2.Mention + " defeated " + config.Player1.Mention;
                        case "PAPER": return config.Player1.Mention + " defeated " + Player2.Mention;
                        default: break;
                    } break;
                default: break;
            }
            return "ERROR";
        }//RPS
        Random rnd = new Random();//RPS
        [Command("rps")]public async Task rps() //1-ko 2-papir 3-ollo
        {

            if (Firstplayer) //Első játékos
            {
                config.Player1 = Context.Message.Author as IGuildUser;
                config.Player1draw = rpsdraws[rnd.Next(0, 3)];
                await Context.Channel.SendMessageAsync(Context.Message.Author.Mention + " wants to play Rock, Paper, Scissors. Use command " + config.prefix + "rps to play!");
                Firstplayer = false;
                Console.Write("rps: " + Firstplayer + " " + config.Player1.Id);

            }
            else
            {
                Player2 = Context.Message.Author as IGuildUser;
                if (vips.Contains(Player2.Id) && vips.Contains(config.Player1.Id))
                {
                    Player2draw = rpsdraws[rnd.Next(0, 3)];
                    await Context.Channel.SendMessageAsync(Player2.Mention + " has challenged  " + config.Player1.Mention);
                    System.Threading.Thread.Sleep(1000);
                    await Context.Channel.SendMessageAsync(config.Player1.Mention + " has drawn " + config.Player1draw);
                    System.Threading.Thread.Sleep(1000);
                    await Context.Channel.SendMessageAsync(Player2.Mention + " has drawn " + Player2draw);
                    System.Threading.Thread.Sleep(500);
                    await Context.Channel.SendMessageAsync(Kinyert(config.Player1draw, Player2draw));
                }
                else if (vips.Contains(Player2.Id)) // második vip, első nem -> mindig vip nyer
                {
                    string Player2draw = "####Error";
                    switch (config.Player1draw)
                    {
                        case "ROCK": Player2draw = "PAPER"; break;
                        case "PAPER": Player2draw = "SCISSORS"; break;
                        case "SCISSORS": Player2draw = "ROCK"; break;
                        default:
                            break;
                    }
                    await Context.Channel.SendMessageAsync(Player2.Mention + " has challenged  " + config.Player1.Mention);
                    System.Threading.Thread.Sleep(1000);
                    await Context.Channel.SendMessageAsync(config.Player1.Mention + " has drawn " + config.Player1draw);
                    System.Threading.Thread.Sleep(1000);
                    await Context.Channel.SendMessageAsync(Player2.Mention + " has drawn " + Player2draw);
                    System.Threading.Thread.Sleep(500);
                    await Context.Channel.SendMessageAsync(Kinyert(config.Player1draw, Player2draw));
                }
                else if (vips.Contains(config.Player1.Id))
                {
                    string Player2draw = "####Error";
                    switch (config.Player1draw)
                    {
                        case "ROCK": Player2draw = "SCISSORS"; break;
                        case "PAPER": Player2draw = "ROCK"; break;
                        case "SCISSORS": Player2draw = "PAPER"; break;
                        default:
                            break;
                    }
                    await Context.Channel.SendMessageAsync(Player2.Mention + " has challenged  " + config.Player1.Mention);
                    System.Threading.Thread.Sleep(1000);
                    await Context.Channel.SendMessageAsync(config.Player1.Mention + " has drawn " + config.Player1draw);
                    System.Threading.Thread.Sleep(1000);
                    await Context.Channel.SendMessageAsync(Player2.Mention + " has drawn " + Player2draw);
                    System.Threading.Thread.Sleep(500);
                    await Context.Channel.SendMessageAsync(Kinyert(config.Player1draw, Player2draw));
                }
                else
                {
                    Player2draw = rpsdraws[rnd.Next(0, 3)];
                    await Context.Channel.SendMessageAsync(Player2.Mention + " has challenged  " + config.Player1.Mention);
                    System.Threading.Thread.Sleep(1000);
                    await Context.Channel.SendMessageAsync(config.Player1.Mention + " has drawn " + config.Player1draw);
                    System.Threading.Thread.Sleep(1000);
                    await Context.Channel.SendMessageAsync(Player2.Mention + " has drawn " + Player2draw);
                    System.Threading.Thread.Sleep(500);
                    await Context.Channel.SendMessageAsync(Kinyert(config.Player1draw, Player2draw));
                }
                Firstplayer = true;
            }
        }
*/
        //----------------------------------------------------------------------------------------------------//

        [Command("Ping")] public async Task Ping() { await ReplyAsync("Pong!"); }

        //----------------------------------------------------------------------------------------------------//

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
        //[Command("teams") a szobába lévő játékosokat két csapatra osztja és kiírja a console-ra. Ha elfogadják a kiosztást akkor két külön szobába rakja őket. (ha 8 bol csak 6 embert kell osztani akkor azt a 2-t \{aember, bember})]
        //[Command("drawteam") a szobában lévőkből sorsol x(felhasznalotol parameter) embert, egy csapatba (döntés segítés)]
        
        //Számláló ami számolja hogy czkrisz2 hányszor mute-olta magát és kiírja óránként.
        //

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
        [Command("DrawTeam")] public async Task DrawnTeam([Remainder] int playerNum )
        {
            var sw = Stopwatch.StartNew();
            await ReplyAsync("Drawing Team.");
            sw.Stop();
            int seed = Convert.ToInt32(Math.Floor(sw.Elapsed.TotalMilliseconds));
            IVoiceChannel channel = (Context.Message.Author as IGuildUser).VoiceChannel;
            await drawteam.drawTeam(channel, seed);
        }

        [Command("Teszt"), RequireUserPermission(GuildPermission.Administrator)] public async Task Teszt([Remainder] string target) 
        { 
            if (target.Substring(0, 3) == "<@!")
            {
                await Context.Channel.SendMessageAsync(Convert.ToUInt64(target.Substring(3, target.Length - 4)).ToString());
            }
        }
        //
        [Command("ping2")]
        [Alias("latency")]
        [Summary("Shows the websocket connection's latency and time it takes for me send a message.")]
        public async Task PingAsync()
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
