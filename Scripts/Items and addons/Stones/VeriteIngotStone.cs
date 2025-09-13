using System;
using Server.Items;
using Server.Mobiles;

namespace Server.Items
{
	public class VeriteStone : Item
	{
		public override string DefaultName
		{
			get { return "an VeriteIngot stone"; }
		}

		[Constructable]
		public VeriteStone() : base( 0xED4 )
		{
			Movable = false;
			Hue = 0x89F;
		}

		public override void OnDoubleClick( Mobile from )
		{
			Container pack = from.Backpack;
			int i_Bank;
			i_Bank = Banker.GetBalance( from );
			Container bank = from.FindBankNoCreate();
			//if ( pack != null && pack.ConsumeTotal( typeof( Gold ), 10000 ) )
			if ( ( pack != null && pack.ConsumeTotal( typeof( Gold ), 10000 ) ) || ( bank != null && bank.ConsumeTotal( typeof( Gold ), 10000 ) ) )
			{
				VeriteIngot ingotBag = new VeriteIngot( 125 );

				if ( !from.AddToBackpack( ingotBag ) )
					ingotBag.Delete();
			}
			else
				from.SendMessage(0,"you need 10,000 gold in your pack or 10,000 gold in your bank (no cheques accepted)");
		}


		public VeriteStone( Serial serial ) : base( serial )
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
