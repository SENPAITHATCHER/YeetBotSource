using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using Discord;
using Discord.Audio;
using System.IO;
using VideoLibrary;

public class AudioModule : ModuleBase<ICommandContext>
{
    // Scroll down further for the AudioService.
    // Like, way down
    private readonly AudioService _service;
    private static bool Joined = false;
    public IVoiceChannel voiceChannel;
    // Remember to add an instance of the AudioService
    // to your IServiceCollection when you initialize your bot
    public AudioModule(AudioService service)
    {
        _service = service;
    }

    // You *MUST* mark these commands with 'RunMode.Async'
    // otherwise the bot will not respond until the Task times out.
    [Command("join", RunMode = RunMode.Async)]
    public async Task JoinCmd()
    {
        try
        {
            await _service.JoinAudio(Context.Guild, (Context.User as IVoiceState).VoiceChannel);
            await Context.Channel.SendMessageAsync("Joined to \"" + (Context.User as IVoiceState).VoiceChannel.Name + "\" " + Context.Message.Author.Mention + "!");
            Joined = true;
        }
        catch (Exception e) { Console.WriteLine(e); }
    }

    // Remember to add preconditions to your commands,
    // this is merely the minimal amount necessary.
    // Adding more commands of your own is also encouraged.
    [Command("leave", RunMode = RunMode.Sync)]
    public async Task LeaveCmd()
    {
        Joined = false;
        await _service.LeaveAudio(Context.Guild);
    }
    

    [Command("play", RunMode = RunMode.Async)]
    public async Task PlayCmd([Remainder] string song)
    {
        if (!Joined)
        {
            Joined = true;
        }
        await _service.SendAudioAsync(Context.Guild, Context.Channel, song);
        
    }
              
}