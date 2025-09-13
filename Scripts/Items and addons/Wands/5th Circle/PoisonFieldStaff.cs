using System;
using Server;
using Server.Spells.Fifth;
using Server.Targeting;

namespace Server.Items
{
	public class PoisonFieldMagicStaff : BaseMagicStaff
	{
		[Constructable]
		public PoisonFieldMagicStaff() : base( MagicStaffEffect.Charges, 1, 9 )
		{
			IntRequirement = 30;			Name = "wand of poisonous fields";
			SkillBonuses.SetValues( 1, SkillName.Magery, 50 );
		}

		public override void AddNameProperties( ObjectPropertyList list )
		{
			base.AddNameProperties( list );
			list.Add( 1070722, "5th Circle of Power" );			list.Add( 1049644, "Requires 30 Intelligence" );
		}

		public PoisonFieldMagicStaff( Serial serial ) : base( serial )
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
			if ( IntRequirement != 30 ) { IntRequirement = 30; }
		}

		public override void OnMagicStaffUse( Mobile from )
		{
			Cast( new PoisonFieldSpell( from, this ) );
		}
	}
}