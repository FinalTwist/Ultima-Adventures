using System;
using System.Collections;
using Server.Items;
using Server.ContextMenus;
using Server.Misc;
using Server.Network;

namespace Server.Mobiles
{
	[CorpseName( "a Spartan Lord corpse" )] 
	public class SpartanLord : BaseCreature
	{

		public override bool ShowFameTitle{ get{ return false; } }

		[Constructable]
		public SpartanLord() : base( AIType.AI_Melee, FightMode.Aggressor, 10, 1, 0.2, 0.2 )
		{
			SpeechHue = Utility.RandomDyedHue();
			Name = "Leonidas";
			Hue = Utility.RandomSkinHue();
            Title = "The Spartan Lord";
            Body = 0x190;

			SetStr( 150, 200 );
			SetDex( 100, 125 );
			SetInt( 72, 85 );

			SetDamage( 30, 35 );
			
			SetHits ( 600, 650);

			SetSkill( SkillName.Fencing, 120.0, 100.0 );
			SetSkill( SkillName.Swords, 120.0, 100.0 );
			SetSkill( SkillName.Tactics, 120.0, 100.0 );
			SetSkill( SkillName.Wrestling, 120.0, 100.0 );
            SetSkill(SkillName.Chivalry, 100.0, 120.0);

            SetResistance( ResistanceType.Physical, 70, 80 );
			SetResistance( ResistanceType.Fire, 40, 50 );
			SetResistance( ResistanceType.Cold, 80, 90 );
			SetResistance( ResistanceType.Poison, 50, 60 );
			SetResistance( ResistanceType.Energy, 25, 35 );

			Fame = 22500;
			Karma = 15500;

			AddItem( new Cloak( 33 ) );
			AddItem( new BoneArms() );
			AddItem( new BoneLegs() );
			AddItem( new PhalanxSpear() );
			AddItem( new BronzeShield() );
			AddItem( new NorseHelm() );
			AddItem( new Sandals() );

			Utility.AssignRandomHair( this );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
        }

        public override void OnDeath(Container c)
        {
            base.OnDeath(c);

            if (Utility.RandomDouble() <= 0.03) // 3% chance to drop
                c.DropItem(new AchillesSpear());
                c.DropItem(new AchillesShield());
        }

        protected override BaseAI ForcedAI
        {
            get
            {
                return new OmniAI(this);
            }
        }

        public SpartanLord( Serial serial ) : base( serial )
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