using System;
using Server;
using Server.Items;
using Server.Misc;

namespace Server.Mobiles
{
	[CorpseName( "a dragon corpse" )]
	public class DragonKing : BaseCreature
	{
		public override bool ReacquireOnMovement{ get{ return !Controlled; } }
		public override bool HasBreath{ get{ return true; } }
		public override double BreathEffectDelay{ get{ return 0.1; } }
		public override void BreathDealDamage( Mobile target, int form ){ base.BreathDealDamage( target, 9 ); }

		[Constructable]
		public DragonKing () : base( AIType.AI_Mage, FightMode.Weakest, 10, 1, 0.2, 0.4 )
		{
			Name = NameList.RandomName( "dragon" );
			Title = "the dragon king";
			Body = 106;
			BaseSoundID = 362;

			SetStr( 1096, 1185 );
			SetDex( 86, 175 );
			SetInt( 686, 775 );

			SetHits( 1500, 1900 );

			SetDamage( 29, 35 );

			SetDamageType( ResistanceType.Physical, 75 );
			SetDamageType( ResistanceType.Fire, 25 );

			SetResistance( ResistanceType.Physical, 65, 75 );
			SetResistance( ResistanceType.Fire, 80, 90 );
			SetResistance( ResistanceType.Cold, 60, 70 );
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


			Mobile killer = this.LastKiller;
			if ( killer != null )
			{
				if ( killer is BaseCreature )
					killer = ((BaseCreature)killer).GetMaster();

				if ( killer is PlayerMobile )
				{
					if ( GetPlayerInfo.LuckyKiller( killer.Luck, killer ) && Server.Misc.MyServerSettings.ClientVersion() && !CharacterDatabase.GetKeys( killer, "DragonRiding" ) )
					{
						c.DropItem( new DragonRidingScroll() );
					}
					if ( GetPlayerInfo.LuckyKiller( killer.Luck, killer ) && Utility.RandomMinMax( 1, 5 ) == 1 && !Server.Items.CharacterDatabase.GetSpecialsKilled( killer, "DragonKing" ) )
					{
						Server.Items.CharacterDatabase.SetSpecialsKilled( killer, "DragonKing", true );
						ManualOfItems book = new ManualOfItems();
							book.Hue = 0x6DF;
							book.Name = "Tome of Dragon King Relics";
							book.m_Charges = 1;
							book.m_Skill_1 = 99;
							book.m_Skill_2 = 0;
							book.m_Skill_3 = 0;
							book.m_Skill_4 = 0;
							book.m_Skill_5 = 0;
							book.m_Value_1 = 20.0;
							book.m_Value_2 = 0.0;
							book.m_Value_3 = 0.0;
							book.m_Value_4 = 0.0;
							book.m_Value_5 = 0.0;
							book.m_Slayer_1 = 6;
							book.m_Slayer_2 = 0;
							book.m_Owner = null;
							book.m_Extra = "of the Dragon King";
							book.m_FromWho = "Taken from the King of Dragons";
							book.m_HowGiven = "Acquired by";
							book.m_Points = 150;
							book.m_Hue = 0x6DF;
							c.DropItem( book );
					}
					Server.Mobiles.Dragons.DropSpecial( this, killer, "", "Dark", this.Name + " " + this.Title, c, 10, 0x6DD );
				}
			}
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 3 );
			AddLoot( LootPack.Gems, 5 );
		}

		public override bool AutoDispel{ get{ return true; } }
		public override HideType HideType{ get{ return HideType.Draconic; } }
		public override int Hides{ get{ return 40; } }
		public override int Meat{ get{ return 19; } }
		public override int Scales{ get{ return 12; } }
		public override ScaleType ScaleType{ get{ return ScaleType.Blue; } }
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

		public DragonKing( Serial serial ) : base( serial )
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