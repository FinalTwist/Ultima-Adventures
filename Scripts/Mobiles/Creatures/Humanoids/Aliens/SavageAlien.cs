using System;
using Server;
using Server.Misc;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a savage corpse" )]
	public class SavageAlien : BaseCreature
	{
		[Constructable]
		public SavageAlien() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "an alien savage";

			Hue = Utility.RandomList( 0x6F6, 0x97F, 0x99B, 0x6E4, 0x5E0, 0xB38, 0xB2B );

			string metal = Server.Misc.MorphingTime.GetSpaceAceMetalName();
			string cloth = Server.Misc.MorphingTime.GetSpaceAceClothName();
			string bone = Server.Misc.MorphingTime.GetSpaceAceBoneName();
			string wood = Server.Misc.MorphingTime.GetSpaceAceWoodName();

			if ( Female = Utility.RandomBool() )
			{
				Body = 401;
			}
			else
			{
				Body = 400;
			}

			SetStr( 336, 385 );
			SetDex( 281, 305 );
			SetInt( 96, 115 );

			SetHits( 202, 231 );
			SetMana( 0 );

			SetDamage( 7, 23 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 45, 55 );
			SetResistance( ResistanceType.Fire, 30, 40 );
			SetResistance( ResistanceType.Cold, 30, 40 );
			SetResistance( ResistanceType.Poison, 40, 50 );
			SetResistance( ResistanceType.Energy, 40, 50 );

			SetSkill( SkillName.MagicResist, 125.1, 140.0 );
			SetSkill( SkillName.Tactics, 90.1, 100.0 );
			SetSkill( SkillName.Wrestling, 90.1, 100.0 );

			SetSkill( SkillName.Fencing, 125.1, 140.0 );
			SetSkill( SkillName.Macing, 125.1, 140.0 );
			SetSkill( SkillName.Wrestling, 125.1, 140.0 );
			SetSkill( SkillName.MagicResist, 67.5, 100.0 );
			SetSkill( SkillName.Swords, 125.1, 140.0 );
			SetSkill( SkillName.Tactics, 125.1, 140.0 );

			Fame = 10000;
			Karma = -10000;

			VirtualArmor = 50;

			Item cloth1 = new SavageArms();
			  	MorphingTime.MakeSpaceAceBoneArmor( cloth1, bone, false );
			  	AddItem( cloth1 );
			Item cloth2 = new SavageLegs();
			  	MorphingTime.MakeSpaceAceBoneArmor( cloth2, bone, false );
			  	AddItem( cloth2 );
			Item cloth3 = new LeatherSkirt();
				cloth3.Name = "skin skirt";
			  	MorphingTime.MakeSpaceAceBoneArmor( cloth3, bone, true );
			  	cloth3.Layer = Layer.Waist;
			  	AddItem( cloth3 );

			switch ( Utility.RandomMinMax( 0, 3 ) )
			{
				case 0: Item cloth4 = new OrcHelm(); MorphingTime.MakeSpaceAceBoneArmor( cloth4, bone, false ); AddItem( cloth4 ); break;
				case 1: Item cloth5 = new SavageHelm(); MorphingTime.MakeSpaceAceBoneArmor( cloth5, bone, false ); AddItem( cloth5 ); break;
				case 2: Item cloth6 = new TribalMask(); cloth6.Name = "skin mask"; MorphingTime.MakeSpaceAceBoneArmor( cloth6, bone, true ); AddItem( cloth6 ); break;
			}

			if ( Utility.RandomMinMax( 1, 10 ) == 1 )
			{
				Item cloth7 = new SavageChest();
					MorphingTime.MakeSpaceAceBoneArmor( cloth7, bone, false );
					AddItem( cloth7 );
			}
			else if ( Female )
			{
				Item cloth8 = new FemaleLeatherChest();
					cloth8.Name = "skin tunic";
					MorphingTime.MakeSpaceAceBoneArmor( cloth8, bone, true );
					AddItem( cloth8 );
			}

			IntelligentAction.GiveBasicWepShld( this );

			if ( this.FindItemOnLayer( Layer.OneHanded ) != null )
			{
				if ( MaterialInfo.IsAnyKindOfMetalItem( this.FindItemOnLayer( Layer.OneHanded ) ) )
				{
					MorphingTime.MakeSpaceAceMetalArmorWeapon( this.FindItemOnLayer( Layer.OneHanded ), metal );
				}
				else if ( MaterialInfo.IsAnyKindOfClothItem( this.FindItemOnLayer( Layer.OneHanded ) ) )
				{
					MorphingTime.MakeSpaceAceClothArmorWeapon( this.FindItemOnLayer( Layer.OneHanded ), cloth );
				}
				else if ( MaterialInfo.IsAnyKindOfWoodItem( this.FindItemOnLayer( Layer.OneHanded ) ) )
				{
					MorphingTime.MakeSpaceAceWoodArmorWeapon( this.FindItemOnLayer( Layer.OneHanded ), wood );
				}
			}

			if ( this.FindItemOnLayer( Layer.TwoHanded ) != null )
			{
				Item hand = this.FindItemOnLayer( Layer.TwoHanded );

				if ( hand is BaseShield )
				{
					switch( Utility.RandomMinMax( 1, 4 ) )
					{
						case 1: hand.ItemID = 0x1B76; hand.Name = "hull plate";		break;
						case 2: hand.ItemID = 0x1B76; hand.Name = "deck plate";		break;
						case 3: hand.ItemID = 0x1B72; hand.Name = "hatch door";		break;
						case 4: hand.ItemID = 0x1B7B; hand.Name = "hatch cover";	break;
					}
					MorphingTime.MakeSpaceAceMetalArmorWeapon( hand, metal );
				}
				else if ( MaterialInfo.IsAnyKindOfMetalItem( hand ) )
				{
					MorphingTime.MakeSpaceAceMetalArmorWeapon( hand, metal );
				}
				else if ( MaterialInfo.IsAnyKindOfClothItem( hand ) )
				{
					MorphingTime.MakeSpaceAceClothArmorWeapon( hand, cloth );
				}
				else if ( MaterialInfo.IsAnyKindOfWoodItem( hand ) )
				{
					MorphingTime.MakeSpaceAceWoodArmorWeapon( hand, wood );
				}
			}
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
			AddLoot( LootPack.Average );
			AddLoot( LootPack.Meager );
		}

		public override int Meat{ get{ return 1; } }
		public override bool ClickTitle{ get{ return false; } }
		public override bool ShowFameTitle{ get{ return false; } }
		public override bool AlwaysAttackable{ get{ return true; } }
		public override bool CanRummageCorpses{ get{ return true; } }

		public SavageAlien( Serial serial ) : base( serial )
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