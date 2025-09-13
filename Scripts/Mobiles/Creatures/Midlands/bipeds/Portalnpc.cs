using System;
using Server.Mobiles;
using Server.Items;
using Server.Misc;

namespace Server.Mobiles
{
	[CorpseName( "a human corpse" )]
	public class PortalNpc : BaseCreature
	{
		[Constructable]
		public PortalNpc() : base( AIType.AI_Melee, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			Hue = Server.Misc.RandomThings.GetRandomSkinColor();

			if ( this.Female = Utility.RandomBool() )
			{
				Body = 0x191;
				Name = NameList.RandomName( "female" );
			}
			else
			{
				Body = 0x190;
				Name = NameList.RandomName( "male" );
			}

			SetStr( 27, 37 );
			SetDex( 28, 43 );
			SetInt( 29, 37 );

			SetHits( 17, 22 );
			SetMana( 0 );

			SetDamage( 4, 7 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 10, 15 );

			SetSkill( SkillName.Fencing, 30.0, 40.5 );
			SetSkill( SkillName.Macing, 30.0, 40.5 );
			SetSkill( SkillName.MagicResist, 30.0, 40.5 );
			SetSkill( SkillName.Swords, 30.0, 40.5 );
			SetSkill( SkillName.Tactics, 30.0, 40.5 );
			SetSkill( SkillName.Wrestling, 30.0, 40.5 );

			Fame = 0;
			Karma = 300;

			VirtualArmor = 12;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = -15.3;
			
			AddItem( new TinkerTools() );

			switch ( Utility.Random( 7 ))
			{
				case 0: AddItem( new Hatchet() ); break;
				case 1: AddItem( new Pickaxe() ); break;
			}
			
			Utility.AssignRandomHair( this );
		}


		public PortalNpc( Serial serial ) : base( serial )
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