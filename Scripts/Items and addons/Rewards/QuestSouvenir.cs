using System;
using Server;
using System.Collections;
using System.Collections.Generic;
using Server.Items;
using Server.Misc;
using Server.Network;
using Server.Commands;
using Server.Commands.Generic;
using Server.Mobiles;
using Server.Accounting;

namespace Server.Items
{
	public class QuestSouvenir : Item
	{
		public Mobile SouvenirMobile;

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Souvenir_Mobile { get{ return SouvenirMobile; } set{ SouvenirMobile = value; } }

		public string SouvenirOwner;
		
		[CommandProperty(AccessLevel.Owner)]
		public string Souvenir_Owner { get { return SouvenirOwner; } set { SouvenirOwner = value; InvalidateProperties(); } }

		[Constructable]
		public QuestSouvenir() : base( 0x1C14 )
		{
			Hue = 0;
			Name = "souvenir";
			Weight = 1.0;
		}

		private static int[] m_Sounds = new int[] { 0x505, 0x506, 0x507 };

		public override void OnDoubleClick( Mobile from )
		{
			if ( this.Name == "Bell of Courage" )
			{
				from.PlaySound( m_Sounds[Utility.Random( m_Sounds.Length )] );
				from.SendMessage( "You ring the bell, producing a courageous melody." );
			}
			else if ( this.Name == "Candle of Love" )
			{
				from.SendMessage( "You feel the loving warmth of the flame." );
			}
			else if ( this.Name == "Book of Truth" )
			{
				from.SendMessage( "You learn a little bit more about the principles of truth." );
			}
			else if ( this.Name == "Scales of Ethicality" )
			{
				from.SendMessage( "You scale seems to weigh the ethics of the situation." );
			}
			else if ( this.Name == "Orb of Logic" )
			{
				from.SendMessage( "You feel a strong sense of logic from the orb." );
			}
			else if ( this.Name == "Lantern of Discipline" )
			{
				from.SendMessage( "The lantern glows with a disciplined light." );
			}
			else if ( this.ItemID == 0x1A7F )
			{
				this.ItemID = 0x1A80;
			}
			else if ( this.ItemID == 0x1A80 )
			{
				this.ItemID = 0x1A7F;
			}
			else if ( this.ItemID > 19512 && this.ItemID < 19539 )
			{
				from.SendMessage( "This cube holds immense magical power." );
			}
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Souvenir");
            list.Add( 1049644, SouvenirOwner);
        } 

		public QuestSouvenir( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			writer.Write( (Mobile)SouvenirMobile );
            writer.Write( SouvenirOwner );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			SouvenirMobile = reader.ReadMobile();
            SouvenirOwner = reader.ReadString();

			if ( Name == "Candle of Love" ){ Light = LightType.Circle150; }
			else if ( Name == "Lantern of Discipline" ){ Light = LightType.Circle150; }
			else if ( Name == "Orb of Logic" ){ Light = LightType.Circle150; }
			else if ( ItemID > 19512 && ItemID < 19539 ){ Light = LightType.Circle150; }
		}

		public static void GiveReward( Mobile from, string name, int hue, int id )
		{
			if ( from is PlayerMobile )
			{
				ArrayList targets = new ArrayList();
				foreach ( Item item in World.Items.Values )
				if ( item is QuestSouvenir && item.Name == name )
				{
					if ( ((QuestSouvenir)item).SouvenirMobile == from )
						targets.Add( item );
				}
				for ( int i = 0; i < targets.Count; ++i )
				{
					Item item = ( Item )targets[ i ];
					item.Delete();
				}

				QuestSouvenir reward = new QuestSouvenir();

				reward.Name = name;
				reward.Hue = hue;
				reward.ItemID = id;
				reward.SouvenirMobile = from;
				reward.SouvenirOwner = "Given to " + from.Name;

				if ( name == "Candle of Love" ){ reward.Light = LightType.Circle150; }
				else if ( name == "Lantern of Discipline" ){ reward.Light = LightType.Circle150; }
				else if ( name == "Orb of Logic" ){ reward.Light = LightType.Circle150; }
				else if ( id > 19512 && id < 19539 ){ reward.Light = LightType.Circle150; }
				else if ( id >= 0x530C && id <= 0x531B ){ reward.Light = LightType.Circle150; }

				from.AddToBackpack( reward );
			}
		}
	}
}