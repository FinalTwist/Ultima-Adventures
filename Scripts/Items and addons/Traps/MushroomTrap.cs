using System;
using Server;
using Server.Network;
using Server.Items;
using System.Collections.Generic;
using Server.Misc;
using System.Collections;
using Server.Mobiles;
using System.Text;
using System.IO;
using Server.Regions;

namespace Server.Items
{
	public class MushroomTrap : BaseTrap
	{
		public int ShroomType;
		
		[CommandProperty(AccessLevel.Owner)]
		public int Shroom_Type { get { return ShroomType; } set { ShroomType = value; InvalidateProperties(); } }

		[Constructable]
		public MushroomTrap() : base( 0x1A81 )
		{
			ShroomType = Utility.RandomMinMax( 1, 3 );
			Light = LightType.Circle150;
			Name = "mushroom";
			switch( Utility.RandomMinMax( 1, 6 ) )
			{
				case 1: Name = "strange mushroom"; break;
				case 2: Name = "weird mushroom"; break;
				case 3: Name = "odd mushroom"; break;
				case 4: Name = "curious mushroom"; break;
				case 5: Name = "peculiar mushroom"; break;
				case 6: Name = "bizarre mushroom"; break;
			}
			Hue = Utility.RandomList( 0x47E, 0x48B, 0x495, 0xB95, 0x5B6, 0x5B7, 0x55F, 0x55C, 0x556, 0x54F, 0x489 );
		}

		public override bool PassivelyTriggered{ get{ return true; } }
		public override TimeSpan PassiveTriggerDelay{ get{ return TimeSpan.Zero; } }
		public override int PassiveTriggerRange{ get{ return 2; } }
		public override TimeSpan ResetDelay{ get{ return TimeSpan.Zero; } }

		public override void OnTrigger( Mobile from )
		{
			if ( !from.Alive || !from.Player || ItemID != 0x1A81 || from.AccessLevel > AccessLevel.Player )
				return;

			if ( Server.Misc.SeeIfGemInBag.GemInPocket( from ) == true || Server.Misc.SeeIfJewelInBag.JewelInPocket( from ) == true )
				return;

			if ( HiddenTrap.CheckTrapAvoidance( from, this ) == 0 )
				return;

			if ( !from.Player )
				return;

			ItemID = 0x1126;
			Effects.PlaySound( Location, Map, 0x306 );

			int itHurts = 0;

			switch( ShroomType )
			{
				case 1:

						itHurts = (int)( (Utility.RandomMinMax(40,200) * ( 100 - from.PhysicalResistance ) ) / 100 );
						Spells.SpellHelper.Damage( TimeSpan.FromSeconds( 0.5 ), from, from, itHurts ); // WIZARD
						from.FixedParticles( 0x36BD, 20, 10, 5044, EffectLayer.Head );
						from.PlaySound( 0x307 );
						from.LocalOverheadMessage(MessageType.Emote, 0xB1F, true, "A mushroom exploded near you!");

					break;

				case 2:
						int itSicks = 5;

						if ( from.PoisonResistance >= 70 ){ itSicks = 1; }
						else if ( from.PoisonResistance >= 50 ){ itSicks = 2; }
						else if ( from.PoisonResistance >= 30 ){ itSicks = 3; }
						else if ( from.PoisonResistance >= 10 ){ itSicks = 4; }

						switch( Utility.RandomMinMax( 1, itSicks ) )
						{
							case 1: from.ApplyPoison( from, Poison.Lesser );	break;
							case 2: from.ApplyPoison( from, Poison.Regular );	break;
							case 3: from.ApplyPoison( from, Poison.Greater );	break;
							case 4: from.ApplyPoison( from, Poison.Deadly );	break;
							case 5: from.ApplyPoison( from, Poison.Lethal );	break;
						}

						Effects.SendLocationEffect( this.Location, this.Map, 0x11A8 - 2, 16, 3, 0, 0 );
						Effects.PlaySound( this.Location, this.Map, 0x231 );
						from.LocalOverheadMessage(MessageType.Emote, 0xB1F, true, "A mushroom released odd spores!");

					break;

				case 3:

						from.BoltEffect( 0 );
						itHurts = (int)( (Utility.RandomMinMax(40,200) * ( 100 - from.EnergyResistance ) ) / 100 );
						from.Damage( itHurts, from );
						from.LocalOverheadMessage(MessageType.Emote, 0xB1F, true, "A mushroom released strange energy!");

					break;
			}

			Timer.DelayCall( TimeSpan.FromSeconds( 2.0 ), new TimerCallback( OnMushroomReset ) );

			LoggingFunctions.LogTraps( from, "a strange mushroom" );
		}

		public virtual void OnMushroomReset()
		{
			if ( Region.Find( Location, Map ).IsPartOf( typeof( DungeonRegion ) ) )
				ItemID = 0x1A81; // reset
			else
				Delete();
		}

		public MushroomTrap( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
            writer.Write( ShroomType );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( ItemID == 0x1126 )
				OnMushroomReset();

            ShroomType = reader.ReadInt();
		}
	}
}