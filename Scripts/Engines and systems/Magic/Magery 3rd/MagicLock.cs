using System;
using Server;
using Server.Misc;
using Server.Targeting;
using Server.Network;
using Server.Items;
using Server.Mobiles;
using System.Collections;
using Felladrin.Automations;

namespace Server.Spells.Third
{
	public class MagicLockSpell : MagerySpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Magic Lock", "An Por",
				215,
				9001,
				Reagent.Garlic,
				Reagent.Bloodmoss,
				Reagent.SulfurousAsh
			);

		public override SpellCircle Circle { get { return SpellCircle.Third; } }

		public MagicLockSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

		public void Target( object o )
		{
			if ( o is Item )
			{
				Item targ = (Item)o;

				if ( targ is LockableContainer )
				{
					LockableContainer box = (LockableContainer)targ;
					if ( Multis.BaseHouse.CheckLockedDownOrSecured( box ) )
					{
						// You cannot cast this on a locked down item.
						Caster.LocalOverheadMessage( MessageType.Regular, 0x22, 501761 );
					}
					else if ( box.Locked || box.LockLevel == 0 || box is ParagonChest )
					{
						// Target must be an unlocked chest.
						Caster.SendLocalizedMessage( 501762 );
					}
					else if ( CheckSequence() )
					{
						SpellHelper.Turn( Caster, box );

						Point3D loc = box.GetWorldLocation();

						Effects.SendLocationParticles(
							EffectItem.Create( loc, box.Map, EffectItem.DefaultDuration ),
							0x376A, 9, 32, Server.Items.CharacterDatabase.GetMySpellHue( Caster, 0 ), 0, 5020, 0 );

						Effects.PlaySound( loc, box.Map, 0x1FA );

						// The chest is now locked!
						Caster.LocalOverheadMessage( MessageType.Regular, 0x3B2, 501763 );

						box.LockLevel = (int)(Caster.Skills[SkillName.Magery].Value);
							if ( box.LockLevel > 90 ){ box.LockLevel = 90; }
							if ( box.LockLevel < 0 ){ box.LockLevel = 0; }
						box.MaxLockLevel = box.LockLevel + 20;
						box.RequiredSkill = box.LockLevel;

						//box.LockLevel = -255; // signal magic lock
						box.Locked = true;
					}
				}
				else if ( targ is BaseDoor )
				{
					BaseDoor door = (BaseDoor)targ;
					if ( Server.Items.DoorType.IsDungeonDoor( door ) )
					{
						if ( door.Locked == true )
							Caster.SendMessage( "That door is already locked!" );
						else
						{
							SpellHelper.Turn( Caster, door );

							Point3D loc = door.GetWorldLocation();

							Effects.SendLocationParticles(
								EffectItem.Create( loc, door.Map, EffectItem.DefaultDuration ),
								0x376A, 9, 32, Server.Items.CharacterDatabase.GetMySpellHue( Caster, 0 ), 0, 5020, 0 );

							Effects.PlaySound( loc, door.Map, 0x1FA );

							Caster.SendMessage( "That door is now locked!" );

							door.Locked = true;
							Server.Items.DoorType.LockDoors( door );

							new InternalTimer( door, Caster ).Start();
						}
					}
					else
						Caster.SendMessage( "This spell has no effect on that!" );
				}
				else
				{
					Caster.SendMessage( "This spell has no effect on that!" );
				}
				
			}
			else if ( o is PlayerMobile )
			{
				Caster.SendMessage( "That soul seems too strong to trap in the flask!" );
			}
			else if ( o is BaseCreature )
			{
				BaseCreature bc = (BaseCreature)o;

				if ( !bc.Alive )
				{
					Caster.SendMessage( "You cannot trap something that is dead!" );
				}
				else if ( o is LockedCreature )
				{
					Caster.SendMessage( "That creature cannot be trapped again!" );
				}
				else if ( bc.Controlled )
				{
					Caster.SendMessage( "That is under the control of another!" );
				}
				else if ( bc.Blessed )
				{
					Caster.SendMessage( "That is protected by a mysterious aura!" );
				}
				else if ( bc.IsHitchStabled )
				{
					Caster.SendMessage( "That is secured by the post." );
				}
				else if ( (double)bc.Fame / 35000 > (double)Caster.Fame / 15000 )
				{
					Caster.SendMessage( "You fail... That creature's soul is stronger than yours." );
				}
				else if ( bc is CloneCharacterOnLogout.CharacterClone )
				{
					Caster.SendMessage( "This player is protected by a mysterious aura." );
				}
				else if ( bc is BaseVendor && ((BaseVendor)bc).IsInvulnerable )
				{
					Caster.SendMessage( "This appears to be protected in some way." );
				}
				else if ( bc.EmoteHue == 505 || bc.ControlSlots >= 100 ) // SUMMON QUEST AND QUEST MONSTERS
				{
					Server.Misc.IntelligentAction.FizzleSpell( Caster );
					Caster.SendMessage( "You are not powerful enough to trap that!" );
				}
				else if (bc is EpicCharacter || bc is TimeLord )
				{
					bc.Say("Do you think you can capture me??  Let me show you what I can do to you, ant.");
					Caster.SendMessage( "You feel a powerful force hit you!" );
					Caster.Kill();
				}
				else if ( Caster.Backpack.FindItemByType( typeof ( IronFlask ) ) == null )
				{
					Caster.SendMessage( "You need an empty iron flask to trap them!" );
				}
				else
				{
					int level = Server.Misc.IntelligentAction.GetCreatureLevel( (Mobile)o ) + 10;
					int magery = (int)(Caster.Skills[SkillName.Magery].Value);

					if ( magery >= level )
					{
						int success = 10 + ( magery - level );

						if ( Utility.RandomMinMax( 1, 100 ) > success )
						{
							Server.Misc.IntelligentAction.FizzleSpell( Caster );
							Caster.SendMessage( "You fail to trap them in the flask!" );
						}
						else
						{
							Item flask = Caster.Backpack.FindItemByType( typeof ( IronFlask ) );
							if ( flask != null ){ flask.Consume(); }

							Caster.SendMessage( "You trap " + bc.Name + " in the flask!" );

							IronFlaskFilled bottle = new IronFlaskFilled();

							int hitpoison = 0;

							if ( bc.HitPoison == Poison.Lesser ){ hitpoison = 1; }
							else if ( bc.HitPoison == Poison.Regular ){ hitpoison = 2; }
							else if ( bc.HitPoison == Poison.Greater ){ hitpoison = 3; }
							else if ( bc.HitPoison == Poison.Deadly ){ hitpoison = 4; }
							else if ( bc.HitPoison == Poison.Lethal ){ hitpoison = 5; }

							int immune = 0;

							if ( bc.PoisonImmune == Poison.Lesser ){ immune = 1; }
							else if ( bc.PoisonImmune == Poison.Regular ){ immune = 2; }
							else if ( bc.PoisonImmune == Poison.Greater ){ immune = 3; }
							else if ( bc.PoisonImmune == Poison.Deadly ){ immune = 4; }
							else if ( bc.PoisonImmune == Poison.Lethal ){ immune = 5; }

							bottle.TrappedName = bc.Name;
							bottle.TrappedTitle = bc.Title;
							bottle.TrappedBody = bc.Body;
							bottle.TrappedBaseSoundID = bc.BaseSoundID;
							bottle.TrappedHue = bc.Hue;

								// TURN HUMANOIDS TO GHOSTS SO I DON'T NEED TO WORRY ABOUT CLOTHES AND GEAR
								if ( bc.Body == 400 || bc.Body == 401 || bc.Body == 605 || bc.Body == 606 )
								{
									bottle.TrappedBody = 0x3CA;
									bottle.TrappedHue = 0x9C4;
									bottle.TrappedBaseSoundID = 0x482;
								}

							bottle.TrappedAI = 2; if ( bc.AI == AIType.AI_Mage ){ bottle.TrappedAI = 1; }
							bottle.TrappedStr = bc.RawStr;
							bottle.TrappedDex = bc.RawDex;
							bottle.TrappedInt = bc.RawInt;
							bottle.TrappedHits = bc.HitsMax;
							bottle.TrappedStam = bc.StamMax;
							bottle.TrappedMana = bc.ManaMax;
							bottle.TrappedDmgMin = bc.DamageMin;
							bottle.TrappedDmgMax = bc.DamageMax;
							bottle.TrappedColdDmg = bc.ColdDamage;
							bottle.TrappedEnergyDmg = bc.EnergyDamage;
							bottle.TrappedFireDmg = bc.FireDamage;
							bottle.TrappedPhysicalDmg = bc.PhysicalDamage;
							bottle.TrappedPoisonDmg = bc.PoisonDamage;
							bottle.TrappedColdRst = bc.ColdResistSeed;
							bottle.TrappedEnergyRst = bc.EnergyResistSeed;
							bottle.TrappedFireRst = bc.FireResistSeed;
							bottle.TrappedPhysicalRst = bc.PhysicalResistanceSeed;
							bottle.TrappedPoisonRst = bc.PoisonResistSeed;
							bottle.TrappedVirtualArmor = bc.VirtualArmor;
							bottle.TrappedCanSwim = bc.CanSwim;
							bottle.TrappedCantWalk = bc.CantWalk;
							bottle.TrappedSkills = level + 5;
							bottle.TrappedPoison = hitpoison;
							bottle.TrappedImmune = immune;
							bottle.TrappedAngerSound = bc.GetAngerSound();
							bottle.TrappedIdleSound = bc.GetIdleSound();
							bottle.TrappedDeathSound = bc.GetDeathSound();
							bottle.TrappedAttackSound = bc.GetAttackSound();
							bottle.TrappedHurtSound = bc.GetHurtSound();

							Caster.BoltEffect( 0 );
							Caster.PlaySound(0x665);

							Caster.AddToBackpack( bottle );

							bc.BoltEffect( 0 );
							bc.PlaySound(0x665);
							bc.Delete();
						}
					}
					else
					{
						Server.Misc.IntelligentAction.FizzleSpell( Caster );
						Caster.SendMessage( "You are not powerful enough to trap that!" );
					}
				}
			}

			FinishSequence();
		}

		private class InternalTarget : Target
		{
			private MagicLockSpell m_Owner;

			public InternalTarget( MagicLockSpell owner ) : base( Core.ML ? 10 : 12, false, TargetFlags.None )
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				m_Owner.Target( o );
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}

		private class InternalTimer : Timer
		{
			private BaseDoor m_Door;

			public InternalTimer( BaseDoor door, Mobile caster ) : base( TimeSpan.FromSeconds( 0 ) )
			{
				double val = caster.Skills[SkillName.Magery].Value / 2.0;
				if ( val < 10 )
					val = 10;
				else if ( val > 60 )
					val = 60;

				m_Door = door;
				Delay = TimeSpan.FromSeconds( val );
				Priority = TimerPriority.OneSecond;
			}

			protected override void OnTick()
			{
				if ( m_Door.Locked == true )
				{
					m_Door.Locked = false;
					Server.Items.DoorType.UnlockDoors( m_Door );
					Effects.PlaySound( m_Door.Location, m_Door.Map, 0x3E4 );
				}
			}
		}
	}
}

namespace Server.Items
{
	public class IronFlask : Item
	{
		[Constructable]
		public IronFlask() : base( 0x282E )
		{
			Name = "iron flask";
			Weight = 5.0;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendMessage( "This flask is empty." );
			}
		}

		public IronFlask(Serial serial) : base(serial)
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
	public class IronFlaskFilled : Item
	{
		[Constructable]
		public IronFlaskFilled() : base( 0x282D )
		{
			Name = "iron flask";
			Weight = 5.0;
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);

			string trapped;
			string prisoner;

			if ( TrappedBody > 0 )
			{
				trapped = "Contains A Trapped Soul";
				list.Add( 1070722, trapped );

				prisoner = TrappedName;
					if ( TrappedTitle != "" && TrappedTitle != null ){ prisoner = TrappedName + " " + TrappedTitle; }
			
				list.Add( 1049644, prisoner );
			}
        }

		public override void OnDoubleClick( Mobile from )
		{
			int nFollowers = from.FollowersMax - from.Followers;

			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendMessage( "This must be in your backpack to use." );
			}
			else if ( nFollowers < 1 )
			{
				from.SendMessage("You already have enough henchman in your group.");
			}
			else if ( HenchmanFunctions.IsInRestRegion( from ) == false )
			{
				Map map = from.Map;

				int magery = (int)(from.Skills[SkillName.Magery].Value);

				BaseCreature prisoner = new LockedCreature( this.TrappedAI, this.TrappedSkills, magery, this.TrappedHits, this.TrappedStam, this.TrappedMana, this.TrappedStr, this.TrappedDex, this.TrappedInt, this.TrappedPoison, this.TrappedImmune, this.TrappedAngerSound, this.TrappedIdleSound, this.TrappedDeathSound, this.TrappedAttackSound, this.TrappedHurtSound );

				bool validLocation = false;
				Point3D loc = from.Location;

				for ( int j = 0; !validLocation && j < 10; ++j )
				{
					int x = X + Utility.Random( 3 ) - 1;
					int y = Y + Utility.Random( 3 ) - 1;
					int z = map.GetAverageZ( x, y );

					if ( validLocation = map.CanFit( x, y, this.Z, 16, false, false ) )
						loc = new Point3D( x, y, Z );
					else if ( validLocation = map.CanFit( x, y, z, 16, false, false ) )
						loc = new Point3D( x, y, z );
				}

				prisoner.ControlMaster = from;
				prisoner.Controlled = true;
				prisoner.ControlOrder = OrderType.Come;

				prisoner.Name = this.TrappedName;
				prisoner.Title = this.TrappedTitle;
				prisoner.Body = this.TrappedBody;
				prisoner.BaseSoundID = this.TrappedBaseSoundID;
				prisoner.Hue = this.TrappedHue;
				prisoner.AI = AIType.AI_Mage; if ( this.TrappedAI == 2 ){ prisoner.AI = AIType.AI_Melee; }
				prisoner.DamageMin = this.TrappedDmgMin;
				prisoner.DamageMax = this.TrappedDmgMax;
				prisoner.ColdDamage = this.TrappedColdDmg;
				prisoner.EnergyDamage = this.TrappedEnergyDmg;
				prisoner.FireDamage = this.TrappedFireDmg;
				prisoner.PhysicalDamage = this.TrappedPhysicalDmg;
				prisoner.PoisonDamage = this.TrappedPoisonDmg;
				prisoner.ColdResistSeed = this.TrappedColdRst;
				prisoner.EnergyResistSeed = this.TrappedEnergyRst;
				prisoner.FireResistSeed = this.TrappedFireRst;
				prisoner.PhysicalResistanceSeed = this.TrappedPhysicalRst;
				prisoner.PoisonResistSeed = this.TrappedPoisonRst;
				prisoner.VirtualArmor = this.TrappedVirtualArmor;
				prisoner.CanSwim = this.TrappedCanSwim;
				prisoner.CantWalk = this.TrappedCantWalk;

				from.BoltEffect( 0 );
				from.PlaySound(0x665);
				from.PlaySound(0x03E);
				prisoner.MoveToWorld( loc, map );
				from.SendMessage( "You smash the bottle, releasing " + prisoner.Name + "!" );
				this.Delete();
			}
			else
			{
				from.SendMessage("You don't think it would be a good idea to do that here.");
			}
		}

		public IronFlaskFilled(Serial serial) : base(serial)
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			writer.Write( TrappedName );
			writer.Write( TrappedTitle );
			writer.Write( TrappedBody );
			writer.Write( TrappedBaseSoundID );
			writer.Write( TrappedHue );
			writer.Write( TrappedAI );
			writer.Write( TrappedStr );
			writer.Write( TrappedDex );
			writer.Write( TrappedInt );
			writer.Write( TrappedHits );
			writer.Write( TrappedStam );
			writer.Write( TrappedMana );
			writer.Write( TrappedDmgMin );
			writer.Write( TrappedDmgMax );
			writer.Write( TrappedColdDmg );
			writer.Write( TrappedEnergyDmg );
			writer.Write( TrappedFireDmg );
			writer.Write( TrappedPhysicalDmg );
			writer.Write( TrappedPoisonDmg );
			writer.Write( TrappedColdRst );
			writer.Write( TrappedEnergyRst );
			writer.Write( TrappedFireRst );
			writer.Write( TrappedPhysicalRst );
			writer.Write( TrappedPoisonRst );
			writer.Write( TrappedVirtualArmor );
			writer.Write( TrappedCanSwim );
			writer.Write( TrappedCantWalk );
			writer.Write( TrappedSkills );
			writer.Write( TrappedPoison );
			writer.Write( TrappedImmune );
			writer.Write( TrappedAngerSound );
			writer.Write( TrappedIdleSound );
			writer.Write( TrappedDeathSound );
			writer.Write( TrappedAttackSound );
			writer.Write( TrappedHurtSound );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
            int version = reader.ReadInt();
			TrappedName = reader.ReadString();
			TrappedTitle = reader.ReadString();
			TrappedBody = reader.ReadInt();
			TrappedBaseSoundID = reader.ReadInt();
			TrappedHue = reader.ReadInt();
			TrappedAI = reader.ReadInt();
			TrappedStr = reader.ReadInt();
			TrappedDex = reader.ReadInt();
			TrappedInt = reader.ReadInt();
			TrappedHits = reader.ReadInt();
			TrappedStam = reader.ReadInt();
			TrappedMana = reader.ReadInt();
			TrappedDmgMin = reader.ReadInt();
			TrappedDmgMax = reader.ReadInt();
			TrappedColdDmg = reader.ReadInt();
			TrappedEnergyDmg = reader.ReadInt();
			TrappedFireDmg = reader.ReadInt();
			TrappedPhysicalDmg = reader.ReadInt();
			TrappedPoisonDmg = reader.ReadInt();
			TrappedColdRst = reader.ReadInt();
			TrappedEnergyRst = reader.ReadInt();
			TrappedFireRst = reader.ReadInt();
			TrappedPhysicalRst = reader.ReadInt();
			TrappedPoisonRst = reader.ReadInt();
			TrappedVirtualArmor = reader.ReadInt();
			TrappedCanSwim = reader.ReadBool();
			TrappedCantWalk = reader.ReadBool();
			TrappedSkills = reader.ReadInt();
			TrappedPoison = reader.ReadInt();
			TrappedImmune = reader.ReadInt();
			TrappedAngerSound = reader.ReadInt();
			TrappedIdleSound = reader.ReadInt();
			TrappedDeathSound = reader.ReadInt();
			TrappedAttackSound = reader.ReadInt();
			TrappedHurtSound = reader.ReadInt();
		}

		public string TrappedName;
		public string TrappedTitle;
		public int TrappedBody;
		public int TrappedBaseSoundID;
		public int TrappedHue;
		public int TrappedAI; // 1 Mage, 2 Fighter
		public int TrappedStr;
		public int TrappedDex;
		public int TrappedInt;
		public int TrappedHits;
		public int TrappedStam;
		public int TrappedMana;
		public int TrappedDmgMin;
		public int TrappedDmgMax;
		public int TrappedColdDmg;
		public int TrappedEnergyDmg;
		public int TrappedFireDmg;
		public int TrappedPhysicalDmg;
		public int TrappedPoisonDmg;
		public int TrappedColdRst;
		public int TrappedEnergyRst;
		public int TrappedFireRst;
		public int TrappedPhysicalRst;
		public int TrappedPoisonRst;
		public int TrappedVirtualArmor;
		public bool TrappedCanSwim;
		public bool TrappedCantWalk;
		public int TrappedSkills;
		public int TrappedPoison;
		public int TrappedImmune;
		public int TrappedAngerSound;
		public int TrappedIdleSound;
		public int TrappedDeathSound;
		public int TrappedAttackSound;
		public int TrappedHurtSound;

		[CommandProperty(AccessLevel.Owner)]
		public string Trapped_Name { get { return TrappedName; } set { TrappedName = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public string Trapped_Title { get { return TrappedTitle; } set { TrappedTitle = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Trapped_Body { get { return TrappedBody; } set { TrappedBody = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Trapped_BaseSoundID { get { return TrappedBaseSoundID; } set { TrappedBaseSoundID = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Trapped_Hue { get { return TrappedHue; } set { TrappedHue = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Trapped_AI { get { return TrappedAI; } set { TrappedAI = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Trapped_Str { get { return TrappedStr; } set { TrappedStr = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Trapped_Dex { get { return TrappedDex; } set { TrappedDex = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Trapped_Int { get { return TrappedInt; } set { TrappedInt = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Trapped_Hits { get { return TrappedHits; } set { TrappedHits = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Trapped_Stam { get { return TrappedStam; } set { TrappedStam = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Trapped_Mana { get { return TrappedMana; } set { TrappedMana = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Trapped_DmgMin { get { return TrappedDmgMin; } set { TrappedDmgMin = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Trapped_DmgMax { get { return TrappedDmgMax; } set { TrappedDmgMax = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Trapped_ColdDmg { get { return TrappedColdDmg; } set { TrappedColdDmg = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Trapped_EnergyDmg { get { return TrappedEnergyDmg; } set { TrappedEnergyDmg = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Trapped_FireDmg { get { return TrappedFireDmg; } set { TrappedFireDmg = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Trapped_PhysicalDmg { get { return TrappedPhysicalDmg; } set { TrappedPhysicalDmg = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Trapped_PoisonDmg { get { return TrappedPoisonDmg; } set { TrappedPoisonDmg = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Trapped_ColdRst { get { return TrappedColdRst; } set { TrappedColdRst = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Trapped_EnergyRst { get { return TrappedEnergyRst; } set { TrappedEnergyRst = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Trapped_FireRst { get { return TrappedFireRst; } set { TrappedFireRst = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Trapped_PhysicalRst { get { return TrappedPhysicalRst; } set { TrappedPhysicalRst = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Trapped_PoisonRst { get { return TrappedPoisonRst; } set { TrappedPoisonRst = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Trapped_VirtualArmor { get { return TrappedVirtualArmor; } set { TrappedVirtualArmor = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public bool Trapped_CanSwim { get { return TrappedCanSwim; } set { TrappedCanSwim = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public bool Trapped_CantWalk { get { return TrappedCantWalk; } set { TrappedCantWalk = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Trapped_Skills { get { return TrappedSkills; } set { TrappedSkills = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Trapped_Poison { get { return TrappedPoison; } set { TrappedPoison = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Trapped_Immune { get { return TrappedImmune; } set { TrappedImmune = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Trapped_AngerSound { get { return TrappedAngerSound; } set { TrappedAngerSound = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Trapped_IdleSound { get { return TrappedIdleSound; } set { TrappedIdleSound = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Trapped_DeathSound { get { return TrappedDeathSound; } set { TrappedDeathSound = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Trapped_AttackSound { get { return TrappedAttackSound; } set { TrappedAttackSound = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Trapped_HurtSound { get { return TrappedHurtSound; } set { TrappedHurtSound = value; InvalidateProperties(); } }
	}
}

namespace Server.Mobiles
{
	[CorpseName( "a corpse" )]
	public class LockedCreature : BaseCreature
	{
		public int BCPoison;
		public int BCImmune;
		public int BCAngerSound;
		public int BCIdleSound;
		public int BCDeathSound;
		public int BCAttackSound;
		public int BCHurtSound;

		public override bool DeleteCorpseOnDeath { get { return true; } }

		[Constructable]
		public LockedCreature( int job, int skills, int time, int maxhits, int maxstam, int maxmana, int str, int dex, int iq, int poison, int immune, int anger, int idle, int death, int attack, int hurt ): base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.3, 0.6 )
		{
			BCPoison = poison+0;
			BCImmune = immune+0;
			BCAngerSound = anger+0;
			BCIdleSound = idle+0;
			BCDeathSound = death+0;
			BCAttackSound = attack+0;
			BCHurtSound = hurt+0;

			Timer.DelayCall( TimeSpan.FromSeconds( (double)(10+(3*time)) ), new TimerCallback( Delete ) );

			Name = "a creature";
			Body = 2;

			SetStr( str );
			SetDex( dex);
			SetInt( iq );

			SetHits( maxhits );
			SetStam( maxstam );
			SetMana( maxmana );

			if ( job == 1 )
			{
				SetSkill( SkillName.EvalInt, (double)skills );
				SetSkill( SkillName.Magery, (double)skills );
				SetSkill( SkillName.Meditation, (double)skills );
				SetSkill( SkillName.MagicResist, (double)skills );
				SetSkill( SkillName.Wrestling, (double)skills );
			}
			else
			{
				SetSkill( SkillName.Anatomy, (double)skills );
				SetSkill( SkillName.MagicResist, (double)skills );
				SetSkill( SkillName.Tactics, (double)skills );
				SetSkill( SkillName.Wrestling, (double)skills );
			}

			Fame = 0;
			Karma = 0;

			ControlSlots = 3;
		}

		public override bool IsScaredOfScaryThings{ get{ return false; } }
		public override bool IsScaryToPets{ get{ return true; } }
		public override bool BleedImmune{ get{ return true; } }
		public override bool BardImmune{ get{ return true; } }
		public override bool ShowFameTitle{ get{ return false; } }
		public override bool AlwaysAttackable{ get{ return true; } }

        public override int GetIdleSound(){ return BCIdleSound; }
        public override int GetAngerSound(){ return BCAngerSound; }
        public override int GetHurtSound(){ return BCHurtSound; }
        public override int GetDeathSound(){ return BCDeathSound; }
        public override int GetAttackSound(){ return BCAttackSound; }

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

		public LockedCreature( Serial serial ): base( serial )
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
			Timer.DelayCall( TimeSpan.FromSeconds( 10.0 ), new TimerCallback( Delete ) );
		}
	}
}