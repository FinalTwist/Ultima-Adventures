using System;

namespace Server.Items
{

[FlipableAttribute( 0x2FBA, 0x3174 )]
	public class SantasElvenRobe : BaseOuterTorso
	{

		[Constructable]
		public SantasElvenRobe() : base( 0x2FBA )
		{
			Name = "Santa's Elven Robe";
			Weight = 3.0;
		}
		public override void GetProperties( ObjectPropertyList list )
	         {
	  	    base.GetProperties( list );

		    list.Add( 1041052 ); 
    	     }

		public SantasElvenRobe( Serial serial ) : base( serial )
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

