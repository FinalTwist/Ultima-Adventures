using System;
using Server;
using System.Collections;
using System.Collections.Generic;
using Server.Misc;
using Server.Items;
using Server.Network;
using Server.Commands;
using Server.Commands.Generic;
using Server.Mobiles;
using Server.Accounting;
using Server.Regions;
using Server.Multis;

namespace Server.Mobiles
{
    [CorpseName( "A GraveDigger corpse" )]
    public class GraveDigger2 : BaseCreature
    {
	public virtual bool IsInvulnerable{ get{ return true; } }
	private String lastgiven;
	private int numbergiven;

	[Constructable]
	public GraveDigger2() : base( AIType.AI_Animal, FightMode.None, 10, 1, 0.2, 0.4 )
	{
		Name = NameList.RandomName( "ratman" );
	    Title = "the GraveDigger";
	    Body = 42;
	    BaseSoundID = 0xCC;
	    Hidden = false;
	    CantWalk = true;
	    //Timer.DelayCall( TimeSpan.FromMinutes( 5.0 ), new TimerCallback( Delete ) );
	    Blessed = true;
		lastgiven = "";
		numbergiven = 0;

	}

	public GraveDigger2(Serial serial) : base(serial)
	{
	}

	public override bool HandlesOnSpeech( Mobile from ) 
	{ 
	    return true; 
	} 

	public override void OnSpeech( SpeechEventArgs e ) 
	{
	    if( e.Mobile.InRange( this, 4 ))
	    {
			if ( Insensitive.Contains( e.Speech, "fetch" ))
			{
				e.Mobile.SendMessage("Yes sire, as you command.");
				GetMyCorpse(e.Mobile);
			}
	    }
	    base.OnSpeech( e ); 

	}
	
		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if ( InRange( m, 4 ) && !InRange( oldLocation, 4 ) )
			{
				if ( m is PlayerMobile && !m.Hidden ) 
				{
                           		switch (Utility.Random(5))
                           		{
                                		case 0: Say("Holy Sire, I can fetch corpses in these lands for you."); break;
                        	     	 	case 1: Say("Greetings sire, speak to me when you need me to fetch a corpse."); break;
										case 2: Say("I live to fetch corpses for masters like you."); break;
                         	 	}
				
				}
			}

		}

		public override bool OnDragDrop( Mobile from, Item dropped )
		{
			if (dropped is CorpseItem)
			{
				
				if (dropped.Name == lastgiven)
					numbergiven +=1;
				else if (numbergiven >0 )
					numbergiven -=1;

				if (numbergiven > 3)
				{
					if (Utility.RandomBool())
						Say("I already have many bones like this, Master.");
					return true;
				}

				int amount = Utility.RandomMinMax(3, 5);
				if (((PlayerMobile)from).SoulBound)
					amount = Utility.RandomMinMax(5,10);
				AetherGlobe.ChangeCurse( -amount ); 
				Say("Yes Master, I will dig this one a grave.");
				Say("How kind of Master to help me.");

				if (from is PlayerMobile)
				{
					((PlayerMobile)from).BalanceEffect -= amount;
					Titles.AwardKarma( from, Utility.RandomMinMax(5,50), true );
				}

				return true;
			}

			return base.OnDragDrop( from, dropped );
		}

	public override void Serialize(GenericWriter writer)
	{
	    base.Serialize(writer);
	    writer.Write((int) 0);
	}

	public override void Deserialize(GenericReader reader)
	{
	    base.Deserialize(reader);
	    int version = reader.ReadInt();
	    //Timer.DelayCall( TimeSpan.FromSeconds( 1.0 ), new TimerCallback( Delete ) );
	}

	public static void GetMyCorpse( Mobile from )
	{

	    if (!from.Alive)
	    {
		from.SendMessage("But you are dead sire, I cannot help thee");
		return;
	    }

	    Map map = from.Map;

	    if ( map == null )
		return;

	    int distchk = 0;

	    ArrayList bodies = new ArrayList();
	    ArrayList empty = new ArrayList();
	    foreach ( Item body in World.Items.Values ) 
		if ( body is Corpse )
		{
		    Corpse cadaver = (Corpse)body;

		    int carrying = body.GetTotal( TotalType.Items );
			BaseHouse house = BaseHouse.FindHouseAt( body.Location, body.Map, 2 );

		    if ( house == null && cadaver.Owner == from && carrying > 0 )
		    {
			distchk++;
			bodies.Add( body ); 
		    }
		    else if ( house != null && cadaver.Owner == from && carrying < 1 )
		    {
			empty.Add( body );  
		    }
		}


	    for ( int u = 0; u < empty.Count; ++u ){ Item theEmpty = ( Item )empty[ u ]; theEmpty.Delete(); }
	    if ( distchk == 0 ){ from.SendMessage("You have no nearby corpse in this area!"); }
	    else
	    {

		int i_Bank;
		i_Bank = Banker.GetBalance( from );				
		Container bank = from.FindBankNoCreate();				
		if ( ( from.Backpack != null && from.Backpack.ConsumeTotal( typeof( Gold ), 20000 ) ) || ( bank != null && bank.ConsumeTotal( typeof( Gold ), 20000 ) ) )
		{
		    for ( int h = 0; h < bodies.Count; ++h )
		    {
			Corpse theBody = ( Corpse )bodies[ h ];
			theBody.MoveToWorld(new Point3D(from.X, from.Y, from.Z), from.Map);
		    }
		}
		else
		{
		    from.SendMessage("I only work for gold, Sire.");
		    from.SendMessage("Make sure you have 20,000 gold (coins) in your pack or bank.");
		}
	    }
	}


    }
}
