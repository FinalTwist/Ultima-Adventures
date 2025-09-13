using System;
using Server;
using Server.Mobiles;
using Server.Targeting;
using System.Collections;
using Server.Network;
using System.Text;
using Server.Items;
using System.Collections.Generic;
using Server.ContextMenus;
using Server.Misc;

namespace Server.Items
{
	public class HueStone : Item
	{
		public int mNCharges = 0;

		[CommandProperty(AccessLevel.GameMaster)]
		public int NCharges { get { return mNCharges; } set { mNCharges = value; } }

		public string mNType = "Illusionist Stone";

		[CommandProperty(AccessLevel.GameMaster)]
		public string NType { get { return mNType; } set { mNType = value; } }

		[Constructable]
		public HueStone() : base( 0x2FF6 )
		{
			NCharges = 0;
			Name = "Illusionist Stone";
			NType = "Illusionist Stone";
			Movable = false;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( from.InRange( this.GetWorldLocation(), 2 ) )
			{
				string sCount = "charges";
				if ( NCharges == 1 ){ sCount = "charge"; }
				from.SendMessage( "This stone has " + NCharges + " " + sCount + "." );

				if ( NCharges > 0 )
				{
					from.SendMessage( "Choose an item you wish to change to the same color as the illusionist stone." );
					from.SendMessage( "The item must be in your pack to change it`s color." );
					from.Target = new WHueTarget( this );
				}
				else
				{
					from.SendMessage( "There is not enough charges to use this illusionist stone." );
					from.SendMessage( "You must target 7500 gold to charge the stone." );
					from.Target = new WHueTarget( this );
				}
			}
			else
			{
				from.SendLocalizedMessage( 502138 ); // That is too far away for you to use
			}
		}

		private class WHueTarget : Target
		{
			private HueStone m_Dye;

			public WHueTarget( HueStone whue ) : base( 1, false, TargetFlags.None )
			{
				m_Dye = whue;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				object obj = targeted;

				string sCownt = "Charges";

				if ( obj is HueStone )
				{
					Item iStone = targeted as Item;

						from.RevealingAction();
						from.PlaySound( 0x1FA );


					if ( iStone.Hue == 0 ){          iStone.Hue = 0x483; iStone.Name = "Illusionist Stone ( dark green )"; }
					else if ( iStone.Hue == 0x483 ){ iStone.Hue = 0x484; iStone.Name = "Illusionist Stone ( dark blue )"; }
					else if ( iStone.Hue == 0x484 ){ iStone.Hue = 0x485; iStone.Name = "Illusionist Stone ( dark red )"; }
					else if ( iStone.Hue == 0x485 ){ iStone.Hue = 0x486; iStone.Name = "Illusionist Stone ( dark purple )"; }
					else if ( iStone.Hue == 0x486 ){ iStone.Hue = 0x487; iStone.Name = "Illusionist Stone ( dark yellow )"; }
					else if ( iStone.Hue == 0x487 ){ iStone.Hue = 0x488; iStone.Name = "Illusionist Stone ( dark bluegreen )"; }
					else if ( iStone.Hue == 0x488 ){ iStone.Hue = 0x48A; iStone.Name = "Illusionist Stone ( blue green )"; }
					else if ( iStone.Hue == 0x48A ){ iStone.Hue = 0x48B; iStone.Name = "Illusionist Stone ( purple green )"; }
					else if ( iStone.Hue == 0x48B ){ iStone.Hue = 0x48C; iStone.Name = "Illusionist Stone ( green blue )"; }
					else if ( iStone.Hue == 0x48C ){ iStone.Hue = 0x493; iStone.Name = "Illusionist Stone ( green purple )"; }
					else if ( iStone.Hue == 0x493 ){ iStone.Hue = 0x494; iStone.Name = "Illusionist Stone ( blue red )"; }
					else if ( iStone.Hue == 0x494 ){ iStone.Hue = 0x495; iStone.Name = "Illusionist Stone ( bright blue )"; }
					else if ( iStone.Hue == 0x495 ){ iStone.Hue = 0x497; iStone.Name = "Illusionist Stone ( darkness )"; }
					else if ( iStone.Hue == 0x497 ){ iStone.Hue = 0x498; iStone.Name = "Illusionist Stone ( blue darkness )"; }
					else if ( iStone.Hue == 0x498 ){ iStone.Hue = 0;     iStone.Name = "Illusionist Stone ( colorless )"; }

					else if ( iStone.Hue == 0xB96 ){ iStone.Hue = 0x47E; iStone.Name = "Illusionist Stone ( winter snow )"; }
					else if ( iStone.Hue == 0x47E ){ iStone.Hue = 0x47F; iStone.Name = "Illusionist Stone ( winter ice )"; }
					else if ( iStone.Hue == 0x47F ){ iStone.Hue = 0x480; iStone.Name = "Illusionist Stone ( solid ice )"; }
					else if ( iStone.Hue == 0x480 ){ iStone.Hue = 0x481; iStone.Name = "Illusionist Stone ( snow white )"; }
					else if ( iStone.Hue == 0x481 ){ iStone.Hue = 0x482; iStone.Name = "Illusionist Stone ( dark snow )"; }
					else if ( iStone.Hue == 0x482 ){ iStone.Hue = 0x4AB; iStone.Name = "Illusionist Stone ( frost )"; }
					else if ( iStone.Hue == 0x4AB ){ iStone.Hue = 0xB83; iStone.Name = "Illusionist Stone ( jade )"; }
					else if ( iStone.Hue == 0xB83 ){ iStone.Hue = 0xB93; iStone.Name = "Illusionist Stone ( darker jade )"; }
					else if ( iStone.Hue == 0xB93 ){ iStone.Hue = 0xB94; iStone.Name = "Illusionist Stone ( dark jade )"; }
					else if ( iStone.Hue == 0xB94 ){ iStone.Hue = 0xB95; iStone.Name = "Illusionist Stone ( medium jade )"; }
					else if ( iStone.Hue == 0xB95 ){ iStone.Hue = 0xB96; iStone.Name = "Illusionist Stone ( light jade )"; }

					else if ( iStone.Hue == 0x492 ){ iStone.Hue = 0x48F; iStone.Name = "Illusionist Stone ( green light )"; }
					else if ( iStone.Hue == 0x48F ){ iStone.Hue = 0x490; iStone.Name = "Illusionist Stone ( purple light )"; }
					else if ( iStone.Hue == 0x490 ){ iStone.Hue = 0x491; iStone.Name = "Illusionist Stone ( brown light )"; }
					else if ( iStone.Hue == 0x491 ){ iStone.Hue = 0x48D; iStone.Name = "Illusionist Stone ( ice light )"; }
					else if ( iStone.Hue == 0x48D ){ iStone.Hue = 0x48E; iStone.Name = "Illusionist Stone ( fire light )"; }
					else if ( iStone.Hue == 0x48E ){ iStone.Hue = 0x499; iStone.Name = "Illusionist Stone ( gold )"; }
					else if ( iStone.Hue == 0x499 ){ iStone.Hue = 0x4AA; iStone.Name = "Illusionist Stone ( rose red )"; }
					else if ( iStone.Hue == 0x4AA ){ iStone.Hue = 0x4AC; iStone.Name = "Illusionist Stone ( sun )"; }
					else if ( iStone.Hue == 0x4AC ){ iStone.Hue = 0x489; iStone.Name = "Illusionist Stone ( fire )"; }
					else if ( iStone.Hue == 0x489 ){ iStone.Hue = 0x496; iStone.Name = "Illusionist Stone ( blaze )"; }
					else if ( iStone.Hue == 0x496 ){ iStone.Hue = 0x492; iStone.Name = "Illusionist Stone ( slickness )"; }

					else if ( iStone.Hue == 0x9C3 ){ iStone.Hue = 0x7E3; iStone.Name = "Illusionist Stone ( nightmare )"; }
					else if ( iStone.Hue == 0x7E3 ){ iStone.Hue = 0x1;   iStone.Name = "Illusionist Stone ( pitch black )"; }
					else if ( iStone.Hue == 0x1 ){   iStone.Hue = 0x81C; iStone.Name = "Illusionist Stone ( moonlight )"; }
					else if ( iStone.Hue == 0x81C ){ iStone.Hue = 0x81B; iStone.Name = "Illusionist Stone ( dark nights )"; }
					else if ( iStone.Hue == 0x81B ){ iStone.Hue = 0xB97; iStone.Name = "Illusionist Stone ( necrotic flesh )"; }
					else if ( iStone.Hue == 0xB97 ){ iStone.Hue = 0x6F3; iStone.Name = "Illusionist Stone ( bloody hell )"; }
					else if ( iStone.Hue == 0x6F3 ){ iStone.Hue = 0xB85; iStone.Name = "Illusionist Stone ( bloodstone )"; }
					else if ( iStone.Hue == 0xB85 ){ iStone.Hue = 0x5B5; iStone.Name = "Illusionist Stone ( dark blood )"; }
					else if ( iStone.Hue == 0x5B5 ){ iStone.Hue = 0x9C2; iStone.Name = "Illusionist Stone ( ghostly )"; }
					else if ( iStone.Hue == 0x9C2 ){ iStone.Hue = 0x9C3; iStone.Name = "Illusionist Stone ( ghostly bright )"; }

					else {				 iStone.Hue = 0;     iStone.Name = "Illusionist Stone ( colorless )"; }

					m_Dye.NType = iStone.Name;

					from.SendMessage( "The Illusionist Stone magically changed color." );
				}
				else if ( obj is Item )
				{
					Item iDye = targeted as Item;

					if ( !iDye.IsChildOf( from.Backpack ) )
					{
						from.SendMessage( "You can only use this stone on things in your pack." );
					}
					else if ( ( iDye is Gold ) && ( iDye.Amount > 7499 ) )
					{
						m_Dye.NCharges = m_Dye.NCharges + 1;

						if ( iDye.Amount == 7500 )
						{
							iDye.Delete();
						}
						else
						{ 
							iDye.Amount = iDye.Amount - 7500;
						}

						from.RevealingAction();
						from.PlaySound( 0x2E6 );
						string sCount = "charges";
							if ( m_Dye.NCharges == 1 ){ sCount = "charge"; sCownt = "Charge"; }
						from.PrivateOverheadMessage( MessageType.Regular, 0x44, false, "This stone now has " + m_Dye.NCharges.ToString() + " " + sCount + ".", from.NetState );
					}
					else if ( ( iDye is Gold ) && ( iDye.Amount < 5000 ) )
					{
						from.PrivateOverheadMessage( MessageType.Regular, 0x44, false, "I do not have enough gold to charge this stone.", from.NetState );
					}
					else if ( ( iDye.IsChildOf( from.Backpack ) ) && ( m_Dye.NCharges > 0 ) )
					{
						if ( iDye is DDRelicRugAddonDeed ) // WIZARD ADDED TO DYE RELIC RUGS
						{
							DDRelicRugAddonDeed relic = (DDRelicRugAddonDeed)iDye;
							relic.RelicColor = m_Dye.Hue;
						}
						else if ( ( iDye is MyTentEastAddonDeed ) || ( iDye is MyTentSouthAddonDeed ) ) // WIZARD ADDED TO DYE TENTS
						{
							if ( iDye is MyTentEastAddonDeed ) { MyTentEastAddonDeed tent = (MyTentEastAddonDeed)iDye; tent.TentColor = m_Dye.Hue; }
							else { MyTentSouthAddonDeed tent = (MyTentSouthAddonDeed)iDye; tent.TentColor = m_Dye.Hue; }
						}
						else if ( ( iDye is MyCircusTentEastAddonDeed ) || ( iDye is MyCircusTentSouthAddonDeed ) ) // WIZARD ADDED TO DYE TENTS
						{
							if ( iDye is MyCircusTentEastAddonDeed ) { MyCircusTentEastAddonDeed tent = (MyCircusTentEastAddonDeed)iDye; tent.TentColor1 = m_Dye.Hue; }
							else { MyCircusTentSouthAddonDeed tent = (MyCircusTentSouthAddonDeed)iDye; tent.TentColor1 = m_Dye.Hue; }
						}
						iDye.Hue = m_Dye.Hue;
						from.RevealingAction();
						from.PlaySound( 0x1FA );
						m_Dye.NCharges = m_Dye.NCharges - 1;
					}
					else if ( m_Dye.NCharges < 1 )
					{
						from.PrivateOverheadMessage( MessageType.Regular, 0x44, false, "This illusionist stone has no charges.", from.NetState );
					}
					else
					{
						from.SendMessage( "You cannot use the stone on that." );
					}
				}
				else
				{
					from.SendMessage( "You cannot use the stone on that." );
				}
			}
		}

		public override void AddNameProperties( ObjectPropertyList list )
		{
			base.AddNameProperties( list );
			list.Add( 1070722, "Double Click To Use. Target This Stone To Change It`s Color, Gold To Charge The Stone, Or An Item In Your Pack To Change It`s Color." );
			list.Add( 1049644, "5000 Gold Per Charge - This Stone Changes Items To Match It`s Color." );
		}

		public HueStone( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			writer.Write( mNCharges );
				if ( Hue == 0 ) { NType = "Illusionist Stone ( colorless )"; }
				else if ( Hue == 0x47E ) { NType = "Illusionist Stone ( winter snow )"; }
				else if ( Hue == 0x48F ) { NType = "Illusionist Stone ( green light )"; }
				else if ( Hue == 0x7E3 ) { NType = "Illusionist Stone ( nightmare )"; }
			writer.Write( mNType );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			mNCharges = reader.ReadInt();
			mNType = reader.ReadString();
			Name = NType;
		}
	}
}