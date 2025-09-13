using System;
using System.Collections;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a bird corpse" )]
	public class Mockobo : BaseMount
	{
		public override WeaponAbility GetWeaponAbility()
		{
			return WeaponAbility.WhirlwindAttack;
		}

		[Constructable]
		public Mockobo() : this("a mockobo")
		{
		}

		[Constructable]
		public Mockobo( string name ) : base( name, 25, 0x19, AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			if (Utility.RandomDouble() > 0.85)
		{
			
			BaseSoundID = 0x2EE;
            Hue = 0x36;
            Name = "a greater mockobo";
			SetStr( 600, 750 );
			SetDex( 150, 185 );
			SetInt( 16, 40 );

			SetHits( 400, 750 );
			SetMana( 300 );

			SetDamage( 25, 30 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 40, 60 );
			SetResistance( ResistanceType.Cold, 15, 35 );
			SetResistance( ResistanceType.Poison, 5, 10 );
			SetResistance( ResistanceType.Energy, 5, 10 );
            SetResistance(ResistanceType.Fire, 5, 10);

            SetSkill( SkillName.MagicResist, 50, 70 );
			SetSkill( SkillName.Tactics, 70.1, 100 );
			SetSkill( SkillName.Wrestling, 80, 110 );

			Fame = 1700;
			Karma = -500;

			VirtualArmor = 24;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 79.1;

			if ( Utility.RandomMinMax( 1, 5 ) == 1 )
			{
				Item egg = new Eggs( Utility.RandomMinMax( 1, 5 ) );
				PackItem( egg );
			}
		}
else

		{
			
			BaseSoundID = 0x2EE;
            Hue = 0x36;
            SetStr( 500, 600 );
			SetDex( 100, 150 );
			SetInt( 16, 40 );

			SetHits( 350, 650 );
			SetMana( 200 );

			SetDamage( 20, 25 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 35, 50 );
			SetResistance( ResistanceType.Cold, 15, 25 );
			SetResistance( ResistanceType.Poison, 5, 10 );
			SetResistance( ResistanceType.Energy, 5, 10 );
            SetResistance(ResistanceType.Fire, 5, 10);

            SetSkill( SkillName.MagicResist, 25.1, 60 );
			SetSkill( SkillName.Tactics, 70.1, 100.0 );
			SetSkill( SkillName.Wrestling, 45.1, 100 );

			Fame = 1000;
			Karma = -500;

			VirtualArmor = 24;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 59.1;

			if ( Utility.RandomMinMax( 1, 5 ) == 1 )
			{
				Item egg = new Eggs( Utility.RandomMinMax( 1, 5 ) );
				PackItem( egg );
			}
		}
	}

		public override void OnAfterSpawn()
		{
			base.OnAfterSpawn();

			if ( this.Map == Map.TerMur && this.Home.X >= 909 && this.Home.X <= 1131 && this.Home.Y >= 922 && this.Home.Y <= 1264 )
			{
				this.Hue = 0xBA2;
				this.Name = "a green mockobo";
                this.SetDamageType(ResistanceType.Poison, 100);
                this.SetDamageType(ResistanceType.Physical, 0);
                this.SetResistance(ResistanceType.Poison, 35, 55);
            }
            if (this.Map == Map.Trammel && this.Home.X >= 157 && this.Home.X <= 235 && this.Home.Y >= 2248 && this.Home.Y <= 2309)
            {
                this.Hue = 0x92C;
                this.Name = "a blue mockobo";
                this.SetDamageType(ResistanceType.Cold, 100);
                this.SetDamageType(ResistanceType.Physical, 0);
                this.SetResistance(ResistanceType.Cold, 35, 55);
               // this.WeaponAbility.LightningStriker;
            }
            if (this.Map == Map.TerMur && this.Home.X >= 650 && this.Home.X <= 855 && this.Home.Y >= 522 && this.Home.Y <= 732)
            {
                this.Hue = 0x94C;
                this.Name = "a red mockobo";
                this.SetDamageType(ResistanceType.Fire, 100);
                this.SetDamageType(ResistanceType.Physical, 0);
                this.SetResistance(ResistanceType.Fire, 35, 55);
              //  this.WeaponAbility.FireStrike;
            }
            if (this.Map == Map.Malas && this.Home.X >= 1658 && this.Home.X <= 1808 && this.Home.Y >= 1167 && this.Home.Y <= 1287)
            {
                this.Hue = 0xB6F;
                this.Name = "a black mockobo";
                this.SetDamageType(ResistanceType.Energy, 100);
                this.SetDamageType(ResistanceType.Physical, 0);
                this.SetResistance(ResistanceType.Energy, 35, 55);
             //   this.WeaponAbility.PsychicAttack;
            }
        }

		public override int TreasureMapLevel { get { return 5; } }
		public override int Meat { get { return 5; } }
		public override FoodType FavoriteFood { get { return FoodType.Meat; } }
		public override MeatType MeatType{ get{ return MeatType.Bird; } }
		public override int Feathers{ get{ return 50; } }

		public Mockobo( Serial serial ): base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)2 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}
