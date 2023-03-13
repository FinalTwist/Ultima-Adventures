using System;
using Server;


namespace Server.Items
{
	public class SamaritanRobe : MagicRobe
	{
		[Constructable]
		public SamaritanRobe()
		{
			Name = "Good Samaritan Robe";
			Hue = 0x2a3;
			Attributes.Luck = 500;
			//Resistances.Physical = 10;
			//SkillBonuses.SetValues(0, SkillName.Chivalry, 20); //moved to eclispe
			//Attributes.ReflectPhysical = 10;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public SamaritanRobe( Serial serial ) : base( serial )
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
