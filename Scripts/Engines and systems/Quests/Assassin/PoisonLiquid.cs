using System;
using System.Collections;
using Server.Network;
using Server.Targeting;
using Server.Prompts;
using Server.Mobiles;
using Server.Regions;

namespace Server.Items
{
	public class PoisonLiquid : Item
	{
		public Mobile m_Poisoner;
		public int m_Poison;

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Poisoner { get { return m_Poisoner; } set { m_Poisoner = value; } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int Poison { get { return m_Poison; } set { m_Poison = value; } }

		public override bool OnDragLift(Mobile from)
		{
			if ( from is PlayerMobile ){ m_Poisoner = from; }
			return true;
		}

		public override bool OnDroppedToMobile( Mobile from, Mobile target )
		{
			if ( target is BaseVendor && target.Region.IsPartOf( typeof( VillageRegion ) ) )
			{
				if ( m_Poison == 1 ) { target.ApplyPoison( from, PoisonImpl.Regular ); }
				else if ( m_Poison == 2 ) { target.ApplyPoison( from, PoisonImpl.Greater ); }
				else if ( m_Poison == 3 ) { target.ApplyPoison( from, PoisonImpl.Deadly ); }
				else if ( m_Poison == 4 ) { target.ApplyPoison( from, PoisonImpl.Lethal ); }
				else { target.ApplyPoison( from, PoisonImpl.Lethal ); }

				target.Say( "Poison!");

				target.PlaySound( target.Female ? 813 : 1087 );
				if ( !target.Mounted ) 
					target.Animate( 32, 5, 1, true, false, 0 );                     
				Puke puke = new Puke(); 
				puke.Map = target.Map; 
				puke.Location = target.Location;

				this.Delete();
			}
			else if ( target.Body == 0x191 || target.Body == 0x190 || target.Body == 606 || target.Body == 605 )
			{
				from.AddToBackpack ( this );
				target.Say( "That doesn't look good.");
			}
			else
			{
				from.AddToBackpack ( this );
				from.PrivateOverheadMessage(MessageType.Regular, 1150, false, "They don't seem to want that.", from.NetState);
			}

			return true;
		}

		[Constructable]
		public PoisonLiquid() : base( 0xE26 )
		{
			Hue = 0xB97;
			Name = "bottle of tainted liquid";
			Weight = 1.0;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !IsChildOf( from.Backpack ) ) 
			{
				from.SendMessage( "This must be in your backpack to use." );
				return;
			}
			else
			{
				from.PlaySound( 0x2D6 );
				from.AddToBackpack( new Bottle() );

				if ( from.Body.IsHuman && !from.Mounted )
					from.Animate( 34, 5, 1, true, false, 0 );

				this.Consume();

				from.Say( "Poison!" );
				if ( m_Poison == 5 ) { from.ApplyPoison( from, PoisonImpl.Lethal ); }
				else if ( m_Poison == 4 ) { from.ApplyPoison( from, PoisonImpl.Deadly ); }
				else if ( m_Poison == 3 ) { from.ApplyPoison( from, PoisonImpl.Greater ); }
				else if ( m_Poison == 2 ) { from.ApplyPoison( from, PoisonImpl.Regular ); }
				else { from.ApplyPoison( from, PoisonImpl.Lesser ); }
				from.SendMessage( "Poison!");
			}
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
			string sPoison = "Slightly Poisoned";

			if ( m_Poison == 5 ) { sPoison = "Lethally Poisoned"; }
			else if ( m_Poison == 4 ) { sPoison = "Deathly Poisoned"; }
			else if ( m_Poison == 3 ) { sPoison = "Greatly Poisoned"; }
			else if ( m_Poison == 2 ) { sPoison = "Poisoned"; }

            base.AddNameProperties(list);
			list.Add( 1049644, sPoison); // PARENTHESIS
        }

		public PoisonLiquid( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			writer.Write( m_Poisoner );
			writer.Write( m_Poison );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			m_Poisoner = reader.ReadMobile();
			m_Poison = reader.ReadInt();
		}
	}
}