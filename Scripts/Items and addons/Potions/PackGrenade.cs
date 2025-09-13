// Created by FinalTwist and Gadget2013 
// potion that explodes (with countdown) whenever it is placed in a player's backpack.
// Will not blow up when placed inside a bag or pouch (so people can carry them.
// great for thieves and stealthers.

using System;
using Server;
using Server.Mobiles;
using Server.Network;

namespace Server.Items
{
	public class PackGrenade : BaseExplosionPotion
	{
		public override int MinDamage { get { return 20; } }
		public override int MaxDamage { get { return 90; } }

		private Timer m_exploTimer;
		private bool activated = false;

		[Constructable]
		public PackGrenade() : base( PotionEffect.Explosion )
		{
			Hue = 2110;
		}

		public override void OnAdded(IEntity parent )
		{
		    if (this.RootParentEntity is Mobile) 
			{
				if (this.Parent is Bag || this.Parent is Pouch )
					return;
				
				Mobile victim = (Mobile)this.RootParentEntity;
				m_exploTimer = Timer.DelayCall( TimeSpan.FromSeconds( 1.0 ), TimeSpan.FromSeconds( 1.25 ), 5, new TimerStateCallback( detonate ), new object[]{ victim, 4 } ); 
				this.Hue = 2160;
				activated = true;
				if (victim is PlayerMobile)
				{
					victim.PrivateOverheadMessage( MessageType.Regular, 2160, false, "Something burns in your pack!", victim.NetState );
				}
			}
		}

		private void detonate( object state )
		{
			if ( Deleted )
				return;

			object[] states = (object[])state;
			Mobile from = (Mobile)states[0];
			int timer = (int)states[1];

			object parent = FindParent( from );

			if ( parent != null && parent is Mobile )
			{

				if ( timer == 0 )
				{
					packexplode(from);
					m_exploTimer = null;
				}
				else
				{
					((Mobile)parent).PublicOverheadMessage( MessageType.Regular, 0x22, false, timer.ToString() );
					states[1] = timer - 1;
				}
			}
			else if ( activated )
			{
				// moved out of the pack, etc.

				Effects.PlaySound(new Point3D ( this ), this.Map, 0x307);
				Effects.SendLocationParticles(this, 0x36BD, 20, 10, 5044);
				this.Delete(); // blow up immediately
			}
		}			

		private void packexplode(Mobile victim)
		{
			Point3D loc = new Point3D( victim );
			Map map = victim.Map;
			victim.FixedParticles(0x36BD, 20, 10, 5044, EffectLayer.Head);
			Effects.PlaySound(loc, map, 0x307);
			int damage = Utility.RandomMinMax( MinDamage, MaxDamage );
			AOS.Damage( victim, victim, damage, 0, 100, 0, 0, 0 );
			this.Delete();
		}

		public PackGrenade( Serial serial ) : base( serial )
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
