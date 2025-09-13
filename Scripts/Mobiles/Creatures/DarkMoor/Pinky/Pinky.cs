using System;
using Server;
using Server.Items;
using Server.Misc;

namespace Server.Mobiles
{
 	[CorpseName( "The corpse of Pinky" )]
	public class Pinky : BaseCreature
 	{
 		private bool m_Stunning;
 		public override bool ShowFameTitle{ get{ return false; } }
 	
 		public override bool IsScaredOfScaryThings { get { return false; } }
        public override bool IsScaryToPets { get { return true; } }

        public override bool BardImmune { get { return !Core.SE; } }
        public override bool Unprovokable { get { return Core.SE; } }
        public override bool Uncalmable { get { return Core.SE; } }
        public override Poison PoisonImmune { get { return Poison.Lethal; } }

		public override bool ClickTitle{ get{ return false; } }

		[Constructable]
		public Pinky() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
 			Name = "Pinky";
			Body = 0x191;
			Hue = 33770;
			SpeechHue = 33;
			
			SetStr( 426, 531 );
			SetDex( 200, 300 );
			SetInt( 200, 250 );
				 
			SetHits( 1400, 2500 );
			
			SetMana( 100, 250 );
			SetDex( 100, 250 );
			SetInt( 200, 350 );
			
			SetDamage( 20, 23 );
			
			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Cold, 30 );
			SetDamageType( ResistanceType.Fire, 20 );
			SetDamageType( ResistanceType.Energy, 40 );
			SetDamageType( ResistanceType.Poison, 15 );

			SetResistance( ResistanceType.Physical, 10 );
			SetResistance( ResistanceType.Fire, 20 );
			SetResistance( ResistanceType.Cold, 15 );
			SetResistance( ResistanceType.Poison, 100 );
			SetResistance( ResistanceType.Energy, 10 );

			SetSkill( SkillName.EvalInt, 10.0, 100.0 );
			SetSkill( SkillName.Magery, 10.0, 105.0 );
			SetSkill( SkillName.MagicResist, 80.0, 100.0 );
			SetSkill( SkillName.Tactics, 10.5, 100.0 );
			SetSkill( SkillName.Swords, 60.5, 100 );
			SetSkill( SkillName.Wrestling, 60.0, 100.0 );
			SetSkill( SkillName.Poisoning, 50.0, 100.0 );
			SetSkill( SkillName.Meditation, 100.0, 100.0 );
			
			Fame = 2500;
			Karma = 2500;
				 
			VirtualArmor = 30;
			
			PackGold( 800, 1250 );
			
			AddItem( new Boots() );
			AddItem( new Robe() );
			AddItem( new LeatherChest() );
			AddItem( new LeatherGloves() );
			AddItem( new LeatherGorget() );
			AddItem( new LeatherArms() );
			AddItem( new LeatherLegs() );
						
			Item hair = new Item( Utility.RandomList( 8252 ) );
			hair.Hue = 2487;
			hair.Layer = Layer.Hair;
			hair.Movable = false;
			AddItem( hair );
		}
			public override void GenerateLoot()
		{
			
			switch ( Utility.Random( 150 ))
			{
				case 0: PackItem( new SpiritOfTheTotem() ); break;
				case 1: PackItem( new PinkysRobe() ); break;
				
			}
			
		}			
 		public override void OnDamage( int amount, Mobile from, bool willKill )
		{
			if ( from != null && !willKill && amount > 5 && from.Player && 5 > Utility.Random( 50 ) )
			{
				string[] toSay = new string[]
					{
						"Who dares to challenge me?",
						"You shall never defeat me!",
						"{0}, you'll have to do better than that to defeat me!",
						"{0}, prepare to meet your doom!",
						"{0}, you'll pay for that!"
					};

				this.Say( true, String.Format( toSay[Utility.Random( toSay.Length )], from.Name ) );
			}

			base.OnDamage( amount, from, willKill );
		}
 		public Pinky( Serial serial ) : base( serial )
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