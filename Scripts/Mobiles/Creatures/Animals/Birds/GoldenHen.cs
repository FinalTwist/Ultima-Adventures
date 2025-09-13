using System;
using Server.Mobiles;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a Golden Hen corpse" )]
	public class GoldenHen : BaseCreature
	{
		private DateTime m_pluckedtime;
		[CommandProperty( AccessLevel.GameMaster )]
        public DateTime PluckedTime
        {
            get{ return m_pluckedtime; }
            set{ m_pluckedtime = value; }
        }

		private bool m_plucked;
		[CommandProperty( AccessLevel.GameMaster )]
        public bool Plucked
        {
            get{ return m_plucked; }
            set{ m_plucked = value; }
        }

		[Constructable]
		public GoldenHen() : base( AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{

			Name = "a Golden Hen";
			Body = 0xD0;
			BaseSoundID = 0x6E;

			m_pluckedtime = DateTime.UtcNow;
			m_plucked = false;

			SetStr( 5 );
			SetDex( 15 );
			SetInt( 5 );

			SetHits( 3 );
			SetMana( 0 );

			SetDamage( 1 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 1, 5 );

			SetSkill( SkillName.MagicResist, 4.0 );
			SetSkill( SkillName.Tactics, 5.0 );
			SetSkill( SkillName.Wrestling, 5.0 );

			Fame = 150;
			Karma = 0;

			VirtualArmor = 2;

			Tamable = true;
			ControlSlots = 0;
			MinTameSkill = -0.9;

			if ( Utility.RandomMinMax( 1, 5 ) == 1 )
			{
				Item egg = new Eggs( Utility.RandomMinMax( 1, 3 ) );
				PackItem( egg );
			}
		}


		public override int Meat{ get{ return 1; } }
		public override MeatType MeatType{ get{ return MeatType.Bird; } }
		public override FoodType FavoriteFood{ get{ return FoodType.GrainsAndHay; } }

		public override bool HandlesOnSpeech( Mobile from ) 
		{ 
			return true; 
		} 

		public override void OnThink()
		{
			base.OnThink();
			
			if (this.IsBonded)
				this.IsBonded = false;
			if (this.ControlSlots >0)
				this.ControlSlots = 0;
			if (this.MinTameSkill > 0)
				this.MinTameSkill = 0;
			
		}

		public override void OnSpeech( SpeechEventArgs e ) 
		{
			if ( !(e.Mobile is PlayerMobile) )
				return;
			  
			if( e.Mobile.InRange( this, 2 ))
			{
				if (  Insensitive.Contains( e.Speech, "catch" ) || Insensitive.Contains( e.Speech, "get" ) || Insensitive.Contains( e.Speech, "steal" ) )
				{
					PlayerMobile player = (PlayerMobile)e.Mobile;
					if (player.Followers < player.FollowersMax)
					{
						Say("*cluck!*"); 

						this.SetControlMaster( e.Mobile );	
					}
				}
			}
			base.OnSpeech( e );
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( DateTime.UtcNow > ( m_pluckedtime + TimeSpan.FromHours( 23.0 )) && ((!m_plucked && Utility.RandomDouble() <= 0.05) || (m_plucked && Utility.RandomDouble() < 0.02)) )
			{
				Point3D gagme = this.Location;

				GoldenHen newhen = new GoldenHen();
				
				newhen.MoveToWorld( gagme, this.Map );
				((BaseCreature)newhen).Home = gagme;
				((BaseCreature)newhen).RangeHome = 10;
				newhen.OnAfterSpawn();
				newhen.Say("*cluck*");

				if (!m_plucked)
					m_plucked = true;
			}
			if ( DateTime.UtcNow > ( m_pluckedtime + TimeSpan.FromHours( 23.0 )) && this.Controlled && this.ControlMaster != null && from == this.ControlMaster ) 			
			{
				int amount = Utility.RandomMinMax(1, 500);
				Gold gg = new Gold(amount);
				gg.MoveToWorld(this.Location, this.Map);
				m_pluckedtime = DateTime.UtcNow;
				this.Say("*cluck*");
			}

			base.OnDoubleClick( from );
		}

		public override int Feathers{ get{ return 25; } }

		public GoldenHen(Serial serial) : base(serial)
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