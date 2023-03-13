
using System;
using Server;
using Server.Mobiles;

namespace Server.Items
{
	public class StatGlobeAgile : Item
	{
		


		[Constructable]
		public StatGlobeAgile() : base(0xE2E) 
		{
			Weight = 1.0;
			Name = "Double click to set Str/Dex/Int to 50/125/50";
			
			LootType = LootType.Blessed; 
		}

		public override void OnDoubleClick( Mobile from )
		{
			
			if ( !(from is PlayerMobile) )
				return;
			
			PlayerMobile pm = (PlayerMobile)from;

			pm.Str = 50;
			pm.Dex = 125;
			pm.Int = 50;

			pm.SendMessage("Dex now set to 125!");
		}

		public StatGlobeAgile( Serial serial ) : base( serial )
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