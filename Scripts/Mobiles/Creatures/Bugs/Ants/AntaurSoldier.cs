using System;
using System.Collections;
using Server.Items;
using Server.Network;

namespace Server.Mobiles
{
	[CorpseName( "an antaur corpse" )]
	public class AntaurSoldier : BaseCreature
	{
		[Constructable]
		public AntaurSoldier() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "an antaur soldier";
			Body = 782;
			BaseSoundID = 959;
			Hue = 2601;

			SetStr( 196, 220 );
			SetDex( 101, 125 );
			SetInt( 36, 60 );

			SetHits( 96, 107 );

			SetDamage( 5, 15 );

			SetDamageType( ResistanceType.Physical, 80 );
			SetDamageType( ResistanceType.Poison, 20 );

			SetResistance( ResistanceType.Physical, 20, 35 );
			SetResistance( ResistanceType.Fire, 20, 35 );
			SetResistance( ResistanceType.Cold, 10, 25 );
			SetResistance( ResistanceType.Poison, 20, 35 );
			SetResistance( ResistanceType.Energy, 10, 25 );

			SetSkill( SkillName.MagicResist, 60.0 );
			SetSkill( SkillName.Tactics, 80.0 );
			SetSkill( SkillName.Wrestling, 80.0 );

			Fame = 3000;
			Karma = -3000;

			VirtualArmor = 35;

			Item Venom = new VenomSack();
				Venom.Name = "venom sack";
				AddItem( Venom );
		}

		public override void OnGotMeleeAttack( Mobile attacker )
		{
			base.OnGotMeleeAttack( attacker );

			if ( Utility.RandomMinMax( 1, 8 ) == 1 )
			{
				int goo = 0;

				foreach ( Item splash in this.GetItemsInRange( 10 ) ){ if ( splash is MonsterSplatter && splash.Name == "acidic ichor" ){ goo++; } }

				if ( goo == 0 )
				{
					MonsterSplatter.AddSplatter( this.X, this.Y, this.Z, this.Map, this.Location, this, "acidic ichor", 1167, 0 );
				}
			}
		}

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );

			if ( Utility.RandomMinMax( 1, 8 ) == 1 )
			{
				Item acid = new BottleOfAcid();
				acid.Name = "jar of acidic ichor";
				acid.ItemID = 0x1007;
				acid.Hue = 0xB96;
				c.DropItem( acid );
			}
		}

		public override bool OnBeforeDeath()
		{
			if ( Utility.RandomMinMax( 1, 2 ) == 1 )
			{
				this.Body = 16;
				this.BaseSoundID = 278;
				this.Hue = 1167;
				this.PlaySound( 0x580 );
				Effects.SendLocationEffect( this.Location, this.Map, 0x23B2, 16, 10, 1166, 0 );

				if ( Utility.RandomMinMax( 1, 7 ) == 1 )
				{
					int goo = 0;

					foreach ( Item splash in this.GetItemsInRange( 10 ) ){ if ( splash is MonsterSplatter && splash.Name == "acidic ichor" ){ goo++; } }

					if ( goo == 0 )
					{
						MonsterSplatter.AddSplatter( this.X, this.Y, this.Z, this.Map, this.Location, this, "acidic ichor", 1167, 0 );
					}
				}
			}
			return base.OnBeforeDeath();
		}

		public override int GetAngerSound()
		{
			return 0xB5;
		}

		public override int GetIdleSound()
		{
			return 0xB5;
		}

		public override int GetAttackSound()
		{
			return 0x289;
		}

		public override int GetHurtSound()
		{
			return 0xBC;
		}

		public override int GetDeathSound()
		{
			return 0xE4;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
		}

		public AntaurSoldier( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 1 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}