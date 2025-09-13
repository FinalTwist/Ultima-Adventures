using System;
using Server;
using Server.Items;

namespace Solaris.BoardGames
{
	//a bomberman obstacle defines the object types
	public class BombCountUpgrade : BombermanUpgrade
	{
		public BombCountUpgrade() : base( 0x284F, "Bomb Count Upgrade" )
		{
			Hue = 1;
		}
		
		//deserialize constructor
		public BombCountUpgrade( Serial serial ) : base( serial )
		{
		}
		
		protected override void Upgrade( Mobile m )
		{
			base.Upgrade( m );
			
			BombBag bag = (BombBag)m.Backpack.FindItemByType( typeof( BombBag ) );
			
			if( bag != null )
			{
				bag.MaxBombs += 1;
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