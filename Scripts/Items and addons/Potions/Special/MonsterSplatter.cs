using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Misc;
using Server.Network;

namespace Server.Items
{
	public class MonsterSplatter : Item
	{
		public Mobile owner;

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Owner { get{ return owner; } set{ owner = value; } }

		[Constructable]
		public MonsterSplatter( Mobile source ) : base( 0x122A )
		{
			Weight = 1.0;
			Movable = false;
			owner = source;
			Name = "splatter";
			ItemID = Utility.RandomList( 0x122A, 0x122A, 0x122A, 0x122B, 0x122D, 0x122E, 0x263B, 0x263C, 0x263D, 0x263E, 0x263F, 0x2640 );
			ItemRemovalTimer thisTimer = new ItemRemovalTimer( this ); 
			thisTimer.Start(); 
		}

		public MonsterSplatter(Serial serial) : base(serial)
		{
		}

		public static int Hurt( Mobile m, int min, int max )
		{
			int v = 0;

			if ( m is PlayerMobile )
			{
				int alchemySkill = (int)(Server.Items.BasePotion.EnhancePotions( m ) / 5);
				v = Utility.RandomMinMax( min,max ) + alchemySkill;
			}
			else
			{
				v = Utility.RandomMinMax( min,max );
			}

			return v;
		}

		public override bool OnMoveOver( Mobile m )
		{
			bool hurts = true;

			if ( m.Blessed )
				hurts = false;

			if ( !m.Alive )
				hurts = false;

			if ( owner is BaseCreature && m is BaseCreature )
			{
				BaseCreature bc = (BaseCreature)m;

				if ( !bc.Controlled )
					hurts = false;
			}

			if ( hurts )
			{
				SlayerEntry SilverSlayer = SlayerGroup.GetEntryByName( SlayerName.Silver );
				SlayerEntry ExorcismSlayer = SlayerGroup.GetEntryByName( SlayerName.Exorcism );

				if ( m is PlayerMobile && Spells.Research.ResearchAirWalk.UnderEffect( m ) )
				{
					Point3D air = new Point3D( ( m.X+1 ), ( m.Y+1 ), ( m.Z+5 ) );
					Effects.SendLocationParticles(EffectItem.Create(air, m.Map, EffectItem.DefaultDuration), 0x2007, 9, 32, Server.Items.CharacterDatabase.GetMySpellHue( m, 0 ), 0, 5022, 0);
					m.PlaySound( 0x014 );
				}
				else if ( this.Name == "hot magma" && !(m is MagmaElemental) )
				{
					owner.DoHarmful( m );
					Effects.SendLocationParticles( EffectItem.Create( m.Location, m.Map, EffectItem.DefaultDuration ), 0x3709, 10, 30, 5052 );
					Effects.PlaySound( m.Location, m.Map, 0x225 );
					AOS.Damage( true, m, owner, Hurt( owner, 24, 48 ), 	0, 		100, 	0, 		0, 		0 );
					//												Ph,		Fr,		Cd,		Ps,		Eg
				}
				else if ( this.Name == "quick silver" )
				{
					owner.DoHarmful( m );
					Effects.PlaySound( m.Location, m.Map, 0x4D1 );
					AOS.Damage( true, m, owner, Hurt( owner, 24, 48 ), 	50, 	0, 		0, 		0, 		50 );
					//												Ph,		Fr,		Cd,		Ps,		Eg
				}
				else if ( this.Name == "holy water" && ( SilverSlayer.Slays(m) || ExorcismSlayer.Slays(m) ) )
				{
					owner.DoHarmful( m );
					Effects.SendLocationParticles( EffectItem.Create( m.Location, m.Map, EffectItem.DefaultDuration ), 0x3709, 10, 30, 5052 );
					Effects.PlaySound( m.Location, m.Map, 0x225 );
					AOS.Damage( true, m, owner, Hurt( owner, 40, 60 ), 20, 	20, 	20, 		20, 		20 );
					//												Ph,		Fr,		Cd,		Ps,		Eg
				}
				else if ( this.Name == "glowing goo" && !(m is GlowBeetle) && !(m is GlowBeetleRiding) )
				{
					owner.DoHarmful( m );
					Effects.SendLocationParticles( EffectItem.Create( m.Location, m.Map, EffectItem.DefaultDuration ), 0x36B0, 1, 14, 63, 7, 9915, 0 );
					int eSound = 0x229;
					if ( m.Body == 0x190 && m is PlayerMobile ){ eSound = 0x43F; }
					else if ( m.Body == 0x191 && m is PlayerMobile ){ eSound = 0x32D; }
					Effects.PlaySound( m.Location, m.Map, eSound );
					AOS.Damage( true, m, owner, Hurt( owner, 24, 48 ), 	0, 		0, 		0, 		50, 	50 );
					//												Ph,		Fr,		Cd,		Ps,		Eg
				}
				else if ( this.Name == "scorching ooze" && !(m is Lavapede) )
				{
					owner.DoHarmful( m );
					Effects.SendLocationParticles( EffectItem.Create( m.Location, m.Map, EffectItem.DefaultDuration ), 0x36B0, 1, 14, 63, 7, 9915, 0 );
					int eSound = 0x229;
					if ( m.Body == 0x190 && m is PlayerMobile ){ eSound = 0x43F; }
					else if ( m.Body == 0x191 && m is PlayerMobile ){ eSound = 0x32D; }
					Effects.PlaySound( m.Location, m.Map, eSound );
					AOS.Damage( true, m, owner, Hurt( owner, 24, 48 ), 	0, 		100, 	0, 		0, 		0 );
					//												Ph,		Fr,		Cd,		Ps,		Eg
				}
				else if ( this.Name == "blue slime" && !(m is SlimeDevil) )
				{
					owner.DoHarmful( m );
					Effects.PlaySound( m.Location, m.Map, 0x4D1 );
					AOS.Damage( true, m, owner, Hurt( owner, 24, 48 ), 	0, 		0, 		100, 	0, 		0 );
					//												Ph,		Fr,		Cd,		Ps,		Eg
				}
				else if ( this.Name == "swamp muck" && !(m is SwampThing) )
				{
					owner.DoHarmful( m );
					Effects.PlaySound( m.Location, m.Map, 0x4D1 );
					AOS.Damage( true, m, owner, Hurt( owner, 24, 48 ), 	50, 	0, 		0, 		50, 	0 );
					//												Ph,		Fr,		Cd,		Ps,		Eg
				}
				else if ( this.Name == "poisonous slime" && !(m is AbyssCrawler) )
				{
					owner.DoHarmful( m );
					Effects.PlaySound( m.Location, m.Map, 0x4D1 );
					AOS.Damage( true, m, owner, Hurt( owner, 24, 48 ), 	0, 		0, 		0, 		100, 	0 );
					//												Ph,		Fr,		Cd,		Ps,		Eg
				}
				else if ( this.Name == "poison spit" && !(m is Neptar) && !(m is NeptarWizard) )
				{
					owner.DoHarmful( m );
					Effects.PlaySound( m.Location, m.Map, 0x4D1 );
					AOS.Damage( true, m, owner, Hurt( owner, 24, 48 ), 	0, 		0, 		0, 		100, 	0 );
					//												Ph,		Fr,		Cd,		Ps,		Eg
				}
				else if ( this.Name == "poison spittle" && !(m is Lurker) )
				{
					owner.DoHarmful( m );
					Effects.PlaySound( m.Location, m.Map, 0x4D1 );
					AOS.Damage( true, m, owner, Hurt( owner, 24, 48 ), 	0, 		0, 		0, 		100, 	0 );
					//												Ph,		Fr,		Cd,		Ps,		Eg
				}
				else if ( this.Name == "fungal slime" && !(m is Fungal) && !(m is FungalMage) && !(m is CreepingFungus) )
				{
					owner.DoHarmful( m );
					Effects.PlaySound( m.Location, m.Map, 0x4D1 );
					AOS.Damage( true, m, owner, Hurt( owner, 24, 48 ), 	50, 	0, 		0, 		50, 	0 );
					//												Ph,		Fr,		Cd,		Ps,		Eg
				}
				else if ( this.Name == "spider ooze" && !(m is ZombieSpider) )
				{
					owner.DoHarmful( m );
					Effects.PlaySound( m.Location, m.Map, 0x4D1 );
					AOS.Damage( true, m, owner, Hurt( owner, 24, 48 ), 	50, 	0, 		0, 		50, 	0 );
					//												Ph,		Fr,		Cd,		Ps,		Eg
				}
				else if ( this.Name == "acidic slime" && !(m is ToxicElemental) )
				{
					owner.DoHarmful( m );
					Effects.PlaySound( m.Location, m.Map, 0x231 );
					AOS.Damage( true, m, owner, Hurt( owner, 24, 48 ), 	50, 	0, 		0, 		50, 	0 );
					//												Ph,		Fr,		Cd,		Ps,		Eg
				}
				else if ( this.Name == "acidic ichor" && !(m is AntaurKing) && !(m is AntaurProgenitor) && !(m is AntaurSoldier) && !(m is AntaurWorker) )
				{
					owner.DoHarmful( m );
					Effects.PlaySound( m.Location, m.Map, 0x231 );
					AOS.Damage( true, m, owner, Hurt( owner, 24, 48 ), 	50, 	0, 		0, 		50, 	0 );
					//												Ph,		Fr,		Cd,		Ps,		Eg
				}
				else if ( this.Name == "thick blood" && !(m is BloodElemental) && !(m is BloodDemon ) )
				{
					owner.DoHarmful( m );
					Effects.SendLocationParticles( EffectItem.Create( m.Location, m.Map, EffectItem.DefaultDuration ), 0x36B0, 1, 14, 0x25, 7, 9915, 0 );
					int eSound = 0x229;
					if ( m.Body == 0x190 && m is PlayerMobile ){ eSound = 0x43F; }
					else if ( m.Body == 0x191 && m is PlayerMobile ){ eSound = 0x32D; }
					Effects.PlaySound( m.Location, m.Map, eSound );
					AOS.Damage( true, m, owner, Hurt( owner, 24, 48 ), 	0, 		0, 		0, 		100, 	0 );
					//												Ph,		Fr,		Cd,		Ps,		Eg
				}
				else if ( this.Name == "infected blood" && !(m is Infected ) )
				{
					owner.DoHarmful( m );
					Effects.SendLocationParticles( EffectItem.Create( m.Location, m.Map, EffectItem.DefaultDuration ), 0x36B0, 1, 14, 0x25, 7, 9915, 0 );
					int eSound = 0x229;
					if ( m.Body == 0x190 && m is PlayerMobile ){ eSound = 0x43F; }
					else if ( m.Body == 0x191 && m is PlayerMobile ){ eSound = 0x32D; }
					Effects.PlaySound( m.Location, m.Map, eSound );
					AOS.Damage( true, m, owner, Hurt( owner, 24, 48 ), 	0, 		0, 		0, 		100, 	0 );
					//												Ph,		Fr,		Cd,		Ps,		Eg
				}
				else if ( this.Name == "alien blood" && !(m is Xenomorph ) && !(m is Xenomutant ) )
				{
					owner.DoHarmful( m );
					Effects.SendLocationParticles( EffectItem.Create( m.Location, m.Map, EffectItem.DefaultDuration ), 0x36B0, 1, 14, 0x25, 7, 9915, 0 );
					int eSound = 0x229;
					if ( m.Body == 0x190 && m is PlayerMobile ){ eSound = 0x43F; }
					else if ( m.Body == 0x191 && m is PlayerMobile ){ eSound = 0x32D; }
					Effects.PlaySound( m.Location, m.Map, eSound );
					AOS.Damage( true, m, owner, Hurt( owner, 24, 48 ), 	20, 	20, 	20, 	20, 	20 );
					//												Ph,		Fr,		Cd,		Ps,		Eg
				}
				else if ( this.Name == "green blood" && !(m is ZombieGiant ) )
				{
					owner.DoHarmful( m );
					Effects.SendLocationParticles( EffectItem.Create( m.Location, m.Map, EffectItem.DefaultDuration ), 0x36B0, 1, 14, 0x25, 7, 9915, 0 );
					int eSound = 0x229;
					if ( m.Body == 0x190 && m is PlayerMobile ){ eSound = 0x43F; }
					else if ( m.Body == 0x191 && m is PlayerMobile ){ eSound = 0x32D; }
					Effects.PlaySound( m.Location, m.Map, eSound );
					AOS.Damage( true, m, owner, Hurt( owner, 24, 48 ), 	20, 	0, 		0, 		80, 	0 );
					//												Ph,		Fr,		Cd,		Ps,		Eg
				}
				else if ( this.Name == "toxic blood" && !(m is Mutant ) )
				{
					owner.DoHarmful( m );
					Effects.SendLocationParticles( EffectItem.Create( m.Location, m.Map, EffectItem.DefaultDuration ), 0x36B0, 1, 14, 0x25, 7, 9915, 0 );
					int eSound = 0x229;
					if ( m.Body == 0x190 && m is PlayerMobile ){ eSound = 0x43F; }
					else if ( m.Body == 0x191 && m is PlayerMobile ){ eSound = 0x32D; }
					Effects.PlaySound( m.Location, m.Map, eSound );
					AOS.Damage( true, m, owner, Hurt( owner, 24, 48 ), 	0, 		0, 		0, 		100, 	0 );
					//												Ph,		Fr,		Cd,		Ps,		Eg
				}
				else if ( this.Name == "freezing water" && !(m is WaterElemental) && !(m is WaterWeird) && !(m is DeepWaterElemental) && !(m is Dagon) )
				{
					owner.DoHarmful( m );
					Effects.PlaySound( m.Location, m.Map, 0x4D1 );
					AOS.Damage( true, m, owner, Hurt( owner, 20, 40 ), 	0, 		0, 		100, 	0, 		0 );
					//												Ph,		Fr,		Cd,		Ps,		Eg
				}
				else if ( this.Name == "deep water" && !(m is WaterElemental) && !(m is WaterWeird) && !(m is DeepWaterElemental) && !(m is Dagon) )
				{
					owner.DoHarmful( m );
					Effects.PlaySound( m.Location, m.Map, 0x4D1 );
					AOS.Damage( true, m, owner, Hurt( owner, 40, 60 ), 	0, 		0, 		100, 	0, 		0 );
					//												Ph,		Fr,		Cd,		Ps,		Eg
				}
				else if ( this.Name == "lesser poison potion" || this.Name == "poison potion" || this.Name == "greater poison potion" | this.Name == "deadly poison potion" || this.Name == "lethal poison potion" )
				{
					int pSkill = (int)(owner.Skills[SkillName.Poisoning].Value/50);
					int tSkill = (int)(owner.Skills[SkillName.TasteID].Value/33);
					int aSkill = (int)(owner.Skills[SkillName.Alchemy].Value/33);

					int pMin = pSkill + tSkill + aSkill;
					int pMax = pMin * 2;
					Poison pois = Poison.Lesser;

					if ( this.Name == "poison potion" ){ 				pMin = pMin+2; 	pMax = pMax+2;	pois = Poison.Regular; }
					else if ( this.Name == "greater poison potion" ){ 	pMin = pMin+3; 	pMax = pMax+3;	pois = Poison.Greater; }
					else if ( this.Name == "deadly poison potion" ){ 	pMin = pMin+4; 	pMax = pMax+4;	pois = Poison.Deadly; }
					else if ( this.Name == "lethal poison potion" ){ 	pMin = pMin+5; 	pMax = pMax+5;	pois = Poison.Lethal; }

					if ( pMin >= Utility.RandomMinMax( 1, 16 ) )
					{
						m.ApplyPoison( owner, pois );
					}

					owner.DoHarmful( m );
					Effects.PlaySound( m.Location, m.Map, 0x4D1 );
					AOS.Damage( true, m, owner, Hurt( owner, pMin, pMax ), 	0, 		0, 		0, 		100, 	0 );
					//													Ph,		Fr,		Cd,		Ps,		Eg
				}
				else if ( this.Name == "liquid fire" )
				{
					int liqMin = Server.Items.BaseLiquid.GetLiquidBonus( owner );
					int liqMax = liqMin * 2;
					owner.DoHarmful( m );
					Effects.SendLocationEffect( m.Location, m.Map, 0x3709, 30, 10 );
					m.PlaySound( 0x208 );
					AOS.Damage( true, m, owner, Hurt( owner, liqMin, liqMax ), 	20, 	80, 	0, 		0, 		0 );
					//														Ph,		Fr,		Cd,		Ps,		Eg
				}
				else if ( this.Name == "liquid goo" )
				{
					int liqMin = Server.Items.BaseLiquid.GetLiquidBonus( owner );
					int liqMax = liqMin * 2;
					owner.DoHarmful( m );
					Effects.SendLocationEffect( m.Location, m.Map, Utility.RandomList( 0x3967, 0x3979 ), 30, 10 );
					m.PlaySound( 0x5C3 );
					AOS.Damage( true, m, owner, Hurt( owner, liqMin, liqMax ), 	20, 	0, 		0, 		0, 		80 );
					//														Ph,		Fr,		Cd,		Ps,		Eg
				}
				else if ( this.Name == "liquid ice" )
				{
					int liqMin = Server.Items.BaseLiquid.GetLiquidBonus( owner );
					int liqMax = liqMin * 2;
					owner.DoHarmful( m );
					Effects.SendLocationEffect( m.Location, m.Map, 0x1A84, 30, 10, 0x9C1, 0 );
					m.PlaySound( 0x10B );
					AOS.Damage( true, m, owner, Hurt( owner, liqMin, liqMax ), 	20, 	0, 		80, 	0, 		0 );
					//														Ph,		Fr,		Cd,		Ps,		Eg
				}
				else if ( this.Name == "liquid rot" )
				{
					int liqMin = Server.Items.BaseLiquid.GetLiquidBonus( owner );
					int liqMax = liqMin * 2;
					owner.DoHarmful( m );
					Effects.SendLocationEffect( m.Location, m.Map, 0x3400, 60 );
					Effects.PlaySound( m.Location, m.Map, 0x108 );
					AOS.Damage( true, m, owner, Hurt( owner, liqMin, liqMax ), 	20, 	0, 		0, 		80, 	0 );
					//														Ph,		Fr,		Cd,		Ps,		Eg
				}
				else if ( this.Name == "liquid pain" )
				{
					int liqMin = Server.Items.BaseLiquid.GetLiquidBonus( owner );
					int liqMax = liqMin * 2;
					owner.DoHarmful( m );
					m.FixedParticles( 0x37C4, 1, 8, 9916, 39, 3, EffectLayer.Head );
					m.FixedParticles( 0x37C4, 1, 8, 9502, 39, 4, EffectLayer.Head );
					m.PlaySound( 0x210 );
					AOS.Damage( true, m, owner, Hurt( owner, liqMin, liqMax ), 	80, 	5, 		5, 		5, 		5 );
					//														Ph,		Fr,		Cd,		Ps,		Eg
				}
			}
			return true;
		}

		public static void AddSplatter( int iX, int iY, int iZ, Map iMap, Point3D iLoc, Mobile source, string description, int color, int glow )
		{
			Effects.PlaySound(iLoc, iMap, 0x026);

			double weight = 1.0;	if ( glow > 0 ){ weight = 2.0; }

			MonsterSplatter Spill = new MonsterSplatter( source ); Spill.Name = description; Spill.Hue = color; Spill.Weight = weight;
				Spill.MoveToWorld( new Point3D( (iX-2), (iY-1), iZ ), iMap );
			Spill = new MonsterSplatter( source ); Spill.Name = description; Spill.Hue = color; Spill.Weight = weight;
				Spill.MoveToWorld( new Point3D( (iX-1), (iY-1), iZ ), iMap );
			Spill = new MonsterSplatter( source ); Spill.Name = description; Spill.Hue = color; Spill.Weight = weight;
				Spill.MoveToWorld( new Point3D( (iX-1), iY, iZ ), iMap );
			Spill = new MonsterSplatter( source ); Spill.Name = description; Spill.Hue = color; Spill.Weight = weight;
				Spill.MoveToWorld( new Point3D( (iX-1), (iY+1), iZ ), iMap );
			Spill = new MonsterSplatter( source ); Spill.Name = description; Spill.Hue = color; Spill.Weight = weight;
				Spill.MoveToWorld( new Point3D( iX, (iY+1), iZ ), iMap );
			Spill = new MonsterSplatter( source ); Spill.Name = description; Spill.Hue = color; Spill.Weight = weight;
				Spill.MoveToWorld( new Point3D( (iX+1), (iY+1), iZ ), iMap );
			Spill = new MonsterSplatter( source ); Spill.Name = description; Spill.Hue = color; Spill.Weight = weight;
				Spill.MoveToWorld( new Point3D( (iX+1), iY, iZ ), iMap );
			Spill = new MonsterSplatter( source ); Spill.Name = description; Spill.Hue = color; Spill.Weight = weight;
				Spill.MoveToWorld( new Point3D( (iX+1), (iY-1), iZ ), iMap );
			Spill = new MonsterSplatter( source ); Spill.Name = description; Spill.Hue = color; Spill.Weight = weight;
				Spill.MoveToWorld( new Point3D( iX, (iY-1), iZ ), iMap );
			Spill = new MonsterSplatter( source ); Spill.Name = description; Spill.Hue = color; Spill.Weight = weight;
				Spill.MoveToWorld( new Point3D( (iX+1), (iY-2), iZ ), iMap );
			Spill = new MonsterSplatter( source ); Spill.Name = description; Spill.Hue = color; Spill.Weight = weight;
				Spill.MoveToWorld( new Point3D( (iX+2), (iY-2), iZ ), iMap );
			Spill = new MonsterSplatter( source ); Spill.Name = description; Spill.Hue = color; Spill.Weight = weight;
				Spill.MoveToWorld( new Point3D( (iX-2), (iY+1), iZ ), iMap );
			Spill = new MonsterSplatter( source ); Spill.Name = description; Spill.Hue = color; Spill.Weight = weight;
				Spill.MoveToWorld( new Point3D( (iX-2), (iY+2), iZ ), iMap );
			Spill = new MonsterSplatter( source ); Spill.Name = description; Spill.Hue = color; Spill.Weight = weight;
				Spill.MoveToWorld( new Point3D( (iX+1), (iY+2), iZ ), iMap );

			if ( glow > 0 )
			{
				StrangeGlow Glow = new StrangeGlow(); Glow.Name = description;
					Glow.MoveToWorld( new Point3D( (iX-2), (iY-1), iZ ), iMap );
				Glow = new StrangeGlow(); Glow.Name = description;
					Glow.MoveToWorld( new Point3D( (iX-1), (iY-1), iZ ), iMap );
				Glow = new StrangeGlow(); Glow.Name = description;
					Glow.MoveToWorld( new Point3D( (iX-1), iY, iZ ), iMap );
				Glow = new StrangeGlow(); Glow.Name = description;
					Glow.MoveToWorld( new Point3D( (iX-1), (iY+1), iZ ), iMap );
				Glow = new StrangeGlow(); Glow.Name = description;
					Glow.MoveToWorld( new Point3D( iX, (iY+1), iZ ), iMap );
				Glow = new StrangeGlow(); Glow.Name = description;
					Glow.MoveToWorld( new Point3D( (iX+1), (iY+1), iZ ), iMap );
				Glow = new StrangeGlow(); Glow.Name = description;
					Glow.MoveToWorld( new Point3D( (iX+1), iY, iZ ), iMap );
				Glow = new StrangeGlow(); Glow.Name = description;
					Glow.MoveToWorld( new Point3D( (iX+1), (iY-1), iZ ), iMap );
				Glow = new StrangeGlow(); Glow.Name = description;
					Glow.MoveToWorld( new Point3D( iX, (iY-1), iZ ), iMap );
				Glow = new StrangeGlow(); Glow.Name = description;
					Glow.MoveToWorld( new Point3D( (iX+1), (iY-2), iZ ), iMap );
				Glow = new StrangeGlow(); Glow.Name = description;
					Glow.MoveToWorld( new Point3D( (iX+2), (iY-2), iZ ), iMap );
				Glow = new StrangeGlow(); Glow.Name = description;
					Glow.MoveToWorld( new Point3D( (iX-2), (iY+1), iZ ), iMap );
				Glow = new StrangeGlow(); Glow.Name = description;
					Glow.MoveToWorld( new Point3D( (iX-2), (iY+2), iZ ), iMap );
				Glow = new StrangeGlow(); Glow.Name = description;
					Glow.MoveToWorld( new Point3D( (iX+1), (iY+2), iZ ), iMap );
			}
		}

		public static bool TooMuchSplatter( Mobile from )
		{
			int splatter = 0;

			foreach ( Item i in from.GetItemsInRange( 10 ) )
			{
				if ( i is MonsterSplatter )
				{
					MonsterSplatter splat = (MonsterSplatter)i;
					if ( splat.owner != from )
						splatter++;
				}
			}

			if ( splatter > 16 ){ return true; }

			return false;
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			writer.Write( (Mobile)owner );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			owner = reader.ReadMobile();
			this.Delete(); // none when the world starts 
		}

		public class ItemRemovalTimer : Timer 
		{ 
			private Item i_item; 
			public ItemRemovalTimer( Item item ) : base( TimeSpan.FromSeconds( 30.0 ) ) 
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