using System;
using System.Collections.Generic;
using Server;
using Server.Targeting;
using Server.Items;
using Server.Network;
using Server.ContextMenus;
using Server.Misc;
using Server.Mobiles;
using System.Collections;
using Server.Gumps;

namespace Server.Mobiles
{
	public class LibrarianGuildmaster : BaseGuildmaster
	{
		public override NpcGuild NpcGuild{ get{ return NpcGuild.LibrariansGuild; } }

		[Constructable]
		public LibrarianGuildmaster() : base( "librarian" )
		{
			Job = JobFragment.scholar;
			Karma = Utility.RandomMinMax( 13, -45 );
			SetSkill( SkillName.Inscribe, 60.0, 83.0 );
			SetSkill( SkillName.MagicResist, 65.0, 88.0 );
			SetSkill( SkillName.Wrestling, 60.0, 80.0 );
			SetSkill( SkillName.ItemID, 64.0, 100.0 );
		}

		public override void InitSBInfo()
		{
			SBInfos.Add( new SBLibraryGuild() ); 
			SBInfos.Add( new SBBuyArtifacts() ); 
		}

		public override VendorShoeType ShoeType
		{
			get{ return Utility.RandomBool() ? VendorShoeType.Shoes : VendorShoeType.Sandals; }
		}

		public override void InitOutfit()
		{
			base.InitOutfit();

			AddItem( new Server.Items.Robe( Server.Misc.RandomThings.GetRandomColor(0) ) );
		}

		///////////////////////////////////////////////////////////////////////////

		public override bool OnDragDrop( Mobile from, Item dropped )
		{
			if ( dropped is Gold )
			{
				string sMessage = "";

				if ( dropped.Amount == 500 )
				{
					if ( from.Skills[SkillName.Inscribe].Value >= 30 )
					{
						if ( Server.Misc.Research.AlreadyHasBag( from ) )
						{
							this.PublicOverheadMessage( MessageType.Regular, 0, false, string.Format ( "Good luck with your research." ) ); 
						}
						else
						{
							ResearchBag bag = new ResearchBag();
							from.PlaySound( 0x2E6 );
							Server.Misc.Research.SetupBag( from, bag );
							from.AddToBackpack( bag );
							this.PublicOverheadMessage( MessageType.Regular, 0, false, string.Format ( "Good luck with your research." ) ); 
						}
						dropped.Delete();
					}
					else
					{
						sMessage = "You need to be a neophyte scribe before I sell that to you.";
						from.AddToBackpack ( dropped );
					}
				}
				else
				{
					sMessage = "You look like you need this more than I do.";
					from.AddToBackpack ( dropped );
				}

				this.PrivateOverheadMessage(MessageType.Regular, 1153, false, sMessage, from.NetState);
			}
			else if ( dropped is Runebook )
			{
				if ( dropped.ItemID == 0x22C5 ){ dropped.ItemID = 0x0F3D; }
				else if ( dropped.ItemID == 0x0F3D ){ dropped.ItemID = 0x5687; }
				else if ( dropped.ItemID == 0x5687 ){ dropped.ItemID = 0x4F50; }
				else if ( dropped.ItemID == 0x4F50 ){ dropped.ItemID = 0x4F51; }
				else if ( dropped.ItemID == 0x4F51 ){ dropped.ItemID = 0x5463; }
				else if ( dropped.ItemID == 0x5449 ){ dropped.ItemID = 0x544A; }
				else { dropped.ItemID = 0x22C5; }

				from.PlaySound( 0x249 );
				from.AddToBackpack ( dropped );
				this.PrivateOverheadMessage(MessageType.Regular, 1153, false, "I have changed the cover of your book.", from.NetState);
			}

			return base.OnDragDrop( from, dropped );
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
						mobile.SendGump(new SpeechGump( "The Writing On The Wall", SpeechFunctions.SpeechText( m_Giver.Name, m_Mobile.Name, "Sage" ) ));
					}
				}
            }
        }
		///////////////////////////////////////////////////////////////////////////

		private class FixEntry : ContextMenuEntry
		{
			private LibrarianGuildmaster m_Sage;
			private Mobile m_From;

			public FixEntry( LibrarianGuildmaster LibrarianGuildmaster, Mobile from ) : base( 6120, 12 )
			{
				m_Sage = LibrarianGuildmaster;
				m_From = from;
			}

			public override void OnClick()
			{
				m_Sage.BeginRepair( m_From );
			}
		}

		public override void AddCustomContextEntries( Mobile from, List<ContextMenuEntry> list )
		{
			if ( from.Alive && !from.Blessed )
			{
				list.Add( new FixEntry( this, from ) );
			}

			base.AddCustomContextEntries( from, list );
		}

        public void BeginRepair(Mobile from)
        {
            if ( Deleted || !from.Alive )
                return;

			int nCost = 500;
			int nCost2 = 5000;
			int idCost = 200;

			if ( BeggingPose(from) > 0 ) // LET US SEE IF THEY ARE BEGGING - WIZARD
			{
				nCost = nCost - (int)( ( from.Skills[SkillName.Begging].Value * 0.005 ) * nCost );
				nCost2 = nCost2 - (int)( ( from.Skills[SkillName.Begging].Value * 0.005 ) * nCost2 );
				idCost = idCost - (int)( ( from.Skills[SkillName.Begging].Value * 0.005 ) * idCost ); if ( idCost < 1 ){ idCost = 1; }
				SayTo(from, "Since you are begging, I will ask for less to identify an item, wands cost " + nCost.ToString() + " gold and artifacts cost " + nCost2.ToString() + " gold. Or maybe identify another type item for " + idCost.ToString() + " gold?");
			}
			else { SayTo(from, "If you want me to identify an item, wands cost " + nCost.ToString() + " gold and artifacts cost " + nCost2.ToString() + " gold. Or maybe identify another type item for " + idCost.ToString() + " gold?"); }

            from.Target = new RepairTarget(this);
        }

        private class RepairTarget : Target
        {
            private LibrarianGuildmaster m_Sage;

            public RepairTarget(LibrarianGuildmaster mage) : base(12, false, TargetFlags.None)
            {
                m_Sage = mage;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                Container pack = from.Backpack;

				int nCost = 500;
				int nCost2 = 5000;

				if ( BeggingPose(from) > 0 ) // LET US SEE IF THEY ARE BEGGING - WIZARD
				{
					nCost = nCost - (int)( ( from.Skills[SkillName.Begging].Value * 0.005 ) * nCost );
					nCost2 = nCost2 - (int)( ( from.Skills[SkillName.Begging].Value * 0.005 ) * nCost2 );
				}

                if (targeted is UnknownWand && from.Backpack != null)
                {
                    int toConsume = nCost;

                    if (pack.ConsumeTotal(typeof(Gold), toConsume))
                    {
                        from.SendMessage(String.Format("You pay {0} gold.", toConsume));

						if ( Utility.RandomMinMax( 1, 100 ) > 10 )
						{
							Server.Items.UnknownWand.WandType( (Item)targeted, from, m_Sage );
						}
						else
						{
							int nJunk = Utility.RandomMinMax( 1, 5 );
							string stickName = "";
							switch( nJunk )
							{
								case 1: stickName = "a useless stick"; break;
								case 2: stickName = "a wand that was never enchanted"; break;
								case 3: stickName = "a fake wand"; break;
								case 4: stickName = "nothing magical at all"; break;
								case 5: stickName = "a simple metal rod"; break;
							}
							m_Sage.SayTo(from, "This seems to be " + stickName + ".");

						}
						((Item)targeted).Delete();
                    }
                    else
                    {
                        m_Sage.SayTo(from, "It would cost you {0} gold to have that identified.", toConsume);
                        from.SendMessage("You do not have enough gold.");
                    }
                }
				/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
				else if ( targeted is UnidentifiedArtifact )
				{
					Container packs = from.Backpack;
					nCost2 = 5000;
					UnidentifiedArtifact WhatIsIt = (UnidentifiedArtifact)targeted;

					if ( BeggingPose(from) > 0 ) // LET US SEE IF THEY ARE BEGGING - WIZARD
					{
						nCost2 = nCost2 - (int)( ( from.Skills[SkillName.Begging].Value * 0.005 ) * nCost2 ); if ( nCost2 < 1 ){ nCost2 = 1; }
					}
					int toConsume = nCost2;

                    if (packs.ConsumeTotal(typeof(Gold), toConsume))
                    {
						Container bpack = (Container)targeted;
							List<Item> items = new List<Item>();
							foreach (Item item in bpack.Items)
							{
								items.Add(item);
							}
							foreach (Item item in items)
							{
								from.AddToBackpack ( item );
							}
                        from.SendMessage(String.Format("You pay {0} gold.", toConsume));
						m_Sage.SayTo(from, "Let me tell you about this artifact...");
						WhatIsIt.Delete();
                    }
                    else
                    {
                        m_Sage.SayTo(from, "It would cost you {0} gold to have that identified.", toConsume);
                        from.SendMessage("You do not have enough gold.");
                    }
				}
				/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
				else if ( targeted is UnidentifiedItem )
				{
					Container packs = from.Backpack;
					nCost = 200;
					UnidentifiedItem WhatIsIt = (UnidentifiedItem)targeted;

					if ( BeggingPose(from) > 0 ) // LET US SEE IF THEY ARE BEGGING - WIZARD
					{
						nCost = nCost - (int)( ( from.Skills[SkillName.Begging].Value * 0.005 ) * nCost ); if ( nCost < 1 ){ nCost = 1; }
					}
					int toConsume = nCost;

                    if ( WhatIsIt.VendorCanID != "Sage" )
                    {
                        m_Sage.SayTo( from, "Sorry, I cannot tell what that is." );
					}
                    else if (packs.ConsumeTotal(typeof(Gold), toConsume))
                    {
						Container bpack = (Container)targeted;
							List<Item> items = new List<Item>();
							foreach (Item item in bpack.Items)
							{
								items.Add(item);
							}
							foreach (Item item in items)
							{
								from.AddToBackpack ( item );
							}
                        from.SendMessage(String.Format("You pay {0} gold.", toConsume));
						m_Sage.SayTo(from, "Let me tell you about this item...");
						WhatIsIt.Delete();
                    }
                    else
                    {
                        m_Sage.SayTo(from, "It would cost you {0} gold to have that identified.", toConsume);
                        from.SendMessage("You do not have enough gold.");
                    }
				}
				/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
				else if ( targeted is ScrollClue )
				{
					Container packs = from.Backpack;
					nCost = 100;
					ScrollClue WhatIsIt = (ScrollClue)targeted;

					if ( BeggingPose(from) > 0 ) // LET US SEE IF THEY ARE BEGGING - WIZARD
					{
						nCost = nCost - (int)( ( from.Skills[SkillName.Begging].Value * 0.005 ) * nCost ); if ( nCost < 1 ){ nCost = 1; }
					}
					int toConsume = nCost;

                    if ( WhatIsIt.ScrollIntelligence == 0 )
                    {
                        m_Sage.SayTo( from, "That was already deciphered by someone." );
					}
                    else if (packs.ConsumeTotal(typeof(Gold), toConsume))
                    {
						if ( WhatIsIt.ScrollIntelligence >= 80 ){ WhatIsIt.Name = "diabolically coded parchment"; }
						else if ( WhatIsIt.ScrollIntelligence >= 70 ){ WhatIsIt.Name = "ingeniously coded parchment"; }
						else if ( WhatIsIt.ScrollIntelligence >= 60 ){ WhatIsIt.Name = "deviously coded parchment"; }
						else if ( WhatIsIt.ScrollIntelligence >= 50 ){ WhatIsIt.Name = "cleverly coded parchment"; }
						else if ( WhatIsIt.ScrollIntelligence >= 40 ){ WhatIsIt.Name = "adeptly coded parchment"; }
						else if ( WhatIsIt.ScrollIntelligence >= 30 ){ WhatIsIt.Name = "expertly coded parchment"; }
						else { WhatIsIt.Name = "plainly coded parchment"; }

						WhatIsIt.ScrollIntelligence = 0;
						WhatIsIt.InvalidateProperties();
                        from.SendMessage(String.Format("You pay {0} gold.", toConsume));
						m_Sage.SayTo(from, "Let me show you what this reads...");
						WhatIsIt.ScrollSolved = "Deciphered by " + m_Sage.Name + " the Librarian";
						from.PlaySound( 0x249 );
						WhatIsIt.InvalidateProperties();
                    }
                    else
                    {
                        m_Sage.SayTo(from, "It would cost you {0} gold to have that deciphered.", toConsume);
                        from.SendMessage("You do not have enough gold.");
                    }
				}
				/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
				else
				{
					m_Sage.SayTo(from, "That does not need my services.");
				}
            }
        }

		public LibrarianGuildmaster( Serial serial ) : base( serial )
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