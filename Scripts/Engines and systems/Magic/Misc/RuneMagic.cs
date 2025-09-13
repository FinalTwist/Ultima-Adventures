using System;
using System.Collections;
using Server.Multis;
using Server.Mobiles;
using Server.Network;
using Server.ContextMenus;
using Server.Spells;
using System.Collections.Generic;
using Server.Misc;
using Server;
using Server.Gumps;
using Server.Commands;

namespace Server.Items
{
	public class RuneBag : BaseContainer
	{
		public override int DefaultGumpID{ get{ return 0x3D; } }
		public override int DefaultDropSound{ get{ return 0x48; } }

		public override Rectangle2D Bounds
		{
			get{ return new Rectangle2D( 29, 34, 108, 94 ); }
		}

		[Constructable]
		public RuneBag() : base( 0xE84 )
		{
			Name = "rune bag";
			Weight = 1.0;
		}

		public void RuneBagCast( Mobile m, RuneBag bag )
		{

// descs from U6. For reference.
// I've left in my notes for rareness, this is custom to my shard.

			bool foundAn	= false; // Negate/Dispel	- common
			bool foundBet	= false; // Small		- semi-common
			bool foundCorp	= false; // Death		- rare
			bool foundDes	= false; // Lower/Down		- semi-common
			bool foundEx	= false; // Freedom		- semi-common
			bool foundFlam	= false; // Flame		- semi-common
			bool foundGrav	= false; // Energy/Field	- rare
			bool foundHur	= false; // Wind		- semi-common
			bool foundIn	= false; // Make/Create/Cause	- common
			bool foundJux	= false; // Danger/Trap/Harm	- semi-common
			bool foundKal	= false; // Summon/Invoke	- common
			bool foundLor	= false; // Light		- common
			bool foundMani	= false; // Life/Healing	- common
			bool foundNox	= false; // Poison		- semi-common
			bool foundOrt	= false; // Magic		- semi-common
			bool foundPor	= false; // Move/Movement	- semi-common
			bool foundQuas	= false; // Illusion		- semi-common
			bool foundRel	= false; // Change		- common
			bool foundSanct	= false; // Protect/Protection	- semi-common
			bool foundTym	= false; // Time		- rare
			bool foundUus	= false; // Raise/Up		- semi-common
			bool foundVas	= false; // Great		- rare
			bool foundWis	= false; // Know/Knowledge	- semi-common
			bool foundXen	= false; // Creature		- semi-common
			bool foundYlem	= false; // Matter		- semi-common
			bool foundZu	= false; // Sleep		- semi-common

			foreach( Item I in bag.Items ) 
			{ 
				if      ( I is An )
					foundAn		= true;
				else if ( I is Bet )
					foundBet	= true;
				else if ( I is Corp )
					foundCorp	= true;
				else if ( I is Des )
					foundDes	= true;
				else if ( I is Ex )
					foundEx		= true;
				else if ( I is Flam )
					foundFlam	= true;
				else if ( I is Grav )
					foundGrav	= true;
				else if ( I is Hur )
					foundHur	= true;
				else if ( I is In )
					foundIn		= true;
				else if ( I is Jux )
					foundJux	= true;
				else if ( I is Kal )
					foundKal	= true;
				else if ( I is Lor )
					foundLor	= true;
				else if ( I is Mani )
					foundMani	= true;
				else if ( I is Nox )
					foundNox	= true;
				else if ( I is Ort )
					foundOrt	= true;
				else if ( I is Por )
					foundPor	= true;
				else if ( I is Quas )
					foundQuas	= true;
				else if ( I is Rel )
					foundRel	= true;
				else if ( I is Sanct )
					foundSanct	= true;
				else if ( I is Tym )
					foundTym	= true;
				else if ( I is Uus )
					foundUus	= true;
				else if ( I is Vas )
					foundVas	= true;
				else if ( I is Wis )
					foundWis	= true;
				else if ( I is Xen )
					foundXen	= true;
				else if ( I is Ylem )
					foundYlem	= true;
				else if ( I is Zu )
					foundZu		= true;
			}


			int m_SpellID = -1;  // no match


/// spells go here.  ////////////////////////////////////////////
/*
			if ( ( found ) ) && bag.Items.Count ==  )
				m_SpellID = ;
*/



///  first circle   /////////////////////////////////////////////


// clumsy: Uus Jux
			if ( ( foundUus && foundJux ) && bag.Items.Count == 2 )
				m_SpellID = 0;

// Create food: In Mani Ylem
			if ( ( foundIn && foundMani && foundYlem ) && bag.Items.Count == 3 )
				m_SpellID = 1;

// Feeblemind: Rel Wis
			if ( ( foundRel && foundWis ) && bag.Items.Count == 2 )
				m_SpellID = 2;

// Heal: In Mani
			if ( ( foundIn && foundMani ) && bag.Items.Count == 2 )
				m_SpellID = 3;

// Magic arrow: In Por Ylem
			if ( ( foundIn && foundPor && foundYlem ) && bag.Items.Count == 3 )
				m_SpellID = 4;

// Night sight: In Lor
			if ( ( foundIn && foundLor ) && bag.Items.Count == 2 )
				m_SpellID = 5;

// Reactive armor: Flam Sanct
			if ( ( foundFlam && foundSanct ) && bag.Items.Count == 2 )
				m_SpellID = 6;

// Weaken: Des Mani
			if ( ( foundDes && foundMani ) && bag.Items.Count == 2 )
				m_SpellID = 7;



///  second circle   ///////////////////////////////////////////////


// Agility: Ex Uus
			if ( ( foundUus && foundEx ) && bag.Items.Count == 2 )
				m_SpellID = 8;

// Cunning: Uus Wis
			if ( ( foundUus && foundWis ) && bag.Items.Count == 2 )
				m_SpellID = 9;

// Cure: An Nox
			if ( ( foundAn && foundNox ) && bag.Items.Count == 2 )
				m_SpellID = 10;

// Harm: An Mani
			if ( ( foundAn && foundMani ) && bag.Items.Count == 2 )
				m_SpellID = 11;

// Magic Trap: In Jux
			if ( ( foundIn && foundJux ) && bag.Items.Count == 2 )
				m_SpellID = 12;

// Magic Untrap: An Jux
			if ( ( foundAn && foundJux ) && bag.Items.Count == 2 )
				m_SpellID = 13;

// Protection: Uus Sanct
			if ( ( foundUus && foundSanct ) && bag.Items.Count == 2 )
				m_SpellID = 14;

// Strength: Uus Mani
			if ( ( foundUus && foundMani ) && bag.Items.Count == 2 )
				m_SpellID = 15;



///  Third circle   ////////////////////////////////////////////

// Bless: Rel Sanct
			if ( ( foundRel && foundSanct ) && bag.Items.Count == 2 )
				m_SpellID = 16;

// Fireball: Vas Flam
			if ( ( foundVas && foundFlam ) && bag.Items.Count == 2 )
				m_SpellID = 17;

// Magic Lock: An Por
			if ( ( foundAn && foundPor ) && bag.Items.Count == 2 )
				m_SpellID = 18;

// Poison: In Nox
			if ( ( foundIn && foundNox ) && bag.Items.Count == 2 )
				m_SpellID = 19;

// Telekinesis: Ort Por Ylem
			if ( ( foundOrt && foundPor && foundYlem ) && bag.Items.Count == 3 )
				m_SpellID = 20;

// Teleport: Rel Por
			if ( ( foundRel && foundPor ) && bag.Items.Count == 2 )
				m_SpellID = 21;

// Unlock: Ex Por
			if ( ( foundEx && foundPor ) && bag.Items.Count == 2 )
				m_SpellID = 22;

// Wall of Stone: In Sanct Ylem
			if ( ( foundIn && foundSanct && foundYlem ) && bag.Items.Count == 3 )
				m_SpellID = 23;



///  Fourth circle   ////////////////////////////////////////////


// Arch Cure: Vas An Nox
			if ( ( foundVas && foundAn && foundNox ) && bag.Items.Count == 3 )
				m_SpellID = 24;

// Arch Protection: Vas Uus Sanct
			if ( ( foundVas && foundUus && foundSanct ) && bag.Items.Count == 3 )
				m_SpellID = 25;

// Curse: Des Sanct
			if ( ( foundDes && foundSanct ) && bag.Items.Count == 2 )
				m_SpellID = 26;

// Fire Field: In Flam Grav
			if ( ( foundIn && foundFlam && foundGrav ) && bag.Items.Count == 3 )
				m_SpellID = 27;

// Greater Heal: In Vas Mani
			if ( ( foundIn && foundVas && foundMani ) && bag.Items.Count == 3 )
				m_SpellID = 28;

// Lightning: Por Ort Grav
			if ( ( foundPor && foundOrt && foundGrav ) && bag.Items.Count == 3 )
				m_SpellID = 29;

// Mana Drain: Ort Rel
			if ( ( foundOrt && foundRel ) && bag.Items.Count == 2 )
				m_SpellID = 30;

// Recall: Kal Ort Por
			if ( ( foundKal && foundOrt && foundPor ) && bag.Items.Count == 3 )
				m_SpellID = 31;



/// Fifth  circle   ////////////////////////////////////////////


// Blade Spirits: In Jux Hur Ylem
			if ( ( foundIn && foundJux && foundHur && foundYlem ) && bag.Items.Count == 4 )
				m_SpellID = 32;

// Dispel Field: An Grav
			if ( ( foundAn && foundGrav ) && bag.Items.Count == 2 )
				m_SpellID = 33;

// Incognito: Kal In Ex
			if ( ( foundKal && foundIn && foundEx ) && bag.Items.Count == 3 )
				m_SpellID = 34;

// Magic Reflection: In Jux Sanct
			if ( ( foundIn && foundJux && foundSanct ) && bag.Items.Count == 3 )
				m_SpellID = 35;

// Mind Blast: Por Corp Wis
			if ( ( foundPor && foundCorp && foundWis ) && bag.Items.Count == 3 )
				m_SpellID = 36;

// Paralyze: An Ex Por
			if ( ( foundAn && foundEx && foundPor ) && bag.Items.Count == 3 )
				m_SpellID = 37;

// PoisonField: In Nox Grav
			if ( ( foundIn && foundNox && foundGrav ) && bag.Items.Count == 3 )
				m_SpellID = 38;

// Summon Creature: Kal Xen
			if ( ( foundKal && foundXen ) && bag.Items.Count == 2 )
				m_SpellID = 39;



/// Sixth  circle   ////////////////////////////////////////////


// Dispel: An Ort
			if ( ( foundAn && foundOrt ) && bag.Items.Count == 2 )
				m_SpellID = 40;

// Eenrgy Bolt: Corp Por
			if ( ( foundCorp && foundPor ) && bag.Items.Count == 2 )
				m_SpellID = 41;

// Explosion: Vas Ort Flam
			if ( ( foundVas && foundOrt && foundFlam ) && bag.Items.Count == 3 )
				m_SpellID = 42;

// Invisibility: An Lor Xen
			if ( ( foundAn && foundLor && foundXen ) && bag.Items.Count == 3 )
				m_SpellID = 43;

// Mark: Kal Por Ylem
			if ( ( foundKal && foundPor && foundYlem ) && bag.Items.Count == 3 )
				m_SpellID = 44;

// Mass Curse: Vas Des Sanct
			if ( ( foundVas && foundDes && foundSanct ) && bag.Items.Count == 3 )
				m_SpellID = 45;

// Paralyze Field: In Ex Grav
			if ( ( foundIn && foundEx && foundGrav ) && bag.Items.Count == 3 )
				m_SpellID = 46;

// Reveal: Wis Quas
			if ( ( foundWis && foundQuas ) && bag.Items.Count == 2 )
				m_SpellID = 47;



/// Seventh  circle   ////////////////////////////////////////////


// Chain Lightning: Vas Ort Grav
			if ( ( foundVas && foundOrt && foundGrav ) && bag.Items.Count == 3 )
				m_SpellID = 48;

// Energy Field: In Sanct Grav
			if ( ( foundIn && foundSanct && foundGrav ) && bag.Items.Count == 3 )
				m_SpellID = 49;

// Flame Strike: Kal Vas Flam
			if ( ( foundKal && foundVas && foundFlam ) && bag.Items.Count == 3 )
				m_SpellID = 50;

// Gate Travel: Vas Rel Por
			if ( ( foundVas && foundRel && foundPor ) && bag.Items.Count == 3 )
				m_SpellID = 51;

// Mana Vampire: Ort Sanct
			if ( ( foundOrt && foundSanct ) && bag.Items.Count == 2 )
				m_SpellID = 52;

// Mass Dispel: Vas An Ort
			if ( ( foundVas && foundAn && foundOrt ) && bag.Items.Count == 3 )
				m_SpellID = 53;

// Meteor Swarm: Flam Kal Des Ylem
			if ( ( foundFlam && foundKal && foundDes && foundYlem ) && bag.Items.Count == 4 )
				m_SpellID = 54;

// Polymorph: Vas Ylem Rel
			if ( ( foundVas && foundYlem && foundRel ) && bag.Items.Count == 3 )
				m_SpellID = 55;


// Captain the First was the First one to code the 8th circle spells and publish them!
/// Eighth  circle   //////////////////////////////////////////// 

// "Earthquake", "In Vas Por" 
         if ( ( foundIn && foundVas && foundPor ) && bag.Items.Count == 3 ) 
            m_SpellID = 56; 

// "Energy Vortex", "Vas Corp Por" 
         if ( ( foundVas && foundCorp && foundPor ) && bag.Items.Count == 3 ) 
            m_SpellID = 57; 

// "Resurrection", "An Corp" 
         if ( ( foundAn && foundCorp ) && bag.Items.Count == 2 ) 
            m_SpellID = 58; 

// "Air Elemental", "Kal Vas Xen Hur" 
         if ( ( foundKal && foundVas && foundXen && foundHur ) && bag.Items.Count ==  4 ) 
            m_SpellID = 59; 

// "Summon Daemon", "Kal Vas Xen Corp" 
         if ( ( foundKal && foundVas && foundXen && foundCorp ) && bag.Items.Count ==  4 ) 
            m_SpellID = 60; 

// "Earth Elemental", "Kal Vas Xen Ylem" 
         if ( ( foundKal && foundVas && foundXen && foundYlem ) && bag.Items.Count ==  4 ) 
            m_SpellID = 61; 

// "Fire Elemental", "Kal Vas Xen Flam" 
         if ( ( foundKal && foundVas && foundXen && foundFlam ) && bag.Items.Count ==  4 ) 
            m_SpellID = 62; 

// "Water Elemental", "Kal Vas Xen An Flam" 
         if ( ( foundKal && foundVas && foundXen && foundAn && foundFlam ) && bag.Items.Count ==  5 ) 
            m_SpellID = 63; 

//FIST Start Necromancy Spells

// "Curse Weapon", An Sanct Grav Corp
		 if ( ( foundAn && foundSanct && foundGrav && foundCorp ) && bag.Items.Count == 4 )
			 m_SpellID = 103;
// "Blood Oath", In Jux Mani Xen
		 if ( ( foundIn && foundJux && foundMani && foundXen ) && bag.Items.Count == 4 )
			 m_SpellID = 101;
// "Corpse Skin", In An Corp Ylem
		 if ( ( foundIn && foundAn && foundCorp && foundYlem ) && bag.Items.Count == 4 )
			 m_SpellID = 102;
// "Evil Omen", Por Tym An Sanct
		 if ( ( foundPor && foundTym && foundAn && foundSanct ) && bag.Items.Count == 4 )
			 m_SpellID = 104;
// "Pain Spike", In Sanct
		 if ( ( foundIn && foundSanct ) && bag.Items.Count == 2 )
			 m_SpellID = 108;
// "Wraith Form", Rel Xen Uus
		 if ( ( foundRel && foundXen && foundUus ) && bag.Items.Count == 3 )
			 m_SpellID = 115;
// "Mind Rot", Wis An Bet
		 if ( ( foundAn && foundWis && foundBet ) && bag.Items.Count == 3 )
			 m_SpellID = 107;
// "Summon Familiar, Kal Xen Bet
		 if ( ( foundKal && foundXen && foundBet ) && bag.Items.Count == 3 )
			 m_SpellID = 111;
// "Animate Dead", Uus Corp
		 if ( ( foundUus && foundCorp ) && bag.Items.Count == 2 )
			 m_SpellID = 100;
// "Horrific Beast, Rel Xen Vas Bet
		 if ( ( foundRel && foundXen && foundVas && foundBet ) && bag.Items.Count == 4 )
			 m_SpellID = 105;
// "Poison Strike", In Vas Nox
		 if ( ( foundIn && foundVas && foundNox ) && bag.Items.Count == 3 )
			 m_SpellID = 109;
// "Wither", Kal Vas An Flam
		 if ( ( foundKal && foundVas && foundAn && foundFlam ) && bag.Items.Count == 4 )
			 m_SpellID = 114;
// "Strangle", In Bet Nox
		 if ( ( foundIn && foundBet && foundNox ) && bag.Items.Count == 3 )
			 m_SpellID = 110;
// "Lich Form", Rel Xen Corp Ort
		 if ( ( foundRel && foundXen && foundCorp && foundOrt ) && bag.Items.Count == 4 )
			 m_SpellID = 106;
// "Exorcism", Ort Corp Grav
		 if ( ( foundOrt && foundCorp && foundGrav ) && bag.Items.Count == 3 )
			 m_SpellID = 116;
// "Vengeful Spirit", Kal Xen Bet Bet
		 if ( ( foundKal && foundXen && foundBet ) && bag.Items.Count == 4 )//FIST Should say for as Bet is found, but need 2 of them. May work with any other rune as well...
			 m_SpellID = 113;
// "Vampiric Embrace", Rel Xen An Sanct
		 if ( ( foundRel && foundXen && foundAn && foundSanct ) && bag.Items.Count == 4 )
			 m_SpellID = 112;
		 
/// end spells /////////////////////////////////////

         if ( foundBet || foundTym || foundZu )  // currently unused.
		m.SendMessage( "One of the runestones feels cold." );

/// end spells /////////////////////////////////////


/// begin spellcasting /////////////////////////////


			if ( !Multis.DesignContext.Check( m ) )
				return; // They are customizing

			if ( !IsChildOf( m.Backpack ) )
			{
				m.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
				return;
			}

			if ( m_SpellID == -1 )
			{
				m.SendMessage( "Not a known spell" );
				return;
			}

			Spell spell = SpellRegistry.NewSpell( m_SpellID, m, this );

			if ( spell != null )
			{
				spell.Cast();
// this should delete the runes after casting, if you want to do that.
// code snippet 'borrowed' from baseboard =D
//
//				for ( int i = bag.Items.Count - 1; i >= 0; --i )
//				{
//					if ( i < bag.Items.Count )
//						((Item)Items[i]).Delete();
//				}
			}
			else
				m.SendLocalizedMessage( 502345 ); // This spell has been temporarily disabled.
		}

		public class RuneBagMenu : ContextMenuEntry 
		{ 
			private RuneBag i_RuneBag; 
			private Mobile m_From; 

			public RuneBagMenu( Mobile from, RuneBag bag ) : base( 6122, 1 ) 
			{ 
				m_From = from; 
				i_RuneBag = bag; 
			} 

			public override void OnClick() 
			{          
				if( i_RuneBag.IsChildOf( m_From.Backpack ) ) 
				{ 
					i_RuneBag.Open( m_From );
				} 
				else 
				{
					m_From.SendMessage( "This must be in your backpack to look through." );
				} 
			} 
		} 

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list ) 
		{
			base.GetContextMenuEntries( from, list );

			if ( from.Alive )
				list.Add( new RuneBagMenu( from, this ) );
		}

		public override void OnDoubleClick( Mobile from )
		{
			if( this.IsChildOf( from.Backpack ) ) 
			{ 
				this.RuneBagCast( from, this );
			}
			else
			{
				from.SendMessage( "This must be in your backpack to look through." );
			}
		}

		public RuneBag( Serial serial ) : base( serial )
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


///  Full Rune Bag ///////////////////////////////////////////

	public class FullRuneBag : Bag
	{
		[Constructable]
		public FullRuneBag() : base()
		{
			Name = "Full Rune Bag";
			Hue = Utility.RandomMetalHue();

			PlaceItemIn( this, 30, 35, new An()	);
			PlaceItemIn( this, 40, 35, new Bet()	);
			PlaceItemIn( this, 50, 35, new Corp()	);
			PlaceItemIn( this, 60, 35, new Des()	);
			PlaceItemIn( this, 70, 35, new Ex()	);
			PlaceItemIn( this, 80, 35, new Flam()	);
			PlaceItemIn( this, 90, 35, new Grav()	);

			PlaceItemIn( this, 30, 50, new Hur()	);
			PlaceItemIn( this, 40, 50, new In()	);
			PlaceItemIn( this, 50, 50, new Jux()	);
			PlaceItemIn( this, 60, 50, new Kal()	);
			PlaceItemIn( this, 70, 50, new Lor()	);
			PlaceItemIn( this, 80, 50, new Mani()	);
			PlaceItemIn( this, 90, 50, new Nox()	);

			PlaceItemIn( this, 30, 65, new Ort()	);
			PlaceItemIn( this, 40, 65, new Por()	);
			PlaceItemIn( this, 50, 65, new Quas()	);
			PlaceItemIn( this, 60, 65, new Rel()	);
			PlaceItemIn( this, 70, 65, new Sanct()	);
			PlaceItemIn( this, 80, 65, new Tym()	);
			PlaceItemIn( this, 90, 65, new Uus()	);

			PlaceItemIn( this, 30, 80, new Vas()	);
			PlaceItemIn( this, 40, 80, new Wis()	);
			PlaceItemIn( this, 50, 80, new Xen()	);
			PlaceItemIn( this, 60, 80, new Ylem()	);
			PlaceItemIn( this, 70, 80, new Zu()	);

			PlaceItemIn( this, 70, 100, new RuneMagicBook() );
			PlaceItemIn( this, 90, 100, new RuneBag() );
		}

		private static void PlaceItemIn( Container parent, int x, int y, Item item )
		{
			parent.AddItem( item );
			item.Location = new Point3D( x, y, 0 );
		}

		public FullRuneBag( Serial serial ) : base( serial )
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


///////////// Runestones ///////////////

/// base runestone

	public abstract class RuneStone : Item
	{
		public RuneStone() : base( 0x1F15 )
		{
			Name = "rune stone";
			Weight = 0.01;
			ItemID = 0x1F14;
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Magic Rune Stone");
        }

		public RuneStone( Serial serial ) : base( serial )
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


///////////////////////////////////////////////////
/// end base runestone. Custom runestones below ///
///////////////////////////////////////////////////

	public class An : RuneStone
	{
		[Constructable]
		public An() : base()
		{
			Name = "An";
			ItemID = 0x2379;
		}
	
		public An( Serial serial ) : base( serial )
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

	public class Bet : RuneStone
	{
		[Constructable]
		public Bet() : base()
		{
			Name = "Bet";
			ItemID = 0x237A;
		}
	
		public Bet( Serial serial ) : base( serial )
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

	public class Corp : RuneStone
	{
		[Constructable]
		public Corp() : base()
		{
			Name = "Corp";
			ItemID = 0x237B;
		}
	
		public Corp( Serial serial ) : base( serial )
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

	public class Des : RuneStone
	{
		[Constructable]
		public Des() : base()
		{
			Name = "Des";
			ItemID = 0x237C;
		}
	
		public Des( Serial serial ) : base( serial )
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

	public class Ex : RuneStone
	{
		[Constructable]
		public Ex() : base()
		{
			Name = "Ex";
			ItemID = 0x237D;
		}
	
		public Ex( Serial serial ) : base( serial )
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


	public class Flam : RuneStone
	{
		[Constructable]
		public Flam() : base()
		{
			Name = "Flam";
			ItemID = 0x2387;
		}
	
		public Flam( Serial serial ) : base( serial )
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

	public class Grav : RuneStone
	{
		[Constructable]
		public Grav() : base()
		{
			Name = "Grav";
			ItemID = 0x2389;
		}
	
		public Grav( Serial serial ) : base( serial )
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

	public class Hur : RuneStone
	{
		[Constructable]
		public Hur() : base()
		{
			Name = "Hur";
			ItemID = 0x238A;
		}
	
		public Hur( Serial serial ) : base( serial )
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

	public class In : RuneStone
	{
		[Constructable]
		public In() : base()
		{
			Name = "In";
			ItemID = 0x2393;
		}
	
		public In( Serial serial ) : base( serial )
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

	public class Jux : RuneStone
	{
		[Constructable]
		public Jux() : base()
		{
			Name = "Jux";
			ItemID = 0x2394;
		}
	
		public Jux( Serial serial ) : base( serial )
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

	public class Kal : RuneStone
	{
		[Constructable]
		public Kal() : base()
		{
			Name = "Kal";
			ItemID = 0x2395;
		}
	
		public Kal( Serial serial ) : base( serial )
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

	public class Lor : RuneStone
	{
		[Constructable]
		public Lor() : base()
		{
			Name = "Lor";
			ItemID = 0x2396;
		}
	
		public Lor( Serial serial ) : base( serial )
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

	public class Mani : RuneStone
	{
		[Constructable]
		public Mani() : base()
		{
			Name = "Mani";
			ItemID = 0x237E;
		}

		public Mani( Serial serial ) : base( serial )
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

	public class Nox : RuneStone
	{
		[Constructable]
		public Nox() : base()
		{
			Name = "Nox";
			ItemID = 0x238B;
		}
	
		public Nox( Serial serial ) : base( serial )
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

	public class Ort : RuneStone
	{
		[Constructable]
		public Ort() : base()
		{
			Name = "Ort";
			ItemID = 0x2398;
		}
	
		public Ort( Serial serial ) : base( serial )
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

	public class Por : RuneStone
	{
		[Constructable]
		public Por() : base()
		{
			Name = "Por";
			ItemID = 0x237F;
		}
	
		public Por( Serial serial ) : base( serial )
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

	public class Quas : RuneStone
	{
		[Constructable]
		public Quas() : base()
		{
			Name = "Quas";
			ItemID = 0x2380;
		}
	
		public Quas( Serial serial ) : base( serial )
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

	public class Rel : RuneStone
	{
		[Constructable]
		public Rel() : base()
		{
			Name = "Rel";
			ItemID = 0x2381;
		}
	
		public Rel( Serial serial ) : base( serial )
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

	public class Sanct : RuneStone
	{
		[Constructable]
		public Sanct() : base()
		{
			Name = "Sanct";
			ItemID = 0x2382;
		}
	
		public Sanct( Serial serial ) : base( serial )
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

	public class Tym : RuneStone
	{
		[Constructable]
		public Tym() : base()
		{
			Name = "Tym";
			ItemID = 0x2383;
		}
	
		public Tym( Serial serial ) : base( serial )
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

	public class Uus : RuneStone
	{
		[Constructable]
		public Uus() : base()
		{
			Name = "Uus";
			ItemID = 0x2384;
		}
	
		public Uus( Serial serial ) : base( serial )
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

	public class Vas : RuneStone
	{
		[Constructable]
		public Vas() : base()
		{
			Name = "Vas";
			ItemID = 0x2385;
		}
	
		public Vas( Serial serial ) : base( serial )
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

	public class Wis : RuneStone
	{
		[Constructable]
		public Wis() : base()
		{
			Name = "Wis";
			ItemID = 0x2399;
		}
	
		public Wis( Serial serial ) : base( serial )
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

	public class Xen : RuneStone
	{
		[Constructable]
		public Xen() : base()
		{
			Name = "Xen";
			ItemID = 0x239C;
		}
	
		public Xen( Serial serial ) : base( serial )
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

	public class Ylem : RuneStone
	{
		[Constructable]
		public Ylem() : base()
		{
			Name = "Ylem";
			ItemID = 0x239D;
		}
	
		public Ylem( Serial serial ) : base( serial )
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

	public class Zu : RuneStone
	{
		[Constructable]
		public Zu() : base()
		{
			Name = "Zu";
			ItemID = 0x239E;
		}
	
		public Zu( Serial serial ) : base( serial )
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

/// rune magic book ^.^
/// alter to fit the RP and world setting of your shard, obviously.
/// This is just a very generic basis to work with.

	public class RuneMagicBook : BlueBook
	{
		[Constructable]
		public RuneMagicBook() : base( "Rune Magic", "Garamon", 50, true ) // writable so players can make notes
		{
			Hue = RandomThings.GetRandomColor(0);
			ItemID = 0x2255;

			// NOTE: There are 8 lines per page and 
			// approx 22 to 24 characters per line! 
			//	0----+----1----+----2----+ 
			int cnt = 0; 
			string[] lines; 
			lines = new string[] 
			{ 
				"     Rune Magic", 
				"     by Garamon", 
				"", 
				"With reagents being rare", 
				"in the Abyss, I began to", 
				"research other ways to",
				"cast magery spells. I",
				"have found various old", 
			}; 
			Pages[cnt++].Lines = lines;

			lines = new string[] 
			{ 
				"stone tablets here that", 
				"describe the use of rune", 
				"stones in this manner.", 
				"One must find a rune of", 
				"marked with the symbols", 
				"needed to speak the", 
				"mantra for the spell.", 
				"Once the correct ones", 
			}; 
			Pages[cnt++].Lines = lines;

			lines = new string[] 
			{ 
				"are assembled they must", 
				"be placed in a magical", 
				"rune bag where one can", 
				"then use the sack to", 
				"unleash the magic power", 
				"of the spell. This is", 
				"by no means a simple", 
				"process, as gathering", 
			}; 
			Pages[cnt++].Lines = lines;

			lines = new string[] 
			{ 
				"the runes can be quite", 
				"tedious, but it is a", 
				"way to cast spells in", 
				"a pinch. The runes and", 
				"bag seem to bind with", 
				"the caster as I thought", 
				"I lost them at one", 
				"point, but they seemed", 
			}; 
			Pages[cnt++].Lines = lines;

			lines = new string[] 
			{ 
				"to have come back to me", 
				"as if magically. Though", 
				"I could lose my spell", 
				"book and reagents, the", 
				"runes allow me to still", 
				"work with spells. I", 
				"have been searching for", 
				"a spell to summon a", 
			}; 
			Pages[cnt++].Lines = lines;

			lines = new string[] 
			{ 
				"daemon for years now.", 
				"I have already found the", 
				"runes that will allow me", 
				"to cast such a spell", 
				"without the need of a", 
				"rare scroll. Many mages", 
				"scoff at the use of", 
				"runes, but to me they", 
			}; 
			Pages[cnt++].Lines = lines;

			lines = new string[] 
			{ 
				"are becoming a valuable", 
				"arcana that I have not", 
				"been able to do without.", 
				"I will attempt to write", 
				"my findings on these", 
				"ancient ways to cast", 
				"magic spells so others", 
				"may one day benefit.", 
			}; 
			Pages[cnt++].Lines = lines;

			lines = new string[] 
			{ 
				"     Rune Magic", 
				"     by Garamon", 
				"", 
				"The following is all of", 
				"my research on rune", 
				"magic, the known spells,",
				"and the rune symbols.",
				"", 
			}; 
			Pages[cnt++].Lines = lines;

			lines = new string[] 
			{ 
				"     Rune Bags", 
				"", 
				"Rune bags and runes are", 
				"imbued with the power to", 
				"assist the caster in the", 
				"casting of a spell without", 
				"the need of reagents.", 
				"", 
			}; 
			Pages[cnt++].Lines = lines;

			lines = new string[] 
			{ 
				"Place one of each", 
				"required rune stone", 
				"inside the rune bag", 
				"by opening the bag.", 
				"[click on the bag]", 
				"and concentrate on the", 
				"incantation of the spell.", 
				"[double click the bag]", 
			}; 
			Pages[cnt++].Lines = lines;

			lines = new string[] 
			{ 
				"Following is a complete", 
				"list of all known spells", 
				"and the runes needed to", 
				"cast them.", 
				"Note that even with the", 
				"runes, a mage must still", 
				"have the will and power", 
				"to cast the spell.", 
			}; 
			Pages[cnt++].Lines = lines;

			lines = new string[] 
			{ 
				"First Circle", 
				" Clumsy", 
				"    In Jux", 
				"",
				"",
				" Create Food", 
				"    In Mani Ylem", 
				"", 
			}; 
			Pages[cnt++].Lines = lines;

			lines = new string[] 
			{ 
				"", 
				" Feeblemind", 
				"    Rel Wis", 
				"",
				"",
				" Heal", 
				"    In Mani", 
				"", 
			}; 
			Pages[cnt++].Lines = lines;

			lines = new string[] 
			{ 
				"", 
				" Magic Arrow", 
				"    In Por Ylem", 
				"",
				"",
				" Night Sight", 
				"    In Lor", 
				"", 
			}; 
			Pages[cnt++].Lines = lines;

			lines = new string[] 
			{ 
				"", 
				" Reactive Armor", 
				"    Flam Sanct", 
				"",
				"", 
				" Weaken", 
				"    Des Mani", 
				"", 
			}; 
			Pages[cnt++].Lines = lines;

			lines = new string[] 
			{ 
				"  Meanings of Runes", 
				"", 
				"An   - Negate/Dispel", 
				"Bet  - Small", 
				"Corp - Death", 
				"Des  - Lower/Down", 
				"Ex   - Freedom", 
				"Flam - Flame", 
			}; 
			Pages[cnt++].Lines = lines; 

			lines = new string[] 
			{ 
				"Grav - Energy/Field", 
				"Hur  - Wind", 
				"In   - Make/Create/Cause", 
				"Jux  - Danger/Trap/Harm", 
				"Kal  - Summon/Invoke", 
				"Lor  - Light", 
				"Mani - Life/Healing", 
				"Nox  - Poison", 
			}; 
			Pages[cnt++].Lines = lines;

			lines = new string[] 
			{ 
				"Ort  - Magic", 
				"Por  - Move/Movement", 
				"Quas - Illusion", 
				"Rel  - Change", 
				"Sanct- Protection", 
				"Tym  - Time", 
				"Uus  - Raise/Up", 
				"Vas  - Great", 
			}; 
			Pages[cnt++].Lines = lines;

			lines = new string[] 
			{ 
				"Wis  - Knowledge", 
				"Xen  - Creature", 
				"Ylem - Matter", 
				"Zu   - Sleep", 
				"", 
				"Runes must be used in", 
				"combinations to form", 
				"spells of power!", 
			}; 
			Pages[cnt++].Lines = lines;

			lines = new string[] 
			{ 
				"", 
				"The meanings are the key!", 
				"", 
				"", 
				"The following pages have", 
				"been left blank for thee", 
				"to take thy notes here.", 
				"", 
			}; 
			Pages[cnt++].Lines = lines;

			lines = new string[] 
			{ 
				"", 
				"Go forth to learn", 
				"the other spells.", 
				"", 
				"Best of luck in", 
				"thy experiments!", 
				"", 
				" - Garamon", 

			}; 
			Pages[cnt++].Lines = lines;
		}

		public RuneMagicBook( Serial serial ) : base( serial )
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

	public class RuneBagBook : RuneMagicBook
	{
		[Constructable]
		public RuneBagBook() : base()
		{
		}

		public RuneBagBook( Serial serial ) : base( serial )
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
