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

namespace Server.Mobiles
{
	public class Alchemist : BaseVendor
	{
		private List<SBInfo> m_SBInfos = new List<SBInfo>();
		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } }

		public override NpcGuild NpcGuild{ get{ return NpcGuild.AlchemistsGuild; } }

		[Constructable]
		public Alchemist() : base( "the alchemist" )
		{
			SetSkill( SkillName.Alchemy, 85.0, 100.0 );
			SetSkill( SkillName.TasteID, 65.0, 88.0 );
			SetSkill( SkillName.Poisoning, 65.0, 88.0 );
			Job = JobFragment.alchemist;
			Karma = Utility.RandomMinMax( 13, -45 );
		}

		public override void InitSBInfo()
		{
			m_SBInfos.Add( new SBAlchemist() ); 
			m_SBInfos.Add( new SBMixologist() ); 
			m_SBInfos.Add( new SBBuyArtifacts() ); 

			if ( Region.IsPartOf( "the Enchanted Pass" ) )
				m_SBInfos.Add( new SBGodlyBrewing() );
		}

		public override VendorShoeType ShoeType
		{
			get{ return Utility.RandomBool() ? VendorShoeType.Shoes : VendorShoeType.Sandals; }
		}

		public override void InitOutfit()
		{
			base.InitOutfit();

			AddItem( new Server.Items.Robe( Utility.RandomPinkHue() ) );
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
						mobile.SendGump(new SpeechGump( "A Sip And A Taste", SpeechFunctions.SpeechText( m_Giver.Name, m_Mobile.Name, "Alchemist" ) ));
					}
				}
            }
        }
		///////////////////////////////////////////////////////////////////////////

		private class FixEntry : ContextMenuEntry
		{
			private Alchemist m_Alchemist;
			private Mobile m_From;

			public FixEntry( Alchemist Alchemist, Mobile from ) : base( 6120, 12 )
			{
				m_Alchemist = Alchemist;
				m_From = from;
			}

			public override void OnClick()
			{
				m_Alchemist.BeginRepair( m_From );
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

			int nCost = 40;
			int nCost2 = 1000;

			if ( BeggingPose(from) > 0 ) // LET US SEE IF THEY ARE BEGGING - WIZARD
			{
				nCost = nCost - (int)( ( from.Skills[SkillName.Begging].Value * 0.005 ) * nCost ); if ( nCost < 1 ){ nCost = 1; }
				nCost2 = nCost2 - (int)( ( from.Skills[SkillName.Begging].Value * 0.005 ) * nCost2 );
				SayTo(from, "Since you are begging, I will ask for less to identify a liquid, bottles cost " + nCost.ToString() + " gold and kegs cost " + nCost2.ToString() + " gold.");
			}
			else { SayTo(from, "If you want me to identify an unknown liquid, bottles cost " + nCost.ToString() + " gold and kegs cost " + nCost2.ToString() + " gold."); }

            from.Target = new RepairTarget(this);
        }

        private class RepairTarget : Target
        {
            private Alchemist m_Alchemist;

            public RepairTarget(Alchemist mage) : base(12, false, TargetFlags.None)
            {
                m_Alchemist = mage;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
				int nCost = 40;
				int nCost2 = 1000;

				if ( BeggingPose(from) > 0 ) // LET US SEE IF THEY ARE BEGGING - WIZARD
				{
					nCost = nCost - (int)( ( from.Skills[SkillName.Begging].Value * 0.005 ) * nCost ); if ( nCost < 1 ){ nCost = 1; }
					nCost2 = nCost2 - (int)( ( from.Skills[SkillName.Begging].Value * 0.005 ) * nCost2 );
				}

                if (targeted is UnknownLiquid && from.Backpack != null)
                {
					Item brew = targeted as Item;
                    Container pack = from.Backpack;
                    int toConsume = nCost;

                    if (pack.ConsumeTotal(typeof(Gold), toConsume))
                    {
						if ( BeggingPose(from) > 0 ){ Titles.AwardKarma( from, -BeggingKarma( from ), true ); } // DO ANY KARMA LOSS
                        from.SendMessage(String.Format("You pay {0} gold.", toConsume));

						m_Alchemist.Animate( 34, 5, 1, true, false, 0 );
						m_Alchemist.PlaySound( 0x2D6 );

						int potionType = Utility.RandomMinMax( 0, 40 );
						string potionName = "";

						if ( potionType == 0 ){ from.AddToBackpack( new NightSightPotion() ); potionName = "night sight potion"; }
						else if ( potionType == 1 ){ from.AddToBackpack( new LesserCurePotion() ); potionName = "lesser cure potion"; }
						else if ( potionType == 2 ){ from.AddToBackpack( new CurePotion() ); potionName = "cure potion"; }
						else if ( potionType == 3 ){ from.AddToBackpack( new GreaterCurePotion() ); potionName = "greater cure potion"; }
						else if ( potionType == 4 ){ from.AddToBackpack( new AgilityPotion() ); potionName = "agility potion"; }
						else if ( potionType == 5 ){ from.AddToBackpack( new GreaterAgilityPotion() ); potionName = "greater agility potion"; }
						else if ( potionType == 6 ){ from.AddToBackpack( new StrengthPotion() ); potionName = "strength"; }
						else if ( potionType == 7 ){ from.AddToBackpack( new GreaterStrengthPotion() ); potionName = "greater strength potion"; }
						else if ( potionType == 8 ){ from.AddToBackpack( new LesserPoisonPotion() ); potionName = "lesser poison"; }
						else if ( potionType == 9 ){ from.AddToBackpack( new PoisonPotion() ); potionName = "poison"; }
						else if ( potionType == 10 ){ from.AddToBackpack( new GreaterPoisonPotion() ); potionName = "greater poison"; }
						else if ( potionType == 11 ){ from.AddToBackpack( new DeadlyPoisonPotion() ); potionName = "deadly poison"; }
						else if ( potionType == 12 ){ from.AddToBackpack( new RefreshPotion() ); potionName = "refresh potion"; }
						else if ( potionType == 13 ){ from.AddToBackpack( new TotalRefreshPotion() ); potionName = "total refresh potion"; }
						else if ( potionType == 14 ){ from.AddToBackpack( new LesserHealPotion() ); potionName = "lesser heal potion"; }
						else if ( potionType == 15 ){ from.AddToBackpack( new HealPotion() ); potionName = "heal potion"; }
						else if ( potionType == 16 ){ from.AddToBackpack( new GreaterHealPotion() ); potionName = "greater heal potion"; }
						else if ( potionType == 17 ){ from.AddToBackpack( new LesserExplosionPotion() ); potionName = "lesser explosion potion"; }
						else if ( potionType == 18 ){ from.AddToBackpack( new ExplosionPotion() ); potionName = "explosion potion"; }
						else if ( potionType == 19 ){ from.AddToBackpack( new GreaterExplosionPotion() ); potionName = "greater explosion potion"; }
						else if ( potionType == 20 ){ from.AddToBackpack( new LesserInvisibilityPotion() ); potionName = "lesser invisibility potion"; }
						else if ( potionType == 21 ){ from.AddToBackpack( new InvisibilityPotion() ); potionName = "invisibility potion"; }
						else if ( potionType == 22 ){ from.AddToBackpack( new GreaterInvisibilityPotion() ); potionName = "greater invisibility potion"; }
						else if ( potionType == 23 ){ from.AddToBackpack( new LesserRejuvenatePotion() ); potionName = "lesser rejuvenation potion"; }
						else if ( potionType == 24 ){ from.AddToBackpack( new RejuvenatePotion() ); potionName = "rejuvenation potion"; }
						else if ( potionType == 25 ){ from.AddToBackpack( new GreaterRejuvenatePotion() ); potionName = "greater rejuvenation potion"; }
						else if ( potionType == 26 ){ from.AddToBackpack( new LesserManaPotion() ); potionName = "lesser mana potion"; }
						else if ( potionType == 27 ){ from.AddToBackpack( new ManaPotion() ); potionName = "mana potion"; }
						else if ( potionType == 28 ){ from.AddToBackpack( new GreaterManaPotion() ); potionName = "greater mana potion"; }
						else if ( potionType == 29 ){ from.AddToBackpack( new InvulnerabilityPotion() ); potionName = "invulnerability potion"; }
						else if ( potionType == 30 ){ from.AddToBackpack( new AutoResPotion() ); potionName = "resurrection potion"; }
						else if ( potionType == 31 ){ from.AddToBackpack( new OilMetal() ); potionName = "metal enhancement oil"; }
						else if ( potionType == 32 ){ from.AddToBackpack( new OilLeather() ); potionName = "leather enhancement oil"; }
						else if ( potionType == 33 ){ from.AddToBackpack( new BottleOfAcid() ); potionName = "acid"; }
						else if ( potionType == 34 ){ from.AddToBackpack( new MagicalDyes() ); potionName = "magical dye"; }
						else if ( potionType == 35 ){ from.AddToBackpack( new BeverageBottle(BeverageType.Ale) ); potionName = "ale"; }
						else if ( potionType == 36 ){ from.AddToBackpack( new BeverageBottle(BeverageType.Wine) ); potionName = "wine"; }
						else if ( potionType == 37 ){ from.AddToBackpack( new BeverageBottle(BeverageType.Liquor) ); potionName = "liquor"; }
						else if ( potionType == 38 ){ from.AddToBackpack( new BeverageBottle(BeverageType.Ale) ); potionName = "ale"; }
						else if ( potionType == 39 ){ from.AddToBackpack( new BeverageBottle(BeverageType.Wine) ); potionName = "wine"; }
						else { from.AddToBackpack( new BeverageBottle(BeverageType.Liquor) ); potionName = "liquor"; }

                        m_Alchemist.SayTo(from, "This seems to be a bottle of " + potionName + ".");
						brew.Delete();
                    }
                    else
                    {
                        m_Alchemist.SayTo(from, "It would cost you {0} gold to have that identified.", toConsume);
                        from.SendMessage("You do not have enough gold.");
                    }
                }
                else if (targeted is UnknownKeg && from.Backpack != null)
                {
					Item brew = targeted as Item;
                    Container pack = from.Backpack;
                    int toConsume = nCost2;

                    if (pack.ConsumeTotal(typeof(Gold), toConsume))
                    {
						if ( BeggingPose(from) > 0 ){ Titles.AwardKarma( from, -BeggingKarma( from ), true ); } // DO ANY KARMA LOSS
                        from.SendMessage(String.Format("You pay {0} gold.", toConsume));

						m_Alchemist.Animate( 34, 5, 1, true, false, 0 );
						m_Alchemist.PlaySound( 0x2D6 );

						Item Kitem = new PotionKeg();
						PotionKeg barrel = (PotionKeg)Kitem;
						UnknownKeg tub = (UnknownKeg)brew;
						barrel.Held = tub.KegFilled;
						int nBarrel = 0;

						int potionType = Utility.RandomMinMax( 1, 36 );

						if ( potionType == 1 ){ barrel.Type = PotionEffect.Nightsight; }
						else if ( potionType == 2 ){ barrel.Type = PotionEffect.CureLesser; }
						else if ( potionType == 3 ){ barrel.Type = PotionEffect.Cure; }
						else if ( potionType == 4 ){ barrel.Type = PotionEffect.CureGreater; }
						else if ( potionType == 5 ){ barrel.Type = PotionEffect.Agility; }
						else if ( potionType == 6 ){ barrel.Type = PotionEffect.AgilityGreater; }
						else if ( potionType == 7 ){ barrel.Type = PotionEffect.Strength; }
						else if ( potionType == 8 ){ barrel.Type = PotionEffect.StrengthGreater; }
						else if ( potionType == 9 ){ barrel.Type = PotionEffect.PoisonLesser; }
						else if ( potionType == 10 ){ barrel.Type = PotionEffect.Poison; }
						else if ( potionType == 11 ){ barrel.Type = PotionEffect.PoisonGreater; }
						else if ( potionType == 12 ){ barrel.Type = PotionEffect.PoisonDeadly; }
						else if ( potionType == 13 ){ barrel.Type = PotionEffect.Refresh; }
						else if ( potionType == 14 ){ barrel.Type = PotionEffect.RefreshTotal; }
						else if ( potionType == 15 ){ barrel.Type = PotionEffect.HealLesser; }
						else if ( potionType == 16 ){ barrel.Type = PotionEffect.Heal; }
						else if ( potionType == 17 ){ barrel.Type = PotionEffect.HealGreater; }
						else if ( potionType == 18 ){ barrel.Type = PotionEffect.ExplosionLesser; }
						else if ( potionType == 19 ){ barrel.Type = PotionEffect.Explosion; }
						else if ( potionType == 20 ){ barrel.Type = PotionEffect.ExplosionGreater; }
						else if ( potionType == 21 ){ barrel.Type = PotionEffect.InvisibilityLesser; }
						else if ( potionType == 22 ){ barrel.Type = PotionEffect.Invisibility; }
						else if ( potionType == 23 ){ barrel.Type = PotionEffect.InvisibilityGreater; }
						else if ( potionType == 24 ){ barrel.Type = PotionEffect.RejuvenateLesser; }
						else if ( potionType == 25 ){ barrel.Type = PotionEffect.Rejuvenate; }
						else if ( potionType == 26 ){ barrel.Type = PotionEffect.RejuvenateGreater; }
						else if ( potionType == 27 ){ barrel.Type = PotionEffect.ManaLesser; }
						else if ( potionType == 28 ){ barrel.Type = PotionEffect.Mana; }
						else if ( potionType == 29 ){ barrel.Type = PotionEffect.ManaGreater; }
						else if ( potionType == 30 ){ barrel.Type = PotionEffect.PoisonLethal; }
						else if ( potionType == 31 ){ barrel.Type = PotionEffect.Invulnerability; }

						if ( potionType > 31 )
						{
							nBarrel = 1;
							from.AddToBackpack( new Keg() );
							Effects.PlaySound(from.Location, from.Map, 0x026);
							m_Alchemist.SayTo(from, "This seems to be barrel of dirty water, which I will dump out for you.");
						}

						if ( nBarrel == 0 )
						{
							Server.Items.PotionKeg.SetColorKeg( barrel, barrel );
							from.AddToBackpack( barrel );
							m_Alchemist.SayTo(from, "This seems to be a " + barrel.Name + ".");
						}

						brew.Delete();
                    }
                    else
                    {
                        m_Alchemist.SayTo(from, "It would cost you {0} gold to have that identified.", toConsume);
                        from.SendMessage("You do not have enough gold.");
                    }
                }
				else
				{
					m_Alchemist.SayTo(from, "That does not need my services.");
				}
            }
        }

		public Alchemist( Serial serial ) : base( serial )
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