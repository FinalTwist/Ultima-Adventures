using System;
using System.Collections;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
	[CorpseName( "an insect corpse" )] // TODO: Corpse name?
	public class Shaclaw : BaseCreature
	{
		[Constructable]
		public Shaclaw() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a shaclaw";
			Body = 135;
			BaseSoundID = 959;

			SetStr( 796, 825 );
			SetDex( 86, 105 );
			SetInt( 436, 475 );

			SetHits( 478, 495 );

			SetDamage( 16, 22 );

			SetDamageType( ResistanceType.Physical, 70 );
			SetDamageType( ResistanceType.Poison, 30 );

			SetResistance( ResistanceType.Physical, 55, 65 );
			SetResistance( ResistanceType.Fire, 60, 70 );
			SetResistance( ResistanceType.Cold, 30, 40 );
			SetResistance( ResistanceType.Poison, 35, 40 );
			SetResistance( ResistanceType.Energy, 35, 45 );

			SetSkill( SkillName.MagicResist, 49.1, 60.0 );
			SetSkill( SkillName.Tactics, 97.6, 100.0 );
			SetSkill( SkillName.Wrestling, 90.1, 92.5 );

			Fame = 14000;
			Karma = -14000;

			VirtualArmor = 60;

			AddItem( new LighterSource() );

			Item Venom = new VenomSack();
				Venom.Name = "deadly venom sack";
				AddItem( Venom );
		}

		public override void OnGotMeleeAttack( Mobile attacker )
		{
			base.OnGotMeleeAttack( attacker );

			if ( Utility.RandomMinMax( 1, 4 ) == 1 )
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

			if ( Utility.RandomMinMax( 1, 4 ) == 1 )
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
			this.BaseSoundID = 278;
			this.PlaySound( 0x580 );
			
			Effects.SendLocationEffect( this.Location, this.Map, 0x23B2, 16, 10, 1166, 0 );

			int goo = 0;

			foreach ( Item splash in this.GetItemsInRange( 10 ) ){ if ( splash is MonsterSplatter && splash.Name == "acidic ichor" ){ goo++; } }

			if ( goo == 0 )
			{
				MonsterSplatter.AddSplatter( this.X, this.Y, this.Z, this.Map, this.Location, this, "acidic ichor", 1167, 0 );
			}

			return base.OnBeforeDeath();
		}

		public override int GetAngerSound()
		{
			return 0x621;
		}

		public override int GetIdleSound()
		{
			return 0x259;
		}

		public override int GetAttackSound()
		{
			return 0x61E;
		}

		public override int GetHurtSound()
		{
			return 0x620;
		}

		public override int GetDeathSound()
		{
			return 0x61F;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 2 );
			AddLoot( LootPack.Gems, 8 );
		}

		public Shaclaw( Serial serial ) : base( serial )
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