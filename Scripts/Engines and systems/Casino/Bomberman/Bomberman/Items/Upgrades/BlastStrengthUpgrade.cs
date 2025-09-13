using System;
using Server;
using Server.Items;

namespace Solaris.BoardGames
{
	//a bomberman obstacle defines the object types
	public class BlastStrengthUpgrade : BombermanUpgrade
	{
		public BlastStrengthUpgrade() : base( 0x283B, "Blast Strength Upgrade" )
		{
			Hue = 1161;
		}
		
		//deserialize constructor
		public BlastStrengthUpgrade( Serial serial ) : base( serial )
		{
		}
		
		protected override void Upgrade( Mobile m )
		{
			base.Upgrade( m );
			
			BombBag bag = (BombBag)m.Backpack.FindItemByType( typeof( BombBag ) );
			
			if( bag != null )
			{
				bag.BombStrength += 1;
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