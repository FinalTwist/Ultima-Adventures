using System;
using Server;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;
using System.Collections.Generic;
using Server.Misc;

namespace Server.Mobiles
{
	[CorpseName( "a broken machine" )]
	public class RustGolem : BaseCreature
	{
		[Constructable]
		public RustGolem() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a rust golem";
			Body = Utility.RandomList( 752, 358 );
			Hue = Utility.RandomList( 0xB97, 0xB98, 0xB99, 0xB9A, 0xB88 );

			SetStr( 151, 180 );
			SetDex( 71, 90 );
			SetInt( 31, 62 );

			SetHits( 101, 118 );

			SetDamage( 14, 20 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 30, 35 );
			SetResistance( ResistanceType.Fire, 10, 20 );
			SetResistance( ResistanceType.Cold, 10, 20 );
			SetResistance( ResistanceType.Poison, 100 );
			SetResistance( ResistanceType.Energy, 15, 25 );

			SetSkill( SkillName.MagicResist, 50.1, 95.0 );
			SetSkill( SkillName.Tactics, 60.1, 100.0 );
			SetSkill( SkillName.Wrestling, 60.1, 100.0 );

			Fame = 4250;
			Karma = -4250;

			if ( 0.1 > Utility.RandomDouble() )
				PackItem( new PowerCrystal() );

			if ( 0.15 > Utility.RandomDouble() )
				PackItem( new ClockworkAssembly() );

			if ( 0.2 > Utility.RandomDouble() )
				PackItem( new ArcaneGem() );

			if ( 0.25 > Utility.RandomDouble() )
				PackItem( new Gears( Utility.RandomMinMax( 1, 5 ) ) );

			if ( 0.25 > Utility.RandomDouble() )
				PackItem( new BottleOil( Utility.RandomMinMax( 1, 5 ) ) );

			VirtualArmor = 39;
		}

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );
			IronIngot ingut = new IronIngot();
   			ingut.Amount = Utility.RandomMinMax( 13, 21 );
   			c.DropItem(ingut);
		}

		public override int GetAngerSound()
		{
			return 541;
		}

		public override int GetIdleSound()
		{
			return 542;
		}

		public override int GetDeathSound()
		{
			return 545;
		}

		public override int GetAttackSound()
		{
			return 562;
		}

		public override int GetHurtSound()
		{
			return 320;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.Gems );
		}

		public override bool BleedImmune{ get{ return true; } }
		public override bool BardImmune { get { return !Core.SE; } }
		public override bool Unprovokable { get { return Core.SE; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		public override bool IsScaredOfScaryThings{ get{ return false; } }
		public override bool IsScaryToPets{ get{ return true; } }

		public override void OnGaveMeleeAttack( Mobile m )
		{
			base.OnGaveMeleeAttack( m );

			if ( 1 == Utility.RandomMinMax( 1, 20 ) )
			{
				Container cont = m.Backpack;
				Item iRuined = Server.Items.HiddenTrap.GetMyItem( m );

				if ( iRuined != null )
				{
					if ( Server.Items.HiddenTrap.IAmShielding( m, 100 ) || Server.Items.HiddenTrap.IAmAWeaponSlayer( m, this ) )
					{
					}
					else if ( m.CheckSkill( SkillName.MagicResist, 0, 100 ) )
					{
					}
					else if ( iRuined is BaseWeapon )
					{
						BaseWeapon iRusted = (BaseWeapon)iRuined;

						if ( Server.Misc.MaterialInfo.IsAnyKindOfMetalItem( iRuined ) )
						{
							if ( Server.Items.HiddenTrap.CheckInsuranceOnTrap( iRuined, m ) == true )
							{
								m.LocalOverheadMessage(MessageType.Emote, 1150, true, "The golem almost rusted one of your protected items!");
							}
							else
							{
								m.LocalOverheadMessage(MessageType.Emote, 0xB1F, true, "The golem rusted one of your equipped items!");
								RustyJunk broke = new RustyJunk();
								broke.ItemID = iRuined.ItemID;
								broke.Name = "rusted item";
								broke.Weight = iRuined.Weight;
								m.AddToBackpack ( broke );
								iRuined.Delete();
							}
						}
					}
					if ( iRuined is BaseArmor )
					{
						BaseArmor iRusted = (BaseArmor)iRuined;

						if ( Server.Misc.MaterialInfo.IsAnyKindOfMetalItem( iRuined ) )
						{
							if ( Server.Items.HiddenTrap.CheckInsuranceOnTrap( iRuined, m ) == true )
							{
								m.LocalOverheadMessage(MessageType.Emote, 1150, true, "The golem almost rusted one of your protected items!");
							}
							else
							{
								m.LocalOverheadMessage(MessageType.Emote, 0xB1F, true, "The golem rusted one of your equipped items!");
								RustyJunk broke = new RustyJunk();
								broke.ItemID = iRuined.ItemID;
								broke.Name = "rusted item";
								broke.Weight = Utility.RandomMinMax( 1, 4 );
								m.AddToBackpack ( broke );
								iRuined.Delete();
							}
						}
					}
				}
			}
		}

		public override void CheckReflect( Mobile caster, ref bool reflect )
		{
			if ( Utility.RandomMinMax( 1, 2 ) == 1 ){ reflect = true; } // 25% spells are reflected back to the caster
			else { reflect = false; }
		}

		public RustGolem( Serial serial ) : base( serial )
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
		}
	}
}