using Server;
using System;
using System.Collections;
using Server.Network;
using Server.Targeting;
using Server.Prompts;
using Server.Misc;
using Server.Mobiles;


namespace Server.Items
{
	public class HolyDraughtOfHumility : Item
	{
		[Constructable]
		public HolyDraughtOfHumility() : this( 1 )
		{
		}


		[Constructable]
		public HolyDraughtOfHumility( int amount ) : base( 0x2827 )
		{
			Weight = 0.01;
			Stackable = true;
			Amount = amount;
			Hue = 0xB9E;
			Name = "Wonderous Miraculous Draught of Eternal and Holy Devotion to Humility";
		}


		public override void OnDoubleClick( Mobile from )
		{
			if ( !(from is PlayerMobile))
				return;
			
			if (from.SkillsTotal > 10000)
				from.SendMessage( "You are past the point of humility and the potion does nothing." );
			
			else
			{
				from.SkillsCap = 10000;
				from.SendMessage( "The potion cleanses your sins and prepares you for a life of humility." );
				from.SendMessage( "You are now a disciple of the path of the moon." );
				this.Delete();
			}
		}


		public HolyDraughtOfHumility( Serial serial ) : base( serial )
		{
		}


		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( ( int) 0 ); // version
		}


		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}
