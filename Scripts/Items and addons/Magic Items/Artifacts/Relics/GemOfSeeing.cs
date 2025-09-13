using System;
using Server.Mobiles;
using Server.Multis;
using Server.Targeting;
using Server.Regions;

namespace Server.Items 
{
	public enum GemOfSeeingEffect
	{
		Charges
	}

	public class GemOfSeeing : Item
	{
		private GemOfSeeingEffect m_GemOfSeeingEffect;
		private int m_Charges;

		[CommandProperty( AccessLevel.GameMaster )]
		public GemOfSeeingEffect Effect { get{ return m_GemOfSeeingEffect; } set{ m_GemOfSeeingEffect = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int Charges { get{ return m_Charges; } set{ m_Charges = value; InvalidateProperties(); } }

		[Constructable]
		public GemOfSeeing() : base( 0x4078 )
		{
			Weight = 1.0; 
			Charges = 50;
			Name = "Gem of Seeing";
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
			list.Add( 1049644, "Find Hidden Items And Traps" );
        } 
		
		public override void OnDoubleClick( Mobile from )
		{
			if ( from.InRange( this.GetWorldLocation(), 3 ) && Charges > 0 )
			{
				ConsumeCharge( from );
				from.SendLocalizedMessage( 500819 );
				from.Target = new InternalTarget();
				return;
			}
			else if ( !from.InRange( this.GetWorldLocation(), 3 ) && Charges > 0 )
			{
				from.SendLocalizedMessage( 502138 ); // That is too far away for you to use
				return;
			}
			else
			{
				from.SendMessage( "The gem has lost all of its magic." );
				this.Delete();
				return;
			}
		}

		public void ConsumeCharge( Mobile from )
		{
			--Charges;
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			list.Add( 1060584, m_Charges.ToString() );
		}

		private class InternalTarget : Target
		{
			public InternalTarget() : base( 12, true, TargetFlags.None )
			{
			}

			protected override void OnTarget( Mobile src, object targ )
			{
				bool foundAnyone = false;

				Point3D p;
				if ( targ is Mobile )
					p = ((Mobile)targ).Location;
				else if ( targ is Item )
					p = ((Item)targ).Location;
				else if ( targ is IPoint3D )
					p = new Point3D( (IPoint3D)targ );
				else 
					p = src.Location;

				double srcSkill = 100.00;
				int range = (int)(srcSkill / 10.0);

				BaseHouse house = BaseHouse.FindHouseAt( p, src.Map, 16 );

				bool inHouse = ( house != null && house.IsFriend( src ) );

				if ( inHouse )
					range = 22;

				if ( range > 0 )
				{
					IPooledEnumerable inRange = src.Map.GetMobilesInRange( p, range );

					foreach ( Mobile trg in inRange )
					{
						if ( trg.Hidden && src != trg )
						{
							double ss = srcSkill + Utility.Random( 21 ) - 10;
							double ts = trg.Skills[SkillName.Hiding].Value + Utility.Random( 21 ) - 10;

							if ( src.AccessLevel >= trg.AccessLevel && ( ss >= ts || ( inHouse && house.IsInside( trg ) ) ) )
							{
								trg.RevealingAction();
								trg.SendLocalizedMessage( 500814 ); // You have been revealed!
								foundAnyone = true;
							}
						}
					}

					inRange.Free();

					/// WIZARD WANTS THIS TO WORK FOR NORMAL TRAPS, HIDDEN TRAPS, & HIDDEN CONTAINERS ///
					IPooledEnumerable TitemsInRange = src.Map.GetItemsInRange( p, range );
					foreach ( Item item in TitemsInRange )
					{
						if ( Server.SkillHandlers.DetectHidden.DetectSomething( item, src, false ) )
							foundAnyone = true;
					}
					TitemsInRange.Free();
				}

				if ( !foundAnyone )
				{
					src.SendLocalizedMessage( 500817 ); // You can see nothing hidden there.
				}
			}
		}

		public GemOfSeeing( Serial serial ) : base( serial )
		{ 
		} 
		
		public override void Serialize( GenericWriter writer ) 
		{ 
			base.Serialize( writer ); 
			writer.Write( (int) 0 );
			writer.Write( (int) m_GemOfSeeingEffect );
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
					m_GemOfSeeingEffect = (GemOfSeeingEffect)reader.ReadInt();
					m_Charges = (int)reader.ReadInt();
					break;
				}
			}
		}
	}
}