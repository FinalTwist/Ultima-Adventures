//Proudly Created by Darck... This script is needed for the first forms body change. 
//Please if this is distributed please leave the credit for this base please..
using System;
using System.Collections;
using Server;
using Server.Items;
using System.Collections.Generic;

namespace Server.Mobiles
{
	[CorpseName( "a dark master's corpse" )]
	public class GanonFinalForm : BaseCreature
	{
		public override bool IgnoreYoungProtection { get { return Core.ML; } }

	        public override WeaponAbility GetWeaponAbility()
        {
            switch (Utility.Random(3))
            {
                default:
                case 0: return WeaponAbility.DoubleStrike;
                case 1: return WeaponAbility.WhirlwindAttack;
                case 2: return WeaponAbility.CrushingBlow;
            }
        }

		[Constructable]
		public GanonFinalForm() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "Ganondorf";
			Title = "the Dark Master";
			Body = 0x25D;
            HairItemID = 0x203B;
            HairHue = 0x469; 
            Hue = 0x747;

            SetStr(900, 1000);
            SetDex(125, 135);
            SetInt(1000, 1200);

            SetHits( 30000 );

            SetDamage(20, 30);

            SetDamageType(ResistanceType.Physical, 50);
            SetDamageType(ResistanceType.Energy, 50);

            SetResistance(ResistanceType.Physical, 55, 65);
            SetResistance(ResistanceType.Fire, 60, 80);
            SetResistance(ResistanceType.Cold, 60, 80);
            SetResistance(ResistanceType.Poison, 60, 80);
            SetResistance(ResistanceType.Energy, 60, 80);

            SetSkill(SkillName.Wrestling, 90.1, 100.0);
            SetSkill(SkillName.Tactics, 90.2, 110.0);
            SetSkill(SkillName.MagicResist, 120.2, 160.0);
            SetSkill(SkillName.Swords, 120.0);
            SetSkill(SkillName.Anatomy, 120.0);
            SetSkill(SkillName.Focus, 120.0);

			Fame = 100000;
			Karma = 100000;

			VirtualArmor = 60;

            PlateChest chest = new PlateChest();
            chest.Hue = 1150;
            AddItem(chest);
            PlateArms arms = new PlateArms();
            arms.Hue = 1150;
            AddItem(arms);
            PlateGloves gloves = new PlateGloves();
            gloves.Hue = 1150;
            AddItem(gloves);
            PlateGorget gorget = new PlateGorget();
            gorget.Hue = 1150;
            AddItem(gorget);
            PlateLegs legs = new PlateLegs();
            legs.Hue = 1150;
            AddItem(legs);
            Robe robe = new Robe();
            robe.Hue = 1175;
            robe.Name = "Ganondorf's Proof of Purchase";
            robe.Movable = true;
            AddItem(robe);
            AddItem(new NoDachi());

            AddItem(new Boots());
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.SuperBoss, 6 );
			AddLoot( LootPack.HighScrolls, Utility.RandomMinMax( 6, 60 ) );
		}

		public override bool BardImmune{ get{ return !Core.SE; } }
		public override bool Unprovokable{ get{ return Core.SE; } }
		public override bool AreaPeaceImmune { get { return Core.SE; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }

		public override int TreasureMapLevel{ get{ return 5; } }

		private static bool m_InHere;

		public override void OnDamage( int amount, Mobile from, bool willKill )
		{
			if ( from != null && from != this && !m_InHere )
			{
				m_InHere = true;
				AOS.Damage( from, this, Utility.RandomMinMax( 8, 20 ), 100, 0, 0, 0, 0 );

				MovingEffect( from, 0xECA, 10, 0, false, false, 0, 0 );
				PlaySound( 0x491 );

				if ( 0.05 > Utility.RandomDouble() )
					Timer.DelayCall( TimeSpan.FromSeconds( 1.0 ), new TimerStateCallback( CreateBones_Callback ), from );

				m_InHere = false;
			}
		}

		public virtual void CreateBones_Callback( object state )
		{
			Mobile from = (Mobile)state;
			Map map = from.Map;

			if ( map == null )
				return;

			int count = Utility.RandomMinMax( 1, 3 );

			for ( int i = 0; i < count; ++i )
			{
				int x = from.X + Utility.RandomMinMax( -1, 1 );
				int y = from.Y + Utility.RandomMinMax( -1, 1 );
				int z = from.Z;

				if ( !map.CanFit( x, y, z, 16, false, true ) )
				{
					z = map.GetAverageZ( x, y );

					if ( z == from.Z || !map.CanFit( x, y, z, 16, false, true ) )
						continue;
				}

				UnholyBone bone = new UnholyBone();

				bone.Hue = 0;
				bone.Name = "unholy bones";
				bone.ItemID = Utility.Random( 0xECA, 9 );

				bone.MoveToWorld( new Point3D( x, y, z ), map );
			}
		}

		public GanonFinalForm( Serial serial ) : base( serial )
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