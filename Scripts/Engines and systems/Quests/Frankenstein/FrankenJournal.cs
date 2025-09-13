using System;
using Server;
using Server.Network;
using Server.Multis;
using Server.Gumps;
using Server.Mobiles;
using Server.Accounting;

namespace Server.Items
{
	public class FrankenJournal : Item
	{
		public Mobile JournalOwner;
		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Journal_Owner { get{ return JournalOwner; } set{ JournalOwner = value; } }

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public int HasHead;
		[CommandProperty(AccessLevel.Owner)]
		public int Has_Head { get { return HasHead; } set { HasHead = value; InvalidateProperties(); } }

		public int HasTorso;
		[CommandProperty(AccessLevel.Owner)]
		public int Has_Torso { get { return HasTorso; } set { HasTorso = value; InvalidateProperties(); } }

		public int HasBrain;
		[CommandProperty(AccessLevel.Owner)]
		public int Has_Brain { get { return HasBrain; } set { HasBrain = value; InvalidateProperties(); } }

		public int HasArmLeft;
		[CommandProperty(AccessLevel.Owner)]
		public int Has_ArmLeft { get { return HasArmLeft; } set { HasArmLeft = value; InvalidateProperties(); } }

		public int HasArmRight;
		[CommandProperty(AccessLevel.Owner)]
		public int Has_ArmRight { get { return HasArmRight; } set { HasArmRight = value; InvalidateProperties(); } }

		public int HasLegLeft;
		[CommandProperty(AccessLevel.Owner)]
		public int Has_LegLeft { get { return HasLegLeft; } set { HasLegLeft = value; InvalidateProperties(); } }

		public int HasLegRight;
		[CommandProperty(AccessLevel.Owner)]
		public int Has_LegRight { get { return HasLegRight; } set { HasLegRight = value; InvalidateProperties(); } }

		public string BrainFrom;
		[CommandProperty(AccessLevel.Owner)]
		public string Brain_From { get { return BrainFrom; } set { BrainFrom = value; InvalidateProperties(); } }

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		[Constructable]
		public FrankenJournal() : base( 0x1A97 )
		{
			Weight = 1.0;
			Hue = 0xB51;
			Name = "Frankenstein's Journal";
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			if ( JournalOwner != null ){ list.Add( 1049644, "Now Belongs to " + JournalOwner.Name + "" ); }
        }

		public override void OnDoubleClick( Mobile from )
		{
			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1060640 ); // The item must be in your backpack to use it.
			}
			else if ( JournalOwner != from )
			{
				from.SendMessage( "This journal does not belong to you toss it out!" );
				bool remove = true;
				foreach ( Account a in Accounts.GetAccounts() )
				{
					if (a == null)
						break;

					int index = 0;

					for (int i = 0; i < a.Length; ++i)
					{
						Mobile m = a[i];

						if (m == null)
							continue;

						if ( m == JournalOwner )
						{
							m.AddToBackpack( this );
							remove = false;
						}

						++index;
					}
				}
				if ( remove )
				{
					this.Delete();
				}
			}
			else
			{
				from.SendSound( 0x55 );
				from.CloseGump( typeof( FrankenGump ) );
				from.SendGump( new FrankenGump( this, from ) );
			}
		}

		public FrankenJournal(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);
			writer.Write( (Mobile)JournalOwner);
			writer.Write( HasHead );
			writer.Write( HasTorso );
			writer.Write( HasBrain );
			writer.Write( HasArmLeft );
			writer.Write( HasArmRight );
			writer.Write( HasLegLeft );
			writer.Write( HasLegRight );
			writer.Write( BrainFrom );
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
			JournalOwner = reader.ReadMobile();
			HasHead = reader.ReadInt();
			HasTorso = reader.ReadInt();
			HasBrain = reader.ReadInt();
			HasArmLeft = reader.ReadInt();
			HasArmRight = reader.ReadInt();
			HasLegLeft = reader.ReadInt();
			HasLegRight = reader.ReadInt();
			BrainFrom = reader.ReadString();
		}

		private class FrankenGump : Gump
		{
			private FrankenJournal m_Journal;
			private Mobile m_From;

			public FrankenGump( FrankenJournal book, Mobile from ) : base( 25, 25 )
			{
				m_Journal = book;
				m_From = from;

				this.Closable=true;
				this.Disposable=true;
				this.Dragable=true;
				this.Resizable=false;

				AddPage(0);
				AddImage(0, 0, 154);
				AddImage(300, 0, 154);
				AddImage(0, 300, 154);
				AddImage(300, 300, 154);
				AddImage(2, 2, 129);
				AddImage(298, 2, 129);
				AddImage(2, 298, 129);
				AddImage(298, 298, 129);
				AddImage(363, 6, 138);
				AddImage(6, 6, 133);
				AddImage(179, 12, 165);
				AddImage(273, 8, 165);
				AddImage(271, 568, 130);
				AddImage(151, 568, 130);
				AddImage(556, 563, 143);
				AddImage(123, 563, 159);
				AddItem(8, 476, 19440);
				AddItem(18, 386, 2215);

				AddHtml( 189, 42, 220, 20, @"<BODY><BASEFONT Color=#FBFBFB><BIG><CENTER>Frankenstein's Journal</CENTER></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 110, 91, 379, 312, @"<BODY><BASEFONT Color=#FCFF00><BIG>This book contains the writings of Doctor Victor Frankenstein, a notable alchemist and forensic expert. Within these pages, are the secrets to reanimating a creature that can serve your purposes. Where most only have achieved such creatures of human size, this tome explains how to create a creature of great power. To do this, one would need to be at least a neophyte undertaker. While carrying this book, and using a surgeon knife, you must find the corpses of giants to obtain the body parts necessary for the construction of such a creature. Giants are creatures like ogres, ettins, and cyclops. These body parts may be difficult to sever from the creature, so you may have to slay many to collect what you need. If you get body parts you don’t need, then perhaps the undertaker in the Black Magic Guild will procure them from you.<br><br>As you collect individual severed parts, double click them and target this journal to add it to your upcoming experiment. You may only have one of each body part for this experiment: a torso, head, left arm, right arm, left leg, and right leg. You will also need a brain from a giant, and the more powerful the better. A brain of a storm giant will give your creation more power than the brain of a stupid ogre. Once you have a brain, add it to your experiment in the same manner. Unlike other body parts, you can add a different brain later on before running the final experiment. Whenever you add a different brain, you will throw the old one away.<br><br>Once you have everything you need, you then need to find a power coil that can generate enough electrical energy to reanimate the corpse. The undertaker I wrote of earlier has one in their lab, but he also will sell you a finely tinkered one to place in your home. When you are close enough to a power coil, then select the type of creature you want to reanimate. You have your choice of a reanimated warrior or a slave to carry your items for you. The warrior will fight at your command, while the other will carry your items and other creatures seem to leave it be.<br><br>An item will appear in your pack that will allow you to summon the creature. Once summoned, the item will vanish until you release the creature and then the item will reappear in your pack. If the warrior creature were to die in battle, then the item will appear in your pack as well. In order to summon your reanimation, you will need embalming fluid to keep it from rotting away. Undertakers sell these at high prices, but one good a forensics can sometimes find them on the corpses of other reanimations, zombies, or mummies. If you manage to get some embalming fluid, simply use it on the reanimation’s item in your pack to add the preservative.</BIG></BASEFONT></BODY>", (bool)false, (bool)true);

				int bodyParts = 0;

				if ( book.HasBrain > 0 ){ bodyParts = 1; }

				bodyParts = bodyParts + book.HasTorso + book.HasHead + book.HasArmLeft + book.HasArmRight + book.HasLegLeft + book.HasLegRight;

				if ( book.HasBrain > 0 )
				{ 
					AddItem(113, 413, 9698);
					AddHtml( 154, 415, 284, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>from " + book.BrainFrom + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					AddHtml( 154, 444, 284, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>Level " + book.HasBrain + " Brain</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				}

				if ( book.HasArmRight > 0 ){ AddItem(449, 417, 14988); } // RIGHT ARM
				if ( book.HasArmLeft > 0 ){ AddItem(547, 417, 14991); } // LEFT ARM
				if ( book.HasLegRight > 0 ){ AddItem(471, 467, 16025); } // RIGHT LEG
				if ( book.HasLegLeft > 0 ){ AddItem(522, 466, 16002); } // LEFT LEG
				if ( book.HasTorso > 0 ){ AddItem(491, 415, 15003); } // TORSO
				if ( book.HasHead > 0 ){ AddItem(504, 399, 15873); } // HEAD

				if ( bodyParts > 6 )
				{
					AddButton(156, 495, 4005, 4005, 1, GumpButtonType.Reply, 0);
					AddHtml( 195, 496, 241, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>Reanimate a Slave</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

					AddButton(157, 535, 4005, 4005, 2, GumpButtonType.Reply, 0);
					AddHtml( 196, 536, 241, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>Reanimate a Protector</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				}
			}

			public override void OnResponse( NetState sender, RelayInfo info )
			{
				if ( info.ButtonID > 0 )
				{
					Point3D loc = m_From.Location;
					Map map = m_From.Map;

					bool nearCoil = false;
					foreach ( Item coil in m_From.GetItemsInRange( 10 ) )
					{
						if ( coil is PowerCoil )
						{
							nearCoil = true;
							loc = new Point3D(coil.X, coil.Y, (coil.Z+20));
						}
					}

					if ( nearCoil )
					{
						int Fighter = info.ButtonID-1;

						FrankenPorterItem flesh = new FrankenPorterItem();

						string QuestLog = "has reanimated a flesh golem";

						flesh.PorterOwner = m_From.Serial;
						flesh.PorterLevel = m_Journal.HasBrain;
						flesh.PorterType = Fighter;

						m_From.AddToBackpack ( flesh );

						Server.Misc.LoggingFunctions.LogGenericQuest( m_From, QuestLog );

						m_From.PrivateOverheadMessage(MessageType.Regular, 1153, false, "My experiment is a success.", m_From.NetState);

						int sound = Utility.RandomList( 0x028, 0x029 );
						Effects.SendLocationEffect( loc, map, 0x2A4E, 30, 10, 0, 0 );
						m_From.PlaySound( sound );

						m_Journal.Delete();
					}
					else
					{
						m_From.SendMessage("You need to be near a power coil to do that.");
						m_From.SendSound( 0x55 );
					}
				}
				else
				{
					m_From.SendSound( 0x55 );
					m_From.CloseGump( typeof( StatsGump ) );
				}
			}
		}
	}
}