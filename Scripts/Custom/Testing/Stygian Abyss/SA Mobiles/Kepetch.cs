using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;

namespace Server.Mobiles
{
	[CorpseName( "a kepetch corpse" )]
	public class Kepetch : BaseCreature, ICarvable
	{
		private DateTime m_NextWoolTime;

		[CommandProperty( AccessLevel.GameMaster )]
		public DateTime NextWoolTime
		{
			get{ return m_NextWoolTime; }
			set{ m_NextWoolTime = value; Body = ( DateTime.UtcNow >= m_NextWoolTime ) ? 0xCF : 0xDF; }
		}

		public void Carve( Mobile from, Item item )
		{
			if ( DateTime.UtcNow < m_NextWoolTime )
			{
				// This sheep is not yet ready to be shorn.
				PrivateOverheadMessage( MessageType.Regular, 0x3B2, 500449, from.NetState );
				return;
			}

			from.SendLocalizedMessage( 500452 ); // You place the gathered wool into your backpack.
			from.AddToBackpack( new Wool( Map == Map.Felucca ? 2 : 1 ) );

			NextWoolTime = DateTime.UtcNow + TimeSpan.FromHours( 3.0 ); // TODO: Proper time delay
		}

		public override void OnThink()
		{
			base.OnThink();
			Body = ( DateTime.UtcNow >= m_NextWoolTime ) ? 726 : 727;
		}

		[Constructable]
		public Kepetch() : base( AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			Name = "a kepetch";
			Body = 726;

			SetStr( 337, 354 );
			SetDex( 184, 194 );
			SetInt( 32, 37 );

			SetHits( 308, 366 );

			SetDamage( 7, 17 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 55, 65 );
			SetResistance( ResistanceType.Fire, 40, 45 );
			SetResistance( ResistanceType.Cold, 45, 55 );
			SetResistance( ResistanceType.Poison, 55, 65 );
			SetResistance( ResistanceType.Energy, 65, 75 );

			SetSkill( SkillName.Anatomy, 119.7, 124.1 );
			SetSkill( SkillName.MagicResist, 89.9, 97.4 );
			SetSkill( SkillName.Tactics, 117.4, 123.5 );
			SetSkill( SkillName.Wrestling, 107.7, 113.9 );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average, 2 );
		}

		public override int Meat{ get{ return 5; } }
		public override int Hides{ get{ return 14; } }
		public override HideType HideType{ get{ return HideType.Spined; } }
		public override FoodType FavoriteFood{ get{ return FoodType.FruitsAndVegies | FoodType.GrainsAndHay; } }

		public override int Wool{ get{ return (Body == 726 ? 3 : 0); } }

		public override int GetIdleSound() { return 1545; } 
		public override int GetAngerSound() { return 1542; } 
		public override int GetHurtSound() { return 1544; } 
		public override int GetDeathSound()	{ return 1543; }

		public Kepetch( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 );

			writer.WriteDeltaTime( m_NextWoolTime );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 1:
				{
					NextWoolTime = reader.ReadDeltaTime();
					break;
				}
			}
		}
	}
}