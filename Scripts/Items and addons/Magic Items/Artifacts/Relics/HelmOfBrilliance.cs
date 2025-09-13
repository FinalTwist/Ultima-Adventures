using System;
using Server.Network;
using Server.Items;
using Server.Targeting;
using Server.Spells.Third;
using Server.Spells;
using Server.Misc;

namespace Server.Items
{
	public enum HelmEffect
	{
		Charges
	}

	public class HelmOfBrilliance : NorseHelm
	{
		public override int InitMinHits{ get{ return 80; } }
		public override int InitMaxHits{ get{ return 160; } }

		private HelmEffect m_HelmEffect;
		private int m_Charges;

		[CommandProperty( AccessLevel.GameMaster )]
		public HelmEffect Effect { get{ return m_HelmEffect; } set{ m_HelmEffect = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int Charges { get{ return m_Charges; } set{ m_Charges = value; InvalidateProperties(); } }

		[Constructable]
		public HelmOfBrilliance()
		{
			Name = "Helm of Brilliance";
			Hue = 0x499;
			Attributes.NightSight = 1;
			FireBonus = 50;
			Charges = 50;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( Parent != from )
			{
				from.SendMessage( "You must be wearing the helm to unleash a fireball." );
			}
			else if ( Charges < 1 )
			{
				from.SendMessage( "That magic has been drained from the helm." );
			}
			else
			{
				ConsumeCharge( from );
				new FireballSpell( from, this ).Cast();
			}
			return;
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
			if ( Charges > 0 ){ list.Add( 1049644, "Use While Worn To Cast Fireball" ); }
        }

		public void ConsumeCharge( Mobile from )
		{
			--Charges;

			if ( Charges == 0 )
			{
				from.SendMessage( "That magic has been drained from the helm." );
			}
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			if ( Charges > 0 ){ list.Add( 1060584, m_Charges.ToString() ); }
		}

		public HelmOfBrilliance( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
			writer.Write( (int) m_HelmEffect );
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
					m_HelmEffect = (HelmEffect)reader.ReadInt();
					m_Charges = (int)reader.ReadInt();
					break;
				}
			}
			if ( Attributes.NightSight == 0 )
				Attributes.NightSight = 1;
		}
	}
}