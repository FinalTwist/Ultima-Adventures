using System;
using System.Collections;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a xorn's corpse" )]
	public class Xorn : BaseCreature
	{
		[Constructable]
		public Xorn( ) : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a xorn";
			Body = 789;
			Hue = 0x31C;
			BaseSoundID = 268;

			SetStr( 226, 255 );
			SetDex( 126, 145 );
			SetInt( 71, 92 );

			SetHits( 136, 153 );

			SetDamage( 9, 16 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 30, 40 );
			SetResistance( ResistanceType.Fire, 90, 100 );
			SetResistance( ResistanceType.Cold, 90, 100 );
			SetResistance( ResistanceType.Poison, 10, 20 );
			SetResistance( ResistanceType.Energy, 40, 50 );

			SetSkill( SkillName.MagicResist, 50.1, 95.0 );
			SetSkill( SkillName.Tactics, 60.1, 100.0 );
			SetSkill( SkillName.Wrestling, 60.1, 100.0 );

			Fame = 4500;
			Karma = -4500;

			VirtualArmor = 23;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
			AddLoot( LootPack.Gems, 2 );
			AddLoot( LootPack.Gems, 2 );
			AddLoot( LootPack.Gems, 2 );
			AddLoot( LootPack.Gems, 2 );
		}

		public override bool BleedImmune{ get{ return true; } }
		public override int TreasureMapLevel{ get{ return 1; } }
		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }

		public void XornEats()
		{
			ArrayList list = new ArrayList();
			int nGold = 0;
			int toEat = 0;

			foreach ( Mobile m in this.GetMobilesInRange( 2 ) )
			{
				if ( m == this || !CanBeHarmful( m ) )
					continue;

				else if ( m.Player && m.TotalGold > 0 )
					list.Add( m );
			}

			foreach ( Mobile m in list )
			{
				nGold = m.TotalGold;
				Container pack = m.Backpack;
				toEat = Utility.RandomMinMax( 1, nGold );
				pack.ConsumeTotal(typeof(Gold), toEat);
				m.PlaySound( Utility.Random( 0x3A, 3 ) );
				m.SendMessage( "The xorn ate some of your gold!" );
			}
		}

		public override void OnGaveMeleeAttack( Mobile defender )
		{
			base.OnGaveMeleeAttack( defender );

			if ( 0.1 >= Utility.RandomDouble() )
				XornEats();
		}

		public override void OnGotMeleeAttack( Mobile attacker )
		{
			base.OnGotMeleeAttack( attacker );

			if ( 0.1 >= Utility.RandomDouble() )
				XornEats();
		}

		public Xorn( Serial serial ) : base( serial )
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