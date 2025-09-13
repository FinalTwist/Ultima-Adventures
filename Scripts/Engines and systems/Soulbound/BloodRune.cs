using System;
using Server;
using Server.Misc;
using Server.Mobiles;
using Server.OneTime.Events;
using Server.Regions;

namespace Server.Items
{
	// Sosaria 3240 1704 0 Trammel
	// Serpent Isles - 2128 1160 0 Malas
	// Lodaria - 2965 922 0 Felucca
	// Savage Empire - 882 1531 0 TerMur
	public class BloodRune : Item
	{	
		private PlayerMobile m_Caster;

		public PlayerMobile Caster {
			get { return m_Caster; }
			set { m_Caster = value;}
		}
		private Point3D m_Pointer;
		public Point3D Pointer {
			get { return m_Pointer; }
			set { m_Pointer = value;}
		}
		private Map m_PointerMap;
		public Map PointerMap {
			get { return m_PointerMap; }
			set { m_PointerMap = value;}
		}
		private int m_MinutesRemaining;
		public int MinutesRemaining {
			get { return m_MinutesRemaining; }
			set { m_MinutesRemaining = value; InvalidateProperties();}
		}
		private int m_SecondsRemaining;
		public int SecondsRemaining {
			get { return m_SecondsRemaining; }
			set { m_SecondsRemaining = value;}
		}
		public Point3D MalasEntrance {
			get { return new Point3D(2128,1160,0); }
			set {}
		}
		public Point3D FeluccaEntrance {
			get { return new Point3D(2965,922,0); }
			set {}
		}
		public Point3D TerMurEntrance {
			get { return new Point3D(882,1531,0); }
			set {}
		}		
		public Point3D TrammelEntrance {
			get { return new Point3D(3240,1704,0); }
			set {}
		}
		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		public override int LabelNumber{ get{ return 1061204; } } // blood rune

		[Constructable]
		public BloodRune() : this( 1 )
		{
		}

		[Constructable]
		public BloodRune( int amount ) 
		{
			MinutesRemaining = 0;
			SecondsRemaining = 10;
			
			Stackable = false;
			Hue = 38;
			Amount = amount;
			ItemID = 0x1F14;
			Light = LightType.Circle150;
		}

		public BloodRune( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 1 ); // version
			writer.Write( (int) m_MinutesRemaining);
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			m_MinutesRemaining = reader.ReadInt();
			if (m_MinutesRemaining > 0) {
				OneTimeMinEvent.MinTimerTick += UpdateMinutesRemaining;
			}
			m_SecondsRemaining = 10;
			Light = LightType.Circle150;
		}
		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			list.Add( 1060658, "{0}\t{1}", "On use", "Teleport back to the nearest entrance to Hall of Legends - 1 hour cooldown" );  // ~1_val~: ~2_val~
			if (MinutesRemaining > 0) {
				list.Add( 1060658, "{0}\t{1}", "Minutes remaining", (MinutesRemaining+1).ToString() );  // ~1_val~: ~2_val~
			}
		}

		public void UpdateMinutesRemaining(object sender, EventArgs e) {
			if (MinutesRemaining > 0) {
				--MinutesRemaining;	
			} else {
				OneTimeMinEvent.MinTimerTick -= UpdateMinutesRemaining;
			}
		}

		public void PerformIncantation(object sender, EventArgs e) {
			if (this.Caster != null) {
				if (SecondsRemaining > 0) {
					this.Caster.CantWalk=true;
					--SecondsRemaining;
				} else {
					SecondsRemaining = 10;
					this.Caster.CantWalk=false;
					this.Caster.PlaySound( 0x1FD );
					this.Caster.MoveToWorld( Pointer, PointerMap );
					this.Caster.PlaySound( 0x1FD );	
					OneTimeSecEvent.SecTimerTick -= PerformIncantation;			
				}
			} else {
				OneTimeSecEvent.SecTimerTick -= PerformIncantation;	
			}
		}

		public override void OnDoubleClick( Mobile from )
		{
			if (from is PlayerMobile) {
				PlayerMobile player = (PlayerMobile)from;
				this.Caster = player;
				if (!player.SoulBound) {
					player.SendLocalizedMessage( 1061202 ); // You cannot bind your soul to this item
				} else if (MinutesRemaining == 0) {
					string world = Worlds.GetMyWorld( player.Map, player.Location, player.X, player.Y );
					PointerMap = Worlds.GetMyDefaultMap( world );
					Region region = Region.Find( player.Location, player.Map );
					if ( region.IsPartOf( typeof( DungeonRegion ) ) ) {
						player.SendMessage("You cannot use this item in a dungeon.");
						return;
					} else {
						Pointer = Point3D.Zero;
						switch (Worlds.GetMyMapString(PointerMap)) {
							case "Felucca":
								Pointer	= this.FeluccaEntrance;
							break;
							case "Malas":
								Pointer	= this.MalasEntrance;
							break;
							case "TerMur":
								Pointer	= this.TerMurEntrance;
							break;
							case "Trammel":
								Pointer = this.TrammelEntrance;
							break;
						}
						if ( Pointer != Point3D.Zero )
						{
							MinutesRemaining = 59;
							player.SayTo(player, "namina uman...");
							OneTimeSecEvent.SecTimerTick += PerformIncantation;
							OneTimeMinEvent.MinTimerTick += UpdateMinutesRemaining;
						} else {
							player.SendMessage("The blood rune does not work here");
						}
					}
				} else {
					string plural = (MinutesRemaining < 2) ? "minute" : "minutes";
					player.SendMessage("You cannot use this rune at this time, you will be able to use it in " + MinutesRemaining.ToString() + " " + plural );
				}
			}
		}
	}
}