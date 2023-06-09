//////////////////////
//Created By Kyleman//               
//////////////////////
using System;
using Server;

namespace Server.Items
{
	[FlipableAttribute( 0xDF1, 0xDF0 )]
	public class PowerPole : BaseStaff
	{
	 	public override int ArtifactRarity{ get{ return 15; } }
	 	public override int InitMinHits{ get{ return 250; } }
	 	public override int InitMaxHits{ get{ return Utility.RandomMinMax(126, 150); } }
		
		public override int DefMaxRange{ get{ return 3; } }
		
		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.WhirlwindAttack; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.ParalyzingBlow; } }

		public override int AosStrengthReq{ get{ return 35; } }
		public override int AosMinDamage{ get{ return 15; } }
		public override int AosMaxDamage{ get{ return 19; } }
		public override int AosSpeed{ get{ return 45; } }

		
	 	[Constructable]
	 	public PowerPole() : base( 0xDF0 )
	 	{
	 	 	Name = "gokus power pole";
	 	 	Hue = 142;
	 	 	Attributes.SpellChanneling = 1;
	 	 	Attributes.BonusStr = 15;
	 	 	Attributes.BonusHits = 15;
	 	 	Attributes.RegenHits = 10;
	 	 	WeaponAttributes.HitLeechHits = 50;
	 	 	Attributes.AttackChance = 50;
	 	 	Attributes.WeaponDamage = 50;
	 	 	Attributes.WeaponSpeed = 30;
	 	 	WeaponAttributes.HitFireArea = 50;
	 	 	WeaponAttributes.ResistFireBonus = 15;
	 	 	WeaponAttributes.ResistEnergyBonus = 5;
	 	 	WeaponAttributes.SelfRepair = 20;
	 	 	WeaponAttributes.HitFireball = 75;
			LootType = LootType.Blessed;
	 	}

	 	public PowerPole(Serial serial) : base( serial )
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