using System;
using Server;
using Server.Items;
using Server.Gumps;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace Server.Mobiles
{
    [CorpseName( "A blood lich corpse" )]
    public class BloodLich : BaseCreature
    {
	public virtual bool IsInvulnerable{ get{ return true; } }

	private DateTime m_NextMockery = DateTime.UtcNow;

	public DateTime NextMockery {
		get { return m_NextMockery; }
		set { m_NextMockery = value; }
	}

	public static string BadForceReply {
		get { return "Don't insult me mortal, your soul is not yet strong enough!"; }
		set {}
	}

	public static string AlreadyHaveOne {
		get { return "You already have one of these, you don't need another"; }
		set {}
	}

	[Constructable]
	public BloodLich() : base( AIType.AI_Mage, FightMode.None, 10, 1, 0.2, 0.4 )
	{		
		Name = NameList.RandomName( "ancient lich" );
		Title = "the blood lich";
		Body = 24;
		Hue = 1994;
	    BaseSoundID = 412;
	    Hidden = false;
	    CantWalk = false;
	    Blessed = true;
	}

	public BloodLich(Serial serial) : base(serial)
	{
	}

	public override bool HandlesOnSpeech( Mobile from ) 
	{ 
	    return true; 
	} 

	public override void OnMovement( Mobile mobile, Point3D oldLocation )
		{
		if (!(mobile is PlayerMobile) || mobile.Map == null || mobile == null || this.Map == null || this == null || this.Controlled) 
			return;

		if (mobile is PlayerMobile && mobile.AccessLevel == AccessLevel.Player)
		{
			if ( InRange( mobile, 4 ) && !InRange( oldLocation, 4 ) && InLOS( mobile ) && !mobile.Hidden )
			{	
				if (this.m_NextMockery < DateTime.UtcNow) {
					PlayerMobile player = (PlayerMobile)mobile;
		    		Phylactery phylactery = player.FindPhylactery();
					if (phylactery != null) {
						int power = phylactery.TotalPower();
						if (power < 10) {
							SayTo( player, "Bah! Your vault is weak!" );
						} else if (power < 100) {
							SayTo( player, "Haha! Your vault is mediocre" );
						} else if (power < 500) {
							SayTo( player, "Meh, your vault is ok I guess!" );
						} else if (power < 800) {
							SayTo( player, "Your soul's power is getting there, mortal." );
						} else if (power <= phylactery.GetRawPoints(mobile)) {
							SayTo( player, "Your vault is extremely powerful! Care to trade?!" );
						}
					} else if (player.SoulBound) {
						SayTo( player, "Ah a new soulbound being, speak the words 'soul tome' to begin." );
					} else {
						SayTo( player, "Your soul is not bound, be gone." );
					}
					player.SendSound( 0x19D );
					this.m_NextMockery = DateTime.UtcNow.AddSeconds(15);
				}
			}
		}
	}
	// teleport rune get player MAP and port them to appropriate entrance

	public override void OnSpeech( SpeechEventArgs e ) 
	{
		string badReply = "I do not understand your mortal tongue.";
	    if( e.Mobile.InRange( this, 4 ))
	    {
	    	if (e.Mobile is PlayerMobile) {
	    		string speech = e.Speech.ToLower();
	    		PlayerMobile player = (PlayerMobile)e.Mobile;
	    		if (!player.SoulBound) {
	    			SayTo( player, "You should not be here " + player.Name + ", your soul is not yet immortal." );
	    			return;
    			} else {
		    		Phylactery playersPhylactery = player.FindPhylactery();
    				int force = player.SoulForce;
    				int newForce = force;
    				if (speech == "soul tome") {
	    				SayTo( player, "Here is a knowledge tome all about soul binding, you have much to learn" );
	    				SoulTome tome = new SoulTome();
	    				player.Backpack.DropItem(tome);
	    				player.SendMessage("A dark and ancient book has been placed in your backpack.");
		    		} else if (speech == "vault") {
		    			if ( playersPhylactery == null ) {
		    				if (force >= 500) {
			    				SayTo( player, "I shall give you an ancient item of power. A vault.. in return for some of your soul's force." );
			    				player.SendMessage("You feel your soul ripple as the vault tears at your inner thoughts");
			    				Phylactery phylactery = new Phylactery();
			    				player.Backpack.DropItem(phylactery);
			    				newForce -= 500;
			    			} else {
			    				SayTo( player, BadForceReply );
			    			}
	    				} else {
	    					SayTo( player, "Er, I am not sure your soul can handle two vaults." );
	    				}
	    			} else if (speech == "mamina manu") {
	    				if (playersPhylactery == null) {
	    					SayTo( player, "Haha, nice try, but you require a vault before you can begin the binding of souls" );
	    				} else {
	    					SayTo( player, "Excellent, let us begin the binding of soul essences!");
	    					player.SendGump( new PhylacteryGump( e.Mobile, playersPhylactery, this, 1) );
	    				}
					} else {
		    			SayTo( player, badReply);
		    		}
		    		player.SoulForce = newForce;
				}
	    	}
	    }
	    base.OnSpeech( e ); 

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
	}
	
    }
}
