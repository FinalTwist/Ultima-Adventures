using System;
using Server;
using Server.Spells;
using Server.Network;
using Server.Mobiles;
using Server.Items;
using Server.Misc;
using System.Collections.Generic;
using System.Collections;
using Server.Commands;
using Server.Commands.Generic;

namespace Server.Spells.Syth
{
	public abstract class SythSpell : Spell
	{
		public virtual int spellIndex { get { return 1; } }
		public abstract int RequiredTithing { get; }
		public abstract double RequiredSkill { get; }
		public abstract int RequiredMana{ get; }
		public override bool ClearHandsOnCast { get { return false; } }
		public override SkillName CastSkill { get { return SkillName.EvalInt; } }
		public override SkillName DamageSkill { get { return SkillName.Swords; } }
		public override int CastRecoveryBase { get { return 7; } }

		public SythSpell( Mobile caster, Item scroll, SpellInfo info ) : base( caster, scroll, info )
		{
		}

		public static string SpellInfo( int spellID, int slice )
		{
			string value = "";
			string name = "";
			string mantra = "";
			string map = "";
			string dungeon = "";
			string syth = "";
			string world = "";
			string scroll = "";
			string describe = "";
			string mana = "";
			string skill = "";
			string crystal = "";
			string icon = "";

			if ( spellID == 270 ){ name = "Psychokinesis"; icon="1038"; crystal = "6"; skill = "10"; mana = "5"; mantra = "Dzwol Hyal"; map = "Trammel"; dungeon = "the Fires of Hell"; world = "Land of Sosaria"; syth = "Prince Myrhal of Rax"; scroll = "SythDatacron01"; 
				describe = "Allows a Syth to use or move an object that is out of reach. Can be used to set off some Chest Traps from a safe distance as well."; }
			else if ( spellID == 271 ){ name = "Death Grip"; icon="1002"; crystal = "16"; skill = "20"; mana = "8"; mantra = "Zayin Kun"; map = "Trammel"; dungeon = "Dungeon Abandon"; world = "Land of Sosaria"; syth = "Lady Kath of Naelex"; scroll = "SythDatacron02"; 
				describe = "The Syth can reach out with their hatred and anger, psychically gripping a foe for 5-20 points of damage every few seconds. This effect lasts for 2-75 seconds. The more a Syth is skilled with swords and tactics, and their accumulated hatred and anger, increases the effectiveness of this power."; }
			else if ( spellID == 272 ){ name = "Projection"; icon="21287"; crystal = "24"; skill = "30"; mana = "12"; mantra = "Rhak Skuri"; map = "Trammel"; dungeon = "the Ancient Pyramid"; world = "Land of Sosaria"; syth = "Saint Kargoth"; scroll = "SythDatacron03"; 
				describe = "This power allows the Syth to create a projection of themselves that can distract foes from the Syth themselves. This projection contains physical attributes as the psychic energy of the Syth can maintain the illusion for up to about 3 minutes, depending on how powerful they are."; }
			else if ( spellID == 273 ){ name = "Throw Sword"; icon="1005"; crystal = "12"; skill = "40"; mana = "16"; mantra = "Chwit Sutta"; map = "Trammel"; dungeon = "Dungeon Exodus"; world = "Land of Sosaria"; syth = "Sir Maeril of Naelax"; scroll = "SythDatacron04"; 
				describe = "The Syth can throw their equipped sword at an enemy, doing not only the sword's normal damage but also an extra 17-53 points of damage. The types of resistances that the sword inflicts damage on match that of the sword. The sword quickly returns to the Syth's hand upon throwing it."; }
			else if ( spellID == 274 ){ name = "Speed"; icon="1043"; crystal = "80"; skill = "50"; mana = "20"; mantra = "Qyasik Tukata"; map = "Trammel"; dungeon = "Dungeon Clues"; world = "Land of Sosaria"; syth = "Lord Monduiz Dephaar"; scroll = "SythDatacron05"; 
				describe = "This increases the running speed of the Syth for about 10-25 minutes, making them run as fast as a stallion. This power cannot be called upon within certain areas and will often cease to function when entering those areas."; }
			else if ( spellID == 275 ){ name = "Syth Lightning"; icon="1010"; crystal = "32"; skill = "60"; mana = "24"; mantra = "Sutta Wo"; map = "Trammel"; dungeon = "the Mausoleum"; world = "Island of Umber Veil"; syth = "Lord Androma of Gara"; scroll = "SythDatacron06"; 
				describe = "The power of Syth lightning can be massive, and the stronger the Syth, the more enemies that will be struck by the power of this lightning. The damage dealt will be around 12-75 points of energy damage for each that are struck."; }
			else if ( spellID == 276 ){ name = "Absorption"; icon="23015"; crystal = "500"; skill = "70"; mana = "28"; mantra = "Taral Wai"; map = "Trammel"; dungeon = "the City of the Dead"; world = "Land of Ambrosia"; syth = "Sir Farian of Lirtham"; scroll = "SythDatacron07"; 
				describe = "This power gives the essence of the Syth the ability to absorb most damaging magery spells, and the Syth then redirects those spells back at the one who cast it. The amount that a Syth can absorb is dependent on their power."; }
			else if ( spellID == 277 ){ name = "Psychic Blast"; icon="23010"; crystal = "48"; skill = "80"; mana = "32"; mantra = "Wai Kusk"; map = "Felucca"; dungeon = "Dungeon Wrong"; world = "Land of Lodoria"; syth = "Lord Thyrian of Naelax"; scroll = "SythDatacron08"; 
				describe = "Summon your rage to perform a mental attack that deals an amount of energy damage based upon your power as a Syth. Elemental Resistances may reduce damage done by this attack. The damage dealt can be between 80-125 points."; }
			else if ( spellID == 278 ){ name = "Drain Life"; icon="1026"; crystal = "52"; skill = "90"; mana = "36"; mantra = "Derriphan Tyuk"; map = "Felucca"; dungeon = "the Lodoria Catacombs"; world = "Land of Lodoria"; syth = "Sir Minar of Darmen"; scroll = "SythDatacron09"; 
				describe = "This power will drain about 10-15 points of life, every few seconds, from a creature for 10-60 seconds, where such life is transferred to the Syth. This power cannot affect supernatural creatures, constructs, golems, or elementals."; }
			else if ( spellID == 279 ){ name = "Clone"; icon="2261"; crystal = "250"; skill = "100"; mana = "40"; mantra = "Itsu Sutta"; map = "Felucca"; dungeon = "Dungeon Deceit"; world = "Land of Lodoria"; syth = "Sir Rezinar of Haxx"; scroll = "SythDatacron10"; 
				describe = "This power allows the Syth to create a cloning crystal that can hold the Syth's genetic pattern. If the Syth meets an untimely end, the crystal will activate in 30 seconds and create a clone body that the Syth's soul can then occupy. These crystal are quite fragile so you would need to make sure the crystal did not crumble while you were resting for long periods of time."; }

			if ( slice == 1 ){ value = name; }
			else if ( slice == 2 ){ value = skill; }
			else if ( slice == 3 ){ value = mana; }
			else if ( slice == 4 ){ value = mantra; }
			else if ( slice == 5 ){ value = map; }
			else if ( slice == 6 ){ value = dungeon; }
			else if ( slice == 7 ){ value = world; }
			else if ( slice == 8 ){ value = syth; }
			else if ( slice == 9 ){ value = scroll; }
			else if ( slice == 10 ){ value = crystal; }
			else if ( slice == 11 ){ value = icon; }
			else { value = describe; }

			return value;
		}

		public static bool SythNotIllegal( Mobile from, bool checkSword )
		{
			bool HoldingSword = false;
			bool WearingCloth = false;

			if ( !checkSword )
			{
				HoldingSword = true;
			}
			else if ( from.FindItemOnLayer( Layer.OneHanded ) != null )
			{
				Item oneHand = from.FindItemOnLayer( Layer.OneHanded );
				if ( oneHand is BaseSword ){ HoldingSword = true; }
			}
			if ( from.FindItemOnLayer( Layer.TwoHanded ) != null )
			{
				Item twoHand = from.FindItemOnLayer( Layer.TwoHanded );
				if ( twoHand is BaseSword ){ HoldingSword = true; }
				else if ( twoHand is BaseShield && twoHand.Name != null && (twoHand.Name).Contains("Syth") ){ WearingCloth = true; }
			}

			if ( !HoldingSword ){ return false; }

			if ( from.FindItemOnLayer( Layer.Helm ) != null )
			{
				Item hat = from.FindItemOnLayer( Layer.Helm );
				if ( ( hat.ItemID == 0x4CDA || hat.ItemID == 0x4CDC || hat.ItemID == 0x2FBB ) && hat.Name != null && (hat.Name).Contains("Syth") )
					WearingCloth = true;
			}
			if ( from.FindItemOnLayer( Layer.OuterTorso ) != null )
			{
				Item robe = from.FindItemOnLayer( Layer.OuterTorso );
				if ( robe.Name != null && (robe.Name).Contains("Syth") )
					WearingCloth = true;
			}
			if ( from.FindItemOnLayer( Layer.Cloak ) != null )
			{
				Item cloak = from.FindItemOnLayer( Layer.Cloak );
				if ( cloak.Name != null && (cloak.Name).Contains("Syth") )
					WearingCloth = true;
			}
			if ( from.FindItemOnLayer( Layer.Talisman ) != null )
			{
				Item talisman = from.FindItemOnLayer( Layer.Talisman );
				if ( talisman.Name != null && (talisman.Name).Contains("Syth") && talisman.ItemID == 0x4CDE )
					WearingCloth = true;
			}

			if ( from.Skills[SkillName.EvalInt].Base < 10 || from.Skills[SkillName.Swords].Base < 10 || from.Skills[SkillName.Tactics].Base < 10 )
			{
				return false;
			}
			else if ( !WearingCloth )
			{
				return false;
			}

			return true;
		}

		public override bool CheckCast(Mobile caster)
		{
			if ( !base.CheckCast( caster ) )
				return false;

			if ( Caster.Karma > 0 )
			{
				Caster.SendMessage( "You have too much Karma to use this power." );
				return false;
			}
			else if ( GetSythSkillMax( Caster ) < RequiredSkill )
			{
				Caster.SendMessage( "You must have at least " + RequiredSkill + " Evaluate Intelligence and Swords to use this power." );
				return false;
			}
			else if ( GetCrystals( Caster ) < RequiredTithing )
			{
				Caster.SendMessage( "You must have at least " + RequiredTithing + " Crystals in your datacron to use this power." );
				return false;
			}
			else if ( Caster.Mana < GetMana() )
			{
				Caster.SendMessage( "You must have at least " + GetMana() + " Mana to use this power." );
				return false;
			}
			else if ( this is SythSpeed && Server.Misc.MyServerSettings.NoMountsInCertainRegions() && Server.Mobiles.AnimalTrainer.IsNoMountRegion( Region.Find( Caster.Location, Caster.Map ) ) )
			{
				Caster.SendMessage( "This power doesn't seem to work in this place." );
				return false;
			}

			return true;
		}

		public override bool CheckFizzle()
		{
			int requiredTithing = this.RequiredTithing;

			if ( AosAttributes.GetValue( Caster, AosAttribute.LowerRegCost ) > Utility.Random( 200 ) )
				requiredTithing = 0;

			int mana = GetMana();

			if ( Caster.Karma > 0 )
			{
				Caster.SendMessage( "You have too much Karma to use this power." );
				return false;
			}
			else if ( GetSythSkillMax( Caster ) < RequiredSkill )
			{
				Caster.SendMessage( "You must have at least " + RequiredSkill + " Evaluate Intelligence and Swords to use this power." );
				return false;
			}
			else if ( GetCrystals( Caster ) < requiredTithing )
			{
				Caster.SendMessage( "You must have at least " + requiredTithing + " Crystals in your datacron to use this power." );
				return false;
			}
			else if ( Caster.Mana < mana )
			{
				Caster.SendMessage( "You must have at least " + mana + " Mana to use this power." );
				return false;
			}
			else if ( this is SythSpeed && Server.Misc.MyServerSettings.NoMountsInCertainRegions() && Server.Mobiles.AnimalTrainer.IsNoMountRegion( Region.Find( Caster.Location, Caster.Map ) ) )
			{
				Caster.SendMessage( "This power doesn't seem to work in this place." );
				return false;
			}

			if ( requiredTithing > 0 ){ DrainCrystals( Caster, requiredTithing ); }

			return true;
		}

		public override void DoFizzle()
		{
			Caster.PrivateOverheadMessage(MessageType.Regular, 0x3B2, false, "You fail to invoke the power.", Caster.NetState);
			Caster.FixedParticles( 0x3735, 1, 30, 9503, EffectLayer.Waist );
			Caster.PlaySound( 0x19D );
			Caster.NextSpellTime = Core.TickCount;
		}

		public override int ComputeKarmaAward()
		{
			int circle = (int)(RequiredSkill / 10);
				if ( circle < 1 ){ circle = 1; }
			return -( 40 + ( 10 * circle ) );
		}

		public override int GetMana()
		{
			return ScaleMana( RequiredMana );
		}

		public override bool SayMantra()
		{
			return true;
		}

		public override void DoHurtFizzle()
		{
			Caster.PlaySound( 0x1F8 );
		}

		public override void OnDisturb( DisturbType type, bool message )
		{
			base.OnDisturb( type, message );

			if ( message )
				Caster.PlaySound( 0x1F8 );
		}

		public override void OnBeginCast()
		{
			base.OnBeginCast();

			Caster.FixedEffect( 0x37C4, 10, 42, 4, 3 );
		}

		public override void GetCastSkills( out double min, out double max )
		{
			min = RequiredSkill;
			max = RequiredSkill + 20.0;
		}

		public int ComputePowerValue( int div )
		{
			return ComputePowerValue( Caster, div );
		}

		public static int ComputePowerValue( Mobile from, int div )
		{
			if ( from == null )
				return 0;

			int v = (int) Math.Sqrt( ( from.Karma * -1 ) + 20000 + ( from.Skills.EvalInt.Fixed * 10 ) );

			return v / div;
		}

		public virtual bool CheckResisted( Mobile target )
		{
			double n = GetResistPercent( target );

			n /= 100.0;

			if( n <= 0.0 )
				return false;

			if( n >= 1.0 )
				return true;

			int maxSkill = (1 + 7) * 10;
			maxSkill += (1 + (7 / 6)) * 25;

			if( target.Skills[SkillName.MagicResist].Value < maxSkill )
				target.CheckSkill( SkillName.MagicResist, 0.0, 120.0 );

			return (n >= Utility.RandomDouble());
		}

		public virtual double GetResistPercentForCircle( Mobile target )
		{
			double firstPercent = target.Skills[SkillName.MagicResist].Value / 5.0;
			double secondPercent = target.Skills[SkillName.MagicResist].Value - (((Caster.Skills[CastSkill].Value - 20.0) / 5.0) + (1 + 7) * 5.0);

			return (firstPercent > secondPercent ? firstPercent : secondPercent) / 2.0; // Seems should be about half of what stratics says.
		}

		public virtual double GetResistPercent( Mobile target )
		{
			return GetResistPercentForCircle( target );
		}

		public static void DrainCrystals( Mobile from, int tithing )
		{
			ArrayList targets = new ArrayList();
			foreach ( Item item in World.Items.Values )
			{
				if ( item is SythSpellbook )
				{
					SythSpellbook book = (SythSpellbook)item;
					if ( book.owner == from )
					{
						book.crystals = book.crystals - tithing;
						if ( book.crystals < 1 ){ book.crystals = 0; }
						book.InvalidateProperties();
					}
				}
			}
		}

		public static int GetCrystals( Mobile from )
		{
			int crystal = 0;

			ArrayList targets = new ArrayList();
			foreach ( Item item in World.Items.Values )
			{
				if ( item is SythSpellbook )
				{
					SythSpellbook book = (SythSpellbook)item;
					if ( book.owner == from )
					{
						crystal = book.crystals;
					}
				}
			}

			return crystal;
		}

		public static double GetSythDamage( Mobile from )
		{
			int karma = ( from.Karma * -1 );
				if ( karma < 1 ){ karma = 0; }
				if ( karma > MyServerSettings.KarmaMax() ){ karma = MyServerSettings.KarmaMax(); }

			double hate = karma / 120; // MAX 125
			hate = hate + from.Skills[SkillName.Tactics].Value + from.Skills[SkillName.Swords].Value; // MAX 375

			return hate;
		}

		public static bool GetSythSkill( Mobile from, int skill )
		{
			if ( from.Skills[SkillName.Swords].Value < skill ){ return false; }
			else if ( from.Skills[SkillName.EvalInt].Value < skill ){ return false; }

			return true;
		}

		public static double GetSythSkillMax( Mobile from )
		{
			if ( from.Skills[SkillName.Swords].Value > from.Skills[SkillName.EvalInt].Value ){ return from.Skills[SkillName.Swords].Value; }

			return from.Skills[SkillName.EvalInt].Value;
		}

		public static bool HasSpell( Mobile from, int spellID )
		{
			Spellbook book = Spellbook.Find( from, spellID );

			return ( book != null && book.HasSpell( spellID ) );
		}

		public static void CastSpell( Mobile from, int spell ) //////////////////////////////////////////////////////////////////////////////////////
		{
			if ( HasSpell( from, spell ) )
			{
				if ( spell == 270 ){ InvokeCommand( "Psychokinesis", from ); }
				else if ( spell == 271 ){ InvokeCommand( "DeathGrip", from ); }
				else if ( spell == 272 ){ InvokeCommand( "Projection", from ); }
				else if ( spell == 273 ){ InvokeCommand( "ThrowSword", from ); }
				else if ( spell == 274 ){ InvokeCommand( "SythSpeed", from ); }
				else if ( spell == 275 ){ InvokeCommand( "SythLightning", from ); }
				else if ( spell == 276 ){ InvokeCommand( "Absorption", from ); }
				else if ( spell == 277 ){ InvokeCommand( "PsychicBlast", from ); }
				else if ( spell == 278 ){ InvokeCommand( "DrainLife", from ); }
				else if ( spell == 279 ){ InvokeCommand( "CloneBody", from ); }
			}
		}

        public static void InvokeCommand( string c, Mobile from )
        {
            CommandSystem.Handle(from, String.Format("{0}{1}", CommandSystem.Prefix, c));
        }
	}
}