using System;
using Server;
using Server.Guilds;

namespace Server.Items
{
	public class SignOfOrder: GargishOrderShield
	{
		public override int BasePhysicalResistance{ get{ return 1; } }
		public override int BaseFireResistance{ get{ return 0; } }
		public override int BaseColdResistance{ get{ return 5; } }
		public override int BasePoisonResistance{ get{ return 5; } }
		public override int BaseEnergyResistance{ get{ return 0; } }

		public override int InitMinHits{ get{ return Utility.RandomMinMax(100, 125); } }
		public override int InitMaxHits{ get{ return Utility.RandomMinMax(126, 150); } }

		[Constructable]
		public SignOfOrder() : base( )
		{
			SkillBonuses.SetValues( 0, SkillName.Chivalry, 10.0 );
			Attributes.AttackChance = 20;
			Attributes.DefendChance = 15;
			Attributes.CastSpeed = 1;
		}

		public SignOfOrder( Serial serial ) : base(serial)
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
	}
}