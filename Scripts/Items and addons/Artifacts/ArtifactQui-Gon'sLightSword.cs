// THEMIGHTYMIBHUNTER PRESENTS A TEMPLATE ..BE IT FIRST VERSION... FOR A JEDI ARTIFACT 1 HANDED SABER... MORE TO COME.. ALSO WIP, DUEL WEILDED sABERS


using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	public class QuiGonsLightSword : BaseSword
	{
		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.ArmorIgnore; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.DoubleStrike; } }
		public override WeaponAbility ThirdAbility{ get{ return WeaponAbility.DefenseMastery; } }
		public override WeaponAbility FourthAbility{ get{ return WeaponAbility.ElementalStrike; } }
		public override WeaponAbility FifthAbility{ get{ return WeaponAbility.DeathBlow; } }

		public override int AosStrengthReq{ get{ return 35; } }
		public override int AosMinDamage{ get{ return 20; } }
		public override int AosMaxDamage{ get{ return 24; } }
		public override int AosSpeed{ get{ return 30; } }
		public override float MlSpeed{ get{ return 3.00f; } }

		public override int DefHitSound{ get{ return 0x53D; } }
		public override int DefMissSound{ get{ return 0x53E; } }

		public override int InitMinHits{ get{ return 50; } }
		public override int InitMaxHits{ get{ return 110; } }

		public override WeaponAnimation DefAnimation{ get{ return WeaponAnimation.Slash1H; } }

		[Constructable]
		public QuiGonsLightSword() : base( 0x3FBA )
		{
			switch( Utility.RandomMinMax( 0, 4 ) )
			{
				case 0: Hue = 0x48D; AosElementDamages.Physical = 50; AosElementDamages.Cold = 50;   break; // ICE
				case 1: Hue = 0x48E; AosElementDamages.Physical = 50; AosElementDamages.Fire = 50;   break; // FIRE
				case 2: Hue = 0x48F; AosElementDamages.Physical = 50; AosElementDamages.Poison = 50; break; // POISON
				case 3: Hue = 0x490; AosElementDamages.Physical = 50; AosElementDamages.Energy = 50; break; // ENERGY
				case 4: Hue = 0x491; AosElementDamages.Physical = 100; break; // BLUNT
			}
		      
			Hue = 0x52F;
			
			SkillBonuses.SetValues( 0, SkillName.Fencing, 25 );
			AccuracyLevel = WeaponAccuracyLevel.Supremely;
			DamageLevel = WeaponDamageLevel.Vanq;
			Attributes.AttackChance = 10;
			Name = "Qui-Gon Jinn's Light Saber";
			Weight = 5.0;
			Layer = Layer.OneHanded;
            Attributes.NightSight = 1;}
			
			
        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }



		public override bool OnEquip( Mobile from )
		{
			from.PlaySound( 0x53F );

			base.OnEquip( from );

			return true;
		}
		
  
		
		public QuiGonsLightSword( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
            writer.Write( Hue );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
            Hue = reader.ReadInt();

			if ( Hue == 0x48D ){ AosElementDamages.Physical = 50; AosElementDamages.Cold = 50; }
			else if ( Hue == 0x48E ){ AosElementDamages.Physical = 50; AosElementDamages.Fire = 50; }
			else if ( Hue == 0x48F ){ AosElementDamages.Physical = 50; AosElementDamages.Poison = 50; }
			else if ( Hue == 0x490 ){ AosElementDamages.Physical = 50; AosElementDamages.Energy = 50; }
			else { AosElementDamages.Physical = 100; }
		}
	}
}