using System;
using Server;
using Server.Mobiles;

namespace Server.Items
{
    public class CanopicJar : Item
	{
		public string JarOwner;

		[CommandProperty(AccessLevel.Owner)]
		public string Jar_Owner { get { return JarOwner; } set { JarOwner = value; InvalidateProperties(); } }

		public string JarContents;

		[CommandProperty(AccessLevel.Owner)]
		public string Jar_Contents { get { return JarContents; } set { JarContents = value; InvalidateProperties(); } }

		public string JarPotion;

		[CommandProperty(AccessLevel.Owner)]
		public string Jar_Potion { get { return JarPotion; } set { JarPotion = value; InvalidateProperties(); } }

        [Constructable]
        public CanopicJar() : base(0x2FEE)
		{
			ItemID = Utility.RandomList( 0x2FEE, 0x2FEF, 0x2FF0, 0x2FF1 );
			Weight = 20.0;

			if ( Weight < 30.0 )
			{
				Weight = 30.0;

				string who = NameList.RandomName( "drakkul" );
				string title = "Pharaoh";
				string era = "First";
				string body = "Heart";

				switch ( Utility.RandomMinMax( 0, 4 ) )
				{
					case 0: title = "Pharaoh"; break;
					case 1: title = "King"; break;
					case 2: title = "Queen"; break;
					case 3: title = "Priest"; break;
					case 4: title = "Priestess"; break;
				}

				switch ( Utility.RandomMinMax( 0, 12 ) )
				{
					case 0: era = "first"; break;
					case 1: era = "second"; break;
					case 2: era = "third"; break;
					case 3: era = "fourth"; break;
					case 4: era = "fifth"; break;
					case 5: era = "sixth"; break;
					case 6: era = "seventh"; break;
					case 7: era = "eighth"; break;
					case 8: era = "ninth"; break;
					case 9: era = "tenth"; break;
					case 10: era = "eleventh"; break;
					case 11: era = "twelfth"; break;
					case 12: era = "thirteenth"; break;
				}

				switch ( Utility.RandomMinMax( 0, 5 ) )
				{
					case 0: body = "heart"; break;
					case 1: body = "brain"; break;
					case 2: body = "stomach"; break;
					case 3: body = "liver"; break;
					case 4: body = "intestines"; break;
					case 5: body = "lungs"; break;
				}

				Name = "canopic jar from the " + era + " dynasty";
				JarContents = "contains the " + body + " of " + who + " the " + title;
				JarOwner = who + " the " + title;

				switch ( Utility.RandomMinMax( 1, 31 ) )
				{
					case 1: JarPotion = "Nightsight Potions"; break;
					case 2: JarPotion = "Lesser Cure Potions"; break;
					case 3: JarPotion = "Cure Potions"; break;
					case 4: JarPotion = "Greater Cure Potions"; break;
					case 5: JarPotion = "Agility Potions"; break;
					case 6: JarPotion = "Greater Agility Potions"; break;
					case 7: JarPotion = "Strength Potions"; break;
					case 8: JarPotion = "Greater Strength Potions"; break;
					case 9: JarPotion = "Lesser Poison Potions"; break;
					case 10: JarPotion = "Poison Potions"; break;
					case 11: JarPotion = "Greater Poison Potions"; break;
					case 12: JarPotion = "Deadly Poison Potions"; break;
					case 13: JarPotion = "Refresh Potions"; break;
					case 14: JarPotion = "Total Refresh Potions"; break;
					case 15: JarPotion = "Lesser Heal Potions"; break;
					case 16: JarPotion = "Heal Potions"; break;
					case 17: JarPotion = "Greater Heal Potions"; break;
					case 18: JarPotion = "Lesser Explosion Potions"; break;
					case 19: JarPotion = "Explosion Potions"; break;
					case 20: JarPotion = "Greater Explosion Potions"; break;
					case 21: JarPotion = "Lesser Invisibility Potions"; break;
					case 22: JarPotion = "Invisibility Potions"; break;
					case 23: JarPotion = "Greater Invisibility Potions"; break;
					case 24: JarPotion = "Lesser Rejuvenate Potions"; break;
					case 25: JarPotion = "Rejuvenate Potions"; break;
					case 26: JarPotion = "Greater Rejuvenate Potions"; break;
					case 27: JarPotion = "Lesser Mana Potions"; break;
					case 28: JarPotion = "Mana Potions"; break;
					case 29: JarPotion = "Greater Mana Potions"; break;
					case 30: JarPotion = "Lethal Poison Potions"; break;
					case 31: JarPotion = "Invulnerability Potions"; break;
				}
			}
        }

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, JarContents);
			list.Add( 1049644, "Can Make A Keg Of " + JarPotion);
        } 

        public override void OnDoubleClick(Mobile from)
		{
			if ( !IsChildOf( from.Backpack ) ) 
			{
				from.SendMessage( "This must be in your backpack to use." );
				return;
			}
			else
			{
				Container pack = from.Backpack;
				int alchemists = 0;

				PotionKeg keg = new PotionKeg();
					keg.Delete();

				int barrel = 0;

				foreach( Item i in from.Backpack.Items )
				{
					if ( barrel == 0 && i is PotionKeg )
					{
						PotionKeg p = (PotionKeg)i;
						if ( p.Held < 1 ){ barrel = 1; keg = p; }
					}
				}

				foreach ( Mobile m in this.GetMobilesInRange( 10 ) )
				{
					if ( m is AlchemistGuildmaster || m is Alchemist )
						++alchemists;
				}

				if ( from.Skills[SkillName.Alchemy].Value < 50 )
				{
					from.SendMessage( "You should be at least an apprentice alchemist to try this!" );
				}
				else if ( alchemists == 0 )
				{
					from.SendMessage( "You need to be near a local alchemist for their help!" );
				}
				else if ( barrel == 0 )
				{
					from.SendMessage( "You need an empty potion keg to dump this in." );
				}
				else
				{
                    if (pack.ConsumeTotal(typeof(Gold), 500))
                    {
                        from.SendMessage("You give the alchemist 500 gold for their help.");
                        Effects.PlaySound(from.Location, from.Map, 0x026);

						if ( JarPotion == "Nightsight Potions" ){ keg.Type = PotionEffect.Nightsight; }
						else if ( JarPotion == "Lesser Cure Potions" ){ keg.Type = PotionEffect.CureLesser; }
						else if ( JarPotion == "Cure Potions" ){ keg.Type = PotionEffect.Cure; }
						else if ( JarPotion == "Greater Cure Potions" ){ keg.Type = PotionEffect.CureGreater; }
						else if ( JarPotion == "Agility Potions" ){ keg.Type = PotionEffect.Agility; }
						else if ( JarPotion == "Greater Agility Potions" ){ keg.Type = PotionEffect.AgilityGreater; }
						else if ( JarPotion == "Strength Potions" ){ keg.Type = PotionEffect.Strength; }
						else if ( JarPotion == "Greater Strength Potions" ){ keg.Type = PotionEffect.StrengthGreater; }
						else if ( JarPotion == "Lesser Poison Potions" ){ keg.Type = PotionEffect.PoisonLesser; }
						else if ( JarPotion == "Poison Potions" ){ keg.Type = PotionEffect.Poison; }
						else if ( JarPotion == "Greater Poison Potions" ){ keg.Type = PotionEffect.PoisonGreater; }
						else if ( JarPotion == "Deadly Poison Potions" ){ keg.Type = PotionEffect.PoisonDeadly; }
						else if ( JarPotion == "Refresh Potions" ){ keg.Type = PotionEffect.Refresh; }
						else if ( JarPotion == "Total Refresh Potions" ){ keg.Type = PotionEffect.RefreshTotal; }
						else if ( JarPotion == "Lesser Heal Potions" ){ keg.Type = PotionEffect.HealLesser; }
						else if ( JarPotion == "Heal Potions" ){ keg.Type = PotionEffect.Heal; }
						else if ( JarPotion == "Greater Heal Potions" ){ keg.Type = PotionEffect.HealGreater; }
						else if ( JarPotion == "Lesser Explosion Potions" ){ keg.Type = PotionEffect.ExplosionLesser; }
						else if ( JarPotion == "Explosion Potions" ){ keg.Type = PotionEffect.Explosion; }
						else if ( JarPotion == "Greater Explosion Potions" ){ keg.Type = PotionEffect.ExplosionGreater; }
						else if ( JarPotion == "Lesser Invisibility Potions" ){ keg.Type = PotionEffect.InvisibilityLesser; }
						else if ( JarPotion == "Invisibility Potions" ){ keg.Type = PotionEffect.Invisibility; }
						else if ( JarPotion == "Greater Invisibility Potions" ){ keg.Type = PotionEffect.InvisibilityGreater; }
						else if ( JarPotion == "Lesser Rejuvenate Potions" ){ keg.Type = PotionEffect.RejuvenateLesser; }
						else if ( JarPotion == "Rejuvenate Potions" ){ keg.Type = PotionEffect.Rejuvenate; }
						else if ( JarPotion == "Greater Rejuvenate Potions" ){ keg.Type = PotionEffect.RejuvenateGreater; }
						else if ( JarPotion == "Lesser Mana Potions" ){ keg.Type = PotionEffect.ManaLesser; }
						else if ( JarPotion == "Mana Potions" ){ keg.Type = PotionEffect.Mana; }
						else if ( JarPotion == "Greater Mana Potions" ){ keg.Type = PotionEffect.ManaGreater; }
						else if ( JarPotion == "Lethal Poison Potions" ){ keg.Type = PotionEffect.PoisonLethal; }
						else { keg.Type = PotionEffect.Invulnerability; }

						int holdBonus = (int)(from.Skills[SkillName.Alchemy].Value / 2);
						int fill = Utility.RandomMinMax( 5, 50 ) + holdBonus;
							if ( fill > 100 ){ fill = 100; }

						keg.Held = fill;
						Item b = (Item)keg;
						Server.Items.PotionKeg.SetColorKeg( b, b );

						EmptyCanopicJar jar = new EmptyCanopicJar();
						jar.ItemID = this.ItemID;
						jar.Name = this.Name;
						jar.RelicOwner = "belonged to " + JarOwner;
						from.AddToBackpack( jar );
						this.Delete();
                    }
                    else
                    {
                        from.SendMessage("You will need at least 500 gold in your pack to pay the alchemist.");
                    }
				}
			}
        }

        public CanopicJar( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
            writer.Write( JarOwner );
            writer.Write( JarContents );
            writer.Write( JarPotion );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
            JarOwner = reader.ReadString();
			JarContents = reader.ReadString();
            JarPotion = reader.ReadString();
	    }
    }
}