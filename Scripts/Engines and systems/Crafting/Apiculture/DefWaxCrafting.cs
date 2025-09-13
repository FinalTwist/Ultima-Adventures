using System;
using Server.Items;

namespace Server.Engines.Craft
{
	public class DefWaxingPot : CraftSystem
	{
		public override SkillName MainSkill
		{
			get	{ return SkillName.Cooking; }
		}

        public override int GumpTitleNumber
        {
            get { return 0; }
        }
 
        public override string GumpTitleString
        {
            get { return "<BASEFONT Color=#FBFBFB><CENTER>WAX CRAFTING MENU</CENTER></BASEFONT>"; }
        }

		private static CraftSystem m_CraftSystem;

		public static CraftSystem CraftSystem
		{
			get
			{
				if ( m_CraftSystem == null )
					m_CraftSystem = new DefWaxingPot();

				return m_CraftSystem;
			}
		}

		public override CraftECA ECA{ get{ return CraftECA.ChanceMinusSixtyToFourtyFive; } }

		public override double GetChanceAtMin( CraftItem item )
		{
			return 0.5; // 50%
		}

		private DefWaxingPot() : base( 1, 1, 1.25 )// base( 1, 1, 4.5 )
		{
		}

		public override int CanCraft( Mobile from, BaseTool tool, Type itemType )
		{
			if( tool == null || tool.Deleted || tool.UsesRemaining < 0 )
				return 1044038; // You have worn out your tool!
			else if ( !BaseTool.CheckAccessible( tool, from ) )
				return 1044263; // The tool must be on your person to use.

			return 0;
		}

		public override void PlayCraftEffect( Mobile from )
		{
			from.PlaySound( 0x04E );
		}

		public override int PlayEndingEffect( Mobile from, bool failed, bool lostMaterial, bool toolBroken, int quality, bool makersMark, CraftItem item )
		{
			if ( toolBroken )
				from.SendLocalizedMessage( 1044038 ); // You have worn out your tool

			if ( failed )
			{
				if ( lostMaterial )
					return 1044043; // You failed to create the item, and some of your materials are lost.
				else
					return 1044157; // You failed to create the item, but no materials were lost.
			}
			else
			{
				return 1044154; // You create the item.
			}
		}

		public override void InitCraftList()
		{
			int index = -1;

/*
encaustic painting - mix dyes with wax - get a canvas - make a painting
wax tablets of spells
wax sculptors
*/

			#region Candles

			index = AddCraft(typeof(Candle), "Candles", "Candle, Small", 5.0, 45.0, typeof( Beeswax ), 1025154, 20, 1042081 );
			AddRes( index, typeof( IronIngot ), 1044036, 2, 1042081 );

			index = AddCraft(typeof(CandleLarge), "Candles", "Candle, Large", 15.0, 55.0, typeof( Beeswax ), 1025154, 20, 1042081 );
			AddRes( index, typeof( IronIngot ), 1044036, 2, 1042081 );

			AddCraft( typeof( ColorCandleShort ), "Candles", "Candle, Short, Dyeable", 10.0, 50.0, typeof( Beeswax ), 1025154, 10, 1042081 );

			AddCraft( typeof( ColorCandleLong ), "Candles", "Candle, Tall, Dyeable", 20.0, 60.0, typeof( Beeswax ), 1025154, 20, 1042081 );

			index = AddCraft(typeof(WallSconce), "Candles", "Candle, Sconce, Wall", 50.0, 90.0, typeof( Beeswax ), 1025154, 20, 1042081 );
			AddRes( index, typeof( IronIngot ), 1044036, 2, 1042081 );

			index = AddCraft(typeof(CandleSkull), "Candles", "Candle, Skull", 50.0, 90.0, typeof( Beeswax ), 1025154, 20, 1042081 );
			AddRes( index, typeof( Head ), "Human Head", 1, 1042081 );

			index = AddCraft(typeof(CandleReligious), "Candles", "Candle, Religious", 80.0, 120.0, typeof( Beeswax ), 1025154, 20, 1042081 );
			AddRes( index, typeof( IronIngot ), 1044036, 2, 1042081 );

			#endregion

			#region Rub

			index = AddCraft(typeof(JarsOfWaxInstrument), "Wax Polish", "Jar of Instrument Polish", 60.0, 100.0, typeof( Beeswax ), 1025154, 10, 1042081 );
			AddRes( index, typeof ( Bottle ), 1044529, 1, 500315 );

			index = AddCraft(typeof(JarsOfWaxLeather), "Wax Polish", "Jar of Leather Polish", 60.0, 100.0, typeof( Beeswax ), 1025154, 10, 1042081 );
			AddRes( index, typeof ( Bottle ), 1044529, 1, 500315 );

			index = AddCraft(typeof(JarsOfWaxMetal), "Wax Polish", "Jar of Metal Polish", 60.0, 100.0, typeof( Beeswax ), 1025154, 10, 1042081 );
			AddRes( index, typeof ( Bottle ), 1044529, 1, 500315 );

			#endregion

			#region Paintings

			index = AddCraft(typeof(WaxPainting), "Encaustic Paintings", "Painting, Large", 60.0, 100.0, typeof( Beeswax ), 1025154, 50, 1042081 );
			AddRes( index, typeof ( Dyes ), "dyes", 1, 1042081 );
			AddRes( index, typeof ( PaintCanvas ), "painting canvas", 1, 1042081 );
			AddRes( index, typeof ( Board ), "boards", 4, 1042081 );

			index = AddCraft(typeof(WaxPaintingA), "Encaustic Paintings", "Painting", 60.0, 100.0, typeof( Beeswax ), 1025154, 30, 1042081 );
			AddRes( index, typeof ( Dyes ), "dyes", 1, 1042081 );
			AddRes( index, typeof ( PaintCanvas ), "painting canvas", 1, 1042081 );
			AddRes( index, typeof ( Board ), "boards", 4, 1042081 );

			index = AddCraft(typeof(WaxPaintingB), "Encaustic Paintings", "Painting", 60.0, 100.0, typeof( Beeswax ), 1025154, 30, 1042081 );
			AddRes( index, typeof ( Dyes ), "dyes", 1, 1042081 );
			AddRes( index, typeof ( PaintCanvas ), "painting canvas", 1, 1042081 );
			AddRes( index, typeof ( Board ), "boards", 4, 1042081 );

			index = AddCraft(typeof(WaxPaintingC), "Encaustic Paintings", "Painting", 60.0, 100.0, typeof( Beeswax ), 1025154, 30, 1042081 );
			AddRes( index, typeof ( Dyes ), "dyes", 1, 1042081 );
			AddRes( index, typeof ( PaintCanvas ), "painting canvas", 1, 1042081 );
			AddRes( index, typeof ( Board ), "boards", 4, 1042081 );

			index = AddCraft(typeof(WaxPaintingD), "Encaustic Paintings", "Painting", 60.0, 100.0, typeof( Beeswax ), 1025154, 30, 1042081 );
			AddRes( index, typeof ( Dyes ), "dyes", 1, 1042081 );
			AddRes( index, typeof ( PaintCanvas ), "painting canvas", 1, 1042081 );
			AddRes( index, typeof ( Board ), "boards", 4, 1042081 );

			index = AddCraft(typeof(WaxPaintingE), "Encaustic Paintings", "Painting", 60.0, 100.0, typeof( Beeswax ), 1025154, 30, 1042081 );
			AddRes( index, typeof ( Dyes ), "dyes", 1, 1042081 );
			AddRes( index, typeof ( PaintCanvas ), "painting canvas", 1, 1042081 );
			AddRes( index, typeof ( Board ), "boards", 4, 1042081 );

			index = AddCraft(typeof(WaxPaintingF), "Encaustic Paintings", "Painting", 60.0, 100.0, typeof( Beeswax ), 1025154, 30, 1042081 );
			AddRes( index, typeof ( Dyes ), "dyes", 1, 1042081 );
			AddRes( index, typeof ( PaintCanvas ), "painting canvas", 1, 1042081 );
			AddRes( index, typeof ( Board ), "boards", 4, 1042081 );

			index = AddCraft(typeof(WaxPaintingG), "Encaustic Paintings", "Painting", 60.0, 100.0, typeof( Beeswax ), 1025154, 30, 1042081 );
			AddRes( index, typeof ( Dyes ), "dyes", 1, 1042081 );
			AddRes( index, typeof ( PaintCanvas ), "painting canvas", 1, 1042081 );
			AddRes( index, typeof ( Board ), "boards", 4, 1042081 );

			#endregion

			#region Sculptors

			AddCraft(typeof(WaxSculptors), "Wax Scupltors", "Sculptor", 60.0, 100.0, typeof( Beeswax ), 1025154, 40, 1042081 );
			AddCraft(typeof(WaxSculptorsA), "Wax Scupltors", "Sculptor", 60.0, 100.0, typeof( Beeswax ), 1025154, 40, 1042081 );
			AddCraft(typeof(WaxSculptorsB), "Wax Scupltors", "Sculptor", 60.0, 100.0, typeof( Beeswax ), 1025154, 40, 1042081 );
			AddCraft(typeof(WaxSculptorsC), "Wax Scupltors", "Sculptor", 60.0, 100.0, typeof( Beeswax ), 1025154, 40, 1042081 );
			AddCraft(typeof(WaxSculptorsD), "Wax Scupltors", "Sculptor, Angel", 60.0, 100.0, typeof( Beeswax ), 1025154, 40, 1042081 );
			AddCraft(typeof(WaxSculptorsE), "Wax Scupltors", "Sculptor, Dragon", 80.0, 120.0, typeof( Beeswax ), 1025154, 60, 1042081 );

			#endregion
		}
	}
}