using System;
using Server;


namespace Server.Items
{
	public class LunaLance : Lance
	{
		public override int InitMinHits{ get{ return 80; } }
		public override int InitMaxHits{ get{ return 160; } }


		[Constructable]
		public LunaLance()
		{
			Name = "Holy Lance";
			Hue = 0x47E;
			SkillBonuses.SetValues( 0, SkillName.Chivalry, 10.0 );
			Attributes.BonusStr = 5;
			Attributes.WeaponSpeed = 20;
			Attributes.WeaponDamage = 35;
			WeaponAttributes.UseBestSkill = 1;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public LunaLance( Serial serial ) : base( serial )
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