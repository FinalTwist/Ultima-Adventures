using System;
using Server;
using Server.Misc;
using Server.Items;
using System.Collections;
using Server.Regions;

namespace Server.Mobiles 
{ 
	[CorpseName( "a lycanthrope corpse" )] 
	public class WereWolf : BaseCreature 
	{
		public override bool CanChew { get{return true;}}
		public override WeaponAbility GetWeaponAbility()
		{
			if ( this.Body == 708 || this.Body == 94 || this.Body == 34 || this.Body == 42 || this.Body == 39 )
				return WeaponAbility.BleedAttack;

			return null;
		}

		private bool m_TrueForm;

		[Constructable] 
		public WereWolf() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 ) 
		{
			SpeechHue = Server.Misc.RandomThings.GetSpeechHue();
			Hue = Server.Misc.RandomThings.GetRandomSkinColor();

			if ( this.Female = Utility.RandomBool() ) 
			{
				this.Body = 0x191; 
				this.Name = NameList.RandomName( "female" ); 
				Utility.AssignRandomHair( this );
				HairHue = Utility.RandomHairHue();
			} 
			else 
			{ 
				this.Body = 0x190; 
				this.Name = NameList.RandomName( "male" );
				Utility.AssignRandomHair( this );
				int HairColor = Utility.RandomHairHue();
				FacialHairItemID = Utility.RandomList( 0, 8254, 8255, 8256, 8257, 8267, 8268, 8269 );
				HairHue = HairColor;
				FacialHairHue = HairColor;
			} 

			SetStr( 80, 120 );
			SetDex( 80, 120 );
			SetInt( 30, 50 );

			SetDamage( 8, 10 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 35, 45 );
			SetResistance( ResistanceType.Fire, 25, 30 );
			SetResistance( ResistanceType.Cold, 25, 30 );
			SetResistance( ResistanceType.Poison, 10, 20 );
			SetResistance( ResistanceType.Energy, 10, 20 );

			SetSkill( SkillName.DetectHidden, 80.0 );
			SetSkill( SkillName.Anatomy, 125.0 );
			SetSkill( SkillName.Poisoning, 60.0, 82.5 );
			SetSkill( SkillName.MagicResist, 83.5, 92.5 );
			SetSkill( SkillName.Swords, 125.0 );
			SetSkill( SkillName.Wrestling, 125.0 );
			SetSkill( SkillName.Tactics, 125.0 );

			Fame = 8000;
			Karma = -8000;

			VirtualArmor = 5;

			if ( Utility.RandomMinMax( 0, 1 ) == 1 )
			{
				switch ( Utility.Random( 5 ))
				{
					case 0: AddItem( new StuddedChest( ) );		AddItem( new StuddedArms( ) );		AddItem( new StuddedLegs( ) );		AddItem( new StuddedGorget( ) );	AddItem( new StuddedGloves( ) ); break;
					case 1: AddItem( new PlateChest( ) );		AddItem( new PlateArms( ) );		AddItem( new PlateLegs( ) );		AddItem( new PlateGorget( ) );		AddItem( new PlateGloves( ) ); break;
					case 2: AddItem( new ChainChest( ) );		AddItem( new RingmailArms( ) );		AddItem( new ChainLegs( ) );		AddItem( new RingmailGloves( ) ); break;
					case 3: AddItem( new RingmailChest( ) );	AddItem( new RingmailArms( ) );		AddItem( new RingmailLegs( ) );		AddItem( new RingmailGloves( ) ); break;
					case 4: AddItem( new BoneChest( ) );		AddItem( new BoneArms( ) );			AddItem( new BoneLegs( ) );			AddItem( new BoneGloves( ) ); break;
				}

				switch ( Utility.Random( 9 ))
				{
					case 0: AddItem( new PlateHelm( ) ); break;
					case 1: AddItem( new Bascinet( ) ); break;
					case 2: AddItem( new ChainCoif( ) ); break;
					case 3: AddItem( new CloseHelm( ) ); break;
					case 4: AddItem( new OrcHelm( ) ); break;
					case 5: AddItem( new NorseHelm( ) ); break;
					case 6: AddItem( new BoneHelm( ) ); break;
				}

				AddItem( new Boots( ) );

				switch ( Utility.Random( 7 ))
				{
					case 0: AddItem( new BattleAxe() ); break;
					case 1: AddItem( new VikingSword() ); break;
					case 2: AddItem( new Halberd() ); break;
					case 3: AddItem( new DoubleAxe() ); break;
					case 4: AddItem( new ExecutionersAxe() ); break;
					case 5: AddItem( new WarAxe() ); break;
					case 6: AddItem( new TwoHandedAxe() ); break;
				}

				switch ( Utility.RandomMinMax( 0, 5 ) )
				{
					case 0: Title = "the warrior"; break;
					case 1: Title = "the berserker"; break;
					case 2: Title = "the barbarian"; break;
					case 3: Title = "the fighter"; break;
					case 4: Title = "the knight"; break;
					case 5: Title = "the champion"; break;
				}
			}
			else
			{
				switch ( Utility.RandomMinMax( 0, 4 ) )
				{
					case 0: Title = "the thief"; break;
					case 1: Title = "the rogue"; break;
					case 2: Title = "the robber"; break;
					case 3: Title = "the brigand"; break;
					case 4: Title = "the bandit"; break;
				}

				AddItem( new Boots( Utility.RandomNeutralHue() ) );
				AddItem( new FancyShirt( RandomThings.GetRandomColor(0) ));
				AddItem( new LongPants( Utility.RandomNeutralHue() ) );
				AddItem( new SkullCap( RandomThings.GetRandomColor(0) ));

				switch ( Utility.Random( 7 ))
				{
					case 0: AddItem( new Longsword() ); break;
					case 1: AddItem( new Cutlass() ); break;
					case 2: AddItem( new Broadsword() ); break;
					case 3: AddItem( new Axe() ); break;
					case 4: AddItem( new Club() ); break;
					case 5: AddItem( new Dagger() ); break;
					case 6: AddItem( new Spear() ); break;
				}
			}

			if ( Utility.RandomBool() ) { Morph(); }
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
			AddLoot( LootPack.Rich );
		}

		public override void OnAfterSpawn()
		{
			base.OnAfterSpawn();

			Region reg = Region.Find( this.Location, this.Map );

			if ( reg.IsPartOf( typeof( NecromancerRegion ) ) || reg.IsPartOf( "the Crypts of Dracula" ) || reg.IsPartOf( "the Castle of Dracula" ) )
			{
				m_TrueForm = true;

				Body = Utility.RandomList( 708, 94 );
				BaseSoundID = 0xE5;
				Name = "a werewolf";

				Title = null;

				SetStr( 200, 250 );
				SetDex( 200, 250 );
				SetInt( 30, 60 );

				SetHits( 200, 250 );

				SetDamage( 12, 15 );

				VirtualArmor = 10;
			}
		}

		public override bool ClickTitle{ get{ return false; } }
		public override bool ShowFameTitle{ get{ return false; } }
		public override bool AlwaysAttackable{ get{ return true; } }
		public override int Meat{ get{ return 1; } }
		public override int Hides{ get{ return 7; } }
		public override int Furs{ get{ return Utility.RandomList( 0, 0, 0, 4 ); } }
		public override FurType FurType{ get{ return FurType.Regular; } }

		public override bool OnBeforeDeath()
		{
			if ( m_TrueForm )
			{
				return base.OnBeforeDeath();
			}
			else
			{
				Morph();
				return false;
			}
		}

		public void Morph()
		{
			if (m_TrueForm)
			return;

			m_TrueForm = true;

			Body = Utility.RandomList( 708, 94 );
			BaseSoundID = 0xE5;
			Name = "a werewolf";
			string Growl = "Grrrrr!";
			Hue = 0;

			switch ( Utility.Random( 6 ) )
			{
				case 0: Body = 34; 	BaseSoundID = 0xA3; 	Name = "a werebear"; 				Growl = "Grrrrr!";		break;
				case 1: Body = 42; 	BaseSoundID = 437; 		Name = "a wererat";					Growl = "Squeak!";		break;
				case 2: Body = 39; 	BaseSoundID = 422; 		Name = "a werebat";	Hue = 0x497;	Growl = "Squeak!";		break;
				case 3: Body = 176; BaseSoundID = 0x3EE; 	Name = "a werecat"; 				Growl = "Grrrrr!";		break;
			}

			Say(Growl);

			Title = null;

			SetStr( 200, 250 );
			SetDex( 200, 250 );
			SetInt( 30, 60 );

			SetHits( 200, 250 );

			SetDamage( 12, 15 );

			VirtualArmor = 10;
		}

		public WereWolf( Serial serial ) : base( serial ) 
		{ 
		} 

		public override void Serialize( GenericWriter writer ) 
		{ 
			base.Serialize( writer ); 
			writer.Write( (int) 0 );
			writer.Write( m_TrueForm );	
		} 

		public override void Deserialize( GenericReader reader ) 
		{ 
			base.Deserialize( reader ); 
			int version = reader.ReadInt();
			switch ( version )
			{
				case 0:
				{
					m_TrueForm = reader.ReadBool();
					break;
				}
			}
		} 
	} 
}