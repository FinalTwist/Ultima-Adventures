using System;
using Server;

namespace Server.Items
{
	public class EternalGuardianStaff : GnarledStaff
	{

		public override int InitMinHits{ get{ return Utility.RandomMinMax(100, 125); } }
		public override int InitMaxHits{ get{ return Utility.RandomMinMax(126, 150); } }

		[Constructable]
		public EternalGuardianStaff()
		{
			Name = ("Eternal Guardian Staff");
		
			Hue = 95;
			
			SkillBonuses.SetValues( 0, SkillName.Mysticism, 15.0 );		
			Attributes.SpellDamage = 10;
			Attributes.LowerManaCost = 5;	
			Attributes.SpellChanneling = 1;	

		}
		
		public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }



		public EternalGuardianStaff( Serial serial ) : base( serial )
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