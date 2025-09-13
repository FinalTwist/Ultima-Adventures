using System;
using Server.Network;
using Server.Items;
using Server.Engines.Harvest;


namespace Server.Items
{
	public class GrimReapersScythe : Scythe
	{
		public override int InitMinHits{ get{ return 80; } }
		public override int InitMaxHits{ get{ return 160; } }


		[Constructable]
		public GrimReapersScythe()
		{
			Hue = 0x47E;
			Name = "Grim Reaper's Scythe";
			WeaponAttributes.LowerStatReq = 50;
			WeaponAttributes.HitLeechHits = 20;
			WeaponAttributes.HitDispel = 25;
			WeaponAttributes.UseBestSkill = 1;
			AccuracyLevel = WeaponAccuracyLevel.Supremely;
			DamageLevel = WeaponDamageLevel.Vanq;
            Slayer = SlayerName.Repond;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public GrimReapersScythe( Serial serial ) : base( serial )
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