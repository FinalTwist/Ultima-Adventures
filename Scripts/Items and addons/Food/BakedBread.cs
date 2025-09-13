using System;
using System.Collections;
using Server.Network;
using Server.Targeting;
using Server.Prompts;

namespace Server.Items
{
	public class BakedBread : Item
	{
		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		[Constructable]
		public BakedBread() : this( 1 )
		{
		}

		[Constructable]
		public BakedBread( int amount ) : base( 0x103B )
		{
			Name = "bread";
			Hue = 0;
			Amount = amount;
			Stackable = true;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !IsChildOf( from.Backpack ) ) 
			{
				from.SendMessage( "This must be in your backpack to use." );
				return;
			}
			else
			{
				Target t;
				int number;

				if ( from.Hunger < 20 )
				{
					from.Hunger += 3;
					// Send message to character about their current Hunger value
					int iHunger = from.Hunger;
					if ( iHunger < 5 )
						from.SendLocalizedMessage( 500868 ); // You eat the food, but are still extremely hungry.
					else if ( iHunger < 10 )
						from.SendLocalizedMessage( 500869 ); // You eat the food, and begin to feel more satiated.
					else if ( iHunger < 15 )
						from.SendLocalizedMessage( 500870 ); // After eating the food, you feel much less hungry.
					else
						from.SendLocalizedMessage( 500871 ); // You feel quite full after consuming the food.

					this.Consume();

					// Play a random "eat" sound
					from.PlaySound( Utility.Random( 0x3A, 3 ) );

					if ( from.Body.IsHuman && !from.Mounted )
						from.Animate( 34, 5, 1, true, false, 0 );

					int iHeal = (int)from.Skills[SkillName.TasteID].Value;
					int iHurt = from.HitsMax - from.Hits;

					if ( iHurt > 0 ) // WIZARD DID THIS FOR TASTE ID
					{
						if ( iHeal > iHurt )
						{
							iHeal = iHurt;
						}

						from.Hits = from.Hits + iHeal;

						if ( from.Poisoned )
						{
							if ( (int)from.Skills[SkillName.TasteID].Value >= Utility.RandomMinMax( 1, 100 ) )
							{
								from.CurePoison( from );
								from.SendLocalizedMessage( 1010059 ); // You have been cured of all poisons.
							}
						}
					}
				}
				else
				{
					from.SendMessage( "You are simply too full to eat any more!" );
					from.Hunger = 20;
				}
			}
		}

		public BakedBread(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}
}