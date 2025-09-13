using System;
using Server;


namespace Server.Items
{
	public class DjinnisRing : SilverRing
	{
		public override int InitMinHits{ get{ return 150; } }
		public override int InitMaxHits{ get{ return 150; } }


		[Constructable]
		public DjinnisRing()
		{
			Name = "Djinni's Ring";
			Attributes.BonusInt = 25;
			Attributes.SpellDamage = 35;
			Attributes.CastSpeed = 2;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public DjinnisRing( Serial serial ) : base( serial )
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
