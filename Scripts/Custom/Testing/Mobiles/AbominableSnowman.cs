//Scripted by Jumpnjahosofat

using System;
using System.Collections;
using Server.Items;
using Server.Spells;
using Server.Spells.Seventh;
using Server.Spells.Fifth;
using Server.Targeting;

namespace Server.Mobiles
{
	[CorpseName( "an abominable snowman's corpse" )]
	public class AbominableSnowman : BaseCreature
	{

		[Constructable]
		public AbominableSnowman () : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "an abominable snowman";
			Body = 54;
			BaseSoundID = 367;
			Hue = 0x481;

			SetStr( 767, 945 );
			SetDex( 66, 75 );
			SetInt( 46, 70 );

			SetHits( 2000, 2552 );

			SetDamage( 20, 25 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 45, 55 );
			SetResistance( ResistanceType.Fire, 30, 40 );
			SetResistance( ResistanceType.Cold, 30, 40 );
			SetResistance( ResistanceType.Poison, 40, 50 );
			SetResistance( ResistanceType.Energy, 40, 50 );

			SetSkill( SkillName.MagicResist, 125.1, 140.0 );
			SetSkill( SkillName.Tactics, 90.1, 100.0 );
			SetSkill( SkillName.Wrestling, 90.1, 100.0 );

			Fame = 15000;
			Karma = -15000;

			VirtualArmor = 50;

			PackItem( new Club() );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich, 2 );
		}

		public override bool CanRummageCorpses{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Regular; } }
		public override int TreasureMapLevel{ get{ return 3; } }
		public override int Meat{ get{ return 2; } }

		private static bool m_InHere;

      		private bool speak;

		public override void OnDamage( int amount, Mobile from, bool willKill ) 
		{
			if(Utility.RandomDouble() <= 0.20)
			{
				if ( from != null && from != this && !m_InHere )
				{
					MovingEffect( from, 0x36D4, 10, 0, false, false, 0x481, 0 );
					PlaySound( 0x229 );
           					speak=from.Squelched;
					from.FixedEffect( 0x37B9, 10, 5 );
					from.SendMessage( "you are frozen stiff!!" );
					from.Paralyze( TimeSpan.FromSeconds( 10.0 ) );
					from.Hidden = true;
					from.Warmode = false;
            					{ 
               						Point3D loc = new Point3D( from.X, from.Y, from.Z ); 

	               					Item item = new InternalItem( loc, from.Map, from,from , speak ); 

            					} 
				}
 			}
		}

		public AbominableSnowman( Serial serial ) : base( serial )
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
      		private class InternalItem : Item 
      		{ 
         			private Timer m_Timer; 
         			private DateTime m_End; 
      			private Mobile m_Owner;
      			private bool squeltched; 
			public InternalItem( Point3D loc, Map map, Mobile caster, Mobile m, bool talk ) : base( 0x2328 ) 

         			{ 
            				Visible = false; 
            				Movable = false; 
         				m_Owner=m;
         				squeltched=talk;

            				MoveToWorld( loc, map ); 

            				if ( caster.InLOS( this ) ) 
               				Visible = true; 
            				else 
               				Delete(); 

            				if ( Deleted ) 
               				return; 

            				m_Timer = new InternalTimer( this, TimeSpan.FromSeconds( 10.0 ), m_Owner, squeltched ); 
            				m_Timer.Start(); 

							m_End = DateTime.Now + TimeSpan.FromSeconds( 10.0 );
         			} 

         			public InternalItem( Serial serial ) : base( serial ) 
         			{ 
         			} 

         			public override void Serialize( GenericWriter writer ) 
         			{ 
            				base.Serialize( writer ); 

            				writer.Write( (int) 1 ); // version 

            				writer.Write( m_End - DateTime.UtcNow ); 
         				writer.Write(m_Owner);
         				writer.Write(squeltched);
         			} 

         			public override void Deserialize( GenericReader reader ) 
         			{ 
            				base.Deserialize( reader ); 

            				int version = reader.ReadInt(); 
         				m_Owner = reader.ReadMobile();
         				squeltched=reader.ReadBool();
         				if(m_Owner!=null)
         				{
         					m_Owner.Hidden=false;
         					m_Owner.Squelched=squeltched;
         				}
         				this.Delete();
         			} 

         			public override void OnAfterDelete() 
         			{ 
            				base.OnAfterDelete(); 

            				if ( m_Timer != null ) 
               				m_Timer.Stop();
         				if(m_Owner!=null)
         				m_Owner.Squelched=squeltched;
         			} 

         			private class InternalTimer : Timer 
         			{ 
            				private InternalItem m_Item; 
         				private Mobile m_Owner;
         				private bool speak;

            				public InternalTimer( InternalItem item, TimeSpan duration, Mobile caster, bool talk ) : base( duration ) 
            				{ 
               					m_Item = item; 
            					m_Owner=caster;
            					speak=talk;
            				} 

            				protected override void OnTick() 
            				{ 
               					m_Item.Delete(); 
            					m_Owner.Squelched=speak;
            					m_Owner.Hidden=false;
            				} 
         			} 
      		} 
  	} 
}