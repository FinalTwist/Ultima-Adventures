using Server;
using System;
using System.Collections;
using Server.Network;
using Server.Targeting;
using Server.Prompts;
using Server.Misc;

namespace Server.Items
{
	public class UnusualDyes : Item
	{
		public int DyeColor;
		
		[CommandProperty(AccessLevel.Owner)]
		public int Dye_Color { get { return DyeColor; } set { DyeColor = value; InvalidateProperties(); } }

		[Constructable]
		public UnusualDyes() : this( 1 )
		{
		}

		[Constructable]
		public UnusualDyes( int amount ) : base( 0x282F )
		{
			Weight = 0.01;
			Stackable = true;
			Amount = amount;
			switch( Utility.RandomMinMax( 0, 6 ) )
			{
				case 0: Name = "an odd jar of dye"; break;
				case 1: Name = "an unusual jar of dye"; break;
				case 2: Name = "a bizarre jar of dye"; break;
				case 3: Name = "a curious jar of dye"; break;
				case 4: Name = "a peculiar jar of dye"; break;
				case 5: Name = "a strange jar of dye"; break;
				case 6: Name = "a weird jar of dye"; break;
			}
			Hue = RandomThings.GetRandomSpecialColor();
			DyeColor = Hue;
		}

		public override void AddNameProperties( ObjectPropertyList list )
		{
			base.AddNameProperties( list );
			list.Add( 1070722, "Mix Into Dye Tubs" );
		}

		public override void OnDoubleClick( Mobile from )
		{
			Target t;

			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1060640 ); // The item must be in your backpack to use it.
			}
			else
			{
				from.SendMessage( "What dye tub do you want to mix this in?" );
				t = new DyeTarget( this );
				from.Target = t;
			}
		}

		private class DyeTarget : Target
		{
			private UnusualDyes m_Dye;

			public DyeTarget( UnusualDyes tube ) : base( 1, false, TargetFlags.None )
			{
				m_Dye = tube;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				int color = m_Dye.DyeColor;

				if ( targeted is DyeTub )
				{
					DyeTub tub = (DyeTub)targeted;

					if ( tub.Redyable )
					{
						tub.Hue = color;
						tub.DyedHue = color;
						from.RevealingAction();
						from.PlaySound( 0x23E );
						from.AddToBackpack( new Jar() );
						m_Dye.Consume();
					}
					else if ( tub is BlackDyeTub )
					{
						from.SendLocalizedMessage( 1010092 ); // You can not use this on a black dye tub.
					}
					else
					{
						from.SendMessage( "That dye tub may not be redyed." );
					}
				}
				else if ( targeted is MagicPigment )
				{
					MagicPigment pigment = (MagicPigment)targeted;
					pigment.Hue = color;
					from.RevealingAction();
					from.PlaySound( 0x23E );
					from.AddToBackpack( new Jar() );
					m_Dye.Consume();
				}
				else
				{
					from.SendLocalizedMessage( 500857 ); // Use this on a dye tub.
				}
			}
		}

		public UnusualDyes( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( ( int) 0 ); // version
            writer.Write( DyeColor );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
            DyeColor = reader.ReadInt();
		}
	}
}