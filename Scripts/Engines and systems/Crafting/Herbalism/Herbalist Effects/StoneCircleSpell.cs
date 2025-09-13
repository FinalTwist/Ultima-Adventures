using System;
using System.Collections;
using Server.Misc;
using Server.Network;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;

namespace Server.Spells.Herbalist
{
	public class StoneCircleSpell : HerbalistSpell
	{
		private InternalItem m_Circlea;
		private static SpellInfo m_Info = new SpellInfo( "", "", 239, 9021 );
		public override int HerbalistSpellCircle{ get{ return 4; } }
		public override double CastDelay{ get{ return 3.0; } }
		public override double RequiredSkill{ get{ return 30.0; } }
		public override int RequiredMana{ get{ return 0; } }
		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 1.0 ); } }

		public StoneCircleSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

		public void Target( IPoint3D p )
		{
			if ( !Caster.CanSee( p ) )
			{
				Caster.SendLocalizedMessage( 500237 ); // Target can not be seen.
			}
			else if ( SpellHelper.CheckTown( p, Caster ) && CheckSequence() )
			{
				SpellHelper.Turn( Caster, p );
				SpellHelper.GetSurfaceTop( ref p );
				Effects.PlaySound( p, Caster.Map, 0x222 );
				Point3D loc = new Point3D( p.X, p.Y, p.Z );
               	int mushx;
				int mushy;
				int mushz;
              	InternalItem firstFlamea = new InternalItem( Caster.Location, Caster.Map, Caster );
				mushx=loc.X-2;
				mushy=loc.Y-2;
				mushz=loc.Z;
				Point3D mushxyz = new Point3D(mushx,mushy,mushz);
				firstFlamea.MoveToWorld( mushxyz, Caster.Map );
				InternalItem firstFlamec = new InternalItem( Caster.Location, Caster.Map, Caster );
				mushx=loc.X;
				mushy=loc.Y-3;
				mushz=loc.Z;
				Point3D mushxyzb = new Point3D(mushx,mushy,mushz);
				firstFlamec.MoveToWorld( mushxyzb, Caster.Map );
				InternalItem firstFlamed = new InternalItem( Caster.Location, Caster.Map, Caster );
				mushx=loc.X+2;
				mushy=loc.Y-2;
				mushz=loc.Z;
				Point3D mushxyzc = new Point3D(mushx,mushy,mushz);
				firstFlamed.MoveToWorld( mushxyzc, Caster.Map );
				InternalItem hiddenflame = new InternalItem( Caster.Location, Caster.Map, Caster );
				mushx=loc.X+2;
				mushy=loc.Y-1;
				mushz=loc.Z;
				Point3D mushxyzhid = new Point3D(mushx,mushy,mushz);
				hiddenflame.MoveToWorld( mushxyzhid, Caster.Map );
				InternalItem hiddenrock = new InternalItem( Caster.Location, Caster.Map, Caster );
				mushx=loc.X+2;
				mushy=loc.Y+1;
				mushz=loc.Z;
				Point3D rockaxyz = new Point3D(mushx,mushy,mushz);
				hiddenrock.MoveToWorld( rockaxyz, Caster.Map );
				InternalItem hiddenflamea = new InternalItem( Caster.Location, Caster.Map, Caster );
				mushx=loc.X-2;
				mushy=loc.Y-1;
				mushz=loc.Z;
				Point3D mushxyzhida = new Point3D(mushx,mushy,mushz);
				hiddenflamea.MoveToWorld( mushxyzhida, Caster.Map );
				InternalItem hiddenrocks = new InternalItem( Caster.Location, Caster.Map, Caster );
				mushx=loc.X-2;
				mushy=loc.Y+1;
				mushz=loc.Z;
				Point3D rocksaxyz = new Point3D(mushx,mushy,mushz);
				hiddenrocks.MoveToWorld( rocksaxyz, Caster.Map );
				InternalItem hiddenrocka = new InternalItem( Caster.Location, Caster.Map, Caster );
				mushx=loc.X+1;
				mushy=loc.Y+2;
				mushz=loc.Z;
				Point3D rockbxyz = new Point3D(mushx,mushy,mushz);
				hiddenrocka.MoveToWorld( rockbxyz, Caster.Map );
				InternalItem hiddenrockb = new InternalItem( Caster.Location, Caster.Map, Caster );
				mushx=loc.X+1;
				mushy=loc.Y-2;
				mushz=loc.Z;
				Point3D rockcxyz = new Point3D(mushx,mushy,mushz);
				hiddenrockb.MoveToWorld( rockcxyz, Caster.Map );
				InternalItem hiddenrockc = new InternalItem( Caster.Location, Caster.Map, Caster );
				mushx=loc.X-1;
				mushy=loc.Y-2;
				mushz=loc.Z;
				Point3D rockdxyz = new Point3D(mushx,mushy,mushz);
				hiddenrockc.MoveToWorld( rockdxyz, Caster.Map );
				InternalItem hiddenrockd = new InternalItem( Caster.Location, Caster.Map, Caster );
				mushx=loc.X-1;
				mushy=loc.Y+2;
				mushz=loc.Z;
				Point3D rockexyz = new Point3D(mushx,mushy,mushz);
				hiddenrockd.MoveToWorld( rockexyz, Caster.Map );
				InternalItem firstFlamee = new InternalItem( Caster.Location, Caster.Map, Caster );
				mushx=loc.X+3;
				mushy=loc.Y;
				mushz=loc.Z;
				Point3D mushxyzd = new Point3D(mushx,mushy,mushz);
				firstFlamee.MoveToWorld( mushxyzd, Caster.Map );
				InternalItem firstFlamef = new InternalItem( Caster.Location, Caster.Map, Caster );
				mushx=loc.X+2;
				mushy=loc.Y+2;
				mushz=loc.Z;
				Point3D mushxyze = new Point3D(mushx,mushy,mushz);
				firstFlamef.MoveToWorld( mushxyze, Caster.Map );
				InternalItem firstFlameg = new InternalItem( Caster.Location, Caster.Map, Caster );
				mushx=loc.X;
				mushy=loc.Y+3;
				mushz=loc.Z;
				Point3D mushxyzf = new Point3D(mushx,mushy,mushz);
				firstFlameg.MoveToWorld( mushxyzf, Caster.Map );
				InternalItem firstFlameh = new InternalItem( Caster.Location, Caster.Map, Caster );
				mushx=loc.X-2;
				mushy=loc.Y+2;
				mushz=loc.Z;
				Point3D mushxyzg = new Point3D(mushx,mushy,mushz);
				firstFlameh.MoveToWorld( mushxyzg, Caster.Map );
         		InternalItem firstFlamei = new InternalItem( Caster.Location, Caster.Map, Caster );
				mushx=loc.X-3;
				mushy=loc.Y;
				mushz=loc.Z;
				Point3D mushxyzh = new Point3D(mushx,mushy,mushz);
				firstFlamei.MoveToWorld( mushxyzh, Caster.Map );  
            }
			FinishSequence();
		}
    
		[DispellableField]
		private class InternalItem : Item
		{
			private Timer m_Timer;
			private DateTime m_End;
			private Mobile m_Caster;
			private ArrayList frozen;

			public override bool BlocksFit{ get{ return true; } }

			public InternalItem( Point3D loc, Map map, Mobile caster ) : base( 0x08E2 )
			{
				Visible = false;
				Movable = false;
				ItemID=Utility.RandomList(2274,2275,2272,2273,2279,2280);
				Name="stone";
				MoveToWorld( loc, map );
				m_Caster=caster;

				if ( caster.InLOS( this ) )
					Visible = true;
				else
					Delete();

				if ( Deleted )
					return;

				m_Timer = new InternalTimer( this, TimeSpan.FromSeconds( 30.0 ) );
				m_Timer.Start();
				m_End = DateTime.UtcNow + TimeSpan.FromSeconds( 30.0 );
			}

			public InternalItem( Serial serial ) : base( serial )
			{
			}

			public override bool OnMoveOver( Mobile m )
			{
				m.SendMessage("The magic of the stones prevents you from crossing.");
				return false;
			}

			public override void Serialize( GenericWriter writer )
			{
				base.Serialize( writer );
				writer.Write( (int) 1 ); // version
				writer.Write( m_End - DateTime.UtcNow );
			}

			public override void Deserialize( GenericReader reader )
			{
				base.Deserialize( reader );

				int version = reader.ReadInt();

				switch ( version )
				{
					case 1:
					{
						TimeSpan duration = reader.ReadTimeSpan();
						m_Timer = new InternalTimer( this, duration );
						m_Timer.Start();
						m_End = DateTime.UtcNow + duration;
						break;
					}
					case 0:
					{
						TimeSpan duration = TimeSpan.FromSeconds( 10.0 );
						m_Timer = new InternalTimer( this, duration );
						m_Timer.Start();
						m_End = DateTime.UtcNow + duration;
						break;
					}
				}
			}

			public override void OnAfterDelete()
			{
				base.OnAfterDelete();

				if ( m_Timer != null )
					m_Timer.Stop();
			}

			private class InternalTimer : Timer
			{
				private InternalItem m_Item;

				public InternalTimer( InternalItem item, TimeSpan duration ) : base( duration )
				{
					m_Item = item;
				}

				protected override void OnTick()
				{
					m_Item.Delete();
				}
			}
		}

		private class InternalTarget : Target
		{
			private StoneCircleSpell m_Owner;

			public InternalTarget( StoneCircleSpell owner ) : base( 12, true, TargetFlags.None )
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is IPoint3D )
				m_Owner.Target( (IPoint3D)o );
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}
	}
}
