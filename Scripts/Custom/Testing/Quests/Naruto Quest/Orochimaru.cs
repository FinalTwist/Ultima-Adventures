using System;
using Server.Items;

namespace Server.Mobiles
{
	public class Orochimaru : BaseCreature
	{
		public override bool ClickTitle{ get{ return false; } }
		private bool m_TrueForm;

		[Constructable]
		public Orochimaru() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Hue = 1000;

			{
				
				Body = 400;
				Name = "Orochimaru";
			}

			SetStr( 500, 1500 );
			SetDex( 300, 300 );
			SetInt( 300, 300 );

			SetDamage( 105, 233 );

			SetHits( 10000, 12000 );

			SetSkill( SkillName.Fencing, 88.8, 97.5 );
			SetSkill( SkillName.Macing, 99.9, 110.0 );
			SetSkill( SkillName.MagicResist, 25.0, 47.5 );
			SetSkill( SkillName.Swords, 200.0, 250.5 );
			SetSkill( SkillName.Tactics, 99.9, 110.0 );
			SetSkill( SkillName.Wrestling, 15.0, 37.5 );

			Fame = 0;
			Karma = 0;

			AddItem( new OrochimaruShirt() );			
			AddItem( new OrochimaruPants() );
			AddItem( new OrochimaruBoots() );
			AddItem( new OrochimaruUnderShirt() );
			AddItem( new Katana() );


			AddItem( new LongHair( 1 ) );
			PackItem( new Gold( 6000 ) );
			//PackItem( new Gold( 60000 ) );
			//PackItem( new Gold( 60000 ) );
			//PackItem( new Gold( 60000 ) );
			//PackItem( new Gold( 60000 ) );
			//PackItem( new Gold( 60000 ) );
			//PackItem( new Gold( 60000 ) );
			PackItem( new OrochimarusHeart() );
		}

		public override bool CanRummageCorpses{ get{ return true; } }
		public override bool BardImmune{ get{ return true; } }
		public override bool Unprovokable{ get{ return true; } }
		public override bool Uncalmable{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		public override bool AlwaysMurderer{ get{ return true; } }
       		
		public void spawnsnakes( Mobile target )
		{
			Map map = this.Map;

			if ( map == null )
				return;

			int snakes = 0;

			foreach ( Mobile m in this.GetMobilesInRange( 10 ) )
			{
				if ( m is GiganticSnake || m is GiantSerpent || m is Snake )
					++snakes;
			}

			if ( snakes < 4 )
			{
				int newsnakes = 1;

				for ( int i = 0; i < newsnakes; ++i )
				{
					BaseCreature Snake;

					switch ( Utility.Random( 6 ) )
					{
						default:
						case 0: case 1:	Snake = new GiganticSnake(); break;
						case 2: case 3:	Snake = new GiantSerpent(); break;
						case 4:	case 5:	Snake = new Snake(); break;
					}

					Snake.Team = this.Team;

					bool validLocation = false;
					Point3D loc = this.Location;

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

					Snake.MoveToWorld( loc, map );
					Snake.Combatant = target;
				}
			}
		}

		public void DoSpecialAbility( Mobile target )
		{

			if ( 0.75 >= Utility.RandomDouble() ) // 25% chance to more daemons
				spawnsnakes( target );

		}

		public override void OnGotMeleeAttack( Mobile attacker )
		{
			base.OnGotMeleeAttack( attacker );

			DoSpecialAbility( attacker );
		}

		public override void OnGaveMeleeAttack( Mobile defender )
		{
			base.OnGaveMeleeAttack( defender );

			DoSpecialAbility( defender );
		}

		public Orochimaru( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
			
			writer.Write( m_TrueForm );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			m_TrueForm = reader.ReadBool();
		}
	}
}