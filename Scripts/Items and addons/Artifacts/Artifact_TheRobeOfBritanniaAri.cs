using System;
using Server;


namespace Server.Items
{
	public class TheRobeOfBritanniaAri : MagicRobe
	{
		[Constructable]
		public TheRobeOfBritanniaAri()
		{
			Name = "Robe of Sosaria";
			Hue = 0x48b;
			Resistances.Physical = 10;
			Resistances.Cold = 10;
			Resistances.Fire = 10;
			Resistances.Energy = 10;
			Resistances.Poison = 10;
			Attributes.RegenStam = 15;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public TheRobeOfBritanniaAri( Serial serial ) : base( serial )
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
