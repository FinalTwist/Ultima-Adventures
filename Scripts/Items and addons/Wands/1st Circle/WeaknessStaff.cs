using System;
using Server;
using Server.Spells.First;
using Server.Targeting;

namespace Server.Items
{
	public class WeaknessMagicStaff : BaseMagicStaff
	{
		[Constructable]
		public WeaknessMagicStaff() : base( MagicStaffEffect.Charges, 1, 25 )
		{
			IntRequirement = 10;			Name = "wand of weakness";
			SkillBonuses.SetValues( 1, SkillName.Magery, 10 );
		}

		public override void AddNameProperties( ObjectPropertyList list )
		{
			base.AddNameProperties( list );
			list.Add( 1070722, "1st Circle of Power" );			list.Add( 1049644, "Requires 10 Intelligence" );
		}

		public WeaknessMagicStaff( Serial serial ) : base( serial )
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
			if ( IntRequirement != 10 ) { IntRequirement = 10; }
		}

		public override void OnMagicStaffUse( Mobile from )
		{
			Cast( new WeakenSpell( from, this ) );
		}
	}
}