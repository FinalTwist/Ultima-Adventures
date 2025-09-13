//=================================================
//This script was created by Zalana
//This script was created on 12/29/2012 6:49:56 PM
//=================================================
using System;
using Server;
using Server.Items;
using System.Collections.Generic;

namespace Server.Mobiles
{
	[CorpseName( "Corpse of The Lamb" )]
	public class TheLamb : BaseMount
	{
	
		private double kReflectDamagePercent = 95;
		
		[Constructable]
		public TheLamb() : this( "The Lamb" )
		{
		}
		
		[Constructable]
		public TheLamb( string name )
            : base(name, 0x7A, 0x3EB4, AIType.AI_Mage, FightMode.Evil, 10, 1, 0.2, 0.4)
        {
            this.BaseSoundID = 0x4BC;
		
			Name = "The Lamb";
			Hue = 1157;
			SetStr( 70000, 100000 );
			SetDex( 50000, 65000 );
			SetInt( 100000, 150000 );

			SetHits( 100000, 200000 );
			SetStam( 67500, 90000 );
			
			SetDamage(100, 200);
			
			SetDamageType( ResistanceType.Physical, 100 );
			SetDamageType( ResistanceType.Fire, 100 );
			SetDamageType( ResistanceType.Energy, 100 );
			SetDamageType( ResistanceType.Cold, 100 );
			SetDamageType( ResistanceType.Poison, 100 );

			SetSkill( SkillName.Anatomy, 150, 200 );
			SetSkill( SkillName.DetectHidden, 150, 200 );
			SetSkill( SkillName.EvalInt, 150, 200 );
			SetSkill( SkillName.Magery, 150, 200 );
			SetSkill( SkillName.Tactics, 150, 200 );
			SetSkill( SkillName.Wrestling, 150, 200 );
			SetSkill( SkillName.Poisoning, 150, 200 );

			SetResistance( ResistanceType.Physical, 100, 200 );
			SetResistance( ResistanceType.Fire, 100, 200 );
			SetResistance( ResistanceType.Energy, 100, 200 );
			SetResistance( ResistanceType.Cold, 100, 200 );
			SetResistance( ResistanceType.Poison, 100, 200 );

			Fame = 15000;
			Karma = -15000;
			
			VirtualArmor = 2000;
			Tamable = false;
			
			switch ( Utility.Random( 120 ))
            {
                case 0: PackItem( new ZalanasMysticalAmulet() ); break;
                case 1: PackItem( new ZalanasBarbarianHoops() ); break;
                case 2: PackItem( new ZalanasArcherBand() ); break;
                case 3: PackItem( new ZalanasArcherBracelet() ); break;
                case 4: PackItem( new ZalanasBarbarianBand() ); break;
                case 5: PackItem( new ZalanasMysticalHoops() ); break;
                case 6: PackItem( new ZalanasArcherHoops() ); break;
                case 7: PackItem( new ZalanasMysticalBracelet() ); break;
                case 8: PackItem( new ZalanasBarbarianBracelet() ); break;
                case 9: PackItem( new ZalanasMysticalBand() ); break;
				case 10: PackItem( new ZalanasArcherAmulet() ); break;
				case 11: PackItem( new ZalanasBarbarianAmulet() ); break;
            }
		}
		
		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.Average );
			AddLoot( LootPack.LowScrolls );
			AddLoot( LootPack.Potions );
		}


		public TheLamb( Serial serial ) : base( serial )
		{
		}
		
		public override bool Unprovokable
        {
            get
            {
                return true;
            }
        }
        public override bool AlwaysMurderer
        {
            get
            {
                return true;
            }
        }
        public override bool BardImmune
        {
            get
            {
                return true;
            }
        }
		public override Poison PoisonImmune
        {
            get
            {
                return Poison.Lethal;
            }
        }
        public override bool CanRummageCorpses
        {
            get
            {
                return true;
            }
        }
        public override bool BleedImmune
        {
            get
            {
                return true;
            }
        }
		public override bool HasBreath
        {
            get
            {
                return true;
            }
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
