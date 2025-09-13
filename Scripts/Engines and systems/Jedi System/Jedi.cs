using System;
using System.Collections.Generic;
using Server;
using Server.Targeting;
using Server.Items;
using Server.Network;
using Server.ContextMenus;
using Server.Gumps;
using Server.Misc;
using Server.Mobiles;

namespace Server.Mobiles 
{ 
	public class Jedi : BaseCreature 
	{
		public int actionCount;
		[CommandProperty(AccessLevel.Owner)]
		public int action_Count { get { return actionCount; } set { actionCount = value; InvalidateProperties(); } }

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
		public override double BreathEffectDelay{ get{ return 0.1; } }
		public override void BreathDealDamage( Mobile target, int form ){ base.BreathDealDamage( target, 21 ); }

		[Constructable] 
		public Jedi() : base( AIType.AI_Mage, FightMode.Evil, 10, 1, 0.2, 0.4 ) 
		{
            SpeechHue = Server.Misc.RandomThings.GetSpeechHue();
			Title = "the Jedi";
			Hue = Server.Misc.RandomThings.GetRandomSkinColor();
				if ( Utility.RandomBool() ){ Hue = Utility.RandomList( 0x6F6, 0x97F, 0x99B, 0x6E4, 0x5E0, 0xB38, 0xB2B ); }

			if ( this.Female = Utility.RandomBool() )
			{
				Body = 0x191;
				Name = NameList.RandomName( "female" );
				Utility.AssignRandomHair( this );
				HairHue = Utility.RandomHairHue();
			}
			else
			{
				Body = 0x190;
				Name = NameList.RandomName( "male" ); 
				Utility.AssignRandomHair( this );
				int HairColor = Utility.RandomHairHue();
				FacialHairItemID = Utility.RandomList( 0, 8254, 8255, 8256, 8257, 8267, 8268, 8269 );
				HairHue = HairColor;
				FacialHairHue = HairColor;
			}

			SetStr( 350, 400 );
			SetDex( 177, 255 );
			SetInt( 350, 400 );

			SetHits( 502, 531 );

			SetDamage( 22, 29 );

			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Energy, 50 );

			SetResistance( ResistanceType.Physical, 45, 55 );
			SetResistance( ResistanceType.Fire, 30, 40 );
			SetResistance( ResistanceType.Cold, 30, 40 );
			SetResistance( ResistanceType.Poison, 40, 50 );
			SetResistance( ResistanceType.Energy, 60, 70 );

			SetSkill( SkillName.EvalInt, 100.5, 150.0 );
			SetSkill( SkillName.Magery, 100.5, 150.0 );
			SetSkill( SkillName.Meditation, 100.5, 150.0 );
			SetSkill( SkillName.MagicResist, 100.5, 150.0 );

			SetSkill( SkillName.Anatomy, 100.5, 150.0 );
			SetSkill( SkillName.MagicResist, 100.5, 150.0 );
			SetSkill( SkillName.Wrestling, 100.5, 150.0 );
			SetSkill( SkillName.Swords, 100.5, 150.0 );
			SetSkill( SkillName.Tactics, 100.5, 150.0 );

			Fame = 14000;
			Karma = 14000;

			int color = Utility.RandomNeutralHue();

			VirtualArmor = 90;

			Item robe = new Robe( color );
				robe.Name = "Jedi robe";
				AddItem( robe );
				if ( Utility.RandomMinMax( 1, 10 ) == 1 ){ SpecialItem( robe, this ); }

			Item boots = new ElvenBoots();
				boots.Name = "Jedi boots";
				AddItem( boots );
				if ( Utility.RandomMinMax( 1, 10 ) == 1 ){ SpecialItem( boots, this ); }

			if ( Utility.RandomBool() )
			{
				Item cloak = new Cloak( Utility.RandomNeutralHue() );
					cloak.Name = "Jedi cloak";
					AddItem( cloak );
				if ( Utility.RandomMinMax( 1, 10 ) == 1 ){ SpecialItem( cloak, this ); }
			}

			if ( Utility.RandomBool() )
			{
				Item gloves = new LeatherGloves();
					gloves.Name = "Jedi gloves";
					AddItem( gloves );
				if ( Utility.RandomMinMax( 1, 10 ) == 1 ){ SpecialItem( gloves, this ); }
			}

			Item hood = new ClothHood( color );
				hood.Name = "Jedi hood";
				AddItem( hood );
			if ( Utility.RandomMinMax( 1, 10 ) == 1 ){ SpecialItem( hood, this ); }

			if ( Utility.RandomMinMax( 1, 5 ) == 1 )
			{
				if ( Utility.RandomMinMax( 1, 5 ) == 1 )
				{
					Item sword = new DoubleLaserSword();
						sword.Name = "Jedi double laser sword";
						((BaseWeapon)sword).Attributes.SpellChanneling = 1;
						AddItem( sword );
						if ( Utility.RandomMinMax( 1, 10 ) == 1 ){ SpecialItem( sword, this ); }
				}
				else
				{
					Item sword = new LightSword();
						sword.Name = "Jedi laser sword";
						((BaseWeapon)sword).Attributes.SpellChanneling = 1;
						AddItem( sword );
						if ( Utility.RandomMinMax( 1, 10 ) == 1 ){ SpecialItem( sword, this ); }
				}
			}
			else
			{
				Item sword = new Longsword();
					sword.Name = "Jedi sword";
					((BaseWeapon)sword).Attributes.SpellChanneling = 1;
					AddItem( sword );
					if ( Utility.RandomMinMax( 1, 10 ) == 1 ){ SpecialItem( sword, this ); }
					if ( Server.Misc.MaterialInfo.IsMetalItem( sword ) && Utility.RandomMinMax( 1, 20 ) == 1 ){ ((BaseWeapon)sword).Resource = CraftResource.Xormite; }
					else if ( Server.Misc.MaterialInfo.IsMetalItem( sword ) ){ Server.Misc.MorphingTime.MakeSpaceAceMetalArmorWeapon( sword, null ); }
			}
		}

		public static void SpecialItem( Item item, Mobile m )
		{
			int min = (int)(m.Fame/200);
			int max = (int)(m.Fame/100);
			int props = (int)(m.Fame/1500) + Utility.RandomMinMax( 0, (int)(m.Fame/2000) );

			if ( item is BaseHat ){ BaseHat hat = (BaseHat)item; BaseRunicTool.ApplyAttributesTo( hat, false, 0, props, min, max ); }
			else if ( item is BaseClothing ){ BaseClothing cloth = (BaseClothing)item; BaseRunicTool.ApplyAttributesTo( cloth, false, 0, props, min, max ); }
			else if ( item is BaseArmor ){ BaseArmor armor = (BaseArmor)item; BaseRunicTool.ApplyAttributesTo( armor, false, 0, props, min, max ); }
			else if ( item is BaseWeapon ){ BaseWeapon weapon = (BaseWeapon)item; BaseRunicTool.ApplyAttributesTo( weapon, false, 0, props, min, max ); }
		}

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );

			if ( Utility.RandomBool() )
			{
				LoreBook book = new LoreBook();
				book.BookTitle = "The Jedi Order";
				book.Name = book.BookTitle;
				book.BookAuthor = "Zoda the Jedi Master";
				LoreBook.SetBookCover( 16, book );
				book.ItemID = 0x992D;
				book.Light = LightType.Circle225;
				LoreBook.GetText( book );
				c.DropItem( book );
			}
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list ) 
		{ 
			base.GetContextMenuEntries( from, list ); 
			list.Add( new SpeechGumpEntry( from, this ) ); 
		} 

		public class SpeechGumpEntry : ContextMenuEntry
		{
			private Mobile m_Mobile;
			private Mobile m_Giver;
			
			public SpeechGumpEntry( Mobile from, Mobile giver ) : base( 6146, 3 )
			{
				m_Mobile = from;
				m_Giver = giver;
			}

			public override void OnClick()
			{
			    if( !( m_Mobile is PlayerMobile ) )
				return;
				
				PlayerMobile mobile = (PlayerMobile) m_Mobile;
				{
					if ( ! mobile.HasGump( typeof( SpeechGump ) ) )
					{
						mobile.SendGump(new SpeechGump( "The Way of the Jedi", SpeechFunctions.SpeechText( m_Giver.Name, m_Mobile.Name, "Jedi" ) ));
					}
				}
            }
        }

		public override bool OnDragDrop( Mobile from, Item dropped )
		{
			if ( dropped is Gold && dropped.Amount >= 5000 && from.Karma >= 0 && from.Skills[SkillName.EvalInt].Base >= 25 )
			{
				this.Say( "Take this, and be enlightened." );

				LoreBook book = new LoreBook();
				book.BookTitle = "The Jedi Order";
				book.Name = book.BookTitle;
				book.BookAuthor = "Zoda the Jedi Master";
				LoreBook.SetBookCover( 16, book );
				book.ItemID = 0x992D;
				book.Light = LightType.Circle225;
				LoreBook.GetText( book );
				from.AddToBackpack ( book );

				dropped.Delete();
			}
			else
			{
				from.AddToBackpack ( dropped );
			}

			return base.OnDragDrop( from, dropped );
		}

		public override void OnGaveMeleeAttack( Mobile defender )
		{
			SwitchTactics();
			base.OnGaveMeleeAttack( defender );
		}

		public override void OnGotMeleeAttack( Mobile attacker )
		{
			SwitchTactics();
			base.OnGotMeleeAttack( attacker );
		}

		public override void OnDamage( int amount, Mobile from, bool willKill )
		{
			SwitchTactics();
			base.OnDamage( amount, from, willKill );
		}

		public override void OnDamagedBySpell( Mobile from )
		{
			SwitchTactics();
			base.OnDamagedBySpell( from );
		}

		public void SwitchTactics()
		{
			actionCount++;

			if ( actionCount > 20 )
			{
				actionCount = 0;
				if ( AI == AIType.AI_Melee ){ AI = AIType.AI_Mage; }
				else { AI = AIType.AI_Melee; }
			}
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Meager );
		}

		public override bool ClickTitle{ get{ return false; } }
		public override bool ShowFameTitle{ get{ return false; } }
		public override bool CanRummageCorpses{ get{ return true; } }
		public override bool AlwaysAttackable{ get{ return true; } }
		public override int Meat{ get{ return 1; } }
		public override Poison PoisonImmune{ get{ return Poison.Greater; } }

		public Jedi( Serial serial ) : base( serial ) 
		{ 
		} 

		public override void Serialize( GenericWriter writer ) 
		{ 
			base.Serialize( writer ); 
			writer.Write( (int) 0 ); 
			writer.Write( actionCount );
		} 

		public override void Deserialize( GenericReader reader ) 
		{ 
			base.Deserialize( reader ); 
			int version = reader.ReadInt(); 
			actionCount = reader.ReadInt();
		} 
	} 
}