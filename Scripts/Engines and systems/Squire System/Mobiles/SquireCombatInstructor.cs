using System;
using System.Collections.Generic;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	public class SquireCombatInstructor : BaseVendor
	{
		private List<SBInfo> m_SBInfos = new List<SBInfo>();
		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } }

		[Constructable]
		public SquireCombatInstructor() : base( "the squire combat instructor" )
		{
			SetSkill( SkillName.Fencing, 75.0, 85.0 );
			SetSkill( SkillName.Macing, 75.0, 85.0 );
			SetSkill( SkillName.Swords, 75.0, 85.0 );
			SetSkill( SkillName.Chivalry, 100.0 );
		}

		public override void InitSBInfo()
		{
			m_SBInfos.Add( new SBSquireCombatInstructor() );
		}

		public override void InitOutfit()
		{
			AddItem( new RingmailChest() );
			AddItem( new RingmailLegs() );
			AddItem( new RingmailArms() );
			AddItem( new RingmailGloves() );

			switch ( Utility.Random( 3 ) )
			{
				case 0: AddItem( new BodySash( 0x482 ) ); break;
				case 1: AddItem( new Doublet( 0x482 ) ); break;
				case 2: AddItem( new Tunic( 0x482 ) ); break;
			}

			AddItem( new Broadsword() );

			Item shield = new MetalKiteShield();

			shield.Hue = Utility.RandomNondyedHue();
			Utility.AssignRandomHair( this );

			AddItem( shield );

			switch ( Utility.Random( 2 ) )
			{
				case 0: AddItem( new Boots() ); break;
				case 1: AddItem( new ThighBoots() ); break;
			}

			PackGold( 100, 200 );
		}

		public SquireCombatInstructor( Serial serial ) : base( serial )
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