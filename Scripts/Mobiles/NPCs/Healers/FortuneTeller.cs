using System;
using System.Collections.Generic;
using Server;
using Server.Targeting;
using Server.Items;
using Server.Network;
using Server.ContextMenus;
using Server.Misc;
using Server.Mobiles;

namespace Server.Mobiles
{
	public class FortuneTeller : BaseVendor
	{
		private List<SBInfo> m_SBInfos = new List<SBInfo>();
		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } }

		[Constructable]
		public FortuneTeller() : base( "the fortune teller" )
		{
			SetSkill( SkillName.EvalInt, 65.0, 88.0 );
			SetSkill( SkillName.Forensics, 75.0, 98.0 );
			SetSkill( SkillName.Magery, 64.0, 100.0 );
			SetSkill( SkillName.Meditation, 60.0, 83.0 );
			SetSkill( SkillName.MagicResist, 65.0, 88.0 );
			SetSkill( SkillName.Wrestling, 60.0, 80.0 );
			SetSkill( SkillName.Necromancy, 64.0, 100.0 );
		}

		public override void InitSBInfo()
		{
			SBInfos.Add( new SBMage() );
			SBInfos.Add( new SBFortuneTeller() );
			m_SBInfos.Add( new SBBuyArtifacts() ); 

			if ( this.Map == Map.Felucca )
				m_SBInfos.Add( new SBElfWizard() );
		}

		public override void InitOutfit()
		{
			base.InitOutfit();

			AddItem( new Robe( RandomBrightHue() ) );

			switch ( Utility.Random( 3 ) )
			{
				case 0: AddItem( new SkullCap( RandomBrightHue() ) ); break;
				case 1: AddItem( new WizardsHat( RandomBrightHue() ) ); break;
				case 2: AddItem( new Bandana( RandomBrightHue() ) ); break;
			}

			AddItem( new Spellbook() );
		}

		public FortuneTeller( Serial serial ) : base( serial )
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