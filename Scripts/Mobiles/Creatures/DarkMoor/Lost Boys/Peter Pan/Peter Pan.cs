using System;
using System.Collections;
using Server.Items;
using Server.Mobiles;
using Server.Network;

namespace Server.Mobiles
{
	[CorpseName( "Peter Pan's corpse" )]
	public class PeterPan : BaseCreature
	{

		private ArrayList m_Spawns;
		private DateTime m_NextSpawn;
		private DateTime m_NextAbility;
		[Constructable]
		public PeterPan() : base( AIType.AI_Melee, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			m_Spawns = new ArrayList();
			Name = "Peter Pan";
			Title = ", King of the Lost Boys";
			Body = 400;
			BaseSoundID = 0x45A;
			this.Hue = Utility.RandomSkinHue();

			SetStr( 250 );
			SetDex( 300 );
			SetInt( 2000 );

			SetMana( 300 );
			SetHits( 1000 );

			SetDamage( 35, 40 );

			SetDamageType( ResistanceType.Physical, 60 );

			SetResistance( ResistanceType.Physical, 60 );
			SetResistance( ResistanceType.Fire, 60 );
			SetResistance( ResistanceType.Cold, -30 );
			SetResistance( ResistanceType.Poison, 60 );
			SetResistance( ResistanceType.Energy, 60 );

			SetSkill( SkillName.MagicResist, 120.0 );
			SetSkill( SkillName.Tactics, 120.0 );
			SetSkill( SkillName.Macing, 120.0 );
			SetSkill( SkillName.Anatomy, 120.0 );
            SetSkill(SkillName.Magery, 120.0);
            SetSkill(SkillName.Swords, 120.0);
            SetSkill(SkillName.Bushido, 120.0);

            AddItem( new Longsword());
			AddItem( new ShortPants( 72 ));
			AddItem( new Sandals() );
            AddItem(new Shirt(72));
            AddItem(new TallStrawHat(72));


            Fame = 5000;
			Karma = 5000;

			VirtualArmor = 40;
		}

		public override void OnThink()
		{
			if ( this.m_NextSpawn <= DateTime.UtcNow && this.m_Spawns != null && this.m_Spawns.Count < 1 )
			{
				Tinkerbell Tink = new Tinkerbell();
				Tink.MoveToWorld( this.Location, this.Map );
				this.m_Spawns.Add( Tink );
				this.m_NextSpawn = DateTime.UtcNow + TimeSpan.FromSeconds( 90.0 );
			}

			if ( this.m_NextAbility < DateTime.UtcNow )
			{
				if ( this.Hits < this.HitsMaxSeed && this.Combatant == null && this.Mana > 10 )
				{
					this.Hits += Utility.Random( 30, 40 );
					this.Mana -= 10;
					this.m_NextAbility = DateTime.UtcNow + TimeSpan.FromSeconds( 5.0 );
				}
				if ( this.Combatant != null )
				{
					if ( this.Hits < this.HitsMaxSeed / 2 && this.Mana >= 10 )
					{
						this.Hits += Utility.Random( 40, 50 );
						this.Mana -= 10;
						this.Say( "Tink, grant me some dust!." );
						this.m_NextAbility = DateTime.UtcNow + TimeSpan.FromSeconds( 5.0 );
					}

					else if ( this.Combatant.Hits < this.Combatant.HitsMax / 2 && this.Mana >= 15 )
					{
						this.Say( "Tinkerbell, bring forth your grasping hands." );
						Blood blood = new Blood();
						blood.ItemID = 3391;
						blood.Name = "Grasping Roots";
						this.Mana -= 15;
						this.m_NextAbility = DateTime.UtcNow + TimeSpan.FromSeconds( 10.0 );
					}

					else if ( this.Mana >= 30 )
					{
						this.Say( "Tinkerbell, bring forth your wrath!" );
						ArrayList alist = new ArrayList();
						IPooledEnumerable eable = this.Map.GetMobilesInRange( this.Location, 10 );
						foreach( Mobile m in eable )
							alist.Add( m );
						eable.Free();
						this.PlaySound( 518 );
						this.m_NextAbility = DateTime.UtcNow + TimeSpan.FromSeconds( 15.0 );
						Effects.SendLocationParticles( EffectItem.Create( this.Location, this.Map, TimeSpan.FromSeconds( 10.0 ) ), 0x37CC, 1, 50, 2101, 7, 9909, 0 );
						if ( alist.Count > 0 )
						{
							for( int i = 0; i < alist.Count; i++ )
							{
								Mobile m = (Mobile)alist[i];
								if ( m is Tinkerbell )
								{}
								else
								{
									AOS.Damage( m, this, Utility.Random( 35, 50 ), 100, 0, 0, 0, 0 );
									m.BoltEffect( 2 );
								}
							}
						}
					}
				}
			}
		}

		public override bool AlwaysMurderer{ get{ return true; }}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.AosSuperBoss, 3 );
			AddLoot( LootPack.Gems, 15 );
			AddLoot( LootPack.MedScrolls, 5 );
			AddLoot( LootPack.HighScrolls, 3 );
		}

		public PeterPan( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
			writer.WriteMobileList( m_Spawns );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			m_Spawns = reader.ReadMobileList();
		}
	}
}