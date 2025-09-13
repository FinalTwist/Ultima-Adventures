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
	public class Spammer : BaseCreature, IOneTime
	{

		private int m_OneTimeType;
        public int OneTimeType
        {
            get{ return m_OneTimeType; }
            set{ m_OneTimeType = value; }
        }
		public override bool NoHouseRestrictions{ get{ return true; } }
		public override bool AllowEquipFrom( Mobile from ){ return m_Owner == from; }
		public override bool CanBeDamaged(){ return true; }
		public override bool CanBeRenamedBy( Mobile from ){ return m_Owner == from; }
		public override bool CanPaperdollBeOpenedBy(Mobile from){ return true; }

		public Mobile m_Owner;
		public Direction m_Direction;

		private string m_spam;

		private int m_tick;
		private int m_moneytick;


		[Constructable]
		public Spammer( Mobile owner ) : base( AIType.AI_Use_Default, FightMode.None, 1, 1, 0.2, 0.2 )
		{
			m_Owner = owner;
			
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

			switch ( Utility.Random( 3 ) )
			{
				case 0: Server.Misc.IntelligentAction.DressUpWizards( this ); 		break;
				case 1: Server.Misc.IntelligentAction.DressUpFighters( this, "", false, 0 );	break;
				case 2: Server.Misc.IntelligentAction.DressUpRogues( this, "", false, 0, "" );			break;
			}
SpeechHue = Server.Misc.RandomThings.GetSpeechHue();
			CantWalk = true;

			SetStr( 100 );
			SetDex( 100 );
			SetInt( 100 );

			SetDamage( 1 );

			Fame = 0;
			Karma = 0;

			m_spam = "";
			m_tick = 0;
			m_OneTimeType = 3;	
			m_moneytick = 0;
		}

		public override void GenerateLoot()
		{
		}

		public Spammer( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			writer.Write( m_Owner );
			writer.Write( m_spam );
			writer.Write( m_moneytick);
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			m_Owner = reader.ReadMobile();
			m_spam = reader.ReadString();
			m_moneytick = reader.ReadInt();

			m_OneTimeType = 3;	
		}

		public override bool HandlesOnSpeech( Mobile from )
		{
			if( from.InRange( this.Location, 10 ) )
				return true;
			return base.HandlesOnSpeech( from );
		}

		public override void OnSpeech( SpeechEventArgs args )
		{
			string said = args.Speech.ToString();//args.Speech.ToLower();
			Mobile from = args.Mobile;

			if (m_spam == "" && from == m_Owner)
			{
				m_spam = said;
			}

		}

		public override void AddNameProperties( ObjectPropertyList list )
		{
			
			base.AddNameProperties( list );	
			if (m_Owner != null)
				list.Add( "This being was hired by " + m_Owner.Name ); 
			else
				list.Add( "This being was hired by an unknown person." ); 
			list.Add( "Drop more gold to increase length of time.");
			list.Add( "Double click to reset spam.");
		}


		public override void OnDoubleClick( Mobile from )
		{
			if (from == m_Owner)
				m_spam = "";

			base.OnDoubleClick(from);
		}

		public override bool OnDragDrop( Mobile from, Item dropped )
		{
			Container pack = this.Backpack;

			if ( dropped is Gold)
			{
				pack.DropItem(dropped);
				Say("Yes m'Lord, I'll stay here longer.");
				return true;
			}
			return base.OnDragDrop(from, dropped);
		}



		public override void OnAfterSpawn()
		{
			this.Say("Aye M'lord, I shall stand here and speak thine words diligently.");
			this.Say("I will charge 500 gold a day, you may pay me as much as you like. ");

			base.OnAfterSpawn();
		}

        public void OneTimeTick()
        {
			if (m_spam == "" && m_tick == 4)
			{
				this.Say("Please speak the words you wish me to say.");
			}
			if (m_tick <= Utility.RandomMinMax(5, 10))
				m_tick ++;
			else 
			{
				this.Yell(m_spam);
				m_tick = 0;

			}
			if (m_moneytick <= 1440)
				m_moneytick ++;
			else 
			{
				Container backpack = this.Backpack;
				if (backpack != null) 
				{
					Gold gold = (Gold)backpack.FindItemByType(typeof(Gold));
					if (gold != null) 
					{
						gold.Amount -= 500;
						if (gold.Amount <= 0 || gold == null)
							this.Delete();
					}
					else
						this.Delete();
				}
			}
		}
	}
}