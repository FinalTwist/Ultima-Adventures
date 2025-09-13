using System;
using Server;

namespace Server.Items
{
	public class CelestialRune : PlanarRune
	{
		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		public override int LabelNumber{ get{ return 1061196; } } // celestial rune

		[Constructable]
		public CelestialRune() : this( 1 )
		{
		}

		[Constructable]
		public CelestialRune( int amount ) : base(amount)
		{
			BoundEssence = "CelstialEssence";
			Hue = 1995;
			Amount = amount;
		}

		public CelestialRune( Serial serial ) : base( serial )
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
			list.Add( 1061200, "{0}\t{1}","caster", "speed qualities" ); //This matter hums with ~1_val~ ~2_val~
		}
	}
}