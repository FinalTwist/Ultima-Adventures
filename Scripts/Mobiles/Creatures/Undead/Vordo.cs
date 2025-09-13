using System;
using Server;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.Misc;
using Server.Network;
using System.Text;
using Server.Mobiles;
using Server.Engines.PartySystem;

namespace Server.Mobiles
{
	[CorpseName( "a ghostly essence" )]
	public class Vordo : BaseCreature 
	{ 
		[Constructable] 
		public Vordo() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 ) 
		{
			Body = 400;
			Name = "Vordo";
			Title = "of the darkest magic";
			Hue = 0x47E;

			AddItem( new Robe() );
			AddItem( new WizardsHat() );
			AddItem( new ThighBoots() );

			MorphingTime.BlessMyClothes( this );
			MorphingTime.ColorMyClothes( this, 1194 );

			BaseSoundID = 412;

			SetStr( 300, 400 );
			SetDex( 177, 255 );
			SetInt( 650, 750 );

			SetHits( 1050, 1250 );

			SetDamage( 35, 55 );

			SetDamageType( ResistanceType.Physical, 10 );
			SetDamageType( ResistanceType.Cold, 40 );
			SetDamageType( ResistanceType.Energy, 50 );

			SetResistance( ResistanceType.Physical, 40, 60 );
			SetResistance( ResistanceType.Fire, 20, 30 );
			SetResistance( ResistanceType.Cold, 50, 60 );
			SetResistance( ResistanceType.Poison, 55, 65 );
			SetResistance( ResistanceType.Energy, 40, 50 );

			SetSkill( SkillName.Necromancy, 89, 99.1 );
			SetSkill( SkillName.SpiritSpeak, 90.0, 99.0 );

			SetSkill( SkillName.EvalInt, 100.0 );
			SetSkill( SkillName.Magery, 70.1, 80.0 );
			SetSkill( SkillName.Meditation, 85.1, 95.0 );
			SetSkill( SkillName.MagicResist, 80.1, 100.0 );
			SetSkill( SkillName.Tactics, 70.1, 90.0 );


			Fame = 12000;
			Karma = -12000;

			VirtualArmor = 40;
			PackNecroReg( 17, 24 );

			AddItem( new LightSource() );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 3 );
			AddLoot( LootPack.MedScrolls, 2 );
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
		}

		public override void OnGaveMeleeAttack( Mobile defender )
		{
			base.OnGaveMeleeAttack( defender );
			Server.Misc.IntelligentAction.DoSpecialAbility( this, defender );
		}

		public override bool OnBeforeDeath()
		{
			this.Body = 13;

			Server.Misc.IntelligentAction.BeforeMyDeath( this );
			Server.Misc.IntelligentAction.DropItem( this, this.LastKiller );
			return base.OnBeforeDeath();
		}

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );
			GhostlyDust ingut = new GhostlyDust();
   			ingut.Amount = Utility.RandomMinMax( 1, 2 );
   			c.DropItem(ingut);

			if ( 1 == Utility.RandomMinMax( 0, 1 ) )
			{
				switch( Utility.RandomMinMax( 0, 8 ) )
				{
					case 0: Item relic1 = new MagicTalisman(); MorphingItem.MorphMyItem( relic1, "IGNORED", "Vordo's", "IGNORED", MorphingTemplates.TemplateVordo("misc") ); c.DropItem(relic1); break;
					case 1: Item relic2 = new MagicRobe(); relic2.ItemID = 0x1F03; relic2.Name = "robe"; MorphingItem.MorphMyItem( relic2, "IGNORED", "Vordo's", "IGNORED", MorphingTemplates.TemplateVordo("misc") ); c.DropItem(relic2); break;
					case 2: Item relic3 = new MagicHat(); relic3.ItemID = 5912; relic3.Name = "hat"; MorphingItem.MorphMyItem( relic3, "IGNORED", "Vordo's", "IGNORED", MorphingTemplates.TemplateVordo("misc") ); c.DropItem(relic3); break;
					case 3: Item relic4 = new MagicCloak(); relic4.ItemID = 0x1515; relic4.Name = "cloak"; MorphingItem.MorphMyItem( relic4, "IGNORED", "Vordo's", "IGNORED", MorphingTemplates.TemplateVordo("misc") ); c.DropItem(relic4); break;
					case 4: Item relic5 = new MagicBoots(); relic5.ItemID = 0x170B; relic5.Name = "boots"; MorphingItem.MorphMyItem( relic5, "IGNORED", "Vordo's", "IGNORED", MorphingTemplates.TemplateVordo("misc") ); c.DropItem(relic5); break;
					case 5: Item relic6 = new MagicBelt(); relic6.ItemID = 0x2790; relic6.Name = "belt"; MorphingItem.MorphMyItem( relic6, "IGNORED", "Vordo's", "IGNORED", MorphingTemplates.TemplateVordo("misc") ); c.DropItem(relic6); break;
					case 6: Item relic7 = new SilverRing(); relic7.Name = "ring"; MorphingItem.MorphMyItem( relic7, "IGNORED", "Vordo's", "IGNORED", MorphingTemplates.TemplateVordo("misc") ); c.DropItem(relic7); break;
					case 7: Item relic8 = new Necklace(); relic8.Name = "amulet"; MorphingItem.MorphMyItem( relic8, "IGNORED", "Vordo's", "IGNORED", MorphingTemplates.TemplateVordo("misc") ); c.DropItem(relic8); break;
					case 8: Item relic9 = new Dagger(); relic9.Name = "sacrificial dagger"; MorphingItem.MorphMyItem( relic9, "IGNORED", "Vordo's", "IGNORED", MorphingTemplates.TemplateVordo("weapons") ); c.DropItem(relic9); break;
				}
			}

			Mobile killer = this.LastKiller;
			if ( killer != null )
			{
				if ( killer is BaseCreature )
					killer = ((BaseCreature)killer).GetMaster();

				if ( killer is PlayerMobile )
				{
					Party p = Engines.PartySystem.Party.Get( killer );
					if ( p != null )
					{
						foreach ( PartyMemberInfo pmi in p.Members )
						{
							if ( pmi.Mobile is PlayerMobile && pmi.Mobile.InRange(this.Location, 20) && pmi.Mobile.Map == this.Map )
							{
								pmi.Mobile.AddToBackpack( new VordoScroll() );
								pmi.Mobile.SendMessage("An item has appeared in your backpack!");
							}
						}
					}
					else
					{
						killer.AddToBackpack( new VordoScroll() );
						killer.SendMessage("An item has appeared in your backpack!");
					}
				}
			}
		}

		public override bool BleedImmune{ get{ return false; } }
		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }
		public override bool CanRummageCorpses{ get{ return true; } }
		public override bool ShowFameTitle{ get{ return false; } }
		public override bool AlwaysAttackable{ get{ return true; } }
		public override bool Unprovokable{ get{ return true; } }
		public override int TreasureMapLevel{ get{ return 4; } }

		public override int GetIdleSound()
		{
			return 0x19D;
		}

		public override int GetAngerSound()
		{
			return 0x175;
		}

		public override int GetDeathSound()
		{
			return 0x108;
		}

		public override int GetAttackSound()
		{
			return 0xE2;
		}

		public override int GetHurtSound()
		{
			return 0x28B;
		}

		public Vordo( Serial serial ) : base( serial )
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
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
namespace Server.Items
{
	public class VordoScroll : Item
	{
		[Constructable]
		public VordoScroll() : base( 0x227A )
		{
			Name = "Vordo's Magical Gate Research";
			Weight = 1;
		}

		public VordoScroll( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			string world = Worlds.GetMyWorld( from.Map, from.Location, from.X, from.Y );

			if ( !IsChildOf( from.Backpack ) ) 
			{
				from.SendMessage( "This spell must be in your backpack to use." );
			}
			else if ( world == "the Bottle World of Kuldar" )
			{
				Server.Items.CharacterDatabase.SetKeys( from, "VordoKey", true );
				from.PlaySound( 0x249 );
				from.SendMessage( "You learned Vordo's secrets to escaping this place." );
				from.SendMessage( "The parchment crumbles to dust." );
				this.Delete();
			}
			else
			{
				from.PlaySound( 0x249 );
				from.SendMessage( "This seems like a bunch of scribbles." );
				from.SendMessage( "The parchment crumbles to dust." );
				this.Delete();
			}
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);

			list.Add( 1070722, "Vordo's notes on escaping the bottle.");
			list.Add( 1049644, "Learn to use teleporting magic here.");
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