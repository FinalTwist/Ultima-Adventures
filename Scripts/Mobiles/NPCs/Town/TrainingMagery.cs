using System;
using Server;
using Server.ContextMenus;
using System.Collections;
using System.Collections.Generic;
using Server.Network;
using System.Text;
using Server.Items;
using Server.Mobiles;
using Server.Targeting;

namespace Server.Mobiles
{
	public class TrainingMagery : Citizens
	{
		[Constructable]
		public TrainingMagery()
		{
			Server.Misc.MorphingTime.RemoveMyClothes( this );
			Server.Misc.IntelligentAction.DressUpWizards( this );
			CitizenType = 1;
			if ( Backpack != null ){ Backpack.Delete(); }
			SetupCitizen();
			Blessed = true;
			CantWalk = true;
			AI = AIType.AI_Melee;
		}

		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
		}

		public override void OnThink()
		{
			if ( DateTime.UtcNow >= m_NextTalk )
			{
				foreach ( Item pentagram in this.GetItemsInRange( 4 ) )
				{
					if ( pentagram is MagicHit )
					{
						if ( this.FindItemOnLayer( Layer.FirstValid ) != null ) { this.Delete(); }
						else if ( this.FindItemOnLayer( Layer.TwoHanded ) != null ) { this.Delete(); }
						else if ( this.FindItemOnLayer( Layer.OneHanded ) != null ) { this.Delete(); }
						pentagram.OnDoubleClick( this );
						m_NextTalk = (DateTime.UtcNow + TimeSpan.FromSeconds( Utility.RandomMinMax( 10, 30 ) ));
					}
				}
			}
		}

		public override void OnAfterSpawn()
		{
			base.OnAfterSpawn();
			Server.Misc.TavernPatrons.RemoveSomeGear( this, false );
			Server.Misc.MorphingTime.CheckNecromancer( this );
			Server.Items.EssenceBase.ColorCitizen( this );
		}

		public TrainingMagery( Serial serial ) : base( serial )
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

namespace Server.Items
{
	public class MagicHit : Item
	{
		[Constructable]
		public MagicHit() : base( 0x1B72 )
		{
			Name = "magic target";
			Visible = false;
			Movable = false;
		}

		public MagicHit( Serial serial ) : base( serial )
		{
		}

		public override void OnAfterSpawn()
		{
			base.OnAfterSpawn();
			Mobile mSp = new SpellCritter();
			mSp.MoveToWorld(new Point3D(this.X, this.Y, this.Z), this.Map);
		}

		public override void OnDoubleClick( Mobile from )
		{
			Mobile mSp = null;
			foreach ( Mobile mouse in this.GetMobilesInRange( 1 ) )
			{
				if ( mouse is SpellCritter )
				{
					mSp = mouse;
				}
			}
			if ( mSp != null )
			{
				from.Direction = from.GetDirectionTo( GetWorldLocation() );

				string[] chant = new string[] {"Ahm","An","Beh","Bet","Cah","Corp","Des","Ex","Flam","Grav","Hur","In","Jux","Kal","Lor","Lum","Mani","Mu","Nox","Om","Ort","Por","Quas","Ra","Rel","Sanct","Summ","Tym","Uus","Vas","Wis","Xen","Ylem","Zu"};
					string pray_chant_1 = chant[Utility.RandomMinMax( 0, (chant.Length-1) )];
					string pray_chant_2 = chant[Utility.RandomMinMax( 0, (chant.Length-1) )];
					string pray_chant_3 = chant[Utility.RandomMinMax( 0, (chant.Length-1) )];
					string pray_chant_4 = ""; if ( Utility.RandomBool() ){ pray_chant_4 = " " + chant[Utility.RandomMinMax( 0, (chant.Length-1) )]; }

				string pray_chant = pray_chant_1 + " " + pray_chant_2 + " " + pray_chant_3 + pray_chant_4;
				from.Say( pray_chant );
				from.Animate( Utility.RandomList( 236, 212, 230, 224, 209, 221, 233, 263, 245, 218, 206 ), 5, 1, true, false, 0 );

				int circle = Utility.RandomMinMax( 1, 8 );
				int spells = Utility.RandomMinMax( 1, 62 );

				if ( spells == 1 ) // magic arrow
				{
					from.MovingParticles( mSp, 0x36E4, 5, 0, false, false, 0, 0, 3600, 0, 0, 0 );
					from.PlaySound( 0x1E5 );
				}
				else if ( spells == 2 ) // harm
				{
					mSp.FixedParticles( 0x374A, 10, 30, 5013, 0, 2, EffectLayer.Waist );
					mSp.PlaySound( 0x0FC );
				}
				else if ( spells == 3 ) // lightning
				{
					mSp.FixedParticles( 0x2A4E, 10, 15, 5038, Utility.RandomList( 0, 0x48D, 0x48E, 0x48F, 0x490, 0x491 ), 2, EffectLayer.Head );
					mSp.PlaySound( 0x029 );
				}
				else if ( spells == 4 ) // mind blast
				{
					mSp.FixedParticles( 0x374A, 10, 15, 5038, 1181, 2, EffectLayer.Head );
					mSp.PlaySound( 0x213 );
				}
				else if ( spells == 5 ) // energy bolt
				{
					from.MovingParticles( mSp, 0x3818, 7, 0, false, true, 0, 0, 3043, 4043, 0x211, 0 );
					mSp.PlaySound( 0x20A );
				}
				else if ( spells == 6 ) // web
				{
					from.MovingParticles( mSp, 0x10D3, 7, 0, false, false, 0, 0, 0 );
					mSp.PlaySound( 0x62D );
				}
				else if ( spells == 7 ) // radiation
				{
					mSp.FixedParticles( 0x3400, 10, 30, 5013, 0xB96, 2, EffectLayer.Waist );
					mSp.PlaySound( 0x108 );
				}
				else if ( spells == 8 ) // electricity
				{
					if ( Utility.RandomBool() )
					{
						mSp.FixedParticles( Utility.RandomList( 0x3967, 0x3979 ), 10, 30, 5013, 0, 2, EffectLayer.Waist );
						mSp.PlaySound( 0x5C3 );
					}
					else
					{
						mSp.FixedParticles( 0x5547, 10, 30, 5052, 0, 0, EffectLayer.LeftFoot );
						mSp.PlaySound( 0x665 );
					}
				}
				else if ( spells == 9 ) // electrical storm
				{
					mSp.FixedParticles( Utility.RandomList( 0x3967, 0x3979 ), 10, 30, 5013, 0, 2, EffectLayer.Head );
					mSp.PlaySound( 0x5C3 );
					mSp.BoltEffect( 0 );
				}
				else if ( spells == 10 ) // dark void
				{
					mSp.FixedParticles( 0x3400, 10, 30, 5052, Utility.RandomList( 0x496, 0x844, 0x9C1 ), 0, EffectLayer.Head );
					mSp.PlaySound( 0x108 );
				}
				else if ( spells == 11 ) // acid
				{
					mSp.FixedParticles( 0x1A84, 10, 30, 5052, 0x48E, 0, EffectLayer.Head );
					mSp.PlaySound( 0x026 );
				}
				else if ( spells == 12 ) // magical sparkles
				{
					int sparks = Utility.RandomMinMax(1,4);

					if ( sparks == 1 )
					{
						mSp.FixedParticles( 0x3039, 10, 30, 5052, Utility.RandomList( 0, 0x48D, 0x48E, 0x48F, 0x490, 0x491 ), 0, EffectLayer.Head );
					}
					else if ( sparks == 2 )
					{
						mSp.FixedParticles( 0x5469, 10, 30, 5052, Utility.RandomList( 0, 0x48D, 0x48E, 0x48F, 0x490, 0x491 ), 0, EffectLayer.Head );
					}
					else if ( sparks == 3 )
					{
						mSp.FixedParticles( 0x3F29, 10, 30, 5052, Utility.RandomList( 0, 0x48D, 0x48E, 0x48F, 0x490, 0x491 ), 0, EffectLayer.LeftFoot );
					}
					else
					{
						mSp.FixedParticles( 0x54E1, 10, 30, 5052, Utility.RandomList( 0, 0x48D, 0x48E, 0x48F, 0x490, 0x491 ), 0, EffectLayer.Head );
					}
					mSp.PlaySound( Utility.RandomList( 0x1DF, 0x1E2, 0x1E8, 0x1ED, 0x1F1, 0x1F7, 0x1FD, 0x203, 0x209, 0x20B, 0x5BC, 0x5C4, 0x5C5, 0x5C9 ) );
				}
				else if ( spells == 13 ) // fire tornado
				{
					mSp.FixedParticles( 0x3F29, 10, 30, 5052, 0, 0, EffectLayer.LeftFoot );
					mSp.PlaySound( 0x345 );
				}
				else if ( spells == 14 ) // magic tentacles
				{
					mSp.FixedParticles( 0x5475, 10, 30, 5052, Utility.RandomList( 0, 0x48D, 0x48E, 0x48F, 0x490, 0x491 ), 0, EffectLayer.LeftFoot );
					mSp.PlaySound( Utility.RandomList( 0x1DF, 0x1E2, 0x1E8, 0x1ED, 0x1F1, 0x1F7, 0x1FD, 0x203, 0x209, 0x20B, 0x5BC, 0x5C4, 0x5C5, 0x5C9 ) );
				}
				else if ( spells == 15 ) // vortex
				{
					mSp.FixedParticles( 0x5508, 10, 30, 5052, Utility.RandomList( 0, 0x48D, 0x48E, 0x48F, 0x490, 0x491 ), 0, EffectLayer.Head );
					mSp.PlaySound( 0x665 );
				}
				else if ( spells == 16 ) // shoot lightning
				{
					from.MovingParticles( mSp, 0x3818, 5, 0, false, false, 0, 0, 3600, 0, 0, 0 );
					mSp.PlaySound( 0x211 );
				}
				else if ( spells == 17 ) // fire bolt
				{
					from.MovingParticles( mSp, 0x4D17, 5, 0, false, false, 0, 0, 3600, 0, 0, 0 );
					mSp.PlaySound( 0x15E );
				}
				else if ( spells == 18 ) // fireball
				{
					if ( Utility.RandomBool() )
					{
						mSp.FixedParticles( 0x5562, 10, 30, 5052, 0, 0, EffectLayer.Head );
						mSp.PlaySound( 0x44B );
					}
					else
					{
						from.MovingParticles( mSp, 0x36D4, 7, 0, false, true, 0, 0, 9502, 4019, 0x160, 0 );
						mSp.PlaySound( Core.AOS ? 0x15E : 0x44B );
					}
				}
				else if ( spells == 19 ) // devastate
				{
					mSp.FixedParticles( 0x2A4E, 10, 30, 5052, 0, 0, EffectLayer.Head );
					mSp.PlaySound( 0x029 );
				}
				else if ( spells == 20 ) // meteors
				{
					Effects.SendLocationEffect( mSp.Location, mSp.Map, Utility.RandomList( 0x33E5, 0x33F5 ), 85, 10, 0xB38, 0 );
					if ( circle > 3 )
					{
						Point3D blast2 = new Point3D( ( mSp.X-1 ), ( mSp.Y ), mSp.Z );
						Effects.SendLocationEffect( blast2, mSp.Map, Utility.RandomList( 0x33E5, 0x33F5 ), 85, 10, 0xB38, 0 );
					}
					if ( circle > 5 )
					{
						Point3D blast3 = new Point3D( ( mSp.X+1 ), ( mSp.Y ), mSp.Z );
						Effects.SendLocationEffect( blast3, mSp.Map, Utility.RandomList( 0x33E5, 0x33F5 ), 85, 10, 0xB38, 0 );
					}
					if ( circle > 6 )
					{
						Point3D blast4 = new Point3D( ( mSp.X ), ( mSp.Y-1 ), mSp.Z );
						Effects.SendLocationEffect( blast4, mSp.Map, Utility.RandomList( 0x33E5, 0x33F5 ), 85, 10, 0xB38, 0 );
					}
					if ( circle > 7 )
					{
						Point3D blast5 = new Point3D( ( mSp.X ), ( mSp.Y+1 ), mSp.Z );
						Effects.SendLocationEffect( blast5, mSp.Map, Utility.RandomList( 0x33E5, 0x33F5 ), 85, 10, 0xB38, 0 );
					}
					mSp.PlaySound( 0x65A );
				}
				else if ( spells == 21 ) // destruction
				{
					mSp.FixedParticles( 0x36B0, 10, 30, 5052, 0xAB3, 0, EffectLayer.Head );
					mSp.PlaySound( 0x664 );
				}
				else if ( spells == 22 ) // flame bolt
				{
					from.MovingParticles( mSp, 0x3818, 5, 0, false, false, 0xAD2, 0, 3600, 0, 0, 0 );
					mSp.PlaySound( 0x658 );
				}
				else if ( spells == 23 ) // flame strike
				{
					if ( Utility.RandomBool() )
					{
						mSp.FixedParticles( 0x551A, 10, 30, 5052, 0, 0, EffectLayer.LeftFoot );
						mSp.PlaySound( 0x345 );
					}
					else
					{
						mSp.FixedParticles( 0x3709, 10, 30, 5052, 0, 0, EffectLayer.LeftFoot );
						mSp.PlaySound( 0x208 );
					}
				}
				else if ( spells == 24 ) // ignite
				{
					Point3D blast1 = new Point3D( ( mSp.X ), ( mSp.Y ), mSp.Z );
					Effects.SendLocationEffect( blast1, mSp.Map, 0x3728, 85, 10, 0xB70, 0 );
					if ( circle > 3 )
					{
						Point3D blast2 = new Point3D( ( mSp.X-1 ), ( mSp.Y ), mSp.Z );
						Effects.SendLocationEffect( blast2, mSp.Map, 0x3728, 85, 10, 0xB70, 0 );
					}
					if ( circle > 5 )
					{
						Point3D blast3 = new Point3D( ( mSp.X+1 ), ( mSp.Y ), mSp.Z );
						Effects.SendLocationEffect( blast3, mSp.Map, 0x3728, 85, 10, 0xB70, 0 );
					}
					if ( circle > 6 )
					{
						Point3D blast4 = new Point3D( ( mSp.X ), ( mSp.Y-1 ), mSp.Z );
						Effects.SendLocationEffect( blast4, mSp.Map, 0x3728, 85, 10, 0xB70, 0 );
					}
					if ( circle > 7 )
					{
						Point3D blast5 = new Point3D( ( mSp.X ), ( mSp.Y+1 ), mSp.Z );
						Effects.SendLocationEffect( blast5, mSp.Map, 0x3728, 85, 10, 0xB70, 0 );
					}
					mSp.PlaySound( 0x208 );
				}
				else if ( spells == 25 ) // explosion
				{
					Point3D blast1 = new Point3D( ( mSp.X ), ( mSp.Y ), mSp.Z );
					Effects.SendLocationEffect( blast1, mSp.Map, Utility.RandomList( 0x36BD, 0x3822 ), 85, 10, 0, 0 );
					if ( circle > 3 )
					{
						Point3D blast2 = new Point3D( ( mSp.X-1 ), ( mSp.Y ), mSp.Z );
						Effects.SendLocationEffect( blast2, mSp.Map, Utility.RandomList( 0x36BD, 0x3822 ), 85, 10, 0, 0 );
					}
					if ( circle > 5 )
					{
						Point3D blast3 = new Point3D( ( mSp.X+1 ), ( mSp.Y ), mSp.Z );
						Effects.SendLocationEffect( blast3, mSp.Map, Utility.RandomList( 0x36BD, 0x3822 ), 85, 10, 0, 0 );
					}
					if ( circle > 6 )
					{
						Point3D blast4 = new Point3D( ( mSp.X ), ( mSp.Y-1 ), mSp.Z );
						Effects.SendLocationEffect( blast4, mSp.Map, Utility.RandomList( 0x36BD, 0x3822 ), 85, 10, 0, 0 );
					}
					if ( circle > 7 )
					{
						Point3D blast5 = new Point3D( ( mSp.X ), ( mSp.Y+1 ), mSp.Z );
						Effects.SendLocationEffect( blast5, mSp.Map, Utility.RandomList( 0x36BD, 0x3822 ), 85, 10, 0, 0 );
					}
					mSp.PlaySound( 0x307 );
				}
				else if ( spells == 26 ) // steam
				{
					mSp.FixedParticles( 0x3400, 10, 30, 5052, 0x9C4, 0, EffectLayer.Head );
					mSp.PlaySound( 0x108 );
				}
				else if ( spells == 27 ) // ice bolt
				{
					from.MovingParticles( mSp, 0x4D18, 5, 0, false, false, 0, 0, 3600, 0, 0, 0 );
					mSp.PlaySound( 0x650 );
				}
				else if ( spells == 28 ) // icicle
				{
					from.MovingParticles( mSp, 0x28EF, 5, 0, false, false, 0xB77, 0, 3600, 0, 0, 0 );
					mSp.PlaySound( 0x1E5 );
				}
				else if ( spells == 29 ) // hail storm
				{
					mSp.FixedParticles( Utility.RandomList(0x384E,0x3859), 20, 10, 5044, 0, 0, EffectLayer.Head );
					mSp.PlaySound( 0x64F );
				}
				else if ( spells == 30 ) // frost strike
				{
					mSp.FixedParticles( 0x23B32, 10, 30, 5052, 0x809, 0, EffectLayer.LeftFoot );
					mSp.PlaySound( 0x64F );
				}
				else if ( spells == 31 ) // avalanche
				{
					Point3D blast1 = new Point3D( ( mSp.X ), ( mSp.Y ), mSp.Z );
					Effects.SendLocationEffect( blast1, mSp.Map, Utility.RandomList( 0x33E5, 0x33F5 ), 85, 10, 0xB77, 0 );
					if ( circle > 3 )
					{
						Point3D blast2 = new Point3D( ( mSp.X-1 ), ( mSp.Y ), mSp.Z );
						Effects.SendLocationEffect( blast2, mSp.Map, Utility.RandomList( 0x33E5, 0x33F5 ), 85, 10, 0xB77, 0 );
					}
					if ( circle > 5 )
					{
						Point3D blast3 = new Point3D( ( mSp.X+1 ), ( mSp.Y ), mSp.Z );
						Effects.SendLocationEffect( blast3, mSp.Map, Utility.RandomList( 0x33E5, 0x33F5 ), 85, 10, 0xB77, 0 );
					}
					if ( circle > 6 )
					{
						Point3D blast4 = new Point3D( ( mSp.X ), ( mSp.Y-1 ), mSp.Z );
						Effects.SendLocationEffect( blast4, mSp.Map, Utility.RandomList( 0x33E5, 0x33F5 ), 85, 10, 0xB77, 0 );
					}
					if ( circle > 7 )
					{
						Point3D blast5 = new Point3D( ( mSp.X ), ( mSp.Y+1 ), mSp.Z );
						Effects.SendLocationEffect( blast5, mSp.Map, Utility.RandomList( 0x33E5, 0x33F5 ), 85, 10, 0xB77, 0 );
					}
					mSp.PlaySound( 0x65A );
				}
				else if ( spells == 32 ) // snow ball
				{
					from.MovingParticles( mSp, 0x36E4, 7, 0, false, true, 0xBB3, 0, 9502, 4019, 0x160, 0 );
					mSp.PlaySound( 0x650 );
				}
				else if ( spells == 33 ) // cold
				{
					mSp.FixedParticles( 0x5590, 10, 30, 5052, 0xB77, 0, EffectLayer.Head );
					mSp.PlaySound( Utility.RandomList(0x10B,0x5590) );
				}
				else if ( spells == 34 ) // poison bolt
				{
					from.MovingParticles( mSp, 0x4F49, 5, 0, false, false, 0, 0, 3600, 0, 0, 0 );
					mSp.PlaySound( 0x658 );
				}
				else if ( spells == 35 ) // physic blast
				{
					mSp.FixedParticles( 0x3822, 20, 10, 5044, 0xAF1, 0, EffectLayer.Head );
					mSp.PlaySound( 0x658 );
				}
				else if ( spells == 36 ) // evil lightning
				{
					mSp.FixedParticles( 0x55A6, 20, 10, 5044, EffectLayer.Head );
					mSp.PlaySound( 0x653 );
				}
				else if ( spells == 37 ) // strike
				{
					mSp.FixedParticles( 0x36BD, 20, 10, 5044, EffectLayer.Head );
					mSp.PlaySound( 0x307 );
				}
				else if ( spells == 38 ) // mind rot
				{
					mSp.PlaySound( 0x1FB );
					mSp.PlaySound( 0x258 );
					mSp.FixedParticles( 0x373A, 1, 17, 9903, 15, 4, EffectLayer.Head );
				}
				else if ( spells == 39 ) // pain spike
				{
					mSp.FixedParticles( 0x37C4, 1, 8, 9916, 39, 3, EffectLayer.Head );
					mSp.FixedParticles( 0x37C4, 1, 8, 9502, 39, 4, EffectLayer.Head );
					mSp.PlaySound( 0x210 );
				}
				else if ( spells == 40 ) // strangle
				{
					mSp.PlaySound( 0x22F );
					mSp.FixedParticles( 0x36CB, 1, 9, 9911, 67, 5, EffectLayer.Head );
					mSp.FixedParticles( 0x374A, 1, 17, 9502, 1108, 4, (EffectLayer)255 );
				}
				else if ( spells == 41 ) // wither
				{
					Effects.PlaySound( mSp.Location, mSp.Map, 0x1FB );
					mSp.PlaySound( 0x10B );
					mSp.FixedParticles( 0x37CC, 1, 9, 9911, 0xB1F, 5, EffectLayer.Waist );
				}
				else if ( spells == 42 ) // poison strike
				{
					mSp.FixedParticles( 0x36B0, 1, 9, 9911, 9915, 5, EffectLayer.Waist );
					mSp.PlaySound( 0x229 );
				}
				else if ( spells == 43 ) // poison
				{
					mSp.FixedParticles( 0x374A, 10, 15, 5021, 0, 0, EffectLayer.Waist );
					mSp.PlaySound( 0x205 );
				}
				else if ( spells == 44 ) // poison
				{
					mSp.FixedParticles( 0x3400, 10, 30, 5052, 0, 0, EffectLayer.Waist );
					mSp.PlaySound( 0x108 );
				}
				else if ( spells == 45 ) // poison
				{
					mSp.FixedParticles( 0x36B0, 10, 30, 5052, 9915, 0, EffectLayer.Waist );
					mSp.PlaySound( 0x229 );
				}
				else if ( spells == 46 ) // venom vine
				{
					mSp.FixedParticles( 0x5475, 10, 30, 5052, 0, 0, EffectLayer.LeftFoot );
					mSp.PlaySound( 0x64F );
				}
				else if ( spells == 47 ) // vines
				{
					mSp.FixedParticles( 0x5487, 10, 30, 5052, 0, 0, EffectLayer.LeftFoot );
					mSp.PlaySound( 0x64F );
				}
				else if ( spells == 48 ) // leaves
				{
					mSp.FixedParticles( 0x54F4, 10, 30, 5052, 0, 0, EffectLayer.LeftFoot );
					mSp.PlaySound( 0x10B );
				}
				else if ( spells == 49 ) // magical
				{
					mSp.FixedParticles( 0x3039, 10, 30, 5052, 0, 0, EffectLayer.LeftFoot );
					mSp.PlaySound( Utility.RandomList( 0x1DF, 0x1E2, 0x1E8, 0x1ED, 0x1F1, 0x1F7, 0x1FD, 0x203, 0x209, 0x20B, 0x5BC, 0x5C4, 0x5C5, 0x5C9 ) );
				}
				else if ( spells == 50 ) // air
				{
					if ( Utility.RandomBool() )
					{
						mSp.FixedParticles( 0x5492, 10, 30, 5052, 0, 0, EffectLayer.LeftFoot );
					}
					else
					{
						mSp.FixedParticles( 0x5590, 10, 30, 5052, 0, 0, EffectLayer.LeftFoot );
					}
					mSp.PlaySound( Utility.RandomList(0x10B,0x5590) );
				}
				else if ( spells == 51 ) // stone hands
				{
					mSp.FixedParticles( 0x3837, 10, 30, 5052, 0, 0, EffectLayer.LeftFoot );
					mSp.PlaySound( 0x65A );
				}
				else if ( spells == 52 ) // water
				{
					if ( Utility.RandomBool() )
					{
						mSp.FixedParticles( 0x1A84, 10, 30, 5052, 0xB3D, 0, EffectLayer.Waist );
						mSp.PlaySound( 0x026 );
					}
					else
					{
						mSp.FixedParticles( 0x5558, 10, 30, 5052, 0, 0, EffectLayer.LeftFoot );
						mSp.PlaySound( 0x026 );
					}
				}
				else if ( spells == 53 ) // weed
				{
					mSp.FixedParticles( 0x3400, 10, 30, 5052, 0xB97, 0, EffectLayer.LeftFoot );
					mSp.PlaySound( 0x64F );
				}
				else if ( spells == 54 ) // water globe
				{
					if ( Utility.RandomBool() )
					{
						mSp.FixedParticles( 0x37E5, 10, 30, 5052, 0, 0, EffectLayer.Head );
						mSp.PlaySound( 0x5BF );
					}
					else
					{
						mSp.FixedParticles( 0x559A, 10, 30, 5052, 0, 0, EffectLayer.Head );
						mSp.PlaySound( 0x56D );
					}
				}
				else if ( spells == 55 ) // poison field
				{
					mSp.FixedParticles( Utility.RandomList(0x3915,0x3924), 10, 30, 5052, 0, 0, EffectLayer.LeftFoot );
					mSp.PlaySound( 0x5BC );
				}
				else if ( spells == 56 ) // fire field
				{
					if ( Utility.RandomBool() )
					{
						mSp.FixedParticles( Utility.RandomList(0x3998,0x398D), 10, 30, 5052, 0, 0, EffectLayer.LeftFoot );
						mSp.PlaySound( 0x356 );
					}
					else
					{
						mSp.FixedParticles( 0x55B1, 10, 30, 5052, 0, 0, EffectLayer.LeftFoot );
						mSp.PlaySound( 0x5CF );
					}
				}
				else if ( spells == 57 ) // bird wings
				{
					mSp.FixedParticles( 0x3FE5, 10, 30, 5052, 0xB60, 0, EffectLayer.Head );
					mSp.PlaySound( 0x64D );
				}
				else if ( spells == 58 ) // throwing skull
				{
					from.MovingParticles( mSp, 0x3FF9, 7, 0, false, true, 0, 0, 9502, 4019, 0x160, 0 );
					from.PlaySound( 0x658 );
				}
				else if ( spells == 59 ) // insects
				{
					mSp.FixedParticles( 0x554F, 10, 30, 5052, 0, 0, EffectLayer.LeftFoot );
					mSp.PlaySound( Utility.RandomList(0x5CC,0x5CB) );
				}
				else if ( spells == 60 ) // water splash
				{
					if ( Utility.RandomBool() )
					{
						mSp.FixedParticles( 0x5536, 10, 30, 5052, 0, 0, EffectLayer.Head );
						mSp.PlaySound( 0x5CA );
					}
					else
					{
						mSp.FixedParticles( 0x23B2, 10, 30, 5052, 0, 0, EffectLayer.LeftFoot );
						mSp.PlaySound( 0x026 );
					}
				}
				else if ( spells == 61 ) // ice storm
				{
					if ( Utility.RandomBool() )
					{
						mSp.FixedParticles( Utility.RandomList(0x384E,0x3859), 10, 30, 5052, 0xB79, 0, EffectLayer.LeftFoot );
					}
					else
					{
						mSp.FixedParticles( 0x55BB, 10, 30, 5052, 0, 0, EffectLayer.LeftFoot );
					}
					mSp.PlaySound( 0x5CE );
				}
				else if ( spells == 62 ) // ice spike
				{
					mSp.FixedParticles( 0x5571, 10, 30, 5052, 0, 0, EffectLayer.LeftFoot );
					mSp.PlaySound( 0x65D );
				}
			}
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

namespace Server.Mobiles
{
	[CorpseName( "a mouse corpse" )]
	public class SpellCritter : BaseCreature
	{
		[Constructable]
		public SpellCritter() : base( AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			Name = "a mouse";
			Body = 0;
			BaseSoundID = 0;
			Hidden = true;
			CantWalk = true;
			SetSkill( SkillName.Hiding, 500.0 );
			SetSkill( SkillName.Stealth, 500.0 );
		}

		public override bool DeleteCorpseOnDeath{ get{ return true; } }

		public SpellCritter(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}
}