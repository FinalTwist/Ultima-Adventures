using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Mobiles;
using Server.Misc;
using Server.Engines.PartySystem;
using Server.OneTime.Events;

namespace Server.Items
{
	public class RoyalBloodUrn : Item
	{
		private PlayerMobile m_Caster;

		public PlayerMobile Caster {
			get { return m_Caster; }
			set { m_Caster = value;}
		}

		private List<Mobile> m_SummonedUndead;

		public List<Mobile> SummonedUndead {
			get { return m_SummonedUndead; }
			set { m_SummonedUndead = value;}
		}

		private int m_MinutesRemaining;
		public int MinutesRemaining {
			get { return m_MinutesRemaining; }
			set { m_MinutesRemaining = value; InvalidateProperties();}
		}

		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		public override int LabelNumber{ get{ return 1061224; } } // royal blood urn

		[Constructable]
		public RoyalBloodUrn() : this( 1 )
		{
		}

		[Constructable]
		public RoyalBloodUrn( int amount ) 
		{
			SummonedUndead = new List<Mobile>();
			MinutesRemaining = 0;
			
			Stackable = false;
			Hue = 38;
			Amount = amount;
			ItemID = 0x241E;
			Light = LightType.Circle150;
		}

		public RoyalBloodUrn( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			writer.Write( (int) m_MinutesRemaining);
			writer.Write(m_SummonedUndead);
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			m_MinutesRemaining = reader.ReadInt();
			m_SummonedUndead = reader.ReadStrongMobileList();
			if (m_SummonedUndead == null)
				m_SummonedUndead = new List<Mobile>();
			Light = LightType.Circle150;
		}
		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			list.Add( 1060658, "{0}\t{1}", "Difficulty", "(Challenging - requires group)" );  // ~1_val~: ~2_val~
			if (MinutesRemaining > 1) {
				list.Add( 1060658, "{0}\t{1}", "Minutes remaining", (MinutesRemaining+1).ToString() );  // ~1_val~: ~2_val~
			}
		}

		public void CheckMinutes(object sender, EventArgs e) {
			if (MinutesRemaining == 0) {
				this.Caster.SendMessage("The ritual has expired.");
				foreach(Mobile undead in SummonedUndead) {
					if (undead.Alive) {
						undead.Delete();	
					}
				}
				SummonedUndead = new List<Mobile>();
				OneTimeMinEvent.MinTimerTick -= CheckMinutes;	
			}
			--MinutesRemaining;
		}

		public override void OnDoubleClick( Mobile mobile )
		{
			if (mobile is PlayerMobile && ((PlayerMobile)mobile).SoulBound) {
				PlayerMobile player = (PlayerMobile)mobile;
				if (!(Worlds.IsCrypt( new Point3D(player.X, player.Y, player.Z), player.Map ))) {
					player.SendMessage("You must be in a crypt area to perform this ritual.");
					return;
				};
				Phylactery phylactery = player.FindPhylactery(); 
				if (phylactery != null) {
					if (MinutesRemaining > 0) {
						mobile.SendMessage("A ritual is already taking place.");
						return;
					}
					this.Caster = player;
					Party party = (Party)player.Party;
					if (party != null) {
						if (party.Members.Count >= 3) {
							int groupPower = player.GetGroupPhylacteryPower();
							if (!party.IsSoulBound()) {
								mobile.SendMessage("Your group has a non soul bound party member, the ritual cannot proceed.");	
								return;
							} 
							MinutesRemaining = 9;
							BloodLichMonarch monarch = new BloodLichMonarch();
							if ((groupPower-750) > 2755) {
								monarch.SetHits(groupPower-750, groupPower);								
							}
							for (int i = 0; i < 6; i++) {
								Mobile undead = phylactery.GetEscapedUndead(true);
								int xy = Utility.RandomMinMax(3, 6);
								undead.MoveToWorld(new Point3D(player.X+xy, player.Y+xy, player.Z), player.Map);
								SummonedUndead.Add(undead);
							}
							monarch.MoveToWorld(new Point3D(player.X+7, player.Y+7, player.Z), player.Map);
							monarch.SayTo(player, "You are a fool to have awoken me, mortal");
							SummonedUndead.Add(monarch);
							OneTimeMinEvent.MinTimerTick += CheckMinutes;
						} else {
							mobile.SendMessage("Your group is not large enough to fulfil the ritual.");	
						}
					} else {
						mobile.SendMessage("You require a group to perform the ritual.");	
					}
				} else {
					mobile.SendMessage("You require a phylactery to complete this ritual.");
				}
			} else {
				mobile.SendMessage("You are not elligible to use this item.");
			}
		}
	}
}