using System;
using Server;


namespace Server.Items
{
	public class TitansHammer : WarHammer
	{
		public override int InitMinHits{ get{ return 80; } }
		public override int InitMaxHits{ get{ return 160; } }


		public override int LabelNumber{ get{ return 1060024; } } // Titan's Hammer


		[Constructable]
		public TitansHammer()
		{
			Hue = 0x482;
			WeaponAttributes.HitEnergyArea = 100;
			Attributes.BonusStr = 15;
			Attributes.AttackChance = 15;
			Attributes.WeaponDamage = 50;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public TitansHammer( Serial serial ) : base( serial )
		{
		}


		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );


			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );


			int version = reader.ReadInt();
		}
	}
}