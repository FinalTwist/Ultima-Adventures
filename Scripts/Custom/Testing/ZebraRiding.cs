using System;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a zebra corpse" )]
	public class ZebraRiding : BaseMount
	{
		[Constructable]
		public ZebraRiding() : this( "a zebra" )
		{
		}

		[Constructable]
		public ZebraRiding( string name ) : base( name, 115, 115, AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			BaseSoundID = 0xA8;

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

			Fame = 300;
			Karma = 0;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 29.1;
		}

		public override int Meat{ get{ return 3; } }
		public override int Hides{ get{ return 10; } }
		public override int Furs{ get{ return Utility.RandomList( 0, 0, 0, 5 ); } }
		public override FurType FurType{ get{ return FurType.Regular; } }

		public override void OnDoubleClick( Mobile from )
		{
			if ( Server.Misc.MyServerSettings.ClientVersion() || ( Body == 0xE2 && ItemID == 0x3EA0 ) )
				base.OnDoubleClick( from );
		}

		public override bool OnBeforeDeath()
		{
			Server.Items.HorseArmor.DropArmor( this );
			return base.OnBeforeDeath();
		}

		public ZebraRiding( Serial serial ) : base( serial )
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

			if ( !Server.Misc.MyServerSettings.ClientVersion() && Body == 587 && ItemID == 587 )
			{
				Body = 0xE2;
				ItemID = 0x3EA0;
			}
		}
	}
}