// Script Package: Sleepable Beds
// Version: 1.0
// Author: Oak
// Servers: RunUO 2.0
// Date: 7/7/2006
// History: 
//  Written for RunUO 1.0 shard, Sylvan Dreams,  in February 2005. Based largely on work by David on his Sleepable NPCs scripts.
//  Modified for RunUO 2.0, removed shard specific customizations (wing layers, etc.)
//  Eni
//   - added quick recover code by Zardoz, set variable QuickRecovery to turn it on/off.
//   - changed script to implement the new hair system (sleepers have hair again)
//   - added support for players lying awake in bed
using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Engines.PartySystem;
using Server.Misc;
using Server.Guilds;
using Server.Mobiles;
using Server.Network;
using Server.ContextMenus;
using Server.Multis;

namespace Server.Items
{
	public class SleeperBedBody : Container 
	{
		private Mobile m_Owner;
		private string m_SleeperBedBodyName;	
		private bool m_Blessed;
		private Timer m_Timer;
		private List<Item> m_EquipItems;	//private ArrayList m_EquipItems;		
		private bool m_spell;
		
		// Eni stuff
		public static bool QuickRecovery = true; // enable quick recover code by Zardoz
		private bool m_AlwaysSleep;
		private bool m_Asleep;
		private SleeperBaseAddon m_Addon;
	
		// Hair stuff, old way doesnt work
		private HairInfo m_Hair;					// This contains the hair of the owner
		private FacialHairInfo m_FacialHair;		// This contains the facial hair of the owner

		public HairInfo Hair { get { return m_Hair; } }
		public FacialHairInfo FacialHair { get { return m_FacialHair; } }

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Owner
		{
			get{ return m_Owner; }
		}

		public List<Item> EquipItems
		{
			get{ return m_EquipItems; }
		}

		[Constructable] 
		public SleeperBedBody( Mobile owner, bool isSpell, bool AlwaysSleep, SleeperBaseAddon addon) : base( 0x2006 )
		{
			// Eni
			m_AlwaysSleep = AlwaysSleep;
			m_Asleep = false;
			
			Stackable = true; // To supress console warnings, stackable must be true
			Amount = owner.Body; // protocol defines that for itemid 0x2006, amount=body
			Stackable = false;
			Movable = false;

			m_Addon = addon;
			m_Owner = owner;
			Name = m_Owner.Name;
			m_SleeperBedBodyName = "a sleeping " + Name;
			Hue = m_Owner.Hue;
			Direction = m_Owner.Direction;
			m_spell = isSpell;
						// Eni - new hairstuff
			if (m_Owner.HairItemID > 0) { m_Hair = new HairInfo(m_Owner.HairItemID, m_Owner.HairHue); }
			if (m_Owner.FacialHairItemID > 0 ) { m_FacialHair = new FacialHairInfo(m_Owner.FacialHairItemID, m_Owner.FacialHairHue); }

			m_EquipItems = new List<Item>();
			AddFromLayer( m_Owner, Layer.FirstValid, ref m_EquipItems );
			AddFromLayer( m_Owner, Layer.TwoHanded, ref m_EquipItems );
			AddFromLayer( m_Owner, Layer.Shoes, ref m_EquipItems );
			AddFromLayer( m_Owner, Layer.Pants, ref m_EquipItems );
			AddFromLayer( m_Owner, Layer.Shirt, ref m_EquipItems );
			AddFromLayer( m_Owner, Layer.Helm, ref m_EquipItems );
			AddFromLayer( m_Owner, Layer.Gloves, ref m_EquipItems );
			AddFromLayer( m_Owner, Layer.Ring, ref m_EquipItems );
			AddFromLayer( m_Owner, Layer.Neck, ref m_EquipItems );
			//AddFromLayer( m_Owner, Layer.Hair, ref m_EquipItems );
			AddFromLayer( m_Owner, Layer.Waist, ref m_EquipItems );
			AddFromLayer( m_Owner, Layer.InnerTorso, ref m_EquipItems );
			AddFromLayer( m_Owner, Layer.Bracelet, ref m_EquipItems );
			//AddFromLayer( m_Owner, Layer.FacialHair, ref m_EquipItems );
			AddFromLayer( m_Owner, Layer.MiddleTorso, ref m_EquipItems );
			AddFromLayer( m_Owner, Layer.Earrings, ref m_EquipItems );
			AddFromLayer( m_Owner, Layer.Arms, ref m_EquipItems );
			AddFromLayer( m_Owner, Layer.Cloak, ref m_EquipItems );
			AddFromLayer( m_Owner, Layer.OuterTorso, ref m_EquipItems );
			AddFromLayer( m_Owner, Layer.OuterLegs, ref m_EquipItems );
			AddFromLayer( m_Owner, Layer.LastUserValid, ref m_EquipItems );
			
			if (m_AlwaysSleep) {
				m_Timer = new InternalTimer( m_Owner, this );
				m_Timer.Start();
			}
		}
				private void AddFromLayer( Mobile from, Layer layer, ref List<Item> list ) 
		{
			if( list == null )
				list = new List<Item>();

			Item worn = from.FindItemOnLayer( layer );
			if ( worn != null )
			{
				Item item = new Item(); 
				item.ItemID = worn.ItemID;
				item.Hue = worn.Hue;
				item.Layer = layer;
				DropItem( item );
				list.Add( item ); 
			}
		}

		public override void OnDoubleClick( Mobile from )
		{
			if (!from.CanSee( this ) || !from.InLOS( this ) || !from.InRange( this, 3 ) )
			{
				from.SendLocalizedMessage( 1019045 );
				return;
			}

			if (from == m_Owner) {
				if (m_AlwaysSleep) {
					from.SendMessage("Tickling won't work. You need to double click the bed to wake up!");
				}
				else
				{
					m_Asleep = !m_Asleep;
					if (m_Asleep) {
						m_Timer = new InternalTimer( m_Owner, this );
						m_Timer.Start();
						this.PublicOverheadMessage(0,m_Owner.EmoteHue, false,"*closes eyes*");
						from.SendMessage( "You close your eyes and prepare to sleep" );
					}
					else
					{
						if (m_Timer != null)
							m_Timer.Stop();
						this.PublicOverheadMessage(0,m_Owner.EmoteHue, false,"*stretches*");
						from.SendMessage( "You open your eyes but decide to stay in bed a little while longer" );
					}
				}
			}
			else
			{
				// if not the house owner or co-owner
				BaseHouse m_house = BaseHouse.FindHouseAt( from );
				if (m_house != null)
				{
					int fromLevel = 0;
					int sleeperLevel = 0;
					
					if (m_house.IsOwner(from))
						fromLevel = 3;
					else if (m_house.IsCoOwner(from))
						fromLevel = 2;
					else if (m_house.IsFriend(from))
						fromLevel = 1;
					
					if (m_house.IsOwner(m_Owner))
						sleeperLevel = 3;
					else if (m_house.IsCoOwner(m_Owner))
						sleeperLevel = 2;
					else if (m_house.IsFriend(m_Owner))
						sleeperLevel = 1;
						
					if ((fromLevel > sleeperLevel) || (fromLevel == sleeperLevel && fromLevel > 1))
					{
						from.LocalOverheadMessage( MessageType.Regular, from.SpeechHue, true, "You force them out of their bed" );
						if (m_Addon != null && m_Owner != null)
							m_Addon.UseBed( m_Owner, new Point3D(0,0,0), m_Owner.Direction );
							
						return;
					}
				}

				if (m_AlwaysSleep) {
					from.LocalOverheadMessage( MessageType.Regular, from.SpeechHue, true, "You decide not to bother them" );
				}
				else
				{
					if (m_Asleep)
						from.LocalOverheadMessage( MessageType.Regular, from.SpeechHue, true, "You decide to let them sleep" );
					else
						from.LocalOverheadMessage( MessageType.Regular, from.SpeechHue, true, "They're already awake!" );
				}
			}
		}

		public override bool OnDragDrop( Mobile from, Item dropped )
		{
			from.SendLocalizedMessage( 1005468 ); // Me Sleepy.

			return false;
		}

		public override bool OnDragDropInto( Mobile from, Item item, Point3D p )
		{
			from.SendLocalizedMessage( 1005468 ); // Me Sleepy.

			return false;
		}
      
		public override bool CheckContentDisplay( Mobile from )
		{
			return false;
		}

		public override bool DisplaysContent{ get{ return false; } }

		public override void OnAfterDelete()
		{
			if ( m_Timer != null )
				m_Timer.Stop();

			m_Timer = null;

			for( int i = 0; i < m_EquipItems.Count; i++ )
			{
				object o = m_EquipItems[i];
				if( o != null && o is Item )
				{
					Item item = (Item)o;
					item.Delete();
				}
			}

			base.OnAfterDelete();
		}
		
		public SleeperBedBody( Serial serial ) : base( serial )
		{
		}

		public override void SendInfoTo( NetState state, bool sendOplPacket )
		{
			base.SendInfoTo( state, sendOplPacket );

			if ( ItemID == 0x2006 )
			{
				state.Send( new SleeperBedBodyContent( state.Mobile, this ) );
				state.Send( new SleeperBedBodyEquip( state.Mobile, this ) );
			}
		}

		public override void AddNameProperty( ObjectPropertyList list )
		{
			if ( m_SleeperBedBodyName != null )
				list.Add( m_SleeperBedBodyName );
			else
				list.Add( 1049644, String.Format( "a sleeping {0}", Name ) );
		}

		public override void OnSingleClick( Mobile from )
		{
			if (m_AlwaysSleep)
			{
				LabelTo( from, m_SleeperBedBodyName == null ? String.Format( "a sleeping {0}", Name ) : m_SleeperBedBodyName );
			}
			else
			{
				if (m_Asleep)
					LabelTo( from, m_SleeperBedBodyName == null ? String.Format( "a sleeping {0}", Name ) : m_SleeperBedBodyName );
				else
					LabelTo( from, Name );
			}
		}
			
		public static string GetBodyName( Mobile m )
		{
			Type t = m.GetType();

			object[] attrs = t.GetCustomAttributes( typeof( SleeperBedNameAttribute ), true );

			if ( attrs != null && attrs.Length > 0 )
			{
				SleeperBedNameAttribute attr = attrs[0] as SleeperBedNameAttribute;

				if ( attr != null )
					return attr.Name;
			}

			return m.Name;
		}
	
		public override void Serialize( GenericWriter writer ) 
		{ 
			base.Serialize( writer ); 

			writer.Write( (int) 4 );
			
			// Eni - version 4
			writer.Write( m_Addon );
			
			// Eni - version 2
			writer.Write( m_AlwaysSleep );
			writer.Write( m_Asleep );

			writer.Write(m_spell); // version 1

			writer.Write(m_Owner); // version 0
			writer.Write(m_SleeperBedBodyName);
			//writer.Write(m_Blessed); // removed in version 3
			
			writer.Write( m_EquipItems );
		} 

		public override void Deserialize( GenericReader reader ) 
		{ 
			m_spell = true;
			base.Deserialize( reader ); 

			int version = reader.ReadInt(); 
			switch( version )
			{
				case 4:
				{
					m_Addon = (SleeperBaseAddon)reader.ReadItem();
					goto case 3;
				}
				case 3:
				{
					goto case 2;
				}
				case 2:
				{
					m_AlwaysSleep = reader.ReadBool();
					m_Asleep = reader.ReadBool();
					goto case 1;
				}
				case 1:
				{
					m_spell = reader.ReadBool();
					goto case 0;
				}
				case 0:
				{
					m_Owner = reader.ReadMobile();
					m_SleeperBedBodyName = reader.ReadString();
					if (version < 3)
						m_Blessed = reader.ReadBool();

					m_EquipItems = reader.ReadStrongItemList();
					break;
				}
			}
			
			if (m_Owner != null) {
				if (m_Owner.HairItemID > 0) { m_Hair = new HairInfo(m_Owner.HairItemID, m_Owner.HairHue); }
				if (m_Owner.FacialHairItemID > 0 ) { m_FacialHair = new FacialHairInfo(m_Owner.FacialHairItemID, m_Owner.FacialHairHue); }
			}

			// Delete on Server restart if spell action
			if( m_spell )
				this.Delete();
		} 
		
		private class InternalTimer : Timer
		{
			private Mobile m_Owner;
			private Item m_Body;
			private DateTime shutitoff = DateTime.Now + TimeSpan.FromSeconds(300); 
			public InternalTimer( Mobile m, Item body ) : base( TimeSpan.FromSeconds(10),TimeSpan.FromSeconds(10) )
			{
				m_Owner = m;
				m_Body = body;
				Priority = TimerPriority.FiveSeconds;
			}
			protected override void OnTick() 
			{ 
				if (DateTime.Now < shutitoff)
				{
					if(m_Body != null)
						m_Body.PublicOverheadMessage(0,m_Owner.SpeechHue,false,"zZz"); 
					if(m_Owner != null)
					{
						m_Owner.PlaySound(  m_Owner.Female ? 819 : 1093);
					
						// Eni - quick recovery code by Zardoz
						if ( QuickRecovery ) {
							if ( m_Owner.Hits < m_Owner.HitsMax )
							{
								m_Owner.Hits += ( m_Owner.HitsMax / 10 );
								if ( m_Owner.Hits > m_Owner.HitsMax )
									m_Owner.Hits = m_Owner.HitsMax;
							}
							
							if ( m_Owner.Stam < m_Owner.StamMax )
							{
								m_Owner.Stam += ( m_Owner.StamMax / 10 );
								if ( m_Owner.Stam > m_Owner.StamMax )
									m_Owner.Stam = m_Owner.StamMax;
							}
							
							if ( m_Owner.Mana < m_Owner.ManaMax )
							{
								m_Owner.Mana += ( m_Owner.ManaMax / 10 );
								if ( m_Owner.Mana > m_Owner.ManaMax )
									m_Owner.Mana = m_Owner.ManaMax;
							}
						}
					}
				}
				else
				{
					Stop();
					m_Body.PublicOverheadMessage(0,m_Owner.EmoteHue, false,"*falls into a deep, quiet sleep*");
				}
			} 
		}
	}
}
  
