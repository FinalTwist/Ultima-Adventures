using System; 
using System.Collections; 
using System.Collections.Generic;
using Server;
using Server.Misc; 
using Server.Items; 
using Server.Mobiles;
using Server.Gumps;
using Server.Targeting;
using Server.OneTime;

namespace Server.Mobiles 
{ 
	public class BaseCursed : AuraCreature, IOneTime
	{
       // public int OneTimeType = 3; //second : 3 = second, 4 = minute, 5 = hour, 6 = day (Pick a time interval 3-6)
		private int m_OneTimeType;
        public int OneTimeType
        {
            get{ return m_OneTimeType; }
            set{ m_OneTimeType = value; }
        }
		
        public override bool ReacquireOnMovement{ get { return false; } }
		public override TimeSpan ReacquireDelay{ get { return TimeSpan.FromSeconds( 1.0 ); } }

/*		public DateTime m_NextResurrect;
		public static TimeSpan ResurrectDelay = TimeSpan.FromSeconds( 20.0 );
		public DateTime m_NextTalk;
		public static TimeSpan TalkDelay = TimeSpan.FromSeconds( 2.0 );*/
		public int talkingtimer;
		public int restimer;
		public bool talk;
		public bool res;
		
		public override bool AlwaysMurderer{ get{ return true; } }
		public override bool AutoDispel{ get{ return true; } }
		public override double AutoDispelChance{ get{ return 0.75; } }
		public override bool BardImmune{ get{ return true; } }
		public override bool Unprovokable{ get{ return Core.SE; } }
		public override bool Uncalmable{ get{ return Core.SE; } }
		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }
		public override bool CanMoveOverObstacles { get { return true; } }
		public override bool CanDestroyObstacles { get { return true; } }
		
		public bool aichange;
		public virtual bool notames{ get{ return true; } }
		public virtual bool drain{ get{ return true; } }
		private bool drainstart = false;
		private int absorb;

		//public virtual bool morph{ get{ return true; } }
		//private Mobile m_MorphedInto;
		//private DateTime m_LastMorph;
		//public virtual string DefaultName{ get{ return "the Accursed"; } }
		//public virtual int DefaultHue{ get{ return 0; } }
		

		public virtual bool CheckResurrect( Mobile m )
		{
			return true;
		}
		
		public BaseCursed(AIType ai, FightMode fm, int PR, int FR, double AS, double PS) : base( ai, fm, PR, FR, AS, PS )
        {
            SpeechHue = Utility.RandomDyedHue(); 
			RangePerception = BaseCreature.DefaultRangePerception*2;
			//Criminal = true;
			AIFullSpeedActive = AIFullSpeedPassive = true; // Force full speed
			m_OneTimeType = 3;
			absorb = 0;
			Karma = -5000;
			drainstart = false;
			aichange = true;
			talkingtimer = 0;
			restimer = 0;
			talk = true;
			res = true;
		}

		public override bool IsEnemy( Mobile m )
		{
		    if ( m is BaseCursed || m is Citizens || m is DarkMoorGreeter || m is PlayerVendor || m is TownHerald || m is BaseVendor )
				return false;

			if (this is LegesElecti && m is BaseChampion)
				return false;

			if ( ( DisguiseTimers.IsDisguised( m ) || Server.Spells.Fifth.IncognitoSpell.m_Timers.Contains(m)) && !m.Criminal )
				return false;

			if (this is DemonKnight || this is LegesElecti )
			{
				if (m is PlayerMobile)
					return true;

				else if ( m is BaseCreature && ((BaseCreature)m).Controlled && ((BaseCreature)m).ControlMaster != null )
				{
					Mobile owner = ((BaseCreature)m).ControlMaster;
					if (owner is PlayerMobile)
						return true;
				} 
				else if ( m is BaseCreature && ((BaseCreature)m).Summoned && ((BaseCreature)m).SummonMaster != null )
				{
					Mobile owner = ((BaseCreature)m).SummonMaster;
					if (owner is PlayerMobile)
						return true;
				} 
				else
					return false;
			}

		    if ( m is BaseCreature && ((BaseCreature)m).ControlMaster != null && m.Combatant != this)
		    {
				Mobile owner = ((BaseCreature)m).ControlMaster;
				if (owner is PlayerMobile && owner.Karma <= -500)
					return false;
		    } 

			if (this is Praetor && m is MonsterNestEntity && ((MonsterNestEntity)m).MonstersNest is PortalEvil )
				return false;

			if (this is Praetor && m is MonsterNestEntity && ((MonsterNestEntity)m).MonstersNest is PortalGood )
				return true;

			if (this is Praetor && m.Combatant is MonsterNestEntity )
			{
				Mobile nest = m.Combatant;
				if (((MonsterNestEntity)nest).MonstersNest is PortalEvil)
					return true;
			}

		    if ( m is PlayerMobile && m.Karma <= -500 && !m.Criminal )
				return false;

			if (m is BaseCreature && m.Karma <= 0)
				return false;

		    return true;
		}

		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if ( !(this is LegesElecti) && !m.Frozen && ((BaseCursed)this).res && InRange( m, 4 ) && !InRange( oldLocation, 4 ) && InLOS( m ) && m is PlayerMobile )
			{

				if ( !m.Alive && ( m.Criminal || m.Karma <= -500) )
				{
					((BaseCursed)this).res = false;

					if ( m.Map == null || !m.Map.CanFit( m.Location, 16, false, false ) )
					{
						Say("Alibi, quaeso");  
					}
					else if ( CheckResurrect( m ) )
					{
						OfferResurrection( m );
					}
				}
				else if ( m.Hits < m.HitsMax && (m is PlayerMobile && m.Criminal || m.Karma <= -500) )
				{
					OfferHeal( (PlayerMobile) m );
				}
			}
			
			if (m is PlayerMobile && m.AccessLevel == AccessLevel.Player && ((BaseCursed)this).talk && this.Combatant == null  )
			{
					if (m.Karma > -5000)
					{
						switch (Utility.Random(16))
						{
							case 0: Say("Salvete " + m.Name + " aderst mors tua."); break;
							case 1: Say("" + m.Name + ", Hodie est felix die"); break;
							case 2: Say("" + m.Name + ", venite ad me mortale."); break;
							case 3: Say("Ego have been vultus pro vobis " + m.Name ); break;
							case 4: Say("Iungere nobis ad corruptionem."); break;
							case 5: Say("" + m.Name + ", ego sentio tuo infirmitatem."); break;
							case 6: Say("Pater tuus ego sum, " + m.Name + "."); break;
							case 7: Say("Tempus ad me, " + m.Name + "."); break;
							case 8: Say("Adducere mihi animam meam."); break;
						}
					}
					else 
					{
						switch (Utility.Random(10))
						{
							case 0: Say("Tibi domino nostro et ambules in semita, " + m.Name + "."); break;
							case 1: Say("Ego in te fortitudinem sentiunt " + m.Name + "."); break;
							case 2: Say("" + m.Name + ", grata frater."); break;
							case 3: Say("Salvete " + m.Name + ", nos una perdere et hoc mundo."); break;
							case 4: Say("Mali autem Dominus Deus noster."); break;
							case 5: Say("" + m.Name + ", Salve frater pari."); break;

						}
					}
					
					((BaseCursed)this).talk = false;
			}
			base.OnMovement(m, oldLocation);
		}				
	

		public override void OnThink()
		{

			if (this.Combatant != null ) // in fight mode
			{	
				Mobile target = this.Combatant;
				
				if (this.Hits <= (this.HitsMaxSeed / 5) && Utility.RandomDouble() > 66.0 && aichange) // not doing very good, change of tactic and close AI
				{
					if ( (((BaseCreature)this).AI == AIType.AI_Mage) || (((BaseCreature)this).AI == AIType.AI_Mage) && this.GetDistanceToSqrt(target) >= 4 ) // hone in to the enemy
					{
						((BaseCreature)this).AI = AIType.AI_Melee;
						this.Combatant = target;
						
					}
					else if ( ( ((BaseCreature)this).AI == AIType.AI_Melee && this.GetDistanceToSqrt(target) >=4 ) || this.GetDistanceToSqrt(target) <4 ) //im a melee AI and enemy is keeping distance or this enemy has killed me close distance
					{
						if (Utility.RandomBool() )
							((BaseCreature)this).AI = AIType.AI_Mage;
						else 
							((BaseCreature)this).AI = AIType.AI_Mage;
						this.Combatant = target;
						
					}
					
					aichange = false;
				}	

				if (this.Hits <= (this.HitsMaxSeed / 2) && Utility.RandomDouble() > 0.66 && !((BaseCursed)this).drainstart ) //time to look at sucking lifeforce
				{
					if (aichange)
					{
						((BaseCreature)this).AI = AIType.AI_Mage; // so he stays away from target
						this.Combatant = target;
						aichange = false;
					}
				
					drainstart = true;

					if (Utility.RandomDouble() > 0.66 && ((BaseCursed)this).talk)
					{
						((BaseCursed)this).talk = false;
						this.Say ("Ego potentiora, mortale.");
					}
				}
				
				if ( target.Hits < (target.HitsMax / 3) && aichange)
				{
					((BaseCreature)this).AI = AIType.AI_Melee; // hone in for the kill
					this.Combatant = target;
					aichange = false;
				}				
				
				if (target is BaseCreature)
				{
					BaseCreature bc = (BaseCreature)target;

					if ( bc.Controlled && Utility.RandomDouble() > 0.70 && bc.ControlMaster != null && this.notames ) // being attacked by a pet?  sent it to its master instead
					{
						if ( ((BaseCursed)this).talk )
						{
							this.Say( "Uos ego ad te dominum." );
							((BaseCursed)this).talk = false;
						}
						NoTames( bc, bc.ControlMaster);
						this.Combatant = target;
					}				
				}
				/*else if (target is PlayerMobile)
				{
					PlayerMobile pm = (PlayerMobile)target;
					if ( Utility.RandomDouble() > 0.90) // why not add some spice to this battle?
					{
						if ( ((BaseCursed)this).talk )
						{
							this.Say ("Ego mortalis anima tua, quia capta est!");
							((BaseCursed)this).talk = false;
						}
						
						MorphedInto = target;
					}				
				}*/
			}

			else if(this.Combatant == null)//no longer attacking, reset
			{
				((BaseCreature)this).AI = AIType.AI_Mage;
				aichange = true;
				//if (MorphedInto != null)
				//	Revert();
			}
			
			base.OnThink();
					
		}
		
		public override void OnDamagedBySpell(Mobile attacker)
        {
		
			if (attacker == null)
				return;
			else if (this.Combatant != attacker)
				this.Combatant = attacker; // make sure he targets the person casting the spell, not a tank/decoy
				
			if ( this.Hits <= (this.HitsMax / 3) && Utility.RandomBool() && !((BaseCursed)this).drainstart) // not doing too good, use long distance and close AI
			{
				if (aichange )
				{
					if (Utility.RandomBool() )
						((BaseCreature)this).AI = AIType.AI_Mage;
					else 
						((BaseCreature)this).AI = AIType.AI_Mage;
					aichange = false;
					this.Combatant = attacker;
				}
				if (this.drain)
				{
					drainstart = true;
					if (Utility.RandomDouble() > 0.70 && ((BaseCursed)this).talk)
					{
						((BaseCursed)this).talk = false;
						this.Say (" Ego potentiora! ");
					}
					
				}
			}
			else if (aichange )
			{
				((BaseCreature)this).AI = AIType.AI_Melee; // being attacked by mage, switch to melee
				this.Combatant = attacker;
			}
				
			base.OnDamagedBySpell(attacker);

		}
		
        public override void OnGotMeleeAttack(Mobile attacker)
        {
			if (attacker == null )
				return;
				
			if (this.Combatant == null)
				this.Combatant = attacker;
			
				Item twohanded = attacker.FindItemOnLayer( Layer.TwoHanded );			
				Item firstvalid = attacker.FindItemOnLayer( Layer.FirstValid );
				
			if (twohanded is BaseRanged || firstvalid is BaseRanged)
				AntiArcher( attacker );
				
			if (this.Hits <= (this.HitsMax / 3) && Utility.RandomBool() && !((BaseCursed)this).drainstart ) // need to keep distance and heal
			{
				if (aichange && this.GetDistanceToSqrt(attacker) <= 3) // close melee
				{
					((BaseCreature)this).AI = AIType.AI_Mage;
					aichange = false;
					this.Combatant = attacker;
				}
				else if (aichange && this.GetDistanceToSqrt(attacker) > 3) // archer
				{
					((BaseCreature)this).AI = AIType.AI_Melee;
					aichange = false;
					this.Combatant = attacker;
				}				
				if (this.drain)
				{
					drainstart = true;
					if (Utility.RandomDouble() > 0.70 && ((BaseCursed)this).talk)
					{
						((BaseCursed)this).talk = false;
						this.Say (" Ego potentiora! ");
					}					
				}
			}
			else if ( attacker.Hits < (attacker.HitsMax / 3) && aichange)
			{
				((BaseCreature)this).AI = AIType.AI_Mage; // hone in for the kill
				aichange = false;
				this.Combatant = attacker;
			}
				
			base.OnGotMeleeAttack(attacker);
		}

/*
		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile MorphedInto
		{
			get { return m_MorphedInto; }
			set
			{
				if ( value == this )
					value = null;

				if ( m_MorphedInto != value )
				{
					Revert();

					if ( value != null )
					{

						Morph( value );
						m_LastMorph = DateTime.UtcNow;
					}

					m_MorphedInto = value;
					Delta( MobileDelta.Noto );
				}
			}
		}

        public override bool CheckIdle()
		{
			bool idle = base.CheckIdle();

			if ( idle && m_MorphedInto != null && DateTime.UtcNow - m_LastMorph > TimeSpan.FromSeconds( 30 ) )
				MorphedInto = null;

			return idle;
		}

		protected virtual void Morph( Mobile m )
		{

			Body = m.Body;
			Hue = m.Hue;
			Female = m.Female;
			Name = m.Name;
			NameHue = m.NameHue;
			Title = m.Title;
			Kills = m.Kills;
			HairItemID = m.HairItemID;
			HairHue = m.HairHue;
			FacialHairItemID = m.FacialHairItemID;
			FacialHairHue = m.FacialHairHue;

			// TODO: Skills?

			foreach ( Item item in m.Items )
			{
				if ( item.Layer != Layer.Backpack && item.Layer != Layer.Mount && item.Layer != Layer.Bank )
					AddItem( new ClonedItem( item ) ); // TODO: Clone weapon/armor attributes
			}

			PlaySound( 0x511 );
			FixedParticles( 0x376A, 1, 14, 5045, EffectLayer.Waist );
		}

		protected virtual void Revert()
		{
			if (this is Honorae)
			{
				Body = 93;
				Hue = 1979;
			}
			if (this is Praetor)
			{
				Body = 970;
				Hue = 1974;
			}
			Hue = ( IsParagon && DefaultHue == 0 ) ? Paragon.Hue : DefaultHue;
			Female = false;
			Name = DefaultName;
			NameHue = -1;
			Title = null;
			Kills = 0;
			HairItemID = 0;
			HairHue = 0;
			FacialHairItemID = 0;
			FacialHairHue = 0;

			DeleteClonedItems();

			PlaySound( 0x511 );
			FixedParticles( 0x376A, 1, 14, 5045, EffectLayer.Waist );
			MorphedInto = null;
		}

		public void DeleteClonedItems()
		{
			for ( int i = Items.Count - 1; i >= 0; --i )
			{
				Item item = Items[i];

				if ( item is ClonedItem )
					item.Delete();
			}

			if ( Backpack != null )
			{
				for ( int i = Backpack.Items.Count - 1; i >= 0; --i )
				{
					Item item = Backpack.Items[i];

					if ( item is ClonedItem )
						item.Delete();
				}
			}
		}

		public override void OnAfterDelete()
		{
			DeleteClonedItems();

			base.OnAfterDelete();
		}
		
		private class ClonedItem : Item
		{
			public ClonedItem( Item item )
				: base( item.ItemID )
			{
				Name = item.Name;
				Weight = item.Weight;
				Hue = item.Hue;
				Layer = item.Layer;
				Movable = false;
			}

			public ClonedItem( Serial serial )
				: base( serial )
			{
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
			}
		}

*/
        public void OneTimeTick()
        {

			if (((BaseCursed)this).restimer > 20 )
			{
				if ( !((BaseCursed)this).res )
					((BaseCursed)this).res = true;
				((BaseCursed)this).restimer = 0;
			}
			else
				((BaseCursed)this).restimer += 1;
			
			if (((BaseCursed)this).talkingtimer > 15)
			{
				if ( !((BaseCursed)this).talk )
					((BaseCursed)this).talk = true;
				((BaseCursed)this).talkingtimer = 0;
			}
			else
				((BaseCursed)this).talkingtimer += 1;

			if (this.Hits >= (this.HitsMax /2) && Utility.RandomDouble() >= 0.80 && !((BaseCursed)this).aichange) // things are good, chance to open ai to change
				((BaseCursed)this).aichange = true;

			if ( !((BaseCursed)this).drainstart && this.Combatant != null && this.Hits < this.HitsMax && Utility.RandomDouble() > 0.94 ) // random chance to start drain link
				((BaseCursed)this).drainstart = true;

			if (((BaseCursed)this).drainstart && this.Combatant != null)
			{
				Mobile target = this.Combatant;

				if (this.Hits <= this.HitsMax && !target.Hidden && this.CanSee( target ) && InLOS( target ) && target.Hits > 0) // link is active
				{
					if (((BaseCursed)this).absorb <= 25)
						((BaseCursed)this).absorb +=1;
				
					this.Hits += ((BaseCursed)this).absorb * 3; // transfer of health
					target.Hits -= ((BaseCursed)this).absorb * 3;
					
					if (Utility.RandomBool())
					{
						if (Utility.RandomBool())
							this.Say("Voluptuaem!");
						else 
							this.Say("Sum Vivus!");
					}
					
					if ( Utility.RandomDouble() > 0.80)
					{
						int amount = ( target.Mana * (100 - Utility.RandomMinMax( 50, 90 )) ) / 100;
						target.Mana -= amount;
						this.Mana += amount;
					}
					else if (Utility.RandomDouble() > 0.80)
					{
						int amount = ( target.Stam * (100 - Utility.RandomMinMax( 50, 100 )) ) / 100;
						target.Stam -= amount;
						this.Stam += amount;
					}	
					
					if ( target.Hits  <= 0 || Utility.RandomDouble() > 0.90 ) // target dead or 90% chance to lose the drain link
					{
						((BaseCursed)this).drainstart = false;
						((BaseCursed)this).absorb = 0;
					}
					
				}
				else // lost the link
				{
					((BaseCursed)this).absorb = 0;
					((BaseCursed)this).drainstart = false;
				}
			}
		}

        public void NoTames( BaseCreature pet, Mobile target )
		{
			if ( pet == null || target == null || Utility.RandomDouble() < 0.33  )
				return;

            else if ( pet.Controlled && pet.CanBeHarmful(target) && Utility.RandomDouble() > 0.666)
            {
				if (this is Honorae || this is Praetor )
				{
					switch (Utility.Random(3))
					{
						case 0: this.Say("Traderet Dominum Tuum!"); break;
						case 1: this.Say("Te Oblitus!"); break;
						case 2: this.Say("Amici Hostibus!"); break;
					}
				}
					
                pet.Provoke(pet, target, true);
            }
		}		

		public virtual void OfferResurrection( Mobile m )
		{
			
			if (m.Criminal || m.Karma <= -500)
			{
				if (this is Honorae || this is Praetor)
				{
					Direction = GetDirectionTo( m );

					m.PlaySound( 0x214 );
					m.FixedEffect( 0x376A, 10, 16 );

					m.CloseGump( typeof( ResurrectCostGump ) );
					m.SendGump( new ResurrectCostGump( m, 1 ) );
				}
				else 
				{
					Direction = GetDirectionTo( m );
					Say("An Corp");

					m.PlaySound(0x1F2);
					m.FixedEffect( 0x376A, 10, 16 );
					
					m.Resurrect();
				}
			}
		}

		public virtual void OfferHeal( PlayerMobile m )
		{
			Direction = GetDirectionTo( m );

			//if ( m.CheckYoungHealTime() )
			if ( ((BaseCursed)this).res && m.Criminal && m.Karma <= -5000 && !m.Hidden)
			{
				Say("Ego auxiliatus sum tibi"); 
				Say("In Vas Mani");

				m.PlaySound( 0x1F2 );
				m.FixedEffect( 0x376A, 9, 32 );

				m.Hits = (m.Hits + 50);
				((BaseCursed)this).res = false;
			}
			else
			{
				Say("Fortes estis"); 
			}

		}
		
		public virtual void AntiArcher( Mobile archer )
		{
			if ( ((BaseCursed)this).aichange )
			{
					((BaseCreature)this).AI = AIType.AI_Melee;
					((BaseCursed)this).aichange = false;
					this.Combatant = archer;
			}
			
			if (Utility.RandomDouble() > 0.80) // 80% chance of teleporting to archer
			{
				Map map = this.Map;

				if ( map == null )
					return;

				int offset = Utility.Random( 8 ) * 2;

				Point3D to = archer.Location;

				for ( int i = 0; i < m_Offsets.Length; i += 2 )
					{
						int x = archer.X + m_Offsets[(offset + i) % m_Offsets.Length];
						int y = archer.Y + m_Offsets[(offset + i + 1) % m_Offsets.Length];

						if ( map.CanSpawnMobile( x, y, archer.Z ) )
						{
							to = new Point3D( x, y, archer.Z );
							break;
						}
						else
						{
							int z = map.GetAverageZ( x, y );

							if ( map.CanSpawnMobile( x, y, z ) )
							{
								to = new Point3D( x, y, z );
								break;
							}
						}
					}

					Point3D from = this.Location;

					this.Location = to;

					Server.Spells.SpellHelper.Turn( this, archer );
					Server.Spells.SpellHelper.Turn( archer, this );

					this.ProcessDelta();

					this.PlaySound( 0x58D );
			}
		}

		private static int[] m_Offsets = new int[]
			{
				-1, -1,
				-1,  0,
				-1,  1,
				0, -1,
				0,  1,
				1, -1,
				1,  0,
				1,  1
			};
			
		public override bool OnBeforeDeath()
		{
			if (Utility.RandomDouble() < 0.60 && !(this is DemonKnight))
			{
				this.HitsMaxSeed -= 200;
				if (this.HitsMaxSeed < 100)
					this.HitsMaxSeed = 100;
				this.Hits = this.HitsMaxSeed;
				this.Mana = this.ManaMax;
				this.Stam = this.StamMax;
				if (Utility.RandomBool())
					this.Say("Mors in me non habet quicquam.");
				if (Utility.RandomBool())
					this.Say("Fatuus vobis!");			
				return false;
			}
			
			base.OnBeforeDeath();
			return true;
		}
		
		
		public BaseCursed( Serial serial ) : base( serial ) 
		{ 
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
			m_OneTimeType = 3;
			AIFullSpeedActive = AIFullSpeedPassive = true; // Force full speed
		}

	}
}

