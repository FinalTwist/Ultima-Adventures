using System;
using Server;

namespace Server.Items
{
	public class Mangler : Broadsword
	{
		public override int InitMinHits{ get{ return Utility.RandomMinMax(100, 125); } }
		public override int InitMaxHits{ get{ return Utility.RandomMinMax(126, 150); } }
		
		[Constructable]
		public Mangler() : base( )
		{
			Hue = 2001;
			
			Name = ("Mangler");
		
			WeaponAttributes.HitLeechMana = 50;
			Attributes.WeaponDamage = 50;
			Attributes.WeaponSpeed = 25;
			WeaponAttributes.HitHarm = 50;
			WeaponAttributes.UseBestSkill = 1;			
			WeaponAttributes.HitLowerDefend = 30;		

		}
		
		public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }

		public Mangler( Serial serial ) : base(serial)
		{
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)0 ); //version
		}
	}
}