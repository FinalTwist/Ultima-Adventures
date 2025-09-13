using System;
using Server.Misc;

namespace Server.Items
{
	[FlipableAttribute( 0x1515, 0x1530 )] 
	public class GTunic : Doublet 
	{ 
		private SkillMod m_SkillMod0; 
		private SkillMod m_SkillMod1; 
		private SkillMod m_SkillMod2; 
		private StatMod m_StatMod0; 
		
		[Constructable] 
		public GTunic() : base( 73 ) 
		{ 
			Name = "Green Tunic"; 
			DefineMods();
			Resistances.Physical = 50;
			
		} 

		private void DefineMods()
		{
			m_SkillMod0 = new DefaultSkillMod( SkillName.Swords, true, 20 ); 
			m_SkillMod1 = new DefaultSkillMod( SkillName.Parry, true, 20 ); 
			m_SkillMod2 = new DefaultSkillMod( SkillName.MagicResist, true, 20 ); 
			m_StatMod0 = new StatMod( StatType.Dex, "DruidCloak", 15, TimeSpan.Zero ); 
		}

		private void SetMods( Mobile wearer )
		{			
			wearer.AddSkillMod( m_SkillMod0 ); 
			wearer.AddSkillMod( m_SkillMod1 ); 
			wearer.AddSkillMod( m_SkillMod2 ); 
			wearer.AddStatMod( m_StatMod0 ); 
		}

		public override bool OnEquip( Mobile from ) 
		{ 
			SetMods( from );
			return true;  
		} 

		public override bool Dye( Mobile from, DyeTub sender )
		{
			from.SendLocalizedMessage( 1042083 ); // You can not dye that.
			return false;
		}

		public override void OnRemoved(IEntity parent ) 
		{ 
			if ( parent is Mobile ) 
			{ 
				Mobile m = (Mobile)parent;
				m.RemoveStatMod( "GTunic" ); 

				if ( m.Hits > m.HitsMax )
					m.Hits = m.HitsMax; 

				if ( m_SkillMod0 != null ) 
					m_SkillMod0.Remove(); 

				if ( m_SkillMod1 != null ) 
					m_SkillMod1.Remove(); 

				if ( m_SkillMod2 != null ) 
					m_SkillMod2.Remove(); 
			} 
		} 

		public override void OnSingleClick( Mobile from ) 
		{ 
			this.LabelTo( from, Name ); 
		} 

		public GTunic( Serial serial ) : base( serial ) 
		{ 
			DefineMods();
			
			if ( Parent != null && this.Parent is Mobile ) 
				SetMods( (Mobile)Parent );
		} 

		public override void Serialize( GenericWriter writer ) 
		{ 
			base.Serialize( writer ); 
			writer.Write( (int) 0 ); 
		} 

		public override void Deserialize(GenericReader reader) 
		{ 
			base.Deserialize( reader ); 
			int version = reader.ReadInt(); 
		} 
	} 
} 
