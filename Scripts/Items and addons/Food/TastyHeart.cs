using System;
using System.Collections;
using Server.Network;
using Server.Targeting;
using Server.Prompts;

namespace Server.Items
{
	public class TastyHeart : Item
	{
		private string HeartName;

		[CommandProperty( AccessLevel.GameMaster )]
		public string Heart_Name { get { return HeartName; } set { HeartName = value; } }

		[Constructable]
		public TastyHeart() : this( null )
		{
		}

		[Constructable]
		public TastyHeart( string sName ) : base( 0x1CED )
		{
			if ( sName != null ){ HeartName = "the heart of " + sName; } else { HeartName = "heart"; }
			Name = HeartName;
			Weight = 0.1;
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
				if ( from.Hunger < 20 )
				{
					from.Hunger += 3;
					// Send message to character about their current Hunger value
					int iHunger = from.Hunger;
					if ( iHunger < 5 )
						from.SendMessage( "You eat the " + HeartName + ", but are still extremely hungry." );
					else if ( iHunger < 10 )
						from.SendMessage( "You eat the " + HeartName + ", feeling more satiated." );
					else if ( iHunger < 15 )
						from.SendMessage( "You eat the " + HeartName + ", feeling much less hungry." );
					else
						from.SendMessage( "You eat the " + HeartName + ", but now feel quite full." );

					this.Consume();

					// Play a random "eat" sound
					from.PlaySound( Utility.Random( 0x3A, 3 ) );

					if ( from.Body.IsHuman && !from.Mounted )
						from.Animate( 34, 5, 1, true, false, 0 );

					int iHeal = (int)from.Skills[SkillName.TasteID].Value;
					int iHurt = from.HitsMax - from.Hits;

					if ( iHurt > 0 )
					{
						if ( iHeal > iHurt )
						{
							iHeal = iHurt;
						}

						from.Hits = from.Hits + iHeal;
					}

					Misc.Titles.AwardKarma( from, -50, true );
				}
				else
				{
					from.SendMessage( "You don't feel hungry enough to eat the " + HeartName + "." );
					from.Hunger = 20;
				}
			}
		}

		public TastyHeart(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);
            writer.Write( HeartName );
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
            HeartName = reader.ReadString();
		}
	}
}