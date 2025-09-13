using System;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.Network;

namespace Server.Mobiles
{
	[CorpseName( "an ant lion corpse" )]
	public class AntLion : BaseCreature
	{
		[Constructable]
		public AntLion() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "an ant lion";
			Body = 787;
			BaseSoundID = 1006;

			SetStr( 296, 320 );
			SetDex( 81, 105 );
			SetInt( 36, 60 );

			SetHits( 151, 162 );

			SetDamage( 7, 21 );

			SetDamageType( ResistanceType.Physical, 70 );
			SetDamageType( ResistanceType.Poison, 30 );

			SetResistance( ResistanceType.Physical, 45, 60 );
			SetResistance( ResistanceType.Fire, 25, 35 );
			SetResistance( ResistanceType.Cold, 30, 40 );
			SetResistance( ResistanceType.Poison, 40, 50 );
			SetResistance( ResistanceType.Energy, 30, 35 );

			SetSkill( SkillName.MagicResist, 70.0 );
			SetSkill( SkillName.Tactics, 90.0 );
			SetSkill( SkillName.Wrestling, 90.0 );

			Fame = 4500;
			Karma = -4500;

			VirtualArmor = 45;

			PackItem( new Bone( 3 ) );
			PackItem( new FertileDirt( Utility.RandomMinMax( 1, 5 ) ) );

			if ( Core.ML && Utility.RandomDouble() < .33 )
				PackItem( Engines.Plants.Seed.RandomPeculiarSeed(2) );

			Item orepile = null; /* no trust, no love :( */

			switch (Utility.Random(4))
			{
				case 0: orepile = new DullCopperOre(); break;
				case 1: orepile = new ShadowIronOre(); break;
				case 2: orepile = new CopperOre(); break;
				default: orepile = new BronzeOre(); break;
			}
			orepile.Amount = Utility.RandomMinMax(1, 10);
			orepile.ItemID = 0x19B9;
			PackItem(orepile);
		}

		public override int GetAngerSound()
		{
			return 0x5A;
		}

		public override int GetIdleSound()
		{
			return 0x5A;
		}

		public override int GetAttackSound()
		{
			return 0x164;
		}

		public override int GetHurtSound()
		{
			return 0x187;
		}

		public override int GetDeathSound()
		{
			return 0x1BA;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average, 2 );
		}

		public override void RevealingAction()
		{
			if ( Hidden && CantWalk )
			{
				Point3D dirt1 = new Point3D( ( this.X ), ( this.Y ), this.Z );
				Point3D dirt2 = new Point3D( ( this.X-1 ), ( this.Y ), this.Z );
				Point3D dirt3 = new Point3D( ( this.X+1 ), ( this.Y ), this.Z );
				Point3D dirt4 = new Point3D( ( this.X ), ( this.Y-1 ), this.Z );
				Point3D dirt5 = new Point3D( ( this.X ), ( this.Y+1 ), this.Z );

				this.PlaySound( 0x65A );
				Effects.SendLocationEffect( dirt1, this.Map, Utility.RandomList( 0x36BD, 0x36B0, 0x36CB ), 16, 0xB88, 0 );
				Effects.SendLocationEffect( dirt2, this.Map, Utility.RandomList( 0x36BD, 0x36B0, 0x36CB ), 16, 0xB88, 0 );
				Effects.SendLocationEffect( dirt3, this.Map, Utility.RandomList( 0x36BD, 0x36B0, 0x36CB ), 16, 0xB88, 0 );
				Effects.SendLocationEffect( dirt4, this.Map, Utility.RandomList( 0x36BD, 0x36B0, 0x36CB ), 16, 0xB88, 0 );
				Effects.SendLocationEffect( dirt5, this.Map, Utility.RandomList( 0x36BD, 0x36B0, 0x36CB ), 16, 0xB88, 0 );
				this.CantWalk = false;
				this.Hidden = false;
			}
			base.RevealingAction();
		}

		public override void OnThink()
		{
			if ( !CantWalk && Combatant == null && !Hidden ) // DIVE UNDER GROUND AND WAIT FOR VICTIM
			{
				bool dive = true;

				foreach ( NetState state in NetState.Instances )
				{
					Mobile m = state.Mobile;

					if ( m is PlayerMobile && m.InRange( this.Location, 20 ) && m.Alive && m.Map == this.Map )
					{
						if ( m.AccessLevel == AccessLevel.Player ){ dive = false; }
					}
				}

				if ( dive )
				{
					this.Location = this.Home;

					Point3D dirt1 = new Point3D( ( this.X ), ( this.Y ), this.Z );
					Point3D dirt2 = new Point3D( ( this.X-1 ), ( this.Y ), this.Z );
					Point3D dirt3 = new Point3D( ( this.X+1 ), ( this.Y ), this.Z );
					Point3D dirt4 = new Point3D( ( this.X ), ( this.Y-1 ), this.Z );
					Point3D dirt5 = new Point3D( ( this.X ), ( this.Y+1 ), this.Z );

					this.PlaySound( 0x65A );
					Effects.SendLocationEffect( dirt1, this.Map, Utility.RandomList( 0x36BD, 0x36B0, 0x36CB ), 16, 0xB88, 0 );
					Effects.SendLocationEffect( dirt2, this.Map, Utility.RandomList( 0x36BD, 0x36B0, 0x36CB ), 16, 0xB88, 0 );
					Effects.SendLocationEffect( dirt3, this.Map, Utility.RandomList( 0x36BD, 0x36B0, 0x36CB ), 16, 0xB88, 0 );
					Effects.SendLocationEffect( dirt4, this.Map, Utility.RandomList( 0x36BD, 0x36B0, 0x36CB ), 16, 0xB88, 0 );
					Effects.SendLocationEffect( dirt5, this.Map, Utility.RandomList( 0x36BD, 0x36B0, 0x36CB ), 16, 0xB88, 0 );

					this.Warmode = false;
					this.CantWalk = true;
					this.Hidden = true;
				}
			}

			base.OnThink();
		}

		public override bool IsEnemy( Mobile m )
		{
			if ( Hidden && m is PlayerMobile )
			{
				Point3D dirt1 = new Point3D( ( this.X ), ( this.Y ), this.Z );
				Point3D dirt2 = new Point3D( ( this.X-1 ), ( this.Y ), this.Z );
				Point3D dirt3 = new Point3D( ( this.X+1 ), ( this.Y ), this.Z );
				Point3D dirt4 = new Point3D( ( this.X ), ( this.Y-1 ), this.Z );
				Point3D dirt5 = new Point3D( ( this.X ), ( this.Y+1 ), this.Z );

				this.PlaySound( 0x65A );
				Effects.SendLocationEffect( dirt1, this.Map, Utility.RandomList( 0x36BD, 0x36B0, 0x36CB ), 16, 0xB88, 0 );
				Effects.SendLocationEffect( dirt2, this.Map, Utility.RandomList( 0x36BD, 0x36B0, 0x36CB ), 16, 0xB88, 0 );
				Effects.SendLocationEffect( dirt3, this.Map, Utility.RandomList( 0x36BD, 0x36B0, 0x36CB ), 16, 0xB88, 0 );
				Effects.SendLocationEffect( dirt4, this.Map, Utility.RandomList( 0x36BD, 0x36B0, 0x36CB ), 16, 0xB88, 0 );
				Effects.SendLocationEffect( dirt5, this.Map, Utility.RandomList( 0x36BD, 0x36B0, 0x36CB ), 16, 0xB88, 0 );

				this.Warmode = true;
				this.Combatant = m;
				this.CantWalk = false;
				this.Hidden = false;

				return true;
			}

			return base.IsEnemy( m );
		}

		public AntLion( Serial serial ) : base( serial )
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