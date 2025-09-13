using System;
using Server;

namespace Server.Items
{
	public class TorchOfTrapFinding : MagicTorch
	{
		[Constructable]
		public TorchOfTrapFinding()
		{
			Hue = 0;
			Name = "Torch of Trap Burning";
			SkillBonuses.SetValues(0, SkillName.RemoveTrap, 100);
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        } 

		public TorchOfTrapFinding( Serial serial ) : base( serial )
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