using System;
using Server;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.Misc;

namespace Server.Mobiles
{
	[CorpseName( "a giant corpse" )]
	public class StoneGiant : BaseCreature
	{
		public override WeaponAbility GetWeaponAbility()
		{
			return WeaponAbility.Dismount;
		}

		public override int BreathPhysicalDamage{ get{ return 100; } }
		public override int BreathFireDamage{ get{ return 0; } }
		public override int BreathColdDamage{ get{ return 0; } }
		public override int BreathPoisonDamage{ get{ return 0; } }
		public override int BreathEnergyDamage{ get{ return 0; } }
		public override int BreathEffectHue{ get{ return 0; } }
		public override int BreathEffectSound{ get{ return 0x65A; } }
		public override int BreathEffectItemID{ get{ return 0x1363; } } // LARGE BOULDER
		public override bool HasBreath{ get{ return true; } }
		public override double BreathEffectDelay{ get{ return 0.1; } }
		public override void BreathDealDamage( Mobile target, int form ){ base.BreathDealDamage( target, 7 ); }
		public override double BreathDamageScalar{ get{ return 0.7; } }

		[Constructable]
		public StoneGiant() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = NameList.RandomName( "giant" );
			Title = "the stone giant";
			Body = 485;
			Hue = 2500;
			BaseSoundID = 609;

			SetStr( 536, 585 );
			SetDex( 126, 145 );
			SetInt( 281, 305 );

			SetHits( 322, 351 );
			SetMana( 0 );

			SetDamage( 16, 23 );

			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Fire, 50 );

			SetResistance( ResistanceType.Physical, 45, 50 );
			SetResistance( ResistanceType.Fire, 50, 60 );
			SetResistance( ResistanceType.Cold, 25, 35 );
			SetResistance( ResistanceType.Poison, 30, 40 );
			SetResistance( ResistanceType.Energy, 30, 40 );

			SetSkill( SkillName.MagicResist, 60.3, 105.0 );
			SetSkill( SkillName.Tactics, 80.1, 100.0 );
			SetSkill( SkillName.Wrestling, 80.1, 90.0 );

			Fame = 11000;
			Karma = -11000;

			VirtualArmor = 48;
		}

		public override int GetAttackSound(){ return 0x5F8; }	// A
		public override int GetDeathSound(){ return 0x5F9; }	// D
		public override int GetHurtSound(){ return 0x5FA; }		// H

		public override void OnAfterSpawn()
		{
			Hue = 2500; // REGULAR STONE
			switch ( Utility.RandomMinMax( 0, 12 ) )
			{
				case 0: Hue = MaterialInfo.GetMaterialColor( "copper", "monster", 0 ); break; // Copper
				case 1: Hue = MaterialInfo.GetMaterialColor( "verite", "monster", 0 ); break; // Verite
				case 2: Hue = MaterialInfo.GetMaterialColor( "valorite", "monster", 0 ); break; // Valorite
				case 3: Hue = MaterialInfo.GetMaterialColor( "agapite", "monster", 0 ); break; // Agapite
				case 4: Hue = MaterialInfo.GetMaterialColor( "bronze", "monster", 0 ); break; // Bronze
				case 5: Hue = MaterialInfo.GetMaterialColor( "dull copper", "monster", 0 ); break; // Dull Copper
				case 6: Hue = MaterialInfo.GetMaterialColor( "gold", "monster", 0 ); break; // Gold
				case 7: Hue = MaterialInfo.GetMaterialColor( "shadow iron", "monster", 0 ); break; // Shadow Iron
				case 8:
					if ( Worlds.IsExploringSeaAreas( this ) == true ){ Hue = MaterialInfo.GetMaterialColor( "nepturite", "monster", 0 ); }
					else if ( Worlds.GetMyWorld( this.Map, this.Location, this.X, this.Y ) == "the Serpent Island" ){ Hue = MaterialInfo.GetMaterialColor( "obsidian", "monster", 0 ); }
					else if ( Worlds.GetMyWorld( this.Map, this.Location, this.X, this.Y ) == "the Underworld" && this.Map == Map.TerMur ){ Hue = MaterialInfo.GetMaterialColor( "xormite", "monster", 0 ); }
					else if ( Worlds.GetMyWorld( this.Map, this.Location, this.X, this.Y ) == "the Underworld" ){ Hue = MaterialInfo.GetMaterialColor( "mithril", "monster", 0 ); }
					break; // Special
			}

			base.OnAfterSpawn();
		}

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );

			Mobile killer = this.LastKiller;

			Item granite = new Granite();

			if ( this.Hue == MaterialInfo.GetMaterialColor( "copper", "monster", 0 ) ){ granite = new CopperGranite(); }
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "verite", "monster", 0 ) ){ granite = new VeriteGranite(); }
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "valorite", "monster", 0 ) ){ granite = new ValoriteGranite(); }
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "agapite", "monster", 0 ) ){ granite = new AgapiteGranite(); }
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "bronze", "monster", 0 ) ){ granite = new BronzeGranite(); }
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "dull copper", "monster", 0 ) ){ granite = new DullCopperGranite(); }
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "gold", "monster", 0 ) ){ granite = new GoldGranite(); }
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "shadow iron", "monster", 0 ) ){ granite = new ShadowIronGranite(); }
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "mithril", "monster", 0 ) ){ granite = new MithrilGranite(); }
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "xormite", "monster", 0 ) ){ granite = new XormiteGranite(); }
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "obsidian", "monster", 0 ) ){ granite = new ObsidianGranite(); }
			else if ( this.Hue == MaterialInfo.GetMaterialColor( "nepturite", "monster", 0 ) ){ granite = new NepturiteGranite(); }

   			granite.Amount = Utility.RandomMinMax( 1, 5 );

			if ( killer != null )
			{
				if ( killer is BaseCreature )
					killer = ((BaseCreature)killer).GetMaster();

				if ( killer is PlayerMobile )
				{
					if ( GetPlayerInfo.LuckyKiller( killer.Luck, killer ) && Utility.RandomMinMax( 1, 4 ) == 1 )
					{
						LootChest MyChest = new LootChest( Server.Misc.IntelligentAction.FameBasedLevel( this ) );
						MyChest.ItemID = Utility.RandomList( 0x1248, 0x1264 );
						MyChest.GumpID = 0x3D;
						MyChest.TrapType = TrapType.None;
						MyChest.Locked = false;
						MyChest.Name = "stone giant sack";
						MyChest.Hue = 0x9C4;
						c.DropItem( MyChest );
					}
				}
			}

   			c.DropItem(granite);
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.Average );
		}

		public override int Meat{ get{ return 4; } }
		public override int TreasureMapLevel{ get{ return 3; } }
		public override int Hides{ get{ return 18; } }
		public override HideType HideType{ get{ return HideType.Goliath; } }

		public StoneGiant( Serial serial ) : base( serial )
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