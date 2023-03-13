using System;
using Server.Items;
using Server.Mobiles;
using Server.Misc;

namespace Server.Mobiles
{
	[CorpseName( "a dinosaur corpse" )]
	public class RaptorRiding : BaseMount
	{
		[Constructable]
		public RaptorRiding() : this( "a raptor" )
		{
		}

		[Constructable]
		public RaptorRiding( string name ) : base( name, 218, 16036, AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			if (Utility.RandomDouble() > 0.85)
		{
			BaseSoundID = 0x5A;
Name = "a greater raptor";
			SetStr( 126, 250 );
			SetDex( 56, 175 );
			SetInt( 11, 20 );

			SetHits( 76, 190 );
			SetMana( 0 );

			SetDamage( 6, 29 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 35, 55 );
			SetResistance( ResistanceType.Fire, 30, 55 );
			SetResistance( ResistanceType.Poison, 25, 45 );
			SetResistance( ResistanceType.Energy, 25, 35 );

			SetSkill( SkillName.MagicResist, 55.1, 70.0 );
			SetSkill( SkillName.Tactics, 60.1, 80.0 );
			SetSkill( SkillName.Wrestling, 60.1, 80.0 );

			Fame = 4000;
			Karma = -3000;

			VirtualArmor = 40;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 89.7;
		}
		else
		{
			BaseSoundID = 0x5A;

			SetStr( 126, 150 );
			SetDex( 56, 75 );
			SetInt( 11, 20 );

			SetHits( 76, 90 );
			SetMana( 0 );

			SetDamage( 6, 24 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 35, 45 );
			SetResistance( ResistanceType.Fire, 30, 45 );
			SetResistance( ResistanceType.Poison, 25, 35 );
			SetResistance( ResistanceType.Energy, 25, 35 );

			SetSkill( SkillName.MagicResist, 55.1, 70.0 );
			SetSkill( SkillName.Tactics, 60.1, 80.0 );
			SetSkill( SkillName.Wrestling, 60.1, 80.0 );

			Fame = 3000;
			Karma = -3000;

			VirtualArmor = 40;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 80.7;
		}
	}

		public override void OnAfterSpawn()
		{
			base.OnAfterSpawn();

			if ( Worlds.GetMyWorld( this.Map, this.Location, this.X, this.Y ) == "the Underworld" )
			{ 
			    this.Body = 116;
			    this.ItemID = Server.Mobiles.BaseMount.MountBody(116);
			    if (Utility.RandomBool())
			    {
				this.Body = 219;
				this.ItemID = Server.Mobiles.BaseMount.MountBody(219);
				this.Name = "a darkrazor"; 
			    }
			}
			else if ( Worlds.GetMyWorld( this.Map, this.Location, this.X, this.Y ) == "the Isles of Dread" ){ this.Body = Server.Mobiles.BaseMount.MountBody(117); }
		}

		public override HideType HideType{ get{ return HideType.Dinosaur; } }
		public override int Meat{ get{ return 4; } }
		public override int Hides{ get{ return 12; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Fish | FoodType.Meat; } }
		public override int Scales{ get{ return 2; } }
		public override ScaleType ScaleType{ get{ return ( ScaleType.Dinosaur ); } }

		public override int GetAttackSound(){ return 0x622; }	// A
		public override int GetDeathSound(){ return 0x623; }	// D
		public override int GetHurtSound(){ return 0x624; }		// H

		public RaptorRiding(Serial serial) : base(serial)
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

			if ( Body == 116 ) { ItemID = Server.Mobiles.BaseMount.MountBody(116); }
			else if ( Body == 117 ) { ItemID = Server.Mobiles.BaseMount.MountBody(117); }
			else if ( Body == 219 ) { ItemID = Server.Mobiles.BaseMount.MountBody(219); }
			else { ItemID = 16036; }
		}
	}
}
