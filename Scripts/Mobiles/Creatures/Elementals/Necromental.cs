using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Prompts;
using Server.Network;
using Server.Misc;

namespace Server.Mobiles
{
	[CorpseName( "a pile of rocks" )]
	public class Necromental : BaseCreature
	{
		public override double DispelDifficulty{ get{ return 120.5; } }
		public override double DispelFocus{ get{ return 35.0; } }

		public override int BreathPhysicalDamage{ get{ return 100; } }
		public override int BreathFireDamage{ get{ return 0; } }
		public override int BreathColdDamage{ get{ return 0; } }
		public override int BreathPoisonDamage{ get{ return 0; } }
		public override int BreathEnergyDamage{ get{ return 0; } }
		public override int BreathEffectHue{ get{ return 0; } }
		public override int BreathEffectSound{ get{ return 0x65A; } }
		public override int BreathEffectItemID{ get{ return 0; } }
		public override bool ReacquireOnMovement{ get{ return !Controlled; } }
		public override bool HasBreath{ get{ return true; } }
		public override double BreathEffectDelay{ get{ return 0.1; } }
		public override void BreathDealDamage( Mobile target, int form ){ base.BreathDealDamage( target, 29 ); }

		[Constructable]
		public Necromental() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a necromental";
			Body = 14;
			Hue = 0x763;
			BaseSoundID = 268;

			SetStr( 226, 255 );
			SetDex( 126, 145 );
			SetInt( 71, 92 );

			SetHits( 236, 353 );

			SetDamage( 28 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 30, 40 );
			SetResistance( ResistanceType.Fire, 40, 50 );
			SetResistance( ResistanceType.Cold, 40, 50 );
			SetResistance( ResistanceType.Poison, 30, 40 );
			SetResistance( ResistanceType.Energy, 10, 20 );

			SetSkill( SkillName.MagicResist, 50.1, 95.0 );
			SetSkill( SkillName.Tactics, 60.1, 100.0 );
			SetSkill( SkillName.Wrestling, 60.1, 100.0 );

			Fame = 3500;
			Karma = -3500;

			VirtualArmor = 32;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich, 3 );
			AddLoot( LootPack.Gems, 2 );
		}
		
		public override void OnDeath( Container c )
		{
			base.OnDeath( c );

   			c.DropItem( new GraveStones() );
			if ( Utility.RandomMinMax( 1, 5 ) == 1 ){ c.DropItem( new GraveStones() ); }
			if ( Utility.RandomMinMax( 1, 10 ) == 1 ){ c.DropItem( new GraveStones() ); }
		}

		public override int GetAttackSound(){ return 0x626; }	// A
		public override int GetDeathSound(){ return 0x627; }	// D
		public override int GetHurtSound(){ return 0x628; }		// H

		public override bool BleedImmune{ get{ return true; } }
		public override bool AutoDispel{ get{ return true; } }
		public override int TreasureMapLevel{ get{ return 4; } }

		public override void CheckReflect( Mobile caster, ref bool reflect )
		{
			if ( Utility.RandomMinMax( 1, 2 ) == 1 ){ reflect = true; } // 25% spells are reflected back to the caster
			else { reflect = false; }
		}

		public override void AlterMeleeDamageFrom( Mobile from, ref int damage )
		{
			if ( from is BaseCreature )
			{
				BaseCreature bc = (BaseCreature)from;

				if ( bc.Controlled || bc.BardTarget == this )
					damage = 0; // Immune to pets and provoked creatures
			}
			else if ( from != null )
			{
				int hitback = (int)(damage/2); if (hitback > 50){ hitback = 50; }
				AOS.Damage( from, this, hitback, 100, 0, 0, 0, 0 );
			}
		}

		public Necromental( Serial serial ) : base( serial )
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
	}
}

namespace Server.Items
{
	[Furniture]
	public class GraveStones : Item
	{
		[Constructable]
		public GraveStones() : base( 0x116E )
		{
			Weight = 10.0;
			Name = "grave stone";
			Hue = 0x763;
			ItemID = Utility.RandomList( 0xED4, 0xED5, 0xED6, 0xED7, 0xED8, 0xEDD, 0xEDE, 0x1165, 0x1166, 0x1167, 0x1168, 0x1169, 0x116A, 0x116B, 0x116C, 0x116D, 0x116E, 0x116F, 0x1170, 0x1171, 0x1172, 0x1173, 0x1174, 0x1175, 0x1176, 0x1177, 0x1178, 0x1179, 0x117A, 0x117B, 0x117C, 0x117D, 0x117E, 0x117F, 0x1180, 0x1181, 0x1182, 0x1183, 0x1184 );
			if ( Name == "grave stone" )
			{
				Name = "Here Lies " + RandomThings.GetRandomName() + " the " + RandomThings.GetRandomJobTitle(0);
			}
		}

		public GraveStones(Serial serial) : base(serial)
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			from.SendMessage("Please enter a new name for this grave stone.");
			from.Prompt = new RenamePrompt( this );
		}

		private class RenamePrompt : Prompt
		{
			private GraveStones m_Sign;

			public RenamePrompt( GraveStones sign )
			{
				m_Sign = sign;
			}

			public override void OnResponse( Mobile from, string text )
			{
				m_Sign.Name = text;
				from.SendMessage("The grave stone has been changed."); 
			}
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