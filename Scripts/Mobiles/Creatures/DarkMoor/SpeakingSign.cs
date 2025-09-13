using System;
using System.Collections;
using Server;
using Server.Mobiles;
using Server.Network;


/* 
Use < [add SpeakingSign "enter custom name"  > command to set name of item when creating it. 
This allows you to set name with spaces between words,

 */

namespace Server.Items
{
	[FlipableAttribute( 0x0BCF, 0x0BD0, 0x0BD1, 0x0BD2, 0x1F28, 0x1F29 , 0x1297, 0x1298, 0x1299, 0x129A, 0x129B, 0x129C, 0x129D, 0x129E )]
	
	public class SpeakingSign : Item
	{
		private DateTime LastUse;
		public virtual TimeSpan Delay{ get{ return TimeSpan.FromMinutes( 2.0 ); } }
		public string Message { get; set; }
	
	[Constructable]
		public SpeakingSign( string message ) : base( 0x0BCF )
		{
			Movable = false;
			Name = "a sign";
			Message = message;
		}
		
		public virtual void HeedWarning()
		{
			//PublicOverheadMessage( type, hue, number, "" );
			PublicOverheadMessage( MessageType.Regular, 0x3B2, false, String.Format( "{0}", Message) );
		}

		public override bool HandlesOnMovement { get { return true; } }

		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
				if ( m.AccessLevel > AccessLevel.Player && m.Hidden )
				return;
				
					if ( m is PlayerMobile && m.InRange( this, 10 ) )
					{
						if ( LastUse + Delay > DateTime.UtcNow  )
						{	
							return;
						}
						else
						{	
							HeedWarning();
							LastUse = DateTime.UtcNow;
						}
					}
				
		}

		public SpeakingSign( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); 
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}
