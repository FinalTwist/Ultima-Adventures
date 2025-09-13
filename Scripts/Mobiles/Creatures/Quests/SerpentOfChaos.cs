using System;
using Server;
using System.Collections;
using System.Collections.Generic;
using Server.Items;
using Server.Misc;

namespace Server.Mobiles
{
	[CorpseName( "an ethereal corpse" )]
	public class SerpentOfChaos : BaseCreature
	{
		public override bool HasBreath{ get{ return true; } }
		public override bool ReacquireOnMovement{ get{ return !Controlled; } }
		public override double BreathEffectDelay{ get{ return 0.1; } }
		public override void BreathDealDamage( Mobile target, int form ){ base.BreathDealDamage( target, 17 ); }

		[Constructable]
		public SerpentOfChaos() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "Serpent of Chaos";

			Body = 21;
			BaseSoundID = 219;
			Hue = 0x4AA;

			SetStr( 476, 505 );
			SetDex( 266, 285 );
			SetInt( 171, 195 );

			SetHits( 286, 303 );

			SetDamage( 11, 13 );

			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Fire, 50 );

			SetResistance( ResistanceType.Physical, 50, 60 );
			SetResistance( ResistanceType.Cold, 30, 40 );
			SetResistance( ResistanceType.Fire, 60, 70 );
			SetResistance( ResistanceType.Poison, 30, 40 );
			SetResistance( ResistanceType.Energy, 30, 40 );

			SetSkill( SkillName.MagicResist, 85.1, 95.0 );
			SetSkill( SkillName.Tactics, 70.1, 80.0 );
			SetSkill( SkillName.Wrestling, 60.1, 80.0 );

			Fame = 15000;
			Karma = -15000;

			VirtualArmor = 58;
		}

		public override void OnDamage( int amount, Mobile from, bool willKill )
		{
			if ( this.Body == 13 && willKill == false && Utility.Random( 4 ) == 1 )
			{
				this.Body = 21;
				this.BaseSoundID = 219;
			}
			else if ( willKill == false && Utility.Random( 4 ) == 1 )
			{
				this.Body = 13;
				this.BaseSoundID = 655;
			}

			base.OnDamage( amount, from, willKill );
		}

		public override bool OnBeforeDeath()
		{
			int CanDie = 0;
			int CanDie2 = 0;
			Mobile winner = this;

			foreach ( Mobile m in this.GetMobilesInRange( 30 ) )
			{
				if ( m is PlayerMobile && !m.Blessed )
				{
					Item rock = m.Backpack.FindItemByType( typeof ( BlackrockSerpentChaos ) );
					if ( rock != null )
					{
						CanDie = 1;
						winner = m;
						rock.Delete();
					}

					Item balance = m.Backpack.FindItemByType( typeof ( SerpentCapturedBalance ) );
					if ( balance != null )
					{
						CanDie2 = 1;
					}
				}
			}

			if ( CanDie != 1 || CanDie2 != 1 )
			{
				this.Hits = this.HitsMax;
				this.FixedParticles( 0x376A, 9, 32, 5030, EffectLayer.Waist );
				this.PlaySound( 0x202 );
				return false;
			}
			else
			{
				this.Body = 13;
				this.BaseSoundID = 655;

				SerpentSpawnerChaos MySpawner = new SerpentSpawnerChaos();
				Point3D loc = new Point3D( 2345, 297, 15 );
				MySpawner.MoveToWorld( loc, Map );

				string Iam = "the Serpent of Chaos";
				Server.Misc.LoggingFunctions.LogSlayingLord( this.LastKiller, Iam );

				if ( winner is PlayerMobile )
				{
					winner.AddToBackpack( new SerpentCapturedChaos() );
					winner.SendMessage( "You have subdued the Serpent of Chaos!" );
					LoggingFunctions.LogGenericQuest( winner, "has subdued the serpent of chaos" );
				}

				return base.OnBeforeDeath();
			}
		}

		public override bool DeleteCorpseOnDeath{ get{ return true; } }
		public override bool BardImmune { get { return true; } }

		public SerpentOfChaos( Serial serial ) : base( serial )
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
