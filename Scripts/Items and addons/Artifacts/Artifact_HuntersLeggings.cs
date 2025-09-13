using System;
using Server;


namespace Server.Items
{
	public class HuntersLeggings : LeatherLegs
	{
		public override int InitMinHits{ get{ return 80; } }
		public override int InitMaxHits{ get{ return 160; } }


		public override int LabelNumber{ get{ return 1061595; } } // Hunter's Leggings


		public override int BaseColdResistance{ get{ return 25; } }


		[Constructable]
		public HuntersLeggings()
		{
			Name = "Hunter's Leggings";
			Hue = 0x594;
			SkillBonuses.SetValues( 0, SkillName.Archery, 20 );
			Attributes.BonusDex = 5;
			Attributes.NightSight = 1;
			Attributes.AttackChance = 20;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public HuntersLeggings( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );


			writer.Write( (int) 1 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );


			int version = reader.ReadInt();
			switch ( version )
			{
				case 0:
				{
					ColdBonus = 0;
					break;
				}
			}
		}
	}
}