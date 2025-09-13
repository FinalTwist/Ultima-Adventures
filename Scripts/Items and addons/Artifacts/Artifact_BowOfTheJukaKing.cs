using System;
using Server;


namespace Server.Items
{
	public class BowOfTheJukaKing : Bow
	{
		public override int InitMinHits{ get{ return 80; } }
		public override int InitMaxHits{ get{ return 160; } }


		public override int LabelNumber{ get{ return 1070636; } }


		[Constructable]
		public BowOfTheJukaKing()
		{
			Hue = 0x460;
			WeaponAttributes.HitMagicArrow = 25;
			Slayer = SlayerName.ReptilianDeath;
			Attributes.AttackChance = 15;
			Attributes.WeaponDamage = 40;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public BowOfTheJukaKing( Serial serial ) : base( serial )
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