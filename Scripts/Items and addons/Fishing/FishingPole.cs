using System;
using System.Collections;
using Server.Targeting;
using Server.Items;
using Server.Engines.Harvest;
using System.Collections.Generic;
using Server.ContextMenus;

namespace Server.Items
{
	public class FishingPole : BaseHarvestTool
	{
		public override HarvestSystem HarvestSystem{ get{ return Fishing.System; } }

		[Constructable]
		public FishingPole() : this( 50 )
		{
		}

		[Constructable]
		public FishingPole( int uses ) : base( uses, 0x0DC0 )
		{
			Layer = Layer.OneHanded;
			Weight = 8.0;
		}

		public FishingPole( Serial serial ) : base( serial )
		{
		}

		public override void AddNameProperties( ObjectPropertyList list )
		{
			
			base.AddNameProperties( list );	
			
			list.Add( "Say 'I wish to start fishing' near water to fish automatically." ); 
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