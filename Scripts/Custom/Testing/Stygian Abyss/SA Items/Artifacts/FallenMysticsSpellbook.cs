using System;
using Server;

namespace Server.Items
{
	public class FallenMysticsSpellbook : Spellbook
	{
	
		[Constructable]
		public FallenMysticsSpellbook() : base()
		{
			Name = ("Fallen Mystic's Spellbook");
		
			Hue = 687;
			
			SkillBonuses.SetValues( 0, SkillName.Mysticism, 10.0 );			
			Attributes.LowerManaCost = 5;	
			Attributes.RegenMana = 1;
			Attributes.LowerRegCost = 10;
			Attributes.CastRecovery = 1;
			Attributes.CastSpeed = 1;		
			Attributes.SpellDamage = 10;
			Slayer = SlayerName.Fey;
		}
		
		public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }

		public FallenMysticsSpellbook( Serial serial ) : base( serial )
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

