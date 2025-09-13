using System;
using System.Collections;
using Server.Items;
using Server.ContextMenus;
using Server.Misc;
using Server.Network;

namespace Server.Mobiles
{
	[CorpseName( "a tribesman corpse" )]
	public class NativeArcher : BaseCreature
	{
		[Constructable]
		public NativeArcher() : base( AIType.AI_Archer, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			((BaseCreature)this).midrace = 1;
			Name = "a tribesman";
			Hue = 743;

			if ( this.Female = Utility.RandomBool() )
			{
				Body = 0x191;
				Item cloth9 = new FemaleLeatherChest();
					cloth9.Hue = 773;
					cloth9.Name = "Native Tunic";
					AddItem( cloth9 );
			}
			else
			{
				Body = 0x190;
			}

			HairHue = 0x96C;

			SetStr( 116, 135 );
			SetDex( 106, 125 );
			SetInt( 71, 85 );

			SetDamage( 23, 27 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetSkill( SkillName.Archery, 60.0, 82.5 );
			SetSkill( SkillName.Macing, 60.0, 82.5 );
			SetSkill( SkillName.Poisoning, 60.0, 82.5 );
			SetSkill( SkillName.MagicResist, 57.5, 80.0 );
			SetSkill( SkillName.Swords, 60.0, 82.5 );
			SetSkill( SkillName.Tactics, 60.0, 82.5 );

			Fame = 1100;
			Karma = -1100;
			VirtualArmor = 20;

			PackItem( new Bandage( Utility.RandomMinMax( 1, 15 ) ) );

			Item cloth1 = new SavageArms();
			  	cloth1.Hue = 773;
				cloth1.Name = "Native Guantlets";
			  	AddItem( cloth1 );
			Item cloth2 = new SavageLegs();
			  	cloth2.Hue = 773;
				cloth2.Name = "Native Leggings";
			  	AddItem( cloth2 );
			Item cloth3 = new TribalMask();
			  	cloth3.Hue = 773;
				cloth3.Name = "Native Tribal Mask";
			  	AddItem( cloth3 );
			Item cloth4 = new LeatherSkirt();
			  	cloth4.Hue = 773;
				cloth4.Name = "Native Skirt";
			  	cloth4.Layer = Layer.Waist;
			  	AddItem( cloth4 );

			AddItem( new Bow() );
			PackItem( new Arrow( Utility.RandomMinMax( 5, 15 ) ) );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Meager );
		}

		public override bool ClickTitle{ get{ return false; } }
		public override bool ShowFameTitle{ get{ return false; } }
		public override bool AlwaysAttackable{ get{ return true; } }
		public override bool CanRummageCorpses{ get{ return true; } }

		public NativeArcher( Serial serial ) : base( serial )
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