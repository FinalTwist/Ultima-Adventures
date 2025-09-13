using System;
using Server;
using Server.Network;
using Server.Mobiles;
using Server.Items;
using System.Collections.Generic;
using Server.Misc;
using System.Collections;
using Server.Targeting;
using Server.Gumps;

namespace Server.Items
{
    public class PlayersHandbook : Item
	{
        [Constructable]
        public PlayersHandbook() : base( 0x301F )
		{
            Name = "Players Handbook";
			Weight = 1.0;
        }

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
            list.Add( 1049644, "Dungeons & Dragons");
        }

        public override void OnDoubleClick( Mobile from )
		{
			from.SendMessage( "What person do you want to look up?" );
			Target t = new BookTarget( this );
			from.Target = t;
			from.SendSound( 0x55 );
        }

		public static bool IsPeople( Mobile m )
		{
			if ( (SlayerGroup.GetEntryByName( SlayerName.OrcSlaying )).Slays(m) )
				return false;

			if ( m is BaseVendor || m is BasePerson )
				return false;

			if ( (SlayerGroup.GetEntryByName( SlayerName.Repond )).Slays(m) && ( m.Body == 0x190 || m.Body == 0x191 || m.Body == 605 || m.Body == 606 ) )
				return true;

			if ( (SlayerGroup.GetEntryByName( SlayerName.Fey )).Slays(m) && ( m.Body == 0x190 || m.Body == 0x191 || m.Body == 605 || m.Body == 606 ) )
				return true;

			return false;
		}

		private class BookTarget : Target
		{
			private PlayersHandbook m_Book;

			public BookTarget( PlayersHandbook researched ) : base( 12, true, TargetFlags.None )
			{
				m_Book = researched;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( targeted is PlayerMobile )
				{
					Mobile p = (Mobile)targeted;
					from.CloseGump( typeof( StatsGump ) );
					from.SendGump( new StatsGump( p, 1 ) );
					from.SendSound( 0x55 );
				}
				else if ( targeted is BaseVendor || targeted is BasePerson || targeted is Citizens )
				{
					from.SendMessage( "The Players Handbook doesn't cover those types of characters." );
				}
				else if ( targeted is HenchmanMonster || targeted is HenchmanWizard || targeted is HenchmanFighter || targeted is HenchmanArcher )
				{
					from.SendMessage( "These henchman wouldn't want the scrutiny." );
				}
				else if (	targeted is PackBeast || targeted is GolemPorter || targeted is GolemFighter || targeted is Robot || 
							targeted is FrankenPorter || targeted is FrankenFighter || targeted is HenchmanFamiliar || targeted is AerialServant )
				{
					from.SendMessage( "They don't seem to be in this book." );
				}
				else if ( targeted is Mobile )
				{
					Mobile m = (Mobile)targeted;

					if ( !IsPeople( m ) )
					{
						from.SendMessage( "You would probably need the Monster Manual for that." );
					}
					else if ( m is BaseCreature )
					{
						BaseCreature c = (BaseCreature)m;
						from.CloseGump( typeof( Server.SkillHandlers.AnimalLoreGump ) );
						from.SendGump( new Server.SkillHandlers.AnimalLoreGump( c, 2 ) );
						from.SendSound( 0x55 );
					}
					else
					{
						from.SendMessage( "That doesn't seem to be in this book." );
					}
				}
				else
				{
					from.SendMessage( "That doesn't seem to be in this book." );
				}
			}
		}

        public PlayersHandbook( Serial serial ) : base( serial )
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