using System;
using Server;


namespace Server.Items
{
	public class PowerSurge : MagicLantern
	{
		[Constructable]
		public PowerSurge()
		{
            Name = "Lantern of Power";
            Hue = Utility.RandomList( 1158, 1159, 1163, 1168, 1170, 16 );
            Attributes.AttackChance = 20;
			Attributes.BonusStr = 20;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        } 


		public PowerSurge( Serial serial ) : base( serial )
		{
		}


		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}


		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}