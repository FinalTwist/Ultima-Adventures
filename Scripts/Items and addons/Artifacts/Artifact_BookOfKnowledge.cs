using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Engines.Craft;


namespace Server.Items
{
	public class BookOfKnowledge : Spellbook, ITokunoDyable
	{
		[Constructable]
		public BookOfKnowledge() : base()
		{
			Name = "Book Of Knowledge";
			Hue = 1171;
			
			Attributes.SpellDamage = 35;
			Attributes.BonusInt = 15;
			Attributes.CastSpeed = 1;
			Attributes.CastRecovery = 1;
			LootType = LootType.Regular;
			SkillBonuses.SetValues(0, SkillName.EvalInt, 15);
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public BookOfKnowledge( Serial serial ) : base( serial )
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


