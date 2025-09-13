using System;
using Server;

namespace Server.Items
{
	public class BloodPentagramAddon : BaseAddon
	{
		public override BaseAddonDeed Deed{ get{ return new BloodPentagramDeed(); } }

		[Constructable]
		public BloodPentagramAddon()
		{       
			AddComponent( new AddonComponent( 7409 ), 2, 3, 0 );	
			AddComponent( new AddonComponent( 7428 ), 2, 2, 0 );
			AddComponent( new AddonComponent( 7429 ), 1, 2, 0 );
			AddComponent( new AddonComponent( 7430 ), 0, 2, 0 );
			AddComponent( new AddonComponent( 7431 ), -1, 2, 0 );	
			AddComponent( new AddonComponent( 7412 ), -1, 3, 0 );		
			AddComponent( new AddonComponent( 7440 ), 0, 1, 0 );	
			AddComponent( new AddonComponent( 7441 ), 0, 0, 0 );	
			AddComponent( new AddonComponent( 7434 ), 0, -1, 0 );	
			AddComponent( new AddonComponent( 7426 ), 3, 1, 0 );	
			AddComponent( new AddonComponent( 7436 ), 2, -1, 0 );	
			AddComponent( new AddonComponent( 7422 ), 2, -2, 0 );			
			AddComponent( new AddonComponent( 7421 ), 1, -2, 0 );	  
			AddComponent( new AddonComponent( 7420 ), 0, -2, 0 );		 
			AddComponent( new AddonComponent( 7419 ), -1, -2, 0 );		
			AddComponent( new AddonComponent( 7418 ), -1, -1, 0 );		
			AddComponent( new AddonComponent( 7433 ), -1, 0, 0 );			
			AddComponent( new AddonComponent( 7432 ), -1, 1, 0 );			    
			AddComponent( new AddonComponent( 7442 ), 1, 0, 0 );			
			AddComponent( new AddonComponent( 7439 ), 1, 1, 0 );			
			AddComponent( new AddonComponent( 7425 ), 3, 0, 0 );			
			AddComponent( new AddonComponent( 7435 ), 1, -1, 0 );		
			AddComponent( new AddonComponent( 7417 ), -2, -1, 0 );		
			AddComponent( new AddonComponent( 1 ), -2, -2, 0 );			
			AddComponent( new AddonComponent( 7415 ), -2, 1, 0 );		
			AddComponent( new AddonComponent( 7416 ), -2, 0, 0 );		
			AddComponent( new AddonComponent( 7423 ), 3, -2, 0 );		
			AddComponent( new AddonComponent( 7424 ), 3, -1, 0 );	
			AddComponent( new AddonComponent( 7438 ), 2, 1, 0 );		
			AddComponent( new AddonComponent( 7437 ), 2, 0, 0 );		 
			AddComponent( new AddonComponent( 7427 ), 3, 2, 0 );			 
			AddComponent( new AddonComponent( 7411 ), 0, 3, 0 );			
			AddComponent( new AddonComponent( 7410 ), 1, 3, 0 );			    
			AddComponent( new AddonComponent( 7413 ), -2, 3, 0 );			
			AddComponent( new AddonComponent( 7414 ), -2, 2, 0 );
		}

		public BloodPentagramAddon( Serial serial ) : base( serial )
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

	public class BloodPentagramDeed : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return new BloodPentagramAddon(); } }
		public override int LabelNumber{ get{ return 1044328; } } // bloodpentagram

		[Constructable]
		public BloodPentagramDeed()
		{
			Name = "Blood Pentagram Deed";
			Hue = 0x96C;
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			list.Add( "Happy Halloween" );
		}

		public BloodPentagramDeed( Serial serial ) : base( serial )
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