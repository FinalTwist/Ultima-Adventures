using System;
using Server;
using Server.Items;
using Server.Network;
using Server.Commands;

namespace Server.Gumps
{
    public class AddTrapGump : Gump
    {
		public static void Initialize()
		{
			CommandSystem.Register( "AddTrap", AccessLevel.GameMaster, new CommandEventHandler( AddTrap_OnCommand ) );
		}

		[Usage( "AddTrap" )]
		[Description( "Displays a menu from which you can interactively add traps." )]
		public static void AddTrap_OnCommand( CommandEventArgs e )
		{
			e.Mobile.SendGump( new AddTrapGump() );
		}

        public AddTrapGump() : base( 40, 40 )
        {
            this.Closable=true;
			this.Disposable=false;
			this.Dragable=true;
			this.Resizable=false;

			AddPage(0);

			AddImage(60, 71, 2624);
			AddImage(443, 279, 2625);
			AddImage(443, 492, 2628);
			AddImage(443, 70, 2625);
			AddImage(42, 72, 2623);
			AddImage(62, 54, 2621);
			AddImage(42, 54, 2620);
			AddImage(42, 282, 2623);
			AddImage(176, 53, 2621);
			AddImage(443, 53, 2622);
			AddImage(42, 492, 2626);
			AddImage(62, 492, 2627);
			AddImage(180, 492, 2627);
			AddImage(60, 288, 2624);
			AddImage(183, 68, 2624);
			AddImage(61, 190, 2624);
			AddImage(187, 289, 2624);
			AddImage(186, 184, 2624);
			AddImage(443, 289, 2625);

			AddItem(226, 83, 4366);
			AddItem(390, 95, 4359);
			AddItem(236, 189, 4385);
			AddItem(371, 198, 4378);
			AddItem(371, 449, 4348);
			AddItem(238, 453, 4367);
			AddItem(74, 442, 4389);
			AddItem(230, 293, 4511);
			AddItem(233, 379, 4517);
			AddItem(374, 358, 4528);
			AddItem(354, 266, 4533);
			AddItem(74, 393, 4523);
			AddItem(75, 239, 4549);
			AddItem(75, 309, 7582);
			AddItem(73, 243, 6582);
			AddItem(61, 73, 14108);

			AddButton(132, 124, 2151, 2151, 1, GumpButtonType.Reply, 0);	// FIRE COLUMN
			AddButton(132, 246, 2151, 2151, 2, GumpButtonType.Reply, 0);	// FLAME SPURT
			AddButton(132, 324, 2151, 2151, 3, GumpButtonType.Reply, 0);	// GIANT SPIKE
			AddButton(132, 400, 2151, 2151, 4, GumpButtonType.Reply, 0);	// GAS
			AddButton(132, 456, 2151, 2151, 5, GumpButtonType.Reply, 0);	// MUSHROOM

			AddButton(280, 95, 2151, 2151, 6, GumpButtonType.Reply, 0);		// WALL SPIKE WEST WALL
			AddButton(280, 202, 2151, 2151, 7, GumpButtonType.Reply, 0);	// WALL SPIKE NORTH WALL
			AddButton(280, 294, 2151, 2151, 8, GumpButtonType.Reply, 0);	// WALL SPIKE WEST FLOOR
			AddButton(280, 386, 2151, 2151, 9, GumpButtonType.Reply, 0);	// WALL SPIKE NORTH FLOOR
			AddButton(280, 450, 2151, 2151, 10, GumpButtonType.Reply, 0);	// STONE FACE WEST

			AddButton(354, 95, 2151, 2151, 11, GumpButtonType.Reply, 0);	// WALL SAW NORTH
			AddButton(354, 202, 2151, 2151, 12, GumpButtonType.Reply, 0);	// WALL SAW WEST
			AddButton(354, 294, 2151, 2151, 13, GumpButtonType.Reply, 0);	// FLOOR SAW WEST
			AddButton(354, 386, 2151, 2151, 14, GumpButtonType.Reply, 0);	// FLOOR SAW NORTH
			AddButton(354, 450, 2151, 2151, 15, GumpButtonType.Reply, 0);	// STONE FACE NORTH
        }

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;

            switch(info.ButtonID)
            {
                case 1:
				{
					FireColumnTrap sTrap = new FireColumnTrap();
					sTrap.MoveToWorld (new Point3D(from.X, from.Y, from.Z), from.Map);
					from.SendGump(new AddTrapGump());
					break;
				}
                case 2:
				{
					FlameSpurtTrap sTrap = new FlameSpurtTrap();
					sTrap.MoveToWorld (new Point3D(from.X, from.Y, from.Z), from.Map);
					from.SendGump(new AddTrapGump());
					break;
				}
                case 3:
				{
					GiantSpikeTrap sTrap = new GiantSpikeTrap();
					sTrap.MoveToWorld (new Point3D(from.X, from.Y, from.Z), from.Map);
					from.SendGump(new AddTrapGump());
					break;
				}
                case 4:
				{
					GasTrap sTrap = new GasTrap( GasTrapType.Floor );
					sTrap.MoveToWorld (new Point3D(from.X, from.Y, (from.Z+3)), from.Map);
					from.SendGump(new AddTrapGump());
					break;
				}
                case 5:
				{
					MushroomTrap sTrap = new MushroomTrap();
					sTrap.MoveToWorld (new Point3D(from.X, from.Y, from.Z), from.Map);
					from.SendGump(new AddTrapGump());
					break;
				}
                case 6:
				{
					SpikeTrap sTrap = new SpikeTrap( SpikeTrapType.WestWall );
					sTrap.ItemID = 4360;
					sTrap.MoveToWorld (new Point3D(from.X, from.Y, (from.Z+5)), from.Map);
					from.SendGump(new AddTrapGump());
					break;
				}
                case 7:
				{
					SpikeTrap sTrap = new SpikeTrap( SpikeTrapType.NorthWall );
					sTrap.ItemID = 4379;
					sTrap.MoveToWorld (new Point3D(from.X, from.Y, (from.Z+5)), from.Map);
					from.SendGump(new AddTrapGump());
					break;
				}
                case 8:
				{
					SpikeTrap sTrap = new SpikeTrap( SpikeTrapType.WestFloor );
					sTrap.ItemID = 4506;
					sTrap.MoveToWorld (new Point3D(from.X, from.Y, from.Z), from.Map);
					from.SendGump(new AddTrapGump());
					break;
				}
                case 9:
				{
					SpikeTrap sTrap = new SpikeTrap( SpikeTrapType.NorthFloor );
					sTrap.ItemID = 4512;
					sTrap.MoveToWorld (new Point3D(from.X, from.Y, from.Z), from.Map);
					from.SendGump(new AddTrapGump());
					break;
				}
                case 10:
				{
					StoneFaceTrap sTrap = new StoneFaceTrap();
					sTrap.ItemID = 0x110F;
					sTrap.MoveToWorld (new Point3D(from.X, from.Y, (from.Z+3)), from.Map);
					from.SendGump(new AddTrapGump());
					break;
				}
                case 11:
				{
					SawTrap sTrap = new SawTrap( SawTrapType.NorthWall );
					sTrap.ItemID = 0x1103;
					sTrap.MoveToWorld (new Point3D(from.X, from.Y, (from.Z+10)), from.Map);
					from.SendGump(new AddTrapGump());
					break;
				}
                case 12:
				{
					SawTrap sTrap = new SawTrap( SawTrapType.WestWall );
					sTrap.ItemID = 0x1116;
					sTrap.MoveToWorld (new Point3D(from.X, from.Y, (from.Z+10)), from.Map);
					from.SendGump(new AddTrapGump());
					break;
				}
                case 13:
				{
					SawTrap sTrap = new SawTrap( SawTrapType.WestFloor );
					sTrap.ItemID = 0x11B1;
					sTrap.MoveToWorld (new Point3D(from.X, from.Y, from.Z), from.Map);
					from.SendGump(new AddTrapGump());
					break;
				}
                case 14:
				{
					SawTrap sTrap = new SawTrap( SawTrapType.NorthFloor );
					sTrap.ItemID = 0x11AC;
					sTrap.MoveToWorld (new Point3D(from.X, from.Y, from.Z), from.Map);
					from.SendGump(new AddTrapGump());
					break;
				}
                case 15:
				{
					StoneFaceTrap sTrap = new StoneFaceTrap();
					sTrap.ItemID = 0x10FC;
					sTrap.MoveToWorld (new Point3D(from.X, from.Y, (from.Z+3)), from.Map);
					from.SendGump(new AddTrapGump());
					break;
				}
            }
        }
    }
}