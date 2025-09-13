using System;
using Server.Network;
using Server.Items;
using Server.Targeting;
using Server.Spells.Third;
using Server.Spells;
using Server.Misc;

namespace Server.Items
{
	public enum TeleportRobeEffect
	{
		Charges
	}

	public class RobeOfTeleportation : Robe
	{
		private TeleportRobeEffect m_TeleportRobeEffect;
		private int m_Charges;

		[CommandProperty( AccessLevel.GameMaster )]
		public TeleportRobeEffect Effect { get{ return m_TeleportRobeEffect; } set{ m_TeleportRobeEffect = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int Charges { get{ return m_Charges; } set{ m_Charges = value; InvalidateProperties(); } }

		[Constructable]
		public RobeOfTeleportation()
		{
			Name = "Robe Of Teleportation";
			Hue = RandomThings.GetRandomColor( 0 );
			Charges = 200;
			Attributes.BonusDex = 10;
			Attributes.AttackChance = 20;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( Parent != from )
			{
				from.SendMessage( "You must be wearing the robe to teleport." );
			}
			else if ( Charges < 1 )
			{
				from.SendMessage( "All of the magic has been drained from the robe." );
				from.AddToBackpack( new Robe(this.Hue) );
				this.Delete();
			}
			else
			{
				ConsumeCharge( from );
				new TeleportSpell( from, this ).Cast();
			}
			return;
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
			list.Add( 1049644, "Use While Worn To Teleport" );
        }

		public void ConsumeCharge( Mobile from )
		{
			--Charges;

			if ( Charges == 0 )
			{
				from.SendMessage( "All of the magic has been drained from the robe." );
				from.AddToBackpack( new Robe(this.Hue) );
				this.Delete();
			}
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			list.Add( 1060584, m_Charges.ToString() );
		}

		public RobeOfTeleportation( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			writer.Write( (int) m_TeleportRobeEffect );
			writer.Write( (int) m_Charges );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			switch ( version )
			{
				case 0:
				{
					m_TeleportRobeEffect = (TeleportRobeEffect)reader.ReadInt();
					m_Charges = (int)reader.ReadInt();
					break;
				}
			}
		}
	}
}
