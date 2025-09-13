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
	public class AncientFishingPole15 : BaseAFP
	{

		public SkillMod m_SkillMod;

		[Constructable]
		public AncientFishingPole15() : base( 0x0DC0 )
		{
			Layer = Layer.OneHanded;
			Weight = 8.0;
			Hue = 1150;
			Name = "Ancient Fishing Pole";
		}

		[Constructable]
		public AncientFishingPole15( int uses ) : base( uses, 0x0DC0 )
		{
			Layer = Layer.OneHanded;
			Weight = 8.0;
			Hue = 1150;
			Name = "Ancient Fishing Pole";
		}

		public override void AddNameProperties( ObjectPropertyList list )
		{
			base.AddNameProperties( list );
			list.Add( 1062613, "+ 15" );
		}

		public override bool OnEquip( Mobile m )
		{
			base.OnEquip( m );
			m_SkillMod = new DefaultSkillMod( SkillName.Fishing, true, 15 ); 
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

		public AncientFishingPole15( Serial serial ) : base( serial )
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
}