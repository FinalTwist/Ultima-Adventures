using Server;
using Server.Network;

namespace Felladrin.Automations
{
    public static class PlayMusicOnLogin
    {
        public static class Config
        {
            public static bool Enabled = true;                          // Is this system enabled?
            public static bool PlayRandomMusic = true;                  // Should we play a random music from the list?
            public static MusicName SingleMusic = MusicName.OpenTitle;    // Music to be played if PlayRandomMusic = false.
        }

        public static void Initialize()
        {
            if (Config.Enabled)
                EventSink.Login += OnLogin;
        }

        static void OnLogin(LoginEventArgs args)
        {
            MusicName toPlay = Config.SingleMusic;

            if (Config.PlayRandomMusic)
                toPlay = MusicList[Utility.Random(MusicList.Length)];

            args.Mobile.Send(PlayMusic.GetInstance(toPlay));
        }
        
        public static MusicName[] MusicList = {
            MusicName.Opn_Gen,
            MusicName.Opn_Gen2,
            MusicName.Opn_Gen3,
            MusicName.Opn_Gen4,
            MusicName.Opn_Gen5,
            MusicName.Opn_Gen6,
            MusicName.Opn_Gen7,
            MusicName.Opn_Gen8,
            MusicName.Opn_Gen9,
            MusicName.Opn_Gen10,
            MusicName.Opn_Gen11,
            MusicName.Opn_Gen12,
            MusicName.Opn_Gen13
        };
    }
}