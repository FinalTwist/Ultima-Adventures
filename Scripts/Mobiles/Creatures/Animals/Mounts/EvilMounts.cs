using System;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "an animal corpse" )]
	public class EvilMount : BaseMount
	{
		public override bool DeleteCorpseOnDeath { get { return true; } }

		[Constructable]
		public EvilMount() : this( "an animal" )
		{
		}

		[Constructable]
		public EvilMount( string name ) : base( name, 0xE2, 0x3EA0, AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			SetStr( 22, 98 );
			SetDex( 56, 75 );
			SetInt( 6, 10 );

			SetHits( 28, 45 );
			SetMana( 0 );

			SetDamage( 3, 4 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 15, 20 );

			SetSkill( SkillName.MagicResist, 25.1, 30.0 );
			SetSkill( SkillName.Tactics, 29.3, 44.0 );
			SetSkill( SkillName.Wrestling, 29.3, 44.0 );

			Fame = 0;
			Karma = 0;
		}

		public override void OnThink()
		{
			if ( Rider == null ){ this.Delete(); }
			base.OnThink();
		}

		public EvilMount( Serial serial ) : base( serial )
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