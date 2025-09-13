using System;
using Server;

namespace Server.Items
{
	public class NecklaceOfGenesis : BaseNecklace
	{
		
		public override int ArtifactRarity{ get{ return 100; } }
                 
                [Constructable]

		public NecklaceOfGenesis() : base( 0x1085 ) 
		{
			Weight = 1.0; 
            		Name = "Necklace Of Genesis"; 
            		Hue = 2561;

			Attributes.AttackChance = 10;
			Attributes.BonusHits = 10;
			Attributes.DefendChance = 5;
			Attributes.ReflectPhysical = 10;
			Attributes.SpellDamage = 5;
                        Attributes.RegenHits = 2;
			
			}

		public override void OnAdded(IEntity parent )
		{
			base.OnAdded( parent );
			if( parent is Mobile )
			{
				Mobile from = (Mobile)parent;
				from.Skills.Swords.Base += 10;
			}
		}
		public override void OnRemoved(IEntity parent )
		{
			base.OnRemoved( parent );
			if( parent is Mobile )
			{
				Mobile from = (Mobile)parent;
				from.Skills.Swords.Base -= 10;
			}
			
		}

		public NecklaceOfGenesis( Serial serial ) : base( serial )
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