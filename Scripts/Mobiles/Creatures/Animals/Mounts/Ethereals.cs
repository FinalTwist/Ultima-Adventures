using System;
using Server.Mobiles;
using Server.Items;
using Server.Spells;
using Server.Engines.VeteranRewards;
using System.Collections;
using Server.Misc;
using Server.Regions;

namespace Server.Mobiles
{
	public class EtherealMount : Item, IMount, IMountItem, Engines.VeteranRewards.IRewardItem
	{
		private int m_MountedID;
		private int m_RegularID;
		private Mobile m_Rider;
		private bool m_IsRewardItem;
		private bool m_IsDonationItem;
		private Mobile m_Owner;

		[CommandProperty( AccessLevel.GameMaster )]
		public bool IsRewardItem
		{
			get { return m_IsRewardItem; }
			set { m_IsRewardItem = value; }
		}

		public override double DefaultWeight
		{
			get { return 1.0; }
		}

		[CommandProperty( AccessLevel.GameMaster, AccessLevel.Administrator )]
		public bool IsDonationItem
		{
			get { return m_IsDonationItem; }
			set { m_IsDonationItem = value; InvalidateProperties(); }
		}

		[Constructable]
		public EtherealMount( int itemID, int mountID ) : base( itemID )
		{
			m_MountedID = mountID;
			m_RegularID = itemID;
			m_Rider = null;
			m_Owner = null;

			Layer = Layer.Invalid;
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int MountedID
		{
			get
			{
				return m_MountedID;
			}
			set
			{
				if( m_MountedID != value )
				{
					m_MountedID = value;

					if( m_Rider != null )
						ItemID = value;
				}
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int RegularID
		{
			get
			{
				return m_RegularID;
			}
			set
			{
				if( m_RegularID != value )
				{
					m_RegularID = value;

					if( m_Rider == null )
						ItemID = value;
				}
			}
		}

		public EtherealMount( Serial serial ) : base( serial )
		{
		}

		public override bool DisplayLootType { get { return false; } }

		public virtual int FollowerSlots { get { return 0; } }

		public void RemoveFollowers()
		{
			if( m_Rider != null )
				m_Rider.Followers -= FollowerSlots;

			if( m_Rider != null && m_Rider.Followers < 0 )
				m_Rider.Followers = 0;
		}

		public void AddFollowers()
		{
			if( m_Rider != null )
				m_Rider.Followers += FollowerSlots;
		}

		public virtual bool Validate( Mobile from )
		{
			if( Parent == null )
			{
				from.SayTo( from, 1010095 ); // This must be on your person to use.
				return false;
			}
			else if ( ( this is PaladinWarhorse ) && ( from.Skills[SkillName.Chivalry].Base < 100 || from.Karma < 0 ) )
			{
				from.SendMessage("Only grandmaster knights may ride this holy steed.");
				return false;
			}
			else if ( this is Warhorse && (
				from.Skills[SkillName.Tactics].Base < 100 && 
				from.Skills[SkillName.Swords].Base < 100 && 
				from.Skills[SkillName.Macing].Base < 100 && 
				from.Skills[SkillName.Archery].Base < 100 && 
				from.Skills[SkillName.Fencing].Base < 100 
			))
			{
				from.SendMessage("Only grandmaster warriors may ride this warhorse.");
				return false;
			}
			else if ( ( this is DeathKnightWarhorse ) && ( from.Skills[SkillName.Chivalry].Base < 100 || from.Karma > 0 ) )
			{
				from.SendMessage("Only grandmaster death knights may ride this evil steed.");
				return false;
			}
			else if ( ( this is NecroHorse ) && ( from.Skills[SkillName.Necromancy].Base < 100 ) )
			{
				from.SendMessage("Only a grandmaster in necromancy may ride this undead steed.");
				return false;
			}
			else if ( ( this is DaemonMount ) && ( from.Skills[SkillName.Necromancy].Base < 100 ) && ( from.Skills[SkillName.Magery].Base < 100 ) )
			{
				from.SendMessage("Only a grandmaster in necromancy and magery may ride this evil being.");
				return false;
			}
			else if( m_IsRewardItem && !Engines.VeteranRewards.RewardSystem.CheckIsUsableBy( from, this, null ) )
			{
				// CheckIsUsableBy sends the message
				return false;
			}
			else if( !BaseMount.CheckMountAllowed( from, true ) )
			{
				// CheckMountAllowed sends the message
				return false;
			}
			else if( from.Mounted )
			{
				from.SendLocalizedMessage( 1005583 ); // Please dismount first.
				return false;
			}
			else if( from.IsBodyMod && !from.Body.IsHuman )
			{
				from.SendLocalizedMessage( 1061628 ); // You can't do that while polymorphed.
				return false;
			}
			else if( from.HasTrade )
			{
				from.SendLocalizedMessage( 1042317, "", 0x41 ); // You may not ride at this time
				return false;
			}
			else if( ( from.Followers + FollowerSlots ) > from.FollowersMax )
			{
				from.SendLocalizedMessage( 1049679 ); // You have too many followers to summon your mount.
				return false;
			}
			else if( !Multis.DesignContext.Check( from ) )
			{
				// Check sends the message
				return false;
			}

			return true;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( Server.Misc.MyServerSettings.NoMountsInCertainRegions() && Server.Mobiles.AnimalTrainer.IsNoMountRegion( from.Region ) )
			{
				from.SendMessage( "You cannot mount that while you are in this place." );
			}
			else
			{
				if ( this is NecroHorse ){ m_RegularID = 0x2617; m_MountedID = 0x3EBB; }
				else if ( (this.GetType()).IsAssignableFrom(typeof(SkeletalMount)) ){ m_RegularID = 0x2617; m_MountedID = 0x3EBB; }
				else if ( this is DeathKnightWarhorse ){ m_RegularID = 0x2617; m_MountedID = 0x3EBB; }
				else if ( this is PaladinWarhorse ){ m_RegularID = 0x4C59; m_MountedID = 0x3EBE; }
				else if ( this is Warhorse )
				{
					Hue = 0;
					m_RegularID = 0x211F; m_MountedID = 0x3EA0; ItemID = 0x211F;
					if ( Server.Misc.MyServerSettings.ClientVersion() ){ m_RegularID = 0x55DC; m_MountedID = 594; ItemID = 0x55DC; }
				}
				else if ( this is EtherealHorse ){ m_RegularID = 0x20DD; m_MountedID = 0x3EA0; }
				else if ( this is EtherealLlama ){ m_RegularID = 0x20F6; m_MountedID = 0x3EA6; }
				else if ( this is EtherealOstard ){ m_RegularID = 0x2135; m_MountedID = 0x3EA3; }
				else if ( this is EtherealRidgeback ){ m_RegularID = 0x2615; m_MountedID = 0x3E92; }
				else if ( this is EtherealUnicorn ){ m_RegularID = 0x25CE; m_MountedID = 0x3EB4; }
				else if ( this is EtherealBeetle ){ m_RegularID = 0x260F; m_MountedID = 0x3E95; }
				else if ( this is EtherealKirin ){ m_RegularID = 0x25A0; m_MountedID = 0x3EAD; }
				else if ( this is EtherealSwampDragon ){ m_RegularID = 0x2619; m_MountedID = 0x3EBD; }
				else if ( this is RideablePolarBear ){ m_RegularID = 0x20E1; m_MountedID = 16069; }
				else if ( this is EtherealCuSidhe ){ m_RegularID = 0x2D96; m_MountedID = 0x3E91; }
				else if ( this is EtherealHiryu ){ m_RegularID = 0x276A; m_MountedID = 0x3E94; }
				else if ( this is EtherealReptalon ){ m_RegularID = 0x2d95; m_MountedID = 0x3E90; }
				else if ( this is ChargerOfTheFallen ){ m_RegularID = 0x0499; m_MountedID = 0x3EBA; }

				if( Validate( from ) )
					new EtherealSpell( this, from, from ).Cast();
			}
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)3 ); // version

			writer.Write( m_IsDonationItem );
			writer.Write( m_IsRewardItem );

			writer.Write( (int)m_MountedID );
			writer.Write( (int)m_RegularID );
			writer.Write( m_Rider );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			LootType = LootType.Blessed;

			int version = reader.ReadInt();

			switch( version )
			{
				case 3:
				{
					m_IsDonationItem = reader.ReadBool();
					goto case 2;
				}
				case 2:
				{
					m_IsRewardItem = reader.ReadBool();
					goto case 0;
				}
				case 1: reader.ReadInt(); goto case 0;
				case 0:
				{
					m_MountedID = reader.ReadInt();
					m_RegularID = reader.ReadInt();
					m_Rider = reader.ReadMobile();

					if( m_MountedID == 0x3EA2 )
						m_MountedID = 0x3EAA;

					break;
				}
			}

			AddFollowers();

			if( version < 3 && Weight == 0 )
				Weight = -1;

			if ( Hue == 0 || Hue == 0x4001 ){ Hue = 2858; }
			if ( this is Warhorse ){ Hue = 0; }
		}

		public override DeathMoveResult OnParentDeath( Mobile parent )
		{
			Rider = null;
			Owner = null;

			return DeathMoveResult.RemainEquiped;
		}

		public static void Dismount( Mobile m )
		{
			IMount mount = m.Mount;

			if( mount != null )
			{
				EthyDismount( m, true );
				mount.Rider = null;
		}
		}

		public static void EthyDismount( Mobile m, bool clear )
		{
			IMount mount = m.Mount;

			if ( mount != null && mount is EtherealMount && clear )
			{
				EtherealMount ethy = (EtherealMount)mount;
				ethy.Owner = null;
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Rider
		{
			get
			{
				return m_Rider;
			}
			set
			{
				if( value != m_Rider )
				{
					if( value == null )
					{
						Internalize();
						UnmountMe();

						RemoveFollowers();
						m_Rider = value;
					}
					else
					{
						if( m_Rider != null )
							Dismount( m_Rider );

						Dismount( value );

						RemoveFollowers();
						m_Rider = value;
						AddFollowers();

						MountMe();
					}
				}
			}
		}



		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Owner
		{
			get
			{
				return m_Owner;
			}
			set
			{
				m_Owner = value;
			}
		}

		public virtual int EtherealHue { get { return 2858; } }

		public void UnmountMe()
		{
			Container bp = m_Rider.Backpack;

			ItemID = m_RegularID;
			Layer = Layer.Invalid;
			Movable = true;

			if ( Hue == 0 || Hue == 0x4001 ){ Hue = 2858; }
			if ( this is Warhorse ){ Hue = 0; }

			if( bp != null )
			{
				if ( m_Rider is BaseBlue || m_Rider is BaseRed )
				{
					if (Utility.RandomMinMax(1, 300) != 69)
						this.Delete();
				}
				else 
					bp.DropItem( this );
			}
			else
			{
				Point3D loc = m_Rider.Location;
				Map map = m_Rider.Map;

				if( map == null || map == Map.Internal )
				{
					loc = m_Rider.LogoutLocation;
					map = m_Rider.LogoutMap;
				}

				MoveToWorld( loc, map );
			}
		}

/*
		public void UnmountMe()
		{
			Container bp = m_Rider.Backpack;

			ItemID = m_RegularID;
			Layer = Layer.Invalid;
			Movable = true;

			if ( Hue == 0 || Hue == 0x4001 ){ Hue = 2858; }

			if( bp != null )
			{
				if ( m_Rider is BaseBlue || m_Rider is BaseRed )
				{
				this.Delete();
				}
				else 
				{
				bp.DropItem( this );
				}
			}
			else
*/
		public void MountMe()
		{
			ItemID = m_MountedID;
			Layer = Layer.Mount;
			Movable = false;

			if ( Hue == 0 || Hue == 0x4001 ){ Hue = 2858; }
			if ( this is Warhorse ){ Hue = 0; }

			ProcessDelta();
			m_Rider.ProcessDelta();
			m_Rider.EquipItem( this );
			m_Rider.ProcessDelta();
			ProcessDelta();
		}

		public IMount Mount
		{
			get
			{
				return this;
			}
		}

		public static void StopMounting( Mobile mob )
		{
			if( mob.Spell is EtherealSpell )
				( (EtherealSpell)mob.Spell ).Stop();
		}

		public void OnRiderDamaged( int amount, Mobile from, bool willKill )
		{
		}

		private class EtherealSpell : Spell
		{
			private static SpellInfo m_Info = new SpellInfo( "Ethereal Mount", "", 230 );

			private EtherealMount m_Mount;
			private Mobile m_Rider;
			private Mobile m_Owner;

			public EtherealSpell( EtherealMount mount, Mobile rider, Mobile owner ) : base( rider, null, m_Info )
			{
				m_Rider = rider;
				m_Mount = mount;
				m_Owner = owner;
			}

			public override bool ClearHandsOnCast { get { return false; } }
			public override bool RevealOnCast { get { return false; } }

			public override TimeSpan GetCastRecovery()
			{
				return TimeSpan.Zero;
			}

			public override double CastDelayFastScalar { get { return 0; } }

			public override TimeSpan CastDelayBase
			{
				get
				{
					return TimeSpan.FromSeconds( ( ( m_Mount.IsDonationItem && RewardSystem.GetRewardLevel( m_Rider ) < 3 ) ? ( 7.5 + ( Core.AOS ? 3.0 : 2.0 ) ) : ( Core.AOS ? 3.0 : 2.0 ) ) );
				}
			}

			public override int GetMana()
			{
				return 0;
			}

			public override bool ConsumeReagents()
			{
				return true;
			}

			public override bool CheckFizzle()
			{
				return true;
			}

			private bool m_Stop;

			public void Stop()
			{
				m_Stop = true;
				Disturb( DisturbType.Hurt, false, false );
			}

			public override bool CheckDisturb( DisturbType type, bool checkFirst, bool resistable )
			{
				if( type == DisturbType.EquipRequest || type == DisturbType.UseRequest/* || type == DisturbType.Hurt*/ )
					return false;

				return true;
			}

			public override void DoHurtFizzle()
			{
				if( !m_Stop )
					base.DoHurtFizzle();
			}

			public override void DoFizzle()
			{
				if( !m_Stop )
					base.DoFizzle();
			}

			public override void OnDisturb( DisturbType type, bool message )
			{
				if( message && !m_Stop )
					Caster.SendMessage("You have been disrupted while attempting to summon your mount!");
			}

			public override void OnCast()
			{
				if( !m_Mount.Deleted && m_Mount.Rider == null && m_Mount.Validate( m_Rider ) )
				{
					m_Mount.Rider = m_Rider;

					ArrayList ethy = new ArrayList();
					foreach ( Item item in World.Items.Values )
					{
						if ( item is EtherealMount )
						{
							if ( ((EtherealMount)item).Owner == m_Rider )
							{
								((EtherealMount)item).Owner = null;
							}
						}
					}

					m_Mount.Owner = m_Rider;
				}

				FinishSequence();
			}
		}
	}

	public class EtherealHorse : EtherealMount
	{
		[Constructable]
		public EtherealHorse(): base( 0x20DD, 0x3EA0 )
		{
			Name = "ethereal horse";
		}

		public EtherealHorse( Serial serial ): base( serial )
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

			if( ItemID == 0x2124 )
				ItemID = 0x20DD;
		}
	}

	public class EtherealLlama : EtherealMount
	{
		[Constructable]
		public EtherealLlama()
			: base( 0x20F6, 0x3EA6 )
		{
			Name = "ethereal llama";
		}

		public EtherealLlama( Serial serial )
			: base( serial )
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

	public class EtherealOstard : EtherealMount
	{
		[Constructable]
		public EtherealOstard()
			: base( 0x2135, 0x3EA3 )
		{
			Name = "ethereal ostard";
		}

		public EtherealOstard( Serial serial )
			: base( serial )
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

	public class EtherealRidgeback : EtherealMount
	{
		[Constructable]
		public EtherealRidgeback()
			: base( 0x2615, 0x3E92 )
		{
			Name = "ethereal stegladon";
		}

		public EtherealRidgeback( Serial serial )
			: base( serial )
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

	public class EtherealUnicorn : EtherealMount
	{
		[Constructable]
		public EtherealUnicorn()
			: base( 0x25CE, 0x3EB4 )
		{
			Name = "ethereal unicorn";
		}

		public EtherealUnicorn( Serial serial )
			: base( serial )
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

	public class EtherealBeetle : EtherealMount
	{
		[Constructable]
		public EtherealBeetle()
			: base( 0x260F, 0x3E95 )
		{
			Name = "ethereal beetle";
		}

		public EtherealBeetle( Serial serial )
			: base( serial )
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

	public class EtherealKirin : EtherealMount
	{
		[Constructable]
		public EtherealKirin()
			: base( 0x25A0, 0x3EAD )
		{
			Name = "ethereal ki-rin";
		}

		public EtherealKirin( Serial serial )
			: base( serial )
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

	public class EtherealSwampDragon : EtherealMount
	{
		[Constructable]
		public EtherealSwampDragon()
			: base( 0x2619, 0x3EBD )
		{
			Name = "ethereal lizard";
		}

		public EtherealSwampDragon( Serial serial )
			: base( serial )
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

	public class RideablePolarBear : EtherealMount
	{
		[Constructable]
		public RideablePolarBear()
			: base( 0x20E1, 16069 )
		{
			Name = "ethereal bear";
		}

		public RideablePolarBear( Serial serial )
			: base( serial )
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

	public class EtherealCuSidhe : EtherealMount
	{
		[Constructable]
		public EtherealCuSidhe()
			: base( 0x2D96, 0x3E91 )
		{
			Name = "ethereal wolf";
		}

		public EtherealCuSidhe( Serial serial )
			: base( serial )
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

	public class EtherealHiryu : EtherealMount
	{
		[Constructable]
		public EtherealHiryu()
			: base( 0x276A, 0x3E94 )
		{
			Name = "ethereal bird";
		}

		public EtherealHiryu( Serial serial )
			: base( serial )
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

	public class EtherealReptalon : EtherealMount
	{
		[Constructable]
		public EtherealReptalon()
			: base( 0x2d95, 0x3E90 )
		{
			Name = "ethereal drake";
		}

		public EtherealReptalon( Serial serial )
			: base( serial )
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

	public class ChargerOfTheFallen : EtherealMount
	{
		[Constructable]
		public ChargerOfTheFallen()
			: base( 0x0499, 0x3EBA )
		{
			Name = "ethereal lion";
		}

		public ChargerOfTheFallen( Serial serial )
			: base( serial )
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