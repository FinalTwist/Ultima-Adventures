using System;
using System.Collections;
using Server.Items;
using Server.ContextMenus;
using Server.Misc;
using Server.Network;

namespace Server.Mobiles
{
	[CorpseName( "a pile of cinders" )]
	public class Genie : BasePerson
	{
		[Constructable]
		public Genie () : base( )
		{
			Name = "a genie";
			Body = 13;
			BaseSoundID = 655;
			Hue = 0x489;

			SetStr( 126, 155 );
			SetDex( 166, 185 );
			SetInt( 101, 125 );

			SetHits( 76, 93 );

			SetDamage( 8, 10 );

			SetDamageType( ResistanceType.Physical, 20 );
			SetDamageType( ResistanceType.Cold, 40 );
			SetDamageType( ResistanceType.Energy, 40 );

			SetResistance( ResistanceType.Physical, 35, 45 );
			SetResistance( ResistanceType.Fire, 15, 25 );
			SetResistance( ResistanceType.Cold, 10, 20 );
			SetResistance( ResistanceType.Poison, 10, 20 );
			SetResistance( ResistanceType.Energy, 25, 35 );

			SetSkill( SkillName.EvalInt, 60.1, 75.0 );
			SetSkill( SkillName.Magery, 60.1, 75.0 );
			SetSkill( SkillName.MagicResist, 60.1, 75.0 );
			SetSkill( SkillName.Tactics, 60.1, 80.0 );
			SetSkill( SkillName.Wrestling, 60.1, 80.0 );

			Fame = 4;
			Karma = -4;

			VirtualArmor = 40;
			CantWalk = true;
			Direction = Direction.North;
		}

		public override bool OnBeforeDeath()
		{
			Mobile killer = this.FindMostRecentDamager(true);

			if ( !base.OnBeforeDeath() )
				return false;

			string bSay = "Whoosheesha!";

			this.PublicOverheadMessage( MessageType.Regular, 0, false, string.Format ( bSay ) );
			return true;
		}

		public Genie( Serial serial ) : base( serial )
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