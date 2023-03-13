using System;
using System.Collections.Generic;
using Server;
using System.Collections; 
using Server.Targeting;
using Server.Items;
using Server.Network;
using Server.ContextMenus;
using Server.Gumps;
using Server.Misc;
using Server.Mobiles;
using Server.Regions;

namespace Server.Mobiles
{
	public class Artist : BaseConvo
	{
		public override bool CanTeach { get { return true; } }

		[Constructable]
		public Artist(): base( AIType.AI_Animal, FightMode.None, 10, 1, 0.2, 0.4 )
		{
			InitStats( 31, 41, 51 );

			Job = JobFragment.artist;
			Karma = Utility.RandomMinMax( 13, -45 );

			SetSkill( SkillName.Anatomy, 36, 68 );

			SpeechHue = Server.Misc.RandomThings.GetSpeechHue();
			Title = "the painter";
			Hue = Server.Misc.RandomThings.GetRandomSkinColor();

			if( this.Female = Utility.RandomBool() )
			{
				this.Body = 0x191;
				this.Name = NameList.RandomName( "female" );
			}
			else
			{
				this.Body = 0x190;
				this.Name = NameList.RandomName( "male" );
			}

			AddItem( new Doublet( Utility.RandomDyedHue() ) );
			AddItem( new Sandals( Utility.RandomNeutralHue() ) );
			AddItem( new ShortPants( Utility.RandomNeutralHue() ) );
			AddItem( new HalfApron( Utility.RandomDyedHue() ) );

			Utility.AssignRandomHair( this );

			Container pack = new Backpack();

			pack.DropItem( new Gold( 250, 300 ) );

			pack.Movable = false;

			AddItem( pack );
		}

		public override bool ClickTitle { get { return false; } }

		///////////////////////////////////////////////////////////////////////////
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
						mobile.SendGump(new SpeechGump( "Portraits of Adventurers", SpeechFunctions.SpeechText( m_Giver.Name, m_Mobile.Name, "Painter" ) ));
					}
				}
            }
        }
		///////////////////////////////////////////////////////////////////////////

		public override bool OnDragDrop( Mobile from, Item dropped )
		{
			if ( dropped is PaintCanvas )
			{
				Container pack = from.Backpack;
				int paintPrice = 5000;

				if ( Server.Mobiles.BaseVendor.BeggingPose(from) > 0 ) // LET US SEE IF THEY ARE BEGGING - WIZARD
				{
					paintPrice = paintPrice - (int)( ( from.Skills[SkillName.Begging].Value * 0.005 ) * paintPrice ); if ( paintPrice < 1 ){ paintPrice = 1; }
				}

				if (pack.ConsumeTotal(typeof(Gold), paintPrice))
				{
					if ( Server.Mobiles.BaseVendor.BeggingPose(from) > 0 ){ Titles.AwardKarma( from, -Server.Mobiles.BaseVendor.BeggingKarma( from ), true ); } // DO ANY KARMA LOSS
					this.SayTo(from, "Here is a nice painting of you.");
					from.SendMessage(String.Format("You pay {0} gold.", paintPrice));

					WaxPaintingA portrait = new WaxPaintingA();

					string sTitle = "the " + Server.Misc.GetPlayerInfo.GetSkillTitle( from );
					if ( from.Title != null ){ sTitle = from.Title; }
					sTitle = sTitle.Replace("  ", String.Empty);
					portrait.Name = "painting of " + from.Name + " " + sTitle;
					portrait.PaintingFlipID1 = 0xEA3;
					portrait.PaintingFlipID2 = 0xEA4;
					portrait.ItemID = 0xEA3;

					from.AddToBackpack ( portrait );
				}
				else
				{
					this.SayTo(from, "It would cost you {0} gold to have a portrait done.", paintPrice);
					from.SendMessage("You do not have enough gold.");
					from.AddToBackpack ( new PaintCanvas() );
				}
				dropped.Delete();
			}
			else if ( dropped is WaxPaintingA && dropped.Weight == 15.0 )
			{
				this.SayTo(from, "How about this?");

				WaxPaintingA portrait = (WaxPaintingA)dropped;

				string sTitle = "the " + Server.Misc.GetPlayerInfo.GetSkillTitle( from );
				if ( from.Title != null ){ sTitle = from.Title; }
				sTitle = sTitle.Replace("  ", String.Empty);
				portrait.Name = "painting of " + from.Name + " " + sTitle;

				if ( dropped.ItemID == 0xEA3 || dropped.ItemID == 0xEA4 ){ 			portrait.PaintingFlipID1 = 0xEE7;	portrait.PaintingFlipID2 = 0xEC9;	portrait.ItemID = 0xEE7; }
				else if ( dropped.ItemID == 0xEE7 || dropped.ItemID == 0xEC9 ){		portrait.PaintingFlipID1 = 0xE9F;	portrait.PaintingFlipID2 = 0xEC8;	portrait.ItemID = 0xE9F; }
				else if ( dropped.ItemID == 0xE9F || dropped.ItemID == 0xEC8 ){		portrait.PaintingFlipID1 = 0xEA6;	portrait.PaintingFlipID2 = 0xEA8;	portrait.ItemID = 0xEA6; }
				else if ( dropped.ItemID == 0xEA6 || dropped.ItemID == 0xEA8 ){ 	portrait.PaintingFlipID1 = 0x2A5D;	portrait.PaintingFlipID2 = 0x2A61;	portrait.ItemID = 0x2A5D; }
				else if ( dropped.ItemID == 0x2A5D || dropped.ItemID == 0x2A61 ){	portrait.PaintingFlipID1 = 0x2A65;	portrait.PaintingFlipID2 = 0x2A67;	portrait.ItemID = 0x2A65; }
				else if ( dropped.ItemID == 0x2A65 || dropped.ItemID == 0x2A67 ){	portrait.PaintingFlipID1 = 0x2A69;	portrait.PaintingFlipID2 = 0x2A6D;	portrait.ItemID = 0x2A69; }
				else {																portrait.PaintingFlipID1 = 0xEA3;	portrait.PaintingFlipID2 = 0xEA4;	portrait.ItemID = 0xEA3; }

				from.AddToBackpack ( dropped );
			}
			return base.OnDragDrop( from, dropped );
		}

		public virtual void CheckMorph()
		{
			if ( CheckGargoyle() )
				return;

			if ( CheckNecromancer() )
				return;

			if ( CheckBarbarian() )
				return;

			if ( CheckOrk() )
				return;

			if ( CheckPirate() )
				return;

			CheckElf();
		}

		public virtual bool CheckElf()
		{
			Map map = this.Map;

			if ( ( map != Map.Felucca ) && ( !Region.IsPartOf( "the Enchanted Pass" ) ) )
				return false;

			// BARD'S TALE
			if ( Region.IsPartOf( typeof( BardTownRegion ) ) )
				return false;

			if ( Region.IsPartOf( "the Ethereal Plane" ) )
				return false;

				TurnToElf();

			return true;
		}

		public virtual bool CheckGargoyle()
		{
			Map map = this.Map;

			if ( map != Map.Malas )
				return false;

				TurnToGargoyle();

			return true;
		}

		public virtual bool CheckBarbarian()
		{
			Map map = this.Map;

			if ( ( map != Map.Tokuno ) && ( !Region.IsPartOf( "the Cimmeran Mines" ) ) )
				return false;

			if ( Region.IsPartOf( "the Forgotten Lighthouse" ) )
				return false;

				TurnToBarbarian();

			return true;
		}

		public virtual bool CheckPirate()
		{
			if ( !Region.IsPartOf( "the Forgotten Lighthouse" ) )
				return false;

				TurnToPirate();

			return true;
		}

		public virtual bool CheckOrk()
		{
			Map map = this.Map;

			if ( map != Map.TerMur )
				return false;

			if ( Region.IsPartOf( "the Cimmeran Mines" ) )
				return false;

			if ( Region.IsPartOf( "the Enchanted Pass" ) )
				return false;

			if ( Region.IsPartOf( "InnMaze" ) )
				return false;

			if ( Region.IsPartOf( "the Maze" ) )
				return false;

				TurnToOrk();

			return true;
		}

		public virtual bool CheckNecromancer()
		{
			Map map = this.Map;

			if ( map != Map.Trammel ) // WIZARD - BECAUSE UMBRA IS NOW ON TRAMMEL
				return false;

			if ( ( Region.IsPartOf( "the Undercity of Umbra" ) ) && ( Hue != 0x83E8 ) )
				TurnToNecromancer();

			return true;
		}

		public override void OnAfterSpawn()
		{
			CheckMorph();
		}

		protected override void OnMapChange( Map oldMap )
		{
			base.OnMapChange( oldMap );
			CheckMorph();
		}

		public virtual int GetRandomNecromancerHue()
		{
			switch ( Utility.Random( 20 ) )
			{
				case 0: return 0;
				case 1: return 0x4E9;
				default: return Utility.RandomList( 0x485, 0x497 );
			}
		}

		public virtual void TurnToNecromancer()
		{
			for ( int i = 0; i < this.Items.Count; ++i )
			{
				Item item = this.Items[i];

				if ( item is Hair || item is Beard )
					item.Hue = 0;
				else if ( item is BaseClothing || item is BaseWeapon || item is BaseArmor || item is BaseTool )
					item.Hue = GetRandomNecromancerHue();
			}

			HairHue = 0;
			FacialHairHue = 0;

			Hue = 0x83E8;
		}

		public virtual void TurnToBarbarian()
		{
			for ( int i = 0; i < this.Items.Count; ++i )
			{
				Item item = this.Items[i];

				if ( item is Hair || item is Beard )
				{
					item.Hue = 0x455;
				}
				else if ( ( item is BasePants ) || ( item is BaseOuterLegs ) )
				{
					item.Delete();
					AddItem( new Kilt(Utility.RandomYellowHue()) );
				}
				else if ( item is BaseClothing || item is BaseWeapon || item is BaseArmor || item is BaseTool )
				{
					item.Hue = Utility.RandomYellowHue();
				}
			}

			HairHue = 0x455;
			FacialHairHue = 0x455;
			SpeechHue = Server.Misc.RandomThings.GetSpeechHue();

			if ( Female )
			{
				this.Name = NameList.RandomName( "barb_female" );
			}
			else
			{
				this.Name = NameList.RandomName( "barb_male" );
			}
		}

		public virtual void TurnToOrk()
		{
			for ( int i = 0; i < this.Items.Count; ++i )
			{
				Item item = this.Items[i];

				if ( item is BaseClothing || item is BaseWeapon || item is BaseArmor || item is BaseTool )
					item.Hue = Utility.RandomYellowHue();
			}

			Hue = Utility.RandomList( 0x1C4, 0x1C5, 0x1C6, 0x1C7, 0x1C9, 0x1CA, 0x1CB, 0x1CC, 0x1CE, 0x1CF, 0x1D0, 0x1D1 );
			SpeechHue = Server.Misc.RandomThings.GetSpeechHue();
			HairHue = 0x455;
			FacialHairHue = 0x455;

			if ( Female )
			{
				this.Name = NameList.RandomName( "ork_female" );
			}
			else
			{
				this.Name = NameList.RandomName( "ork_male" );
			}

			this.Title = this.Title.Replace("the ork ", "the ");
			this.Title = this.Title.Replace("the ", "the ork ");
		}

		public virtual void TurnToPirate()
		{
			if ( Utility.RandomMinMax(1,5) > 1 ){ AddItem( new SkullCap(Utility.RandomYellowHue()) ); }
			else { AddItem( new TricorneHat(Utility.RandomYellowHue()) ); }
		}

		public virtual void TurnToElf()
		{
			for ( int i = 0; i < this.Items.Count; ++i )
			{
				Item item = this.Items[i];

				if ( item is Hair || item is Beard )
					item.Delete();
			}

			Race = Race.Elf;

			int hairHue = GetHairHue();
			Utility.AssignRandomHair( this, hairHue );
			FacialHairItemID = 0;
			SpeechHue = Server.Misc.RandomThings.GetSpeechHue();
			Hue = Server.Misc.RandomThings.GetRandomSkinColor(); 

			if ( Female )
			{
				this.Name = NameList.RandomName( "elf_female" );
				this.Body = 606;
			}
			else
			{
				this.Name = NameList.RandomName( "elf_male" );
				this.Body = 605;
			}

			this.Title = this.Title.Replace("the elf ", "the ");
			this.Title = this.Title.Replace("the ", "the elf ");
		}

		public virtual void TurnToGargoyle()
		{
			for ( int i = 0; i < this.Items.Count; ++i )
			{
				Item item = this.Items[i];

				if ( item is BaseClothing || item is Hair || item is Beard )
					item.Delete();
			}

			HairItemID = 0;
			FacialHairItemID = 0;

			Body = 0x2F6;
			Hue = RandomBrightHue() | 0x8000;
			Name = NameList.RandomName( "gargoyle vendor" );

			CapitalizeTitle();
		}

		public virtual int GetHairHue()
		{
			return Utility.RandomHairHue();
		}

		public virtual int RandomBrightHue()
		{
			if ( 0.1 > Utility.RandomDouble() )
				return Utility.RandomList( 0x62, 0x71 );

			return Utility.RandomList( 0x03, 0x0D, 0x13, 0x1C, 0x21, 0x30, 0x37, 0x3A, 0x44, 0x59 );
		}

		public virtual void CapitalizeTitle()
		{
			string title = this.Title;

			if ( title == null )
				return;

			string[] split = title.Split( ' ' );

			for ( int i = 0; i < split.Length; ++i )
			{
				if ( Insensitive.Equals( split[i], "the" ) )
					continue;

				if ( split[i].Length > 1 )
					split[i] = Char.ToUpper( split[i][0] ) + split[i].Substring( 1 );
				else if ( split[i].Length > 0 )
					split[i] = Char.ToUpper( split[i][0] ).ToString();
			}

			this.Title = String.Join( " ", split );
		}

		public Artist( Serial serial ): base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)0 ); // version 
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}
