/* Created by Hammerhand*/

using System;
using System.Collections;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
	[CorpseName( "William Silverdales corpse" )]
	public class WilliamSilverdale : Mobile
	{
		
		private bool m_IsCured;
	
		[CommandProperty( AccessLevel.GameMaster )]
		public bool IsCured 		
		{
            get { return m_IsCured; }
			set
			{
                m_IsCured = value;

                if (m_IsCured)
				{					
					InitBody();
					InitOutfit();
					new InternalTimer( this ).Start();					
				}
				else
				{
					Hue = 0;
					Body = 0x3CA;					
				}

				InvalidateProperties();
			}
		}
		
		[Constructable]
		public WilliamSilverdale() 
		{
			Name = "the shade of William Silverdale";
			Body = 0x3CA; 
			Hue = 0;
            AddItem(new Server.Items.DeathShroud());
		}	

		public WilliamSilverdale( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
		
		public virtual void InitBody()
		{

			Hue = Utility.RandomSkinHue();
			Body = 400;
            Name = "William Silverdale";

            Fame = 25000;
            Karma = 25000;
			}
		

		public virtual void InitOutfit()
		{

			AddItem( new FancyShirt( Utility.RandomBlueHue() ) );

			int lowHue = Utility.RandomBlueHue();

			AddItem( new ShortPants( lowHue ) );
			AddItem( new Boots( lowHue ) );
			AddItem( new Cloak( Utility.RandomBlueHue() ) );

			AddItem( new Longsword() );


			}
		}
		
		public class InternalTimer : Timer
		{
			public Mobile m_WilliamSilverdale;

			public InternalTimer( Mobile williamsilverdale ) : base( TimeSpan.FromSeconds( 5.0 ) )
			{
				Priority = TimerPriority.OneSecond;

				m_WilliamSilverdale = williamsilverdale;
			}

			protected override void OnTick()
			{
				m_WilliamSilverdale.Delete();
			}
		}
	}

