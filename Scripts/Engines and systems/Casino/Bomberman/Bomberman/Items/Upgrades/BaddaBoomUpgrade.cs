using System;
using Server;
using Server.Items;

namespace Solaris.BoardGames
{
	//this causes unstoppable explosions
	public class BaddaBoomUpgrade : BombermanUpgrade
	{
		public BaddaBoomUpgrade() : base( 0x1858, "Big BaddaBoom Upgrade" )
		{
			Hue = 1161;
		}
		
		//deserialize constructor
		public BaddaBoomUpgrade( Serial serial ) : base( serial )
		{
		}
		
		protected override void Upgrade( Mobile m )
		{
			base.Upgrade( m );
			
			
			
			BombBag bag = (BombBag)m.Backpack.FindItemByType( typeof( BombBag ) );
			
			if( bag != null )
			{
				bag.BaddaBoom = true;
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