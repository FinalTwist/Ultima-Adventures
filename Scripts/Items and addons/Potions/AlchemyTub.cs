using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class AlchemyTub : Item
	{
		[Constructable]
		public AlchemyTub() : base( 0x126A )
		{
			Name = "alchemy tub";
			Weight = 50.0;
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Place In Your Home");
            list.Add( 1049644, "Cleans Jars And Bottles");
        } 

		public AlchemyTub( Serial serial ) : base( serial )
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

		public override bool OnDragDrop( Mobile from, Item item )
		{
			if ( this.Movable != false )
			{
				from.SendMessage( "This must be set in your home to use!" );
				return false;
			}
			else if ( item is Bottle || item is Jar || ( item is CrystallineJar && item.Name == "crystalline jar" ) )
			{
				from.SendMessage( "That is already clean!" );
				return false;
			}
			else
			{
				int jar = 0;
				int bottle = 0;
				int crystal = 0;

				if ( item is BaseMixture ){ jar = 1; }
				else if ( item is BasePotion ){ bottle = 1; }
				else if ( item is AutoResPotion ){ bottle = 1; }
				else if ( item is ShieldOfEarthPotion ){ jar = 1; }
				else if ( item is WoodlandProtectionPotion ){ jar = 1; }
				else if ( item is ProtectiveFairyPotion ){ jar = 1; }
				else if ( item is HerbalHealingPotion ){ jar = 1; }
				else if ( item is GraspingRootsPotion ){ jar = 1; }
				else if ( item is BlendWithForestPotion ){ jar = 1; }
				else if ( item is SwarmOfInsectsPotion ){ jar = 1; }
				else if ( item is VolcanicEruptionPotion ){ jar = 1; }
				else if ( item is TreefellowPotion ){ jar = 1; }
				else if ( item is StoneCirclePotion ){ jar = 1; }
				else if ( item is DruidicRunePotion ){ jar = 1; }
				else if ( item is LureStonePotion ){ jar = 1; }
				else if ( item is NaturesPassagePotion ){ jar = 1; }
				else if ( item is MushroomGatewayPotion ){ jar = 1; }
				else if ( item is RestorativeSoilPotion ){ jar = 1; }
				else if ( item is FireflyPotion ){ jar = 1; }
				else if ( item is HellsGateScroll ){ jar = 1; }
				else if ( item is ManaLeechScroll ){ jar = 1; }
				else if ( item is NecroCurePoisonScroll ){ jar = 1; }
				else if ( item is NecroPoisonScroll ){ jar = 1; }
				else if ( item is NecroUnlockScroll ){ jar = 1; }
				else if ( item is PhantasmScroll ){ jar = 1; }
				else if ( item is RetchedAirScroll ){ jar = 1; }
				else if ( item is SpectreShadowScroll ){ jar = 1; }
				else if ( item is UndeadEyesScroll ){ jar = 1; }
				else if ( item is VampireGiftScroll ){ jar = 1; }
				else if ( item is WallOfSpikesScroll ){ jar = 1; }
				else if ( item is BloodPactScroll ){ jar = 1; }
				else if ( item is GhostlyImagesScroll ){ jar = 1; }
				else if ( item is GhostPhaseScroll ){ jar = 1; }
				else if ( item is GraveyardGatewayScroll ){ jar = 1; }
				else if ( item is HellsBrandScroll ){ jar = 1; }
				else if ( item is MagicalDyes ){ bottle = 1; }
				else if ( item is BottleOfAcid ){ bottle = 1; }
				else if ( item is CrystallineJar ){ crystal = 1; }
				else if ( item is NecroSkinPotion ){ jar = 1; }
				else if ( item is UnusualDyes ){ jar = 1; }
				else if ( item is OilWood ){ bottle = 1; }
				else if ( item is OilAmethyst ){ bottle = 1; }
				else if ( item is OilCaddellite ){ bottle = 1; }
				else if ( item is OilEmerald ){ bottle = 1; }
				else if ( item is OilGarnet ){ bottle = 1; }
				else if ( item is OilIce ){ bottle = 1; }
				else if ( item is OilJade ){ bottle = 1; }
				else if ( item is OilLeather ){ bottle = 1; }
				else if ( item is OilMarble ){ bottle = 1; }
				else if ( item is OilMetal ){ bottle = 1; }
				else if ( item is OilOnyx ){ bottle = 1; }
				else if ( item is OilQuartz ){ bottle = 1; }
				else if ( item is OilRuby ){ bottle = 1; }
				else if ( item is OilSapphire ){ bottle = 1; }
				else if ( item is OilSilver ){ bottle = 1; }
				else if ( item is OilSpinel ){ bottle = 1; }
				else if ( item is OilStarRuby ){ bottle = 1; }
				else if ( item is OilTopaz ){ bottle = 1; }
				else if ( item is OilWood ){ bottle = 1; }
				else if ( item is UnknownLiquid ){ bottle = 1; }
				else if ( item is BottleOfParts ){ jar = 1; }
				else if ( item is BeverageBottle ){ bottle = 1; }

				if ( jar > 0 || bottle > 0 || crystal > 0 )
				{
					string cleaned = "bottle";
					string plural = "";
					int give = 1;
						if ( item.Amount > 1 ){ give = item.Amount; plural = "s"; }

					if ( jar > 0 ){ cleaned = "jar"; from.AddToBackpack( new Jar(give) ); }
					else if ( crystal > 0 ){ cleaned = "crystalline flask"; from.AddToBackpack( new CrystallineJar() ); }
					else { cleaned = "bottle"; from.AddToBackpack( new Bottle(give) ); }

					cleaned = cleaned + plural;

					from.SendMessage( "You thoroughly wash the " + cleaned + "." );
					from.PlaySound( 0x026 );

					this.Hue = Server.Misc.RandomThings.GetRandomColor(0);
					item.Delete();
					return true;
				}
				else
				{
					from.SendMessage( "This is for washing alchemical and herbalist containers." );
					return false;
				}
			}
		}
	}
}