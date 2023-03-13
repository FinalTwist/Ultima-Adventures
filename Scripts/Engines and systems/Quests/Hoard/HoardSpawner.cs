using System;
using Server;
using Server.Network;
using Server.Mobiles;
using Server.Items;
using Server.Spells;
using System.Collections.Generic;
using Server.Misc;
using System.Collections;
using System.Text;
using System.IO;
using Server.Regions;
using Server.Targeting;

namespace Server.Items
{
	public class HoardTile : Item
	{
		[Constructable]
		public HoardTile() : base(0x1BC3)
		{
			Movable = false;
			Visible = false;
			Name = "hoard tile";
		}

		public HoardTile(Serial serial) : base(serial)
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

namespace Server.Misc
{
    class HoardPile
    {
		public static void MakeHoard( Mobile from )
		{
			bool Lucky = false;

			Mobile killer = from.LastKiller;
			if ( killer != null )
			{
				if ( killer is BaseCreature )
					killer = ((BaseCreature)killer).GetMaster();

				if ( killer is PlayerMobile )
				{
					Lucky = GetPlayerInfo.LuckyKiller( killer.Luck, killer );
				}
			}

			if ( from.Title != null && from.Title != "" && from.Fame >= 15000 && ( Utility.RandomMinMax( 1, 5 ) == 1 || Lucky ) )
			{
				ArrayList targets = new ArrayList();
				bool morePowerfulCreature = false;

				foreach ( Mobile creature in from.GetMobilesInRange( 15 ) )
				{
					if ( creature is BaseCreature && creature != from && ((BaseCreature)creature).ControlMaster == null && creature.Fame >= from.Fame )
					{
						morePowerfulCreature = true;
					}
				}

				if ( !morePowerfulCreature )
				{
					foreach ( Item spawner in from.GetItemsInRange( 15 ) )
					{
						if ( spawner is HoardTile )
						{
							targets.Add( spawner );
						}
					}

					HoardPiles MyHoard = null;
					Point3D loc = from.Location;
					Map map = from.Map;
					Item spawn = null;

					for ( int i = 0; i < targets.Count; ++i )
					{
						bool buildTreasure = true;

						foreach ( Item loot in from.GetItemsInRange( 1 ) )
						{
							if ( loot is HoardPiles )
							{
								buildTreasure = false;
							}
						}

						if ( buildTreasure )
						{
							MyHoard = new HoardPiles();
								if ( Worlds.IsOnSpaceship( from.Location, from.Map ) ){ MyHoard.ItemID = Utility.RandomList( 0x096D, 0x096E ); }
							spawn = ( Item )targets[ i ];
							loc = spawn.Location;
							map = spawn.Map;
							MyHoard.HoardName = from.Name + " " + from.Title;
							MyHoard.MoveToWorld( loc, map );
							Effects.SendLocationParticles( EffectItem.Create( MyHoard.Location, MyHoard.Map, EffectItem.DefaultDuration ), 0x3709, 10, 30, 5052 );
							Effects.PlaySound( MyHoard, MyHoard.Map, 0x208 );
						}
					}
				}
			}
		}
	}
}