using System;

namespace Server.Items
{
	[FlipableAttribute( 0x170d, 0x170e )]
	public class MistressProtection : BaseShoes
	{
		public override int ArtifactRarity{ get{ return 58; } }

		[Constructable]
		public MistressProtection() : this( 0 )
		{
		}

		[Constructable]
		public MistressProtection( int hue ) : base( 0x170D, hue )
		{
			Weight = 1.0;
			Name = "Mistress Protection";
			Hue = 1950;

			//Attributes.WeaponDamage = 20;
			Attributes.BonusDex = 10;
			Attributes.LowerManaCost = 8;
			Attributes.LowerRegCost = 20;
			Attributes.Luck = 250;
			Attributes.NightSight = 1;
			Attributes.ReflectPhysical = 25;
		}

		public MistressProtection( Serial serial ) : base( serial )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			from.SendLocalizedMessage( sender.FailMessage );
			return false;
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