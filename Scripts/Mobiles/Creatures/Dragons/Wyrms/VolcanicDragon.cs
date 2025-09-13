using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a wyrm corpse" )]
	public class VolcanicDragon : BaseCreature
	{
		public override bool ReacquireOnMovement{ get{ return !Controlled; } }
		public override bool HasBreath{ get{ return true; } }
		public override double BreathEffectDelay{ get{ return 0.1; } }
		public override bool CanChew { get{return true;}}
		public override void BreathDealDamage( Mobile target, int form ){ base.BreathDealDamage( target, 9 ); }

		[Constructable]
		public VolcanicDragon () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = NameList.RandomName( "dragon" );
			Title = "the volcanic wyrm";
			Body = 106;
			BaseSoundID = 362;
			Hue = 0xB85;

			SetStr( 1096, 1185 );
			SetDex( 86, 175 );
			SetInt( 686, 775 );

			SetHits( 658, 711 );

			SetDamage( 29, 35 );

			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Fire, 50 );

			SetResistance( ResistanceType.Physical, 65, 75 );
			SetResistance( ResistanceType.Fire, 100 );
			SetResistance( ResistanceType.Cold, 70, 80 );
			SetResistance( ResistanceType.Poison, 60, 70 );
			SetResistance( ResistanceType.Energy, 60, 70 );

			SetSkill( SkillName.EvalInt, 80.1, 100.0 );
			SetSkill( SkillName.Magery, 80.1, 100.0 );
			SetSkill( SkillName.Meditation, 52.5, 75.0 );
			SetSkill( SkillName.MagicResist, 100.5, 150.0 );
			SetSkill( SkillName.Tactics, 97.6, 100.0 );
			SetSkill( SkillName.Wrestling, 97.6, 100.0 );

			Fame = 22500;
			Karma = -22500;

			VirtualArmor = 70;
		}

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );
			
			if ( 1 == Utility.RandomMinMax( 0, 2 ) )
			{
				LootChest MyChest = new LootChest( Server.Misc.IntelligentAction.FameBasedLevel( this ) );
				MyChest.Name = "obsidian chest";
				MyChest.Hue = 0x497;
				MyChest.ItemID = Utility.RandomList( 0xe40, 0xe41 );
				c.DropItem(MyChest);
			}
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 3 );
			AddLoot( LootPack.Gems, 5 );
		}

		public override bool AutoDispel{ get{ return true; } }
		public override HideType HideType{ get{ if ( Utility.RandomBool() ){ return HideType.Volcanic; } else { return HideType.Draconic; } } }
		public override int Hides{ get{ return 40; } }
		public override int Meat{ get{ return 19; } }
		public override int Scales{ get{ return 12; } }
		public override ScaleType ScaleType{ get{ return ScaleType.Black; } }
		public override Poison PoisonImmune{ get{ return Poison.Regular; } }
		public override Poison HitPoison{ get{ return Utility.RandomBool() ? Poison.Lesser : Poison.Regular; } }
		public override int TreasureMapLevel{ get{ return 5; } }


        public override int GetAngerSound()
        {
            return 0x63E;
        }

        public override int GetDeathSound()
        {
            return 0x63F;
        }

        public override int GetHurtSound()
        {
            return 0x640;
        }

        public override int GetIdleSound()
        {
            return 0x641;
        }

		public VolcanicDragon( Serial serial ) : base( serial )
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