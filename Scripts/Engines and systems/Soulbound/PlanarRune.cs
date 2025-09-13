using System;
using Server;

namespace Server.Items
{
	public class PlanarRune : PhylacteryComponent
	{	
		public override int LabelNumber{ get{ return 1061232; } } // planar rune

		[Constructable]
		public PlanarRune() : this( 1 )
		{
		}

		[Constructable]
		public PlanarRune( int amount ) 
		{
			BoundEssence = "PlanarEssence";
			ComponentType = ComponentType.Regular;
			
			Stackable = true;
			Hue = 87;
			Amount = amount;
			ItemID = 0x1F14;
			Light = LightType.Circle150;
		}

		public PlanarRune( Serial serial ) : base( serial )
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
			Light = LightType.Circle150;
		}
		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			list.Add( 1061200, "{0}\t{1}","caster", "focus qualities" ); //This matter hums with ~1_val~ ~2_val~
		}
	}
}