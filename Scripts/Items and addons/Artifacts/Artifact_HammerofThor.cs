using System;
using Server;
using Server.Spells.Magical;
using Server.Targeting;


namespace Server.Items
{
	public class HammerofThor : BaseMagicObject
	{
		public override int InitMinHits{ get{ return 80; } }
		public override int InitMaxHits{ get{ return 160; } }


		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.WhirlwindAttack; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.CrushingBlow; } }
		public override WeaponAbility ThirdAbility{ get{ return WeaponAbility.LightningStriker; } }
		public override WeaponAbility FourthAbility{ get{ return WeaponAbility.DoubleWhirlwindAttack; } }
		public override WeaponAbility FifthAbility{ get{ return WeaponAbility.DevastatingBlow; } }


		public override int AosStrengthReq{ get{ return 95; } }
		public override int AosMinDamage{ get{ return 17; } }
		public override int AosMaxDamage{ get{ return 18; } }
		public override int AosSpeed{ get{ return 28; } }
		public override float MlSpeed{ get{ return 3.75f; } }


		public override int OldStrengthReq{ get{ return 40; } }
		public override int OldMinDamage{ get{ return 8; } }
		public override int OldMaxDamage{ get{ return 36; } }
		public override int OldSpeed{ get{ return 31; } }


		[Constructable]
		public HammerofThor() : base( MagicObjectEffect.Charges, 50, 50 )
		{
			Hue = 0x430;
			Weight = 10.0;
			ItemID = 0xFB5;			Name = "Hammer of Thor";
			AosElementDamages.Energy = 50;
			AosElementDamages.Physical = 50;
			WeaponAttributes.HitLightning = 50;
			DamageLevel = WeaponDamageLevel.Vanq;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
			list.Add( 1049644, "Can Summon The Power of Lightning" );
		}


		public HammerofThor( Serial serial ) : base( serial )
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


		public override void OnMagicObjectUse( Mobile from )
		{
			Cast( new ThorLightningSpell( from, this ) );
		}
	}
}