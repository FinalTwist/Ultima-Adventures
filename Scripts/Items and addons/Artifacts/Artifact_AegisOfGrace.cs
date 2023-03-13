using System;
using Server.Network;
using Server.Items;


namespace Server.Items
{
	public class AegisOfGrace : DragonHelm
	{
		public override int InitMinHits{ get{ return 80; } }
		public override int InitMaxHits{ get{ return 160; } }


		public override int LabelNumber{ get{ return 1075047; } } // Aegis of Grace


		public override int BasePhysicalResistance{ get{ return 10; } }
		public override int BaseFireResistance{ get{ return 9; } }
		public override int BaseColdResistance{ get{ return 7; } }
		public override int BasePoisonResistance{ get{ return 7; } }
		public override int BaseEnergyResistance{ get{ return 15; } }


		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Dragon; } }
		public override CraftResource DefaultResource{ get{ return CraftResource.Iron; } }


		[Constructable]
		public AegisOfGrace()
		{
			SkillBonuses.SetValues( 0, SkillName.MagicResist, 10.0 );
			Attributes.DefendChance = 20;
			ArmorAttributes.SelfRepair = 2;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public AegisOfGrace( Serial serial ) : base( serial )
		{
		}


		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( 0 ); // version
		}


		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
}