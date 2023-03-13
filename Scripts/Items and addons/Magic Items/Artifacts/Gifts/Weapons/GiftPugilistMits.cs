using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	[FlipableAttribute( 0x13C6, 0x13C6 )]
	public class GiftPugilistGloves : BaseGiftBashing
	{
		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.Disarm; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.ParalyzingBlow; } }
		public override WeaponAbility ThirdAbility{ get{ return WeaponAbility.WhirlwindAttack; } }
		public override WeaponAbility FourthAbility{ get{ return WeaponAbility.FistsOfFury; } }
		public override WeaponAbility FifthAbility{ get{ return WeaponAbility.DeathBlow; } }

		public override int AosStrengthReq{ get{ return 20; } }
		public override int AosMinDamage{ get{ return 2; } }
		public override int AosMaxDamage{ get{ return 5; } }
		public override int AosSpeed{ get{ return 2; } }
		public override float MlSpeed{ get{ return 2.00f; } }

		public override int DefHitSound{ get{ return -1; } }
		public override int DefMissSound{ get{ return -1; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 50; } }

		public override SkillName DefSkill{ get{ return SkillName.Wrestling; } }
		public override WeaponType DefType{ get{ return WeaponType.Fists; } }
		public override WeaponAnimation DefAnimation{ get{ return WeaponAnimation.Wrestle; } }

		[Constructable]
		public GiftPugilistGloves() : base( 0x13C6 )
		{
			Name = "pugilist gloves";
			Weight = 2.0;
			Hue = Server.Misc.RandomThings.GetRandomColor(0);
			Layer = Layer.OneHanded;
			Attributes.SpellChanneling = 1;
			Resource = CraftResource.RegularLeather;
		}

		public override void AddNameProperties( ObjectPropertyList list )
		{
			base.AddNameProperties( list );
			list.Add( 1049644, "Cannot be used with hand-held weapons" );
		}

		public GiftPugilistGloves( Serial serial ) : base( serial )
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