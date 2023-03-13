using System;
using Server;
using Server.Items;
using Server.Engines.CannedEvil;

namespace Server.Mobiles
{
	[CorpseName( "an ophidian corpse" )]
	public class WanderingConcubine : AuraCreature
	{
		[Constructable]
		public WanderingConcubine() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{

			AIFullSpeedActive = true;
			AIFullSpeedPassive = false;
			
			Name = "Concubine " + Server.Misc.RandomThings.GetRandomGirlName();
			Body = 87;
			BaseSoundID = 644;
			Hue = 1470;

			SetStr( 616, 805 );
			SetDex( 125, 225 );
			SetInt( 800, 950 );

			SetHits( 3500, 4500 );

			SetDamage( 40, 55 );

			SetDamageType( ResistanceType.Poison, 100 );

			SetResistance( ResistanceType.Physical, 60, 75 );
			SetResistance( ResistanceType.Fire, 30, 40 );
			SetResistance( ResistanceType.Cold, 60, 70 );
			SetResistance( ResistanceType.Poison, 80, 100 );
			SetResistance( ResistanceType.Energy, 65, 75 );

			SetSkill( SkillName.EvalInt, 90.1, 120.0 );
			SetSkill( SkillName.Magery, 100.1, 120.0 );
			SetSkill( SkillName.Meditation, 70, 90.0 );
			SetSkill( SkillName.MagicResist, 90.1, 100.0 );
			SetSkill( SkillName.Tactics, 50.1, 70.0 );
			SetSkill( SkillName.Wrestling, 80.1, 120.0 );
			SetSkill( SkillName.Poisoning, 90, 120);
			SetSkill( SkillName.DetectHidden, 90, 120);

			Fame = 19000;
			Karma = -19000;

			VirtualArmor = 50;
			
			MinAuraDelay = 5;
			MaxAuraDelay = 15;
			MinAuraDamage = 10;
			MaxAuraDamage = 25;
			AuraRange = 3;
			AuraPoison = Poison.Deadly;
            CanInfect = true;

			RangePerception = 50;
			RangeFight = 50;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.Average, 2 );
			AddLoot( LootPack.MedScrolls, 2 );
		}

		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		public override int TreasureMapLevel{ get{ return 4; } }
		public override int Meat{ get{ return 1; } }
		public override int Hides{ get{ return 5; } }
		public override HideType HideType{ get{ return HideType.Barbed; } }
		public override Poison HitPoison{ get{ return Poison.Lethal; } }
		public override double HitPoisonChance{ get{ return 0.75; } }
		public override bool BardImmune{ get{ return true; } }
		public override bool AutoDispel{ get{ return true; } }
		public override double AutoDispelChance{ get{ return 0.75; } }
		public override bool CanMoveOverObstacles { get { return true; } }
		public override bool CanDestroyObstacles { get { return true; } }

		public override bool OnBeforeDeath()
		{
			
			base.OnBeforeDeath();
			return true;
		}
       public override void OnDeath(Container c)
        {
			AetherGlobe.carrier = null;
			if (AetherGlobe.invasionstage >= 1 )
				AetherGlobe.invasionstage -= 1;

					switch (Utility.Random( 20 ) )
					{
						case 0:
							c.DropItem(new WidowMorphArms());
							break;
						case 1:
							c.DropItem(new WidowMorphChest());
							break;
						case 2:
							c.DropItem(new WidowMorphGloves());
							break;

					}	
			if (Utility.RandomMinMax(1, 5) == 3)
				c.DropItem(new InfectionPotion());


			base.OnDeath(c);
		}

		public override void OnGotMeleeAttack( Mobile attacker )
        {
            base.OnGotMeleeAttack( attacker );

            if ( Utility.RandomMinMax( 1, 4 ) == 1 )
            {
                int goo = 0;

                string Goo = "Poison Splash";
                int Color = 0x3F;

                foreach ( Item splash in this.GetItemsInRange( 10 ) ){ if ( splash is MonsterSplatter && splash.Name == Goo ){ goo++; } }

                if ( goo == 0 )
                {
                    MonsterSplatter.AddSplatter( this.X, this.Y, this.Z, this.Map, this.Location, this, Goo, Color, 0 );
                }
            }
        }

		public override void OnThink()
		{
			base.OnThink();
			
			if (this.Combatant != null )
			{
				if (this.Combatant is BlueGuard || this.Combatant is Honorae || this.Combatant is Praetor)
				{
					Mobile guard = this.Combatant;
					if (Utility.RandomDouble() < 0.05)
						Say ("Cooome tooo meeee, my deeeear...");

					guard.Hits = 1; // makes infecting guards much easier.
				}
				if (this.Combatant is BaseCreature)
				{
					BaseCreature mob = (BaseCreature)this.Combatant;
					if (mob.Controlled && mob.ControlMaster is PlayerMobile && !mob.Poisoned)
						mob.ApplyPoison( mob, Poison.Deadly );
				}
			}

			foreach ( Mobile mob in this.GetMobilesInRange( 10 ) )
			{
					if ( (mob is BaseVendor || mob is BaseChild) && !mob.Blessed )
					{
						if ( !( mob is BaseUndead || ((BaseCreature)mob).CanInfect) )	
						{
								Zombiex zomb = new Zombiex();
								zomb.NewZombie(mob);

								mob.Delete();	
						}
					}
			}
	
		}

        public override bool IsEnemy(Mobile m)
        { 
			Region region = Region.Find( m.Location, m.Map );

            if (m is BaseUndead || (m is BaseCreature && (((BaseCreature)m).CanInfect) ) || m is wOphidianWarrior || m is AcidSlug || m is wOphidianMatriarch || m is wOphidianMage || m is wOphidianKnight || m is wOphidianArchmage || m is OphidianWarrior || m is OphidianMatriarch || m is OphidianMage || m is OphidianKnight || m is OphidianArchmage || m is MonsterNestEntity || m is AncientLich || m is Bogle || m is LichLord || m is Shade || m is Spectre || m is Wraith || m is BoneKnight || m is ZenMorgan || m is Ghoul || m is Mummy || m is SkeletalKnight || m is Skeleton || m is Zombie || m is RevenantLion || m is RottingCorpse || m is SkeletalDragon || m is AirElemental || m is IceElemental || m is ToxicElemental || m is PoisonElemental || m is FireElemental || m is WaterElemental || m is EarthElemental || m is Efreet || m is SnowElemental || m is AgapiteElemental || m is BronzeElemental || m is CopperElemental || m is DullCopperElemental || m is GoldenElemental || m is ShadowIronElemental || m is ValoriteElemental || m is VeriteElemental || m is BloodElemental)
                return false;
			if ( region.IsPartOf( typeof( ChampionSpawnRegion ) ) || region is ChampionSpawnRegion || ( !(m is BaseCreature) && !(m is PlayerMobile) ) ) 
				return false;
			else if (m is BaseCreature && ( (((BaseCreature)m).ControlMaster) is PlayerMobile && !IsEnemy(((BaseCreature)m).ControlMaster)) ) 
				return false;
            return true;
        }

		public WanderingConcubine( Serial serial ) : base( serial )
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
			
			AIFullSpeedActive = true;
			AIFullSpeedPassive = false;
		}
	}
}