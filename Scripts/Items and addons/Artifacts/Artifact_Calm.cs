using System;
using Server;


namespace Server.Items
{
	public class Calm : Halberd
	{
		public override int InitMinHits{ get{ return 80; } }
		public override int InitMaxHits{ get{ return 160; } }


		public override bool CanFortify{ get{ return true; } }


		[Constructable]
		public Calm()
		{
			Name = "Calm";
			Hue = 0x2cb;


			Attributes.SpellChanneling = 1;
			Attributes.WeaponSpeed = 20;
			Attributes.WeaponDamage = 50;


			WeaponAttributes.HitLeechMana = 100;
			WeaponAttributes.UseBestSkill = 1;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public Calm( Serial serial ) : base( serial )
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
