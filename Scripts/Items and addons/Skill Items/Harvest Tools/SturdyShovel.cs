using System;
using Server;
using Server.Engines.Harvest;

namespace Server.Items
{
	public class SturdyShovel : BaseHarvestTool
	{
		public override int LabelNumber{ get{ return 1045125; } } // sturdy shovel
		//public override HarvestSystem HarvestSystem{ get{ return Mining.System; } }

		public override HarvestSystem HarvestSystem
		{ get
			{
				if (this.Map == Map.Midland || this.Map == Map.Underground) 
					return DeepMine.DeepMining.GetSystem(this);
				else if (this.RootParentEntity is Mobile)
				{
					Mobile m = (Mobile)this.RootParentEntity;
					if (m.Map == Map.Midland || m.Map == Map.Underground) 
						return DeepMine.DeepMining.GetSystem(this);
				}	 
				return Mining.System;

			} 
		}

		[Constructable]
		public SturdyShovel() : this( 180 )
		{
		}

		[Constructable]
		public SturdyShovel( int uses ) : base( uses, 0xF39 )
		{
			Weight = 5.0;
			Hue = 0x973;
		}

		public SturdyShovel( Serial serial ) : base( serial )
		{
		}
		
		public override void AddNameProperties( ObjectPropertyList list )
		{
			
			base.AddNameProperties( list );	
			
			list.Add( "Say 'I wish to start mining' near cave/mountain to mine automatically." ); 
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
