using System; 
using Server;
using Server.Items;

namespace Server.Mobiles 
{ 
	[CorpseName( "an archmage corpse" )] 
	public class Archmage : BaseCreature 
	{ 
		public override int BreathPhysicalDamage{ get{ return 0; } }
		public override int BreathFireDamage{ get{ if ( YellHue < 2 ){ return 100; } else { return 0; } } }
		public override int BreathColdDamage{ get{ if ( YellHue == 3 ){ return 100; } else { return 0; } } }
		public override int BreathPoisonDamage{ get{ if ( YellHue == 2 ){ return 100; } else { return 0; } } }
		public override int BreathEnergyDamage{ get{ return 0; } }
		public override int BreathEffectHue{ get{ if ( YellHue == 1 ){ return 0x488; } else if ( YellHue == 2 ){ return 0xB92; } else if ( YellHue == 3 ){ return 0x5B5; } else { return 0x4FD; } } }
		public override int BreathEffectSound{ get{ return 0x238; } }
		public override int BreathEffectItemID{ get{ return 0x1005; } } // EXPLOSION POTION
		public override bool HasBreath{ get{ return true; } }
		public override double BreathEffectDelay{ get{ return 0.1; } }
		public override void BreathDealDamage( Mobile target, int form ){ base.BreathDealDamage( target, 2 ); }

		[Constructable] 
		public Archmage() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 ) 
		{
			((BaseCreature)this).midrace = 1;
			
			Body = 0x190; 
			Name = NameList.RandomName( "evil mage" );
			Title = "the mad archmage";
			EmoteHue = 11;

			Hue = Server.Misc.RandomThings.GetRandomSkinColor();
			Utility.AssignRandomHair( this );
			int HairColor = Utility.RandomHairHue();
			FacialHairItemID = Utility.RandomList( 0, 8254, 8255, 8256, 8257, 8267, 8268, 8269 );
			HairHue = HairColor;
			FacialHairHue = HairColor;

			Robe robe = new Robe( );
				robe.Hue = 0xA2A;
				robe.LootType = LootType.Blessed;
				AddItem( robe );

			WizardsHat hat = new WizardsHat( );
				hat.Hue = 0xA2A;
				hat.LootType = LootType.Blessed;
				AddItem( hat );

			Item boots = new ThighBoots( );
				boots.Hue = 0x497;
				boots.LootType = LootType.Blessed;
				AddItem( boots );

			if ( Utility.RandomMinMax( 1, 4 ) > 1 )
			{
				QuarterStaff staff = new QuarterStaff();
				staff.Name = "staff";
				staff.ItemID = Utility.RandomList( 0xDF0, 0x13F8, 0xE89, 0x2D25, 0x26BC, 0x26C6, 0xDF2, 0xDF3, 0xDF4, 0xDF5 );
				if ( staff.ItemID == 0x26BC || staff.ItemID == 0x26C6 ){ staff.Name = "scepter"; }
				if ( staff.ItemID == 0xDF2 || staff.ItemID == 0xDF3 || staff.ItemID == 0xDF4 || staff.ItemID == 0xDF5 ){ staff.Name = "magic wand"; }
				staff.LootType = LootType.Blessed;
				staff.Attributes.SpellChanneling = 1;
				AddItem( staff );
			}

			SetStr( 986, 1185 );
			SetDex( 177, 255 );
			SetInt( 196, 220 );

			SetHits( 592, 711 );

			SetDamage( 22, 29 );

			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Fire, 25 );
			SetDamageType( ResistanceType.Energy, 25 );

			SetResistance( ResistanceType.Physical, 65, 80 );
			SetResistance( ResistanceType.Fire, 60, 80 );
			SetResistance( ResistanceType.Cold, 50, 60 );
			SetResistance( ResistanceType.Poison, 100 );
			SetResistance( ResistanceType.Energy, 40, 50 );

			SetSkill( SkillName.Anatomy, 25.1, 50.0 );
			SetSkill( SkillName.EvalInt, 90.1, 100.0 );
			SetSkill( SkillName.Magery, 95.5, 100.0 );
			SetSkill( SkillName.Meditation, 25.1, 50.0 );
			SetSkill( SkillName.MagicResist, 100.5, 150.0 );
			SetSkill( SkillName.Tactics, 90.1, 100.0 );
			SetSkill( SkillName.Wrestling, 90.1, 100.0 );

			Fame = 24000;
			Karma = -24000;

			VirtualArmor = 90;

			PackReg( 30, 275 );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 2 );
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.MedScrolls, 2 );
			AddLoot( LootPack.Potions );
		}

		public override bool ClickTitle{ get{ return false; } }
		public override bool ShowFameTitle{ get{ return false; } }
		public override bool AlwaysAttackable{ get{ return true; } }
		public override int Meat{ get{ return 1; } }
		public override bool Unprovokable{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }
		public override int TreasureMapLevel{ get{ return 5; } }

		public void AddArcane( Item item )
		{
			if ( item is IArcaneEquip )
			{
				IArcaneEquip eq = (IArcaneEquip)item;
				eq.CurArcaneCharges = eq.MaxArcaneCharges = 20;
			}

			item.Hue = ArcaneGem.DefaultArcaneHue;
			item.LootType = LootType.Newbied;

			AddItem( item );
		}

		public override void OnAfterSpawn()
		{
			Server.Misc.IntelligentAction.BeforeMyBirth( this );
			base.OnAfterSpawn();
		}

		public override void OnGotMeleeAttack( Mobile attacker )
		{
			base.OnGotMeleeAttack( attacker );
			Server.Misc.IntelligentAction.DoSpecialAbility( this, attacker );
			Server.Misc.IntelligentAction.CryOut( this );
		}

		public override void OnGaveMeleeAttack( Mobile defender )
		{
			base.OnGaveMeleeAttack( defender );
			Server.Misc.IntelligentAction.DoSpecialAbility( this, defender );
		}

		public override bool OnBeforeDeath()
		{
			Server.Misc.IntelligentAction.BeforeMyDeath( this );
			return base.OnBeforeDeath();
		}

		public Archmage( Serial serial ) : base( serial ) 
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