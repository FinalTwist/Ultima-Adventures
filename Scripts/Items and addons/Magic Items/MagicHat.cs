using System;
using Server;
using Server.Misc;
using Server.Targeting;

namespace Server.Items
{
	public class MagicHat : GoldRing
	{
		[Constructable]
		public MagicHat()
		{
			Weight = 1.0;
			Name = "hat";

			Resource = CraftResource.None;

			int myHat = Utility.RandomMinMax( 0, 23 );
				if ( Utility.RandomMinMax( 1, 20 ) == 1 ){ myHat = Utility.RandomMinMax( 0, 31 ); }

			switch ( myHat ) 
			{
				case 0: ItemID = 5914; Name = "feathered hat"; break;
				case 1: ItemID = 5916; Name = "jester hat"; break;
				case 2: ItemID = 5911; Name = "straw hat"; break;
				case 3: ItemID = 5910; Name = "tall straw hat"; break;
				case 4: ItemID = 5908; Name = "wide brim hat"; break;
				case 5: ItemID = 5912; Name = "wizard hat"; break;
				case 6: ItemID = 5915; Name = "tricorne hat"; break;
				case 7: ItemID = 5907; Name = "floppy hat"; break;
				case 8: ItemID = 5444; Name = "skullcap"; break;
				case 9: ItemID = 5909; Name = "bonnet"; break;
				case 10: ItemID = 0x278F; Name = "executioners hood"; break;
				case 11: ItemID = 0x1540; Name = "bandana"; break;
				case 12: ItemID = 0x1549; Name = "shaman mask"; break;
				case 13: ItemID = 0x154B; Name = "tribal mask"; break;
				case 14: ItemID = 0x2B71; Name = "hood"; break;
				case 15: ItemID = 0x3176; Name = "cowl"; break;
				case 16: ItemID = 0x2FC3; Name = "witch hat"; break;
				case 17: ItemID = 0x2FBC; Name = "pirate hat"; break;
				case 18: ItemID = 5445; Name = "bearskin cap"; break;
				case 19: ItemID = 5447; Name = "dearskin cap"; break;
				case 20: ItemID = 0x2B6D; Name = "wolfskin cap"; break;
				case 21: ItemID = 0x49C3; Name = "stag mask"; break;
				case 22: ItemID = 0x4C15; Name = "fool's hat"; break;
				case 23: ItemID = 0x4D09; Name = "fancy hood"; break;
				case 24: ItemID = 0x4CDB; Name = "reaper hood"; break;
				case 25: ItemID = 0x4CDD; Name = "reaper cowl"; break;
				case 26: ItemID = 0x4D01; Name = "daemon cowl"; break;
				case 27: ItemID = 0x4D02; Name = "lizard cowl"; break;
				case 28: ItemID = 0x4D03; Name = "evil hood"; break;
				case 29: ItemID = 0x4D04; Name = "evil cowl"; break;
				case 30: ItemID = Utility.RandomList( 0x141B, 0x141C ); Name = "orcish mask"; break;
				case 31: ItemID = Utility.RandomList( 0x2B72, 0x3169 ); Name = "gargoyle mask"; break;
			}

			if ( myHat < 18 ){ Hue = RandomThings.GetRandomColor(0); }
			else { Hue = Utility.RandomList( 0, 0x497, 0x47E, 0x4A6, 0x4A7, 0x4A8, 0x4A9 ); }

			if ( Utility.RandomBool() )
				Hue = RandomThings.GetRandomSpecialColor();

			Layer = Layer.Helm;

			//Name = LootPackEntry.MagicItemAdj( "start", false, false, ItemID ) + " " + Name;
		}

        public override void OnDoubleClick( Mobile from )
		{
			if ( this.ItemID == 0x2B71 || this.ItemID == 0x3176 || this.ItemID == 0x3168 || this.ItemID == 0x3177 || this.ItemID == 0x4D09 )
			{
				if ( from.FindItemOnLayer( Layer.Helm ) != this )
				{
					from.SendMessage( "You must be wearing this to change the color." );
				}
				else
				{
					Target t;

					from.SendMessage( "What worn item do you wish to match the color of?" );
					t = new HoodTarget( this );
					from.Target = t;
				}
			}
        }

		private class HoodTarget : Target
		{
			private MagicHat m_Hood;

			public HoodTarget( MagicHat cowl ) : base( 1, false, TargetFlags.None )
			{
				m_Hood = cowl;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( targeted is Item )
				{
					Item iHood = targeted as Item;

					int color = 0;

					if ( from.FindItemOnLayer( Layer.Helm ) != m_Hood ) { from.SendMessage( "You must be wearing this to change the color." ); }
					else if ( from.FindItemOnLayer( Layer.Waist ) == iHood ) { color = iHood.Hue; }
					else if ( from.FindItemOnLayer( Layer.OuterTorso ) == iHood ) { color = iHood.Hue; }
					else if ( from.FindItemOnLayer( Layer.Arms ) == iHood ) { color = iHood.Hue; }
					else if ( from.FindItemOnLayer( Layer.OuterLegs ) == iHood ) { color = iHood.Hue; }
					else if ( from.FindItemOnLayer( Layer.Neck ) == iHood ) { color = iHood.Hue; }
					else if ( from.FindItemOnLayer( Layer.Gloves ) == iHood ) { color = iHood.Hue; }
					else if ( from.FindItemOnLayer( Layer.Shoes ) == iHood ) { color = iHood.Hue; }
					else if ( from.FindItemOnLayer( Layer.Cloak ) == iHood ) { color = iHood.Hue; }
					else if ( from.FindItemOnLayer( Layer.FirstValid ) == iHood ) { color = iHood.Hue; }
					else if ( from.FindItemOnLayer( Layer.InnerLegs ) == iHood ) { color = iHood.Hue; }
					else if ( from.FindItemOnLayer( Layer.InnerTorso ) == iHood ) { color = iHood.Hue; }
					else if ( from.FindItemOnLayer( Layer.Pants ) == iHood ) { color = iHood.Hue; }
					else if ( from.FindItemOnLayer( Layer.Shirt ) == iHood ) { color = iHood.Hue; }
					else
					{
						from.SendMessage( "You can only match colors of certain equipped items." );
					}

					if ( color > 0 )
					{
						m_Hood.Hue = color;
						from.SendMessage( "You change the color to match the item." );
					}
					else
					{
						from.SendMessage( "Items selected must have a distinct color." );
					}
				}
				else
				{
					from.SendMessage( "You can only match color of certain equipped items that have distinct colors." );
				}
			}
		}

		public MagicHat( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}