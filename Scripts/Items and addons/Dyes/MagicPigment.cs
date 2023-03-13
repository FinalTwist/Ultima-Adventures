using Server;
using System;
using System.Collections;
using Server.Network;
using Server.Targeting;
using Server.Prompts;
using Server.Misc;

namespace Server.Items
{
	public class MagicPigment : Item
	{
		[Constructable]
		public MagicPigment() : base( 0x4C5A )
		{
			string OwnerName = RandomThings.GetRandomName();
			if ( OwnerName.EndsWith( "s" ) )
			{
				OwnerName = OwnerName + "'";
			}
			else
			{
				OwnerName = OwnerName + "'s";
			}

			string ItemName = LootPackEntry.MagicItemAdj( "start", false, false, ItemID );

			Weight = 2.0;
			Name = OwnerName + " " + ItemName + " paints";
		}

		public override void AddNameProperties( ObjectPropertyList list )
		{
			base.AddNameProperties( list );
			list.Add( 1070722, "Paint Almost Anything" );
            list.Add( 1049644, "Needs Color Added By Dyeing It");
		}

		public override void OnDoubleClick( Mobile from )
		{
			Target t;

			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1060640 ); // The item must be in your backpack to use it.
			}
			else
			{
				from.SendMessage( "What do you want to paint?" );
				t = new DyeTarget( this );
				from.Target = t;
			}
		}

		private class DyeTarget : Target
		{
			private MagicPigment m_Dye;

			public DyeTarget( MagicPigment tube ) : base( 1, false, TargetFlags.None )
			{
				m_Dye = tube;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( targeted is Item )
				{
					Item iDye = targeted as Item;

					if ( !iDye.IsChildOf( from.Backpack ) )
					{
						from.SendMessage( "You can only paint things in your pack." );
					}
					else if ( ( iDye.Stackable == true ) || ( iDye.ItemID == 8702 ) || ( iDye.ItemID == 4011 ) )
					{
						from.SendMessage( "You cannot paint that." );
					}
					else if ( iDye.IsChildOf( from.Backpack ) )
					{
						iDye.Hue = m_Dye.Hue;
							if ( iDye.Hue == 0x2EF ){ iDye.Hue = 0; }
						from.RevealingAction();
						from.PlaySound( 0x23F );
					}
					else
					{
						from.SendMessage( "You cannot paint that with this." );
					}
				}
				else
				{
					from.SendMessage( "You cannot paint that with this." );
				}
			}
		}

		public MagicPigment( Serial serial ) : base( serial )
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