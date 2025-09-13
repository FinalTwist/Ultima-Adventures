using System;
using Server;
using Server.Items;

namespace Solaris.BoardGames
{
	//a bomberman obstacle defines the object types
	public class DetonatorUpgrade : BombermanUpgrade
	{
		public DetonatorUpgrade() : base( 0xFC1, "Detonator" )
		{
			Hue = 1161;
		}
		
		//deserialize constructor
		public DetonatorUpgrade( Serial serial ) : base( serial )
		{
		}
		
		protected override void Upgrade( Mobile m )
		{
			base.Upgrade( m );
			
			
			
			BombBag bag = (BombBag)m.Backpack.FindItemByType( typeof( BombBag ) );
			
			if( bag != null && bag.Detonator == null )
			{
				BombDetonator detonator = new BombDetonator( bag );
				bag.Detonator = detonator;
				m.Backpack.DropItem( detonator );
			}
		}
		
		
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( 0 );
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();
		}
	}
}