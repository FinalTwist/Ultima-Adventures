using System;
using Server;

namespace Server.Items
{
	public class SpecialSeaweed : Item
	{
		public int SkillNeeded;
		
		[CommandProperty(AccessLevel.Owner)]
		public int Skill_Needed { get { return SkillNeeded; } set { SkillNeeded = value; InvalidateProperties(); } }

		[Constructable]
		public SpecialSeaweed() : this( 1 )
		{
		}

		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		[Constructable]
		public SpecialSeaweed( int amount ) : base( 0x0A96 )
		{
			switch( Utility.Random( 31 ) )
			{
				case 0 : this.Hue = 1109; this.Name = "Seaweed of Nightsight"; SkillNeeded = 50; break;
				case 1 : this.Hue = 45; this.Name = "Seaweed of Lesser Cure"; SkillNeeded = 50; break;
				case 2 : this.Hue = 45; this.Name = "Seaweed of Cure"; SkillNeeded = 60; break;
				case 3 : this.Hue = 45; this.Name = "Seaweed of Greater Cure"; SkillNeeded = 80; break;
				case 4 : this.Hue = 396; this.Name = "Seaweed of Agility"; SkillNeeded = 60; break;
				case 5 : this.Hue = 396; this.Name = "Seaweed of Greater Agility"; SkillNeeded = 80; break;
				case 6 : this.Hue = 1001; this.Name = "Seaweed of Strength"; SkillNeeded = 60; break;
				case 7 : this.Hue = 1001; this.Name = "Seaweed of Greater Strength"; SkillNeeded = 80; break;
				case 8 : this.Hue = 73; this.Name = "Seaweed of Lesser Poison"; SkillNeeded = 50; break;
				case 9 : this.Hue = 73; this.Name = "Seaweed of Poison"; SkillNeeded = 60; break;
				case 10 : this.Hue = 73; this.Name = "Seaweed of Greater Poison"; SkillNeeded = 70; break;
				case 11 : this.Hue = 73; this.Name = "Seaweed of Deadly Poison"; SkillNeeded = 80; break;
				case 12 : this.Hue = 73; this.Name = "Seaweed of Lethal Poison"; SkillNeeded = 90; break;
				case 13 : this.Hue = 140; this.Name = "Seaweed of Refresh"; SkillNeeded = 60; break;
				case 14 : this.Hue = 140; this.Name = "Seaweed of Total Refresh"; SkillNeeded = 80; break;
				case 15 : this.Hue = 50; this.Name = "Seaweed of Lesser Heal"; SkillNeeded = 50; break;
				case 16 : this.Hue = 50; this.Name = "Seaweed of Heal"; SkillNeeded = 60; break;
				case 17 : this.Hue = 50; this.Name = "Seaweed of Greater Heal"; SkillNeeded = 80; break;
				case 18 : this.Hue = 425; this.Name = "Seaweed of Lesser Explosion"; SkillNeeded = 50; break;
				case 19 : this.Hue = 425; this.Name = "Seaweed of Explosion"; SkillNeeded = 60; break;
				case 20 : this.Hue = 425; this.Name = "Seaweed of Greater Explosion"; SkillNeeded = 80; break;
				case 21 : this.Hue = 0x490; this.Name = "Seaweed of Lesser Invisibility"; SkillNeeded = 50; break;
				case 22 : this.Hue = 0x490; this.Name = "Seaweed of Invisibility"; SkillNeeded = 60; break;
				case 23 : this.Hue = 0x490; this.Name = "Seaweed of Greater Invisibility"; SkillNeeded = 80; break;
				case 24 : this.Hue = 0x48E; this.Name = "Seaweed of Lesser Rejuvenate"; SkillNeeded = 50; break;
				case 25 : this.Hue = 0x48E; this.Name = "Seaweed of Rejuvenate"; SkillNeeded = 60; break;
				case 26 : this.Hue = 0x48E; this.Name = "Seaweed of Greater Rejuvenate"; SkillNeeded = 80; break;
				case 27 : this.Hue = 0x48D; this.Name = "Seaweed of Lesser Mana"; SkillNeeded = 50; break;
				case 28 : this.Hue = 0x48D; this.Name = "Seaweed of Mana"; SkillNeeded = 60; break;
				case 29 : this.Hue = 0x48D; this.Name = "Seaweed of Greater Mana"; SkillNeeded = 80; break;
				case 30 : this.Hue = 0x496; this.Name = "Seaweed of Invulnerability"; SkillNeeded = 95; break;
			}

			Stackable = true;
			Amount = amount;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( from.CheckSkill( SkillName.Fishing, SkillNeeded, 125 ) )
			{
				if (!from.Backpack.ConsumeTotal(typeof(Bottle), 1))
				{
					from.SendMessage("You need an empty bottle to drain the fluid from the seaweed.");
					return;
				}
				else
				{
					from.PlaySound( 0x240 );

					if ( this.Name == "Seaweed of Nightsight" ) { from.AddToBackpack( new NightSightPotion() ); }
					else if ( this.Name == "Seaweed of Lesser Cure" ) { from.AddToBackpack( new LesserCurePotion() ); }
					else if ( this.Name == "Seaweed of Cure" ) { from.AddToBackpack( new CurePotion() ); }
					else if ( this.Name == "Seaweed of Greater Cure" ) { from.AddToBackpack( new GreaterCurePotion() ); }
					else if ( this.Name == "Seaweed of Agility" ) { from.AddToBackpack( new AgilityPotion() ); }
					else if ( this.Name == "Seaweed of Greater Agility" ) { from.AddToBackpack( new GreaterAgilityPotion() ); }
					else if ( this.Name == "Seaweed of Strength" ) { from.AddToBackpack( new StrengthPotion() ); }
					else if ( this.Name == "Seaweed of Greater Strength" ) { from.AddToBackpack( new GreaterStrengthPotion() ); }
					else if ( this.Name == "Seaweed of Lesser Poison" ) { from.AddToBackpack( new LesserPoisonPotion() ); }
					else if ( this.Name == "Seaweed of Poison" ) { from.AddToBackpack( new PoisonPotion() ); }
					else if ( this.Name == "Seaweed of Greater Poison" ) { from.AddToBackpack( new GreaterPoisonPotion() ); }
					else if ( this.Name == "Seaweed of Deadly Poison" ) { from.AddToBackpack( new DeadlyPoisonPotion() ); }
					else if ( this.Name == "Seaweed of Lethal Poison" ) { from.AddToBackpack( new LethalPoisonPotion() ); }
					else if ( this.Name == "Seaweed of Refresh" ) { from.AddToBackpack( new RefreshPotion() ); }
					else if ( this.Name == "Seaweed of Total Refresh" ) { from.AddToBackpack( new TotalRefreshPotion() ); }
					else if ( this.Name == "Seaweed of Lesser Heal" ) { from.AddToBackpack( new LesserHealPotion() ); }
					else if ( this.Name == "Seaweed of Heal" ) { from.AddToBackpack( new HealPotion() ); }
					else if ( this.Name == "Seaweed of Greater Heal" ) { from.AddToBackpack( new GreaterHealPotion() ); }
					else if ( this.Name == "Seaweed of Lesser Explosion" ) { from.AddToBackpack( new LesserExplosionPotion() ); }
					else if ( this.Name == "Seaweed of Explosion" ) { from.AddToBackpack( new ExplosionPotion() ); }
					else if ( this.Name == "Seaweed of Greater Explosion" ) { from.AddToBackpack( new GreaterExplosionPotion() ); }
					else if ( this.Name == "Seaweed of Lesser Invisibility" ) { from.AddToBackpack( new LesserInvisibilityPotion() ); }
					else if ( this.Name == "Seaweed of Invisibility" ) { from.AddToBackpack( new InvisibilityPotion() ); }
					else if ( this.Name == "Seaweed of Greater Invisibility" ) { from.AddToBackpack( new GreaterInvisibilityPotion() ); }
					else if ( this.Name == "Seaweed of Lesser Rejuvenate" ) { from.AddToBackpack( new LesserRejuvenatePotion() ); }
					else if ( this.Name == "Seaweed of Rejuvenate" ) { from.AddToBackpack( new RejuvenatePotion() ); }
					else if ( this.Name == "Seaweed of Greater Rejuvenate" ) { from.AddToBackpack( new GreaterRejuvenatePotion() ); }
					else if ( this.Name == "Seaweed of Lesser Mana" ) { from.AddToBackpack( new LesserManaPotion() ); }
					else if ( this.Name == "Seaweed of Mana" ) { from.AddToBackpack( new ManaPotion() ); }
					else if ( this.Name == "Seaweed of Greater Mana" ) { from.AddToBackpack( new GreaterManaPotion() ); }
					else if ( this.Name == "Seaweed of Invulnerability" ) { from.AddToBackpack( new InvulnerabilityPotion() ); }

					from.SendMessage("You squeeze the fluid into the bottle.");
					this.Consume();

					return;
				}
			}
			else
			{
				from.SendMessage("You fail to get any fluid from the seaweed.");
				this.Consume();
				return;
			}
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Squeeze To Attempt To Extract Fluid");
			list.Add( 1049644, "Need An Empty Bottle"); // PARENTHESIS
        }

		public SpecialSeaweed( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
            writer.Write( SkillNeeded );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
            SkillNeeded = reader.ReadInt();
			ItemID = 0x0A96;
		}
	}
}