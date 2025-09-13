using System;
using Server;
using Server.Spells.Magical;
using Server.Targeting;


namespace Server.Items
{
	public class StaffofSnakes : BaseMagicObject
	{
		public override int InitMinHits{ get{ return 80; } }
		public override int InitMaxHits{ get{ return 160; } }


		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.WhirlwindAttack; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.ParalyzingBlow; } }
		public override WeaponAbility ThirdAbility{ get{ return WeaponAbility.ToxicStrike; } }
		public override WeaponAbility FourthAbility{ get{ return WeaponAbility.MagicProtection; } }
		public override WeaponAbility FifthAbility{ get{ return WeaponAbility.InfectiousStrike; } }


		public override int AosStrengthReq{ get{ return 20; } }
		public override int AosMinDamage{ get{ return 15; } }
		public override int AosMaxDamage{ get{ return 17; } }
		public override int AosSpeed{ get{ return 33; } }
		public override float MlSpeed{ get{ return 3.25f; } }


		public override int OldStrengthReq{ get{ return 20; } }
		public override int OldMinDamage{ get{ return 10; } }
		public override int OldMaxDamage{ get{ return 30; } }
		public override int OldSpeed{ get{ return 33; } }


		[Constructable]
		public StaffofSnakes() : base( MagicObjectEffect.Charges, 20, 30 )
		{
			Hue = 0x304;
			Weight = 3.0;
			ItemID = 0x13F8;			Name = "Staff of the Serpent";
			AosElementDamages.Poison = 100;
			Attributes.SpellChanneling = 1;
			Slayer = SlayerName.SnakesBane;
			Layer = Layer.TwoHanded;
			WeaponAttributes.HitPoisonArea = 50;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
			list.Add( 1049644, "Summons Mystical Serpents" );
		}


		public StaffofSnakes( Serial serial ) : base( serial )
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
			Cast( new SummonSnakesSpell( from, this ) );
		}
	}
}