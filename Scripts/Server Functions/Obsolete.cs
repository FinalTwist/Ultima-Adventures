using Server.Accounting;
using Server.Commands.Generic;
using Server.Commands;
using Server.ContextMenus;
using Server.Engines.CannedEvil;
using Server.Engines.Craft;
using Server.Engines.Plants;
using Server.Engines.VeteranRewards;
using Server.Guilds;
using Server.Gumps;
using Server.Items;
using Server.Misc;
using Server.Mobiles;
using Server.Multis;
using Server.Network;
using Server.Prompts;
using Server.Regions;
using Server.Spells.Fifth;
using Server.Spells.First;
using Server.Spells.Fourth;
using Server.Spells.Second;
using Server.Spells.Seventh;
using Server.Spells.Sixth;
using Server.Spells.Third;
using Server.Spells;
using Server.Targeting;
using Server.Targets;
using Server;
using System.Collections.Generic;
using System.Collections;
using System.Data.Odbc;
using System.Data;
using System.Globalization;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Xml;
using System;


// using System;// using Server;// using Server.Items;


namespace Server.Mobiles
{
	[CorpseName( "a wyrm corpse" )]
	public class AmethystWyrm : BaseCreature
	{
		public override int BreathPhysicalDamage{ get{ return 0; } }
		public override int BreathFireDamage{ get{ return 0; } }
		public override int BreathColdDamage{ get{ return 0; } }
		public override int BreathPoisonDamage{ get{ return 0; } }
		public override int BreathEnergyDamage{ get{ return 100; } }
		public override int BreathEffectHue{ get{ return 0x9C2; } }
		public override int BreathEffectSound{ get{ return 0x665; } }
		public override int BreathEffectItemID{ get{ return 0x3818; } }
		public override bool ReacquireOnMovement{ get{ return !Controlled; } }
		public override bool HasBreath{ get{ return true; } }
		public override void BreathDealDamage( Mobile target, int form ){ base.BreathDealDamage( target, 1 ); }


		[Constructable]
		public AmethystWyrm () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = NameList.RandomName( "dragon" );
			Title = "the amethyst wyrm";
			BaseSoundID = 362;
			Body = Server.Misc.MyServerSettings.WyrmBody();
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "amethyst", "monster", 0 );


			SetStr( 721, 760 );
			SetDex( 101, 130 );
			SetInt( 386, 425 );


			SetHits( 433, 456 );


			SetDamage( 17, 25 );


			SetDamageType( ResistanceType.Physical, 75 );
			SetDamageType( ResistanceType.Energy, 25 );


			SetResistance( ResistanceType.Physical, 65, 75 );
			SetResistance( ResistanceType.Energy, 80, 90 );
			SetResistance( ResistanceType.Cold, 70, 80 );
			SetResistance( ResistanceType.Poison, 60, 70 );
			SetResistance( ResistanceType.Fire, 60, 70 );


			SetSkill( SkillName.EvalInt, 99.1, 100.0 );
			SetSkill( SkillName.Magery, 99.1, 100.0 );
			SetSkill( SkillName.MagicResist, 99.1, 100.0 );
			SetSkill( SkillName.Tactics, 97.6, 100.0 );
			SetSkill( SkillName.Wrestling, 90.1, 100.0 );


			Fame = 18000;
			Karma = -18000;


			VirtualArmor = 64;


			Tamable = true;
			ControlSlots = 3;
			MinTameSkill = 96.3;
		}


		public override void OnDeath( Container c )
		{
			base.OnDeath( c );


			Item scale = new HardScales( Utility.RandomMinMax( 15, 20 ), "amethyst scales" );
   			c.DropItem(scale);
		}


		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 2 );
			AddLoot( LootPack.Average );
			AddLoot( LootPack.Gems, Utility.Random( 1, 5 ) );
		}


		public override int GetAttackSound(){ return 0x63E; }	// A
		public override int GetDeathSound(){ return 0x63F; }	// D
		public override int GetHurtSound(){ return 0x640; }		// H
		public override bool AutoDispel{ get{ return !Controlled; } }
		public override int TreasureMapLevel{ get{ return 5; } }
		public override bool BleedImmune{ get{ return true; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Gold; } }
		public override bool CanAngerOnTame { get { return true; } }


		public AmethystWyrm( Serial serial ) : base( serial )
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


			Body = Server.Misc.MyServerSettings.WyrmBody();
		}
	}
}
// using System;// using Server;// using Server.Items;// using Server.Mobiles;


namespace Server.Mobiles
{
	[CorpseName( "a nightmare corpse" )]
	public class AncientNightmare : BaseCreature
	{
		public override bool HasBreath{ get{ return true; } }
		public override bool ReacquireOnMovement{ get{ return !Controlled; } }
		public override double BreathEffectDelay{ get{ return 0.1; } }
		public override void BreathDealDamage( Mobile target, int form ){ base.BreathDealDamage( target, 9 ); }


		[Constructable]
		public AncientNightmare() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "an ancient nightmare";
			Body = 795;
			BaseSoundID = 0xA8;


			SetStr( 496, 525 );
			SetDex( 86, 105 );
			SetInt( 86, 125 );


			SetHits( 298, 315 );


			SetDamage( 16, 22 );


			SetDamageType( ResistanceType.Physical, 40 );
			SetDamageType( ResistanceType.Fire, 40 );
			SetDamageType( ResistanceType.Energy, 20 );


			SetResistance( ResistanceType.Physical, 55, 65 );
			SetResistance( ResistanceType.Fire, 30, 40 );
			SetResistance( ResistanceType.Cold, 30, 40 );
			SetResistance( ResistanceType.Poison, 30, 40 );
			SetResistance( ResistanceType.Energy, 20, 30 );


			SetSkill( SkillName.EvalInt, 10.4, 50.0 );
			SetSkill( SkillName.Magery, 10.4, 50.0 );
			SetSkill( SkillName.MagicResist, 85.3, 100.0 );
			SetSkill( SkillName.Tactics, 97.6, 100.0 );
			SetSkill( SkillName.Wrestling, 80.5, 92.5 );


			Fame = 14000;
			Karma = -14000;


			VirtualArmor = 60;


			PackItem( new SulfurousAsh( Utility.RandomMinMax( 13, 19 ) ) );


			AddItem( new LightSource() );
		}


		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.Average );
			AddLoot( LootPack.LowScrolls );
			AddLoot( LootPack.Potions );
		}


		public override int Meat{ get{ return 5; } }
		public override int Hides{ get{ return 10; } }
		public override HideType HideType{ get{ return HideType.Hellish; } }
		public override int Furs{ get{ return Utility.RandomList( 0, 0, 0, 5 ); } }
		public override FurType FurType{ get{ return FurType.Regular; } }


		public AncientNightmare( Serial serial ) : base( serial )
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
// using System;// using Server;// using Server.Items;// using Server.Misc;


namespace Server.Mobiles
{
	[CorpseName( "a dragon corpse" )]
	public class BlackDragon : BaseCreature
	{
		public override int BreathPhysicalDamage{ get{ return 0; } }
		public override int BreathFireDamage{ get{ return 0; } }
		public override int BreathColdDamage{ get{ return 0; } }
		public override int BreathPoisonDamage{ get{ return 100; } }
		public override int BreathEnergyDamage{ get{ return 0; } }
		public override int BreathEffectHue{ get{ return 0x3F; } }
		public override int BreathEffectSound{ get{ return 0x658; } }
		public override bool ReacquireOnMovement{ get{ return !Controlled; } }
		public override bool HasBreath{ get{ return true; } }
		public override void BreathDealDamage( Mobile target, int form ){ base.BreathDealDamage( target, 10 ); }


		[Constructable]
		public BlackDragon () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a black dragon";
			Body = 12;
			Hue = 0x966;
			BaseSoundID = 362;


			SetStr( 796, 825 );
			SetDex( 86, 105 );
			SetInt( 436, 475 );


			SetHits( 478, 495 );


			SetDamage( 16, 22 );


			SetDamageType( ResistanceType.Physical, 75 );
			SetDamageType( ResistanceType.Poison, 25 );


			SetResistance( ResistanceType.Physical, 55, 65 );
			SetResistance( ResistanceType.Poison, 60, 70 );
			SetResistance( ResistanceType.Cold, 30, 40 );
			SetResistance( ResistanceType.Fire, 25, 35 );
			SetResistance( ResistanceType.Energy, 35, 45 );


			SetSkill( SkillName.EvalInt, 30.1, 40.0 );
			SetSkill( SkillName.Magery, 30.1, 40.0 );
			SetSkill( SkillName.MagicResist, 99.1, 100.0 );
			SetSkill( SkillName.Tactics, 97.6, 100.0 );
			SetSkill( SkillName.Wrestling, 90.1, 92.5 );


			Fame = 15000;
			Karma = -15000;


			VirtualArmor = 60;


			Tamable = true;
			ControlSlots = 3;
			MinTameSkill = 93.9;
		}


		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 2 );
			AddLoot( LootPack.Gems, 8 );
		}


		public override void OnDeath( Container c )
		{
			base.OnDeath( c );


			Mobile killer = this.LastKiller;
			if ( killer != null )
			{
				if ( killer is BaseCreature )
					killer = ((BaseCreature)killer).GetMaster();


				if ( killer is PlayerMobile )
				{
					Server.Mobiles.Dragons.DropSpecial( this, killer, "", "Black", "", c, 25, 0 );
				}
			}
		}


		public override bool AutoDispel{ get{ return !Controlled; } }
		public override int TreasureMapLevel{ get{ return 4; } }
		public override int Meat{ get{ return 19; } }
		public override int Hides{ get{ return 20; } }
		public override HideType HideType{ get{ return HideType.Draconic; } }
		public override int Scales{ get{ return 7; } }
		public override ScaleType ScaleType{ get{ return ( ScaleType.Black ); } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat; } }
		public override bool CanAngerOnTame { get { return true; } }


		public BlackDragon( Serial serial ) : base( serial )
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
// using System;// using Server;// using Server.Items;// using Server.Misc;


namespace Server.Mobiles
{
	[CorpseName( "a dragon corpse" )]
	public class BlueDragon : BaseCreature
	{
		public override int BreathPhysicalDamage{ get{ return 0; } }
		public override int BreathFireDamage{ get{ return 0; } }
		public override int BreathColdDamage{ get{ return 0; } }
		public override int BreathPoisonDamage{ get{ return 0; } }
		public override int BreathEnergyDamage{ get{ return 100; } }
		public override int BreathEffectHue{ get{ return 0x9C2; } }
		public override int BreathEffectSound{ get{ return 0x665; } }
		public override int BreathEffectItemID{ get{ return 0x3818; } }
		public override bool ReacquireOnMovement{ get{ return !Controlled; } }
		public override bool HasBreath{ get{ return true; } }
		public override void BreathDealDamage( Mobile target, int form ){ base.BreathDealDamage( target, 13 ); }


		[Constructable]
		public BlueDragon () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a blue dragon";
			Body = 12;
			Hue = 0x1F4;
			BaseSoundID = 362;


			SetStr( 796, 825 );
			SetDex( 86, 105 );
			SetInt( 436, 475 );


			SetHits( 478, 495 );


			SetDamage( 16, 22 );


			SetDamageType( ResistanceType.Physical, 75 );
			SetDamageType( ResistanceType.Energy, 25 );


			SetResistance( ResistanceType.Physical, 55, 65 );
			SetResistance( ResistanceType.Energy, 60, 70 );
			SetResistance( ResistanceType.Cold, 30, 40 );
			SetResistance( ResistanceType.Poison, 25, 35 );
			SetResistance( ResistanceType.Fire, 35, 45 );


			SetSkill( SkillName.EvalInt, 30.1, 40.0 );
			SetSkill( SkillName.Magery, 30.1, 40.0 );
			SetSkill( SkillName.MagicResist, 99.1, 100.0 );
			SetSkill( SkillName.Tactics, 97.6, 100.0 );
			SetSkill( SkillName.Wrestling, 90.1, 92.5 );


			Fame = 15000;
			Karma = -15000;


			VirtualArmor = 60;


			Tamable = true;
			ControlSlots = 3;
			MinTameSkill = 93.9;
		}


		public override void OnDeath( Container c )
		{
			base.OnDeath( c );


			Mobile killer = this.LastKiller;
			if ( killer != null )
			{
				if ( killer is BaseCreature )
					killer = ((BaseCreature)killer).GetMaster();


				if ( killer is PlayerMobile )
				{
					Server.Mobiles.Dragons.DropSpecial( this, killer, "", "Blue", "", c, 25, 0 );
				}
			}
		}


		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 2 );
			AddLoot( LootPack.Gems, 8 );
		}


		public override bool AutoDispel{ get{ return !Controlled; } }
		public override int TreasureMapLevel{ get{ return 4; } }
		public override int Meat{ get{ return 19; } }
		public override int Hides{ get{ return 20; } }
		public override HideType HideType{ get{ return HideType.Draconic; } }
		public override int Scales{ get{ return 7; } }
		public override ScaleType ScaleType{ get{ return ( ScaleType.Blue ); } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat; } }
		public override bool CanAngerOnTame { get { return true; } }


		public BlueDragon( Serial serial ) : base( serial )
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

namespace Server.Mobiles
{
	[CorpseName( "a bullradon corpse" )]
	public class Bullradon : BaseCreature
	{
		[Constructable]
		public Bullradon() : base( AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			Name = "a bullradon";
			Body = 0x11C;


			SetStr( 146, 175 );
			SetDex( 111, 150 );
			SetInt( 46, 60 );


			SetHits( 131, 160 );
			SetMana( 0 );


			SetDamage( 6, 11 );


			SetDamageType( ResistanceType.Physical, 100 );


			SetResistance( ResistanceType.Physical, 50, 70 );
			SetResistance( ResistanceType.Fire, 30, 50 );
			SetResistance( ResistanceType.Cold, 30, 50 );
			SetResistance( ResistanceType.Poison, 40, 60 );
			SetResistance( ResistanceType.Energy, 30, 50 );


			SetSkill( SkillName.MagicResist, 37.6, 42.5 );
			SetSkill( SkillName.Tactics, 70.6, 83.0 );
			SetSkill( SkillName.Wrestling, 50.1, 57.5 );


			Fame = 2000;
			Karma = -2000;


			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 68.7;
		}


		public override int GetAngerSound()
		{
			return 0x4F8;
		}


		public override int GetIdleSound()
		{
			return 0x4F7;
		}


		public override int GetAttackSound()
		{
			return 0x4F6;
		}


		public override int GetHurtSound()
		{
			return 0x4F9;
		}


		public override int GetDeathSound()
		{
			return 0x4F5;
		}


		public override int Meat{ get{ return 10; } }
		public override int Hides{ get{ return 15; } }
		public override FoodType FavoriteFood{ get{ return FoodType.GrainsAndHay; } }
		public override ScaleType ScaleType{ get{ return ( ScaleType.Dinosaur ); } }


		public Bullradon( Serial serial ) : base( serial )
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
			Body = 0x11C;
		}
	}
}// using System;// using System.Xml;// using Server;// using Server.Regions;// using Server.Mobiles;



namespace Server.Mobiles
{
	[CorpseName( "a cave bear corpse" )]
	public class CaveBear : BaseCreature
	{
		[Constructable]
		public CaveBear() : base( AIType.AI_Melee,FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a cave bear";
			Body = 190;
			BaseSoundID = 0xA3;


			SetStr( 226, 255 );
			SetDex( 121, 145 );
			SetInt( 16, 40 );


			SetHits( 176, 193 );
			SetMana( 0 );


			SetDamage( 14, 19 );


			SetDamageType( ResistanceType.Physical, 100 );


			SetResistance( ResistanceType.Physical, 45, 55 );
			SetResistance( ResistanceType.Cold, 35, 45 );
			SetResistance( ResistanceType.Poison, 15, 20 );
			SetResistance( ResistanceType.Energy, 15, 20 );


			SetSkill( SkillName.MagicResist, 35.1, 50.0 );
			SetSkill( SkillName.Tactics, 90.1, 120.0 );
			SetSkill( SkillName.Wrestling, 65.1, 90.0 );


			Fame = 1500;
			Karma = 0;


			VirtualArmor = 35;


			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 69.1;
		}


		public override int Meat{ get{ return 2; } }
		public override int Hides{ get{ return 16; } }
		public override int Furs{ get{ return Utility.RandomList( 0, 0, 0, 8 ); } }
		public override FurType FurType{ get{ return FurType.Regular; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Fish | FoodType.FruitsAndVegies | FoodType.Meat; } }
		public override PackInstinct PackInstinct{ get{ return PackInstinct.Bear; } }


		public CaveBear( Serial serial ) : base( serial )
		{
		}


		public override void Serialize(GenericWriter writer)
		{
			base.Serialize( writer );


			writer.Write( (int) 0 );
		}


		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );


			int version = reader.ReadInt();
		}
	}
}
// using System;// using Server;// using Server.Items;// using Server.Mobiles;


namespace Server.Mobiles
{
	[CorpseName( "a dark unicorn corpse" )]
	public class DarkUnicorn : BaseCreature
	{
		[Constructable]
		public DarkUnicorn() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a dark unicorn";
			Body = 27;
			BaseSoundID = 0xA8;


			SetStr( 596, 625 );
			SetDex( 186, 205 );
			SetInt( 186, 225 );


			SetHits( 398, 415 );


			SetDamage( 22, 28 );


			SetDamageType( ResistanceType.Physical, 40 );
			SetDamageType( ResistanceType.Fire, 40 );
			SetDamageType( ResistanceType.Energy, 20 );


			SetResistance( ResistanceType.Physical, 55, 65 );
			SetResistance( ResistanceType.Fire, 30, 40 );
			SetResistance( ResistanceType.Cold, 30, 40 );
			SetResistance( ResistanceType.Poison, 30, 40 );
			SetResistance( ResistanceType.Energy, 20, 30 );


			SetSkill( SkillName.EvalInt, 30.4, 70.0 );
			SetSkill( SkillName.Magery, 30.4, 70.0 );
			SetSkill( SkillName.MagicResist, 105.3, 120.0 );
			SetSkill( SkillName.Tactics, 117.6, 120.0 );
			SetSkill( SkillName.Wrestling, 100.5, 112.5 );


			Fame = 19000;
			Karma = -19000;


			VirtualArmor = 70;


			AddItem( new LightSource() );
		}


		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.Average );
			AddLoot( LootPack.LowScrolls );
			AddLoot( LootPack.Potions );
		}


		public override int GetAngerSound()
		{
			if ( !Controlled )
				return 0x16A;


			return base.GetAngerSound();
		}


		public override int Meat{ get{ return 5; } }
		public override int Hides{ get{ return 10; } }
		public override int Furs{ get{ return Utility.RandomList( 0, 0, 0, 5 ); } }
		public override FurType FurType{ get{ return FurType.Regular; } }


		public DarkUnicorn( Serial serial ) : base( serial )
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


			if ( BaseSoundID == 0x16A )
				BaseSoundID = 0xA8;
		}
	}
}
// using System;// using Server;// using Server.Items;


namespace Server.Mobiles
{
	[CorpseName( "a wyrm corpse" )]
	public class DeepSeaDragon : BaseCreature
	{
		public override int BreathPhysicalDamage{ get{ return 0; } }
		public override int BreathFireDamage{ get{ return 0; } }
		public override int BreathColdDamage{ get{ return 0; } }
		public override int BreathPoisonDamage{ get{ return 100; } }
		public override int BreathEnergyDamage{ get{ return 0; } }
		public override int BreathEffectHue{ get{ return 0x3F; } }
		public override int BreathEffectSound{ get{ return 0x658; } }
		public override bool ReacquireOnMovement{ get{ return !Controlled; } }
		public override bool HasBreath{ get{ return true; } }
		public override void BreathDealDamage( Mobile target, int form ){ base.BreathDealDamage( target, 10 ); }


		[Constructable]
		public DeepSeaDragon () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = NameList.RandomName( "dragon" );
			Title = "the sea wyrm";
			Body = Server.Misc.MyServerSettings.WyrmBody();
			Hue = 1365;
			BaseSoundID = 362;
			CanSwim = true;


			SetStr( 796, 825 );
			SetDex( 86, 105 );
			SetInt( 436, 475 );


			SetHits( 478, 495 );


			SetDamage( 16, 22 );


			SetDamageType( ResistanceType.Physical, 75 );
			SetDamageType( ResistanceType.Poison, 25 );


			SetResistance( ResistanceType.Physical, 65, 75 );
			SetResistance( ResistanceType.Poison, 80, 90 );
			SetResistance( ResistanceType.Cold, 70, 80 );
			SetResistance( ResistanceType.Fire, 60, 70 );
			SetResistance( ResistanceType.Energy, 60, 70 );


			SetSkill( SkillName.EvalInt, 30.1, 40.0 );
			SetSkill( SkillName.Magery, 30.1, 40.0 );
			SetSkill( SkillName.MagicResist, 99.1, 100.0 );
			SetSkill( SkillName.Tactics, 97.6, 100.0 );
			SetSkill( SkillName.Wrestling, 90.1, 92.5 );


			Fame = 15000;
			Karma = -15000;


			Tamable = true;
			ControlSlots = 3;
			MinTameSkill = 96.3;


			VirtualArmor = 60;


			if ( 1 == Utility.RandomMinMax( 0, 2 ) )
			{
				switch ( Utility.RandomMinMax( 0, 5 ) )
				{
					case 0: PackItem( new SeaweedLegs() ); break;
					case 1: PackItem( new SeaweedGloves() ); break;
					case 2: PackItem( new SeaweedGorget() ); break;
					case 3: PackItem( new SeaweedArms() ); break;
					case 4: PackItem( new SeaweedChest() ); break;
					case 5: PackItem( new SeaweedHelm() ); break;
				}
			}
		}


		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 2 );
			AddLoot( LootPack.Gems, 8 );
		}


		public override int GetAttackSound(){ return 0x63E; }	// A
		public override int GetDeathSound(){ return 0x63F; }	// D
		public override int GetHurtSound(){ return 0x640; }		// H


		public override bool BleedImmune{ get{ return true; } }
		public override bool AutoDispel{ get{ return !Controlled; } }
		public override int TreasureMapLevel{ get{ return 5; } }
		public override int Meat{ get{ return 19; } }
		public override MeatType MeatType{ get{ return MeatType.Fish; } }
		public override int Hides{ get{ return 20; } }
		public override HideType HideType{ get{ if ( Utility.RandomBool() ){ return HideType.Spined; } else { return HideType.Draconic; } } }
		public override int Scales{ get{ return 7; } }
		public override ScaleType ScaleType{ get{ return ( ScaleType.Blue ); } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat | FoodType.Fish; } }
		public override bool CanAngerOnTame { get { return true; } }


		public DeepSeaDragon( Serial serial ) : base( serial )
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
			Body = Server.Misc.MyServerSettings.WyrmBody();
		}
	}
}
// using System;// using Server;// using Server.Items;


namespace Server.Mobiles
{
	[CorpseName( "a wyrm corpse" )]
	public class DesertWyrm : BaseCreature
	{
		public override int BreathPhysicalDamage{ get{ return 50; } }
		public override int BreathFireDamage{ get{ return 50; } }
		public override int BreathColdDamage{ get{ return 0; } }
		public override int BreathPoisonDamage{ get{ return 0; } }
		public override int BreathEnergyDamage{ get{ return 0; } }
		public override int BreathEffectHue{ get{ return 0x96D; } }
		public override bool ReacquireOnMovement{ get{ return true; } }
		public override bool HasBreath{ get{ return true; } }
		public override int BreathEffectSound{ get{ return 0x654; } }
		public override void BreathDealDamage( Mobile target, int form ){ base.BreathDealDamage( target, 8 ); }


		[Constructable]
		public DesertWyrm () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = NameList.RandomName( "dragon" );
			Title = "the desert wyrm";
			BaseSoundID = 362;
			Hue = 1719;
			Body = Server.Misc.MyServerSettings.WyrmBody();


			SetStr( 721, 760 );
			SetDex( 101, 130 );
			SetInt( 386, 425 );


			SetHits( 433, 456 );


			SetDamage( 17, 25 );


			SetDamageType( ResistanceType.Physical, 75 );
			SetDamageType( ResistanceType.Fire, 25 );


			SetResistance( ResistanceType.Physical, 65, 75 );
			SetResistance( ResistanceType.Fire, 80, 90 );
			SetResistance( ResistanceType.Cold, 70, 80 );
			SetResistance( ResistanceType.Poison, 60, 70 );
			SetResistance( ResistanceType.Energy, 60, 70 );


			SetSkill( SkillName.EvalInt, 99.1, 100.0 );
			SetSkill( SkillName.Magery, 99.1, 100.0 );
			SetSkill( SkillName.MagicResist, 99.1, 100.0 );
			SetSkill( SkillName.Tactics, 97.6, 100.0 );
			SetSkill( SkillName.Wrestling, 90.1, 100.0 );


			Fame = 18000;
			Karma = -18000;


			VirtualArmor = 64;


			Tamable = true;
			ControlSlots = 3;
			MinTameSkill = 96.3;
		}


		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 2 );
			AddLoot( LootPack.Average );
			AddLoot( LootPack.Gems, Utility.Random( 1, 5 ) );
		}


		public override int GetAttackSound(){ return 0x63E; }	// A
		public override int GetDeathSound(){ return 0x63F; }	// D
		public override int GetHurtSound(){ return 0x640; }		// H
		public override int TreasureMapLevel{ get{ return 5; } }
		public override bool AutoDispel{ get{ return !Controlled; } }
		public override int Meat{ get{ return 19; } }
		public override int Hides{ get{ return 20; } }
		public override HideType HideType{ get{ return HideType.Draconic; } }
		public override int Scales{ get{ return 9; } }
		public override ScaleType ScaleType{ get{ return ScaleType.Yellow; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat | FoodType.Meat; } }
		public override bool CanAngerOnTame { get { return true; } }


		public DesertWyrm( Serial serial ) : base( serial )
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


			Body = Server.Misc.MyServerSettings.WyrmBody();
		}
	}
}
// using System;// using Server;// using Server.Items;// using Server.Misc;


namespace Server.Mobiles
{
	[CorpseName( "a dragon corpse" )]
	public class Dragon : BaseCreature
	{
		public override bool ReacquireOnMovement{ get{ return !Controlled; } }
		public override bool HasBreath{ get{ return true; } }
		public override void BreathDealDamage( Mobile target, int form ){ base.BreathDealDamage( target, 9 ); }


		[Constructable]
		public Dragon () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a red dragon";
			Body = 59;
			BaseSoundID = 362;


			SetStr( 796, 825 );
			SetDex( 86, 105 );
			SetInt( 436, 475 );


			SetHits( 478, 495 );


			SetDamage( 16, 22 );


			SetDamageType( ResistanceType.Physical, 100 );


			SetResistance( ResistanceType.Physical, 55, 65 );
			SetResistance( ResistanceType.Fire, 60, 70 );
			SetResistance( ResistanceType.Cold, 30, 40 );
			SetResistance( ResistanceType.Poison, 25, 35 );
			SetResistance( ResistanceType.Energy, 35, 45 );


			SetSkill( SkillName.EvalInt, 30.1, 40.0 );
			SetSkill( SkillName.Magery, 30.1, 40.0 );
			SetSkill( SkillName.MagicResist, 99.1, 100.0 );
			SetSkill( SkillName.Tactics, 97.6, 100.0 );
			SetSkill( SkillName.Wrestling, 90.1, 92.5 );


			Fame = 15000;
			Karma = -15000;


			VirtualArmor = 60;


			Tamable = true;
			ControlSlots = 3;
			MinTameSkill = 93.9;
		}


		public override void OnDeath( Container c )
		{
			base.OnDeath( c );


			Mobile killer = this.LastKiller;
			if ( killer != null )
			{
				if ( killer is BaseCreature )
					killer = ((BaseCreature)killer).GetMaster();


				if ( killer is PlayerMobile )
				{
					Server.Mobiles.Dragons.DropSpecial( this, killer, "", "Red", "", c, 25, 0x845 );
				}
			}
		}


		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 2 );
			AddLoot( LootPack.Gems, 8 );
		}


		public override bool AutoDispel{ get{ return !Controlled; } }
		public override int TreasureMapLevel{ get{ return 4; } }
		public override int Meat{ get{ return 19; } }
		public override int Hides{ get{ return 20; } }
		public override HideType HideType{ get{ return HideType.Draconic; } }
		public override int Scales{ get{ return 7; } }
		public override ScaleType ScaleType{ get{ return ( ScaleType.Red ); } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat; } }
		public override bool CanAngerOnTame { get { return true; } }


		public Dragon( Serial serial ) : base( serial )
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
// using System;// using Server;// using Server.Mobiles;


namespace Server.Mobiles
{
	[CorpseName( "a bear corpse" )]
	public class ElderBlackBear : BaseCreature
	{
		public override WeaponAbility GetWeaponAbility()
		{
			return WeaponAbility.BleedAttack;
		}


		[Constructable]
		public ElderBlackBear() : base( AIType.AI_Melee,FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "an elder black bear";
			Body = 177;
			BaseSoundID = 0xA3;


			SetStr( 226, 255 );
			SetDex( 121, 145 );
			SetInt( 16, 40 );


			SetHits( 176, 193 );
			SetMana( 0 );


			SetDamage( 14, 19 );


			SetDamageType( ResistanceType.Physical, 100 );


			SetResistance( ResistanceType.Physical, 45, 55 );
			SetResistance( ResistanceType.Cold, 35, 45 );
			SetResistance( ResistanceType.Poison, 15, 20 );
			SetResistance( ResistanceType.Energy, 15, 20 );


			SetSkill( SkillName.MagicResist, 35.1, 50.0 );
			SetSkill( SkillName.Tactics, 90.1, 120.0 );
			SetSkill( SkillName.Wrestling, 65.1, 90.0 );


			Fame = 1500;
			Karma = 0;


			VirtualArmor = 35;


			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 69.1;
		}


		public override int Meat{ get{ return 3; } }
		public override int Hides{ get{ return 18; } }
		public override int Furs{ get{ return Utility.RandomList( 0, 0, 0, 8 ); } }
		public override FurType FurType{ get{ return FurType.Regular; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Fish | FoodType.Meat | FoodType.FruitsAndVegies; } }
		public override PackInstinct PackInstinct{ get{ return PackInstinct.Bear; } }


		public ElderBlackBear( Serial serial ) : base( serial )
		{
		}


		public override void Serialize(GenericWriter writer)
		{
			base.Serialize( writer );


			writer.Write( (int) 0 );
		}


		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );


			int version = reader.ReadInt();
		}
	}
}
// using System;// using Server.Mobiles;// using Server.Items;


namespace Server.Mobiles
{
	[CorpseName( "a bear corpse" )]
	public class ElderBrownBear : BaseCreature
	{
		public override WeaponAbility GetWeaponAbility()
		{
			return WeaponAbility.BleedAttack;
		}


		[Constructable]
		public ElderBrownBear() : base( AIType.AI_Melee,FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "an elder brown bear";
			Body = 34;
			BaseSoundID = 0xA3;


			SetStr( 226, 255 );
			SetDex( 121, 145 );
			SetInt( 16, 40 );


			SetHits( 176, 193 );
			SetMana( 0 );


			SetDamage( 14, 19 );


			SetDamageType( ResistanceType.Physical, 100 );


			SetResistance( ResistanceType.Physical, 45, 55 );
			SetResistance( ResistanceType.Cold, 35, 45 );
			SetResistance( ResistanceType.Poison, 15, 20 );
			SetResistance( ResistanceType.Energy, 15, 20 );


			SetSkill( SkillName.MagicResist, 35.1, 50.0 );
			SetSkill( SkillName.Tactics, 90.1, 120.0 );
			SetSkill( SkillName.Wrestling, 65.1, 90.0 );


			Fame = 1500;
			Karma = 0;


			VirtualArmor = 35;


			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 69.1;
		}


		public override int Meat{ get{ return 3; } }
		public override int Hides{ get{ return 18; } }
		public override int Furs{ get{ return Utility.RandomList( 0, 0, 0, 8 ); } }
		public override FurType FurType{ get{ return FurType.Regular; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Fish | FoodType.FruitsAndVegies | FoodType.Meat; } }
		public override PackInstinct PackInstinct{ get{ return PackInstinct.Bear; } }


		public ElderBrownBear( Serial serial ) : base( serial )
		{
		}


		public override void Serialize(GenericWriter writer)
		{
			base.Serialize( writer );


			writer.Write( (int) 0 );
		}


		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );


			int version = reader.ReadInt();
		}
	}
}
// using System;// using Server.Mobiles;// using Server.Items;


namespace Server.Mobiles
{
	[CorpseName( "a bear corpse" )]
	public class ElderPolarBear : BaseCreature
	{
		public override WeaponAbility GetWeaponAbility()
		{
			return WeaponAbility.BleedAttack;
		}


		[Constructable]
		public ElderPolarBear() : base( AIType.AI_Melee,FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "an elder polar bear";
			Body = 179;
			BaseSoundID = 0xA3;


			SetStr( 226, 255 );
			SetDex( 121, 145 );
			SetInt( 16, 40 );


			SetHits( 176, 193 );
			SetMana( 0 );


			SetDamage( 14, 19 );


			SetDamageType( ResistanceType.Physical, 100 );


			SetResistance( ResistanceType.Physical, 45, 55 );
			SetResistance( ResistanceType.Cold, 35, 45 );
			SetResistance( ResistanceType.Poison, 15, 20 );
			SetResistance( ResistanceType.Energy, 15, 20 );


			SetSkill( SkillName.MagicResist, 35.1, 50.0 );
			SetSkill( SkillName.Tactics, 90.1, 120.0 );
			SetSkill( SkillName.Wrestling, 65.1, 90.0 );


			Fame = 1500;
			Karma = 0;


			VirtualArmor = 35;


			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 69.1;
		}


		public override int Meat{ get{ return 3; } }
		public override int Hides{ get{ return 18; } }
		public override int Furs{ get{ return Utility.RandomList( 0, 0, 0, 8 ); } }
		public override FurType FurType{ get{ return FurType.White; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Fish | FoodType.Meat | FoodType.FruitsAndVegies; } }
		public override PackInstinct PackInstinct{ get{ return PackInstinct.Bear; } }


		public ElderPolarBear( Serial serial ) : base( serial )
		{
		}


		public override void Serialize(GenericWriter writer)
		{
			base.Serialize( writer );


			writer.Write( (int) 0 );
		}


		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );


			int version = reader.ReadInt();
		}
	}
}
// using System;// using Server;// using Server.Items;


namespace Server.Mobiles
{
	[CorpseName( "a wyrm corpse" )]
	public class EmeraldWyrm : BaseCreature
	{
		public override int BreathPhysicalDamage{ get{ return 0; } }
		public override int BreathFireDamage{ get{ return 0; } }
		public override int BreathColdDamage{ get{ return 0; } }
		public override int BreathPoisonDamage{ get{ return 100; } }
		public override int BreathEnergyDamage{ get{ return 0; } }
		public override int BreathEffectHue{ get{ return 0x3F; } }
		public override int BreathEffectSound{ get{ return 0x658; } }
		public override bool ReacquireOnMovement{ get{ return !Controlled; } }
		public override bool HasBreath{ get{ return true; } }
		public override void BreathDealDamage( Mobile target, int form ){ base.BreathDealDamage( target, 10 ); }


		[Constructable]
		public EmeraldWyrm () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = NameList.RandomName( "dragon" );
			Title = "the emerald wyrm";
			BaseSoundID = 362;
			Body = Server.Misc.MyServerSettings.WyrmBody();
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "emerald", "monster", 0 );


			SetStr( 721, 760 );
			SetDex( 101, 130 );
			SetInt( 386, 425 );


			SetHits( 433, 456 );


			SetDamage( 17, 25 );


			SetDamageType( ResistanceType.Physical, 75 );
			SetDamageType( ResistanceType.Poison, 25 );


			SetResistance( ResistanceType.Physical, 65, 75 );
			SetResistance( ResistanceType.Poison, 80, 90 );
			SetResistance( ResistanceType.Cold, 70, 80 );
			SetResistance( ResistanceType.Fire, 60, 70 );
			SetResistance( ResistanceType.Energy, 60, 70 );


			SetSkill( SkillName.EvalInt, 99.1, 100.0 );
			SetSkill( SkillName.Magery, 99.1, 100.0 );
			SetSkill( SkillName.MagicResist, 99.1, 100.0 );
			SetSkill( SkillName.Tactics, 97.6, 100.0 );
			SetSkill( SkillName.Wrestling, 90.1, 100.0 );


			Fame = 18000;
			Karma = -18000;


			VirtualArmor = 64;


			Tamable = true;
			ControlSlots = 3;
			MinTameSkill = 96.3;
		}


		public override void OnDeath( Container c )
		{
			base.OnDeath( c );


			Item scale = new HardScales( Utility.RandomMinMax( 15, 20 ), "emerald scales" );
   			c.DropItem(scale);
		}


		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 2 );
			AddLoot( LootPack.Average );
			AddLoot( LootPack.Gems, Utility.Random( 1, 5 ) );
		}


		public override int GetAttackSound(){ return 0x63E; }	// A
		public override int GetDeathSound(){ return 0x63F; }	// D
		public override int GetHurtSound(){ return 0x640; }		// H
		public override bool AutoDispel{ get{ return !Controlled; } }
		public override int TreasureMapLevel{ get{ return 5; } }
		public override bool BleedImmune{ get{ return true; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Gold; } }
		public override bool CanAngerOnTame { get { return true; } }


		public EmeraldWyrm( Serial serial ) : base( serial )
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


			Body = Server.Misc.MyServerSettings.WyrmBody();
		}
	}
}
// using System;// using Server;// using Server.Items;


namespace Server.Mobiles
{
	[CorpseName( "a wyrm corpse" )]
	public class GarnetWyrm : BaseCreature
	{
		public override int BreathPhysicalDamage{ get{ return 0; } }
		public override int BreathFireDamage{ get{ return 0; } }
		public override int BreathColdDamage{ get{ return 0; } }
		public override int BreathPoisonDamage{ get{ return 100; } }
		public override int BreathEnergyDamage{ get{ return 0; } }
		public override int BreathEffectHue{ get{ return 0x3F; } }
		public override int BreathEffectSound{ get{ return 0x658; } }
		public override bool ReacquireOnMovement{ get{ return !Controlled; } }
		public override bool HasBreath{ get{ return true; } }
		public override void BreathDealDamage( Mobile target, int form ){ base.BreathDealDamage( target, 10 ); }


		[Constructable]
		public GarnetWyrm () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = NameList.RandomName( "dragon" );
			Title = "the garnet wyrm";
			BaseSoundID = 362;
			Body = Server.Misc.MyServerSettings.WyrmBody();
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "garnet", "monster", 0 );


			SetStr( 721, 760 );
			SetDex( 101, 130 );
			SetInt( 386, 425 );


			SetHits( 433, 456 );


			SetDamage( 17, 25 );


			SetDamageType( ResistanceType.Physical, 75 );
			SetDamageType( ResistanceType.Poison, 25 );


			SetResistance( ResistanceType.Physical, 65, 75 );
			SetResistance( ResistanceType.Poison, 80, 90 );
			SetResistance( ResistanceType.Cold, 60, 70 );
			SetResistance( ResistanceType.Fire, 60, 70 );
			SetResistance( ResistanceType.Energy, 60, 70 );


			SetSkill( SkillName.EvalInt, 99.1, 100.0 );
			SetSkill( SkillName.Magery, 99.1, 100.0 );
			SetSkill( SkillName.MagicResist, 99.1, 100.0 );
			SetSkill( SkillName.Tactics, 97.6, 100.0 );
			SetSkill( SkillName.Wrestling, 90.1, 100.0 );


			Fame = 18000;
			Karma = -18000;


			VirtualArmor = 64;


			Tamable = true;
			ControlSlots = 3;
			MinTameSkill = 96.3;
		}


		public override void OnDeath( Container c )
		{
			base.OnDeath( c );


			Item scale = new HardScales( Utility.RandomMinMax( 15, 20 ), "garnet scales" );
   			c.DropItem(scale);
		}


		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 2 );
			AddLoot( LootPack.Average );
			AddLoot( LootPack.Gems, Utility.Random( 1, 5 ) );
		}


		public override int GetAttackSound(){ return 0x63E; }	// A
		public override int GetDeathSound(){ return 0x63F; }	// D
		public override int GetHurtSound(){ return 0x640; }		// H
		public override bool AutoDispel{ get{ return !Controlled; } }
		public override int TreasureMapLevel{ get{ return 5; } }
		public override bool BleedImmune{ get{ return true; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Gold; } }
		public override bool CanAngerOnTame { get { return true; } }


		public GarnetWyrm( Serial serial ) : base( serial )
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


			Body = Server.Misc.MyServerSettings.WyrmBody();
		}
	}
}

namespace Server.Mobiles
{
	[CorpseName( "a beetle's corpse" )]
	public class GlowBeetle : BaseCreature
	{
		[Constructable]
		public GlowBeetle () : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a glow beetle";
			Body = 0xA9;
			BaseSoundID = 0x388;


			SetStr( 156, 180 );
			SetDex( 86, 105 );
			SetInt( 6, 10 );


			SetHits( 110, 150 );


			SetDamage( 7, 14 );


			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Energy, 50 );


			SetResistance( ResistanceType.Physical, 40, 50 );
			SetResistance( ResistanceType.Fire, 20, 30 );
			SetResistance( ResistanceType.Cold, 20, 30 );
			SetResistance( ResistanceType.Poison, 20, 30 );
			SetResistance( ResistanceType.Energy, 90, 100 );


			SetSkill( SkillName.Tactics, 55.1, 70.0 );
			SetSkill( SkillName.Wrestling, 60.1, 75.0 );


			Fame = 4000;
			Karma = -4000;


			VirtualArmor = 26;


			AddItem( new LighterSource() );
		}


		public override void GenerateLoot()
		{
			AddLoot( LootPack.Meager );
		}


		public override void OnGotMeleeAttack( Mobile attacker )
		{
			base.OnGotMeleeAttack( attacker );


			if ( Utility.RandomMinMax( 1, 4 ) == 1 )
			{
				int goo = 0;


				foreach ( Item splash in this.GetItemsInRange( 10 ) ){ if ( splash is MonsterSplatter && splash.Name == "glowing goo" ){ goo++; } }


				if ( goo == 0 )
				{
					MonsterSplatter.AddSplatter( this.X, this.Y, this.Z, this.Map, this.Location, this, "glowing goo", 0xB93, 1 );
				}
			}
		}


		public GlowBeetle( Serial serial ) : base( serial )
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


			if ( BaseSoundID == 263 )
				BaseSoundID = 1170;


			Body = 0xA9;
		}
	}
}
// using System;// using System.Collections;// using Server;// using Server.Items;// using Server.Network;


namespace Server.Mobiles
{
	[CorpseName( "a gorceratops corpse" )]
	public class Gorceratops : BaseCreature
	{
		public override WeaponAbility GetWeaponAbility()
		{
			return WeaponAbility.BleedAttack;
		}


		[Constructable]
		public Gorceratops () : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a gorceratops";
			Body = 0x11C;
			BaseSoundID = 0x4F5;
			Hue = Utility.RandomList( 0x7D7, 0x7D8, 0x7D9, 0x7DA, 0x7DB, 0x7DC );


			SetStr( 176, 205 );
			SetDex( 46, 65 );
			SetInt( 46, 70 );


			SetHits( 106, 123 );


			SetDamage( 8, 14 );


			SetDamageType( ResistanceType.Physical, 100 );


			SetResistance( ResistanceType.Physical, 35, 45 );
			SetResistance( ResistanceType.Fire, 25, 35 );
			SetResistance( ResistanceType.Cold, 15, 25 );
			SetResistance( ResistanceType.Poison, 5, 15 );
			SetResistance( ResistanceType.Energy, 5, 15 );


			SetSkill( SkillName.Tactics, 50.1, 70.0 );
			SetSkill( SkillName.Wrestling, 50.1, 70.0 );


			Fame = 3500;
			Karma = -3500;


			VirtualArmor = 40;


			Tamable = true;
			ControlSlots = 2;
			MinTameSkill = 63.9;
		}


		public override void GenerateLoot()
		{
			AddLoot( LootPack.Meager );
		}


		public override int Meat{ get{ return 5; } }
		public override int Hides{ get{ return 10; } }
		public override HideType HideType{ get{ return HideType.Dinosaur; } }
		public override int Scales{ get{ return 4; } }
		public override ScaleType ScaleType{ get{ return ( ScaleType.Dinosaur ); } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat; } }
		public override bool CanAngerOnTame { get { return true; } }


		public Gorceratops( Serial serial ) : base( serial )
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
			Body = 0x11C;
		}
	}
}
// using System;// using System.Collections;// using Server;// using Server.Items;// using Server.Network;


namespace Server.Mobiles
{
	[CorpseName( "a gorgon corpse" )]
	public class Gorgon : BaseCreature
	{
		[Constructable]
		public Gorgon () : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a gorgon";
			Body = 0x11C;
			BaseSoundID = 362;
			Hue = 0x961;


			SetStr( 176, 205 );
			SetDex( 46, 65 );
			SetInt( 46, 70 );


			SetHits( 106, 123 );


			SetDamage( 8, 14 );


			SetDamageType( ResistanceType.Physical, 100 );


			SetResistance( ResistanceType.Physical, 35, 45 );
			SetResistance( ResistanceType.Fire, 25, 35 );
			SetResistance( ResistanceType.Cold, 15, 25 );
			SetResistance( ResistanceType.Poison, 5, 15 );
			SetResistance( ResistanceType.Energy, 5, 15 );


			SetSkill( SkillName.MagicResist, 45.1, 60.0 );
			SetSkill( SkillName.Tactics, 50.1, 70.0 );
			SetSkill( SkillName.Wrestling, 50.1, 70.0 );


			Fame = 3500;
			Karma = -3500;


			VirtualArmor = 40;
		}


		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
		}


		public void TurnStone()
		{
			ArrayList list = new ArrayList();


			foreach ( Mobile m in this.GetMobilesInRange( 2 ) )
			{
				if ( m == this || !CanBeHarmful( m ) )
					continue;


				if ( m is BaseCreature && (((BaseCreature)m).Controlled || ((BaseCreature)m).Summoned || ((BaseCreature)m).Team != this.Team) )
					list.Add( m );
				else if ( m.Player )
					list.Add( m );
			}


			foreach ( Mobile m in list )
			{
				DoHarmful( m );


				m.PlaySound(0x16B);
				m.FixedEffect(0x376A, 6, 1);


				int duration = Utility.RandomMinMax(4, 8);
				m.Paralyze(TimeSpan.FromSeconds(duration));


				m.SendMessage( "You are petrified from the gorgon breath!" );
			}
		}


		public override void OnGaveMeleeAttack( Mobile m )
		{
			base.OnGaveMeleeAttack( m );


			if ( 1 == Utility.RandomMinMax( 1, 20 ) )
			{
				Container cont = m.Backpack;
				Item iStone = Server.Items.HiddenTrap.GetMyItem( m );


				if ( iStone != null )
				{
					if ( m.CheckSkill( SkillName.MagicResist, 0, 100 ) || Server.Items.HiddenTrap.IAmAWeaponSlayer( m, this ) )
					{
					}
					else if ( Server.Items.HiddenTrap.CheckInsuranceOnTrap( iStone, m ) == true )
					{
						m.LocalOverheadMessage(MessageType.Emote, 1150, true, "The gorgon almost turned one of your protected items to stone!");
					}
					else
					{
						m.LocalOverheadMessage(MessageType.Emote, 0xB1F, true, "One of your items has been turned to stone!");
						m.PlaySound( 0x1FB );
						Item rock = new BrokenGear();
						rock.ItemID = iStone.ItemID;
						rock.Hue = 2101;
						rock.Weight = iStone.Weight * 3;
						rock.Name = "useless stone";
						iStone.Delete();
						m.AddToBackpack ( rock );
					}
				}
			}


			if ( 0.1 >= Utility.RandomDouble() )
				TurnStone();
		}


		public override void OnGotMeleeAttack( Mobile attacker )
		{
			base.OnGotMeleeAttack( attacker );


			if ( 0.1 >= Utility.RandomDouble() )
				TurnStone();
		}


		public Gorgon( Serial serial ) : base( serial )
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
			Body = 0x11C;
		}
	}
}
// using Server;// using System;// using Server.Misc;// using Server.Mobiles;


namespace Server.Mobiles
{
	[CorpseName( "a dragon corpse" )]
	public class GreenDragon : BaseCreature
	{
		public override bool ReacquireOnMovement{ get{ return !Controlled; } }
		public override bool HasBreath{ get{ return true; } }
		public override void BreathDealDamage( Mobile target, int form ){ base.BreathDealDamage( target, 9 ); }


		[Constructable]
		public GreenDragon () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a green dragon";
			Body = 12;
			Hue = 2001;
			BaseSoundID = 362;


			SetStr( 796, 825 );
			SetDex( 86, 105 );
			SetInt( 436, 475 );


			SetHits( 478, 495 );


			SetDamage( 16, 22 );


			SetDamageType( ResistanceType.Physical, 75 );
			SetDamageType( ResistanceType.Fire, 25 );


			SetResistance( ResistanceType.Physical, 55, 65 );
			SetResistance( ResistanceType.Fire, 60, 70 );
			SetResistance( ResistanceType.Cold, 30, 40 );
			SetResistance( ResistanceType.Poison, 25, 35 );
			SetResistance( ResistanceType.Energy, 35, 45 );


			SetSkill( SkillName.EvalInt, 30.1, 40.0 );
			SetSkill( SkillName.Magery, 30.1, 40.0 );
			SetSkill( SkillName.MagicResist, 99.1, 100.0 );
			SetSkill( SkillName.Tactics, 97.6, 100.0 );
			SetSkill( SkillName.Wrestling, 90.1, 92.5 );


			Fame = 15000;
			Karma = -15000;


			VirtualArmor = 60;


			Tamable = true;
			ControlSlots = 3;
			MinTameSkill = 93.9;
		}


		public override void OnDeath( Container c )
		{
			base.OnDeath( c );


			Mobile killer = this.LastKiller;
			if ( killer != null )
			{
				if ( killer is BaseCreature )
					killer = ((BaseCreature)killer).GetMaster();


				if ( killer is PlayerMobile )
				{
					Server.Mobiles.Dragons.DropSpecial( this, killer, "", "Green", "", c, 25, 0 );
				}
			}
		}


		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 2 );
			AddLoot( LootPack.Gems, 8 );
		}


		public override bool AutoDispel{ get{ return !Controlled; } }
		public override int TreasureMapLevel{ get{ return 4; } }
		public override int Meat{ get{ return 19; } }
		public override int Hides{ get{ return 20; } }
		public override HideType HideType{ get{ return HideType.Draconic; } }
		public override int Scales{ get{ return 7; } }
		public override ScaleType ScaleType{ get{ return ( ScaleType.Green ); } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat; } }
		public override bool CanAngerOnTame { get { return true; } }


		public GreenDragon( Serial serial ) : base( serial )
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
// using System;// using System.Collections;// using Server.Items;// using Server.Targeting;


namespace Server.Mobiles
{
	[CorpseName( "a griffon corpse" )]
	public class Griffon : BaseCreature
	{
		public override WeaponAbility GetWeaponAbility()
		{
			return WeaponAbility.BleedAttack;
		}


		[Constructable]
		public Griffon() : base( AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			Name = "a griffon";
			Body = 0x31F;
			BaseSoundID = 0x2EE;


			SetStr( 196, 220 );
			SetDex( 186, 210 );
			SetInt( 151, 175 );


			SetHits( 158, 172 );


			SetDamage( 9, 15 );


			SetDamageType( ResistanceType.Physical, 100 );


			SetResistance( ResistanceType.Physical, 25, 30 );
			SetResistance( ResistanceType.Fire, 10, 20 );
			SetResistance( ResistanceType.Cold, 10, 30 );
			SetResistance( ResistanceType.Poison, 20, 30 );
			SetResistance( ResistanceType.Energy, 10, 20 );


			SetSkill( SkillName.MagicResist, 50.1, 65.0 );
			SetSkill( SkillName.Tactics, 70.1, 100.0 );
			SetSkill( SkillName.Wrestling, 60.1, 90.0 );


			Fame = 3500;
			Karma = 3500;


			VirtualArmor = 32;
		}


		public override void GenerateLoot()
		{
			AddLoot( LootPack.Meager, 2 );
		}


		public override int Meat{ get{ return 12; } }
		public override MeatType MeatType{ get{ return MeatType.Bird; } }
		public override int Feathers{ get{ return 50; } }


		public Griffon( Serial serial ) : base( serial )
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
			Body = 0x31F;
		}
	}
}
// using System;// using Server.Mobiles;


namespace Server.Mobiles
{
	[CorpseName( "a grizzly bear corpse" )]
	[TypeAlias( "Server.Mobiles.Grizzlybear" )]
	public class GrizzlyBear : BaseCreature
	{
		[Constructable]
		public GrizzlyBear() : base( AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			Name = "a grizzly bear";
			Body = 212;
			BaseSoundID = 0xA3;


			SetStr( 126, 155 );
			SetDex( 81, 105 );
			SetInt( 16, 40 );


			SetHits( 76, 93 );
			SetMana( 0 );


			SetDamage( 8, 13 );


			SetDamageType( ResistanceType.Physical, 100 );


			SetResistance( ResistanceType.Physical, 25, 35 );
			SetResistance( ResistanceType.Cold, 15, 25 );
			SetResistance( ResistanceType.Poison, 5, 10 );
			SetResistance( ResistanceType.Energy, 5, 10 );


			SetSkill( SkillName.MagicResist, 25.1, 40.0 );
			SetSkill( SkillName.Tactics, 70.1, 100.0 );
			SetSkill( SkillName.Wrestling, 45.1, 70.0 );


			Fame = 1000;
			Karma = 0;


			VirtualArmor = 24;


			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 59.1;
		}


		public override int Meat{ get{ return 2; } }
		public override int Hides{ get{ return 16; } }
		public override int Furs{ get{ return Utility.RandomList( 0, 0, 0, 8 ); } }
		public override FurType FurType{ get{ return FurType.Regular; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Fish | FoodType.FruitsAndVegies | FoodType.Meat; } }
		public override PackInstinct PackInstinct{ get{ return PackInstinct.Bear; } }


		public GrizzlyBear( Serial serial ) : base( serial )
		{
		}


		public override void Serialize(GenericWriter writer)
		{
			base.Serialize( writer );


			writer.Write( (int) 0 );
		}


		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );


			int version = reader.ReadInt();
		}
	}
}
// using System;// using Server;


namespace Server.Mobiles
{
	[CorpseName( "a hippogriff corpse" )]
	public class Hippogriff : BaseCreature
	{
		public override WeaponAbility GetWeaponAbility()
		{
			return WeaponAbility.BleedAttack;
		}


		[Constructable]
		public Hippogriff() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a hippogriff";
			Body = 188;
			BaseSoundID = 0x2EE;


			SetStr( 196, 220 );
			SetDex( 186, 210 );
			SetInt( 151, 175 );


			SetHits( 158, 172 );


			SetDamage( 9, 15 );


			SetDamageType( ResistanceType.Physical, 100 );


			SetResistance( ResistanceType.Physical, 25, 30 );
			SetResistance( ResistanceType.Fire, 10, 20 );
			SetResistance( ResistanceType.Cold, 10, 30 );
			SetResistance( ResistanceType.Poison, 20, 30 );
			SetResistance( ResistanceType.Energy, 10, 20 );


			SetSkill( SkillName.MagicResist, 50.1, 65.0 );
			SetSkill( SkillName.Tactics, 70.1, 100.0 );
			SetSkill( SkillName.Wrestling, 60.1, 90.0 );


			Fame = 3500;
			Karma = 3500;


			VirtualArmor = 32;
		}


		public override void GenerateLoot()
		{
			AddLoot( LootPack.Meager, 2 );
		}


		public override int Meat{ get{ return 12; } }
		public override MeatType MeatType{ get{ return MeatType.Bird; } }
		public override int Feathers{ get{ return 50; } }


		public Hippogriff( Serial serial ) : base( serial )
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
			Body = 188;
		}
	}
}

// using System;// using Server;// using Server.Items;


namespace Server.Mobiles
{
	[CorpseName( "a wyrm corpse" )]
	public class IceDragon : BaseCreature
	{
		public override int BreathPhysicalDamage{ get{ return 0; } }
		public override int BreathFireDamage{ get{ return 0; } }
		public override int BreathColdDamage{ get{ return 100; } }
		public override int BreathPoisonDamage{ get{ return 0; } }
		public override int BreathEnergyDamage{ get{ return 0; } }
		public override int BreathEffectHue{ get{ return 0x481; } }
		public override int BreathEffectSound{ get{ return 0x64F; } }
		public override bool ReacquireOnMovement{ get{ return !Controlled; } }
		public override bool HasBreath{ get{ return true; } }
		public override void BreathDealDamage( Mobile target, int form ){ base.BreathDealDamage( target, 12 ); }


		[Constructable]
		public IceDragon () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = NameList.RandomName( "dragon" );
			Title = "the ice wyrm";
			Body = 46;
			Hue = 1154;
			BaseSoundID = 362;


			SetStr( 796, 825 );
			SetDex( 86, 105 );
			SetInt( 436, 475 );


			SetHits( 478, 495 );


			SetDamage( 16, 22 );


			SetDamageType( ResistanceType.Physical, 75 );
			SetDamageType( ResistanceType.Cold, 25 );


			SetResistance( ResistanceType.Physical, 65, 75 );
			SetResistance( ResistanceType.Cold, 80, 90 );
			SetResistance( ResistanceType.Fire, 40, 60 );
			SetResistance( ResistanceType.Poison, 60, 70 );
			SetResistance( ResistanceType.Energy, 60, 70 );


			SetSkill( SkillName.EvalInt, 30.1, 40.0 );
			SetSkill( SkillName.Magery, 30.1, 40.0 );
			SetSkill( SkillName.MagicResist, 99.1, 100.0 );
			SetSkill( SkillName.Tactics, 97.6, 100.0 );
			SetSkill( SkillName.Wrestling, 90.1, 92.5 );


			Fame = 15000;
			Karma = -15000;


			VirtualArmor = 60;


			Tamable = true;
			ControlSlots = 3;
			MinTameSkill = 96.3;


			if ( 1 == Utility.RandomMinMax( 0, 2 ) )
			{
				switch ( Utility.RandomMinMax( 0, 5 ) )
				{
					case 0: PackItem( new IcySkinLegs() ); break;
					case 1: PackItem( new IcySkinGloves() ); break;
					case 2: PackItem( new IcySkinGorget() ); break;
					case 3: PackItem( new IcySkinArms() ); break;
					case 4: PackItem( new IcySkinChest() ); break;
					case 5: PackItem( new IcySkinHelm() ); break;
				}
			}


			AddItem( new LighterSource() );
		}


		public override void OnDeath( Container c )
		{
			base.OnDeath( c );


			Item scale = new HardScales( Utility.RandomMinMax( 15, 20 ), "ice scales" );
   			c.DropItem(scale);
		}


		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 2 );
			AddLoot( LootPack.Gems, 8 );
		}


		public override int GetAttackSound(){ return 0x63E; }	// A
		public override int GetDeathSound(){ return 0x63F; }	// D
		public override int GetHurtSound(){ return 0x640; }		// H
		public override int Hides{ get{ return 20; } }
		public override HideType HideType{ get{ if ( Utility.RandomBool() ){ return HideType.Frozen; } else { return HideType.Draconic; } } }
		public override bool AutoDispel{ get{ return !Controlled; } }
		public override int TreasureMapLevel{ get{ return 5; } }
		public override bool BleedImmune{ get{ return true; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Gold; } }
		public override bool CanAngerOnTame { get { return true; } }


		public IceDragon( Serial serial ) : base( serial )
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
// using System;// using Server;// using Server.Items;


namespace Server.Mobiles
{
	[CorpseName( "a wyrm corpse" )]
	public class JungleWyrm : BaseCreature
	{
		public override int BreathPhysicalDamage{ get{ return 0; } }
		public override int BreathFireDamage{ get{ return 0; } }
		public override int BreathColdDamage{ get{ return 0; } }
		public override int BreathPoisonDamage{ get{ return 100; } }
		public override int BreathEnergyDamage{ get{ return 0; } }
		public override int BreathEffectHue{ get{ return 0x3F; } }
		public override int BreathEffectSound{ get{ return 0x658; } }
		public override bool ReacquireOnMovement{ get{ return !Controlled; } }
		public override bool HasBreath{ get{ return true; } }
		public override void BreathDealDamage( Mobile target, int form ){ base.BreathDealDamage( target, 10 ); }


		[Constructable]
		public JungleWyrm () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = NameList.RandomName( "dragon" );
			Title = "the jungle wyrm";
			BaseSoundID = 362;
			Body = Server.Misc.MyServerSettings.WyrmBody();
			Hue = 0x7D1;


			SetStr( 721, 760 );
			SetDex( 101, 130 );
			SetInt( 386, 425 );


			SetHits( 433, 456 );


			SetDamage( 17, 25 );


			SetDamageType( ResistanceType.Physical, 75 );
			SetDamageType( ResistanceType.Poison, 25 );


			SetResistance( ResistanceType.Physical, 65, 75 );
			SetResistance( ResistanceType.Poison, 80, 90 );
			SetResistance( ResistanceType.Cold, 70, 80 );
			SetResistance( ResistanceType.Fire, 60, 70 );
			SetResistance( ResistanceType.Energy, 60, 70 );


			SetSkill( SkillName.EvalInt, 99.1, 100.0 );
			SetSkill( SkillName.Magery, 99.1, 100.0 );
			SetSkill( SkillName.MagicResist, 99.1, 100.0 );
			SetSkill( SkillName.Tactics, 97.6, 100.0 );
			SetSkill( SkillName.Wrestling, 90.1, 100.0 );


			Fame = 18000;
			Karma = -18000;


			VirtualArmor = 64;


			Tamable = true;
			ControlSlots = 3;
			MinTameSkill = 96.3;
		}


		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 2 );
			AddLoot( LootPack.Average );
			AddLoot( LootPack.Gems, Utility.Random( 1, 5 ) );
		}


		public override int GetAttackSound(){ return 0x63E; }	// A
		public override int GetDeathSound(){ return 0x63F; }	// D
		public override int GetHurtSound(){ return 0x640; }		// H
		public override int TreasureMapLevel{ get{ return 5; } }
		public override bool AutoDispel{ get{ return !Controlled; } }
		public override int Meat{ get{ return 19; } }
		public override int Hides{ get{ return 20; } }
		public override HideType HideType{ get{ return HideType.Draconic; } }
		public override int Scales{ get{ return 9; } }
		public override ScaleType ScaleType{ get{ return ScaleType.Green; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat | FoodType.Meat; } }
		public override bool CanAngerOnTame { get { return true; } }


		public JungleWyrm( Serial serial ) : base( serial )
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


			Body = Server.Misc.MyServerSettings.WyrmBody();
		}
	}
}
// using System;// using Server;// using Server.Items;


namespace Server.Mobiles
{
	[CorpseName( "a wyrm corpse" )]
	public class LavaDragon : BaseCreature
	{
		public override bool ReacquireOnMovement{ get{ return !Controlled; } }
		public override bool HasBreath{ get{ return true; } }
		public override void BreathDealDamage( Mobile target, int form ){ base.BreathDealDamage( target, 9 ); }


		[Constructable]
		public LavaDragon () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = NameList.RandomName( "dragon" );
			Title = "the fire wyrm";
			Body = Server.Misc.MyServerSettings.WyrmBody();
			Hue = 0xB71;
			BaseSoundID = 362;


			SetStr( 796, 825 );
			SetDex( 86, 105 );
			SetInt( 436, 475 );


			SetHits( 478, 495 );


			SetDamage( 16, 22 );


			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Fire, 50 );


			SetResistance( ResistanceType.Physical, 55, 65 );
			SetResistance( ResistanceType.Fire, 100 );
			SetResistance( ResistanceType.Cold, 15, 25 );
			SetResistance( ResistanceType.Poison, 25, 35 );
			SetResistance( ResistanceType.Energy, 35, 45 );


			SetSkill( SkillName.EvalInt, 30.1, 40.0 );
			SetSkill( SkillName.Magery, 30.1, 40.0 );
			SetSkill( SkillName.MagicResist, 99.1, 100.0 );
			SetSkill( SkillName.Tactics, 97.6, 100.0 );
			SetSkill( SkillName.Wrestling, 90.1, 92.5 );


			Fame = 15000;
			Karma = -15000;


			VirtualArmor = 60;


			Tamable = true;
			ControlSlots = 3;
			MinTameSkill = 96.3;


			if ( 1 == Utility.RandomMinMax( 0, 2 ) )
			{
				switch ( Utility.RandomMinMax( 0, 5 ) )
				{
					case 0: PackItem( new LavaSkinLegs() ); break;
					case 1: PackItem( new LavaSkinGloves() ); break;
					case 2: PackItem( new LavaSkinGorget() ); break;
					case 3: PackItem( new LavaSkinArms() ); break;
					case 4: PackItem( new LavaSkinChest() ); break;
					case 5: PackItem( new LavaSkinHelm() ); break;
				}
			}
		}


		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 2 );
			AddLoot( LootPack.Gems, 8 );
		}


		public override int GetAttackSound(){ return 0x63E; }	// A
		public override int GetDeathSound(){ return 0x63F; }	// D
		public override int GetHurtSound(){ return 0x640; }		// H
		public override bool AutoDispel{ get{ return !Controlled; } }
		public override int TreasureMapLevel{ get{ return 5; } }
		public override int Meat{ get{ return 19; } }
		public override int Hides{ get{ return 20; } }
		public override HideType HideType{ get{ return HideType.Volcanic; } }
		public override int Scales{ get{ return 7; } }
		public override ScaleType ScaleType{ get{ return ( ScaleType.Red ); } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat | FoodType.Meat; } }
		public override bool CanAngerOnTame { get { return true; } }


		public LavaDragon( Serial serial ) : base( serial )
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
			Body = Server.Misc.MyServerSettings.WyrmBody();
		}
	}
}
// using Server;// using System;// using Server.Misc;// using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a feline corpse" )]
	public class Lion : BaseCreature
	{
		public override WeaponAbility GetWeaponAbility()
		{
			return WeaponAbility.BleedAttack;
		}


		[Constructable]
		public Lion() : base( AIType.AI_Melee,FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a lion";
			Body = 187;
			BaseSoundID = 0x3EE;


			SetStr( 112, 160 );
			SetDex( 120, 190 );
			SetInt( 50, 76 );


			SetHits( 64, 88 );
			SetMana( 0 );


			SetDamage( 8, 16 );


			SetDamageType( ResistanceType.Physical, 100 );


			SetResistance( ResistanceType.Physical, 30, 35 );
			SetResistance( ResistanceType.Fire, 5, 10 );
			SetResistance( ResistanceType.Cold, 10, 15 );
			SetResistance( ResistanceType.Poison, 5, 10 );


			SetSkill( SkillName.MagicResist, 15.1, 30.0 );
			SetSkill( SkillName.Tactics, 45.1, 60.0 );
			SetSkill( SkillName.Wrestling, 45.1, 60.0 );


			Fame = 750;
			Karma = 0;


			VirtualArmor = 22;


			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 61.1;
		}


		public override int Meat{ get{ return 1; } }
		public override int Hides{ get{ return 10; } }
		public override int Furs{ get{ return Utility.RandomList( 0, 0, 0, 5 ); } }
		public override FurType FurType{ get{ return FurType.Regular; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Fish | FoodType.Meat; } }
		public override PackInstinct PackInstinct{ get{ return PackInstinct.Feline; } }


		public Lion(Serial serial) : base(serial)
		{
		}


		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);
		}


		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
			Body = 187;
		}
	}
}
// using System;// using Server;// using Server.Items;// using Server.Misc;


namespace Server.Mobiles
{
	[CorpseName( "a dragon corpse" )]
	public class MetalDragon : BaseCreature
	{
		public override bool ReacquireOnMovement{ get{ return !Controlled; } }
		public override bool HasBreath{ get{ return true; } }
		public override void BreathDealDamage( Mobile target, int form ){ base.BreathDealDamage( target, 9 ); }


		[Constructable]
		public MetalDragon () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a metallic dragon";
			Body = 59;
			BaseSoundID = 362;
			Hue = MaterialInfo.GetMaterialColor( "copper", "monster", Hue );


			SetStr( 796, 825 );
			SetDex( 86, 105 );
			SetInt( 436, 475 );


			SetHits( 478, 495 );


			SetDamage( 16, 22 );


			SetDamageType( ResistanceType.Physical, 75 );
			SetDamageType( ResistanceType.Fire, 25 );


			SetResistance( ResistanceType.Physical, 55, 65 );
			SetResistance( ResistanceType.Fire, 60, 70 );
			SetResistance( ResistanceType.Cold, 30, 40 );
			SetResistance( ResistanceType.Poison, 25, 35 );
			SetResistance( ResistanceType.Energy, 35, 45 );


			SetSkill( SkillName.EvalInt, 30.1, 40.0 );
			SetSkill( SkillName.Magery, 30.1, 40.0 );
			SetSkill( SkillName.MagicResist, 99.1, 100.0 );
			SetSkill( SkillName.Tactics, 97.6, 100.0 );
			SetSkill( SkillName.Wrestling, 90.1, 92.5 );


			Fame = 15000;
			Karma = -15000;


			VirtualArmor = 60;


			Tamable = true;
			ControlSlots = 3;
			MinTameSkill = 93.9;
		}


		public override void OnAfterSpawn()
		{
			bool IsChromatic = false;
			switch ( Utility.RandomMinMax( 0, 20 ) )
			{
				case 0: Hue = MaterialInfo.GetMaterialColor( "jade", "monster", 0 ); IsChromatic = true; break;  // jade
				case 1: Hue = MaterialInfo.GetMaterialColor( "onyx", "monster", 0 ); IsChromatic = true; break;  // onyx
				case 2: Hue = MaterialInfo.GetMaterialColor( "quartz", "monster", 0 ); IsChromatic = true; break;  // quartz
				case 3: Hue = MaterialInfo.GetMaterialColor( "ruby", "monster", 0 ); IsChromatic = true; break;  // ruby
				case 4: Hue = MaterialInfo.GetMaterialColor( "sapphire", "monster", 0 ); IsChromatic = true; break;  // sapphire
				case 5: Hue = MaterialInfo.GetMaterialColor( "spinel", "monster", 0 ); IsChromatic = true; break;  // spinel
				case 6: Hue = MaterialInfo.GetMaterialColor( "topaz", "monster", 0 ); IsChromatic = true; break;  // topaz
				case 7: Hue = MaterialInfo.GetMaterialColor( "amethyst", "monster", 0 ); IsChromatic = true; break;  // amethyst
				case 8: Hue = MaterialInfo.GetMaterialColor( "emerald", "monster", 0 ); IsChromatic = true; break;  // emerald
				case 9: Hue = MaterialInfo.GetMaterialColor( "garnet", "monster", 0 ); IsChromatic = true; break;  // garnet
				case 10: Hue = MaterialInfo.GetMaterialColor( "silver", "monster", 0 ); break;  // silver
				case 11: Hue = MaterialInfo.GetMaterialColor( "star ruby", "monster", 0 ); IsChromatic = true; break; // star ruby
				case 12: Hue = MaterialInfo.GetMaterialColor( "copper", "monster", Hue ); break; // Copper
				case 13: Hue = MaterialInfo.GetMaterialColor( "verite", "monster", Hue ); break; // Verite
				case 14: Hue = MaterialInfo.GetMaterialColor( "valorite", "monster", Hue ); break; // Valorite
				case 15: Hue = MaterialInfo.GetMaterialColor( "agapite", "monster", Hue ); break; // Agapite
				case 16: Hue = MaterialInfo.GetMaterialColor( "bronze", "monster", Hue ); break; // Bronze
				case 17: Hue = MaterialInfo.GetMaterialColor( "dull copper", "monster", Hue ); break; // Dull Copper
				case 18: Hue = MaterialInfo.GetMaterialColor( "gold", "monster", Hue ); break; // Gold
				case 19: Hue = MaterialInfo.GetMaterialColor( "shadow iron", "monster", Hue ); break; // Shadow Iron
				case 20:
					if ( Worlds.IsExploringSeaAreas( this ) == true ){ Hue = MaterialInfo.GetMaterialColor( "nepturite", "monster", 0 ); }
					else if ( Worlds.GetMyWorld( this.Map, this.Location, this.X, this.Y ) == "the Savaged Empire" ){ Hue = MaterialInfo.GetMaterialColor( "steel", "monster", Hue ); }
					else if ( Worlds.GetMyWorld( this.Map, this.Location, this.X, this.Y ) == "the Serpent Island" ){ Hue = MaterialInfo.GetMaterialColor( "obsidian", "monster", Hue ); }
					else if ( Worlds.GetMyWorld( this.Map, this.Location, this.X, this.Y ) == "the Island of Umber Veil" ){ Hue = MaterialInfo.GetMaterialColor( "brass", "monster", Hue ); }
					else if ( Worlds.GetMyWorld( this.Map, this.Location, this.X, this.Y ) == "the Underworld" && this.Map == Map.TerMur ){ Hue = MaterialInfo.GetMaterialColor( "xormite", "monster", Hue ); }
					else if ( Worlds.GetMyWorld( this.Map, this.Location, this.X, this.Y ) == "the Underworld" ){ Hue = MaterialInfo.GetMaterialColor( "copper", "mithril", Hue ); }
					break; // Special
			}


			if ( IsChromatic ){ this.Name = "a chromatic dragon"; }


			base.OnAfterSpawn();
		}


		public override void OnDeath( Container c )
		{
			base.OnDeath( c );


			string metal = "Iron";


			if ( this.Hue == MaterialInfo.GetMaterialColor( "onyx", "monster", 0 ) )
			{
				Item scale = new HardScales( Utility.RandomMinMax( 10, 50 ), "onyx scales" );
				c.DropItem(scale);
				metal = "Onyx";
			}
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "quartz", "monster", 0 ) )
			{
				Item scale = new HardScales( Utility.RandomMinMax( 10, 50 ), "quartz scales" );
				c.DropItem(scale);
				metal = "Quartz";
			}
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "ruby", "monster", 0 ) )
			{
				Item scale = new HardScales( Utility.RandomMinMax( 10, 50 ), "ruby scales" );
				c.DropItem(scale);
				metal = "Ruby";
			}
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "sapphire", "monster", 0 ) )
			{
				Item scale = new HardScales( Utility.RandomMinMax( 10, 50 ), "sapphire scales" );
				c.DropItem(scale);
				metal = "Sapphire";
			}
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "spinel", "monster", 0 ) )
			{
				Item scale = new HardScales( Utility.RandomMinMax( 10, 50 ), "spinel scales" );
				c.DropItem(scale);
				metal = "Spinel";
			}
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "topaz", "monster", 0 ) )
			{
				Item scale = new HardScales( Utility.RandomMinMax( 10, 50 ), "topaz scales" );
				c.DropItem(scale);
				metal = "Topaz";
			}
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "amethyst", "monster", 0 ) )
			{
				Item scale = new HardScales( Utility.RandomMinMax( 10, 50 ), "amethyst scales" );
				c.DropItem(scale);
				metal = "Amethyst";
			}
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "emerald", "monster", 0 ) )
			{
				Item scale = new HardScales( Utility.RandomMinMax( 10, 50 ), "emerald scales" );
				c.DropItem(scale);
				metal = "Emerald";
			}
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "garnet", "monster", 0 ) )
			{
				Item scale = new HardScales( Utility.RandomMinMax( 10, 50 ), "garnet scales" );
				c.DropItem(scale);
				metal = "Garnet";
			}
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "silver", "monster", 0 ) )
			{
				Item scale = new HardScales( Utility.RandomMinMax( 10, 50 ), "silver scales" );
				c.DropItem(scale);
				metal = "Silver";
			}
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "star ruby", "monster", 0 ) )
			{
				Item scale = new HardScales( Utility.RandomMinMax( 10, 50 ), "star ruby scales" );
				c.DropItem(scale);
				metal = "Star Ruby";
			}
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "jade", "monster", 0 ) )
			{
				Item scale = new HardScales( Utility.RandomMinMax( 10, 50 ), "jade scales" );
				c.DropItem(scale);
				metal = "Jade";
			}
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "copper", "monster", Hue ) )
			{
				Item scale = new HardScales( Utility.RandomMinMax( 10, 50 ), "copper scales" );
				c.DropItem(scale);
				metal = "Copper";
			}
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "verite", "monster", Hue ) )
			{
				Item scale = new HardScales( Utility.RandomMinMax( 10, 50 ), "verite scales" );
				c.DropItem(scale);
				metal = "Verite";
			}
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "valorite", "monster", Hue ) )
			{
				Item scale = new HardScales( Utility.RandomMinMax( 10, 50 ), "valorite scales" );
				c.DropItem(scale);
				metal = "Valorite";
			}
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "agapite", "monster", Hue ) )
			{
				Item scale = new HardScales( Utility.RandomMinMax( 10, 50 ), "agapite scales" );
				c.DropItem(scale);
				metal = "Agapite";
			}
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "bronze", "monster", Hue ) )
			{
				Item scale = new HardScales( Utility.RandomMinMax( 10, 50 ), "bronze scales" );
				c.DropItem(scale);
				metal = "Bronze";
			}
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "dull copper", "monster", Hue ) )
			{
				Item scale = new HardScales( Utility.RandomMinMax( 10, 50 ), "dull copper scales" );
				c.DropItem(scale);
				metal = "Dull Copper";
			}
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "gold", "monster", Hue ) )
			{
				Item scale = new HardScales( Utility.RandomMinMax( 10, 50 ), "gold scales" );
				c.DropItem(scale);
				metal = "Golden";
			}
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "shadow iron", "monster", Hue ) )
			{
				Item scale = new HardScales( Utility.RandomMinMax( 10, 50 ), "shadow iron scales" );
				c.DropItem(scale);
				metal = "Shadow Iron";
			}
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "brass", "monster", Hue ) )
			{
				Item scale = new HardScales( Utility.RandomMinMax( 10, 50 ), "brass scales" );
				c.DropItem(scale);
				metal = "Brass";
			}
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "steel", "monster", Hue ) )
			{
				Item scale = new HardScales( Utility.RandomMinMax( 10, 50 ), "steel scales" );
				c.DropItem(scale);
				metal = "Steel";
			}
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "mithril", "monster", Hue ) )
			{
				Item scale = new HardScales( Utility.RandomMinMax( 10, 50 ), "mithril scales" );
				c.DropItem(scale);
				metal = "Mithril";
			}
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "xormite", "monster", Hue ) )
			{
				Item scale = new HardScales( Utility.RandomMinMax( 10, 50 ), "xormite scales" );
				c.DropItem(scale);
				metal = "Xormite";
			}
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "obsidian", "monster", Hue ) )
			{
				Item scale = new HardScales( Utility.RandomMinMax( 10, 50 ), "obsidian scales" );
				c.DropItem(scale);
				metal = "Obsidian";
			}
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "nepturite", "monster", Hue ) )
			{
				Item scale = new HardScales( Utility.RandomMinMax( 10, 50 ), "nepturite scales" );
				c.DropItem(scale);
				metal = "Nepturite";
			}


			Mobile killer = this.LastKiller;
			if ( killer != null )
			{
				if ( killer is BaseCreature )
					killer = ((BaseCreature)killer).GetMaster();


				if ( killer is PlayerMobile )
				{
					Server.Mobiles.Dragons.DropSpecial( this, killer, "", metal, "", c, 25, 0 );
				}
			}
		}


		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 2 );
			AddLoot( LootPack.Gems, 8 );
		}


		public override bool AutoDispel{ get{ return !Controlled; } }
		public override int TreasureMapLevel{ get{ return 4; } }
		public override bool BleedImmune{ get{ return true; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Gold; } }
		public override bool CanAngerOnTame { get { return true; } }


		public MetalDragon( Serial serial ) : base( serial )
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
// using System;// using Server;// using Server.Items;


namespace Server.Mobiles
{
	[CorpseName( "a wyrm corpse" )]
	public class MountainWyrm : BaseCreature
	{
		public override int BreathPhysicalDamage{ get{ return 0; } }
		public override int BreathFireDamage{ get{ return 0; } }
		public override int BreathColdDamage{ get{ return 0; } }
		public override int BreathPoisonDamage{ get{ return 0; } }
		public override int BreathEnergyDamage{ get{ return 100; } }
		public override int BreathEffectHue{ get{ return 0x9C2; } }
		public override int BreathEffectSound{ get{ return 0x665; } }
		public override int BreathEffectItemID{ get{ return 0x3818; } }
		public override bool ReacquireOnMovement{ get{ return !Controlled; } }
		public override bool HasBreath{ get{ return true; } }
		public override void BreathDealDamage( Mobile target, int form ){ base.BreathDealDamage( target, 1 ); }


		[Constructable]
		public MountainWyrm () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = NameList.RandomName( "dragon" );
			Title = "the mountain wyrm";
			BaseSoundID = 362;
			Body = Server.Misc.MyServerSettings.WyrmBody();
			Hue = 0x360;


			SetStr( 721, 760 );
			SetDex( 101, 130 );
			SetInt( 386, 425 );


			SetHits( 433, 456 );


			SetDamage( 17, 25 );


			SetDamageType( ResistanceType.Physical, 75 );
			SetDamageType( ResistanceType.Energy, 25 );


			SetResistance( ResistanceType.Physical, 65, 75 );
			SetResistance( ResistanceType.Energy, 80, 90 );
			SetResistance( ResistanceType.Cold, 70, 80 );
			SetResistance( ResistanceType.Poison, 60, 70 );
			SetResistance( ResistanceType.Fire, 60, 70 );


			SetSkill( SkillName.EvalInt, 99.1, 100.0 );
			SetSkill( SkillName.Magery, 99.1, 100.0 );
			SetSkill( SkillName.MagicResist, 99.1, 100.0 );
			SetSkill( SkillName.Tactics, 97.6, 100.0 );
			SetSkill( SkillName.Wrestling, 90.1, 100.0 );


			Fame = 18000;
			Karma = -18000;


			VirtualArmor = 64;


			Tamable = true;
			ControlSlots = 3;
			MinTameSkill = 96.3;
		}


		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 2 );
			AddLoot( LootPack.Average );
			AddLoot( LootPack.Gems, Utility.Random( 1, 5 ) );
		}


		public override int GetAttackSound(){ return 0x63E; }	// A
		public override int GetDeathSound(){ return 0x63F; }	// D
		public override int GetHurtSound(){ return 0x640; }		// H
		public override int TreasureMapLevel{ get{ return 5; } }
		public override bool AutoDispel{ get{ return !Controlled; } }
		public override int Meat{ get{ return 19; } }
		public override int Hides{ get{ return 20; } }
		public override HideType HideType{ get{ return HideType.Draconic; } }
		public override int Scales{ get{ return 9; } }
		public override ScaleType ScaleType{ get{ return ScaleType.Black; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat | FoodType.Meat; } }
		public override bool CanAngerOnTame { get { return true; } }


		public MountainWyrm( Serial serial ) : base( serial )
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


			Body = Server.Misc.MyServerSettings.WyrmBody();
		}
	}
}
// using System;// using Server;// using Server.Items;


namespace Server.Mobiles
{
	[CorpseName( "a wyrm corpse" )]
	public class NightWyrm : BaseCreature
	{
		public override int BreathPhysicalDamage{ get{ return 20; } }
		public override int BreathFireDamage{ get{ return 20; } }
		public override int BreathColdDamage{ get{ return 20; } }
		public override int BreathPoisonDamage{ get{ return 20; } }
		public override int BreathEnergyDamage{ get{ return 20; } }
		public override int BreathEffectHue{ get{ return 0x496; } }
		public override int BreathEffectSound{ get{ return 0x658; } }
		public override int BreathEffectItemID{ get{ return 0x37BC; } }
		public override bool ReacquireOnMovement{ get{ return !Controlled; } }
		public override bool HasBreath{ get{ return true; } }
		public override void BreathDealDamage( Mobile target, int form ){ base.BreathDealDamage( target, 23 ); }


		[Constructable]
		public NightWyrm () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = NameList.RandomName( "dragon" );
			Title = "the night wyrm";
			BaseSoundID = 362;
			Body = Server.Misc.MyServerSettings.WyrmBody();
			Hue = 0x8FD;


			SetStr( 721, 760 );
			SetDex( 101, 130 );
			SetInt( 386, 425 );


			SetHits( 433, 456 );


			SetDamage( 17, 25 );


			SetDamageType( ResistanceType.Physical, 75 );
			SetDamageType( ResistanceType.Energy, 25 );


			SetResistance( ResistanceType.Physical, 65, 75 );
			SetResistance( ResistanceType.Energy, 80, 90 );
			SetResistance( ResistanceType.Cold, 70, 80 );
			SetResistance( ResistanceType.Poison, 60, 70 );
			SetResistance( ResistanceType.Fire, 60, 70 );


			SetSkill( SkillName.EvalInt, 99.1, 100.0 );
			SetSkill( SkillName.Magery, 99.1, 100.0 );
			SetSkill( SkillName.MagicResist, 99.1, 100.0 );
			SetSkill( SkillName.Tactics, 97.6, 100.0 );
			SetSkill( SkillName.Wrestling, 90.1, 100.0 );


			Fame = 18000;
			Karma = -18000;


			VirtualArmor = 64;


			Tamable = true;
			ControlSlots = 3;
			MinTameSkill = 96.3;
		}


		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 2 );
			AddLoot( LootPack.Average );
			AddLoot( LootPack.Gems, Utility.Random( 1, 5 ) );
		}


		public override int GetAttackSound(){ return 0x63E; }	// A
		public override int GetDeathSound(){ return 0x63F; }	// D
		public override int GetHurtSound(){ return 0x640; }		// H
		public override int TreasureMapLevel{ get{ return 5; } }
		public override bool AutoDispel{ get{ return !Controlled; } }
		public override int Meat{ get{ return 19; } }
		public override int Hides{ get{ return 20; } }
		public override HideType HideType{ get{ return HideType.Draconic; } }
		public override int Scales{ get{ return 9; } }
		public override ScaleType ScaleType{ get{ return ScaleType.Black; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat | FoodType.Meat; } }
		public override bool CanAngerOnTame { get { return true; } }


		public NightWyrm( Serial serial ) : base( serial )
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


			Body = Server.Misc.MyServerSettings.WyrmBody();
		}
	}
}
// using System;// using Server;// using Server.Items;


namespace Server.Mobiles
{
	[CorpseName( "a wyrm corpse" )]
	public class OnyxWyrm : BaseCreature
	{
		public override int BreathPhysicalDamage{ get{ return 20; } }
		public override int BreathFireDamage{ get{ return 20; } }
		public override int BreathColdDamage{ get{ return 20; } }
		public override int BreathPoisonDamage{ get{ return 20; } }
		public override int BreathEnergyDamage{ get{ return 20; } }
		public override int BreathEffectHue{ get{ return 0x496; } }
		public override int BreathEffectSound{ get{ return 0x658; } }
		public override int BreathEffectItemID{ get{ return 0x37BC; } }
		public override bool ReacquireOnMovement{ get{ return !Controlled; } }
		public override bool HasBreath{ get{ return true; } }
		public override void BreathDealDamage( Mobile target, int form ){ base.BreathDealDamage( target, 23 ); }


		[Constructable]
		public OnyxWyrm () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = NameList.RandomName( "dragon" );
			Title = "the onyx wyrm";
			BaseSoundID = 362;
			Body = Server.Misc.MyServerSettings.WyrmBody();
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "onyx", "monster", 0 );


			SetStr( 721, 760 );
			SetDex( 101, 130 );
			SetInt( 386, 425 );


			SetHits( 433, 456 );


			SetDamage( 17, 25 );


			SetDamageType( ResistanceType.Physical, 75 );
			SetDamageType( ResistanceType.Fire, 25 );


			SetResistance( ResistanceType.Physical, 65, 75 );
			SetResistance( ResistanceType.Fire, 80, 90 );
			SetResistance( ResistanceType.Cold, 70, 80 );
			SetResistance( ResistanceType.Poison, 60, 70 );
			SetResistance( ResistanceType.Energy, 60, 70 );


			SetSkill( SkillName.EvalInt, 99.1, 100.0 );
			SetSkill( SkillName.Magery, 99.1, 100.0 );
			SetSkill( SkillName.MagicResist, 99.1, 100.0 );
			SetSkill( SkillName.Tactics, 97.6, 100.0 );
			SetSkill( SkillName.Wrestling, 90.1, 100.0 );


			Fame = 18000;
			Karma = -18000;


			VirtualArmor = 64;


			Tamable = true;
			ControlSlots = 3;
			MinTameSkill = 96.3;
		}


		public override void OnDeath( Container c )
		{
			base.OnDeath( c );


			Item scale = new HardScales( Utility.RandomMinMax( 15, 20 ), "onyx scales" );
   			c.DropItem(scale);
		}


		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 2 );
			AddLoot( LootPack.Average );
			AddLoot( LootPack.Gems, Utility.Random( 1, 5 ) );
		}


		public override int GetAttackSound(){ return 0x63E; }	// A
		public override int GetDeathSound(){ return 0x63F; }	// D
		public override int GetHurtSound(){ return 0x640; }		// H
		public override bool AutoDispel{ get{ return !Controlled; } }
		public override int TreasureMapLevel{ get{ return 5; } }
		public override bool BleedImmune{ get{ return true; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Gold; } }
		public override bool CanAngerOnTame { get { return true; } }


		public OnyxWyrm( Serial serial ) : base( serial )
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


			Body = Server.Misc.MyServerSettings.WyrmBody();
		}
	}
}
// using System;// using Server;// using Server.Items;


namespace Server.Mobiles
{
	[CorpseName( "a beetle's corpse" )]
	public class PoisonBeetle : BaseCreature
	{
		[Constructable]
		public PoisonBeetle () : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a poisonous beetle";
			Body = 0xA9;
			BaseSoundID = 0x388;
			Hue = 1167;


			SetStr( 96, 120 );
			SetDex( 86, 105 );
			SetInt( 6, 10 );


			SetHits( 80, 110 );


			SetDamage( 3, 10 );


			SetDamageType( ResistanceType.Physical, 20 );
			SetDamageType( ResistanceType.Poison, 80 );


			SetResistance( ResistanceType.Physical, 40, 50 );
			SetResistance( ResistanceType.Fire, 20, 30 );
			SetResistance( ResistanceType.Cold, 20, 30 );
			SetResistance( ResistanceType.Poison, 90, 100 );
			SetResistance( ResistanceType.Energy, 20, 30 );


			SetSkill( SkillName.Tactics, 55.1, 70.0 );
			SetSkill( SkillName.Wrestling, 60.1, 75.0 );


			Fame = 3000;
			Karma = -3000;


			VirtualArmor = 16;


			Item Venom = new VenomSack();
				Venom.Name = "lethal venom sack";
				AddItem( Venom );
		}


		public override void GenerateLoot()
		{
			AddLoot( LootPack.Meager );
		}


		public override Poison HitPoison{ get{ return Poison.Lethal; } }
		public override double HitPoisonChance{ get{ return 0.6; } }
		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }


		public PoisonBeetle( Serial serial ) : base( serial )
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


			if ( BaseSoundID == 263 )
				BaseSoundID = 1170;


			Body = 0xA9;
		}
	}
}
// using System;// using Server;// using Server.Items;


namespace Server.Mobiles
{
	[CorpseName( "a wyrm corpse" )]
	public class QuartzWyrm : BaseCreature
	{
		public override bool ReacquireOnMovement{ get{ return !Controlled; } }
		public override bool HasBreath{ get{ return true; } }
		public override void BreathDealDamage( Mobile target, int form ){ base.BreathDealDamage( target, 9 ); }


		[Constructable]
		public QuartzWyrm () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = NameList.RandomName( "dragon" );
			Title = "the quartz wyrm";
			BaseSoundID = 362;
			Body = Server.Misc.MyServerSettings.WyrmBody();
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "quartz", "monster", 0 );


			SetStr( 721, 760 );
			SetDex( 101, 130 );
			SetInt( 386, 425 );


			SetHits( 433, 456 );


			SetDamage( 17, 25 );


			SetDamageType( ResistanceType.Physical, 75 );
			SetDamageType( ResistanceType.Fire, 25 );


			SetResistance( ResistanceType.Physical, 65, 75 );
			SetResistance( ResistanceType.Fire, 80, 90 );
			SetResistance( ResistanceType.Cold, 70, 80 );
			SetResistance( ResistanceType.Poison, 60, 70 );
			SetResistance( ResistanceType.Energy, 60, 70 );


			SetSkill( SkillName.EvalInt, 99.1, 100.0 );
			SetSkill( SkillName.Magery, 99.1, 100.0 );
			SetSkill( SkillName.MagicResist, 99.1, 100.0 );
			SetSkill( SkillName.Tactics, 97.6, 100.0 );
			SetSkill( SkillName.Wrestling, 90.1, 100.0 );


			Fame = 18000;
			Karma = -18000;


			VirtualArmor = 64;


			Tamable = true;
			ControlSlots = 3;
			MinTameSkill = 96.3;
		}


		public override void OnDeath( Container c )
		{
			base.OnDeath( c );


			Item scale = new HardScales( Utility.RandomMinMax( 15, 20 ), "quartz scales" );
   			c.DropItem(scale);
		}


		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 2 );
			AddLoot( LootPack.Average );
			AddLoot( LootPack.Gems, Utility.Random( 1, 5 ) );
		}


		public override int GetAttackSound(){ return 0x63E; }	// A
		public override int GetDeathSound(){ return 0x63F; }	// D
		public override int GetHurtSound(){ return 0x640; }		// H
		public override bool AutoDispel{ get{ return !Controlled; } }
		public override int TreasureMapLevel{ get{ return 5; } }
		public override bool BleedImmune{ get{ return true; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Gold; } }
		public override bool CanAngerOnTame { get { return true; } }


		public QuartzWyrm( Serial serial ) : base( serial )
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


			Body = Server.Misc.MyServerSettings.WyrmBody();
		}
	}
}
// using System;// using Server.Items;// using Server.Mobiles;// using Server.Misc;



namespace Server.Mobiles
{
	[CorpseName( "a ravenous corpse" )]
	public class Ravenous : BaseCreature
	{
		public override WeaponAbility GetWeaponAbility()
		{
			return WeaponAbility.BleedAttack;
		}


		[Constructable]
		public Ravenous() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a ravenous";
			Body = 218;
			BaseSoundID = 0x5A;
			Hue = 0x84E;


			SetStr( 166, 190 );
			SetDex( 96, 115 );
			SetInt( 51, 60 );


			SetHits( 116, 130 );
			SetMana( 0 );


			SetDamage( 12, 30 );


			SetDamageType( ResistanceType.Physical, 100 );


			SetResistance( ResistanceType.Physical, 35, 45 );
			SetResistance( ResistanceType.Fire, 30, 45 );
			SetResistance( ResistanceType.Poison, 25, 35 );
			SetResistance( ResistanceType.Energy, 25, 35 );


			SetSkill( SkillName.MagicResist, 55.1, 70.0 );
			SetSkill( SkillName.Tactics, 60.1, 80.0 );
			SetSkill( SkillName.Wrestling, 60.1, 80.0 );


			Fame = 3500;
			Karma = -3500;


			VirtualArmor = 40;


			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 90.7;
		}


		public override int GetAttackSound(){ return 0x622; }	// A
		public override int GetDeathSound(){ return 0x623; }	// D
		public override int GetHurtSound(){ return 0x624; }		// H


		public override HideType HideType{ get{ return HideType.Dinosaur; } }
		public override int Meat{ get{ return 4; } }
		public override int Hides{ get{ return 12; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Fish | FoodType.Meat; } }
		public override int Scales{ get{ return 2; } }
		public override ScaleType ScaleType{ get{ return ( ScaleType.Dinosaur ); } }


		public Ravenous(Serial serial) : base(serial)
		{
		}


		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);


			writer.Write((int) 0);
		}


		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);


			int version = reader.ReadInt();
		}
	}
}
// using System;// using System.Collections;// using Server;// using Server.Items;


namespace Server.Mobiles
{
	[CorpseName( "a wyrm corpse" )]
	public class RubyWyrm : BaseCreature
	{
		public override bool ReacquireOnMovement{ get{ return !Controlled; } }
		public override bool HasBreath{ get{ return true; } }
		public override void BreathDealDamage( Mobile target, int form ){ base.BreathDealDamage( target, 9 ); }


		[Constructable]
		public RubyWyrm () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = NameList.RandomName( "dragon" );
			Title = "the ruby wyrm";
			BaseSoundID = 362;
			Body = Server.Misc.MyServerSettings.WyrmBody();
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "ruby", "monster", 0 );


			SetStr( 721, 760 );
			SetDex( 101, 130 );
			SetInt( 386, 425 );


			SetHits( 433, 456 );


			SetDamage( 17, 25 );


			SetDamageType( ResistanceType.Physical, 75 );
			SetDamageType( ResistanceType.Fire, 25 );


			SetResistance( ResistanceType.Physical, 55, 70 );
			SetResistance( ResistanceType.Cold, 40, 50 );
			SetResistance( ResistanceType.Fire, 80, 90 );
			SetResistance( ResistanceType.Poison, 40, 50 );
			SetResistance( ResistanceType.Energy, 40, 50 );


			SetSkill( SkillName.EvalInt, 99.1, 100.0 );
			SetSkill( SkillName.Magery, 99.1, 100.0 );
			SetSkill( SkillName.MagicResist, 99.1, 100.0 );
			SetSkill( SkillName.Tactics, 97.6, 100.0 );
			SetSkill( SkillName.Wrestling, 90.1, 100.0 );


			Fame = 18000;
			Karma = -18000;


			VirtualArmor = 64;


			Tamable = true;
			ControlSlots = 3;
			MinTameSkill = 96.3;
		}


		public override void OnDeath( Container c )
		{
			base.OnDeath( c );


			Item scale = new HardScales( Utility.RandomMinMax( 15, 20 ), "ruby scales" );
   			c.DropItem(scale);
		}


		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 2 );
			AddLoot( LootPack.Average );
			AddLoot( LootPack.Gems, Utility.Random( 1, 5 ) );
		}


		public override int GetAttackSound(){ return 0x63E; }	// A
		public override int GetDeathSound(){ return 0x63F; }	// D
		public override int GetHurtSound(){ return 0x640; }		// H
		public override bool AutoDispel{ get{ return !Controlled; } }
		public override int TreasureMapLevel{ get{ return 5; } }
		public override bool BleedImmune{ get{ return true; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Gold; } }
		public override bool CanAngerOnTame { get { return true; } }


		public RubyWyrm( Serial serial ) : base( serial )
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


			Body = Server.Misc.MyServerSettings.WyrmBody();
		}
	}
}
// using System;// using Server.Mobiles;// using Server.Items;


namespace Server.Mobiles
{
	[CorpseName( "a bear corpse" )]
	public class SabretoothBear : BaseCreature
	{
		public override WeaponAbility GetWeaponAbility()
		{
			return WeaponAbility.BleedAttack;
		}


		[Constructable]
		public SabretoothBear() : base( AIType.AI_Melee,FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a sabreclaw bear";
			Body = 34;
			BaseSoundID = 0xA3;


			SetStr( 226, 255 );
			SetDex( 121, 145 );
			SetInt( 16, 40 );


			SetHits( 176, 193 );
			SetMana( 0 );


			SetDamage( 14, 19 );


			SetDamageType( ResistanceType.Physical, 100 );


			SetResistance( ResistanceType.Physical, 45, 55 );
			SetResistance( ResistanceType.Cold, 35, 45 );
			SetResistance( ResistanceType.Poison, 15, 20 );
			SetResistance( ResistanceType.Energy, 15, 20 );


			SetSkill( SkillName.MagicResist, 35.1, 50.0 );
			SetSkill( SkillName.Tactics, 90.1, 120.0 );
			SetSkill( SkillName.Wrestling, 65.1, 90.0 );


			Fame = 1500;
			Karma = 0;


			VirtualArmor = 35;


			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 69.1;
		}


		public override int Meat{ get{ return 2; } }
		public override int Hides{ get{ return 16; } }
		public override int Furs{ get{ return Utility.RandomList( 0, 0, 0, 8 ); } }
		public override FurType FurType{ get{ return FurType.Regular; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Fish | FoodType.FruitsAndVegies | FoodType.Meat; } }
		public override PackInstinct PackInstinct{ get{ return PackInstinct.Bear; } }


		public override int GetAngerSound()
		{
			return 0x518;
		}


		public override int GetIdleSound()
		{
			return 0x517;
		}


		public override int GetAttackSound()
		{
			return 0x516;
		}


		public override int GetHurtSound()
		{
			return 0x519;
		}


		public override int GetDeathSound()
		{
			return 0x515;
		}


		public SabretoothBear( Serial serial ) : base( serial )
		{
		}


		public override void Serialize(GenericWriter writer)
		{
			base.Serialize( writer );


			writer.Write( (int) 0 );
		}


		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );


			int version = reader.ReadInt();
		}
	}
}
// using System;// using Server;// using Server.Gumps;// using Server.Mobiles;// using Server.Targeting;// using Server.Network;


namespace Server.Mobiles
{
	[CorpseName( "a wyrm corpse" )]
	public class SapphireWyrm : BaseCreature
	{
		public override int BreathPhysicalDamage{ get{ return 0; } }
		public override int BreathFireDamage{ get{ return 0; } }
		public override int BreathColdDamage{ get{ return 100; } }
		public override int BreathPoisonDamage{ get{ return 0; } }
		public override int BreathEnergyDamage{ get{ return 0; } }
		public override int BreathEffectHue{ get{ return 0x481; } }
		public override int BreathEffectSound{ get{ return 0x64F; } }
		public override bool ReacquireOnMovement{ get{ return !Controlled; } }
		public override bool HasBreath{ get{ return true; } }
		public override void BreathDealDamage( Mobile target, int form ){ base.BreathDealDamage( target, 12 ); }


		[Constructable]
		public SapphireWyrm () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = NameList.RandomName( "dragon" );
			Title = "the sapphire wyrm";
			BaseSoundID = 362;
			Body = Server.Misc.MyServerSettings.WyrmBody();
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "sapphire", "monster", 0 );


			SetStr( 721, 760 );
			SetDex( 101, 130 );
			SetInt( 386, 425 );


			SetHits( 433, 456 );


			SetDamage( 17, 25 );


			SetDamageType( ResistanceType.Physical, 75 );
			SetDamageType( ResistanceType.Cold, 25 );


			SetResistance( ResistanceType.Physical, 55, 70 );
			SetResistance( ResistanceType.Fire, 15, 25 );
			SetResistance( ResistanceType.Cold, 80, 90 );
			SetResistance( ResistanceType.Poison, 40, 50 );
			SetResistance( ResistanceType.Energy, 40, 50 );


			SetSkill( SkillName.EvalInt, 99.1, 100.0 );
			SetSkill( SkillName.Magery, 99.1, 100.0 );
			SetSkill( SkillName.MagicResist, 99.1, 100.0 );
			SetSkill( SkillName.Tactics, 97.6, 100.0 );
			SetSkill( SkillName.Wrestling, 90.1, 100.0 );


			Fame = 18000;
			Karma = -18000;


			VirtualArmor = 64;


			Tamable = true;
			ControlSlots = 3;
			MinTameSkill = 96.3;
		}


		public override void OnDeath( Container c )
		{
			base.OnDeath( c );


			Item scale = new HardScales( Utility.RandomMinMax( 15, 20 ), "sapphire scales" );
   			c.DropItem(scale);
		}


		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 2 );
			AddLoot( LootPack.Average );
			AddLoot( LootPack.Gems, Utility.Random( 1, 5 ) );
		}


		public override int GetAttackSound(){ return 0x63E; }	// A
		public override int GetDeathSound(){ return 0x63F; }	// D
		public override int GetHurtSound(){ return 0x640; }		// H
		public override bool AutoDispel{ get{ return !Controlled; } }
		public override int TreasureMapLevel{ get{ return 5; } }
		public override bool BleedImmune{ get{ return true; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Gold; } }
		public override bool CanAngerOnTame { get { return true; } }


		public SapphireWyrm( Serial serial ) : base( serial )
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


			Body = Server.Misc.MyServerSettings.WyrmBody();
		}
	}
}
// using System;// using System.Collections;// using Server;// using Server.Items;


namespace Server.Mobiles
{
	[CorpseName( "a wyrm corpse" )]
	public class SpinelWyrm : BaseCreature
	{
		public override int BreathPhysicalDamage{ get{ return 0; } }
		public override int BreathFireDamage{ get{ return 0; } }
		public override int BreathColdDamage{ get{ return 0; } }
		public override int BreathPoisonDamage{ get{ return 0; } }
		public override int BreathEnergyDamage{ get{ return 100; } }
		public override int BreathEffectHue{ get{ return 0x9C2; } }
		public override int BreathEffectSound{ get{ return 0x665; } }
		public override int BreathEffectItemID{ get{ return 0x3818; } }
		public override bool ReacquireOnMovement{ get{ return !Controlled; } }
		public override bool HasBreath{ get{ return true; } }
		public override void BreathDealDamage( Mobile target, int form ){ base.BreathDealDamage( target, 1 ); }


		[Constructable]
		public SpinelWyrm () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = NameList.RandomName( "dragon" );
			Title = "the spinel wyrm";
			BaseSoundID = 362;
			Body = Server.Misc.MyServerSettings.WyrmBody();
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "spinel", "monster", 0 );


			SetStr( 721, 760 );
			SetDex( 101, 130 );
			SetInt( 386, 425 );


			SetHits( 433, 456 );


			SetDamage( 17, 25 );


			SetDamageType( ResistanceType.Physical, 75 );
			SetDamageType( ResistanceType.Energy, 25 );


			SetResistance( ResistanceType.Physical, 55, 70 );
			SetResistance( ResistanceType.Fire, 40, 50 );
			SetResistance( ResistanceType.Energy, 80, 90 );
			SetResistance( ResistanceType.Poison, 40, 50 );
			SetResistance( ResistanceType.Cold, 40, 50 );


			SetSkill( SkillName.EvalInt, 99.1, 100.0 );
			SetSkill( SkillName.Magery, 99.1, 100.0 );
			SetSkill( SkillName.MagicResist, 99.1, 100.0 );
			SetSkill( SkillName.Tactics, 97.6, 100.0 );
			SetSkill( SkillName.Wrestling, 90.1, 100.0 );


			Fame = 18000;
			Karma = -18000;


			VirtualArmor = 64;


			Tamable = true;
			ControlSlots = 3;
			MinTameSkill = 96.3;
		}


		public override void OnDeath( Container c )
		{
			base.OnDeath( c );


			Item scale = new HardScales( Utility.RandomMinMax( 15, 20 ), "spinel scales" );
   			c.DropItem(scale);
		}


		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 2 );
			AddLoot( LootPack.Average );
			AddLoot( LootPack.Gems, Utility.Random( 1, 5 ) );
		}


		public override int GetAttackSound(){ return 0x63E; }	// A
		public override int GetDeathSound(){ return 0x63F; }	// D
		public override int GetHurtSound(){ return 0x640; }		// H
		public override bool AutoDispel{ get{ return !Controlled; } }
		public override int TreasureMapLevel{ get{ return 5; } }
		public override bool BleedImmune{ get{ return true; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Gold; } }
		public override bool CanAngerOnTame { get { return true; } }


		public SpinelWyrm( Serial serial ) : base( serial )
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


			Body = Server.Misc.MyServerSettings.WyrmBody();
		}
	}
}
// using System;// using Server;


namespace Server.Mobiles
{
	[CorpseName( "a pile of stones" )]
	public class StoneDragon : BaseCreature
	{
		public override bool ReacquireOnMovement{ get{ return !Controlled; } }
		public override bool HasBreath{ get{ return true; } }
		public override void BreathDealDamage( Mobile target, int form ){ base.BreathDealDamage( target, 9 ); }


		[Constructable]
		public StoneDragon () : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a stone dragon";
			Body = 12;
			Hue = 2500;
			BaseSoundID = 268;


			SetStr( 796, 825 );
			SetDex( 86, 105 );
			SetInt( 436, 475 );


			SetHits( 478, 495 );


			SetDamage( 16, 22 );


			SetDamageType( ResistanceType.Physical, 100 );


			SetResistance( ResistanceType.Physical, 55, 65 );
			SetResistance( ResistanceType.Fire, 60, 70 );
			SetResistance( ResistanceType.Cold, 30, 40 );
			SetResistance( ResistanceType.Poison, 75, 85 );
			SetResistance( ResistanceType.Energy, 15, 20 );


			SetSkill( SkillName.Tactics, 97.6, 100.0 );
			SetSkill( SkillName.Wrestling, 90.1, 92.5 );


			Fame = 15000;
			Karma = -15000;


			VirtualArmor = 60;
		}


		public override void OnDeath( Container c )
		{
			base.OnDeath( c );


			Item scale = new HardScales( Utility.RandomMinMax( 15, 20 ), "marble scales" );
   			c.DropItem(scale);


			Mobile killer = this.LastKiller;
			if ( killer != null )
			{
				if ( killer is BaseCreature )
					killer = ((BaseCreature)killer).GetMaster();


				if ( killer is PlayerMobile )
				{
					Server.Mobiles.Dragons.DropSpecial( this, killer, "", "Stone", "", c, 25, 0 );
				}
			}
		}


		public override void CheckReflect( Mobile caster, ref bool reflect )
		{
			reflect = true; // Every spell is reflected back to the caster
		}


		public override bool OnBeforeDeath()
		{
			this.Body = 0x33D;
			return base.OnBeforeDeath();
		}


		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 2 );
			AddLoot( LootPack.Gems, 8 );
		}


		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }
		public override bool IsScaredOfScaryThings{ get{ return false; } }
		public override bool IsScaryToPets{ get{ return true; } }


		public StoneDragon( Serial serial ) : base( serial )
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
// using System;// using Server;// using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a beetle's corpse" )]
	public class TigerBeetle : BaseCreature
	{
		public override WeaponAbility GetWeaponAbility()
		{
			return WeaponAbility.BleedAttack;
		}


		[Constructable]
		public TigerBeetle () : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a tiger beetle";
			Body = 0xA9;
			BaseSoundID = 0x388;


			SetStr( 96, 120 );
			SetDex( 86, 105 );
			SetInt( 6, 10 );


			SetHits( 80, 110 );


			SetDamage( 3, 10 );


			SetDamageType( ResistanceType.Physical, 20 );
			SetDamageType( ResistanceType.Fire, 80 );


			SetResistance( ResistanceType.Physical, 40, 50 );
			SetResistance( ResistanceType.Fire, 80, 90 );
			SetResistance( ResistanceType.Cold, 20, 30 );
			SetResistance( ResistanceType.Poison, 20, 30 );
			SetResistance( ResistanceType.Energy, 20, 30 );


			SetSkill( SkillName.Tactics, 55.1, 70.0 );
			SetSkill( SkillName.Wrestling, 60.1, 75.0 );


			Fame = 3000;
			Karma = -3000;


			VirtualArmor = 16;
		}


		public override void GenerateLoot()
		{
			AddLoot( LootPack.Meager );
		}


		public TigerBeetle( Serial serial ) : base( serial )
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


			if ( BaseSoundID == 263 )
				BaseSoundID = 1170;


			Body = 0xA9;
		}
	}
}
// using System;// using Server;// using Server.Items;


namespace Server.Mobiles
{
	[CorpseName( "a wyrm corpse" )]
	public class TopazWyrm : BaseCreature
	{
		public override int BreathPhysicalDamage{ get{ return 0; } }
		public override int BreathFireDamage{ get{ return 0; } }
		public override int BreathColdDamage{ get{ return 100; } }
		public override int BreathPoisonDamage{ get{ return 0; } }
		public override int BreathEnergyDamage{ get{ return 0; } }
		public override int BreathEffectHue{ get{ return 0x481; } }
		public override int BreathEffectSound{ get{ return 0x64F; } }
		public override bool ReacquireOnMovement{ get{ return !Controlled; } }
		public override bool HasBreath{ get{ return true; } }
		public override void BreathDealDamage( Mobile target, int form ){ base.BreathDealDamage( target, 12 ); }


		[Constructable]
		public TopazWyrm () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = NameList.RandomName( "dragon" );
			Title = "the topaz wyrm";
			BaseSoundID = 362;
			Body = Server.Misc.MyServerSettings.WyrmBody();
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "topaz", "monster", 0 );


			SetStr( 721, 760 );
			SetDex( 101, 130 );
			SetInt( 386, 425 );


			SetHits( 433, 456 );


			SetDamage( 17, 25 );


			SetDamageType( ResistanceType.Physical, 75 );
			SetDamageType( ResistanceType.Cold, 25 );


			SetResistance( ResistanceType.Physical, 55, 70 );
			SetResistance( ResistanceType.Fire, 15, 25 );
			SetResistance( ResistanceType.Cold, 80, 90 );
			SetResistance( ResistanceType.Poison, 40, 50 );
			SetResistance( ResistanceType.Energy, 40, 50 );


			SetSkill( SkillName.EvalInt, 99.1, 100.0 );
			SetSkill( SkillName.Magery, 99.1, 100.0 );
			SetSkill( SkillName.MagicResist, 99.1, 100.0 );
			SetSkill( SkillName.Tactics, 97.6, 100.0 );
			SetSkill( SkillName.Wrestling, 90.1, 100.0 );


			Fame = 18000;
			Karma = -18000;


			VirtualArmor = 64;


			Tamable = true;
			ControlSlots = 3;
			MinTameSkill = 96.3;
		}


		public override void OnDeath( Container c )
		{
			base.OnDeath( c );


			Item scale = new HardScales( Utility.RandomMinMax( 15, 20 ), "topaz scales" );
   			c.DropItem(scale);
		}


		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 2 );
			AddLoot( LootPack.Average );
			AddLoot( LootPack.Gems, Utility.Random( 1, 5 ) );
		}


		public override int GetAttackSound(){ return 0x63E; }	// A
		public override int GetDeathSound(){ return 0x63F; }	// D
		public override int GetHurtSound(){ return 0x640; }		// H
		public override bool AutoDispel{ get{ return !Controlled; } }
		public override int TreasureMapLevel{ get{ return 5; } }
		public override bool BleedImmune{ get{ return true; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Gold; } }
		public override bool CanAngerOnTame { get { return true; } }


		public TopazWyrm( Serial serial ) : base( serial )
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


			Body = Server.Misc.MyServerSettings.WyrmBody();
		}
	}
}

// using System;// using Server;// using Server.Items;// using Server.Engines.Plants;


namespace Server.Mobiles
{
	[CorpseName( "a swamp drake corpse" )]
	public class SwampDrake : BaseCreature
	{
		public override int BreathPhysicalDamage{ get{ return 0; } }
		public override int BreathFireDamage{ get{ return 0; } }
		public override int BreathColdDamage{ get{ return 0; } }
		public override int BreathPoisonDamage{ get{ return 100; } }
		public override int BreathEnergyDamage{ get{ return 0; } }
		public override int BreathEffectHue{ get{ return 0x3F; } }
		public override int BreathEffectSound{ get{ return 0x658; } }
		public override bool ReacquireOnMovement{ get{ return !Controlled; } }
		public override bool HasBreath{ get{ return true; } }
		public override double BreathEffectDelay{ get{ return 0.1; } }
		public override void BreathDealDamage( Mobile target, int form ){ base.BreathDealDamage( target, 18 ); }


		[Constructable]
		public SwampDrake () : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a swamp drake";
			Body = 55;
			BaseSoundID = 362;


			SetStr( 401, 430 );
			SetDex( 133, 152 );
			SetInt( 101, 140 );


			SetHits( 241, 258 );


			SetDamage( 11, 17 );


			SetDamageType( ResistanceType.Physical, 80 );
			SetDamageType( ResistanceType.Poison, 20 );


			SetResistance( ResistanceType.Physical, 45, 50 );
			SetResistance( ResistanceType.Poison, 50, 60 );
			SetResistance( ResistanceType.Cold, 40, 50 );
			SetResistance( ResistanceType.Fire, 20, 30 );
			SetResistance( ResistanceType.Energy, 30, 40 );


			SetSkill( SkillName.MagicResist, 65.1, 80.0 );
			SetSkill( SkillName.Tactics, 65.1, 90.0 );
			SetSkill( SkillName.Wrestling, 65.1, 80.0 );


			Fame = 5500;
			Karma = -5500;


			VirtualArmor = 46;


			Tamable = true;
			ControlSlots = 2;
			MinTameSkill = 84.3;


			PackReg( 3 );


			if ( Utility.Random( 100 ) > 60 )
			{
				int seed_to_give = Utility.Random( 100 );


				if ( seed_to_give > 90 )
				{
					Seed reward;


					PlantType type;
					switch ( Utility.Random( 17 ) )
					{
						case 0: type = PlantType.CampionFlowers; break;
						case 1: type = PlantType.Poppies; break;
						case 2: type = PlantType.Snowdrops; break;
						case 3: type = PlantType.Bulrushes; break;
						case 4: type = PlantType.Lilies; break;
						case 5: type = PlantType.PampasGrass; break;
						case 6: type = PlantType.Rushes; break;
						case 7: type = PlantType.ElephantEarPlant; break;
						case 8: type = PlantType.Fern; break;
						case 9: type = PlantType.PonytailPalm; break;
						case 10: type = PlantType.SmallPalm; break;
						case 11: type = PlantType.CenturyPlant; break;
						case 12: type = PlantType.WaterPlant; break;
						case 13: type = PlantType.SnakePlant; break;
						case 14: type = PlantType.PricklyPearCactus; break;
						case 15: type = PlantType.BarrelCactus; break;
						default: type = PlantType.TribarrelCactus; break;
					}
						PlantHue hue;
						switch ( Utility.Random( 4 ) )
						{
							case 0: hue = PlantHue.Pink; break;
							case 1: hue = PlantHue.Magenta; break;
							case 2: hue = PlantHue.FireRed; break;
							default: hue = PlantHue.Aqua; break;
						}


						PackItem( new Seed( type, hue, false ) );
				}
				else if ( seed_to_give > 70 )
				{
					PackItem( Engines.Plants.Seed.RandomPeculiarSeed( Utility.RandomMinMax( 1, 4 ) ) );
				}
				else if ( seed_to_give > 40 )
				{
					PackItem( Engines.Plants.Seed.RandomBonsaiSeed() );
				}
				else
				{
					PackItem( new Engines.Plants.Seed() );
				}
			}
		}


		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.MedScrolls, 2 );
		}


		public override int TreasureMapLevel{ get{ return 2; } }
		public override int Meat{ get{ return 10; } }
		public override int Hides{ get{ return 20; } }
		public override HideType HideType{ get{ return HideType.Draconic; } }
		public override int Scales{ get{ return 2; } }
		public override ScaleType ScaleType{ get{ return ( ScaleType.Green ); } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat | FoodType.Fish; } }


		public SwampDrake( Serial serial ) : base( serial )
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

// using System;// using Server;// using Server.Items;


namespace Server.Mobiles
{
	[CorpseName( "a water beetle corpse" )]
	public class WaterBeetle : BaseCreature
	{
		[Constructable]
		public WaterBeetle() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a water beetle";
			Body = 0xA9;
			Hue = 1365;
			SetStr( 96, 120 );
			SetDex( 86, 105 );
			SetInt( 6, 10 );


			CanSwim = true;


			SetHits( 80, 110 );


			SetDamage( 3, 10 );


			SetDamageType( ResistanceType.Physical, 100 );


			SetResistance( ResistanceType.Physical, 40, 50 );
			SetResistance( ResistanceType.Fire, 20, 30 );
			SetResistance( ResistanceType.Cold, 20, 30 );
			SetResistance( ResistanceType.Poison, 20, 30 );
			SetResistance( ResistanceType.Energy, 20, 30 );


			SetSkill( SkillName.Tactics, 55.1, 70.0 );
			SetSkill( SkillName.Wrestling, 60.1, 75.0 );


			Fame = 3000;
			Karma = -3000;


			VirtualArmor = 16;
		}


		public override bool BleedImmune{ get{ return true; } }


		public override int GetAngerSound()
		{
			return 0x21D;
		}


		public override int GetIdleSound()
		{
			return 0x21D;
		}


		public override int GetAttackSound()
		{
			return 0x162;
		}


		public override int GetHurtSound()
		{
			return 0x163;
		}


		public override int GetDeathSound()
		{
			return 0x21D;
		}


		public WaterBeetle( Serial serial ) : base( serial )
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


			Body = 0xA9;
		}
	}
}
// using System;// using Server;// using Server.Items;// using Server.Misc;


namespace Server.Mobiles
{
	[CorpseName( "a dragon corpse" )]
	public class WhiteDragon : BaseCreature
	{
		public override int BreathPhysicalDamage{ get{ return 0; } }
		public override int BreathFireDamage{ get{ return 0; } }
		public override int BreathColdDamage{ get{ return 100; } }
		public override int BreathPoisonDamage{ get{ return 0; } }
		public override int BreathEnergyDamage{ get{ return 0; } }
		public override int BreathEffectHue{ get{ return 0x481; } }
		public override int BreathEffectSound{ get{ return 0x64F; } }
		public override bool ReacquireOnMovement{ get{ return !Controlled; } }
		public override bool HasBreath{ get{ return true; } }
		public override void BreathDealDamage( Mobile target, int form ){ base.BreathDealDamage( target, 12 ); }


		[Constructable]
		public WhiteDragon () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a white dragon";
			Body = 12;
			Hue = 0x9C2;
			BaseSoundID = 362;


			SetStr( 796, 825 );
			SetDex( 86, 105 );
			SetInt( 436, 475 );


			SetHits( 478, 495 );


			SetDamage( 16, 22 );


			SetDamageType( ResistanceType.Physical, 75 );
			SetDamageType( ResistanceType.Cold, 25 );


			SetResistance( ResistanceType.Physical, 55, 65 );
			SetResistance( ResistanceType.Cold, 60, 70 );
			SetResistance( ResistanceType.Fire, 20, 30 );
			SetResistance( ResistanceType.Poison, 25, 35 );
			SetResistance( ResistanceType.Energy, 35, 45 );


			SetSkill( SkillName.EvalInt, 30.1, 40.0 );
			SetSkill( SkillName.Magery, 30.1, 40.0 );
			SetSkill( SkillName.MagicResist, 99.1, 100.0 );
			SetSkill( SkillName.Tactics, 97.6, 100.0 );
			SetSkill( SkillName.Wrestling, 90.1, 92.5 );


			Fame = 15000;
			Karma = -15000;


			VirtualArmor = 60;


			Tamable = true;
			ControlSlots = 3;
			MinTameSkill = 93.9;
		}


		public override void OnDeath( Container c )
		{
			base.OnDeath( c );


			Mobile killer = this.LastKiller;
			if ( killer != null )
			{
				if ( killer is BaseCreature )
					killer = ((BaseCreature)killer).GetMaster();


				if ( killer is PlayerMobile )
				{
					Server.Mobiles.Dragons.DropSpecial( this, killer, "", "White", "", c, 25, 0 );
				}
			}
		}


		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 2 );
			AddLoot( LootPack.Gems, 8 );
		}


		public override bool AutoDispel{ get{ return !Controlled; } }
		public override int TreasureMapLevel{ get{ return 4; } }
		public override int Meat{ get{ return 19; } }
		public override int Hides{ get{ return 20; } }
		public override HideType HideType{ get{ return HideType.Draconic; } }
		public override int Scales{ get{ return 7; } }
		public override ScaleType ScaleType{ get{ return ( ScaleType.White ); } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat; } }
		public override bool CanAngerOnTame { get { return true; } }


		public WhiteDragon( Serial serial ) : base( serial )
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
// using System;// using Server;// using Server.Items;


namespace Server.Mobiles
{
	[CorpseName( "a wyrm corpse" )]
	public class WhiteWyrm : BaseCreature
	{
		public override int BreathPhysicalDamage{ get{ return 0; } }
		public override int BreathFireDamage{ get{ return 0; } }
		public override int BreathColdDamage{ get{ return 100; } }
		public override int BreathPoisonDamage{ get{ return 0; } }
		public override int BreathEnergyDamage{ get{ return 0; } }
		public override int BreathEffectHue{ get{ return 0x481; } }
		public override int BreathEffectSound{ get{ return 0x64F; } }
		public override bool ReacquireOnMovement{ get{ return !Controlled; } }
		public override bool HasBreath{ get{ return true; } }
		public override void BreathDealDamage( Mobile target, int form ){ base.BreathDealDamage( target, 12 ); }


		[Constructable]
		public WhiteWyrm () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = NameList.RandomName( "dragon" );
			Title = "the white wyrm";
			BaseSoundID = 362;
			Hue = 0x9C2;
			Body = Server.Misc.MyServerSettings.WyrmBody();


			SetStr( 721, 760 );
			SetDex( 101, 130 );
			SetInt( 386, 425 );


			SetHits( 433, 456 );


			SetDamage( 17, 25 );


			SetDamageType( ResistanceType.Physical, 75 );
			SetDamageType( ResistanceType.Cold, 25 );


			SetResistance( ResistanceType.Physical, 55, 70 );
			SetResistance( ResistanceType.Fire, 15, 25 );
			SetResistance( ResistanceType.Cold, 80, 90 );
			SetResistance( ResistanceType.Poison, 40, 50 );
			SetResistance( ResistanceType.Energy, 40, 50 );


			SetSkill( SkillName.EvalInt, 99.1, 100.0 );
			SetSkill( SkillName.Magery, 99.1, 100.0 );
			SetSkill( SkillName.MagicResist, 99.1, 100.0 );
			SetSkill( SkillName.Tactics, 97.6, 100.0 );
			SetSkill( SkillName.Wrestling, 90.1, 100.0 );


			Fame = 18000;
			Karma = -18000;


			VirtualArmor = 64;


			Tamable = true;
			ControlSlots = 3;
			MinTameSkill = 96.3;
		}


		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 2 );
			AddLoot( LootPack.Average );
			AddLoot( LootPack.Gems, Utility.Random( 1, 5 ) );
		}


		public override int TreasureMapLevel{ get{ return 5; } }
		public override bool AutoDispel{ get{ return !Controlled; } }
		public override int Meat{ get{ return 19; } }
		public override int Hides{ get{ return 20; } }
		public override HideType HideType{ get{ if ( Utility.RandomBool() ){ return HideType.Frozen; } else { return HideType.Draconic; } } }
		public override int Scales{ get{ return 9; } }
		public override ScaleType ScaleType{ get{ return ScaleType.White; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat; } }
		public override bool CanAngerOnTame { get { return true; } }


		public WhiteWyrm( Serial serial ) : base( serial )
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


			Body = Server.Misc.MyServerSettings.WyrmBody();
		}
	}
}
// using System;// using Server.Items;


namespace Server.Mobiles
{
	[CorpseName( "a wyvern corpse" )]
	public class Wyvern : BaseCreature
	{
		[Constructable]
		public Wyvern () : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a wyvern";
			Body = 62;
			BaseSoundID = 362;


			SetStr( 202, 240 );
			SetDex( 153, 172 );
			SetInt( 51, 90 );


			SetHits( 125, 141 );


			SetDamage( 8, 19 );


			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Poison, 50 );


			SetResistance( ResistanceType.Physical, 35, 45 );
			SetResistance( ResistanceType.Fire, 30, 40 );
			SetResistance( ResistanceType.Cold, 20, 30 );
			SetResistance( ResistanceType.Poison, 90, 100 );
			SetResistance( ResistanceType.Energy, 30, 40 );


			SetSkill( SkillName.Poisoning, 60.1, 80.0 );
			SetSkill( SkillName.MagicResist, 65.1, 80.0 );
			SetSkill( SkillName.Tactics, 65.1, 90.0 );
			SetSkill( SkillName.Wrestling, 65.1, 80.0 );


			Fame = 4000;
			Karma = -4000;


			VirtualArmor = 40;
			
			Item Venom = new VenomSack();
				Venom.Name = "deadly venom sack";
				AddItem( Venom );


			Tamable = true;
			ControlSlots = 3;
			MinTameSkill = 63.9;
		}


		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
			AddLoot( LootPack.Meager );
			AddLoot( LootPack.MedScrolls );
		}


		public override bool ReacquireOnMovement{ get{ return !Controlled; } }


		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }
		public override Poison HitPoison{ get{ return Poison.Deadly; } }
		public override int TreasureMapLevel{ get{ return 2; } }


		public override int Meat{ get{ return 10; } }
		public override int Hides{ get{ return 20; } }
		public override HideType HideType{ get{ return HideType.Draconic; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat; } }
		public override bool CanAngerOnTame { get { return true; } }


		public override int GetAttackSound()
		{
			return 713;
		}


		public override int GetAngerSound()
		{
			return 718;
		}


		public override int GetDeathSound()
		{
			return 716;
		}


		public override int GetHurtSound()
		{
			return 721;
		}


		public override int GetIdleSound()
		{
			return 725;
		}


		public Wyvern( Serial serial ) : base( serial )
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
