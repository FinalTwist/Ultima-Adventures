using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x1439, 0x1438 )]
	public class ThorsHammer : BaseBashing
	{
		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.WhirlwindAttack; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.CrushingBlow; } }

		public override int AosStrengthReq{ get{ return 95; } }
		public override int AosMinDamage{ get{ return 17; } }
		public override int AosMaxDamage{ get{ return 18; } }
		public override int AosSpeed{ get{ return 28; } }
		public override float MlSpeed{ get{ return 3.75f; } }

		public override int OldStrengthReq{ get{ return 40; } }
		public override int OldMinDamage{ get{ return 8; } }
		public override int OldMaxDamage{ get{ return 36; } }
		public override int OldSpeed{ get{ return 31; } }
        
		public override int ArtifactRarity{ get{ return 35; } }
		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 110; } }

		public override WeaponAnimation DefAnimation{ get{ return WeaponAnimation.Bash2H; } }

		[Constructable]
		public ThorsHammer() : base( 0x1439 )
		{
			Weight = 10.0;
			Name = "Thors Hammer";
			 Hue = 2785;
            Attributes.WeaponDamage = 50;
            Attributes.BonusStr = 5;
            Attributes.LowerManaCost = 50;
            Attributes.Luck = 1000;
            Attributes.NightSight = 1;
            Attributes.SpellChanneling = 1;
            Attributes.SpellDamage = 100;
            Attributes.RegenHits = 20;
            WeaponAttributes.HitMagicArrow = 25;
            WeaponAttributes.HitLeechHits = 25;
            WeaponAttributes.SelfRepair = 5;
            WeaponAttributes.HitLightning = 75;
            WeaponAttributes.HitFireball = 55;
            
            AosElementDamages.Fire = 50;
            AosElementDamages.Physical = 50;
            AosElementDamages.Energy = 20;
            AosElementDamages.Cold = 20;
            AosElementDamages.Poison = 20;
            
			Layer = Layer.OneHanded;
		}
        public ThorsHammer( Serial serial ) : base( serial )
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
