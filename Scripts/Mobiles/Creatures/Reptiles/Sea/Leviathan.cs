using System;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.Misc;

namespace Server.Mobiles
{
	[CorpseName( "a leviathan corpse" )]
	public class Leviathan : BaseCreature
	{
		public override bool HasBreath{ get{ return true; } }
		public override int BreathPhysicalDamage{ get{ return 70; } } // TODO: Verify damage type
		public override int BreathColdDamage{ get{ return 30; } }
		public override int BreathFireDamage{ get{ return 0; } }
		public override int BreathEffectHue{ get{ return 0x1ED; } }
		public override double BreathMinDelay{ get{ return 5.0; } }
		public override double BreathMaxDelay{ get{ return 7.5; } }
		public override void BreathDealDamage( Mobile target, int form ){ target.SendMessage( "You are hit by the force of the beast!" ); base.BreathDealDamage( target, 0 ); }

		private Mobile m_Fisher;

		public Mobile Fisher
		{
			get{ return m_Fisher; }
			set{ m_Fisher = value; }
		}

		[Constructable]
		public Leviathan() : this( null )
		{
		}

		[Constructable]
		public Leviathan( Mobile fisher ) : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			m_Fisher = fisher;

			// May not be OSI accurate; mostly copied from krakens
			Name = "a leviathan";
			Body = 77;
			BaseSoundID = 353;

			Hue = 0xB5E;

			SetStr( 1000 );
			SetDex( 501, 520 );
			SetInt( 900, 1200 );

			SetHits( 5500 );

			SetDamage( 45, 55 );

			SetDamageType( ResistanceType.Physical, 70 );
			SetDamageType( ResistanceType.Cold, 30 );

			SetResistance( ResistanceType.Physical, 55, 65 );
			SetResistance( ResistanceType.Fire, 45, 55 );
			SetResistance( ResistanceType.Cold, 45, 55 );
			SetResistance( ResistanceType.Poison, 35, 45 );
			SetResistance( ResistanceType.Energy, 25, 35 );

			SetSkill( SkillName.EvalInt, 97.6, 107.5 );
			SetSkill( SkillName.Magery, 97.6, 107.5 );
			SetSkill( SkillName.MagicResist, 97.6, 107.5 );
			SetSkill( SkillName.Meditation, 97.6, 107.5 );
			SetSkill( SkillName.Tactics, 97.6, 107.5 );
			SetSkill( SkillName.Wrestling, 97.6, 107.5 );

			Fame = 24000;
			Karma = -24000;

			VirtualArmor = 50;

			CanSwim = true;
			CantWalk = true;
		}

		public override bool BleedImmune{ get{ return true; } }
		public override int Hides{ get{ return 10; } }
		public override HideType HideType{ get{ return HideType.Spined; } }

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 5 );
		}

		public override double TreasureMapChance{ get{ return 0.25; } }
		public override int TreasureMapLevel{ get{ return 5; } }

		public Leviathan( Serial serial ) : base( serial )
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

		public static void GiveArtifactTo( Mobile m )
		{
			Item item = new UnidentifiedArtifact();
			if ( MyServerSettings.GetUnidentifiedChance() >= Utility.RandomMinMax( 1, 100 ) ){ item = Loot.RandomArty(); } // WIZARD WANTS A UNIFIED ARTY LISTING

			if ( item == null )
				return;

			// TODO: Confirm messages
			if ( m.AddToBackpack( item ) )
				m.SendMessage( "As a reward for slaying the mighty leviathan, an artifact has been placed in your backpack." );
			else
				m.SendMessage( "As your backpack is full, your reward for destroying the legendary leviathan has been placed at your feet." );
		}

		public override void OnKilledBy( Mobile mob )
		{
			base.OnKilledBy( mob );

			if ( Paragon.CheckArtifactChance( mob, this ) )
			{
				GiveArtifactTo( mob );

				if ( mob == m_Fisher )
					m_Fisher = null;
			}
		}

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );

			if ( m_Fisher != null && 25 > Utility.Random( 100 ) )
				GiveArtifactTo( m_Fisher );

			m_Fisher = null;
		}
	}
}