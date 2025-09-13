
using System;
using Server;
using Server.Mobiles;

namespace Server.Items
{
	public class StatGlobeTank : Item
	{
		


		[Constructable]
		public StatGlobeTank() : base(0xE2E) 
		{
			Weight = 1.0;
			Name = "Double click to set Str/Dex/Int to 125/50/50";
			



			LootType = LootType.Blessed; 
		}

		public override void OnDoubleClick( Mobile from )
		{
			
			if ( !(from is PlayerMobile) )
				return;
			
			PlayerMobile pm = (PlayerMobile)from;

			pm.Str = 125;
			pm.Dex = 50;
			pm.Int = 50;

			pm.SendMessage("Str now set to 125!");
		}

		public StatGlobeTank( Serial serial ) : base( serial )
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