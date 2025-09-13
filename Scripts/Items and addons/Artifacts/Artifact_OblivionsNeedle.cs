using System;
using Server;


namespace Server.Items
{
	public class OblivionsNeedle : Dagger
	{
		public override int InitMinHits{ get{ return 150; } }
		public override int InitMaxHits{ get{ return 150; } }


		public override bool CanFortify{ get{ return true; } }


		[Constructable]
		public OblivionsNeedle()
		{
			Name = "Oblivion Needle";
			Attributes.BonusStam = 20;
			Attributes.AttackChance = 20;
			Attributes.DefendChance = -20;
			Attributes.WeaponDamage = 40;


			WeaponAttributes.HitLeechStam = 50;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public OblivionsNeedle( Serial serial ) : base( serial )
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
