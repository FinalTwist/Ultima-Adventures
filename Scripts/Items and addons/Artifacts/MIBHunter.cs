using System;
using Server.Items;
using Server.Network;
using Server.Spells;
using Server.Mobiles;

namespace Server.Items
{
	public class MIBHunter : BaseRanged
	{
		public override int EffectID{ get{ return 0x528A; } }
		public override Type AmmoType{ get{ return typeof( HarpoonRope ); } }
		public override Item Ammo{ get{ return new HarpoonRope(); } }

		public override int DefHitSound{ get{ return 0x5D2; } }
		public override int DefMissSound{ get{ return 0x5D3; } }

		public override SkillName DefSkill{ get{ return SkillName.Archery; } }
		public override WeaponType DefType{ get{ return WeaponType.Ranged; } }
		public override WeaponAnimation DefAnimation{ get{ return WeaponAnimation.Pierce2H; } }

		public override SkillName AccuracySkill{ get{ return SkillName.Archery; } }

		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.ParalyzingBlow; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.MortalStrike; } }
		public override WeaponAbility ThirdAbility{ get{ return WeaponAbility.MovingShot; } }
		public override WeaponAbility FourthAbility{ get{ return WeaponAbility.DoubleShot; } }
		public override WeaponAbility FifthAbility{ get{ return WeaponAbility.InfectiousStrike; } }

		public override int AosStrengthReq{ get{ return 20; } }
		public override int AosMinDamage{ get{ return Core.ML ? 15 : 16; } }
		public override int AosMaxDamage{ get{ return Core.ML ? 19 : 18; } }
		public override int AosSpeed{ get{ return 25; } }
		public override float MlSpeed{ get{ return 5.00f; } }

		public override int OldStrengthReq{ get{ return 15; } }
		public override int OldMinDamage{ get{ return 9; } }
		public override int OldMaxDamage{ get{ return 41; } }
		public override int OldSpeed{ get{ return 20; } }

		public override int DefMaxRange{ get{ return 15; } }

		public override int InitMinHits{ get{ return 50; } }
		public override int InitMaxHits{ get{ return 125; } }

		[Constructable]
		public MIBHunter() : base( 0xF63 )
		{
			Name = "The Mighty MIB Hunter";
			Weight = 7.0;
			Layer = Layer.TwoHanded;

			//WeaponAttributes.DurabilityBonus = 0; // Pick and choose the attributes for your weapon (remember to remove the // before the ones you entend to use)
			//WeaponAttributes.HitColdArea = 0;
			//WeaponAttributes.HitDispel = 10;
			WeaponAttributes.HitEnergyArea = 25;
			//WeaponAttributes.HitFireArea = 0;
			//WeaponAttributes.HitFireball = 25;
			WeaponAttributes.HitHarm = 25;
			WeaponAttributes.HitLeechHits = 20;
			//WeaponAttributes.HitLeechMana = 20;
			WeaponAttributes.HitLeechStam = 20;                                   
			//WeaponAttributes.HitLightning = 50;
			//WeaponAttributes.HitLowerAttack = 0;
			WeaponAttributes.HitLowerDefend = 25;
			//WeaponAttributes.HitMagicArrow = 0;
			//WeaponAttributes.HitPhysicalArea = 10;
			//WeaponAttributes.HitPoisonArea = 0;
			WeaponAttributes.LowerStatReq = 20;
			//WeaponAttributes.MageWeapon = 1;    
			//WeaponAttributes.ResistColdBonus = 12;
			//WeaponAttributes.ResistEnergyBonus = 2;
			//WeaponAttributes.ResistPhysicalBonus = 4;
			//WeaponAttributes.ResistFireBonus = 15;
			//WeaponAttributes.ResistPoisonBonus = 2;
			WeaponAttributes.SelfRepair = 1;

			Attributes.AttackChance = 15;
			Attributes.BonusDex = 10;
			//Attributes.BonusHits = 20;
			//Attributes.BonusInt = 20;
			//Attributes.BonusMana = 10;
			Attributes.BonusStam = 10;
			Attributes.BonusStr = 5;
			//Attributes.CastRecovery = 2;
			//Attributes.CastSpeed = 2;
			//Attributes.DefendChance = 2;
			//Attributes.EnhancePotions = 0;
			//Attributes.LowerRegCost = 100;
			Attributes.Luck = 100;
			Attributes.SpellChanneling = 1;
			//Attributes.SpellDamage = 10;
		}

		public override bool OnEquip( Mobile from )
		{
			from.SendMessage( "This is a throwing weapon that requires harpoon ropes to throw." );
			return base.OnEquip( from );
		}

		public override TimeSpan OnSwing( Mobile attacker, Mobile defender )
		{
			WeaponAbility a = WeaponAbility.GetCurrentAbility( attacker );

			// Make sure we've been standing still for .25/.5/1 second depending on Era
			if (Core.TickCount > (attacker.LastMoveTime + (Core.SE ? 250 : (Core.AOS ? 500 : 1000) )) || (Core.AOS && WeaponAbility.GetCurrentAbility( attacker ) is MovingShot) )
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
			base.OnHit( attacker, defender, damageBonus );
		}

		public override void OnMiss( Mobile attacker, Mobile defender )
		{
			base.OnMiss( attacker, defender );
		}

		public virtual bool OnFired( Mobile attacker, Mobile defender )
		{
			BaseQuiver quiver = attacker.FindItemOnLayer( Layer.Cloak ) as BaseQuiver;
			Container pack = attacker.Backpack;

			if ( attacker.Player )
			{
				if ( quiver == null || quiver.LowerAmmoCost == 0 || quiver.LowerAmmoCost > Utility.Random( 100 ) )
				{
					if ( quiver != null && quiver.ConsumeTotal( AmmoType, 1 ) )
						quiver.InvalidateWeight();
					else if ( pack == null || !pack.ConsumeTotal( AmmoType, 1 ) )
						return false;
				}
			}

			attacker.MovingEffect( defender, EffectID, 18, 1, false, false );

			return true;
		}

		public MIBHunter( Serial serial ) : base( serial )
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
/*
	public class HarpoonRope : Item
	{
		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		[Constructable]
		public HarpoonRope() : this( 1 )
		{
		}

		[Constructable]
		public HarpoonRope( int amount ) : base( 0x52B1 )
		{
			Name = "harpoon rope";
			Stackable = true;
			Amount = amount;
		}

		public HarpoonRope( Serial serial ) : base( serial )
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
	}*/
}

