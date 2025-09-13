using System;
using Server;


namespace Server.Items
{
	public class BladeOfTheRighteous : Longsword
	{
		public override int InitMinHits{ get{ return 80; } }
		public override int InitMaxHits{ get{ return 160; } }


		public override int LabelNumber{ get{ return 1061107; } } // Blade of the Righteous


		[Constructable]
		public BladeOfTheRighteous()
		{
			Hue = 0x47E;
			//Slayer = SlayerName.DaemonDismissal;
			Slayer = SlayerName.Exorcism;
			WeaponAttributes.HitLeechHits = 50;
			WeaponAttributes.UseBestSkill = 1;
			Attributes.BonusHits = 10;
			Attributes.WeaponDamage = 50;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public BladeOfTheRighteous( Serial serial ) : base( serial )
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


			if ( Slayer == SlayerName.None )
				Slayer = SlayerName.Exorcism;
		}
	}
}