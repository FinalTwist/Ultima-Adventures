using System;
using Server;
using System.Collections; 
using Server.Items; 
using Server.ContextMenus; 
using Server.Misc; 
using Server.Network;
using Server.Mobiles;

namespace Server.Mobiles 
{
	[CorpseName( "an orcish corpse" )] 
	public class Urk : BaseCreature 
	{
		public override InhumanSpeech SpeechType{ get{ return InhumanSpeech.Orc; } }
		
		[Constructable] 
		public Urk() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 ) 
		{
			((BaseCreature)this).midrace = 5;
			BaseSoundID = 0x45A;
			Hue = 0x430;
			Body = 0x190; 
			Name = NameList.RandomName( "urk" );
			HairItemID = 0;
			FacialHairItemID = 0;

			Item helm = new WornHumanDeco();
				helm.Name = "urkish face";
				helm.ItemID = 0x141B;
				helm.Hue = 0x430;
				helm.Layer = Layer.Helm;
				AddItem( helm );

			if ( Utility.RandomMinMax( 1, 2 ) == 1 )
			{
				LeatherArms ratarms = new LeatherArms();
					ratarms.Name = "urkish rat skin arms";
					ratarms.PoisonBonus = 6;
					ratarms.Hue = 0x972;
					AddItem( ratarms );

				LeatherChest ratchest = new LeatherChest();
					ratchest.Name = "urkish rat skin tunic";
					ratchest.PoisonBonus = 8;
					ratchest.Hue = 0x972;
					AddItem( ratchest );

				LeatherGloves ratgloves = new LeatherGloves();
					ratgloves.Name = "urkish rat skin gloves";
					ratgloves.PoisonBonus = 5;
					ratgloves.Hue = 0x972;
					AddItem( ratgloves );

				LeatherGorget ratgorget = new LeatherGorget();
					ratgorget.Name = "urkish rat skin gorget";
					ratgorget.PoisonBonus = 4;
					ratgorget.Hue = 0x972;
					AddItem( ratgorget );

				LeatherLegs ratlegs = new LeatherLegs();
					ratlegs.Name = "urkish rat skin leggings";
					ratlegs.PoisonBonus = 7;
					ratlegs.Hue = 0x972;
					AddItem( ratlegs );
			}
			else
			{
				BoneChest bonechest = new BoneChest();
					bonechest.Name = "urkish chest piece";
					bonechest.PoisonBonus = 8;
					bonechest.Hue = 0x972;
					AddItem( bonechest );

				BoneArms bonearms = new BoneArms();
					bonearms.Name = "urkish bracers";
					bonearms.PoisonBonus = 6;
					bonearms.Hue = 0x972;
					AddItem( bonearms );

				BoneLegs bonelegs = new BoneLegs();
					bonelegs.Name = "urkish leggings";
					bonelegs.PoisonBonus = 7;
					bonelegs.Hue = 0x972;
					AddItem( bonelegs );

				BoneGloves bonegloves = new BoneGloves();
					bonegloves.Name = "urkish gauntlets";
					bonegloves.PoisonBonus = 5;
					bonegloves.Hue = 0x972;
					AddItem( bonegloves );
			}

			Item weapon = new BattleAxe();

			switch ( Utility.Random( 28 ))
			{
				case 0: weapon = new BattleAxe(); weapon.Name = "battle axe"; break;
				case 1: weapon = new VikingSword(); weapon.Name = "great sword"; break;
				case 2: weapon = new Halberd(); weapon.Name = "halberd"; break;
				case 3: weapon = new DoubleAxe(); weapon.Name = "double axe"; break;
				case 4: weapon = new ExecutionersAxe(); weapon.Name = "great axe"; break;
				case 5: weapon = new WarAxe(); weapon.Name = "war axe"; break;
				case 6: weapon = new TwoHandedAxe(); weapon.Name = "two handed axe"; break;
				case 7: weapon = new Cutlass(); weapon.Name = "cutlass"; break;
				case 8: weapon = new Katana(); weapon.Name = "katana"; break;
				case 9: weapon = new Kryss(); weapon.Name = "kryss"; break;
				case 10: weapon = new Broadsword(); weapon.Name = "broadsword"; break;
				case 11: weapon = new Longsword(); weapon.Name = "longsword"; break;
				case 12: weapon = new ThinLongsword(); weapon.Name = "longsword"; break;
				case 13: weapon = new Scimitar(); weapon.Name = "scimitar"; break;
				case 14: weapon = new BoneHarvester(); weapon.Name = "sickle"; break;
				case 15: weapon = new CrescentBlade(); weapon.Name = "crescent blade"; break;
				case 16: weapon = new DoubleBladedStaff(); weapon.Name = "double bladed staff"; break;
				case 17: weapon = new Pike(); weapon.Name = "pike"; break;
				case 18: weapon = new Scythe(); weapon.Name = "scythe"; break;
				case 19: weapon = new Pitchfork(); weapon.Name = "trident"; break;
				case 20: weapon = new ShortSpear(); weapon.Name = "short spear"; break;
				case 21: weapon = new Spear(); weapon.Name = "spear"; break;
				case 22: weapon = new Club(); weapon.Name = "club"; break;
				case 23: weapon = new HammerPick(); weapon.Name = "hammer pick"; break;
				case 24: weapon = new Mace(); weapon.Name = "mace"; break;
				case 25: weapon = new Maul(); weapon.Name = "maul"; break;
				case 26: weapon = new WarHammer(); weapon.Name = "war hammer"; break;
				case 27: weapon = new WarMace(); weapon.Name = "war mace"; break;
			}

			weapon.Name = "urkish " + weapon.Name;
			weapon.Hue = 0x7D1;
			((BaseWeapon)weapon).AosElementDamages.Physical = 60;
			((BaseWeapon)weapon).AosElementDamages.Poison = 40;
			AddItem( weapon );

			switch ( Utility.RandomMinMax( 0, 5 ) )
			{
				case 0: Title = "the urk warrior"; break;
				case 1: Title = "the urk savage"; break;
				case 2: Title = "the urk barbarian"; break;
				case 3: Title = "the urk fighter"; break;
				case 4: Title = "the urk gladiator"; break;
				case 5: Title = "the urk berserker"; break;
			}

			SetStr( 196, 250 );
			SetDex( 76, 95 );
			SetInt( 36, 60 );

			SetHits( 118, 150 );

			SetDamage( 8, 18 );

			SetDamageType( ResistanceType.Physical, 60 );
			SetDamageType( ResistanceType.Poison, 40 );

			SetResistance( ResistanceType.Physical, 35, 45 );
			SetResistance( ResistanceType.Fire, 20, 30 );
			SetResistance( ResistanceType.Cold, 50, 60 );
			SetResistance( ResistanceType.Poison, 60, 80 );
			SetResistance( ResistanceType.Energy, 30, 40 );

			SetSkill( SkillName.MagicResist, 65.1, 80.0 );
			SetSkill( SkillName.Tactics, 85.1, 100.0 );
			SetSkill( SkillName.Macing, 85.1, 95.0 );
			SetSkill( SkillName.Swords, 85.1, 95.0 );
			SetSkill( SkillName.Fencing, 85.1, 95.0 );
			SetSkill( SkillName.Wrestling, 85.1, 95.0 );

			Fame = 3000;
			Karma = -3000;

			VirtualArmor = 40;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
			AddLoot( LootPack.Meager );
		}

		public override bool ClickTitle{ get{ return false; } }
		public override bool ShowFameTitle{ get{ return false; } }
		public override bool CanRummageCorpses{ get{ return true; } }
		public override bool AlwaysAttackable{ get{ return true; } }
		public override int Meat{ get{ return 1; } }

		public override bool OnBeforeDeath()
		{
			this.Body = 17;
			return base.OnBeforeDeath();
		}

		public override OppositionGroup OppositionGroup
		{
			get{ return OppositionGroup.SavagesAndOrcs; }
		}

		public override void OnGaveMeleeAttack( Mobile defender )
		{
			base.OnGaveMeleeAttack( defender );
			Server.Misc.IntelligentAction.PoisonVictim( defender, this );
		}

		public Urk( Serial serial ) : base( serial ) 
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