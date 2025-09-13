
using System;
using Server;

namespace Server.Items
{
	public class Royalknightssword : VikingSword  
	{
		public override int ArtifactRarity{ get{ return 20; } }


		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.CrushingBlow; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.ParalyzingBlow; } }

		public override int AosStrengthReq{ get{ return 40; } }
		public override int AosMinDamage{ get{ return 15; } }
		public override int AosMaxDamage{ get{ return 17; } }
		public override int AosSpeed{ get{ return 28; } }
		public override float MlSpeed{ get{ return 3.75f; } }

		public override int OldStrengthReq{ get{ return 40; } }
		public override int OldMinDamage{ get{ return 6; } }
		public override int OldMaxDamage{ get{ return 34; } }
		public override int OldSpeed{ get{ return 30; } }

		public override int DefHitSound{ get{ return 0x237; } }
		public override int DefMissSound{ get{ return 0x23A; } }
		

		public override int InitMinHits{ get{ return Utility.RandomMinMax(100, 125); } }
		public override int InitMaxHits{ get{ return Utility.RandomMinMax(126, 150); } }

		[Constructable]
		public Royalknightssword()
		{
			Weight = 5.0;
            		Name = "Royal Knights Sword";
            		Hue = 1351;

			WeaponAttributes.DurabilityBonus = 1000; 
			//WeaponAttributes.HitFireArea = 100;
			WeaponAttributes.HitHarm = 46;
			WeaponAttributes.SelfRepair = 5;

			Attributes.AttackChance = 20;
			Attributes.BonusMana = 5;
			Attributes.BonusHits = 20;
			Attributes.DefendChance = 5;
			Attributes.Luck = 100;
			Attributes.WeaponDamage = 70;
			Attributes.WeaponSpeed = 40;


			StrRequirement = 60;

		}

		public Royalknightssword( Serial serial ) : base( serial )
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