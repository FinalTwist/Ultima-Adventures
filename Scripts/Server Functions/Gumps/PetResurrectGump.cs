using System;
using Server;
using Server.Mobiles;
using Server.Network;
using Server.Gumps;

namespace Server.Gumps
{
	public class PetResurrectGump : Gump
	{
		private BaseCreature m_Pet;
		private double m_HitsScalar;

		public PetResurrectGump( Mobile from, BaseCreature pet ) : this( from, pet, 0.0 )
		{
		}

		public PetResurrectGump( Mobile from, BaseCreature pet, double hitsScalar ) : base( 25, 25 )
		{
			from.CloseGump( typeof( PetResurrectGump ) );

			m_Pet = pet;
			m_HitsScalar = hitsScalar;

            this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;

			AddPage(0);
			AddImage(0, 0, 154);
			AddImage(300, 99, 154);
			AddImage(0, 99, 154);
			AddImage(300, 0, 154);
			AddImage(298, 97, 129);
			AddImage(2, 97, 129);
			AddImage(298, 2, 129);
			AddImage(2, 2, 129);
			AddImage(7, 6, 145);
			AddImage(5, 143, 142);
			AddImage(255, 171, 144);
			AddImage(171, 47, 132);
			AddImage(379, 8, 134);
			AddImage(167, 7, 156);
			AddImage(209, 11, 156);
			AddImage(189, 10, 156);
			AddImage(170, 44, 159);
			AddItem(156, 67, 2);
			AddItem(178, 67, 3);
			AddItem(185, 118, 3244);
			AddButton(146, 260, 4023, 4023, 0x1, GumpButtonType.Reply, 0);
			AddButton(374, 260, 4020, 4020, 0x2, GumpButtonType.Reply, 0);
			AddHtml( 267, 95, 224, 22, @"<BODY><BASEFONT Color=#FBFBFB><BIG>RESURRECTION</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 93, 163, 400, 76, @"<BODY><BASEFONT Color=#FCFF00><BIG>Wilt thou sanctify the resurrection of " + pet.Name + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddImage(36, 124, 162);
			AddImage(33, 131, 162);
			AddImage(45, 138, 162);
			AddImage(17, 135, 162);
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			if ( m_Pet.Deleted || !m_Pet.IsBonded || !m_Pet.IsDeadPet )
				return;

			Mobile from = state.Mobile;

			if ( info.ButtonID == 1 )
			{
				if ( m_Pet.Map == null || !m_Pet.Map.CanFit( m_Pet.Location, 16, false, false ) )
				{
					from.SendLocalizedMessage( 503256 ); // You fail to resurrect the creature.
					return;
				}
				else if( m_Pet.Region != null && m_Pet.Region.IsPartOf( "Khaldun" ) )	//TODO: Confirm for pets, as per Bandage's script.
				{
					from.SendLocalizedMessage( 1010395 ); // The veil of death in this area is too strong and resists thy efforts to restore life.
					return;
				}

				m_Pet.PlaySound( 0x214 );
				m_Pet.FixedEffect( 0x376A, 10, 16 );
				m_Pet.ResurrectPet();

				double decreaseAmount;

				if( from == m_Pet.ControlMaster )
					decreaseAmount = 0.1;
				else
					decreaseAmount = 0.2;

				for ( int i = 0; i < m_Pet.Skills.Length; ++i )	//Decrease all skills on pet.
					m_Pet.Skills[i].Base -= decreaseAmount;

				if( !m_Pet.IsDeadPet && m_HitsScalar > 0 )
					m_Pet.Hits = (int)(m_Pet.HitsMax * m_HitsScalar);
			}

		}
	}
}