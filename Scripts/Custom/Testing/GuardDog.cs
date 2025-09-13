using System;
using System.Collections;
using System.Collections.Generic;
using Server.Items;
using Server.Items.Crops;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a dog corpse" )]
	[TypeAlias( "Server.Mobiles.Timberwolf" )]
	public class GuardDog : BaseCreature
	{
		private Mobile m_mark = null;
		[CommandProperty(AccessLevel.GameMaster)]
		public Mobile mark
		{
			get { return m_mark; }
			set { m_mark = value; }
		}

		private Mobile m_mastah = null;
		[CommandProperty(AccessLevel.GameMaster)]
		public Mobile mastah
		{
			get { return m_mastah; }
			set { m_mastah = value; }
		}

		private Mobile m_HasChecked;
		[CommandProperty(AccessLevel.GameMaster)]
		public Mobile HasChecked
		{
			get { return m_HasChecked; }
			set { m_HasChecked = value; }
		}

		[Constructable]
		public GuardDog() : base( AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			AIFullSpeedActive = true; // Force full speed
			AIFullSpeedPassive = false;
			Name = "a guard dog";
			Body = 225;
			BaseSoundID = 0x85;

			SetStr( 80, 90 );
			SetDex( 120, 150 );
			SetInt( 11, 25 );

			SetHits( 150, 280 );
			SetMana( 0 );

			SetDamage( 15, 60 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 15, 20 );
			SetResistance( ResistanceType.Fire, 5, 10 );
			SetResistance( ResistanceType.Cold, 10, 15 );
			SetResistance( ResistanceType.Poison, 5, 10 );
			SetResistance( ResistanceType.Energy, 5, 10 );

			SetSkill( SkillName.MagicResist, 27.6, 45.0 );
			SetSkill( SkillName.Tactics, 40.1, 70.0 );
			SetSkill( SkillName.Wrestling, 60.1, 80.0 );
			SetSkill( SkillName.Wrestling, 60.1, 80.0 );
			SetSkill(SkillName.DetectHidden, 115.0, 120.0);

			Fame = 450;
			Karma = 500;

			VirtualArmor = 16;

			Tamable = false;
			ControlSlots = 1;
			MinTameSkill = 80.1;
			mastah = null;

		}

		public override int Meat{ get{ return 1; } }
		public override int Hides{ get{ return 5; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat; } }
		public override PackInstinct PackInstinct{ get{ return PackInstinct.Canine; } }

		public override void OnAfterSpawn()
		{
			FindMastah();
			
			base.OnAfterSpawn();	
		}

		public override bool IsEnemy( Mobile m )
		{
			
			if ( m is BaseBlue || m is BaseChild || m is BaseVendor || m is BasePerson || m is Citizens || m is PlayerVendor || m is TownHerald || m is Townsperson )
				return false;

			if ( m is PlayerMobile && (Server.Misc.Worlds.GetRegionName( this.Map, this.Location ) == "Lamut County") && !m.Criminal && m.Kills < 5)
				return false;

			if ( m is PlayerMobile && ( !m.Criminal || m.Karma >= -5000 || m.Kills <= 0 ) )
				return false;

			if ( m is BaseCreature )
			{
			    BaseCreature c = (BaseCreature)m;

			    if( c.Controlled && c.ControlMaster is PlayerMobile )
			    {
					PlayerMobile d = (PlayerMobile)c.ControlMaster;
					
					if ( d is PlayerMobile && (Server.Misc.Worlds.GetRegionName( this.Map, this.Location ) == "Lamut County") && !d.Criminal && d.Kills < 5)
						return false;
					
					else if ( d is PlayerMobile && ( d.Criminal || d.Karma <= -5000 || d.Kills >= 5 ) )
						return true;

					else 
						return false;				
					
			    }

			    if ( c.Karma >= 0 || c.FightMode == FightMode.None )
					return false;

			}	
			
			return true;
		}

		public override bool OnBeforeDeath()
		{
			if ( this.LastKiller is PlayerMobile )
			{
				Mobile killer = this.LastKiller;
				List<Mobile> spotters = new List<Mobile>();
								foreach ( Mobile m in killer.GetMobilesInRange( 10 ) )
								{
									//if ( m is BaseBlue && m.CanSee( m_Thief ) && m.InLOS( m_Thief ) )
									if ( m is MidlandGuard || m is MidlandVendor )
									{

										killer.CriminalAction( false );
										((BaseCreature)m).FocusMob = killer;
										m.Combatant = killer;
										m.PublicOverheadMessage( MessageType.Regular, 0, false, string.Format ( "Stop! Over there!" ) ); 
									}

								}	
				if (mastah != null && ((BaseCreature)mastah).midrace > 0)
				{
					((PlayerMobile)LastKiller).AdjustReputation( mastah.Fame/4, ((BaseCreature)mastah).midrace, false);
				}

			}
			return base.OnBeforeDeath();
		}


		public virtual bool IsDrug(Item item)
		{
			if ((item is SmokeweedLeaves) || (item is SmokeweekJoint) )
				return true;

			return false;
		}

		public bool FindMastah()
		{
			if (mastah != null && mastah.Alive && !mastah.Deleted)
			{
				if (InRange( mastah, 3 ) && InLOS(mastah))
					return true;

				else if (!InRange( mastah, 5 ) && InLOS(mastah))
				{
					ControlTarget = mastah;
					ControlOrder = OrderType.Follow;
				}

				else if ( (!InRange( mastah, 5 ) && !InLOS(mastah)) || (InRange( mastah, 5 ) && !InLOS(mastah)))
					MoveToWorld(mastah.Location, mastah.Map);
			}
			if (mastah == null || (mastah != null && (mastah.Deleted || !mastah.Alive)))
			{
				mastah = null;
				foreach ( Mobile m in this.GetMobilesInRange( 12 ) )
				{
					if ( m is MidlandGuard && mastah == null )
					{
						if ( InRange( m, 8 ) && InLOS(m) )
							mastah = m;

						continue;
					}
				}
				if (mastah != null)
				{
					ControlTarget = mastah;
					ControlOrder = OrderType.Follow;
					return true;
				}
			}
			if (mastah != null) 
				return true;
			
			return false;
		}

		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if ( !m.Hidden && InRange( m, 6 ) && InLOS( m ) )
			{
				if (mark == null && m_HasChecked != m && (m is PlayerMobile || (m is BaseCreature && ((BaseCreature)m).Controlled ) ) )
					mark = m;
			}
			base.OnMovement(m, oldLocation);
		}
		public override void OnThink() 
		{ 
			if (mastah != null)
			{
				if (Combatant == null && mastah.Combatant != null)
					Combatant = mastah.Combatant;
				if (Combatant != null && mastah.Combatant == null)
					mastah.Combatant = Combatant;
			}
			if (this.mark != null)
			{
				double markPick = GetDistanceToSqrt(mark);
				if ((markPick < 4) && HasChecked != mark)
				{
					if (mark.Backpack != null)
					{
						Container pouch = mark.Backpack;
						ArrayList finalitems = new ArrayList( pouch.Items );
						foreach (Item items in finalitems)
						{
							if ((items != null) && (!(items.Deleted)))
							{
								HasChecked = mark;
								if (IsDrug(items))
								{
									this.Say("*Growl*");
									this.Direction = this.GetDirectionTo( mark.Location );
									if (mark is PlayerMobile)
									{
										PlayerMobile pm = (PlayerMobile) mark;
										pm.Criminal = true;
										Combatant = mark;
										FocusMob = mark;		
									}
									else if (mark is BaseCreature)
									{
										BaseCreature bc = (BaseCreature) mark;
										bc.Criminal = true;
										Combatant = mark;
										FocusMob = mark;
										if ((bc.Controlled) && (bc.ControlMaster is PlayerMobile))
										{
											PlayerMobile pm = (PlayerMobile) bc.ControlMaster;
											pm.Criminal = true;
										}
									}
									continue;
								}
							}
						}
					}
				}
				else if (markPick > 10 || HasChecked == mark)
				{
					mark = null;
				}
			}
			if (!FindMastah() && Combatant == null)
				this.Kill();
			
			base.OnThink();

		}

		protected override BaseAI ForcedAI { get { return new K9AI( this ); } }

		public GuardDog(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);

		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();

			FindMastah();
			AIFullSpeedActive = true; // Force full speed
			AIFullSpeedPassive = false;
		}
	}
//================== GuardDog AI ======================
	public class K9AI : BaseAI
	{
		public bool RunFrom( Mobile m )
		{
			if( m_Mobile.InRange( m, 1 ) )
				return false;
			Run( (m_Mobile.GetDirectionTo( m ) - 4) & Direction.Mask );
			return true;
		}

		public bool RunTo( Mobile m, int range )
		{
			if( m_Mobile.InRange( m, range ) )
				return false;
			Run( (m_Mobile.GetDirectionTo( m )) & Direction.Mask );
			return true;
		}

		public void Run( Direction d )
		{
			if ( (m_Mobile.Spell != null && m_Mobile.Spell.IsCasting) || m_Mobile.Paralyzed || m_Mobile.Frozen || m_Mobile.DisallowAllMoves )
				return;
			if( !m_Mobile.Mounted && !m_Mobile.Body.IsHuman )
			{
				//Console.WriteLine("GetSpeed");
				m_Mobile.CurrentSpeed = m_Mobile.ActiveSpeed;
			}
			else if ( !m_Mobile.Mounted && m_Mobile.Body.IsHuman )
			{
				m_Mobile.CurrentSpeed = 0.12;
				//Console.WriteLine("0.12");
			}
			else
				m_Mobile.CurrentSpeed = 0.01;
			m_Mobile.Direction = d | Direction.Running;
			DoMove( m_Mobile.Direction, true );
		}

		public K9AI(GuardDog m) : base (m)
		{
		}

		public override bool DoActionWander()
		{
			m_Mobile.DebugSay( "I have no combatant" );

			if ( AcquireFocusMob( m_Mobile.RangePerception, m_Mobile.FightMode, false, false, true ) )
			{
				if ( m_Mobile.Debug )
					m_Mobile.DebugSay( "I have detected {0}, attacking", m_Mobile.FocusMob.Name );

				m_Mobile.Combatant = m_Mobile.FocusMob;
				Action = ActionType.Combat;
			}
			else if (m_Mobile is GuardDog)
			{
				Direction togo = Direction.South;
				Mobile mark = null;
				GuardDog k9 = (GuardDog)m_Mobile;

					foreach ( Mobile mob in k9.GetMobilesInRange( 10 ) )
					{
						if ((mob is PlayerMobile) && (mark == null) && !mob.Hidden)
						{
							mark = mob;
							k9.mark = mark;
							togo = m_Mobile.GetDirectionTo( mob );
						}
						else if ((mob is BaseCreature) && (mark == null) && !mob.Hidden)
						{
							BaseCreature bc = (BaseCreature) mob;
							if (bc.Controlled)
							{
								if ((bc.ControlMaster != null) && (bc.ControlMaster is PlayerMobile) )
								{
									mark = mob;
									k9.mark = mark;
									togo = m_Mobile.GetDirectionTo( mob );
								}
							}
						}
						else if (mob is BlueGuard && ((GuardDog)m_Mobile).mastah == null)
						{
							((GuardDog)m_Mobile).mastah = mob;
							togo = m_Mobile.GetDirectionTo( mob );
						}
					}
					if ((Utility.RandomMinMax( 1,5 ) > 1) && (mark != null)) DoMove(togo);
					else if (((GuardDog)m_Mobile).mastah != null )
					{
						togo = m_Mobile.GetDirectionTo( ((GuardDog)m_Mobile).mastah );
						DoMove(togo);
					} 
					else base.DoActionWander();

			}
			else
			{
				base.DoActionWander();
			}

			return true;
		}

		public override bool DoActionCombat()
		{
			Mobile combatant = m_Mobile.Combatant;

			if ( combatant == null || combatant.Deleted || combatant.Map != m_Mobile.Map || !combatant.Alive || combatant.IsDeadBondedPet )
			{
				m_Mobile.DebugSay( "My combatant is gone, so my guard is up" );

				Action = ActionType.Guard;

				return true;
			}

			if ( !m_Mobile.InRange( combatant, m_Mobile.RangePerception ) )
			{
				// They are somewhat far away, can we find something else?

				if ( AcquireFocusMob( m_Mobile.RangePerception, m_Mobile.FightMode, false, false, true ) )
				{
					m_Mobile.Combatant = m_Mobile.FocusMob;
					m_Mobile.FocusMob = null;
				}
				else if ( !m_Mobile.InRange( combatant, m_Mobile.RangePerception * 3 ) )
				{
					m_Mobile.Combatant = null;
				}

				combatant = m_Mobile.Combatant;

				if ( combatant == null )
				{
					m_Mobile.DebugSay( "My combatant has fled, so I am on guard" );
					Action = ActionType.Guard;

					return true;
				}
			}

			/*if ( !m_Mobile.InLOS( combatant ) )
			{
				if ( AcquireFocusMob( m_Mobile.RangePerception, m_Mobile.FightMode, false, false, true ) )
				{
					m_Mobile.Combatant = combatant = m_Mobile.FocusMob;
					m_Mobile.FocusMob = null;
				}
			}*/

			if ( MoveTo( combatant, true, m_Mobile.RangeFight ) )
			{
				m_Mobile.Direction = m_Mobile.GetDirectionTo( combatant );
			}
			else if ( AcquireFocusMob( m_Mobile.RangePerception, m_Mobile.FightMode, false, false, true ) )
			{
				if ( m_Mobile.Debug )
					m_Mobile.DebugSay( "My move is blocked, so I am going to attack {0}", m_Mobile.FocusMob.Name );

				m_Mobile.Combatant = m_Mobile.FocusMob;
				Action = ActionType.Combat;

				return true;
			}
			else if ( m_Mobile.GetDistanceToSqrt( combatant ) > m_Mobile.RangePerception + 1 )
			{
				if ( m_Mobile.Debug )
					m_Mobile.DebugSay( "I cannot find {0}, so my guard is up", combatant.Name );

				Action = ActionType.Guard;

				return true;
			}
			else
			{
				if ( m_Mobile.Debug )
					m_Mobile.DebugSay( "I should be closer to {0}", combatant.Name );
			}

			if ( !m_Mobile.Controlled && !m_Mobile.Summoned && !m_Mobile.IsParagon )
			{
				if ( m_Mobile.Hits < m_Mobile.HitsMax * 20/100 )
				{
					// We are low on health, should we flee?

					bool flee = false;

					if ( m_Mobile.Hits < combatant.Hits )
					{
						// We are more hurt than them

						int diff = combatant.Hits - m_Mobile.Hits;

						flee = ( Utility.Random( 0, 100 ) < (10 + diff) ); // (10 + diff)% chance to flee
					}
					else
					{
						flee = Utility.Random( 0, 100 ) < 10; // 10% chance to flee
					}

					if ( flee )
					{
						if ( m_Mobile.Debug )
							m_Mobile.DebugSay( "I am going to flee from {0}", combatant.Name );

						Action = ActionType.Flee;
					}
				}
			}

			return true;
		}

		public override bool DoActionGuard()
		{
			if ( AcquireFocusMob(m_Mobile.RangePerception, m_Mobile.FightMode, false, false, true ) )
			{
				if ( m_Mobile.Debug )
					m_Mobile.DebugSay( "I have detected {0}, attacking", m_Mobile.FocusMob.Name );

				m_Mobile.Combatant = m_Mobile.FocusMob;
				Action = ActionType.Combat;
			}
			else
			{
				base.DoActionGuard();
			}

			return true;
		}

		public override bool DoActionFlee()
		{
			if ( m_Mobile.Hits > m_Mobile.HitsMax/2 )
			{
				m_Mobile.DebugSay( "I am stronger now, so I will continue fighting" );
				Action = ActionType.Combat;
			}
			else
			{
				m_Mobile.FocusMob = m_Mobile.Combatant;
				RunFrom(m_Mobile.FocusMob);
				//base.DoActionFlee();
			}

			return true;
		}
	}
}