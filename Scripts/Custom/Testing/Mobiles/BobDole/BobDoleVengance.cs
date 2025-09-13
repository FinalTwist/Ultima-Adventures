using System;
using Server;

namespace Server.Items
{
	[FlipableAttribute( 0xF45, 0xF46 )]
	public class BobDoleVengance : BaseAxe
	{
	 	public override int ArtifactRarity{ get{ return 15; } }
	 	public override int InitMinHits{ get{ return 250; } }
	 	public override int InitMaxHits{ get{ return Utility.RandomMinMax(126, 150); } }
		
		public override int DefMaxRange{ get{ return 2; } }
		
		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.BleedAttack; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.MortalStrike; } }

		public override int AosStrengthReq{ get{ return 35; } }
		public override int AosMinDamage{ get{ return 15; } }
		public override int AosMaxDamage{ get{ return 20; } }
		public override int AosSpeed{ get{ return 37; } }
		public override float MlSpeed{ get{ return 2.50f; } }

		
	 	[Constructable]
	 	public BobDoleVengance() : base( 0xF45 )
	 	{
	 	 	Name = "Bob Dole's Vengance";
	 	 	Hue = 1151;
	 	 	Attributes.SpellChanneling = 1;
	 	 	Attributes.BonusStr = 10;
	 	 	Attributes.Luck = 150;
	 	 	Attributes.RegenHits = 1;
	 	 	WeaponAttributes.HitLeechHits = 50;
	 	 	Attributes.AttackChance = 25;
	 	 	Attributes.WeaponDamage = 65;
	 	 	Attributes.WeaponSpeed = 35;
	 	 	WeaponAttributes.HitHarm = 50;
	 	 	WeaponAttributes.ResistColdBonus = 15;
	 	 	WeaponAttributes.ResistEnergyBonus = 5;
	 	 	WeaponAttributes.SelfRepair = 20;
	 	 	WeaponAttributes.HitLightning = 75;
	 	}

	 	public BobDoleVengance(Serial serial) : base( serial )
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
