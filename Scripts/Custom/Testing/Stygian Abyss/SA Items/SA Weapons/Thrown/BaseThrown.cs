using System;
using Server.Items;
using Server.Network;
using Server.Spells;
using Server.Mobiles;

namespace Server.Items
{

	public abstract class BaseThrown : BaseRanged
	{
		public abstract int MinThrowRange{ get; }
		public virtual int MaxThrowRange{ get{ return MinThrowRange + 4; } }
		private Mobile m_Thrower;
		private Mobile m_Target;
		private Point3D m_KillSave;

		public override int DefMaxRange
		{
			get
			{
				if ( Parent is PlayerMobile )
				{
					int range = ((PlayerMobile)Parent).Str / 10;

					return Math.Min( range, 10 );
				}

				return 10;
			}
		}

		public override int EffectID{ get{ return ItemID; } }
		public override Type AmmoType{ get{ return null; } }
		public override Item Ammo{ get{ return null; } }

		public override int DefHitSound{ get{ return 0x5D3; } }
		public override int DefMissSound{ get{ return 0x5D4; } }

		public override SkillName DefSkill{ get{ return SkillName.Throwing; } }
		//public override WeaponType DefType{ get{ return WeaponType.Ranged; } }
		public override WeaponAnimation DefAnimation{ get{ return WeaponAnimation.Slash1H; } }

		public override SkillName AccuracySkill{ get{ return SkillName.Throwing; } }

		public BaseThrown( int itemID ) : base( itemID )
		{
		}

		public BaseThrown( Serial serial ) : base( serial )
		{
		}

		public override TimeSpan OnSwing( Mobile attacker, Mobile defender )
		{
			TimeSpan ts = base.OnSwing( attacker, defender );

			// time it takes to throw it around including mystic arc
			if ( ts < TimeSpan.FromMilliseconds( 1000 ) )
				ts = TimeSpan.FromMilliseconds( 1000 );

			return ts;
		}

		public override bool OnFired( Mobile attacker, Mobile defender )
		{
			m_Thrower = attacker;

			if ( !attacker.InRange( defender, 1 ) )
			{
				//Internalize();
				Visible = false;
				attacker.MovingEffect( defender, EffectID, 18, 1, false, false );
			}

			return true;
		}

		public override void OnHit( Mobile attacker, Mobile defender, double damageBonus )
		{
			m_Target = defender;
			m_KillSave = defender.Location;

			if ( !(WeaponAbility.GetCurrentAbility( attacker ) is MysticArc) )
				Timer.DelayCall( TimeSpan.FromMilliseconds( 333.0 ), new TimerCallback( ThrowBack ) );

			base.OnHit( attacker, defender, damageBonus );
		}

		public override void OnMiss( Mobile attacker, Mobile defender )
		{
			m_Target = defender;

			if ( !(WeaponAbility.GetCurrentAbility( attacker ) is MysticArc) )
				Timer.DelayCall( TimeSpan.FromMilliseconds( 333.0 ), new TimerCallback( ThrowBack ) );

			base.OnMiss( attacker, defender );
		}

		public virtual void ThrowBack()
		{
			if ( m_Target != null )
				m_Target.MovingEffect( m_Thrower, EffectID, 18, 1, false, false );
			else if ( m_Thrower != null )
				Effects.SendMovingParticles( new Entity( Serial.Zero, m_KillSave, m_Thrower.Map ), m_Thrower, ItemID, 18, 0, false, false, Hue, 0, 9502, 1, 0, (EffectLayer)255, 0x100 );

			Timer.DelayCall( TimeSpan.FromMilliseconds( 333.0 ), new TimerCallback( UnHide ) );
		}

		public virtual void UnHide()
		{
			if ( this != null )
				Visible = true;

			//if ( m_Thrower != null )
				//if ( !m_Thrower.EquipItem( this ) )
					//MoveToWorld( m_Thrower.Location, m_Thrower.Map );
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
			Visible = true;
		}
	}
}