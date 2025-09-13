using System;
using Server;
using Server.Engines.Harvest;

namespace Server.Items
{
	public class OreShovel : BaseHarvestTool
	{
		public override int Hue { get{ return 0x96D; } }
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
		public OreShovel() : this( 50 )
		{
		}

		[Constructable]
		public OreShovel( int uses ) : base( uses, 0xF3A )
		{
			Name = "ore spade";
			Weight = 5.0;
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			list.Add( "Digs Up Iron Ore Only" );
		}
		
		public override void AddNameProperties( ObjectPropertyList list )
		{
			
			base.AddNameProperties( list );	
			
			list.Add( "Say 'I wish to start mining' near cave/mountain to mine automatically." ); 
		}

		public OreShovel( Serial serial ) : base( serial )
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
