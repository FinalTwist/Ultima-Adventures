using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "an ophidian corpse" )]
	public class WidowsConcubine : AuraCreature
	{
		[Constructable]
		public WidowsConcubine() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
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

			SetDamage( 40, 75 );

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

			Fame = 19000;
			Karma = -19000;

			VirtualArmor = 50;
			
			MinAuraDelay = 5;
			MaxAuraDelay = 15;
			MinAuraDamage = 10;
			MaxAuraDamage = 45;
			AuraRange = 3;
			AuraPoison = Poison.Deadly;
            CanInfect = true;
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

       public override void OnDeath(Container c)
        {

					switch (Utility.Random( 50 ) )
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
				WidowsLament.KillsWidow += 1;
						if (Utility.RandomMinMax(1, 20) == 3)
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
				if (this.Combatant is BaseCreature)
				{
					BaseCreature mob = (BaseCreature)this.Combatant;
					if (mob.Controlled && mob.ControlMaster is PlayerMobile && !mob.Poisoned)
						mob.ApplyPoison( mob, Poison.Deadly );
				}
			}

	
		}

		public WidowsConcubine( Serial serial ) : base( serial )
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
