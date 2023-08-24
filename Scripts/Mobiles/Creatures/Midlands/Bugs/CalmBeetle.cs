using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.ContextMenus;
using Server.Misc;

namespace Server.Mobiles
{
	[CorpseName( "a docile beetle corpse" )]
	public class CalmBeetle : BaseMount
	{
		public virtual double BoostedSpeed{ get{ return 0.1; } }

		[Constructable]
		public CalmBeetle() : this( "a docile beetle" )
		{
		}

		public override bool ReduceSpeedWithDamage{ get{ return false; } }

		[Constructable]
		public CalmBeetle( string name ) : base( name, 0xA9, 0x3E95, AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			Hue = 1986;

			BaseSoundID = 0x270;

			SetStr( 94, 170 );
			SetDex( 56, 75 );
			SetInt( 6, 10 );

			SetHits( 71, 88 );
			SetMana( 0 );

			SetDamage( 8, 14 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 15, 20 );
			SetResistance( ResistanceType.Cold, 15, 20 );

			SetSkill( SkillName.MagicResist, 27.1, 32.0 );
			SetSkill( SkillName.Tactics, 29.3, 44.0 );
			SetSkill( SkillName.Wrestling, 29.3, 44.0 );

			Fame = 450;
			Karma = 0;

			Tamable = true;
			ControlSlots = 2;
			MinTameSkill = 45.1;

			Container pack = Backpack;

			if ( pack != null )
				pack.Delete();

			pack = new StrongBackpack();
			pack.Movable = false;

			AddItem( pack );
		}

		public override int GetAngerSound()
		{
			return 0x21D;
		}

		public override int GetIdleSound()
		{
			return 0x21D;
		}

		public override int GetAttackSound()
		{
			return 0x162;
		}

		public override int GetHurtSound()
		{
			return 0x163;
		}

		public override int GetDeathSound()
		{
			return 0x21D;
		}

		public override FoodType FavoriteFood{ get{ return FoodType.Meat; } }

		public CalmBeetle( Serial serial ) : base( serial )
		{
		}

		public override void OnHarmfulSpell( Mobile from )
		{
			if ( !Controlled && ControlMaster == null )
				CurrentSpeed = BoostedSpeed;
		}

		public override void OnCombatantChange()
		{
			if ( Combatant == null && !Controlled && ControlMaster == null )
				CurrentSpeed = PassiveSpeed;
		}

		#region Pack Animal Methods
		public override bool OnBeforeDeath()
		{
			if ( !base.OnBeforeDeath() )
				return false;

			PackAnimal.CombineBackpacks( this );

			return true;
		}
		
		public override void OnDeath( Container c )
        {
            if (AdventuresFunctions.IsPuritain((object)this))
            {
                //set these for each mob
                double odds = 0.50; // 0 - 1 max (e.g. 0.50 is 50%)
                int amount = Utility.RandomMinMax(5, 10); // the min/max for the amount of the reg

                if (odds >= Utility.RandomDouble() && this.Backpack != null)
                {
                    //change the reg type here available are 
                    //RandomNecromancyReagent()
                    //RandomReagent();
                    //RandomMixerReagent()
                    //RandomMixerReagent()
                    //RandomHerbReagent()
                    Item reg = Loot.RandomReagent(); 
                    reg.Amount = amount;

                    if (reg != null)
                        this.Backpack.DropItem(reg);
                }
            }


            base.OnDeath(c);
        }

		public override DeathMoveResult GetInventoryMoveResultFor( Item item )
		{
			return DeathMoveResult.MoveToCorpse;
		}

		public override bool IsSnoop( Mobile from )
		{
			if ( PackAnimal.CheckAccess( this, from ) )
				return false;

			return base.IsSnoop( from );
		}

		public override bool OnDragDrop( Mobile from, Item item )
		{
			if ( CheckFeed( from, item ) )
				return true;

			if ( PackAnimal.CheckAccess( this, from ) )
			{
				AddToBackpack( item );
				return true;
			}

			return base.OnDragDrop( from, item );
		}

		public override bool CheckNonlocalDrop( Mobile from, Item item, Item target )
		{
			return PackAnimal.CheckAccess( this, from );
		}

		public override bool CheckNonlocalLift( Mobile from, Item item )
		{
			return PackAnimal.CheckAccess( this, from );
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
			base.GetContextMenuEntries( from, list );

			PackAnimal.GetContextMenuEntries( this, from, list );
		}
		#endregion

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			Body = 0xA9;
			ItemID = 0x3E95;
			Hue = 1986;
		}
	}
}