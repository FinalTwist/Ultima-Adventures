using System;
using Server;
using Server.Items;
using Server.Misc;

namespace Server.Mobiles
{
	public class Paragon
	{
		public static double ChestChance = .10;   		// Chance that a paragon will carry a paragon chest
		public static double DeedChance = .02;  
		public static Map[] Maps         = new Map[]    // Maps that paragons will spawn on
		{
			Map.Malas // WIZARD WANTS THEM ONLY IN GARGOYLE LAND
		};

		public static Type[] Artifacts = new Type[]
		{
			typeof( GoldBricks ), typeof( PhillipsWoodenSteed ), 
			typeof( AlchemistsBauble ), typeof( ArcticDeathDealer ),
			typeof( BlazeOfDeath ), typeof( BowOfTheJukaKing ),
			typeof( BurglarsBandana ), typeof( CavortingClub ),
			typeof( EnchantedTitanLegBone ), typeof( GwennosHarp ),
			typeof( IolosLute ), typeof( LunaLance ),
			typeof( NightsKiss ), typeof( NoxRangersHeavyCrossbow ),
			typeof( OrcishVisage ), typeof( PolarBearMask ),
			typeof( ShieldOfInvulnerability ), typeof( StaffOfPower ),
			typeof( VioletCourage ), typeof( HeartOfTheLion ), 
			typeof( WrathOfTheDryad ), typeof( PixieSwatter ), 
			typeof( GlovesOfThePugilist )
		};

		public static int    Hue   = Utility.RandomList( 0xB33, 0xB34, 0xB35, 0xB36, 0xB37 );        // Paragon hue
		
		// Buffs
		public static double HitsBuff   = 3.0;
		public static double StrBuff    = 1.20;
		public static double IntBuff    = 1.20;
		public static double DexBuff    = 1.20;
		public static double SkillsBuff = 1.20;
		public static double SpeedBuff  = 1.20;
		public static double FameBuff   = 1.40;
		public static double KarmaBuff  = 1.40;
		public static int    DamageBuff = 5;

		public static void Convert( BaseCreature bc )
		{
			if ( bc.IsParagon )
				return;

			bc.Hue = Hue;

			if ( bc.HitsMaxSeed >= 0 )
				bc.HitsMaxSeed = (int)( bc.HitsMaxSeed * HitsBuff );
			
			bc.RawStr = (int)( bc.RawStr * StrBuff );
			bc.RawInt = (int)( bc.RawInt * IntBuff );
			bc.RawDex = (int)( bc.RawDex * DexBuff );

			bc.Hits = bc.HitsMax;
			bc.Mana = bc.ManaMax;
			bc.Stam = bc.StamMax;
			
			if (bc.Controlled)
				bc.IsBonded = true;

			for( int i = 0; i < bc.Skills.Length; i++ )
			{
				Skill skill = (Skill)bc.Skills[i];

				if ( skill.Base > 0.0 )
					skill.Base *= SkillsBuff;
			}

			bc.PassiveSpeed /= SpeedBuff;
			bc.ActiveSpeed /= SpeedBuff;

			bc.DamageMin += DamageBuff;
			bc.DamageMax += DamageBuff;

			if ( bc.Fame > 0 )
				bc.Fame = (int)( bc.Fame * FameBuff );

			//if ( bc.Fame > 32000 )
			//	bc.Fame = 32000;

			// TODO: Mana regeneration rate = Sqrt( buffedFame ) / 4

			if ( bc.Karma != 0 )
			{
				bc.Karma = (int)( bc.Karma * KarmaBuff );

				if( Math.Abs( bc.Karma ) > 32000 )
					bc.Karma = 32000 * Math.Sign( bc.Karma );
			}
			bc.DynamicFameKarma();
			bc.DynamicGold();
         	bc.DynamicTaming(true);
		}

		public static void UnConvert( BaseCreature bc )
		{
			if ( !bc.IsParagon )
				return;

			bc.Hue = 0;

			if ( bc.HitsMaxSeed >= 0 )
				bc.HitsMaxSeed = (int)( bc.HitsMaxSeed / HitsBuff );
			
			bc.RawStr = (int)( bc.RawStr / StrBuff );
			bc.RawInt = (int)( bc.RawInt / IntBuff );
			bc.RawDex = (int)( bc.RawDex / DexBuff );

			bc.Hits = bc.HitsMax;
			bc.Mana = bc.ManaMax;
			bc.Stam = bc.StamMax;

			for( int i = 0; i < bc.Skills.Length; i++ )
			{
				Skill skill = (Skill)bc.Skills[i];

				if ( skill.Base > 0.0 )
					skill.Base /= SkillsBuff;
			}
			
			bc.PassiveSpeed *= SpeedBuff;
			bc.ActiveSpeed *= SpeedBuff;

			bc.DamageMin -= DamageBuff;
			bc.DamageMax -= DamageBuff;

			if ( bc.Fame > 0 )
				bc.Fame = (int)( bc.Fame / FameBuff );
			if ( bc.Karma != 0 )
				bc.Karma = (int)( bc.Karma / KarmaBuff );
		}

		public static bool CheckConvert( BaseCreature bc )
		{
			return CheckConvert( bc, bc.Location, bc.Map );
		}

		public static bool CheckConvert( BaseCreature bc, Point3D location, Map m )
		{
			if ( !Core.AOS )
				return false;

			if ( Array.IndexOf( Maps, m ) == -1 )
				return false;

			if ( bc is BlackGateDemon || bc is BasePerson || bc is Citizens || bc is BaseVendor || bc is Clone ) // WIZARD FOR NPCS
				return false;

			int fame = bc.Fame;

			if ( fame > 32000 )
				fame = 32000;

			double chance = 1 / Math.Round( 20.0 - ( fame / 3200 ));

			return ( chance > Utility.RandomDouble() );
		}

		public static bool CheckArtifactChance( Mobile m, BaseCreature bc )
		{
			if ( !Core.AOS )
				return false;

			if (bc.Controlled)
				return false;

			double fame = (double)bc.Fame;

			if ( fame > 32000 )
				fame = 32000;

			double chance = 1 / ( Math.Max( 10, 100 * ( 0.83 - Math.Round( Math.Log( Math.Round( fame / 6000, 3 ) + 0.001, 10 ), 3 ) ) ) * ( 100 - Math.Sqrt( m.Luck ) ) / 100.0 );

			return chance > Utility.RandomDouble();
		}

		public static void GiveArtifactTo( Mobile m )
		{
			//Item item = (Item)Activator.CreateInstance( Artifacts[Utility.Random(Artifacts.Length)] );

			Item item = ArtifactBuilder.CreateArtifact( "random" );

			if ( m.AddToBackpack( item ) )
				m.SendMessage( "As a reward for slaying the cursed creature, an artifact has been placed in your backpack." );
			else
				m.SendMessage( "As your backpack is full, your reward for slaying cursed creature has been placed at your feet." );
		}
	}
}