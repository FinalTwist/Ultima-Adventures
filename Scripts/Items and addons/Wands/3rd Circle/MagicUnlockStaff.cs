using System;
using Server;
using Server.Spells.Third;
using Server.Targeting;

namespace Server.Items
{
	public class MagicUnlockMagicStaff : BaseMagicStaff
	{
		[Constructable]
		public MagicUnlockMagicStaff() : base( MagicStaffEffect.Charges, 1, 15 )
		{
			IntRequirement = 20;			Name = "wand of unlocking";
			SkillBonuses.SetValues( 1, SkillName.Magery, 30 );
		}

		public override void AddNameProperties( ObjectPropertyList list )
		{
			base.AddNameProperties( list );
			list.Add( 1070722, "3rd Circle of Power" );			list.Add( 1049644, "Requires 20 Intelligence" );
		}

		public MagicUnlockMagicStaff( Serial serial ) : base( serial )
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
			if ( IntRequirement != 20 ) { IntRequirement = 20; }
		}

		public override void OnMagicStaffUse( Mobile from )
		{
			Cast( new UnlockSpell( from, this ) );
		}
	}
}