using System;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.Network;

namespace Server.Mobiles
{
	[CorpseName( "a slimy corpse" )]
	public class AlchemicSlime : BaseCreature
	{
		public int BCPoison;
		public int BCImmune;

		public override bool DeleteCorpseOnDeath { get { return true; } }

		[Constructable]
		public AlchemicSlime( Mobile owner, int skills, int time, int magery, int str, int dex, int iq, int poisons, int glow ): base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.3, 0.6 )
		{
			Name = "a slime";
			Body = 51;
			BaseSoundID = 456;
			Hue = Utility.RandomSlimeHue();

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

			if ( magery == 1 )
			{
				SetSkill( SkillName.Poisoning, (double)skills );
				SetSkill( SkillName.EvalInt, (double)skills );
				SetSkill( SkillName.Magery, (double)skills );
				SetSkill( SkillName.Meditation, (double)skills );
				SetSkill( SkillName.MagicResist, (double)skills );
				SetSkill( SkillName.Wrestling, (double)skills );
			}
			else
			{
				SetSkill( SkillName.Poisoning, (double)skills );
				SetSkill( SkillName.Anatomy, (double)skills );
				SetSkill( SkillName.MagicResist, (double)skills );
				SetSkill( SkillName.Tactics, (double)skills );
				SetSkill( SkillName.Wrestling, (double)skills );
			}

			Fame = 0;
			Karma = 0;
			ControlSlots = 1;

			if ( glow > 0 ){ AddItem( new LightSource() ); }
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

		public static void MakeSlime( Mobile from, Point3D p, int magery, int poisons, string name, int hue, int phys, int cold, int fire, int pois, int engy, int glow, int hate )
		{
			Map map = from.Map;

			int TotalTime = Server.Items.BaseMixture.Buff( from, "time" );
			int TotalBuff = Server.Items.BaseMixture.Buff( from, "strength" );
			int TotalPoison = Server.Items.BaseMixture.Buff( from, "poison" );
				if ( poisons < 1 ){ TotalPoison = 0; }
			int TotalSkill = Server.Items.BaseMixture.Buff( from, "skills" );
			int TotalDamage = Server.Items.BaseMixture.Buff( from, "damage" );
				int MinDamage = (int)(TotalDamage/2); if ( MinDamage < 1 ){ MinDamage = 1; }
			int Resists = (int)(TotalBuff/2);
				if ( Resists > 70 ){ Resists = 70; }

			BaseCreature slime = new AlchemicSlime( from, TotalSkill, TotalTime, magery, TotalBuff, ((int)(TotalBuff/2)), TotalBuff, TotalPoison, glow );

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

			slime.Name = name;
			slime.Hue = hue;
			if ( magery > 0 ){ slime.AI = AIType.AI_Mage; }

			slime.DamageMin = MinDamage;
			slime.DamageMax = TotalDamage;

			slime.ColdDamage = cold;
			slime.EnergyDamage = engy;
			slime.FireDamage = fire;
			slime.PhysicalDamage = phys;
			slime.PoisonDamage = pois;

			int coldResist = Resists;
				if ( fire > 0 ){ coldResist = (int)(Resists * (1.0 - (fire*0.01))); }
			int fireResist = Resists;
				if ( cold > 0 ){ fireResist = (int)(Resists * (1.0 - (cold*0.01))); }

			slime.ColdResistSeed = coldResist;
			slime.EnergyResistSeed = Resists;
			slime.FireResistSeed = fireResist;
			slime.PhysicalResistanceSeed = Resists;
			slime.PoisonResistSeed = Resists;

			slime.WhisperHue = hate;
			slime.VirtualArmor = TotalDamage;
			slime.ControlMaster = from;
			slime.Controlled = true;
			slime.ControlOrder = OrderType.Guard;

			slime.MoveToWorld( loc, map );
		}

		public AlchemicSlime( Serial serial ) : base( serial )
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
