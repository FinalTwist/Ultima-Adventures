using System;
using Server;

namespace Server.Items
{
	public class Venom : GoldBracelet
	{

		[Constructable]
		public Venom()
		{
		
			Name = ("Venom");
		
			Hue = 1371;
			Attributes.CastRecovery = 1;
			Attributes.CastSpeed = 2;
			Attributes.SpellDamage = 10;
			Resistances.Poison = 20;
		}
		
		public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }

		public Venom( Serial serial ) : base( serial )
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