using System;
using System.Collections;
using Server.Items;
using Server.ContextMenus;
using Server.Misc;
using Server.Network;
using Server.Gumps;
using Server.OneTime;

namespace Server.Mobiles
{
	public class Protester : BaseCreature, IOneTime
	{

		private int m_OneTimeType;
        public int OneTimeType
        {
            get{ return m_OneTimeType; }
            set{ m_OneTimeType = value; }
        }
		public override bool NoHouseRestrictions{ get{ return true; } }
		public override bool CanBeDamaged(){ return true; }


		private int m_tick;




		[Constructable]
		public Protester(  ) : base( AIType.AI_Use_Default, FightMode.None, 1, 1, 0.2, 0.2 )
		{
			
			Title = "";
			NameHue = 1150;
			if( Utility.RandomBool() )
			{
				Body = 401;
				Name = NameList.RandomName("female");
			}
			else
			{
				Body = 400;
				Name = NameList.RandomName("male");
			}
SpeechHue = Server.Misc.RandomThings.GetSpeechHue();
			switch ( Utility.Random( 3 ) )
			{
				case 0: Server.Misc.IntelligentAction.DressUpWizards( this ); 		break;
				case 1: Server.Misc.IntelligentAction.DressUpFighters( this, "", false, 0 );	break;
				case 2: Server.Misc.IntelligentAction.DressUpRogues( this, "", false, 0, "" );			break;
			}

			CantWalk = true;

			SetStr( 100 );
			SetDex( 100 );
			SetInt( 100 );

			SetDamage( 1 );

			Fame = 0;
			Karma = 0;

			m_tick = 0;
			m_OneTimeType = 3;	
		}

		public override void GenerateLoot()
		{
		}

		public Protester( Serial serial ) : base( serial )
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

			m_OneTimeType = 3;	
		}


		public override void AddNameProperties( ObjectPropertyList list )
		{
			
			base.AddNameProperties( list );	

			list.Add( "This person seems unhappy about something." ); 

		}



		public override bool OnDragDrop( Mobile from, Item dropped )
		{
			return false;
		}



        public void OneTimeTick()
        {

			if (m_tick <= Utility.RandomMinMax(15, 40))
				m_tick ++;
			else 
			{
				int final = Utility.RandomMinMax(1, 21);
				if (final == 1)
					this.Yell("Opening long-dead, decomposing bodies or bones should not be a Crime!!");
				if (final == 2)
					this.Yell("The guards are too greedy! Let us also loot the bodies they kill!");
				if (final == 3)
					this.Yell("Barding is too dangerous!  Bards can provoke your own pets on you, that's not fair!");
				if (final == 4)
					this.Yell("We request that Lord British do something about the dangers of using travel spells!  Freedom of movement is a Right!");
				if (final == 5)
					this.Yell("Scribes should make spellbooks blessed!  We don't want to lose our valuable books!");
				if (final == 6)
					this.Yell("The world is littered with bones and tombstones!!  Clean Brittania!!!");
				if (final == 7)
					this.Yell("The evil side has all these advantages!!  No FAIR! Like not being able to enter most cities and being attacked by guards... oh wait nvm.");
				if (final == 8)
					this.Yell("Some traps or enemies can destroy items permanently!  No one should lose items!");
				if (final == 9)
					this.Yell("Guards attack you if your karma is too low!  SOOO Unfair!!!  Let evil people in the cities again!");
				if (final == 10)
					this.Yell("You can only see traps in a dungeon if you are careful and aren't using nightsight! No Fair I hate traps!");
				if (final == 11)
					this.Yell("Traps and mushrooms are super dangerous and can kill you! That's no good, I never want to die!");
				if (final == 12)
					this.Yell("Everything is too expensive!  I don't like having to save my gold, give me everything for free!");
				if (final == 13)
					this.Yell("Killing a shopkeeper will make someone a murderer! NO FAIR! I should be able to kill and murder at will without repercussions!");
				if (final == 14)
					this.Yell("Taking down a tent with an axe isn't obvious!  Why can't I use my hands??  I don't want to use an axe to do that!");
				if (final == 15)
					this.Yell("I hate how my pets go hungry!  As a tamer, I don't want to bother doing anything in this world while still getting all the loot!");
				if (final == 16)
					this.Yell("Vendors don't sell everything all the time!  Going to another city to find things is super lame!");
				if (final == 17)
					this.Yell("I can lose items in dungeons and other ways, I hate that!  I want to use the same sword forever and never have to change!");
				if (final == 18)
					this.Yell("My pets can turn on me if I don't feed them!  That should never happen!!111!! I think having to feed pets is TOO MUCH!");
				if (final == 19)
					this.Yell("I asked the Gods for something and didn't get what I wanted!  I think the Gods should work for me and do whatever I want!");
				if (final == 20)
					this.Yell("Things change - especially if they are SUPER POWERFUL... I Don't like change and I LOVE super powerful things, Don't change ANYTHING!");
				if (final == 21)
					this.Yell("This world has danger everywhere - That's not fair!  I should never die, never lose any items, and make gold all day everyday!");
				
				m_tick = 0;
			}
		}
	}
}