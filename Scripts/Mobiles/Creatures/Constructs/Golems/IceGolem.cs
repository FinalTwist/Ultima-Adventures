using System; 
using System.Collections; 
using Server.Items; 
using Server.ContextMenus; 
using Server.Misc; 
using Server.Network; 

namespace Server.Mobiles 
{
	[CorpseName( "a golem corpse" )] 
	public class IceGolem : AuraCreature
	{
		[Constructable] 
		public IceGolem() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 ) 
		{ 
		
			AIFullSpeedActive = true;
			AIFullSpeedPassive = false;
			
			Name = "an ice golem";
			Hue = 0x480;
			Body = 0x190;
			BaseSoundID = 268;

			NorseHelm MyHelm = new NorseHelm( );
				MyHelm.Hue = 0x4F2;
				MyHelm.ColdBonus = 20;
				MyHelm.Name = "ice helm";
				AddItem( MyHelm );

			VikingSword MySword = new VikingSword( );
				MySword.Hue = 0x4F2;
				MySword.AosElementDamages.Cold = 50;
				MySword.Name = "ice sword";
				AddItem( MySword );

			HeaterShield MyShield = new HeaterShield( );
				MyShield.Hue = 0x4F2;
				MyShield.ColdBonus = 5;
				MyShield.Name = "ice shield";
				AddItem( MyShield );

			Kilt MyKilt = new Kilt( );
				MyKilt.Hue = 0x4F2;
				MyKilt.LootType = LootType.Blessed;
				AddItem( MyKilt );

			SetStr( 386, 400 );
			SetDex( 151, 165 );
			SetInt( 161, 175 );

			SetHits( 300, 400 );

			SetDamage( 25, 30 );

			SetDamageType( ResistanceType.Physical, 25 );
			SetDamageType( ResistanceType.Cold, 75 );

			SetResistance( ResistanceType.Physical, 35, 45 );
			SetResistance( ResistanceType.Fire, 5, 10 );
			SetResistance( ResistanceType.Cold, 100 );
			SetResistance( ResistanceType.Poison, 100 );
			SetResistance( ResistanceType.Energy, 20, 30 );

			SetSkill( SkillName.DetectHidden, 80.0 );
			SetSkill( SkillName.Anatomy, 125.0 );
			SetSkill( SkillName.Poisoning, 60.0, 82.5 );
			SetSkill( SkillName.MagicResist, 83.5, 92.5 );
			SetSkill( SkillName.Swords, 125.0 );
			SetSkill( SkillName.Tactics, 125.0 );

			Fame = 4000;
			Karma = -4000;

			VirtualArmor = 30;

			MinAuraDelay = 5;
			MaxAuraDelay = 15;
			MinAuraDamage = 15;
			MaxAuraDamage = 30;
			AuraRange = 4;
			AuraPoison = null;
			AuraMessage = "The Cold Bites deed within you.";


			AddItem( new LightSource() );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
		}

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );		
			RareMetals stones = new RareMetals( Utility.RandomMinMax( 5, 10 ), "mystical ice stones" );
   			c.DropItem(stones);
		}

		public override bool BleedImmune{ get{ return true; } }
		public override bool BardImmune { get { return !Core.SE; } }
		public override bool Unprovokable { get { return Core.SE; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		public override bool IsScaredOfScaryThings{ get{ return false; } }
		public override bool ClickTitle{ get{ return false; } }
		public override bool ShowFameTitle{ get{ return false; } }
		public override bool AlwaysAttackable{ get{ return true; } }

		public IceGolem( Serial serial ) : base( serial ) 
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
			AIFullSpeedActive = true;
			AIFullSpeedPassive = false;
		} 
	} 
}