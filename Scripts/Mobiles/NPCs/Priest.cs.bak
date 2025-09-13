using System;
using System.Collections.Generic;
using Server;
using Server.Targeting;
using Server.Items;
using Server.Network;
using Server.ContextMenus;
using Server.Gumps;
using Server.Misc;
using Server.Mobiles;
using Server.Regions;

namespace Server.Mobiles
{
	public class Priest : BaseHealer
	{
		public override bool CanTeach{ get{ return true; } }

		public override bool CheckTeach( SkillName skill, Mobile from )
		{
			if ( !base.CheckTeach( skill, from ) )
				return false;

			return ( skill == SkillName.Healing )
				|| ( skill == SkillName.SpiritSpeak )
				|| ( skill == SkillName.Macing );
		}

		[Constructable]
		public Priest()
		{
			Title = "the priest";
			Direction = Direction.East;
			CantWalk = true;
			SpeechHue = Server.Misc.RandomThings.GetSpeechHue();
			NameHue = 0xB0C;
			SetSkill( SkillName.SpiritSpeak, 80.0, 100.0 );
			SetSkill( SkillName.Macing, 80.0, 100.0 );
		}

		public override bool IsActiveVendor{ get{ return true; } }

		public override void InitSBInfo()
		{
			SBInfos.Add( new SBHealer() );

			if ( this.Map == Map.Felucca )
				SBInfos.Add( new SBElfHealer() );
		}

		public override void InitOutfit()
		{
			base.InitOutfit();

			AddItem( new Robe( 0x47E ) );
		}

		///////////////////////////////////////////////////////////////////////////
		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list ) 
		{ 
			base.GetContextMenuEntries( from, list ); 
			list.Add( new SpeechGumpEntry( from, this ) ); 
		} 

		public class SpeechGumpEntry : ContextMenuEntry
		{
			private Mobile m_Mobile;
			private Mobile m_Giver;
			
			public SpeechGumpEntry( Mobile from, Mobile giver ) : base( 6146, 3 )
			{
				m_Mobile = from;
				m_Giver = giver;
			}

			public override void OnClick()
			{
			    if( !( m_Mobile is PlayerMobile ) )
				return;
				
				PlayerMobile mobile = (PlayerMobile) m_Mobile;
				{
					if ( ! mobile.HasGump( typeof( SpeechGump ) ) )
					{
						mobile.SendGump(new SpeechGump( "Thou Art Going To Get Hurt", SpeechFunctions.SpeechText( m_Giver.Name, m_Mobile.Name, "Healer" ) ));
					}
				}
            }
        }
		///////////////////////////////////////////////////////////////////////////

		public override bool OnDragDrop( Mobile from, Item dropped )
		{
			int hasSymbol = 0;
			int hasBook = 0;
			int isPriest = 0;

			if ( dropped is MalletStake )
			{
				MalletStake stake = (MalletStake)dropped;

				int reward = stake.VampiresSlain;

				if ( reward > 0 )
				{
					from.AddToBackpack( new Gold( reward ) );

					string sMessage = "Thank you. Here is " + reward + " gold for your bravery.";

					if ( reward >= 1000 && from.Karma >= 2500 && from.Skills[SkillName.SpiritSpeak].Base > 0 && from.Skills[SkillName.Healing].Base > 0 )
					{
						foreach ( Item item in World.Items.Values )
						{
							if ( item is HolySymbol )
							{
								HolySymbol symbol = (HolySymbol)item;
								if ( symbol.owner == from )
								{
									from.AddToBackpack( symbol );
									hasSymbol = 1;
								}
							}
							else if ( item is HolyManSpellbook )
							{
								HolyManSpellbook book = (HolyManSpellbook)item;
								if ( book.owner == from )
								{
									from.AddToBackpack( book );
									hasBook = 1;
								}
							}
						}

						if ( hasSymbol == 0 ){ from.AddToBackpack ( new HolySymbol( from ) ); }
						if ( hasBook == 0 ){ HolyManSpellbook tome = new HolyManSpellbook( (ulong)0, from ); from.AddToBackpack ( tome ); }

						from.SendMessage( "You have been given your holy symbol and prayer book." );

						if ( hasSymbol + hasBook == 0 )
						{
							isPriest = 1;
							LoggingFunctions.LogGenericQuest( from, "has become a priest" );
							from.FixedParticles( 0x373A, 10, 15, 5018, EffectLayer.Waist );
							from.PlaySound( 0x1EA );
							sMessage = from.Name + ", take the gold and these as well. You may be a good priest one day.";
						}
					}

					this.PrivateOverheadMessage(MessageType.Regular, 1153, false, sMessage, from.NetState);
					if ( isPriest == 0 ){ from.SendSound( 0x3D ); }
					dropped.Delete();
					return true;
				}


				return false;
			}

			return base.OnDragDrop( from, dropped );
		}

		public Priest( Serial serial ) : base( serial )
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