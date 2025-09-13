using System;
using Server;
using Server.Network;
using Server.Gumps;

namespace Server.Items
{
	[Flipable( 0x12AB, 0x12AC )]
    public class tarotpoker : Item
    {
        private bool m_IsNoisy;

        [CommandProperty(AccessLevel.GameMaster)]
        public bool IsNoisy
        {
            get { return m_IsNoisy; }
            set { m_IsNoisy = value; }
        }

        [Constructable]
        public tarotpoker() : base(0x12AB)
        {
            Stackable = false;
            Name = "Deck of Tarot Poker Cards";
            Weight = 0.5;
            m_IsNoisy = true;
        }
        public tarotpoker(Serial serial) : base(serial)
        {
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!from.InRange(this.GetWorldLocation(), 4))
                from.LocalOverheadMessage(MessageType.Regular, 0x3B2, 1019045); // I can't reach that.

            else switch (Utility.Random(22))
                {
                    default:
                    case 0:
                        {
                            from.CloseGump( typeof( TarotGump ) );
                            from.SendGump( new TarotGump( 0x454 ) );
                            this.PublicOverheadMessage(MessageType.Regular, 0, false, string.Format("{0} pulls 'The Fool'", from.Name));
                            this.PublicOverheadMessage(MessageType.Regular, 0, false, string.Format("Everyone bets 10."));
                            break;
                        }
                    case 1:
                        {
                            from.CloseGump( typeof( TarotGump ) );
                            from.SendGump( new TarotGump( 0x45C ) );
                            this.PublicOverheadMessage(MessageType.Regular, 0, false, string.Format("{0} pulls 'The Magician'", from.Name));
                            this.PublicOverheadMessage(MessageType.Regular, 0, false, string.Format("Everyone bets 100."));
                            break;
                        }
                    case 2:
                        {
                            from.CloseGump( typeof( TarotGump ) );
                            from.SendGump( new TarotGump( 0x458 ) );
                            this.PublicOverheadMessage(MessageType.Regular, 0, false, string.Format("{0} pulls 'The High Priestess'", from.Name));
                            this.PublicOverheadMessage(MessageType.Regular, 0, false, string.Format("Everyone bets 70."));
                            break;
                        }
                    case 3:
                        {
                            from.CloseGump( typeof( TarotGump ) );
                            from.SendGump( new TarotGump( 0x453 ) );
                            this.PublicOverheadMessage(MessageType.Regular, 0, false, string.Format("{0} pulls 'The Empress'", from.Name));
                            this.PublicOverheadMessage(MessageType.Regular, 0, false, string.Format("Bet 50."));
                            break;
                        }
                    case 4:
                        {
                            from.CloseGump( typeof( TarotGump ) );
                            from.SendGump( new TarotGump( 0x452 ) );
                            this.PublicOverheadMessage(MessageType.Regular, 0, false, string.Format("{0} pulls 'The Emperor'", from.Name));
                            this.PublicOverheadMessage(MessageType.Regular, 0, false, string.Format("Bet 100 gp."));
                            break;
                        }
                    case 5:
                        {
                            from.CloseGump( typeof( TarotGump ) );
                            from.SendGump( new TarotGump( 0x457 ) );
                            this.PublicOverheadMessage(MessageType.Regular, 0, false, string.Format("{0} pulls 'The Hierophant'", from.Name));
                            this.PublicOverheadMessage(MessageType.Regular, 0, false, string.Format("Bet 10 gp."));
                            break;
                        }
                    case 6:
                        {
                            from.CloseGump( typeof( TarotGump ) );
                            from.SendGump( new TarotGump( 0x45B ) );
                            if (m_IsNoisy)
                            {
                                from.PlaySound(from.Female ? 811 : 1085);//Ooo
                            }
                            this.PublicOverheadMessage(MessageType.Regular, 0, false, string.Format("{0} pulls 'The Lovers'", from.Name));
                            this.PublicOverheadMessage(MessageType.Regular, 0, false, string.Format("You will split the winning pot, unless you pull Death."));
                            break;
                        }
                    case 7:
                        {
                            from.CloseGump( typeof( TarotGump ) );
                            from.SendGump( new TarotGump( 0x461 ) );
                            if (m_IsNoisy)
                            {
                                from.PlaySound(from.Female ? 0x338 : 0x44A);//disgusted noise
                            }
                            this.PublicOverheadMessage(MessageType.Regular, 0, false, string.Format("{0} pulls 'Temperance'", from.Name));
                            this.PublicOverheadMessage(MessageType.Regular, 0, false, string.Format("{0} must now put up half the amount of the current pot!", from.Name));
                            break;
                        }
                    case 8:
                        {
                            from.CloseGump( typeof( TarotGump ) );
                            from.SendGump( new TarotGump( 0x45A ) );
                            if (m_IsNoisy)
                            {
                                from.PlaySound(from.Female ? 816 : 1090); //sigh...
                            }
                            this.PublicOverheadMessage(MessageType.Regular, 0, false, string.Format("{0} pulls 'Justice'", from.Name));
                            this.PublicOverheadMessage(MessageType.Regular, 0, false, string.Format("Skip your next turn."));
                            break;
                        }
                    case 9:
                        {
                            from.CloseGump( typeof( TarotGump ) );
                            from.SendGump( new TarotGump( 0x456 ) );
                            this.PublicOverheadMessage(MessageType.Regular, 0, false, string.Format("{0} pulls 'The Hermit'", from.Name));
                            this.PublicOverheadMessage(MessageType.Regular, 0, false, string.Format("Bet 5 Gp."));
                            break;
                        }
                    case 10:
                        {
                            from.CloseGump( typeof( TarotGump ) );
                            from.SendGump( new TarotGump( 0x463 ) );
                            if (m_IsNoisy)
                            {
                                from.PlaySound(from.Female ? 778 : 1049); //ah!
                            }
                            this.PublicOverheadMessage(MessageType.Regular, 0, false, string.Format("{0} pulls 'The Wheel of Fortune'", from.Name));
                            this.PublicOverheadMessage(MessageType.Regular, 0, false, string.Format("{0} may now take one-tenth of the pot!", from.Name));
                            break;
                        }
                    case 11:
                        {
                            from.CloseGump( typeof( TarotGump ) );
                            from.SendGump( new TarotGump( 0x45F ) );
                            if (m_IsNoisy)
                            {
                                from.PlaySound(from.Female ? 794 : 1066); //giggle
                            }
                            this.PublicOverheadMessage(MessageType.Regular, 0, false, string.Format("{0} pulls 'Strength'", from.Name));
                            this.PublicOverheadMessage(MessageType.Regular, 0, false, string.Format("All players BUT {0} must bet 60.", from.Name));
                            break;
                        }
                    case 12:
                        {
                            from.CloseGump( typeof( TarotGump ) );
                            from.SendGump( new TarotGump( 0x44F ) );
                            this.PublicOverheadMessage(MessageType.Regular, 0, false, string.Format("{0} pulls 'The Chariot'", from.Name));
                            this.PublicOverheadMessage(MessageType.Regular, 0, false, string.Format("Put in double the amount of the last bet!"));
                            break;
                        }
                    case 13:
                        {
                            from.CloseGump( typeof( TarotGump ) );
                            from.SendGump( new TarotGump( 0x450 ) );
                            if (m_IsNoisy)
                            {
                                from.PlaySound(Utility.Random(from.Female ? 0x314 : 0x423, from.Female ? 4 : 5)); //death sounds
                            }
                            this.PublicOverheadMessage(MessageType.Regular, 0, false, string.Format("{0} pulls 'Death'", from.Name));
                            this.PublicOverheadMessage(MessageType.Regular, 0, false, string.Format("You lose! Leave the game now!"));
                            break;
                        }
                    case 14:
                        {
                            from.CloseGump( typeof( TarotGump ) );
                            from.SendGump( new TarotGump( 0x455 ) );
                            if (m_IsNoisy)
                            {
                                from.PlaySound(from.Female ? 793 : 1065); //gasp!
                            }
                            this.PublicOverheadMessage(MessageType.Regular, 0, false, string.Format("{0} pulls 'Hanged Man'", from.Name));
                            this.PublicOverheadMessage(MessageType.Regular, 0, false, string.Format("{0}'s bet must match the amount of the current pot!", from.Name));
                            break;
                        }
                    case 15:
                        {
                            from.CloseGump( typeof( TarotGump ) );
                            from.SendGump( new TarotGump( 0x451 ) );
                            this.PublicOverheadMessage(MessageType.Regular, 0, false, string.Format("{0} pulls 'The Devil'", from.Name));
                            this.PublicOverheadMessage(MessageType.Regular, 0, false, string.Format("Everyone bets 100."));
                            break;
                        }
                    case 16:
                        {
                            from.CloseGump( typeof( TarotGump ) );
                            from.SendGump( new TarotGump( 0x462 ) );
                            this.PublicOverheadMessage(MessageType.Regular, 0, false, string.Format("{0} pulls 'The Tower'", from.Name));
                            this.PublicOverheadMessage(MessageType.Regular, 0, false, string.Format("The player across from {0} must bet 50.", from.Name));
                            break;
                        }
                    case 17:
                        {
                            from.CloseGump( typeof( TarotGump ) );
                            from.SendGump( new TarotGump( 0x45E ) );
                            this.PublicOverheadMessage(MessageType.Regular, 0, false, string.Format("{0} pulls 'The Star'", from.Name));
                            this.PublicOverheadMessage(MessageType.Regular, 0, false, string.Format("Everyone bets 40."));
                            break;
                        }
                    case 18:
                        {
                            from.CloseGump( typeof( TarotGump ) );
                            from.SendGump( new TarotGump( 0x45D ) );
                            this.PublicOverheadMessage(MessageType.Regular, 0, false, string.Format("{0} pulls 'The Moon'", from.Name));
                            this.PublicOverheadMessage(MessageType.Regular, 0, false, string.Format("Everyone bets 30."));
                            break;
                        }
                    case 19:
                        {
                            from.CloseGump( typeof( TarotGump ) );
                            from.SendGump( new TarotGump( 0x460 ) );
                            this.PublicOverheadMessage(MessageType.Regular, 0, false, string.Format("{0} pulls 'The Sun'", from.Name));
                            this.PublicOverheadMessage(MessageType.Regular, 0, false, string.Format("Everyone bets 50."));
                            break;
                        }
                    case 20:
                        {
                            from.CloseGump( typeof( TarotGump ) );
                            from.SendGump( new TarotGump( 0x459 ) );
                            if (m_IsNoisy)
                            {
                                from.PlaySound(from.Female ? 783 : 1054);// Woo-hoo!
                            }
                            this.PublicOverheadMessage(MessageType.Regular, 0, false, string.Format("{0} pulls 'Judgement'", from.Name));
                            this.PublicOverheadMessage(MessageType.Regular, 0, false, string.Format("{0} wins the game and takes the pot!", from.Name));
                            break;
                        }
                    case 21:
                        {
                            from.CloseGump( typeof( TarotGump ) );
                            from.SendGump( new TarotGump( 0x464 ) );
                            this.PublicOverheadMessage(MessageType.Regular, 0, false, string.Format("{0} pulls 'The World'", from.Name));
                            this.PublicOverheadMessage(MessageType.Regular, 0, false, string.Format("Everyone bets 200."));
                            break;
                        }
                }
        }

        private class TarotGump : Gump
        {
            public TarotGump( int card ) : base(0, 0)
            {
				this.Closable=true;
				this.Disposable=true;
				this.Dragable=true;
				this.Resizable=false;
				AddPage(0);
				AddImage(50, 50, 1102);
				AddImage(52, 52, card);
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
            writer.Write((bool)m_IsNoisy);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();

            switch (version)
            {
                case 0:
				{
					m_IsNoisy = reader.ReadBool();
					break;
				}
            }
        }
    }
}
