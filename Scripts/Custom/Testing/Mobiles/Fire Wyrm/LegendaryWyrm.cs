using System;
using System.Collections;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a Toxic corpse" )]
	public class LegendaryWyrm : BaseCreature
	{
		[Constructable]
		public LegendaryWyrm () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Body = 49;
			Hue = 1366;
			Name = "a Legendary Wyrm";
			BaseSoundID = 362;

			SetStr( 688, 720 );
			SetDex( 321, 370 );
			SetInt( 498, 657 );

			SetHits( 3312, 3353 );

			SetDamage( 38, 48 );

			SetDamageType( ResistanceType.Physical, 75 );
			SetDamageType( ResistanceType.Fire, 25 );

			SetResistance( ResistanceType.Physical, 50, 90 );
			SetResistance( ResistanceType.Fire, 50, 150 );
			SetResistance( ResistanceType.Cold, 20, 50 );
			SetResistance( ResistanceType.Poison, 20, 60 );
			SetResistance( ResistanceType.Energy, 80, 90 );

			SetSkill( SkillName.EvalInt, 90.1, 100.0 );
			SetSkill( SkillName.Magery, 99.1, 100.0 );
			SetSkill( SkillName.Meditation, 90.1, 100.0 );
			SetSkill( SkillName.MagicResist, 80.5, 90.0 );
			SetSkill( SkillName.Tactics, 80.1, 90.0 );
			SetSkill( SkillName.Wrestling, 80.1, 90.0 );

			Fame = 24000;
			Karma = -24000;

			VirtualArmor = 60;
			
			//PackItem( new LootTokenCheck( 1000 ) );
		}

		public override void OnDeath( Container c )
        {
		          switch ( Utility.Random( 100 ) ) //Rarity 10
                        { 
				case 0: c.DropItem( new WarriorsBelt( ) );
                          break;
				case 1: c.DropItem( new MageVest( ) );
			  break; 
			 }
                        base.OnDeath( c );
                       }

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 4 );
			AddLoot( LootPack.MedScrolls, 2 );
		}
		
		public override bool HasBreath{ get{ return true; } } // fire breath enabled
		public override bool AutoDispel{ get{ return true; } }
		public override int TreasureMapLevel{ get{ return 4; } }
		public override int Meat{ get{ return 19; } }
		public override int Hides{ get{ return 20; } }
		public override int Scales{ get{ return 20; } }
		public override ScaleType ScaleType{ get{ return ScaleType.White; } }

		public void ChainLightning()
		{
			ArrayList list = new ArrayList();

			foreach ( Mobile m in this.GetMobilesInRange( 5 ) )
			{
				if ( m == this || !CanBeHarmful( m ) )
					continue;

				if ( m is BaseCreature && (((BaseCreature)m).Controlled || ((BaseCreature)m).Summoned || ((BaseCreature)m).Team != this.Team) )
					list.Add( m );
				else if ( m.Player )
					list.Add( m );
			}

			foreach ( Mobile m in list )
			{
				DoHarmful( m );

				m.FixedParticles( 0x3709, 10, 30, 5052, EffectLayer.LeftFoot );
				m.PlaySound( 0x208 );

				m.SendMessage( "Your skin blisters as the surges through you!" );

				int toStrike = Utility.RandomMinMax( 20, 50 );

				Hits += toStrike;
				m.Damage( toStrike, this );
			}
		}

		public override void OnGaveMeleeAttack( Mobile defender )
		{
			base.OnGaveMeleeAttack( defender );

			if ( 0.2 >= Utility.RandomDouble() )
				ChainLightning();
		}

		public override void OnGotMeleeAttack( Mobile attacker )
		{
			base.OnGotMeleeAttack( attacker );

			if ( 0.2 >= Utility.RandomDouble() )
                ChainLightning();
		}

		public LegendaryWyrm( Serial serial ) : base( serial )
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