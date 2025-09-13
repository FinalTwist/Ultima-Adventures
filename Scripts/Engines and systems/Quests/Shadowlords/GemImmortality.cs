using System;
using Server;
using Server.Mobiles;
using Server.Items;
using Server.Misc;

namespace Server.Items
{
	public enum ImortalEffect
	{
		Charges
	}

    public class GemImmortality : Item
	{
		private int m_Charges;

		[CommandProperty( AccessLevel.GameMaster )]
		public int Charges
		{
			get{ return m_Charges; }
			set{ m_Charges = value; InvalidateProperties(); }
		}

        [Constructable]
        public GemImmortality() : base( 0x4078 )
		{
            Name = "gem of immortality";
			Charges = 15;
			Weight = 1.0;
			LootType = LootType.Blessed;
		}

		public override bool DisplayLootType{ get{ return false; } }

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			list.Add( 1060741, m_Charges.ToString() );
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Imbued with Magic");
            list.Add( 1049644, "Magically Avoid Death");
        }

		public static void ConsumeCharge( Mobile from, GemImmortality gem )
		{
			--gem.Charges;

			gem.InvalidateProperties();

			if ( gem.Charges == 0 )
			{
				from.SendLocalizedMessage( 1019073 ); // This item is out of charges.
				Item DudGem = new DudImmortality();
			  	from.AddToBackpack ( DudGem );
				gem.Delete();
			}
		}

        public GemImmortality( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			writer.Write( (int) m_Charges );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			switch ( version )
			{
				case 0:
				{
					m_Charges = (int)reader.ReadInt();

					break;
				}
			}
	    }
    }
}

namespace Server.Items
{
	public class DudImmortality : Item
	{
		[Constructable]
		public DudImmortality() : base( 0x4079 )
		{
			Name = "gem of immortality";
			Weight = 1.0;
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Devoid of Magic");
        }

		public DudImmortality( Serial serial ) : base( serial )
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

namespace Server.Misc
{
    class SeeIfGemInBag
    {
		public static bool IHaveAGem( Mobile m )
		{
			if ( m.Backpack.FindItemByType( typeof ( GemImmortality ) ) != null && m is PlayerMobile && m != null )
			{
				Item rock = m.Backpack.FindItemByType( typeof ( GemImmortality ) );

				GemImmortality gem = (GemImmortality)rock;

				Server.Items.GemImmortality.ConsumeCharge( m, gem );

				m.Hits = m.HitsMax;
				m.Mana = m.ManaMax;
				m.Stam = m.StamMax;
				m.FixedParticles( 0x376A, 9, 32, 5030, EffectLayer.Waist );
				m.SendMessage( "You are restored with the power of the gem!" );
				m.CurePoison( m );
				m.PlaySound( 0x202 );

				return true;
			}

			return false;
		}

		public static bool GemInPocket( Mobile m )
		{
			if ( m.Backpack.FindItemByType( typeof ( GemImmortality ) ) != null && m is PlayerMobile && m != null )
			{
				return true;
			}

			return false;
		}
	}
}