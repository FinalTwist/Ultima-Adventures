using System;
using Server;

namespace Server.Items
{
	public class SoulPack : BaseContainer
	{

		[Constructable]
		public SoulPack() : base( 0xE75 )
		{
			Layer = Layer.Backpack;
			Weight = 3.0;
			object[] list = new object[]
				{
					new MageBlood(1), new BalronBlood(1), new LichBlood(1), new ElderGazerBrain(1), new IceBlood(1), new FireBlood(1), new ElementalCore(1), new BearBone(1), 
					new DimensionalShard(1), new CelestialRune(1), new AncientMoss(1), new EagleEye(1), new HorseHeart(1), new MonkTear(1), new PixieDust(1), new PlanarRune(1), 
					new SageBlood(1), new ScorpionBlood(1), new SnakeOil(1), new TitanFlesh(1), new UnicornShoes(1)
				};

			DropItem((Item)list[Utility.Random( list.Length )]);
			if (Utility.RandomMinMax(1,3000) == 3) {
				Phylactery phylactery = new Phylactery();
				phylactery.PowerLevel = 1;
				phylactery.AddRandomEssences(Utility.RandomMinMax(1,6));
				DropItem((Item)phylactery);
			}
		}

		public SoulPack( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}