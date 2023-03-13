/**********************************************************
	Name: Champion Spawn Monster
	Scripted By: Formosa
	Version: v1.0
	Update Date: March 10, 2013

	Notes: 
	Anyone can modify/redistribute this 
	Do Not Remove/Change This Header!!	
**********************************************************/

using System;
using Server;
using Server.Engines.Plants;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a dryad's champion corpse" )]
	public class ChampionMLDryad : BaseCreature
	{
		public override bool InitialInnocent { get { return true; } }

		public override OppositionGroup OppositionGroup
		{
			get { return OppositionGroup.FeyAndUndead; }
		}

		[Constructable]
		public ChampionMLDryad() : base( AIType.AI_Mage, FightMode.Evil, 10, 1, 0.2, 0.4 )
		{
			ChampionHue();

			Name = "a dryad";
			Body = 266;
			BaseSoundID = 0x57B;

			SetStr( 132, 149 );
			SetDex( 152, 168 );
			SetInt( 251, 280 );

			SetHits( 304, 321 );

			SetDamage( 11, 20 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 40, 50 );
			SetResistance( ResistanceType.Fire, 15, 25 );
			SetResistance( ResistanceType.Cold, 40, 45 );
			SetResistance( ResistanceType.Poison, 30, 40 );
			SetResistance( ResistanceType.Energy, 25, 35 );

			SetSkill( SkillName.Meditation, 80.0, 90.0 );
			SetSkill( SkillName.EvalInt, 70.0, 80.0 );
			SetSkill( SkillName.Magery, 70.0, 80.0 );
			SetSkill( SkillName.Anatomy, 0 );
			SetSkill( SkillName.MagicResist, 100.0, 120.0 );
			SetSkill( SkillName.Tactics, 70.0, 80.0 );
			SetSkill( SkillName.Wrestling, 70.0, 80.0 );

			Fame = 7000;
			Karma = 7000;

			VirtualArmor = 28; // Don't know what it should be

			Tamable = false;
			ControlSlots = 1;
			MinTameSkill = 120.0;

			if ( Core.ML && Utility.RandomDouble() < .60 )
				PackItem( Seed.RandomPeculiarSeed( 1 ) );

			//PackArcaneScroll(0, 2);
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Gems );
		}

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );
			
			c.DropItem( new Apple( 6 ) );
			
			if ( Utility.RandomDouble() < 0.05 )
			{
				switch ( Utility.Random( 2 ) )
				{
					case 0: c.DropItem( new Necklace() ); break;
					case 1: c.DropItem( new GoldRing() ); break;
				}
			}
			
			if ( Utility.RandomDouble() < 0.04 )
				c.DropItem( new GoldBracelet() );
						
			if ( Utility.RandomDouble() < 0.03 )
				c.DropItem( new GoldBeadNecklace() );
					
			if ( Utility.RandomDouble() < 0.02 )
				c.DropItem( new GoldNecklace() );
				
			if ( Utility.RandomDouble() < 0.01 )
			{
				switch ( Utility.Random( 2 ) )
				{
					case 0: c.DropItem( new Beads() ); break;
					case 1: c.DropItem( new GoldEarrings() ); break;
				}
			}
		}

		public override int Meat { get { return 1; } }

		public override void OnThink()
		{
			base.OnThink();

			AreaPeace();
			AreaUndress();
		}

		#region Area Peace
		private DateTime m_NextPeace;

		public void AreaPeace()
		{
			if ( Combatant == null || Deleted || !Alive || m_NextPeace > DateTime.UtcNow || 0.1 < Utility.RandomDouble() )
				return;

			TimeSpan duration = TimeSpan.FromSeconds( Utility.RandomMinMax( 20, 80 ) );

			foreach ( Mobile m in GetMobilesInRange( RangePerception ) )
			{
				PlayerMobile p = m as PlayerMobile;

				if ( IsValidTarget( p ) )
				{
					p.PeacedUntil = DateTime.UtcNow + duration;
					p.SendLocalizedMessage( 1072065 ); // You gaze upon the dryad's beauty, and forget to continue battling!
					p.FixedParticles( 0x376A, 1, 20, 0x7F5, EffectLayer.Waist );
					p.Combatant = null;
				}
			}

			m_NextPeace = DateTime.UtcNow + TimeSpan.FromSeconds( 10 );
			PlaySound( 0x1D3 );
		}

		public bool IsValidTarget( PlayerMobile m )
		{
			if ( m != null && m.PeacedUntil < DateTime.UtcNow && !m.Hidden && m.AccessLevel == AccessLevel.Player && CanBeHarmful( m ) )
				return true;

			return false;
		}
		#endregion

		#region Undress
		private DateTime m_NextUndress;

		public void AreaUndress()
		{
			if ( Combatant == null || Deleted || !Alive || m_NextUndress > DateTime.UtcNow || 0.005 < Utility.RandomDouble() )
				return;

			foreach ( Mobile m in GetMobilesInRange( RangePerception ) )
			{
				if ( m != null && m.Player && !m.Female && !m.Hidden && m.AccessLevel == AccessLevel.Player && CanBeHarmful( m ) )
				{
					UndressItem( m, Layer.OuterTorso );
					UndressItem( m, Layer.InnerTorso );
					UndressItem( m, Layer.MiddleTorso );
					UndressItem( m, Layer.Pants );
					UndressItem( m, Layer.Shirt );

					m.SendLocalizedMessage( 1072197 ); // The dryad's beauty makes your blood race. Your clothing is too confining.
				}
			}

			m_NextUndress = DateTime.UtcNow + TimeSpan.FromMinutes( 1 );
		}

		public void UndressItem( Mobile m, Layer layer )
		{
			Item item = m.FindItemOnLayer( layer );

			if ( item != null && item.Movable )
				m.PlaceInBackpack( item );
		}
		#endregion

		public virtual void ChampionHue()
		{
			switch ( Utility.Random ( 10 ) )
			{
				case 0: Hue = ( 1372 ); break;
				case 1: Hue = ( 2000 ); break;
				case 2: Hue = ( 1260 ); break;
				case 3: Hue = ( 947 ); break;
				case 4: Hue = ( 1207 ); break;
				case 5: Hue = ( 1195 ); break;
				case 6: Hue = ( 1177 ); break;
				case 7: Hue = ( 1172 ); break;
				case 8: Hue = ( 1170 ); break;
				case 9: Hue = ( 1289 ); break;
			}
		}

		public ChampionMLDryad( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}
