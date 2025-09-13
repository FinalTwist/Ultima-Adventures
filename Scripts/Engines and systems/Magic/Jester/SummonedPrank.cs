using System;
using Server;
using Server.Items;
using Server.Targeting;
using Server.Network;
using System.Collections.Generic;
using System.Collections;
using Server.Regions;
using Server.Multis;
using Server.Misc;

namespace Server.Mobiles
{
	public class SummonedPrank : BaseCreature
	{
		public override bool DeleteCorpseOnDeath { get { return true; } }

		[Constructable]
		public SummonedPrank( Mobile owner, int time, int damage, int range, int type ): base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.3, 0.6 )
		{
			Name = "a prank";

			Timer.DelayCall( TimeSpan.FromSeconds( (double)(time) ), new TimerCallback( Delete ) );

			NameHue = 0x3B2;

			SetStr( damage );
			SetDex( range );
			SetInt( 10+type );

			SetHits( damage );
			SetMana( 0 );

			Fame = 0;
			Karma = 0;
			ControlSlots = 3;

			if ( type > 0 ){ CantWalk = true; }
		}

		public override bool IsScaredOfScaryThings{ get{ return false; } }
		public override bool BleedImmune{ get{ return true; } }
		public override bool BardImmune{ get{ return true; } }
		public override bool ShowFameTitle{ get{ return false; } }
		public override bool AlwaysAttackable{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }

		public static void MakePrankster( Mobile from, Point3D p, string name, int body, int hue, int move )
		{
			Map map = from.Map;

			int TotalTime = Server.Spells.Jester.JesterSpell.Buff( from, "time" );
			int TotalBuff = Server.Spells.Jester.JesterSpell.Buff( from, "hurts" );
			int TotalRange = Server.Spells.Jester.JesterSpell.Buff( from, "range" );

			BaseCreature prank = new SummonedPrank( from, TotalTime, TotalBuff, TotalRange, move );

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

			prank.DamageMin = 0;
			prank.DamageMax = 0;
			prank.PhysicalDamage = 100;
			prank.VirtualArmor = 0;
			prank.ControlMaster = from;
			prank.Controlled = true;
			prank.ControlOrder = OrderType.Guard;

			prank.MoveToWorld( loc, map );
		}

		public override void OnGaveMeleeAttack( Mobile defender )
		{
			base.OnGaveMeleeAttack( defender );
			BlowUp( this, this );
		}

		public override void OnGotMeleeAttack( Mobile attacker )
		{
			base.OnGotMeleeAttack( attacker );
			BlowUp( this, this );
		}

		public override bool OnBeforeDeath()
		{
			BlowUp( this, this );
			return base.OnBeforeDeath();
		}

		public static void BlowUp( Mobile from, BaseCreature bc )
		{
			List<Mobile> targets = new List<Mobile>();

			Map map = from.Map;

			if ( map != null && from != null )
			{
				foreach ( Mobile m in from.GetMobilesInRange( from.RawDex ) )
				{
					if ( from.InLOS( m ) && m.Alive && from.CanBeHarmful( m ) && !m.Blessed && from != m && bc.ControlMaster != m && bc.SummonMaster != m )
						targets.Add( m );
				}
				for ( int i = 0; i < targets.Count; ++i )
				{
					Mobile m = targets[i];

					int physDamage = 100;
					int fireDamage = 0;
					int coldDamage = 0;
					int poisDamage = 0;
					int nrgyDamage = 0;

					if ( from.RawInt > 10 )
					{
						physDamage = 40;
						fireDamage = 40;
						coldDamage = 0;
						poisDamage = 0;
						nrgyDamage = 20;

						Effects.SendLocationEffect( m.Location, m.Map, 0x3822, 60, 10, 0, 0 );
						m.PlaySound( 0x307 );
					}
					else
					{
						Effects.SendLocationParticles( EffectItem.Create( m.Location, m.Map, EffectItem.DefaultDuration ), 0x3728, 8, 20, 0, 0, 5042, 0 );
						m.PlaySound( 0x664 );
					}
					AOS.Damage( m, from, from.RawStr, physDamage, fireDamage, coldDamage, poisDamage, nrgyDamage );
				}

				from.Delete();
			}
		}

		public SummonedPrank( Serial serial ) : base( serial )
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