using System;
using System.Collections;
using Server.Items;
using Server.ContextMenus;
using Server.Misc;
using Server.Network;

namespace Server.Mobiles 
{ 
	public class BaseNPC : BaseCreature
	{
		[Constructable] 
		public BaseNPC() : base( AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			Blessed = true;
			SpeechHue = Server.Misc.RandomThings.GetSpeechHue();
			NameHue = 1154;
			Hue = Server.Misc.RandomThings.GetRandomSkinColor();

			if ( Female = Utility.RandomBool() ) 
			{ 
				this.Body = 0x191;
				this.Name = NameList.RandomName( "female" );
			}
			else 
			{ 
				this.Body = 0x190;
				this.Name = NameList.RandomName( "male" );
			}
		}

		public override bool ClickTitle{ get{ return false; } }
		public override bool ShowFameTitle{ get{ return false; } }
		public override bool AlwaysAttackable{ get{ return false; } }
		public override bool CanRummageCorpses{ get{ return false; } }
		public override bool ReacquireOnMovement{ get{ return true; } }
		public override bool DeleteCorpseOnDeath{ get{ return true; } }
		public virtual bool IsInvulnerable { get { return true; } }
		public override bool Unprovokable { get { return true; } }
		public override bool Uncalmable{ get{ return true; } }

		public BaseNPC( Serial serial ) : base( serial ) 
		{ 
		} 

		public override void Serialize( GenericWriter writer ) 
		{ 
			base.Serialize( writer ); 
			writer.Write( (int) 0 ); // version 
		} 

		public override void Deserialize( GenericReader reader ) 
		{ 
			base.Deserialize( reader ); 
			int version = reader.ReadInt(); 
		} 
	}
}   