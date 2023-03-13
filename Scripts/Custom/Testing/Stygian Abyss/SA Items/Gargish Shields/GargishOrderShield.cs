using System;
using Server;
using Server.Guilds;

namespace Server.Items
{
	// Based off an OrderShield
	[FlipableAttribute( 0x422A, 0x422C )]
	public class GargishOrderShield : BaseShield
	{
		public override int BasePhysicalResistance{ get{ return 1; } }
		public override int BaseFireResistance{ get{ return 0; } }
		public override int BaseColdResistance{ get{ return 0; } }
		public override int BasePoisonResistance{ get{ return 0; } }
		public override int BaseEnergyResistance{ get{ return 0; } }

		public override int InitMinHits{ get{ return 100; } }
		public override int InitMaxHits{ get{ return 125; } }

		public override int AosStrReq{ get{ return 95; } }

		public override int ArmorBase{ get{ return 30; } }

		[Constructable]
		public GargishOrderShield() : base( 0x422A )
		{
			if ( !Core.AOS )
				LootType = LootType.Newbied;

			//Weight = 7.0;
		}

		public GargishOrderShield( Serial serial ) : base(serial)
		{
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)0 ); //version
		}

		public override bool OnEquip( Mobile from )
		{
			return Validate( from ) && base.OnEquip( from );
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( Validate( Parent as Mobile ) )
				base.OnSingleClick( from );
		}

		public virtual bool Validate( Mobile m )
		{
			if ( Core.AOS || m == null || !m.Player || m.AccessLevel != AccessLevel.Player )
				return true;

			Guild g = m.Guild as Guild;

			if ( g == null || g.Type != GuildType.Order )
			{
				m.FixedEffect( 0x3728, 10, 13 );
				Delete();

				return false;
			}

			return true;
		}
	}
}
