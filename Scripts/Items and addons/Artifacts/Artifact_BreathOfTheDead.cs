using System;
using Server;


namespace Server.Items
{
	public class BreathOfTheDead : BoneHarvester
	{
		public override int InitMinHits{ get{ return 80; } }
		public override int InitMaxHits{ get{ return 160; } }


		public override int LabelNumber{ get{ return 1061109; } } // Breath of the Dead


		[Constructable]
		public BreathOfTheDead()
		{
			Hue = 0x455;
			WeaponAttributes.HitLeechHits = 100;
			WeaponAttributes.HitHarm = 25;
			Attributes.SpellDamage = 5;
			Attributes.WeaponDamage = 50;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public BreathOfTheDead( Serial serial ) : base( serial )
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