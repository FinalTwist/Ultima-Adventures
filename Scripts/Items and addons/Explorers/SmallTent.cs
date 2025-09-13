using System; 
using System.Collections.Generic;
using Server; 
using Server.Mobiles;
using Server.Items;
using Server.Regions;
using Server.Spells;
using Server.Network;
using Server.Multis;
using System.Collections;

namespace Server.Items 
{
	public enum SmallTentEffect
	{
		Charges
	}

	public class SmallTent : Item
	{
		private SmallTentEffect m_SmallTentEffect;
		private int m_Charges;

		[CommandProperty( AccessLevel.GameMaster )]
		public SmallTentEffect Effect
		{
			get{ return m_SmallTentEffect; }
			set{ m_SmallTentEffect = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int Charges
		{
			get{ return m_Charges; }
			set{ m_Charges = value; InvalidateProperties(); }
		}

		[Constructable]
		public SmallTent() : base( 0x1914 )
		{
			Name = "a small tent";
			Weight = 2.0; 
			Charges = 10;
			Hue = Utility.RandomList( 0x96D, 0x96E, 0x96F, 0x970, 0x971, 0x972, 0x973, 0x974, 0x975, 0x976, 0x977, 0x978, 0x979, 0x97A, 0x97B, 0x97C, 0x97D, 0x97E );		
			//LootType = LootType.Blessed;						
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Setup A Small Tent In Which To Rest");
			list.Add( 1049644, "Usable By Those Skilled In Camping");
        } 
		
		public override void OnDoubleClick( Mobile from )
		{
			bool inCombat = ( from.Combatant != null && from.InRange( from.Combatant.Location, 20 ) && from.Combatant.InLOS( from ) );

			if ( !IsChildOf( from.Backpack ) ) 
			{
				from.SendMessage( "This must be in your backpack to use." );
				return;
			}
			else if ( Server.Misc.Worlds.IsOnBoat( from ) )
			{
				from.SendMessage( "You cannot setup this tent near a boat." );
				return;
			}
			else if ( !Server.Misc.Worlds.IsMainRegion( Server.Misc.Worlds.GetRegionName( from.Map, from.Location ) ) )
			{
				from.SendMessage( "You can only setup this tent in the wilderness." );
				return;
			}
			else if ( inCombat )
			{
				from.SendMessage( "You cannot setup a tent while in combat." );
				return;
			}
			else if ( Charges > 0 && from.CheckSkill( SkillName.Camping, 0.0, 50.0 ) )
			{
				if ( Validate( from ) )
				{
					from.CheckSkill( SkillName.Camping, 0, 100 );
					from.CheckSkill( SkillName.Camping, 0, 100 );
					from.CheckSkill( SkillName.Camping, 0, 100 );
					ConsumeCharge( from );
					new SmallTentSpell( this, from ).Cast();
				}
			}
			else if ( Charges > 0 )
			{
				if ( Validate( from ) )
				{
					from.SendMessage( "Your tent is a bit more worn out as you fail to set it up properly." );
					from.CheckSkill( SkillName.Camping, 0, 100 );
					from.CheckSkill( SkillName.Camping, 0, 100 );
					ConsumeCharge( from );
					return;
				}
			}
			else
			{
				from.SendMessage( "This tent is too worn from over use, and is no longer of any good." );
				return;
			}
		}

		private bool Validate( Mobile from )  
		{
			if ( from.Skills[SkillName.Camping].Value < 10 )
			{
				from.SendMessage( "You need at least a 10.0 camping skill to use this tent!" ); 
				return false;
			}		
			else 
			{
				return true;
			}
		}
		
		public void ConsumeCharge( Mobile from )
		{
			--Charges;

			if ( Charges == 0 )
			{
				from.SendMessage( "This tent is too worn from over use, and is no longer of any good." );
				this.Delete();
			}
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			list.Add( 1060584, m_Charges.ToString() );
		}

		public SmallTent( Serial serial ) : base( serial )
		{ 
		} 
		
		public override void Serialize( GenericWriter writer ) 
		{ 
			base.Serialize( writer ); 
			writer.Write( (int) 0 );
			writer.Write( (int) m_SmallTentEffect );
			writer.Write( (int) m_Charges );
		} 
		
		public override void Deserialize(GenericReader reader) 
		{ 
			base.Deserialize( reader ); 
			int version = reader.ReadInt();
			switch ( version )
			{
				case 0:
				{
					m_SmallTentEffect = (SmallTentEffect)reader.ReadInt();
					m_Charges = (int)reader.ReadInt();

					break;
				}
			}
		}

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		private class SmallTentSpell : Spell
		{
			private static SpellInfo m_Info = new SpellInfo( "Setup Tent", "", 239, 9031, false );

			private SmallTent m_SmallTent;

			public SmallTentSpell( SmallTent SmallTent, Mobile caster ) : base( caster, null, m_Info )
			{
				m_SmallTent = SmallTent;
			}

			public override bool ClearHandsOnCast{ get{ return false; } }
			public override bool RevealOnCast{ get{ return true; } }
			public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 1.0 ); } }

			public override TimeSpan GetCastRecovery()
			{
				return TimeSpan.Zero;
			}

			public override TimeSpan GetCastDelay()
			{
				return TimeSpan.FromSeconds(5);
			}

			public override int GetMana()
			{
				return 0;
			}

			public override bool ConsumeReagents()
			{
				return false;
			}

			public override bool CheckFizzle()
			{
				return false;
			}

			private bool m_Stop;

			public void Stop()
			{
				m_Stop = true;
				Disturb( DisturbType.Hurt, false, false );
			}

			public override bool CheckDisturb( DisturbType type, bool checkFirst, bool resistable )
			{
				if ( type == DisturbType.Hurt )
					return false;
				else
					return true;
			}

			public override void DoHurtFizzle()
			{
				if ( !m_Stop )
					base.DoHurtFizzle();
			}

			public override void DoFizzle()
			{
				if ( !m_Stop )
					base.DoFizzle();
			}

			public override void OnDisturb( DisturbType type, bool message )
			{
				if ( message && !m_Stop )
					Caster.SendMessage( "You have been disrupted while attempting to setup your tent." );
			}

			public override void OnCast()
			{			
				SmallTentBuilt();
				FinishSequence();
			}
			
			private void SmallTentBuilt( )
			{
				if ( m_SmallTent.Validate( Caster ) )
				{
					SmallTentAddon MyWall = new SmallTentAddon( m_SmallTent.Hue, Caster.Serial );
					bool validLocation = false;
					Point3D loc = Caster.Location;
					Map map = Caster.Map;
					MyWall.MoveToWorld( loc, map );
					Caster.PlaySound( 0x55 );
				}
			}
		}
	}
}

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Server.Items
{
	public class SmallTentAddon : BaseAddon
	{
		private int m_Owner;

		[CommandProperty( AccessLevel.GameMaster )]
		public int Owner
		{
			get{ return m_Owner; }
			set{ m_Owner = value; InvalidateProperties(); }
		}

		private DateTime m_DecayTime;
		private Timer m_DecayTimer;

		public virtual TimeSpan DecayDelay{ get{ return TimeSpan.FromMinutes( 5.0 ); } }

		[ Constructable ]
		public SmallTentAddon( int color, int owner )
		{
			m_Owner = owner;

			AddComplexComponent( (BaseAddon) this, 563, -1, -1, 0, color, -1, "a small tent", 1);// 1
			AddComplexComponent( (BaseAddon) this, 568, -1, 0, 0, color, -1, "a small tent", 1);// 2
			AddComplexComponent( (BaseAddon) this, 568, -1, 1, 0, color, -1, "a small tent", 1);// 3
			AddComplexComponent( (BaseAddon) this, 569, 0, -1, 0, color, -1, "a small tent", 1);// 4
			AddComplexComponent( (BaseAddon) this, 569, 1, -1, 0, color, -1, "a small tent", 1);// 5
			AddComplexComponent( (BaseAddon) this, 565, 2, -1, 0, color, -1, "a small tent", 1);// 6
			AddComplexComponent( (BaseAddon) this, 562, 2, 0, 0, color, -1, "a small tent", 1);// 7
			AddComplexComponent( (BaseAddon) this, 562, 2, 1, 0, color, -1, "a small tent", 1);// 8
			AddComplexComponent( (BaseAddon) this, 1552, 0, 0, 20, color, -1, "a small tent", 1);// 9
			AddComplexComponent( (BaseAddon) this, 1553, 2, 0, 20, color, -1, "a small tent", 1);// 10
			AddComplexComponent( (BaseAddon) this, 1547, 0, 1, 20, color, -1, "a small tent", 1);// 11
			AddComplexComponent( (BaseAddon) this, 1548, 1, 0, 20, color, -1, "a small tent", 1);// 12
			AddComplexComponent( (BaseAddon) this, 1549, 2, 1, 20, color, -1, "a small tent", 1);// 13
			AddComplexComponent( (BaseAddon) this, 1544, 1, 1, 28, color, -1, "a small tent", 1);// 14
			AddComplexComponent( (BaseAddon) this, 564, -1, 2, 0, color, -1, "a small tent", 1);// 15
			AddComplexComponent( (BaseAddon) this, 561, 0, 2, 0, color, -1, "a small tent", 1);// 16
			AddComplexComponent( (BaseAddon) this, 561, 1, 2, 0, color, -1, "a small tent", 1);// 17
			AddComplexComponent( (BaseAddon) this, 560, 2, 2, 0, color, -1, "a small tent", 1);// 18
			AddComplexComponent( (BaseAddon) this, 1554, 0, 2, 20, color, -1, "a small tent", 1);// 19
			AddComplexComponent( (BaseAddon) this, 1551, 2, 2, 20, color, -1, "a small tent", 1);// 20
			AddComplexComponent( (BaseAddon) this, 1550, 1, 2, 20, color, -1, "a small tent", 1);// 21

			RefreshDecay( true );
			Timer.DelayCall( TimeSpan.Zero, new TimerCallback( CheckAddComponents ) );
		}

        private static void AddComplexComponent(BaseAddon addon, int item, int xoffset, int yoffset, int zoffset, int hue, int lightsource)
        {
            AddComplexComponent(addon, item, xoffset, yoffset, zoffset, hue, lightsource, null, 1);
        }

        private static void AddComplexComponent(BaseAddon addon, int item, int xoffset, int yoffset, int zoffset, int hue, int lightsource, string name, int amount)
        {
            AddonComponent ac;
            ac = new AddonComponent(item);
            if (name != null && name.Length > 0)
                ac.Name = name;
            if (hue != 0)
                ac.Hue = hue;
            if (amount > 1)
            {
                ac.Stackable = true;
                ac.Amount = amount;
            }
            if (lightsource != -1)
                ac.Light = (LightType) lightsource;
            addon.AddComponent(ac, xoffset, yoffset, zoffset);
        }

		public void CheckAddComponents()
		{
			if( Deleted )
				return;
			AddComponents();
		}

		public virtual void AddComponents()
		{
		}

		public override void OnComponentUsed( AddonComponent ac, Mobile from )
		{
			if ( m_Owner == from.Serial )
			{
				from.CheckSkill( SkillName.Camping, 0, 100 );
				from.CheckSkill( SkillName.Camping, 0, 100 );
				from.CheckSkill( SkillName.Camping, 0, 100 );
				from.PlaySound( 0x55 );
				this.Delete();
			}
		}

		public SmallTentAddon( Serial serial ) : base( serial )
		{
		}

		public virtual void RefreshDecay( bool setDecayTime )
		{
			if( Deleted )
				return;
			if( m_DecayTimer != null )
				m_DecayTimer.Stop();
			if( setDecayTime )
				m_DecayTime = DateTime.UtcNow + DecayDelay;
			m_DecayTimer = Timer.DelayCall( DecayDelay, new TimerCallback( Delete ) );
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( 0 ); // Version
			writer.WriteDeltaTime( m_DecayTime );
			writer.WriteEncodedInt( (int) m_Owner );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			switch ( version )
			{
				case 0:
				{
					m_DecayTime = reader.ReadDeltaTime();
					RefreshDecay( false );
					break;
				}
			}
			m_Owner = reader.ReadEncodedInt();
		}
	}
}