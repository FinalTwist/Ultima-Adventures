using System;
using Server.Items;

namespace Server.Items
{
	public class Knuckles : BaseBashing
	{
		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.Disarm; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.ConcussionBlow; } }

		public override int AosStrengthReq{ get{ return 25; } }
		public override int AosMinDamage{ get{ return 9; } }
		public override int AosMaxDamage{ get{ return 14; } }
		public override int AosSpeed{ get{ return 37; } }

		public override int OldStrengthReq{ get{ return 10; } }
		public override int OldMinDamage{ get{ return 4; } }
		public override int OldMaxDamage{ get{ return 30; } }
		public override int OldSpeed{ get{ return 43; } }

		public override int DefHitSound{ get{ return 0x23B; } }
		public override int DefMissSound{ get{ return 0x23A; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 90; } }

		public override SkillName DefSkill{ get{ return SkillName.Wrestling; } }

		[Constructable]
		public Knuckles() : base( 0x1414 )
		{
			Name = "Wrestler's Knuckles";
			Layer = Layer.OneHanded;
		}

		public Knuckles( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}