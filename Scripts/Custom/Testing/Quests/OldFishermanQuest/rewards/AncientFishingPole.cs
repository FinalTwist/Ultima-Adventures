using Server;
using Server.Network;
using System;
using System.Collections;
using Server.Targeting;
using Server.Items;
using Server.Engines.Harvest;
using Server.ContextMenus;
using System.Collections.Generic;

namespace Server.Items
{
	public class AncientFishingPole30 : BaseAFP
	{

		public SkillMod m_SkillMod;

		[Constructable]
		public AncientFishingPole30() : base( 0x0DC0 )
		{
			Layer = Layer.OneHanded;
			Weight = 8.0;
			Hue = 1150;
			Name = "Ancient Fishing Pole";
		}

		[Constructable]
		public AncientFishingPole30( int uses ) : base( uses, 0x0DC0 )
		{
			Layer = Layer.OneHanded;
			Weight = 8.0;
			Hue = 1150;
			Name = "Ancient Fishing Pole";
		}

		public override void AddNameProperties( ObjectPropertyList list )
		{
			base.AddNameProperties( list );
			list.Add( 1062613, "+ 30" );
		}

		public override bool OnEquip( Mobile m )
		{
			base.OnEquip( m );
			m_SkillMod = new DefaultSkillMod( SkillName.Fishing, true, 30 ); 
			m.AddSkillMod(m_SkillMod );
			return true;
		}

		public override void OnRemoved(IEntity parent )
		{
			base.OnRemoved( parent );

			if ( m_SkillMod != null ) 
			m_SkillMod.Remove(); 
			m_SkillMod = null; 
		}


		public override void OnDoubleClick( Mobile from )
		{
			Fishing.System.BeginHarvesting( from, this );
		}

        public override void GetContextMenuEntries(Mobile from, List<ContextMenuEntry> list)
		{
			base.GetContextMenuEntries( from, list );

			BaseHarvestTool.AddContextMenuEntries( from, this, list, Fishing.System );
		}

		public AncientFishingPole30( Serial serial ) : base( serial )
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
	public abstract class BaseAFP : Item, IUsesRemaining
	{
		private int m_UsesRemaining;

		[CommandProperty( AccessLevel.GameMaster )]
		public int UsesRemaining
		{
			get { return m_UsesRemaining; }
			set { m_UsesRemaining = value; InvalidateProperties(); }
		}

		public bool ShowUsesRemaining{ get{ return true; } set{} }

		public BaseAFP( int itemID ) : this( 50, itemID )
		{
		}

		public BaseAFP( int uses, int itemID ) : base( itemID )
		{
			m_UsesRemaining = uses;
		}

		public BaseAFP( Serial serial ) : base( serial )
		{
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			list.Add( 1060584, m_UsesRemaining.ToString() ); // uses remaining: ~1_val~
		}

		public virtual void DisplayDurabilityTo( Mobile m )
		{
			LabelToAffix( m, 1017323, AffixType.Append, ": " + m_UsesRemaining.ToString() ); // Durability
		}

		public static bool CheckAccessible( Item tool, Mobile m )
		{
			return ( tool.IsChildOf( m ) || tool.Parent == m );
		}

		public static bool CheckTool( Item tool, Mobile m )
		{
			Item check = m.FindItemOnLayer( Layer.OneHanded );

			if ( check is BaseTool && check != tool )
				return false;

			check = m.FindItemOnLayer( Layer.TwoHanded );

			if ( check is BaseAFP && check != tool )
				return false;

			return true;
		}

		public override void OnSingleClick( Mobile from )
		{
			DisplayDurabilityTo( from );

			base.OnSingleClick( from );
		}


		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write( (int) m_UsesRemaining );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 0:
				{
					m_UsesRemaining = reader.ReadInt();
					break;
				}
			}
		}
	}
}