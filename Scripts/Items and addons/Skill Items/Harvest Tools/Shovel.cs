using System;
using Server;
using Server.Engines.Harvest;

namespace Server.Items
{
	public class Shovel : BaseHarvestTool
	{
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
		public Shovel() : this( 50 )
		{
		}

		[Constructable]
		public Shovel( int uses ) : base( uses, 0xF39 )
		{
			Weight = 5.0;
		}
		
		public override void AddNameProperties( ObjectPropertyList list )
		{
			
			base.AddNameProperties( list );	
			
			if (!(this is Monocle))
				list.Add( "Say 'I wish to start mining' near cave/mountain to mine automatically." ); 
		}

		public Shovel( Serial serial ) : base( serial )
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
