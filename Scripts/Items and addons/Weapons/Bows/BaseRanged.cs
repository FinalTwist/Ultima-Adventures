using System;
using Server.Items;
using Server.Network;
using Server.Spells;
using Server.Mobiles;

namespace Server.Items
{
	public abstract class BaseRanged : BaseMeleeWeapon
	{
		public abstract int EffectID{ get; }
		public abstract Type AmmoType{ get; }
		public abstract Item Ammo{ get; }

		public override int DefHitSound{ get{ return 0x234; } }
		public override int DefMissSound{ get{ return 0x238; } }

		public override SkillName DefSkill{ get{ return SkillName.Archery; } }
		public override WeaponType DefType{ get{ return WeaponType.Ranged; } }
		public override WeaponAnimation DefAnimation{ get{ return WeaponAnimation.ShootXBow; } }

		public override SkillName AccuracySkill{ get{ return SkillName.Archery; } }

		private Timer m_RecoveryTimer; // so we don't start too many timers
		private bool m_Balanced;
		private int m_Velocity;
		
		[CommandProperty( AccessLevel.GameMaster )]
		public bool Balanced
		{
			get{ return m_Balanced; }
			set{ m_Balanced = value; InvalidateProperties(); }
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public int Velocity
		{
			get{ return m_Velocity; }
			set{ m_Velocity = value; InvalidateProperties(); }
		}

		public BaseRanged( int itemID ) : base( itemID )
		{
		}

		public BaseRanged( Serial serial ) : base( serial )
		{
		}

		public override TimeSpan OnSwing( Mobile attacker, Mobile defender )
		{
			WeaponAbility a = WeaponAbility.GetCurrentAbility( attacker );

			// Make sure we've been standing still for .25/.5/1 second depending on Era
			if (Core.TickCount > (attacker.LastMoveTime + (Core.SE ? 250 : (Core.AOS ? 500 : 1000))) || (Core.AOS && WeaponAbility.GetCurrentAbility( attacker ) is MovingShot) )
			{
				bool canSwing = true;

				if ( Core.AOS )
				{
					canSwing = ( !attacker.Paralyzed && !attacker.Frozen );

					if ( canSwing )
					{
						Spell sp = attacker.Spell as Spell;

						canSwing = ( sp == null || !sp.IsCasting || !sp.BlocksMovement );
					}
				}

				if ( canSwing && attacker.HarmfulCheck( defender ) )
				{
					attacker.DisruptiveAction();
					attacker.Send( new Swing( 0, attacker, defender ) );

					if ( OnFired( attacker, defender ) )
					{
						if ( CheckHit( attacker, defender ) )
							OnHit( attacker, defender );
						else
							OnMiss( attacker, defender );
					}
				}

				attacker.RevealingAction();

				return GetDelay( attacker );
			}
			else
			{
				attacker.RevealingAction();

				return TimeSpan.FromSeconds( 0.25 );
			}
		}

		public override void OnHit( Mobile attacker, Mobile defender, double damageBonus )
		{
			if ( !( Ammo is Krystal ) && !( Ammo is HarpoonRope ) && !( Ammo is MageEye ) && !( Ammo is ThrowingWeapon ) && attacker.Player && !defender.Player && (defender.Body.IsAnimal || defender.Body.IsMonster) && 0.4 >= Utility.RandomDouble() )
				defender.AddToBackpack( Ammo );

			if ( defender is BaseCreature && Ammo is ThrowingWeapon && attacker.Player )
			{
				BaseCreature bc = (BaseCreature)defender;

				if ( attacker.FindItemOnLayer( Layer.OneHanded ) != null )
				{
					if ( attacker.FindItemOnLayer( Layer.OneHanded ) is ThrowingGloves )
					{
						ThrowingGloves glove = (ThrowingGloves)( attacker.FindItemOnLayer( Layer.OneHanded ) );
						ThrowingWeapon knife = new ThrowingWeapon();

						if ( glove.GloveType == "Stones" ){ knife.ammo = "Throwing Stones"; knife.ItemID = 0x10B6; knife.Name = "throwing stone"; }
						else if ( glove.GloveType == "Axes" ){ knife.ammo = "Throwing Axes"; knife.ItemID = 0x10B3; knife.Name = "throwing axe"; }
						else if ( glove.GloveType == "Daggers" ){ knife.ammo = "Throwing Daggers"; knife.ItemID = 0x10B7; knife.Name = "throwing dagger"; }
						else if ( glove.GloveType == "Darts" ){ knife.ammo = "Throwing Darts"; knife.ItemID = 0x10B5; knife.Name = "throwing dart"; }
						else if ( glove.GloveType == "Cards" && Server.Misc.GetPlayerInfo.isJester( attacker ) ){ knife.ammo = "Throwing Cards"; knife.ItemID = 0x4C29; knife.Name = "throwing card"; }
						else if ( glove.GloveType == "Tomatoes" && Server.Misc.GetPlayerInfo.isJester( attacker ) ){ knife.ammo = "Throwing Tomatoes"; knife.ItemID = 0x4C28; knife.Name = "throwing tomato"; }
						else { knife.ammo = "Throwing Stars"; knife.ItemID = 0x10B2; knife.Name = "throwing star"; }

						bc.PackItem( knife );
					}
				}
			}

			if ( Core.ML && m_Velocity > 0 &&  !(Server.Misc.AdventuresFunctions.IsPuritain((object)attacker)) )
			{
				int bonus = (int) attacker.GetDistanceToSqrt( defender );

				if ( bonus > 0 && m_Velocity > Utility.Random( 100 ) )
				{
					AOS.Damage( defender, attacker, bonus * 3, 100, 0, 0, 0, 0 );

					if ( attacker.Player )
						attacker.SendLocalizedMessage( 1072794 ); // Your arrow hits its mark with velocity!

					if ( defender.Player )
						defender.SendLocalizedMessage( 1072795 ); // You have been hit by an arrow with velocity!
				}
			}

			base.OnHit( attacker, defender, damageBonus );
		}

		public override void OnMiss( Mobile attacker, Mobile defender )
		{
			if ( attacker.Player && 0.4 >= Utility.RandomDouble() )
			{
				if ( Core.SE && !( Ammo is ThrowingWeapon ) && !( Ammo is MageEye ) && !( Ammo is HarpoonRope ) && !( Ammo is Krystal ) )
				{
					PlayerMobile p = attacker as PlayerMobile;

					if ( p != null )
					{
						int reduce = 0;
						
						BaseQuiver quiver = attacker.FindItemOnLayer( Layer.Cloak ) as BaseQuiver;
						
						if (quiver != null)
						{
							if (!Server.Misc.AdventuresFunctions.IsPuritain((object)p))
								reduce = quiver.LowerAmmoCost;
						}
							
						Type ammo = AmmoType;

						if (Utility.Random( 100 ) >= reduce)
						{
							if ( p.RecoverableAmmo.ContainsKey( ammo ) )
								p.RecoverableAmmo[ ammo ]++;
							else
								p.RecoverableAmmo.Add( ammo, 1 );

							if ( !p.Warmode )
							{
								if ( m_RecoveryTimer == null )
									m_RecoveryTimer = Timer.DelayCall( TimeSpan.FromSeconds( 10 ), new TimerCallback( p.RecoverAmmo ) );

								if ( !m_RecoveryTimer.Running )
									m_RecoveryTimer.Start();
							}
						}
					}
				//} else {
				//	Ammo.MoveToWorld( new Point3D( defender.X + Utility.RandomMinMax( -1, 1 ), defender.Y + Utility.RandomMinMax( -1, 1 ), defender.Z ), defender.Map );
				}
			}

			base.OnMiss( attacker, defender );
		}

		public virtual bool OnFired( Mobile attacker, Mobile defender )
		{
			int color = 0;

			if ( this is ThrowingGloves && attacker.Player )
			{
				string ammoType = "Throwing Stones";

				ThrowingGloves glove = (ThrowingGloves)this;
				if ( glove.GloveType == "Stones" ){ ammoType = "Throwing Stones"; color = 0x961; }
				else if ( glove.GloveType == "Axes" ){ ammoType = "Throwing Axes"; }
				else if ( glove.GloveType == "Daggers" ){ ammoType = "Throwing Daggers"; }
				else if ( glove.GloveType == "Darts" ){ ammoType = "Throwing Darts"; }
				else if ( glove.GloveType == "Cards" && Server.Misc.GetPlayerInfo.isJester( attacker ) ){ ammoType = "Throwing Cards"; }
				else if ( glove.GloveType == "Tomatoes" && Server.Misc.GetPlayerInfo.isJester( attacker ) ){ ammoType = "Throwing Tomatoes"; }
				else { ammoType = "Throwing Stars"; glove.GloveType = "Stones"; }

				foreach( Item i in attacker.Backpack.FindItemsByType( typeof( ThrowingWeapon ), true ) )
				{
					if ( ammoType == "Throwing Stones" ){ ((ThrowingWeapon)i).ammo = "Throwing Stones"; i.ItemID = 0x10B6; i.Name = "throwing stone"; }
					else if ( ammoType == "Throwing Axes" ){ ((ThrowingWeapon)i).ammo = "Throwing Axes"; i.ItemID = 0x10B3; i.Name = "throwing axe"; }
					else if ( ammoType == "Throwing Daggers" ){ ((ThrowingWeapon)i).ammo = "Throwing Daggers"; i.ItemID = 0x10B7; i.Name = "throwing dagger"; }
					else if ( ammoType == "Throwing Darts" ){ ((ThrowingWeapon)i).ammo = "Throwing Darts"; i.ItemID = 0x10B5; i.Name = "throwing dart"; }
					else if ( ammoType == "Throwing Cards" ){ ((ThrowingWeapon)i).ammo = "Throwing Cards"; i.ItemID = 0x4C29; i.Name = "throwing card"; }
					else if ( ammoType == "Throwing Tomatoes" ){ ((ThrowingWeapon)i).ammo = "Throwing Tomatoes"; i.ItemID = 0x4C28; i.Name = "throwing tomato"; }
					else { ((ThrowingWeapon)i).ammo = "Throwing Stars"; i.ItemID = 0x10B2; i.Name = "throwing star"; }
					i.InvalidateProperties();
				}
			}

			if ( attacker.Player )
			{
				BaseQuiver quiver = attacker.FindItemOnLayer( Layer.Cloak ) as BaseQuiver;
				Container pack = attacker.Backpack;

				if (quiver != null)
				{
					if ( quiver.LowerAmmoCost > 90)
						quiver.LowerAmmoCost = 90;

					if (Server.Misc.AdventuresFunctions.IsPuritain((object)attacker))
						quiver.LowerAmmoCost = 0;
				}

				if ( quiver == null || (quiver != null && Utility.Random( 100 ) >= quiver.LowerAmmoCost) )
				{
					// consume ammo
					if ( quiver != null && quiver.ConsumeTotal( AmmoType, 1 ) )
						quiver.InvalidateWeight();
					else if ( pack == null || !pack.ConsumeTotal( AmmoType, 1 ) )
						return false;
				}
				else if ( (quiver != null && quiver.FindItemByType( AmmoType ) == null) && ( pack == null || pack.FindItemByType( AmmoType ) == null ) )
				{
					// lower ammo cost should not work when we have no ammo at all
					return false;
				}
			}

			attacker.MovingEffect( defender, EffectID, 18, 1, false, false );

			Server.Gumps.QuickBar.RefreshQuickBar( attacker );

			return true;
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 3 ); // version

			writer.Write( (bool) m_Balanced );
			writer.Write( (int) m_Velocity );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 3:
				{
					m_Balanced = reader.ReadBool();
					m_Velocity = reader.ReadInt();

					goto case 2;
				}
				case 2:
				case 1:
				{
					break;
				}
				case 0:
				{
					/*m_EffectID =*/ reader.ReadInt();
					break;
				}
			}

			if ( version < 2 )
			{
				WeaponAttributes.MageWeapon = 0;
				WeaponAttributes.UseBestSkill = 0;
			}
		}
	}
}