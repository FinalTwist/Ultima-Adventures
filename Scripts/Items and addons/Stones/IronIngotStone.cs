using System;
using Server.Items;
using Server.Mobiles;
using Server.Misc;


namespace Server.Items
{
	public class IronStone : Item
	{
		public override string DefaultName
		{
			get { return "an IronIngot stone"; }
		}

		[Constructable]
		public IronStone() : base( 0xED4 )
		{
			Movable = false;
			Hue = 0x0;
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
				from.SendMessage(0,"10,000 gold has been deduced from your bank, or your pack");
				IronIngot ingotBag = new IronIngot( 1000 );

				if ( !from.AddToBackpack( ingotBag ) )
					ingotBag.Delete();
			}
			else
				from.SendMessage(0,"you need 10,000 gold in your pack or 10,000 gold in your bank (no cheques accepted)");
		}

		public IronStone( Serial serial ) : base( serial )
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
