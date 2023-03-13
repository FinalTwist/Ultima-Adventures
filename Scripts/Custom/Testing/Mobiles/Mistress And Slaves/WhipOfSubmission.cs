//Created By RANCID77 aka EARL...
//I Do Not Mind If You Modify This But Give Credit Where Credit Is Due...
using System;
using System.Collections;
using Server.Network;
using Server.Items;
using Server.Targeting;

namespace Server.Items
{
	[FlipableAttribute( 0x26C3 )]
	public class WhipOfSubmission : BaseRanged
	{
        public override int EffectID{ get{ return 0xF42; } }
		public override Type AmmoType{ get{ return typeof( Arrow ); } }
		public override Item Ammo{ get{ return new Arrow(); } }
		public override int ArtifactRarity{ get{ return 58; } }

		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.CrushingBlow; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.ArmorIgnore; } }

		public override int AosStrengthReq{ get{ return 30; } }
		public override int AosMinDamage{ get{ return 15; } }
		public override int AosMaxDamage{ get{ return 19; } }
		public override int AosSpeed{ get{ return 23; } }
		public override float MlSpeed{ get{ return 2.00f; } }
        public override int DefMaxRange { get { return 11; } }

		public override int DefHitSound{ get{ return 0x145; } }
		public override int DefMissSound{ get{ return 0x23A; } }

		public override int InitMinHits{ get{ return Utility.RandomMinMax(100, 125); } }
		public override int InitMaxHits{ get{ return Utility.RandomMinMax(126, 150); } }

		[Constructable]
		public WhipOfSubmission() : base( 0x26C3 )
		{
           
			Weight = 10.0;
			Name = "Bow Of Submission";
			Hue = 2997;

			Layer = Layer.TwoHanded;

			Attributes.WeaponDamage = 55;
			Attributes.DefendChance = 15;
			Attributes.CastSpeed = 1;
			Attributes.AttackChance = 25;
			Attributes.Luck = 150;
			Attributes.SpellChanneling = 1;
			Attributes.BonusStr = 10;
			Attributes.WeaponSpeed = 40;
			//Attributes.NightSight = 1;
		}

		public WhipOfSubmission( Serial serial ) : base( serial )
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

		public override void OnDoubleClick( Mobile from )
		{
			from.Say( "Now You Shall Feel My Whip... You Foolish Slave..." );
			from.Target = new WhipOfSubmissionTarget( from, this );
		}

		private class InternalTimer : Timer
		{
			private Mobile m_From;

			public InternalTimer( Mobile from ) : base( TimeSpan.FromSeconds( 4.0 ) )
			{
				m_From = from;
			}

			protected override void OnTick()
			{
				m_From.EndAction( typeof( WhipOfSubmission ) );
			}
		}

		private class WhipOfSubmissionTarget : Target
		{
			private Mobile m_Thrower;
			private Item m_WhipOfSubmission;

			public WhipOfSubmissionTarget( Mobile thrower, Item whipofsubmission ) : base ( 10, false, TargetFlags.None )
			{
				m_Thrower = thrower;
				m_WhipOfSubmission = whipofsubmission;
			}

			protected override void OnTarget( Mobile from, object target )
			{
				if ( target == from )
				{
					from.SendMessage( "You Cant Whip Yourself You Fool..." );
				}
                else if (target is Mobile)
                {
                    Mobile targ = (Mobile)target;
                    Container pack = targ.Backpack;

                    if (pack != null && pack.FindItemByType(new Type[] { typeof(LifeStone) }) != null)
                    {
						if ( from.BeginAction( typeof( WhipOfSubmission ) ) )
						{
							new InternalTimer( from ).Start();

							from.PlaySound( 0x145 );

							from.Animate( 9, 1, 1, true, false, 0 );

							targ.SendMessage( "You Have Just Been Whipped For Punishment..." );
							targ.Say( "Plz Master Do Not Whip Me Again... I Will Behave..." );
							from.SendMessage( "You Whip The Slave Drawing A Thin Line Of Blood..." );
						}
						else
						{
							from.SendMessage( "You Must Recoil The Whip Before You Can Whip Them Again..." );
						}
					}
				}
			}
		}
	}
}