using System;
using Server;

namespace Server.Items
{
	public class SpellWeavingBandana : Bandana
	{
		public override int LabelNumber{ get{ return 1063473; } }

		public override int BasePhysicalResistance{ get{ return 10; } }
		public override int BaseFireResistance{ get{ return 10; } }
		public override int BaseColdResistance{ get{ return 10; } }
		public override int BasePoisonResistance{ get{ return 10; } }
		public override int BaseEnergyResistance{ get{ return 10; } }

		public override int InitMinHits{ get{ return Utility.RandomMinMax(100, 125); } }
		public override int InitMaxHits{ get{ return Utility.RandomMinMax(126, 150); } }

		[Constructable]
		public SpellWeavingBandana()
		{
			Hue = Utility.RandomBool() ? 0x58C : 0x10;
                        Name = "Head Wrap of the Arcane"; 
			SkillBonuses.SetValues( 0, SkillName.Spellweaving, 20.0 );
			SkillBonuses.SetValues( 1, SkillName.Magery, 20.0 );
			Attributes.BonusInt = 15;
		}

		public SpellWeavingBandana( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 2 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( version < 2 )
			{
				Resistances.Physical = 0;
				Resistances.Fire = 0;
				Resistances.Cold = 0;
				Resistances.Poison = 0;
				Resistances.Energy = 0;
			}
		}
	}
}