using System;
using Server;
using Server.Items;
using Server.Misc;

namespace Server.Mobiles
{
	public class RuneGuardian : BaseCreature
	{
		public int gBrthPhys;
		public int gBrthCold;
		public int gBrthFire;
		public int gBrthPois;
		public int gBrthEngy;
		public int gBrthEffectHue;
		public int gBrthEffectSound;
		public int gBrthEffectItemID;
		public int gBrthType;
		public int gVirtue;
		public Mobile gSummoner;

		public override bool IsScaredOfScaryThings{ get{ return false; } }
		public override bool IsScaryToPets{ get{ return true; } }
		public override bool BleedImmune{ get{ return true; } }
		public override bool AutoDispel{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }
		public virtual bool Unprovokable{ get{ return true; } }
		public virtual bool Uncalmable{ get{ return true; } }
		public virtual bool AreaPeaceImmune { get{ return true; } }
		public override bool DeleteCorpseOnDeath{ get{ return true; } }

		public override int BreathPhysicalDamage{ get{ return gBrthPhys; } }
		public override int BreathFireDamage{ get{ return gBrthFire; } }
		public override int BreathColdDamage{ get{ return gBrthCold; } }
		public override int BreathPoisonDamage{ get{ return gBrthPois; } }
		public override int BreathEnergyDamage{ get{ return gBrthEngy; } }
		public override int BreathEffectHue{ get{ return gBrthEffectHue; } }
		public override int BreathEffectSound{ get{ return gBrthEffectSound; } }
		public override int BreathEffectItemID{ get{ return gBrthEffectItemID; } }
		public override bool ReacquireOnMovement{ get{ return !Controlled; } }
		public override bool HasBreath{ get{ return true; } }
		public override double BreathEffectDelay{ get{ return 0.1; } }
		public override void BreathDealDamage( Mobile target, int form ){ base.BreathDealDamage( target, gBrthType ); }

		[Constructable]
		public RuneGuardian () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a sentinel";
			Body = Utility.RandomList( 244, 428, 88, 306, 313, 314, 139, 144, 287, 715, 319, 772, 784, 753, 754  );
			Timer.DelayCall( TimeSpan.FromMinutes( (double)(Utility.RandomMinMax( 60, 90 )) ), new TimerCallback( Delete ) );
			NameHue = 0x22;

			if ( Utility.RandomBool() ){ AI = AIType.AI_Melee; }

			if ( Body == 244 ) // BEETLE
			{
				BaseSoundID = 0x388;
				gBrthPhys = 0;
				gBrthCold = 0;
				gBrthFire = 0;
				gBrthPois = 100;
				gBrthEngy = 0;
				gBrthEffectHue = 0x48F;
				gBrthEffectSound = 0x012;
				gBrthEffectItemID = 0x1A85;
				gBrthType = 36;
			}
			else if ( Body == 428 ) // OGRE
			{
				BaseSoundID = 427;
				gBrthPhys = 100;
				gBrthCold = 0;
				gBrthFire = 0;
				gBrthPois = 0;
				gBrthEngy = 0;
				gBrthEffectHue = 0;
				gBrthEffectSound = 0x65A;
				gBrthEffectItemID = 0x1365;
				gBrthType = 7;
			}
			else if ( Body == 88 ) // DEMON
			{
				BaseSoundID = 357;
				gBrthPhys = 20;
				gBrthCold = 20;
				gBrthFire = 20;
				gBrthPois = 20;
				gBrthEngy = 20;
				gBrthEffectHue = 0x496;
				gBrthEffectSound = 0x658;
				gBrthEffectItemID = 0x37BC;
				gBrthType = 23;
			}
			else if ( Body == 306 ) // SERPENTOID
			{
				BaseSoundID = 639;
				gBrthPhys = 0;
				gBrthCold = 0;
				gBrthFire = 0;
				gBrthPois = 100;
				gBrthEngy = 0;
				gBrthEffectHue = 0x3F;
				gBrthEffectSound = 0x658;
				gBrthEffectItemID = 0x36D4;
				gBrthType = 10;
			}
			else if ( Body == 784 ) // ANTOID
			{
				BaseSoundID = 0x24D;
				gBrthPhys = 20;
				gBrthCold = 20;
				gBrthFire = 20;
				gBrthPois = 20;
				gBrthEngy = 20;
				gBrthEffectHue = 0x844;
				gBrthEffectSound = 0x658;
				gBrthEffectItemID = 0x37BC;
				gBrthType = 24;
			}
			else if ( Body == 313 ) // TREE
			{
				BaseSoundID = 442;
				gBrthPhys = 20;
				gBrthCold = 20;
				gBrthFire = 20;
				gBrthPois = 20;
				gBrthEngy = 20;
				gBrthEffectHue = 0x9C1;
				gBrthEffectSound = 0x653;
				gBrthEffectItemID = 0x37BC;
				gBrthType = 25;
			}
			else if ( Body == 314 ) // SPHINX
			{
				BaseSoundID = 0x668;
				gBrthPhys = 50;
				gBrthCold = 50;
				gBrthFire = 0;
				gBrthPois = 0;
				gBrthEngy = 0;
				gBrthEffectHue = 0x96D;
				gBrthEffectSound = 0x654;
				gBrthEffectItemID = 0x36D4;
				gBrthType = 15;
			}
			else if ( Body == 715 ) // WYRM
			{
				BaseSoundID = 362;
				gBrthPhys = 0;
				gBrthCold = 0;
				gBrthFire = 100;
				gBrthPois = 0;
				gBrthEngy = 0;
				gBrthEffectHue = 0;
				gBrthEffectSound = 0x227;
				gBrthEffectItemID = 0x36D4;
				gBrthType = 9;
			}
			else if ( Body == 139 ) // DRAGON
			{
				BaseSoundID = 362;
				gBrthPhys = 0;
				gBrthCold = 100;
				gBrthFire = 0;
				gBrthPois = 0;
				gBrthEngy = 0;
				gBrthEffectHue = 0x481;
				gBrthEffectSound = 0x64F;
				gBrthEffectItemID = 0x36D4;
				gBrthType = 12;
			}
			else if ( Body == 144 ) // NAGA
			{
				BaseSoundID = 644;
				gBrthPhys = 0;
				gBrthCold = 0;
				gBrthFire = 0;
				gBrthPois = 0;
				gBrthEngy = 100;
				gBrthEffectHue = 0x9C2;
				gBrthEffectSound = 0x665;
				gBrthEffectItemID = 0x3818;
				gBrthType = 13;
			}
			else if ( Body == 754 ) // GOLEM
			{
				BaseSoundID = 268;
				gBrthPhys = 0;
				gBrthCold = 0;
				gBrthFire = 0;
				gBrthPois = 0;
				gBrthEngy = 100;
				gBrthEffectHue = 0x9C2;
				gBrthEffectSound = 0x665;
				gBrthEffectItemID = 0x3818;
				gBrthType = 14;
			}
			else if ( Body == 753 ) // ELEMENTAL
			{
				BaseSoundID = 268;
				gBrthPhys = 50;
				gBrthCold = 0;
				gBrthFire = 0;
				gBrthPois = 0;
				gBrthEngy = 50;
				gBrthEffectHue = 0x9B7;
				gBrthEffectSound = 0x658;
				gBrthEffectItemID = 0;
				gBrthType = 33;
			}
			else if ( Body == 287 ) // CERBERUS
			{
				BaseSoundID = 362;
				gBrthPhys = 0;
				gBrthCold = 0;
				gBrthFire = 100;
				gBrthPois = 0;
				gBrthEngy = 0;
				gBrthEffectHue = 0;
				gBrthEffectSound = 0x227;
				gBrthEffectItemID = 0x36D4;
				gBrthType = 9;
			}
			else if ( Body == 319 ) // SPIDER
			{
				BaseSoundID = 0x388;
				gBrthPhys = 0;
				gBrthCold = 0;
				gBrthFire = 0;
				gBrthPois = 100;
				gBrthEngy = 0;
				gBrthEffectHue = 0x3F;
				gBrthEffectSound = 0x658;
				gBrthEffectItemID = 0x36D4;
				gBrthType = 10;
			}
			else if ( Body == 772 ) // GIANT
			{
				BaseSoundID = 609;
				gBrthPhys = 50;
				gBrthCold = 0;
				gBrthFire = 0;
				gBrthPois = 0;
				gBrthEngy = 50;
				gBrthEffectHue = 0;
				gBrthEffectSound = 0x665;
				gBrthEffectItemID = 0x3818;
				gBrthType = 46;
			}

			SetStr( 986, 1185 );
			SetDex( 177, 255 );
			SetInt( 1066, 1145 );

			SetHits( 592, 711 );

			SetDamage( 22, 29 );

			SetDamageType( ResistanceType.Physical, 20 );
			SetDamageType( ResistanceType.Fire, 20 );
			SetDamageType( ResistanceType.Energy, 20 );
			SetDamageType( ResistanceType.Poison, 20 );
			SetDamageType( ResistanceType.Energy, 20 );

			SetResistance( ResistanceType.Physical, 40, 60 );
			SetResistance( ResistanceType.Fire, 40, 60 );
			SetResistance( ResistanceType.Cold, 40, 60 );
			SetResistance( ResistanceType.Poison, 40, 60 );
			SetResistance( ResistanceType.Energy, 40, 60 );

			SetSkill( SkillName.Anatomy, 90.1, 120.0 );
			SetSkill( SkillName.EvalInt, 90.1, 120.0 );
			SetSkill( SkillName.Magery, 90.1, 120.0 );
			SetSkill( SkillName.Meditation, 90.1, 120.0 );
			SetSkill( SkillName.MagicResist, 90.1, 120.0 );
			SetSkill( SkillName.Tactics, 90.1, 120.0 );
			SetSkill( SkillName.Wrestling, 90.1, 120.0 );

			VirtualArmor = 90;

			MyServerSettings.AdditionalHitPoints( this, 4 );
		}

		public override bool OnBeforeDeath()
		{
			RuneOnCorpse myRune = new RuneOnCorpse();
			if ( gVirtue == 1 ){ myRune.Name = "Rune of Compassion"; myRune.ItemID = Utility.RandomList( 0x5318, 0x5319 ); }
			else if ( gVirtue == 2 ){ myRune.Name = "Rune of Honesty"; myRune.ItemID = Utility.RandomList( 0x530E, 0x530F ); }
			else if ( gVirtue == 3 ){ myRune.Name = "Rune of Honor"; myRune.ItemID = Utility.RandomList( 0x531A, 0x531B ); }
			else if ( gVirtue == 4 ){ myRune.Name = "Rune of Humility"; myRune.ItemID = Utility.RandomList( 0x5312, 0x5313 ); }
			else if ( gVirtue == 5 ){ myRune.Name = "Rune of Justice"; myRune.ItemID = Utility.RandomList( 0x5310, 0x5311 ); }
			else if ( gVirtue == 6 ){ myRune.Name = "Rune of Sacrifice"; myRune.ItemID = Utility.RandomList( 0x5314, 0x5315 ); }
			else if ( gVirtue == 7 ){ myRune.Name = "Rune of Spirituality"; myRune.ItemID = Utility.RandomList( 0x530C, 0x530D ); }
			else if ( gVirtue == 8 ){ myRune.Name = "Rune of Valor"; myRune.ItemID = Utility.RandomList( 0x5316, 0x5317 ); }
			myRune.rSummoner = gSummoner;
			myRune.MoveToWorld( Location, Map );
			Server.Misc.IntelligentAction.BurnAway( this );
			return base.OnBeforeDeath();
		}

		public override void OnDelete()
		{
			Server.Misc.IntelligentAction.BurnAway( this );
			base.OnDelete();
		}

		public RuneGuardian( Serial serial ) : base( serial )
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
			this.Delete(); // none when the world starts 
		}
	}
}

namespace Server.Items
{
	public class RuneOnCorpse : Item
	{
		public Mobile rSummoner;

		[Constructable]
		public RuneOnCorpse() : base( 0x530D )
		{
			Movable = false;
			ItemRemovalTimer thisTimer = new ItemRemovalTimer( this ); 
			thisTimer.Start();
			Light = LightType.Circle150;
		}

		public RuneOnCorpse( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( rSummoner != from )
			{
				from.SendMessage( "The rune doesn't seem to budge." );
			}
			else if ( from.InRange( this.GetWorldLocation(), 5 ) )
			{
				string say = "If you had a chest of virtue to put the rune in, it wouldn't have vanished.";

				if ( from.Backpack.FindItemByType( typeof ( RuneBox ) ) != null )
				{
					Item box = from.Backpack.FindItemByType( typeof ( RuneBox ) );

					if ( box is RuneBox )
					{
						RuneBox chest = (RuneBox)box;
						say = "You take possession of the " + Name + "!";
						from.SendSound( 0x3D );
						LoggingFunctions.LogGeneric( from, "has found the " + Name + "." );

						if ( Name == "Rune of Compassion" ){ chest.HasCompassion = 1; }
						else if ( Name == "Rune of Honesty" ){ chest.HasHonesty = 1; }
						else if ( Name == "Rune of Honor" ){ chest.HasHonor = 1; }
						else if ( Name == "Rune of Humility" ){ chest.HasHumility = 1; }
						else if ( Name == "Rune of Justice" ){ chest.HasJustice = 1; }
						else if ( Name == "Rune of Sacrifice" ){ chest.HasSacrifice = 1; }
						else if ( Name == "Rune of Spirituality" ){ chest.HasSpirituality = 1; }
						else { chest.HasValor = 1; }
					}
				}
				from.SendMessage( say );
				this.Delete();
			}
			else
			{
				from.SendLocalizedMessage( 502138 ); // That is too far away for you to use
			}
		}

		public override void AddNameProperties( ObjectPropertyList list )
		{
			base.AddNameProperties( list );
			list.Add( 1070722, "Double Click To Take It" );
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
			this.Delete(); // none when the world starts 
		}

		public class ItemRemovalTimer : Timer 
		{ 
			private Item i_item; 
			public ItemRemovalTimer( Item item ) : base( TimeSpan.FromMinutes( 20.0 ) ) 
			{ 
				Priority = TimerPriority.OneSecond; 
				i_item = item; 
			} 

			protected override void OnTick() 
			{ 
				if (( i_item != null ) && ( !i_item.Deleted ))
				{
					i_item.Delete();
				}
			} 
		} 
	}
}