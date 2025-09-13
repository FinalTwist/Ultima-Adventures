using System;
using Server.Items;

namespace Server.Mobiles
{
	public class Cloud : BaseCreature
	{
		public override bool ClickTitle{ get{ return false; } }
		private bool m_TrueForm;

		[Constructable]
		public Cloud() : base( AIType.AI_Paladin, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			SpeechHue = Utility.RandomDyedHue();
			Title = "the legendary hero";
			Hue = 33770;

			{
				Body = 0x190;
				Name = "Cloud";
			}

			SetStr( 300, 300 );
			SetDex( 300, 300 );
			SetInt( 300, 300 );

			SetDamage( 15, 23 );

			SetHits( 300, 350 );

			SetSkill( SkillName.Fencing, 88.8, 97.5 );
			SetSkill( SkillName.Macing, 99.9, 110.0 );
			SetSkill( SkillName.MagicResist, 25.0, 47.5 );
			SetSkill( SkillName.Swords, 100, 120 );
			SetSkill( SkillName.Tactics, 99.9, 110.0 );
			SetSkill( SkillName.Wrestling, 15.0, 37.5 );
            SetSkill(SkillName.Bushido, 100, 100);
            SetSkill(SkillName.Chivalry, 100, 100);

            Fame = 0;
			Karma = 10000;


			AddItem( new CloudsPants() );
			AddItem( new CloudsOutterShirt() );
			AddItem( new CloudsUnderShirt() );
			AddItem( new CloudsBoots() );
			AddItem( new BusterSword() );

			AddItem( new ShortHair( 254 ) );

				if ( 0.05 > Utility.RandomDouble() )
                                               PackGold( 500, 1000 );
                                               PackItem( new BusterSword() ); 
		}

		public Cloud( Serial serial ) : base( serial )
		{
		}

        // + OmniAI support +
        protected override BaseAI ForcedAI
        {
            get
            {
                return new OmniAI(this);
            }
        }
        // - OmniAI support -
        public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
			
			writer.Write( m_TrueForm );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			m_TrueForm = reader.ReadBool();
		}
	}
}