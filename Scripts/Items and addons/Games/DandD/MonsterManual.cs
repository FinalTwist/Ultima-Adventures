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
    public class MonsterManual : Item
	{
        [Constructable]
        public MonsterManual() : base( 0x301E )
		{
            Name = "Monster Manual";
			Weight = 1.0;
        }

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
            list.Add( 1049644, "Dungeons & Dragons");
        }

        public override void OnDoubleClick( Mobile from )
		{
			from.SendMessage( "What creature do you want to look up?" );
			Target t = new BookTarget( this );
			from.Target = t;
			from.SendSound( 0x55 );
        }

		private class BookTarget : Target
		{
			private MonsterManual m_Book;

			public BookTarget( MonsterManual researched ) : base( 12, true, TargetFlags.None )
			{
				m_Book = researched;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( targeted is PlayerMobile )
				{
					from.SendMessage( "You would probably need the Players Handbook for that." );
				}
				else if ( targeted is HenchmanMonster || targeted is HenchmanWizard || targeted is HenchmanFighter || targeted is HenchmanArcher )
				{
					from.SendMessage( "These henchman wouldn't want the scrutiny." );
				}
				else if (	targeted is BaseVendor || targeted is BasePerson || targeted is Citizens || targeted is PackBeast || 
							targeted is FrankenPorter || targeted is FrankenFighter || targeted is HenchmanFamiliar || targeted is AerialServant || 
							targeted is GolemPorter || targeted is Robot || targeted is GolemFighter || targeted is HenchmanArcher || 
							targeted is HenchmanMonster || targeted is HenchmanFighter || targeted is HenchmanWizard )
				{
					from.SendMessage( "They don't seem to be in this book." );
				}
				else if ( targeted is Mobile )
				{
					Mobile m = (Mobile)targeted;

					if ( Server.Items.PlayersHandbook.IsPeople( m ) )
					{
						from.SendMessage( "You would probably need the Players Handbook for that." );
					}
					else if ( m is BaseCreature )
					{
						BaseCreature c = (BaseCreature)m;
						from.CloseGump( typeof( Server.SkillHandlers.AnimalLoreGump ) );
						from.SendGump( new Server.SkillHandlers.AnimalLoreGump( c, 1 ) );
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

        public MonsterManual( Serial serial ) : base( serial )
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