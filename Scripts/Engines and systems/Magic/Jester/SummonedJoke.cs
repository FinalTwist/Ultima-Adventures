using System;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.Network;

namespace Server.Mobiles
{
	[CorpseName( "a corpse" )]
	public class SummonedJoke : BaseCreature
	{
		public int BCPoison;
		public int BCImmune;

		public override bool DeleteCorpseOnDeath { get { return true; } }

		[Constructable]
		public SummonedJoke( Mobile owner, int skills, int time, int str, int dex, int iq, int poisons ): base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.3, 0.6 )
		{
			Name = "a prank";

			Timer.DelayCall( TimeSpan.FromSeconds( (double)(time) ), new TimerCallback( Delete ) );

			BCPoison = poisons+0;
			BCImmune = poisons+0;
			NameHue = 0x3B2;

			SetStr( str );
			SetDex( dex);
			SetInt( iq );

			SetHits( str );
			SetStam( dex );
			SetMana( iq );

			SetSkill( SkillName.Poisoning, (double)skills );
			SetSkill( SkillName.Anatomy, (double)skills );
			SetSkill( SkillName.MagicResist, (double)skills );
			SetSkill( SkillName.Tactics, (double)skills );
			SetSkill( SkillName.Wrestling, (double)skills );

			Fame = 0;
			Karma = 0;
			ControlSlots = 1;
		}

		public override bool IsScaredOfScaryThings{ get{ return false; } }
		public override bool BleedImmune{ get{ return true; } }
		public override bool BardImmune{ get{ return true; } }
		public override bool ShowFameTitle{ get{ return false; } }
		public override bool AlwaysAttackable{ get{ return true; } }

		public override Poison PoisonImmune
		{
			get
			{
				if ( BCImmune == 1 ){ return Poison.Lesser; }
				else if ( BCImmune == 2 ){ return Poison.Regular; }
				else if ( BCImmune == 3 ){ return Poison.Greater; }
				else if ( BCImmune == 4 ){ return Poison.Deadly; }
				else if ( BCImmune == 5 ){ return Poison.Lethal; }

				return null;
			}
		}

		public override Poison HitPoison
		{
			get
			{
				if ( BCPoison == 1 ){ return Poison.Lesser; }
				else if ( BCPoison == 2 ){ return Poison.Regular; }
				else if ( BCPoison == 3 ){ return Poison.Greater; }
				else if ( BCPoison == 4 ){ return Poison.Deadly; }
				else if ( BCPoison == 5 ){ return Poison.Lethal; }

				return null;
			}
		}

		public static void MakeJoker( Mobile from, Point3D p, int poisons, string name, int body, int hue, int sound, int phys, int cold, int fire, int pois, int engy )
		{
			Map map = from.Map;

			int TotalTime = Server.Spells.Jester.JesterSpell.Buff( from, "time" );
			int TotalBuff = Server.Spells.Jester.JesterSpell.Buff( from, "strength" );
			int TotalPoison = Server.Spells.Jester.JesterSpell.Buff( from, "poison" );
				if ( poisons < 1 ){ TotalPoison = 0; }
			int TotalSkill = Server.Spells.Jester.JesterSpell.Buff( from, "skills" );
			int TotalDamage = Server.Spells.Jester.JesterSpell.Buff( from, "damage" );
				int MinDamage = (int)(TotalDamage/2); if ( MinDamage < 1 ){ MinDamage = 1; }
			int Resists = (int)(TotalBuff/2);
				if ( Resists > 70 ){ Resists = 70; }

			BaseCreature prank = new SummonedJoke( from, TotalSkill, TotalTime, TotalBuff, ((int)(TotalBuff/2)), TotalBuff, TotalPoison );

			bool validLocation = false;
			Point3D loc = p;

			for ( int j = 0; !validLocation && j < 10; ++j )
			{
				int x = p.X + Utility.Random( 3 ) - 1;
				int y = p.Y + Utility.Random( 3 ) - 1;
				int z = map.GetAverageZ( x, y );

				if ( validLocation = map.CanFit( x, y, p.Z, 16, false, false ) )
					loc = new Point3D( x, y, p.Z );
				else if ( validLocation = map.CanFit( x, y, z, 16, false, false ) )
					loc = new Point3D( x, y, z );
			}

			prank.Name = name;
			prank.Hue = hue;
			prank.Body = body;
			prank.BaseSoundID = sound;

			prank.DamageMin = MinDamage;
			prank.DamageMax = TotalDamage;

			prank.ColdDamage = cold;
			prank.EnergyDamage = engy;
			prank.FireDamage = fire;
			prank.PhysicalDamage = phys;
			prank.PoisonDamage = pois;

			int coldResist = Resists;
				if ( fire > 0 ){ coldResist = (int)(Resists * (1.0 - (fire*0.01))); }
			int fireResist = Resists;
				if ( cold > 0 ){ fireResist = (int)(Resists * (1.0 - (cold*0.01))); }

			prank.ColdResistSeed = coldResist;
			prank.EnergyResistSeed = Resists;
			prank.FireResistSeed = fireResist;
			prank.PhysicalResistanceSeed = Resists;
			prank.PoisonResistSeed = Resists;

			prank.VirtualArmor = TotalDamage;
			prank.ControlMaster = from;
			prank.Controlled = true;
			prank.ControlOrder = OrderType.Guard;

			prank.MoveToWorld( loc, map );
		}

		public SummonedJoke( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Timer.DelayCall( TimeSpan.FromSeconds( 10.0 ), new TimerCallback( Delete ) );
		}
	}
}
