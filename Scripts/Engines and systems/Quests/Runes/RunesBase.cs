using System;
using Server;
using System.Collections;
using System.Collections.Generic;
using Server.Items;
using Server.Misc;
using Server.Network;
using Server.Accounting;
using Server.Mobiles;
using Server.Network;

namespace Server.Items
{
	public class RunesBase : BaseAddon
	{
		[ Constructable ]
		public RunesBase()
		{
			Light = LightType.Circle150;
			int iThing = Utility.RandomList( 0x53C5, 0x53C6 );
			string sThing = "Breath of Air";
			int iColor = 0;
			int z = 2;

			AddComplexComponent( (BaseAddon) this, iThing, 0, 0, 7, 0, -1, "Chest of Virtue", 1);
			AddComplexComponent( (BaseAddon) this, 5703, 0, 0, 5, 0, 29, "mystic glow", 1);
			AddComplexComponent( (BaseAddon) this, 0x32F2, 0, 0, 0, 0, -1, "", 1);
		}

		public RunesBase( Serial serial ) : base( serial )
		{
		}

        private static void AddComplexComponent(BaseAddon addon, int item, int xoffset, int yoffset, int zoffset, int hue, int lightsource)
        {
            AddComplexComponent(addon, item, xoffset, yoffset, zoffset, hue, lightsource, null, 1);
        }

        private static void AddComplexComponent(BaseAddon addon, int item, int xoffset, int yoffset, int zoffset, int hue, int lightsource, string name, int amount)
        {
            AddonComponent ac;
            ac = new AddonComponent(item);
            if (name != null && name.Length > 0)
                ac.Name = name;
            if (hue != 0)
                ac.Hue = hue;
            if (amount > 1)
            {
                ac.Stackable = true;
                ac.Amount = amount;
            }
            if (lightsource != -1)
                ac.Light = (LightType) lightsource;
            addon.AddComponent(ac, xoffset, yoffset, zoffset);
        }

		public override void OnComponentUsed( AddonComponent ac, Mobile from )
		{
			if ( !from.InRange( GetWorldLocation(), 2 ) )
			{
				from.SendMessage( "You will have to get closer to open that." );
			}
			else if ( from is PlayerMobile )
			{
				PlayerMobile pm = (PlayerMobile)from;

				bool HasBox = false;
				int goal = 0;
				int nextVirtue = 0;

				RuneBox box = null;
				if ( from.Backpack.FindItemByType( typeof ( RuneBox ) ) != null )
				{
					Item boxx = from.Backpack.FindItemByType( typeof ( RuneBox ) );
					RuneBox boxxx = (RuneBox)boxx;

					if ( boxxx.RuneBoxOwner == from )
					{
						HasBox = true;
						box = boxxx;
						goal = box.HasCompassion + box.HasHonesty + box.HasHonor + box.HasHumility + box.HasJustice + box.HasSacrifice + box.HasSpirituality + box.HasValor;
						if ( box.HasCompassion == 0 ){ nextVirtue = 1; }
						else if ( box.HasHonesty == 0 ){ nextVirtue = 2; }
            			else if ( box.HasHonor == 0 ){ nextVirtue = 3; }
            			else if ( box.HasHumility == 0 ){ nextVirtue = 4; }
            			else if ( box.HasJustice == 0 ){ nextVirtue = 5; }
            			else if ( box.HasSacrifice == 0 ){ nextVirtue = 6; }
            			else if ( box.HasSpirituality == 0 ){ nextVirtue = 7; }
            			else if ( box.HasValor == 0 ){ nextVirtue = 8; }
					}
				}

				if ( CharacterDatabase.GetKeys( from, "Virtue" ) || CharacterDatabase.GetKeys( from, "Corrupt" ) ) // THEY ARE ALREADY DID THIS QUEST
				{
					HasBox = true;
					from.SendMessage( "You don't need this chest as you already dealt with the runes." );
				}
				else if ( goal < 8 && HasBox == true )
				{
					RuneGuardian sentinel = new RuneGuardian();
					sentinel.gVirtue = nextVirtue;
					sentinel.gSummoner = from;
					sentinel.MoveToWorld( Location, Map );
					sentinel.Combatant = from;
					Effects.SendLocationParticles( EffectItem.Create( sentinel.Location, sentinel.Map, EffectItem.DefaultDuration ), 0x3728, 10, 10, 2023 );
					sentinel.PlaySound( 0x1FE );

					RunesBaseEmpty Pedul = new RunesBaseEmpty();
					Pedul.MoveToWorld (new Point3D(this.X, this.Y, this.Z), this.Map);
					from.SendMessage( "You have awakened a sentinel." );
					this.Delete();
				}
				else if ( HasBox == true )
				{
					from.SendMessage( "Your virtue chest is already in your pack." );
				}
				else
				{
					ArrayList targets = new ArrayList();
					foreach ( Item item in World.Items.Values )
					{
						if ( item is RuneBox )
						{
							if ( ((RuneBox)item).RuneBoxOwner == from )
							{
								targets.Add( item );
								HasBox = true;
							}
						}
					}
					for ( int i = 0; i < targets.Count; ++i )
					{
						Item item = ( Item )targets[ i ];

						if ( item is RuneBox )
						{
							from.AddToBackpack( item );
							from.SendMessage( "Your virtue chest is already in your pack." );
						}
					}
				}

				if ( !HasBox )
				{
					SetupVitrtue( from );
					from.SendMessage( "You take possession of the Chest of Virtue!" );
					from.SendSound( 0x3D );
					LoggingFunctions.LogGeneric( from, "has found the Chest of Virtue." );
					CharacterDatabase.SetKeys( from, "Runes", true );
					RunesBaseEmpty Pedul = new RunesBaseEmpty();
					Pedul.MoveToWorld (new Point3D(this.X, this.Y, this.Z), this.Map);
					this.Delete();
				}
			}
		}

		public void SetupVitrtue( Mobile from )
		{
			RuneBox box = new RuneBox();

			box.RuneBoxOwner = from;

			box.HasCompassion = 0;
			box.HasHonesty = 0;
			box.HasHonor = 0;
			box.HasHumility = 0;
			box.HasJustice = 0;
			box.HasSacrifice = 0;
			box.HasSpirituality = 0;
			box.HasValor = 0;

			from.AddToBackpack( box );
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( 0 ); // Version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}