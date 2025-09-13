using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Engines.CannedEvil;

namespace Server.Mobiles
{
	public class Mephitis : BaseChampion
	{
		public override ChampionSkullType SkullType{ get{ return ChampionSkullType.Venom; } }

		public override Type[] UniqueList{ get{ return new Type[] { typeof( Calm ) }; } }
		public override Type[] SharedList { get { return new Type[] { typeof(OblivionsNeedle), typeof(ANecromancerShroud), typeof(EmbroideredOakLeafCloak), typeof(TheMostKnowledgePerson) }; } }
		public override Type[] DecorativeList{ get{ return new Type[] { typeof( Web ), typeof( MonsterStatuette ) }; } }

		public override MonsterStatuetteType[] StatueTypes{ get{ return new MonsterStatuetteType[] { MonsterStatuetteType.Spider }; } }

		private static List<Mobile> stingtargets = new List<Mobile>();

		[Constructable]
		public Mephitis() : base( AIType.AI_Melee )
		{
			Body = 180;
			Name = "Mephitis";

			BaseSoundID = 0x183;

			SetStr( 505, 1000 );
			SetDex( 102, 300 );
			SetInt( 402, 600 );

			SetHits( 6500 );
			SetStam( 105, 600 );

			SetDamage( 41, 70 );

			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Poison, 50 );

			SetResistance( ResistanceType.Physical, 75, 80 );
			SetResistance( ResistanceType.Fire, 60, 70 );
			SetResistance( ResistanceType.Cold, 60, 70 );
			SetResistance( ResistanceType.Poison, 100 );
			SetResistance( ResistanceType.Energy, 60, 70 );

			SetSkill( SkillName.MagicResist, 70.7, 140.0 );
			SetSkill( SkillName.Tactics, 97.6, 100.0 );
			SetSkill( SkillName.Wrestling, 97.6, 120.0 );
			SetSkill( SkillName.Poisoning, 100, 120.0 );
			SetSkill( SkillName.Magery, 100, 120.0 );
			SetSkill( SkillName.DetectHidden, 110, 120.0 );

			Fame = 22500;
			Karma = -22500;

			VirtualArmor = 80;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.UltraRich, 4 );
		}

		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		public override Poison HitPoison{ get{ return Poison.Lethal; } }

		public override void OnGaveMeleeAttack( Mobile attacker )
		{

			if ( Utility.RandomDouble() < 0.20 )
				Sting( attacker );

			base.OnGaveMeleeAttack( attacker );

		}

		public override void OnGotMeleeAttack( Mobile attacker )
		{

			if ( Utility.RandomDouble() < 0.20 )
				Sting( attacker );

			base.OnGotMeleeAttack( attacker );

		}


		public void Sting( Mobile victim )
		{
			if (!HasBeenStung(victim))
				{
					Mobile m = victim;
					this.Direction = this.GetDirectionTo(m);
					Item web = new ParalyzingWeb();
					
					int time = Utility.RandomMinMax(4,8);

					m.Paralyze(TimeSpan.FromSeconds(time));

					Effects.SendMovingParticles(this, m, web.ItemID, 12, 0, false, false, 0, 0, 9502, 1, 0, (EffectLayer)255, 0x100);
					web.MoveToWorld(m.Location, this.Map);

					if (m is PlayerMobile)
						m.SendMessage( 0, "The creature injects something in your skin!" );

					Timer.DelayCall( TimeSpan.FromSeconds( 2 ), new TimerStateCallback ( Grow ), new object[]{ m, web } );

					if (time > 6)
						Timer.DelayCall( TimeSpan.FromSeconds( 5 ), new TimerStateCallback ( Grow ), new object[]{ m, web } );

					Timer.DelayCall( TimeSpan.FromSeconds( time ), new TimerStateCallback ( Harvest ), new object[]{ m, web } );

					HasBeenStung(victim, true);
				}
		}

		public static bool HasBeenStung (Mobile target)
		{
			return HasBeenStung(target, false, false);
		}

		public static bool HasBeenStung (Mobile target, bool decision)
		{
			return HasBeenStung(target, decision, false);
		}

		public static bool HasBeenStung (Mobile target, bool add, bool remove)
		{
			if (Mephitis.stingtargets == null)
				Mephitis.stingtargets = new List<Mobile>();

			if (add)
			{
				Mephitis.stingtargets.Add( target );
				return true;
			}
			if (remove)
			{
				Mephitis.stingtargets.Remove(target);
				return false;
			}
			else 
			{
				for ( int i = 0; i < Mephitis.stingtargets.Count; i++ ) // check if mobile is in list
				{			
					Mobile m = (Mobile)stingtargets[i];
					if (m == target) //already in the list
						return true;
				}
				
				return false; //not in the list, hasnt been stung
			}
		}

		public void Grow( object state )
		{

			if (this.Deleted || this == null)
				return;
				
			object[] states = (object[])state;

			Mobile target = (Mobile)states[0];
			Item web = (Item)states[1];

			Map map = target.Map;

			if ( web == null || map == null || map == Map.Internal || target == null )
				return;

			if (target is PlayerMobile)
				target.SendMessage( 0, "Something is eating you from within!" );
			
			int damnit = Utility.RandomMinMax( target.Hits/10, target.Hits/3);

			AOS.Damage( target, this, (int)damnit, 0, 0, 0, 100, 0 );

		}

		public void Harvest( object state )
		{

			if (this.Deleted || this == null)
				return;
				
			object[] states = (object[])state;

			Mobile target = (Mobile)states[0];
			Item web = (Item)states[1];

			Map map = target.Map;

			if ( web == null || map == null || map == Map.Internal || target == null )
				return;

			if (target is PlayerMobile)
				target.SendMessage( 0, "Creatures Burst from your body!" );
			
			int damnit = Utility.RandomMinMax( target.Hits/5, target.Hits/3);
			AOS.Damage( target, this, (int)damnit, 0, 0, 0, 100, 0 );

			if (web != null)
				web.Delete();

			for ( int i = 0; i < Utility.RandomMinMax(4,7); ++i )
			{
				Mobile spawn = new DreadSpider();
				spawn.OnAfterSpawn();

				spawn.MoveToWorld( target.Location, target.Map);
				spawn.Combatant = target;
			}
			HasBeenStung(target, false, true);

		}

		public Mephitis( Serial serial ) : base( serial )
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