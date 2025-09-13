using System;
using Server;


namespace Server.Items
{
	public class BowofthePhoenix : ElvenCompositeLongbow
	{
		public override int InitMinHits{ get{ return 80; } }
		public override int InitMaxHits{ get{ return 160; } }


		[Constructable]
		public BowofthePhoenix()
		{
			Hue = 0x489;
			Name = "Bow of the Phoenix";
			SkillBonuses.SetValues( 0, SkillName.Archery, 5 );
			AccuracyLevel = WeaponAccuracyLevel.Supremely;
			DamageLevel = WeaponDamageLevel.Vanq;
			AosElementDamages.Fire = 100; 
			WeaponAttributes.HitFireball = 100;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public BowofthePhoenix( Serial serial ) : base( serial )
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