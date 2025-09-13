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




namespace Server.Spells.Jedi
{
	public abstract class JediSpell : Spell
	{
		public virtual int spellIndex { get { return 1; } }
		public abstract int RequiredTithing { get; }
		public abstract double RequiredSkill { get; }
		public abstract int RequiredMana{ get; }
		public override bool ClearHandsOnCast { get { return false; } }
		public override SkillName CastSkill { get { return SkillName.EvalInt; } }
		public override SkillName DamageSkill { get { return SkillName.Swords; } }
		public override int CastRecoveryBase { get { return 7; } }

		public JediSpell( Mobile caster, Item scroll, SpellInfo info ) : base( caster, scroll, info )
		{
		}

		public static string SpellInfo( int spellID, int slice )
		{
			string value = "";
			string name = "";
			string mantra = "";
			string map = "";
			string dungeon = "";
			string jedi = "";
			string world = "";
			string scroll = "";
			string describe = "";
			string mana = "";
			string skill = "";
			string crystal = "";
			string icon = "";

			if ( spellID == 280 ){ name = "Force Grip"; icon="11244"; crystal = "6"; skill = "10"; mana = "5"; mantra = "Drumat"; map = "Sosaria"; dungeon = "the City of Britain"; world = "Land of Sosaria"; jedi = "Jacen Sollo"; scroll = "JediDatacron01"; 
				describe = "Allows a Jedi to use or move an object that is out of reach. Can be used to set off some Chest Traps from a safe distance as well."; }
			else if ( spellID == 281 ){ name = "Mind's Eye"; icon="11243"; crystal = "16"; skill = "20"; mana = "8"; mantra = "Vincent"; map = "Sosaria"; dungeon = "the Town of Moon"; world = "Land of Sosaria"; jedi = "Kiadi Mundia"; scroll = "JediDatacron02"; 
				describe = "Allows the Jedi to concentrate and see that which cannot be seen. They may be able to notice something hidden or a concealed trap."; }
			else if ( spellID == 282 ){ name = "Mirage"; icon="11201"; crystal = "24"; skill = "30"; mana = "12"; mantra = "Michal"; map = "Sosaria"; dungeon = "the Village of Grey"; world = "Land of Sosaria"; jedi = "Kip Fisto"; scroll = "JediDatacron03"; 
				describe = "This power allows the Jedi to create a projection of themselves that can distract foes from the Jedi themselves. This projection contains physical attributes as the psychic energy of the Jedi can maintain the illusion for up to about 3 minutes, depending on how powerful they are."; }
			else if ( spellID == 283 ){ name = "Throw Sabre"; icon="11215"; crystal = "12"; skill = "40"; mana = "16"; mantra = "Tiana"; map = "Sosaria"; dungeon = "the City of Montor"; world = "Land of Sosaria"; jedi = "Marra Jade"; scroll = "JediDatacron04"; 
				describe = "The Jedi can throw their equipped sword at an enemy, doing not only the sword's normal damage but also an extra 17-53 points of damage. The types of resistances that the sword inflicts damage on match that of the sword. The sword quickly returns to the Jedi's hand upon throwing it."; }
			else if ( spellID == 284 ){ name = "Celerity"; icon="11249"; crystal = "80"; skill = "50"; mana = "20"; mantra = "Abigayl"; map = "Sosaria"; dungeon = "the Town of Renika"; world = "the Island of Umber Veil"; jedi = "Numi Sunrider"; scroll = "JediDatacron05"; 
				describe = "This increases the running speed of the Jedi for about 10-25 minutes, making them run as fast as a stallion. This power cannot be called upon within certain areas and will often cease to function when entering those areas."; }
			else if ( spellID == 285 ){ name = "Psychic Aura"; icon="11237"; crystal = "32"; skill = "20"; mana = "24"; mantra = "Wilems"; map = "Lodor"; dungeon = "the City of Elidor"; world = "Land of Lodoria"; jedi = "Plo Kune"; scroll = "JediDatacron06"; 
				describe = "The Jedi can create an aura around them that will protect them better from physical and energy damage, but also makes them weaker against cold, fire, and poison damage. A Jedi will have to use to power again in order to remove these effects."; }
			else if ( spellID == 286 ){ name = "Deflection"; icon="11204"; crystal = "500"; skill = "70"; mana = "28"; mantra = "Morden"; map = "Lodor"; dungeon = "the Village of Springvale"; world = "Land of Lodoria"; jedi = "Kyle Katran"; scroll = "JediDatacron07"; 
				describe = "This power gives the essence of the Jedi the ability to deflect most damaging magery spells back at the one who cast it. The amount that a Jedi can deflect is dependent on their power."; }
			else if ( spellID == 287 ){ name = "Soothing Touch"; icon="11213"; crystal = "48"; skill = "10"; mana = "32"; mantra = "Kurklan"; map = "Lodor"; dungeon = "the Village of Islegem"; world = "Land of Lodoria"; jedi = "Kyp Duron"; scroll = "JediDatacron08"; 
				describe = "The Jedi can heal themselves and others. With a high enough level of Jedi power, they could potentially cure poison, stop bleeding, and heal mortal wounds."; }
			else if ( spellID == 288 ){ name = "Stasis Field"; icon="11253"; crystal = "52"; skill = "50"; mana = "36"; mantra = "Greggs"; map = "Lodor"; dungeon = "Greensky Village"; world = "Land of Lodoria"; jedi = "Ganer Rhysode"; scroll = "JediDatacron09"; 
				describe = "A Jedi can create a field around another that will put them in stasis for a period of time, where they cannot take any actions for a short duration."; }
			else if ( spellID == 289 ){ name = "Replicate"; icon="11218"; crystal = "250"; skill = "100"; mana = "40"; mantra = "Leantre"; map = "Sosaria"; dungeon = "the Kuldar Cemetery"; world = "the Bottle World of Kuldar"; jedi = "Coran Horn"; scroll = "JediDatacron10"; 
				describe = "This power allows the Jedi to create a replication crystal that can hold the Jedi's genetic pattern. If the Jedi meets an untimely end, the crystal will activate in 30 seconds and create a replicant that the Jedi's soul can then occupy. These crystal are quite fragile so you would need to make sure the crystal did not crumble while you were resting for long periods of time."; }

			if ( slice == 1 ){ value = name; }
			else if ( slice == 2 ){ value = skill; }
			else if ( slice == 3 ){ value = mana; }
			else if ( slice == 4 ){ value = jedi; }
			else if ( slice == 5 ){ value = map; }
			else if ( slice == 6 ){ value = dungeon; }
			else if ( slice == 7 ){ value = world; }
			else if ( slice == 8 ){ value = mantra; }
			else if ( slice == 9 ){ value = scroll; }
			else if ( slice == 10 ){ value = crystal; }
			else if ( slice == 11 ){ value = icon; }
			else { value = describe; }

			return value;
		}

		public static bool JediNotIllegal( Mobile from, bool checkSword )
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
				else if ( twoHand is BaseShield && twoHand.Name != null && (twoHand.Name).Contains("Jedi") ){ WearingCloth = true; }
			}

			if ( !HoldingSword ){ return false; }

			if ( from.FindItemOnLayer( Layer.Helm ) != null )
			{
				Item hat = from.FindItemOnLayer( Layer.Helm );
				if ( ( hat.ItemID == 0x2B71 || hat.ItemID == 0x2FBE || hat.ItemID == 0x3168 || hat.ItemID == 0x3176 || hat.ItemID == 0x3177 || hat.ItemID == 0x4D09 ) && hat.Name != null && (hat.Name).Contains("Jedi") )
					WearingCloth = true;
			}
			if ( from.FindItemOnLayer( Layer.OuterTorso ) != null )
			{
				Item robe = from.FindItemOnLayer( Layer.OuterTorso );
				if ( robe.Name != null && (robe.Name).Contains("Jedi") )
					WearingCloth = true;
			}
			if ( from.FindItemOnLayer( Layer.Cloak ) != null )
			{
				Item cloak = from.FindItemOnLayer( Layer.Cloak );
				if ( cloak.Name != null && (cloak.Name).Contains("Jedi") )
					WearingCloth = true;
			}
			if ( from.FindItemOnLayer( Layer.Talisman ) != null )
			{
				Item talisman = from.FindItemOnLayer( Layer.Talisman );
				if ( talisman.Name != null && (talisman.Name).Contains("Jedi") && talisman.ItemID == 0x556F )
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

			if ( Caster.Karma < 0 )
			{
				Caster.SendMessage( "Your Karma is too low to use this power." );
				return false;
			}
			else if ( GetJediSkillMax( Caster ) < RequiredSkill )
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
			else if ( this is Celerity && Server.Misc.MyServerSettings.NoMountsInCertainRegions() && Server.Mobiles.AnimalTrainer.IsNoMountRegion( Region.Find( Caster.Location, Caster.Map ) ) )
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

			if ( Caster.Karma < 0 )
			{
				Caster.SendMessage( "Your Karma is too low to use this power." );
				return false;
			}
			else if ( GetJediSkillMax( Caster ) < RequiredSkill )
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
			else if ( this is Celerity && Server.Misc.MyServerSettings.NoMountsInCertainRegions() && Server.Mobiles.AnimalTrainer.IsNoMountRegion( Region.Find( Caster.Location, Caster.Map ) ) )
			{
				Caster.SendMessage( "This power doesn't seem to work in this place." );
				return false;
			}

			if ( requiredTithing > 0 ){ DrainCrystals( Caster, requiredTithing ); }

			return true;
		}

		public override void DoFizzle()
		{
			Caster.PrivateOverheadMessage(MessageType.Regular, 0x3B2, false, "You fail to concentrate to call upon this power.", Caster.NetState);
			Caster.FixedParticles( 0x3735, 1, 30, 9503, EffectLayer.Waist );
			Caster.PlaySound( 0x19D );
			Caster.NextSpellTime = Core.TickCount;
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

			int v = (int) Math.Sqrt( from.Karma + 20000 + ( from.Skills.EvalInt.Fixed * 10 ) );

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
			if (from.Backpack == null)
				return;

			ArrayList targets = new ArrayList();
			foreach ( Item item in from.Backpack.Items )
			{
				if ( item is JediSpellbook )
				{
					JediSpellbook book = (JediSpellbook)item;
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

			if (from.Backpack == null)
					return crystal;

			ArrayList targets = new ArrayList();
			foreach ( Item item in from.Backpack.Items )
			{
				if ( item is JediSpellbook )
				{
					JediSpellbook book = (JediSpellbook)item;
					if ( book.owner == from )
					{
						crystal = book.crystals;
					}
				}
			}

			return crystal;
		}

		public static double GetJediDamage( Mobile from )
		{
			int karma = from.Karma;
				if ( karma < 1 ){ karma = 0; }
				if ( karma > 15000 ){ karma = 15000; }

			double hate = karma / 120; // MAX 125
			hate = hate + from.Skills[SkillName.Tactics].Value + from.Skills[SkillName.Swords].Value; // MAX 375

			return hate;
		}

		public static bool GetJediSkill( Mobile from, int skill )
		{
			if ( from.Skills[SkillName.Swords].Value < skill ){ return false; }
			else if ( from.Skills[SkillName.EvalInt].Value < skill ){ return false; }

			return true;
		}

		public static double GetJediSkillMax( Mobile from )
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
				if ( spell == 280 ){ InvokeCommand( "ForceGrip", from ); }
				else if ( spell == 281 ){ InvokeCommand( "MindsEye", from ); }
				else if ( spell == 282 ){ InvokeCommand( "Mirage", from ); }
				else if ( spell == 283 ){ InvokeCommand( "ThrowSabre", from ); }
				else if ( spell == 284 ){ InvokeCommand( "Celerity", from ); }
				else if ( spell == 285 ){ InvokeCommand( "PsychicAura", from ); }
				else if ( spell == 286 ){ InvokeCommand( "Deflection", from ); }
				else if ( spell == 287 ){ InvokeCommand( "SoothingTouch", from ); }
				else if ( spell == 288 ){ InvokeCommand( "StasisField", from ); }
				else if ( spell == 289 ){ InvokeCommand( "Replicate", from ); }
			}
		}

        public static void InvokeCommand( string c, Mobile from )
        {
            CommandSystem.Handle(from, String.Format("{0}{1}", CommandSystem.Prefix, c));
        }
	}
}