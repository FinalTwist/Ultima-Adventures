//--------------------------------------------------------------------------------
// Copyright Joe Kraska, 2006. This file is restricted according to the GPL.
// Terms and conditions can be found in COPYING.txt.
//--------------------------------------------------------------------------------
using System;
//--------------------------------------------------------------------------------
using Server;
using Server.Commands;
using Server.Commands.Generic;
using Server.Mobiles;
using Server.Misc;
using Server.Targeting;
using System.IO;
//--------------------------------------------------------------------------------
namespace Server.Scripts.Commands {
//--------------------------------------------------------------------------------
public class RandomEncountersControl
{
    public static void Initialize()
    { 
        Server.Commands.CommandSystem.Register( "rand", AccessLevel.Administrator, new CommandEventHandler( OnCommand ) );
    }

    public static void OnCommand( CommandEventArgs e )
    {
        if( e.Length >= 1 )
        {
            switch( e.Arguments[0] )
            {
                case "import":

                    Import( e );
                    break;

                case "init":

                    Init( e );
                    break;

                case "now":

                    Now( e );
                    break;

                case "stop":

                    Stop( e );
                    break;


            }
        }
    }
    public static void Import( CommandEventArgs e )
    {
        Console.WriteLine("RandomEncounters: Import() command");
        ImportHelper.Import( e.Mobile );
    }
    public static void Now( CommandEventArgs e )
    {
        if( e.Length != 2 ) { e.Mobile.SendMessage( "usage: rand now [timertype]" ); return; }

        Console.WriteLine("RandomEncounters: Now() command");

        RandomEncounterEngine.GenerateEncounters( e.Arguments[1] );
    }
    public static void Init( CommandEventArgs e )
    {
        Console.WriteLine("RandomEncounters: Init() command");
        RandomEncounterEngine.Initialize();
    }
    public static void Stop( CommandEventArgs e )
    {
        Console.WriteLine("RandomEncounters: Stop() command");
        RandomEncounterEngine.Stop();
    }
}
//--------------------------------------------------------------------------------
} // namespace Server.Scripts.Commands
//--------------------------------------------------------------------------------
