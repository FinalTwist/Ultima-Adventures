
using System;
using Server;
using Server.Items;

namespace Server.Items
{
	
	public class CuteFluffyBunnyEarrings: GoldEarrings
	{ 
		private SkillMod m_SkillMod0; 
		private SkillMod m_SkillMod1; 
		private SkillMod m_SkillMod2;
    //private SkillMod m_SkillMod3; 
		//private StatMod m_StatMod0; 
		
		
		[Constructable]
              public CuteFluffyBunnyEarrings()
{

              Weight = 3;
              Name = "Cute Fluffy Bunny Earrings";
              Hue = 2264;

              Attributes.BonusHits = 3;
              Attributes.BonusInt = 3;
              Attributes.BonusMana = 3;
              Attributes.EnhancePotions = 40;
              Attributes.Luck = 50;
              Attributes.NightSight = 1;
              Attributes.ReflectPhysical = 3;
							DefineMods();
		} 

		private void DefineMods()
		{
			m_SkillMod0 = new DefaultSkillMod( SkillName.AnimalTaming, true, 15 ); 
			m_SkillMod1 = new DefaultSkillMod( SkillName.AnimalLore, true, 15 ); 
			m_SkillMod2 = new DefaultSkillMod( SkillName.Veterinary, true, 15 );
      //m_SkillMod3 = new DefaultSkillMod( SkillName.Veterinary, true, 15 ); 
			//m_StatMod0 = new StatMod( StatType.Int, "CuteFluffyBunnyEarrings", 15, TimeSpan.Zero ); 
		}

		private void SetMods( Mobile wearer )
		{			
			wearer.AddSkillMod( m_SkillMod0 ); 
			wearer.AddSkillMod( m_SkillMod1 ); 
			wearer.AddSkillMod( m_SkillMod2 );
      //wearer.AddSkillMod( m_SkillMod3 ); 
			//wearer.AddStatMod( m_StatMod0 ); 
		}

		public override bool OnEquip( Mobile from ) 
		{ 
			SetMods( from );
			return true;  
		} 

/*		public override bool Dye( Mobile from, DyeTub sender )
		{
			from.SendLocalizedMessage( 1042083 ); // You cannot dye that.
			return false;
		} */

		public override void OnRemoved(IEntity parent ) 
		{ 
			if ( parent is Mobile ) 
			{ 
				//Mobile m = (Mobile)parent;
				//m.RemoveStatMod( "CuteFluffyBunnyEarrings" ); 

				//if ( m.Hits > m.HitsMax )
					//m.Hits = m.HitsMax; 

				if ( m_SkillMod0 != null ) 
					m_SkillMod0.Remove(); 

				if ( m_SkillMod1 != null ) 
					m_SkillMod1.Remove(); 

				if ( m_SkillMod2 != null ) 
					m_SkillMod2.Remove(); 
			
			    //if ( m_SkillMod3 != null ) 
					//m_SkillMod3.Remove();
			
			} 
		} 

		public override void OnSingleClick( Mobile from ) 
		{ 
			this.LabelTo( from, Name ); 
		} 

		public CuteFluffyBunnyEarrings( Serial serial ) : base( serial ) 
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
