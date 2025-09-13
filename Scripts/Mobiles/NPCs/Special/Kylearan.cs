using System;
using System.Collections;
using Server.ContextMenus;
using System.Collections.Generic;
using Server.Misc;
using Server.Network;
using Server;
using Server.Items;
using Server.Gumps;
using Server.Mobiles;
using Server.Commands;

namespace Server.Mobiles
{
	public class Kylearan : BaseNPC
	{
		public override bool InitialInnocent{ get{ return true; } }

		[Constructable]
		public Kylearan() : base( )
		{
			Body = 400; 
			Name = "Kylearan";
			Title = "the Magician";
			NameHue = 0xB0C;
			AI = AIType.AI_Citizen;
			FightMode = FightMode.None;

			Direction = Direction.North;
			CantWalk = true;

			Hue = 0x83EA;
			FacialHairItemID = 0x204C; // BEARD
			HairItemID = 0x203C; // LONG HAIR
			FacialHairHue = 0x47E;
			HairHue = 0x47E;

			AddItem( new Sandals() );
			Item cloth1 = new Robe();
				cloth1.Hue = Utility.RandomBlueHue();
				AddItem( cloth1 );
			Item cloth2 = new WizardsHat();
				cloth2.Hue = Utility.RandomBlueHue();
				AddItem( cloth2 );
		}

		public override bool OnBeforeDeath()
		{
			Say("In Vas Mani");
			this.Hits = this.HitsMax;
			this.FixedParticles( 0x376A, 9, 32, 5030, EffectLayer.Waist );
			this.PlaySound( 0x202 );
			return false;
		}

		public override bool IsEnemy( Mobile m )
		{
			return false;
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list ) 
		{ 
			base.GetContextMenuEntries( from, list ); 
			list.Add( new KylearanEntry( from, this ) ); 
		} 

		public class KylearanEntry : ContextMenuEntry
		{
			private Mobile m_Mobile;
			private Mobile m_Giver;
			
			public KylearanEntry( Mobile from, Mobile giver ) : base( 6146, 3 )
			{
				m_Mobile = from;
				m_Giver = giver;
			}

			public override void OnClick()
			{
			    if( !( m_Mobile is PlayerMobile ) )
				return;

				if ( CharacterDatabase.GetBardsTaleQuest( m_Mobile, "BardsTaleEbonyKey" ) )
				{
					m_Giver.Say("You already have the onyx key. Use it to enter Mangar's tower.");
				}
				else if ( ! m_Mobile.HasGump( typeof( SpeechGump ) ) )
				{
					CharacterDatabase.SetBardsTaleQuest( m_Mobile, "BardsTaleEbonyKey", true );
					m_Mobile.SendSound( 0x3D );
					m_Mobile.SendGump(new SpeechGump( "Thank You Brave Adventurer", SpeechFunctions.SpeechText( m_Giver.Name, m_Mobile.Name, "Kylearan" ) ));


					ManualOfItems book = new ManualOfItems();
						book.Hue = 0xA20;
						book.Name = "Tome of Kylearan Relics";
						book.m_Charges = 1;
						book.m_Skill_1 = 17;
						book.m_Skill_2 = 31;
						book.m_Skill_3 = 32;
						book.m_Skill_4 = 33;
						book.m_Skill_5 = 0;
						book.m_Value_1 = 5.0;
						book.m_Value_2 = 5.0;
						book.m_Value_3 = 5.0;
						book.m_Value_4 = 5.0;
						book.m_Value_5 = 0.0;
						book.m_Slayer_1 = 27;
						book.m_Slayer_2 = 0;
						book.m_Owner = m_Mobile;
						book.m_Extra = "of the Archmage";
						book.m_FromWho = "Gifted from Kylearan";
						book.m_HowGiven = "Gifted to";
						book.m_Points = 150;
						book.m_Hue = 0xA20;
						m_Mobile.AddToBackpack( book );
						m_Mobile.SendMessage( "A book has been added to your pack!" );
				}
            }
        }

		public Kylearan( Serial serial ) : base( serial )
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