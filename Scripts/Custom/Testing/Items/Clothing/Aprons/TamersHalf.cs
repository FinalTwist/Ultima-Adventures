using System; 
using Server; 

namespace Server.Items
{ 
	public class TamersHalf : HalfApron
	{
        public override int ArtifactRarity{ get{ return 13; } }
        private SkillMod m_SkillMod0;
        private SkillMod m_SkillMod1;
        [Constructable]
		public TamersHalf()
        {
			Name = "Tamers Half";
			Hue = 1366;

			//SkillBonuses.SetValues( 0, SkillName.AnimalLore, 10.0 );
            //SkillBonuses.SetValues( 1, SkillName.AnimalTaming, 10.0 );
			
			//Attributes.CastSpeed = 1;
			//Attributes.CastRecovery = 1;
            DefineMods();
        }
        private void DefineMods()
        {
            m_SkillMod0 = new DefaultSkillMod(SkillName.AnimalTaming, true, 10);
            m_SkillMod1 = new DefaultSkillMod(SkillName.AnimalLore, true, 10);

        }

        private void SetMods(Mobile wearer)
        {
            wearer.AddSkillMod(m_SkillMod0);
            wearer.AddSkillMod(m_SkillMod1);

        }

        public override bool OnEquip(Mobile from)
        {
            SetMods(from);
            return true;
        }

        public override void OnRemoved(IEntity parent )
        {
            if (parent is Mobile)
            {
                Mobile m = (Mobile)parent;
                m.RemoveStatMod("Tamers Half");

                if (m_SkillMod0 != null)
                    m_SkillMod0.Remove();

                if (m_SkillMod1 != null)
                    m_SkillMod1.Remove();

            }
        }

        public override void OnSingleClick(Mobile from)
        {
            this.LabelTo(from, Name);
        }
        public TamersHalf( Serial serial ) : base( serial )
		{
            DefineMods();

            if (Parent != null && this.Parent is Mobile)
                SetMods((Mobile)Parent);
        }

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}
} 
