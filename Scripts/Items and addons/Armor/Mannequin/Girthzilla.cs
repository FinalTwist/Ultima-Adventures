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
	public class Girthzilla : BaseCreature, IOneTime
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
		public Girthzilla(  ) : base( AIType.AI_Use_Default, FightMode.None, 1, 1, 0.2, 0.2 )
		{
			
			Title = "";
			NameHue = 1150;
			Body = 400;
			Name = "GiRtHZIlla the L0Zer";

			SpeechHue = Server.Misc.RandomThings.GetSpeechHue();

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

		public Girthzilla( Serial serial ) : base( serial )
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

			if (m_tick <= Utility.RandomMinMax(20, 40))
				m_tick ++;
			else 
			{
				int final = Utility.RandomMinMax(1, 21);
				if (final == 1)
					this.Yell("I'm so happy to be here - this is where I belong.");
				if (final == 2)
					this.Yell("I loved it when they shoved me in there.  Another human actually touched me!");
        if (final == 3)
					this.Yell("I was called a loser once. That was the best day of my life.");
        if (final == 4)
					this.Yell("I rename myself all the time to be here, I'm so special.");
        if (final == 5)
					this.Yell("*picks nose and sucks finger*");
        if (final == 6)
					this.Yell("*sits on his own thumb and wiggles*");

				
				m_tick = 0;
			}
		}
	}
}
