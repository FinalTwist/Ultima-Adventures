using System;
using Server;
using Server.Network;
using Server.Multis;
using Server.Regions;
using Server.Mobiles;
using Server.Items.Crops;

namespace Server.Items
{
	public abstract class BaseHarvestPatchAddon : BaseAddon
	{
		public override abstract BaseAddonDeed Deed { get; }
		public abstract Item Crop { get; }

		private int m_crops;

		[CommandProperty( AccessLevel.GameMaster )]
		public int Crops
		{
			get { return m_crops; }
			set
			{
				if ( value < 0 )
					m_crops = 0;
				else
					m_crops = value;
			}
		}

		public BaseHarvestPatchAddon() : base()
		{
			Timer.DelayCall( TimeSpan.FromMinutes( Utility.RandomMinMax(4,8) ), new TimerCallback( Respawn ) );
		}

		public override bool OnMoveOver( Mobile m )
		{
			if ( m_crops > 0 && m is PlayerMobile && m.Alive)
				Timer.DelayCall( TimeSpan.FromSeconds( 1 ), new TimerStateCallback ( Harvest ), new object[]{ m }  );

			return base.OnMoveOver(m);
		}

		public override bool HandlesOnMovement{ get{ return true; } }

		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if ( m_crops > 0 && m is PlayerMobile && m.Alive && m.InRange( this.Location, 1 ) && m.InLOS( this ) )
				Timer.DelayCall( TimeSpan.FromSeconds( 1 ), new TimerStateCallback ( Harvest ), new object[]{ m }  );
		}

		public BaseHarvestPatchAddon( Serial serial ) : base( serial )
		{
		}

		public override void OnComponentUsed( AddonComponent c, Mobile from )
		{
			if ( from.InRange( c.Location, 2 ) )
			{
					if ( m_crops > 0 )
					{
						Item fruit = Crop;

						if ( fruit == null )
							return;

						if ( !from.PlaceInBackpack( fruit ) )
						{
							fruit.Delete();
							from.SendMessage( "There's no room in your pack for this crop." ); // There is no room in your backpack for the fruit.					
						}
						else
						{
							if ( --m_crops == 0 )
								Timer.DelayCall( TimeSpan.FromMinutes( 150 ), new TimerCallback( Respawn ) );

							from.SendMessage( "You harvest some of the crop and put it in your pack." ); // You pick some fruit and put it in your backpack.
						}
					}
					else
						from.SendMessage( "There is nothing more to harvest here." ); // There is no more fruit on this tree
			}
			else
				from.LocalOverheadMessage( MessageType.Regular, 0x3B2, 1019045 ); // I can't reach that.
		}

		public void Harvest( object state )
		{

			if (this.Deleted || this == null)
				return;

			object[] states = (object[])state;
			Mobile from = (Mobile)states[0];
			
			if (!(from is PlayerMobile) || from == null)
				return;

			if ( from.InRange( this.Location, 1 ) )
			{
					if ( m_crops > 0 )
					{
						Item fruit = Crop;

						if ( fruit == null )
							return;

						if ( !from.PlaceInBackpack( fruit ) )
						{
							fruit.Delete();
							from.SendMessage( "There's no room in your pack for this crop." ); // There is no room in your backpack for the fruit.					
						}
						else
						{
							m_crops --;

						    while (m_crops > 0)
							{
								fruit = Crop;
							    from.PlaceInBackpack( fruit ) ;
							    m_crops --;
							}
                                
						    Timer.DelayCall( TimeSpan.FromMinutes( Utility.RandomMinMax(150, 180) ), new TimerCallback( Respawn ) );
						    from.SendMessage( "You harvest the crop and put it in your pack." ); // You pick some fruit and put it in your backpack.
						}
					}
					else
						from.SendMessage( "There is nothing more to harvest here." ); // There is no more fruit on this tree
			}
		}

		private void Respawn()
		{
			m_crops = Utility.RandomMinMax( 1, 5 );
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version

			writer.Write( (int) m_crops );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();

			m_crops = reader.ReadInt();

			if ( m_crops == 0 )
				Respawn();
		}
	}


	public class HopsPatch : BaseHarvestPatchAddon
	{
		public override BaseAddonDeed Deed { get { return new HopsPatchDeed(); } }
		public override Item Crop { get { return new Hops(); } }

        private static int[,] m_AddOnSimpleComponents = new int[,] {
			  {2324, -1, -1, 1}, {11542, -1, 0, 1}, {11575, 0, 0, 1}// 5	6	7
			, {11575, 1, 0, 1}, {11543, 2, 0, 1} 	
			, {2324, -1, 0, 1}, {2324, -1, 1, 1}, {2324, 0, -1, 1}// 8	9	10	
			, {2324, 0, 0, 1}, {2324, 0, 1, 1}, {2324, 1, -1, 1}// 11	12	13	
			, {2324, 1, 0, 1}, {2324, 1, 1, 1}, {2324, 2, -1, 1}// 14	15	16	
			, {2324, 2, 0, 1}, {2324, 2, 1, 1}, {6018, 0, 1, 1}// 17	18	19	
			, {6018, 1, 1, 1}, {6020, 0, -1, 1}, {6020, 1, -1, 1}// 20	21	22	
			, {6022, -1, 1, 1}, {6023, -1, -1, 1}, {6024, 2, -1, 1}// 23	24	25	
			, {6025, 2, 1, 1}, {6019, 2, 0, 1}, {6018, -1, -2, 0}// 26	27	28	
			, {6018, 0, -2, 0}, {6018, 1, -2, 0}, {6018, 2, -2, 0}// 29	30	31	
			, {6019, -2, -1, 0}, {6019, -2, 0, 0}, {6019, -2, 1, 0}// 32	33	34	
			, {6020, -1, 2, 0}, {6020, 0, 2, 0}, {6020, 1, 2, 0}// 35	36	37	
			, {6020, 2, 2, 0}, {6021, 3, -1, 0}, {6021, 3, 0, 0}// 38	39	40	
			, {6021, 3, 1, 0}// 41	
		};

		[ Constructable ]
		public HopsPatch()
		{

            for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
                AddComponent( new AddonComponent( m_AddOnSimpleComponents[i,0] ), m_AddOnSimpleComponents[i,1], m_AddOnSimpleComponents[i,2], m_AddOnSimpleComponents[i,3] );


			AddComplexComponent( (BaseAddon) this, 6817, 1, 1, -5, 0, -1, "", 1);// 2
			AddComplexComponent( (BaseAddon) this, 6817, 2, 1, -5, 0, -1, "", 1);// 3
			AddComplexComponent( (BaseAddon) this, 6817, 0, 1, -5, 0, -1, "", 1);// 4

		}

		public HopsPatch( Serial serial ) : base( serial )
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

	public class HopsPatchDeed : BaseAddonDeed
	{
		public override BaseAddon Addon { get { return new HopsPatch(); } }

		[Constructable]
		public HopsPatchDeed() : base()
		{
			Name = "Patch of Hops";
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			list.Add( "Grows Hops" );
		}

		public HopsPatchDeed( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}
    public class PeaPatch : BaseHarvestPatchAddon
    {
        public override BaseAddonDeed Deed { get { return new PeaPatchDeed(); } }
        public override Item Crop { get { return new WoodenBowlOfPeas(); } }

        private static int[,] m_AddOnSimpleComponents = new int[,] {
              {2324, -1, -1, 1}// 5	6	7	
			, {2324, -1, 0, 1}, {2324, -1, 1, 1}, {2324, 0, -1, 1}// 8	9	10	
			, {2324, 0, 0, 1}, {2324, 0, 1, 1}, {2324, 1, -1, 1}// 11	12	13	
			, {2324, 1, 0, 1}, {2324, 1, 1, 1}, {2324, 2, -1, 1}// 14	15	16	
			, {2324, 2, 0, 1}, {2324, 2, 1, 1}, {6018, 0, 1, 1}// 17	18	19	
			, {6018, 1, 1, 1}, {6020, 0, -1, 1}, {6020, 1, -1, 1}// 20	21	22	
			, {6022, -1, 1, 1}, {6023, -1, -1, 1}, {6024, 2, -1, 1}// 23	24	25	
			, {6025, 2, 1, 1}, {6019, 2, 0, 1}, {6018, -1, -2, 0}// 26	27	28	
			, {6018, 0, -2, 0}, {6018, 1, -2, 0}, {6018, 2, -2, 0}// 29	30	31	
			, {6019, -2, -1, 0}, {6019, -2, 0, 0}, {6019, -2, 1, 0}// 32	33	34	
			, {6020, -1, 2, 0}, {6020, 0, 2, 0}, {6020, 1, 2, 0}// 35	36	37	
			, {6020, 2, 2, 0}, {6021, 3, -1, 0}, {6021, 3, 0, 0}// 38	39	40	
			, {6021, 3, 1, 0}// 41	
		};

        [Constructable]
        public PeaPatch()
        {

            for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
                AddComponent(new AddonComponent(m_AddOnSimpleComponents[i, 0]), m_AddOnSimpleComponents[i, 1], m_AddOnSimpleComponents[i, 2], m_AddOnSimpleComponents[i, 3]);


            AddComplexComponent((BaseAddon)this, 3310, 0, 0, 2, 0, -1, "", 1);// 1
            AddComplexComponent((BaseAddon)this, 3314, 1, 0, 2, 0, -1, "", 1);// 2
            AddComplexComponent((BaseAddon)this, 3310, 2, 0, 2, 0, -1, "", 1);// 3
            AddComplexComponent((BaseAddon)this, 3314, -1, 0, 2, 0, -1, "", 1);// 4

        }

        public PeaPatch(Serial serial) : base(serial)
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
                ac.Light = (LightType)lightsource;
            addon.AddComponent(ac, xoffset, yoffset, zoffset);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0); // Version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }

    public class PeaPatchDeed : BaseAddonDeed
    {
        public override BaseAddon Addon { get { return new PeaPatch(); } }

        [Constructable]
        public PeaPatchDeed() : base()
        {
            Name = "Patch of Peas";
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);
            list.Add("Grows Peas");
        }

        public PeaPatchDeed(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();
        }
    }
    public class GreenTeaPatch : BaseHarvestPatchAddon
    {
        public override BaseAddonDeed Deed { get { return new GreenTeaPatchDeed(); } }
        public override Item Crop { get { return new GreenTeaBasket(); } }

        private static int[,] m_AddOnSimpleComponents = new int[,] {
              {2324, -1, -1, 1}// 5	6	7	
			, {2324, -1, 0, 1}, {2324, -1, 1, 1}, {2324, 0, -1, 1}// 8	9	10	
			, {2324, 0, 0, 1}, {2324, 0, 1, 1}, {2324, 1, -1, 1}// 11	12	13	
			, {2324, 1, 0, 1}, {2324, 1, 1, 1}, {2324, 2, -1, 1}// 14	15	16	
			, {2324, 2, 0, 1}, {2324, 2, 1, 1}, {6018, 0, 1, 1}// 17	18	19	
			, {6018, 1, 1, 1}, {6020, 0, -1, 1}, {6020, 1, -1, 1}// 20	21	22	
			, {6022, -1, 1, 1}, {6023, -1, -1, 1}, {6024, 2, -1, 1}// 23	24	25	
			, {6025, 2, 1, 1}, {6019, 2, 0, 1}, {6018, -1, -2, 0}// 26	27	28	
			, {6018, 0, -2, 0}, {6018, 1, -2, 0}, {6018, 2, -2, 0}// 29	30	31	
			, {6019, -2, -1, 0}, {6019, -2, 0, 0}, {6019, -2, 1, 0}// 32	33	34	
			, {6020, -1, 2, 0}, {6020, 0, 2, 0}, {6020, 1, 2, 0}// 35	36	37	
			, {6020, 2, 2, 0}, {6021, 3, -1, 0}, {6021, 3, 0, 0}// 38	39	40	
			, {6021, 3, 1, 0}// 41	
		};

        [Constructable]
        public GreenTeaPatch()
        {

            for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
                AddComponent(new AddonComponent(m_AddOnSimpleComponents[i, 0]), m_AddOnSimpleComponents[i, 1], m_AddOnSimpleComponents[i, 2], m_AddOnSimpleComponents[i, 3]);


            AddComplexComponent((BaseAddon)this, 3271, 0, 0, 2, 0, -1, "", 1);// 1
            AddComplexComponent((BaseAddon)this, 3271, 1, 0, 2, 0, -1, "", 1);// 2
            AddComplexComponent((BaseAddon)this, 3271, 2, 0, 2, 0, -1, "", 1);// 3
            AddComplexComponent((BaseAddon)this, 3271, -1, 0, 2, 0, -1, "", 1);// 4

        }

        public GreenTeaPatch(Serial serial) : base(serial)
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
                ac.Light = (LightType)lightsource;
            addon.AddComponent(ac, xoffset, yoffset, zoffset);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0); // Version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }

    public class GreenTeaPatchDeed : BaseAddonDeed
    {
        public override BaseAddon Addon { get { return new GreenTeaPatch(); } }

        [Constructable]
        public GreenTeaPatchDeed() : base()
        {
            Name = "Patch of Green Tea";
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);
            list.Add("Grows Green Tea");
        }

        public GreenTeaPatchDeed(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();
        }
    }
    public class HayPatch : BaseHarvestPatchAddon
	{
		public override BaseAddonDeed Deed { get { return new HayPatchDeed(); } }
		public override Item Crop { get { return new WheatSheaf(); } }

        private static int[,] m_AddOnSimpleComponents = new int[,] {
			  {2324, -1, -1, 1}// 5	6	7	
			, {2324, -1, 0, 1}, {2324, -1, 1, 1}, {2324, 0, -1, 1}// 8	9	10	
			, {2324, 0, 0, 1}, {2324, 0, 1, 1}, {2324, 1, -1, 1}// 11	12	13	
			, {2324, 1, 0, 1}, {2324, 1, 1, 1}, {2324, 2, -1, 1}// 14	15	16	
			, {2324, 2, 0, 1}, {2324, 2, 1, 1}, {6018, 0, 1, 1}// 17	18	19	
			, {6018, 1, 1, 1}, {6020, 0, -1, 1}, {6020, 1, -1, 1}// 20	21	22	
			, {6022, -1, 1, 1}, {6023, -1, -1, 1}, {6024, 2, -1, 1}// 23	24	25	
			, {6025, 2, 1, 1}, {6019, 2, 0, 1}, {6018, -1, -2, 0}// 26	27	28	
			, {6018, 0, -2, 0}, {6018, 1, -2, 0}, {6018, 2, -2, 0}// 29	30	31	
			, {6019, -2, -1, 0}, {6019, -2, 0, 0}, {6019, -2, 1, 0}// 32	33	34	
			, {6020, -1, 2, 0}, {6020, 0, 2, 0}, {6020, 1, 2, 0}// 35	36	37	
			, {6020, 2, 2, 0}, {6021, 3, -1, 0}, {6021, 3, 0, 0}// 38	39	40	
			, {6021, 3, 1, 0}// 41	
		};

		[ Constructable ]
		public HayPatch()
		{

            for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
                AddComponent( new AddonComponent( m_AddOnSimpleComponents[i,0] ), m_AddOnSimpleComponents[i,1], m_AddOnSimpleComponents[i,2], m_AddOnSimpleComponents[i,3] );


			AddComplexComponent( (BaseAddon) this, 6802, 0, 0, 2, 0, -1, "", 1);// 1
			AddComplexComponent( (BaseAddon) this, 6802, 1, 0, 2, 0, -1, "", 1);// 2
			AddComplexComponent( (BaseAddon) this, 6802, 2, 0, 2, 0, -1, "", 1);// 3
			AddComplexComponent( (BaseAddon) this, 6802, -1, 0, 2, 0, -1, "", 1);// 4

		}

		public HayPatch( Serial serial ) : base( serial )
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

	public class HayPatchDeed : BaseAddonDeed
	{
		public override BaseAddon Addon { get { return new HayPatch(); } }

		[Constructable]
		public HayPatchDeed() : base()
		{
			Name = "Patch of Wheat";
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			list.Add( "Grows Wheat" );
		}

		public HayPatchDeed( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}

    public class PumpkinPatch : BaseHarvestPatchAddon
    {
        public override BaseAddonDeed Deed { get { return new PumpkinPatchDeed(); } }
        public override Item Crop { get { return new Pumpkin(); } }

        private static int[,] m_AddOnSimpleComponents = new int[,] {
              {2324, -1, -1, 1}// 5	6	7	
			, {2324, -1, 0, 1}, {2324, -1, 1, 1}, {2324, 0, -1, 1}// 8	9	10	
			, {2324, 0, 0, 1}, {2324, 0, 1, 1}, {2324, 1, -1, 1}// 11	12	13	
			, {2324, 1, 0, 1}, {2324, 1, 1, 1}, {2324, 2, -1, 1}// 14	15	16	
			, {2324, 2, 0, 1}, {2324, 2, 1, 1}, {6018, 0, 1, 1}// 17	18	19	
			, {6018, 1, 1, 1}, {6020, 0, -1, 1}, {6020, 1, -1, 1}// 20	21	22	
			, {6022, -1, 1, 1}, {6023, -1, -1, 1}, {6024, 2, -1, 1}// 23	24	25	
			, {6025, 2, 1, 1}, {6019, 2, 0, 1}, {6018, -1, -2, 0}// 26	27	28	
			, {6018, 0, -2, 0}, {6018, 1, -2, 0}, {6018, 2, -2, 0}// 29	30	31	
			, {6019, -2, -1, 0}, {6019, -2, 0, 0}, {6019, -2, 1, 0}// 32	33	34	
			, {6020, -1, 2, 0}, {6020, 0, 2, 0}, {6020, 1, 2, 0}// 35	36	37	
			, {6020, 2, 2, 0}, {6021, 3, -1, 0}, {6021, 3, 0, 0}// 38	39	40	
			, {6021, 3, 1, 0}// 41	
		};

        [Constructable]
        public PumpkinPatch()
        {

            for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
                AddComponent(new AddonComponent(m_AddOnSimpleComponents[i, 0]), m_AddOnSimpleComponents[i, 1], m_AddOnSimpleComponents[i, 2], m_AddOnSimpleComponents[i, 3]);


            AddComplexComponent((BaseAddon)this, 21449, 0, 0, 2, 0, -1, "", 1);// 1
            AddComplexComponent((BaseAddon)this, 21452, 1, 0, 2, 0, -1, "", 1);// 2
            AddComplexComponent((BaseAddon)this, 21451, 2, 0, 2, 0, -1, "", 1);// 3
            AddComplexComponent((BaseAddon)this, 21450, -1, 0, 2, 0, -1, "", 1);// 4

        }

        public PumpkinPatch(Serial serial) : base(serial)
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
                ac.Light = (LightType)lightsource;
            addon.AddComponent(ac, xoffset, yoffset, zoffset);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0); // Version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }

    public class PumpkinPatchDeed : BaseAddonDeed
    {
        public override BaseAddon Addon { get { return new PumpkinPatch(); } }

        [Constructable]
        public PumpkinPatchDeed() : base()
        {
            Name = "Patch of Pumpkins";
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);
            list.Add("Grows Pumpkins");
        }

        public PumpkinPatchDeed(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();
        }
    }
    public class TomatoPatch : BaseHarvestPatchAddon
    {
        public override BaseAddonDeed Deed { get { return new TomatoPatchDeed(); } }
        public override Item Crop { get { return new Tomato(); } }

        private static int[,] m_AddOnSimpleComponents = new int[,] {
              {2324, -1, -1, 1}// 5	6	7	
			, {2324, -1, 0, 1}, {2324, -1, 1, 1}, {2324, 0, -1, 1}// 8	9	10	
			, {2324, 0, 0, 1}, {2324, 0, 1, 1}, {2324, 1, -1, 1}// 11	12	13	
			, {2324, 1, 0, 1}, {2324, 1, 1, 1}, {2324, 2, -1, 1}// 14	15	16	
			, {2324, 2, 0, 1}, {2324, 2, 1, 1}, {6018, 0, 1, 1}// 17	18	19	
			, {6018, 1, 1, 1}, {6020, 0, -1, 1}, {6020, 1, -1, 1}// 20	21	22	
			, {6022, -1, 1, 1}, {6023, -1, -1, 1}, {6024, 2, -1, 1}// 23	24	25	
			, {6025, 2, 1, 1}, {6019, 2, 0, 1}, {6018, -1, -2, 0}// 26	27	28	
			, {6018, 0, -2, 0}, {6018, 1, -2, 0}, {6018, 2, -2, 0}// 29	30	31	
			, {6019, -2, -1, 0}, {6019, -2, 0, 0}, {6019, -2, 1, 0}// 32	33	34	
			, {6020, -1, 2, 0}, {6020, 0, 2, 0}, {6020, 1, 2, 0}// 35	36	37	
			, {6020, 2, 2, 0}, {6021, 3, -1, 0}, {6021, 3, 0, 0}// 38	39	40	
			, {6021, 3, 1, 0}// 41	
		};

        [Constructable]
        public TomatoPatch()
        {

            for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
                AddComponent(new AddonComponent(m_AddOnSimpleComponents[i, 0]), m_AddOnSimpleComponents[i, 1], m_AddOnSimpleComponents[i, 2], m_AddOnSimpleComponents[i, 3]);


            AddComplexComponent((BaseAddon)this, 3564, 0, 0, 2, 0, -1, "", 1);// 1
            AddComplexComponent((BaseAddon)this, 3566, 1, 0, 2, 0, -1, "", 1);// 2
            AddComplexComponent((BaseAddon)this, 3564, 2, 0, 2, 0, -1, "", 1);// 3
            AddComplexComponent((BaseAddon)this, 3566, -1, 0, 2, 0, -1, "", 1);// 4

        }

        public TomatoPatch(Serial serial) : base(serial)
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
                ac.Light = (LightType)lightsource;
            addon.AddComponent(ac, xoffset, yoffset, zoffset);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0); // Version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }

    public class TomatoPatchDeed : BaseAddonDeed
    {
        public override BaseAddon Addon { get { return new TomatoPatch(); } }

        [Constructable]
        public TomatoPatchDeed() : base()
        {
            Name = "Patch of Tomatoes";
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);
            list.Add("Grows Tomatoes");
        }

        public TomatoPatchDeed(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();
        }
    }

    public class CabbagePatch : BaseHarvestPatchAddon
    {
        public override BaseAddonDeed Deed { get { return new CabbagePatchDeed(); } }
        public override Item Crop { get { return new Cabbage(); } }

        private static int[,] m_AddOnSimpleComponents = new int[,] {
              {2324, -1, -1, 1}// 5	6	7	
			, {2324, -1, 0, 1}, {2324, -1, 1, 1}, {2324, 0, -1, 1}// 8	9	10	
			, {2324, 0, 0, 1}, {2324, 0, 1, 1}, {2324, 1, -1, 1}// 11	12	13	
			, {2324, 1, 0, 1}, {2324, 1, 1, 1}, {2324, 2, -1, 1}// 14	15	16	
			, {2324, 2, 0, 1}, {2324, 2, 1, 1}, {6018, 0, 1, 1}// 17	18	19	
			, {6018, 1, 1, 1}, {6020, 0, -1, 1}, {6020, 1, -1, 1}// 20	21	22	
			, {6022, -1, 1, 1}, {6023, -1, -1, 1}, {6024, 2, -1, 1}// 23	24	25	
			, {6025, 2, 1, 1}, {6019, 2, 0, 1}, {6018, -1, -2, 0}// 26	27	28	
			, {6018, 0, -2, 0}, {6018, 1, -2, 0}, {6018, 2, -2, 0}// 29	30	31	
			, {6019, -2, -1, 0}, {6019, -2, 0, 0}, {6019, -2, 1, 0}// 32	33	34	
			, {6020, -1, 2, 0}, {6020, 0, 2, 0}, {6020, 1, 2, 0}// 35	36	37	
			, {6020, 2, 2, 0}, {6021, 3, -1, 0}, {6021, 3, 0, 0}// 38	39	40	
			, {6021, 3, 1, 0}// 41	
		};

        [Constructable]
        public CabbagePatch()
        {

            for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
                AddComponent(new AddonComponent(m_AddOnSimpleComponents[i, 0]), m_AddOnSimpleComponents[i, 1], m_AddOnSimpleComponents[i, 2], m_AddOnSimpleComponents[i, 3]);


            AddComplexComponent((BaseAddon)this, 3196, 0, 0, 2, 0, -1, "", 1);// 1
            AddComplexComponent((BaseAddon)this, 3195, 1, 0, 2, 0, -1, "", 1);// 2
            AddComplexComponent((BaseAddon)this, 3196, 2, 0, 2, 0, -1, "", 1);// 3
            AddComplexComponent((BaseAddon)this, 3195, -1, 0, 2, 0, -1, "", 1);// 4

        }

        public CabbagePatch(Serial serial) : base(serial)
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
                ac.Light = (LightType)lightsource;
            addon.AddComponent(ac, xoffset, yoffset, zoffset);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0); // Version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }

    public class CabbagePatchDeed : BaseAddonDeed
    {
        public override BaseAddon Addon { get { return new CabbagePatch(); } }

        [Constructable]
        public CabbagePatchDeed() : base()
        {
            Name = "Patch of Cabbages";
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);
            list.Add("Grows Cabbages");
        }

        public CabbagePatchDeed(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();
        }
    }

    public class MelonPatch : BaseHarvestPatchAddon
    {
        public override BaseAddonDeed Deed { get { return new MelonPatchDeed(); } }
        public override Item Crop { get { return new SmallWatermelon(); } }

        private static int[,] m_AddOnSimpleComponents = new int[,] {
              {2324, -1, -1, 1}// 5	6	7	
			, {2324, -1, 0, 1}, {2324, -1, 1, 1}, {2324, 0, -1, 1}// 8	9	10	
			, {2324, 0, 0, 1}, {2324, 0, 1, 1}, {2324, 1, -1, 1}// 11	12	13	
			, {2324, 1, 0, 1}, {2324, 1, 1, 1}, {2324, 2, -1, 1}// 14	15	16	
			, {2324, 2, 0, 1}, {2324, 2, 1, 1}, {6018, 0, 1, 1}// 17	18	19	
			, {6018, 1, 1, 1}, {6020, 0, -1, 1}, {6020, 1, -1, 1}// 20	21	22	
			, {6022, -1, 1, 1}, {6023, -1, -1, 1}, {6024, 2, -1, 1}// 23	24	25	
			, {6025, 2, 1, 1}, {6019, 2, 0, 1}, {6018, -1, -2, 0}// 26	27	28	
			, {6018, 0, -2, 0}, {6018, 1, -2, 0}, {6018, 2, -2, 0}// 29	30	31	
			, {6019, -2, -1, 0}, {6019, -2, 0, 0}, {6019, -2, 1, 0}// 32	33	34	
			, {6020, -1, 2, 0}, {6020, 0, 2, 0}, {6020, 1, 2, 0}// 35	36	37	
			, {6020, 2, 2, 0}, {6021, 3, -1, 0}, {6021, 3, 0, 0}// 38	39	40	
			, {6021, 3, 1, 0}// 41	
		};

        [Constructable]
        public MelonPatch()
        {

            for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
                AddComponent(new AddonComponent(m_AddOnSimpleComponents[i, 0]), m_AddOnSimpleComponents[i, 1], m_AddOnSimpleComponents[i, 2], m_AddOnSimpleComponents[i, 3]);


            AddComplexComponent((BaseAddon)this, 3164, 0, 0, 2, 0, -1, "", 1);// 1
            AddComplexComponent((BaseAddon)this, 3165, 1, 0, 2, 0, -1, "", 1);// 2
            AddComplexComponent((BaseAddon)this, 3164, 2, 0, 2, 0, -1, "", 1);// 3
            AddComplexComponent((BaseAddon)this, 3165, -1, 0, 2, 0, -1, "", 1);// 4

        }

        public MelonPatch(Serial serial) : base(serial)
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
                ac.Light = (LightType)lightsource;
            addon.AddComponent(ac, xoffset, yoffset, zoffset);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0); // Version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }

    public class MelonPatchDeed : BaseAddonDeed
    {
        public override BaseAddon Addon { get { return new MelonPatch(); } }

        [Constructable]
        public MelonPatchDeed() : base()
        {
            Name = "Patch of Melons";
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);
            list.Add("Grows Melons");
        }

        public MelonPatchDeed(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();
        }
    }

    public class TurnipPatch : BaseHarvestPatchAddon
    {
        public override BaseAddonDeed Deed { get { return new TurnipPatchDeed(); } }
        public override Item Crop { get { return new Turnip(); } }

        private static int[,] m_AddOnSimpleComponents = new int[,] {
              {2324, -1, -1, 1}// 5	6	7	
			, {2324, -1, 0, 1}, {2324, -1, 1, 1}, {2324, 0, -1, 1}// 8	9	10	
			, {2324, 0, 0, 1}, {2324, 0, 1, 1}, {2324, 1, -1, 1}// 11	12	13	
			, {2324, 1, 0, 1}, {2324, 1, 1, 1}, {2324, 2, -1, 1}// 14	15	16	
			, {2324, 2, 0, 1}, {2324, 2, 1, 1}, {6018, 0, 1, 1}// 17	18	19	
			, {6018, 1, 1, 1}, {6020, 0, -1, 1}, {6020, 1, -1, 1}// 20	21	22	
			, {6022, -1, 1, 1}, {6023, -1, -1, 1}, {6024, 2, -1, 1}// 23	24	25	
			, {6025, 2, 1, 1}, {6019, 2, 0, 1}, {6018, -1, -2, 0}// 26	27	28	
			, {6018, 0, -2, 0}, {6018, 1, -2, 0}, {6018, 2, -2, 0}// 29	30	31	
			, {6019, -2, -1, 0}, {6019, -2, 0, 0}, {6019, -2, 1, 0}// 32	33	34	
			, {6020, -1, 2, 0}, {6020, 0, 2, 0}, {6020, 1, 2, 0}// 35	36	37	
			, {6020, 2, 2, 0}, {6021, 3, -1, 0}, {6021, 3, 0, 0}// 38	39	40	
			, {6021, 3, 1, 0}// 41	
		};

        [Constructable]
        public TurnipPatch()
        {

            for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
                AddComponent(new AddonComponent(m_AddOnSimpleComponents[i, 0]), m_AddOnSimpleComponents[i, 1], m_AddOnSimpleComponents[i, 2], m_AddOnSimpleComponents[i, 3]);


            AddComplexComponent((BaseAddon)this, 3169, 0, 0, 2, 0, -1, "", 1);// 1
            AddComplexComponent((BaseAddon)this, 3170, 1, 0, 2, 0, -1, "", 1);// 2
            AddComplexComponent((BaseAddon)this, 3171, 2, 0, 2, 0, -1, "", 1);// 3
            AddComplexComponent((BaseAddon)this, 3169, -1, 0, 2, 0, -1, "", 1);// 4

        }

        public TurnipPatch(Serial serial) : base(serial)
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
                ac.Light = (LightType)lightsource;
            addon.AddComponent(ac, xoffset, yoffset, zoffset);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0); // Version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }

    public class TurnipPatchDeed : BaseAddonDeed
    {
        public override BaseAddon Addon { get { return new TurnipPatch(); } }

        [Constructable]
        public TurnipPatchDeed() : base()
        {
            Name = "Patch of Turnips";
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);
            list.Add("Grows Turnips");
        }

        public TurnipPatchDeed(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();
        }
    }

    public class GourdPatch : BaseHarvestPatchAddon
    {
        public override BaseAddonDeed Deed { get { return new GourdPatchDeed(); } }
        public override Item Crop { get { return new YellowGourd(); } }

        private static int[,] m_AddOnSimpleComponents = new int[,] {
              {2324, -1, -1, 1}// 5	6	7	
			, {2324, -1, 0, 1}, {2324, -1, 1, 1}, {2324, 0, -1, 1}// 8	9	10	
			, {2324, 0, 0, 1}, {2324, 0, 1, 1}, {2324, 1, -1, 1}// 11	12	13	
			, {2324, 1, 0, 1}, {2324, 1, 1, 1}, {2324, 2, -1, 1}// 14	15	16	
			, {2324, 2, 0, 1}, {2324, 2, 1, 1}, {6018, 0, 1, 1}// 17	18	19	
			, {6018, 1, 1, 1}, {6020, 0, -1, 1}, {6020, 1, -1, 1}// 20	21	22	
			, {6022, -1, 1, 1}, {6023, -1, -1, 1}, {6024, 2, -1, 1}// 23	24	25	
			, {6025, 2, 1, 1}, {6019, 2, 0, 1}, {6018, -1, -2, 0}// 26	27	28	
			, {6018, 0, -2, 0}, {6018, 1, -2, 0}, {6018, 2, -2, 0}// 29	30	31	
			, {6019, -2, -1, 0}, {6019, -2, 0, 0}, {6019, -2, 1, 0}// 32	33	34	
			, {6020, -1, 2, 0}, {6020, 0, 2, 0}, {6020, 1, 2, 0}// 35	36	37	
			, {6020, 2, 2, 0}, {6021, 3, -1, 0}, {6021, 3, 0, 0}// 38	39	40	
			, {6021, 3, 1, 0}// 41	
		};

        [Constructable]
        public GourdPatch()
        {

            for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
                AddComponent(new AddonComponent(m_AddOnSimpleComponents[i, 0]), m_AddOnSimpleComponents[i, 1], m_AddOnSimpleComponents[i, 2], m_AddOnSimpleComponents[i, 3]);


            AddComplexComponent((BaseAddon)this, 3173, 0, 0, 2, 0, -1, "", 1);// 1
            AddComplexComponent((BaseAddon)this, 3172, 1, 0, 2, 0, -1, "", 1);// 2
            AddComplexComponent((BaseAddon)this, 3173, 2, 0, 2, 0, -1, "", 1);// 3
            AddComplexComponent((BaseAddon)this, 3172, -1, 0, 2, 0, -1, "", 1);// 4

        }

        public GourdPatch(Serial serial) : base(serial)
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
                ac.Light = (LightType)lightsource;
            addon.AddComponent(ac, xoffset, yoffset, zoffset);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0); // Version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }

    public class GourdPatchDeed : BaseAddonDeed
    {
        public override BaseAddon Addon { get { return new GourdPatch(); } }

        [Constructable]
        public GourdPatchDeed() : base()
        {
            Name = "Patch of Gourds";
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);
            list.Add("Grows Gourds");
        }

        public GourdPatchDeed(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();
        }
    }
    public class OnionPatch : BaseHarvestPatchAddon
    {
        public override BaseAddonDeed Deed { get { return new OnionPatchDeed(); } }
        public override Item Crop { get { return new Onion(); } }

        private static int[,] m_AddOnSimpleComponents = new int[,] {
              {2324, -1, -1, 1}// 5	6	7	
			, {2324, -1, 0, 1}, {2324, -1, 1, 1}, {2324, 0, -1, 1}// 8	9	10	
			, {2324, 0, 0, 1}, {2324, 0, 1, 1}, {2324, 1, -1, 1}// 11	12	13	
			, {2324, 1, 0, 1}, {2324, 1, 1, 1}, {2324, 2, -1, 1}// 14	15	16	
			, {2324, 2, 0, 1}, {2324, 2, 1, 1}, {6018, 0, 1, 1}// 17	18	19	
			, {6018, 1, 1, 1}, {6020, 0, -1, 1}, {6020, 1, -1, 1}// 20	21	22	
			, {6022, -1, 1, 1}, {6023, -1, -1, 1}, {6024, 2, -1, 1}// 23	24	25	
			, {6025, 2, 1, 1}, {6019, 2, 0, 1}, {6018, -1, -2, 0}// 26	27	28	
			, {6018, 0, -2, 0}, {6018, 1, -2, 0}, {6018, 2, -2, 0}// 29	30	31	
			, {6019, -2, -1, 0}, {6019, -2, 0, 0}, {6019, -2, 1, 0}// 32	33	34	
			, {6020, -1, 2, 0}, {6020, 0, 2, 0}, {6020, 1, 2, 0}// 35	36	37	
			, {6020, 2, 2, 0}, {6021, 3, -1, 0}, {6021, 3, 0, 0}// 38	39	40	
			, {6021, 3, 1, 0}// 41	
		};

        [Constructable]
        public OnionPatch()
        {

            for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
                AddComponent(new AddonComponent(m_AddOnSimpleComponents[i, 0]), m_AddOnSimpleComponents[i, 1], m_AddOnSimpleComponents[i, 2], m_AddOnSimpleComponents[i, 3]);


            AddComplexComponent((BaseAddon)this, 3183, 0, 0, 2, 0, -1, "", 1);// 1
            AddComplexComponent((BaseAddon)this, 3183, 1, 0, 2, 0, -1, "", 1);// 2
            AddComplexComponent((BaseAddon)this, 3183, 2, 0, 2, 0, -1, "", 1);// 3
            AddComplexComponent((BaseAddon)this, 3183, -1, 0, 2, 0, -1, "", 1);// 4

        }

        public OnionPatch(Serial serial) : base(serial)
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
                ac.Light = (LightType)lightsource;
            addon.AddComponent(ac, xoffset, yoffset, zoffset);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0); // Version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }

    public class OnionPatchDeed : BaseAddonDeed
    {
        public override BaseAddon Addon { get { return new OnionPatch(); } }

        [Constructable]
        public OnionPatchDeed() : base()
        {
            Name = "Patch of Onions";
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);
            list.Add("Grows Onions");
        }

        public OnionPatchDeed(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();
        }
    }
    public class LettucePatch : BaseHarvestPatchAddon
    {
        public override BaseAddonDeed Deed { get { return new LettucePatchDeed(); } }
        public override Item Crop { get { return new Lettuce(); } }

        private static int[,] m_AddOnSimpleComponents = new int[,] {
              {2324, -1, -1, 1}// 5	6	7	
			, {2324, -1, 0, 1}, {2324, -1, 1, 1}, {2324, 0, -1, 1}// 8	9	10	
			, {2324, 0, 0, 1}, {2324, 0, 1, 1}, {2324, 1, -1, 1}// 11	12	13	
			, {2324, 1, 0, 1}, {2324, 1, 1, 1}, {2324, 2, -1, 1}// 14	15	16	
			, {2324, 2, 0, 1}, {2324, 2, 1, 1}, {6018, 0, 1, 1}// 17	18	19	
			, {6018, 1, 1, 1}, {6020, 0, -1, 1}, {6020, 1, -1, 1}// 20	21	22	
			, {6022, -1, 1, 1}, {6023, -1, -1, 1}, {6024, 2, -1, 1}// 23	24	25	
			, {6025, 2, 1, 1}, {6019, 2, 0, 1}, {6018, -1, -2, 0}// 26	27	28	
			, {6018, 0, -2, 0}, {6018, 1, -2, 0}, {6018, 2, -2, 0}// 29	30	31	
			, {6019, -2, -1, 0}, {6019, -2, 0, 0}, {6019, -2, 1, 0}// 32	33	34	
			, {6020, -1, 2, 0}, {6020, 0, 2, 0}, {6020, 1, 2, 0}// 35	36	37	
			, {6020, 2, 2, 0}, {6021, 3, -1, 0}, {6021, 3, 0, 0}// 38	39	40	
			, {6021, 3, 1, 0}// 41	
		};

        [Constructable]
        public LettucePatch()
        {

            for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
                AddComponent(new AddonComponent(m_AddOnSimpleComponents[i, 0]), m_AddOnSimpleComponents[i, 1], m_AddOnSimpleComponents[i, 2], m_AddOnSimpleComponents[i, 3]);


            AddComplexComponent((BaseAddon)this, 3184, 0, 0, 2, 0, -1, "", 1);// 1
            AddComplexComponent((BaseAddon)this, 3185, 1, 0, 2, 0, -1, "", 1);// 2
            AddComplexComponent((BaseAddon)this, 3184, 2, 0, 2, 0, -1, "", 1);// 3
            AddComplexComponent((BaseAddon)this, 3185, -1, 0, 2, 0, -1, "", 1);// 4

        }

        public LettucePatch(Serial serial) : base(serial)
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
                ac.Light = (LightType)lightsource;
            addon.AddComponent(ac, xoffset, yoffset, zoffset);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0); // Version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }

    public class LettucePatchDeed : BaseAddonDeed
    {
        public override BaseAddon Addon { get { return new LettucePatch(); } }

        [Constructable]
        public LettucePatchDeed() : base()
        {
            Name = "Patch of Lettuces";
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);
            list.Add("Grows Lettuces");
        }

        public LettucePatchDeed(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();
        }
    }
    public class SquashPatch : BaseHarvestPatchAddon
    {
        public override BaseAddonDeed Deed { get { return new SquashPatchDeed(); } }
        public override Item Crop { get { return new Squash(); } }

        private static int[,] m_AddOnSimpleComponents = new int[,] {
              {2324, -1, -1, 1}// 5	6	7	
			, {2324, -1, 0, 1}, {2324, -1, 1, 1}, {2324, 0, -1, 1}// 8	9	10	
			, {2324, 0, 0, 1}, {2324, 0, 1, 1}, {2324, 1, -1, 1}// 11	12	13	
			, {2324, 1, 0, 1}, {2324, 1, 1, 1}, {2324, 2, -1, 1}// 14	15	16	
			, {2324, 2, 0, 1}, {2324, 2, 1, 1}, {6018, 0, 1, 1}// 17	18	19	
			, {6018, 1, 1, 1}, {6020, 0, -1, 1}, {6020, 1, -1, 1}// 20	21	22	
			, {6022, -1, 1, 1}, {6023, -1, -1, 1}, {6024, 2, -1, 1}// 23	24	25	
			, {6025, 2, 1, 1}, {6019, 2, 0, 1}, {6018, -1, -2, 0}// 26	27	28	
			, {6018, 0, -2, 0}, {6018, 1, -2, 0}, {6018, 2, -2, 0}// 29	30	31	
			, {6019, -2, -1, 0}, {6019, -2, 0, 0}, {6019, -2, 1, 0}// 32	33	34	
			, {6020, -1, 2, 0}, {6020, 0, 2, 0}, {6020, 1, 2, 0}// 35	36	37	
			, {6020, 2, 2, 0}, {6021, 3, -1, 0}, {6021, 3, 0, 0}// 38	39	40	
			, {6021, 3, 1, 0}// 41	
		};

        [Constructable]
        public SquashPatch()
        {

            for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
                AddComponent(new AddonComponent(m_AddOnSimpleComponents[i, 0]), m_AddOnSimpleComponents[i, 1], m_AddOnSimpleComponents[i, 2], m_AddOnSimpleComponents[i, 3]);


            AddComplexComponent((BaseAddon)this, 3186, 0, 0, 2, 0, -1, "", 1);// 1
            AddComplexComponent((BaseAddon)this, 3187, 1, 0, 2, 0, -1, "", 1);// 2
            AddComplexComponent((BaseAddon)this, 3186, 2, 0, 2, 0, -1, "", 1);// 3
            AddComplexComponent((BaseAddon)this, 3187, -1, 0, 2, 0, -1, "", 1);// 4

        }

        public SquashPatch(Serial serial) : base(serial)
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
                ac.Light = (LightType)lightsource;
            addon.AddComponent(ac, xoffset, yoffset, zoffset);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0); // Version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }

    public class SquashPatchDeed : BaseAddonDeed
    {
        public override BaseAddon Addon { get { return new SquashPatch(); } }

        [Constructable]
        public SquashPatchDeed() : base()
        {
            Name = "Patch of Squash";
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);
            list.Add("Grows Squash");
        }

        public SquashPatchDeed(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();
        }
    }
    public class HoneydewPatch : BaseHarvestPatchAddon
    {
        public override BaseAddonDeed Deed { get { return new HoneydewPatchDeed(); } }
        public override Item Crop { get { return new HoneydewMelon(); } }

        private static int[,] m_AddOnSimpleComponents = new int[,] {
              {2324, -1, -1, 1}// 5	6	7	
			, {2324, -1, 0, 1}, {2324, -1, 1, 1}, {2324, 0, -1, 1}// 8	9	10	
			, {2324, 0, 0, 1}, {2324, 0, 1, 1}, {2324, 1, -1, 1}// 11	12	13	
			, {2324, 1, 0, 1}, {2324, 1, 1, 1}, {2324, 2, -1, 1}// 14	15	16	
			, {2324, 2, 0, 1}, {2324, 2, 1, 1}, {6018, 0, 1, 1}// 17	18	19	
			, {6018, 1, 1, 1}, {6020, 0, -1, 1}, {6020, 1, -1, 1}// 20	21	22	
			, {6022, -1, 1, 1}, {6023, -1, -1, 1}, {6024, 2, -1, 1}// 23	24	25	
			, {6025, 2, 1, 1}, {6019, 2, 0, 1}, {6018, -1, -2, 0}// 26	27	28	
			, {6018, 0, -2, 0}, {6018, 1, -2, 0}, {6018, 2, -2, 0}// 29	30	31	
			, {6019, -2, -1, 0}, {6019, -2, 0, 0}, {6019, -2, 1, 0}// 32	33	34	
			, {6020, -1, 2, 0}, {6020, 0, 2, 0}, {6020, 1, 2, 0}// 35	36	37	
			, {6020, 2, 2, 0}, {6021, 3, -1, 0}, {6021, 3, 0, 0}// 38	39	40	
			, {6021, 3, 1, 0}// 41	
		};

        [Constructable]
        public HoneydewPatch()
        {

            for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
                AddComponent(new AddonComponent(m_AddOnSimpleComponents[i, 0]), m_AddOnSimpleComponents[i, 1], m_AddOnSimpleComponents[i, 2], m_AddOnSimpleComponents[i, 3]);


            AddComplexComponent((BaseAddon)this, 3188, 0, 0, 2, 0, -1, "", 1);// 1
            AddComplexComponent((BaseAddon)this, 3189, 1, 0, 2, 0, -1, "", 1);// 2
            AddComplexComponent((BaseAddon)this, 3188, 2, 0, 2, 0, -1, "", 1);// 3
            AddComplexComponent((BaseAddon)this, 3189, -1, 0, 2, 0, -1, "", 1);// 4

        }

        public HoneydewPatch(Serial serial) : base(serial)
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
                ac.Light = (LightType)lightsource;
            addon.AddComponent(ac, xoffset, yoffset, zoffset);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0); // Version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }

    public class HoneydewPatchDeed : BaseAddonDeed
    {
        public override BaseAddon Addon { get { return new HoneydewPatch(); } }

        [Constructable]
        public HoneydewPatchDeed() : base()
        {
            Name = "Patch of Honeydew Melons";
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);
            list.Add("Grows Honeydew Melons");
        }

        public HoneydewPatchDeed(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();
        }
    }

    public class CarrotPatch : BaseHarvestPatchAddon
    {
        public override BaseAddonDeed Deed { get { return new CarrotPatchDeed(); } }
        public override Item Crop { get { return new Carrot(); } }

        private static int[,] m_AddOnSimpleComponents = new int[,] {
              {2324, -1, -1, 1}// 5	6	7	
			, {2324, -1, 0, 1}, {2324, -1, 1, 1}, {2324, 0, -1, 1}// 8	9	10	
			, {2324, 0, 0, 1}, {2324, 0, 1, 1}, {2324, 1, -1, 1}// 11	12	13	
			, {2324, 1, 0, 1}, {2324, 1, 1, 1}, {2324, 2, -1, 1}// 14	15	16	
			, {2324, 2, 0, 1}, {2324, 2, 1, 1}, {6018, 0, 1, 1}// 17	18	19	
			, {6018, 1, 1, 1}, {6020, 0, -1, 1}, {6020, 1, -1, 1}// 20	21	22	
			, {6022, -1, 1, 1}, {6023, -1, -1, 1}, {6024, 2, -1, 1}// 23	24	25	
			, {6025, 2, 1, 1}, {6019, 2, 0, 1}, {6018, -1, -2, 0}// 26	27	28	
			, {6018, 0, -2, 0}, {6018, 1, -2, 0}, {6018, 2, -2, 0}// 29	30	31	
			, {6019, -2, -1, 0}, {6019, -2, 0, 0}, {6019, -2, 1, 0}// 32	33	34	
			, {6020, -1, 2, 0}, {6020, 0, 2, 0}, {6020, 1, 2, 0}// 35	36	37	
			, {6020, 2, 2, 0}, {6021, 3, -1, 0}, {6021, 3, 0, 0}// 38	39	40	
			, {6021, 3, 1, 0}// 41	
		};

        [Constructable]
        public CarrotPatch()
        {

            for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
                AddComponent(new AddonComponent(m_AddOnSimpleComponents[i, 0]), m_AddOnSimpleComponents[i, 1], m_AddOnSimpleComponents[i, 2], m_AddOnSimpleComponents[i, 3]);


            AddComplexComponent((BaseAddon)this, 3190, 0, 0, 2, 0, -1, "", 1);// 1
            AddComplexComponent((BaseAddon)this, 3190, 1, 0, 2, 0, -1, "", 1);// 2
            AddComplexComponent((BaseAddon)this, 3190, 2, 0, 2, 0, -1, "", 1);// 3
            AddComplexComponent((BaseAddon)this, 3190, -1, 0, 2, 0, -1, "", 1);// 4

        }

        public CarrotPatch(Serial serial) : base(serial)
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
                ac.Light = (LightType)lightsource;
            addon.AddComponent(ac, xoffset, yoffset, zoffset);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0); // Version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }

    public class CarrotPatchDeed : BaseAddonDeed
    {
        public override BaseAddon Addon { get { return new CarrotPatch(); } }

        [Constructable]
        public CarrotPatchDeed() : base()
        {
            Name = "Patch of Carrots";
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);
            list.Add("Grows Carrots");
        }

        public CarrotPatchDeed(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();
        }
    }
    public class CantaloupePatch : BaseHarvestPatchAddon
    {
        public override BaseAddonDeed Deed { get { return new CantaloupePatchDeed(); } }
        public override Item Crop { get { return new Cantaloupe(); } }

        private static int[,] m_AddOnSimpleComponents = new int[,] {
              {2324, -1, -1, 1}// 5	6	7	
			, {2324, -1, 0, 1}, {2324, -1, 1, 1}, {2324, 0, -1, 1}// 8	9	10	
			, {2324, 0, 0, 1}, {2324, 0, 1, 1}, {2324, 1, -1, 1}// 11	12	13	
			, {2324, 1, 0, 1}, {2324, 1, 1, 1}, {2324, 2, -1, 1}// 14	15	16	
			, {2324, 2, 0, 1}, {2324, 2, 1, 1}, {6018, 0, 1, 1}// 17	18	19	
			, {6018, 1, 1, 1}, {6020, 0, -1, 1}, {6020, 1, -1, 1}// 20	21	22	
			, {6022, -1, 1, 1}, {6023, -1, -1, 1}, {6024, 2, -1, 1}// 23	24	25	
			, {6025, 2, 1, 1}, {6019, 2, 0, 1}, {6018, -1, -2, 0}// 26	27	28	
			, {6018, 0, -2, 0}, {6018, 1, -2, 0}, {6018, 2, -2, 0}// 29	30	31	
			, {6019, -2, -1, 0}, {6019, -2, 0, 0}, {6019, -2, 1, 0}// 32	33	34	
			, {6020, -1, 2, 0}, {6020, 0, 2, 0}, {6020, 1, 2, 0}// 35	36	37	
			, {6020, 2, 2, 0}, {6021, 3, -1, 0}, {6021, 3, 0, 0}// 38	39	40	
			, {6021, 3, 1, 0}// 41	
		};

        [Constructable]
        public CantaloupePatch()
        {

            for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
                AddComponent(new AddonComponent(m_AddOnSimpleComponents[i, 0]), m_AddOnSimpleComponents[i, 1], m_AddOnSimpleComponents[i, 2], m_AddOnSimpleComponents[i, 3]);


            AddComplexComponent((BaseAddon)this, 3193, 0, 0, 2, 0, -1, "", 1);// 1
            AddComplexComponent((BaseAddon)this, 3194, 1, 0, 2, 0, -1, "", 1);// 2
            AddComplexComponent((BaseAddon)this, 3193, 2, 0, 2, 0, -1, "", 1);// 3
            AddComplexComponent((BaseAddon)this, 3194, -1, 0, 2, 0, -1, "", 1);// 4

        }

        public CantaloupePatch(Serial serial) : base(serial)
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
                ac.Light = (LightType)lightsource;
            addon.AddComponent(ac, xoffset, yoffset, zoffset);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0); // Version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }

    public class CantaloupePatchDeed : BaseAddonDeed
    {
        public override BaseAddon Addon { get { return new CantaloupePatch(); } }

        [Constructable]
        public CantaloupePatchDeed() : base()
        {
            Name = "Patch of Cantaloupes";
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);
            list.Add("Grows Cantaloupes");
        }

        public CantaloupePatchDeed(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();
        }
    }
    public class CornPatch : BaseHarvestPatchAddon
    {
        public override BaseAddonDeed Deed { get { return new CornPatchDeed(); } }
        public override Item Crop { get { return new EarOfCorn(); } }

        private static int[,] m_AddOnSimpleComponents = new int[,] {
              {2324, -1, -1, 1}// 5	6	7	
			, {2324, -1, 0, 1}, {2324, -1, 1, 1}, {2324, 0, -1, 1}// 8	9	10	
			, {2324, 0, 0, 1}, {2324, 0, 1, 1}, {2324, 1, -1, 1}// 11	12	13	
			, {2324, 1, 0, 1}, {2324, 1, 1, 1}, {2324, 2, -1, 1}// 14	15	16	
			, {2324, 2, 0, 1}, {2324, 2, 1, 1}, {6018, 0, 1, 1}// 17	18	19	
			, {6018, 1, 1, 1}, {6020, 0, -1, 1}, {6020, 1, -1, 1}// 20	21	22	
			, {6022, -1, 1, 1}, {6023, -1, -1, 1}, {6024, 2, -1, 1}// 23	24	25	
			, {6025, 2, 1, 1}, {6019, 2, 0, 1}, {6018, -1, -2, 0}// 26	27	28	
			, {6018, 0, -2, 0}, {6018, 1, -2, 0}, {6018, 2, -2, 0}// 29	30	31	
			, {6019, -2, -1, 0}, {6019, -2, 0, 0}, {6019, -2, 1, 0}// 32	33	34	
			, {6020, -1, 2, 0}, {6020, 0, 2, 0}, {6020, 1, 2, 0}// 35	36	37	
			, {6020, 2, 2, 0}, {6021, 3, -1, 0}, {6021, 3, 0, 0}// 38	39	40	
			, {6021, 3, 1, 0}// 41	
		};

        [Constructable]
        public CornPatch()
        {

            for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
                AddComponent(new AddonComponent(m_AddOnSimpleComponents[i, 0]), m_AddOnSimpleComponents[i, 1], m_AddOnSimpleComponents[i, 2], m_AddOnSimpleComponents[i, 3]);


            AddComplexComponent((BaseAddon)this, 3197, 0, 0, 2, 0, -1, "", 1);// 1
            AddComplexComponent((BaseAddon)this, 3197, 1, 0, 2, 0, -1, "", 1);// 2
            AddComplexComponent((BaseAddon)this, 3197, 2, 0, 2, 0, -1, "", 1);// 3
            AddComplexComponent((BaseAddon)this, 3197, -1, 0, 2, 0, -1, "", 1);// 4

        }

        public CornPatch(Serial serial) : base(serial)
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
                ac.Light = (LightType)lightsource;
            addon.AddComponent(ac, xoffset, yoffset, zoffset);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0); // Version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }

    public class CornPatchDeed : BaseAddonDeed
    {
        public override BaseAddon Addon { get { return new CornPatch(); } }

        [Constructable]
        public CornPatchDeed() : base()
        {
            Name = "Patch of Corn";
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);
            list.Add("Grows Corn");
        }

        public CornPatchDeed(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();
        }
    }

    public class PotatoPatch : BaseHarvestPatchAddon
    {
        public override BaseAddonDeed Deed { get { return new PotatoPatchDeed(); } }
        public override Item Crop { get { return new FoodPotato(); } }

        private static int[,] m_AddOnSimpleComponents = new int[,] {
              {2324, -1, -1, 1}// 5	6	7	
			, {2324, -1, 0, 1}, {2324, -1, 1, 1}, {2324, 0, -1, 1}// 8	9	10	
			, {2324, 0, 0, 1}, {2324, 0, 1, 1}, {2324, 1, -1, 1}// 11	12	13	
			, {2324, 1, 0, 1}, {2324, 1, 1, 1}, {2324, 2, -1, 1}// 14	15	16	
			, {2324, 2, 0, 1}, {2324, 2, 1, 1}, {6018, 0, 1, 1}// 17	18	19	
			, {6018, 1, 1, 1}, {6020, 0, -1, 1}, {6020, 1, -1, 1}// 20	21	22	
			, {6022, -1, 1, 1}, {6023, -1, -1, 1}, {6024, 2, -1, 1}// 23	24	25	
			, {6025, 2, 1, 1}, {6019, 2, 0, 1}, {6018, -1, -2, 0}// 26	27	28	
			, {6018, 0, -2, 0}, {6018, 1, -2, 0}, {6018, 2, -2, 0}// 29	30	31	
			, {6019, -2, -1, 0}, {6019, -2, 0, 0}, {6019, -2, 1, 0}// 32	33	34	
			, {6020, -1, 2, 0}, {6020, 0, 2, 0}, {6020, 1, 2, 0}// 35	36	37	
			, {6020, 2, 2, 0}, {6021, 3, -1, 0}, {6021, 3, 0, 0}// 38	39	40	
			, {6021, 3, 1, 0}// 41	
		};

        [Constructable]
        public PotatoPatch()
        {

            for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
                AddComponent(new AddonComponent(m_AddOnSimpleComponents[i, 0]), m_AddOnSimpleComponents[i, 1], m_AddOnSimpleComponents[i, 2], m_AddOnSimpleComponents[i, 3]);


            AddComplexComponent((BaseAddon)this, 3169, 0, 0, 2, 0, -1, "", 1);// 1
            AddComplexComponent((BaseAddon)this, 3170, 1, 0, 2, 0, -1, "", 1);// 2
            AddComplexComponent((BaseAddon)this, 3169, 2, 0, 2, 0, -1, "", 1);// 3
            AddComplexComponent((BaseAddon)this, 3171, -1, 0, 2, 0, -1, "", 1);// 4

        }

        public PotatoPatch(Serial serial) : base(serial)
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
                ac.Light = (LightType)lightsource;
            addon.AddComponent(ac, xoffset, yoffset, zoffset);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0); // Version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }

    public class PotatoPatchDeed : BaseAddonDeed
    {
        public override BaseAddon Addon { get { return new PotatoPatch(); } }

        [Constructable]
        public PotatoPatchDeed() : base()
        {
            Name = "Patch of Potatoes";
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);
            list.Add("Grows Potatoes");
        }

        public PotatoPatchDeed(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();
        }
    }

    public class BananaPatch : BaseHarvestPatchAddon
    {
        public override BaseAddonDeed Deed { get { return new BananaPatchDeed(); } }
        public override Item Crop { get { return new Banana(); } }

        private static int[,] m_AddOnSimpleComponents = new int[,] {
              {2324, -1, -1, 1}// 5	6	7	
			, {2324, -1, 0, 1}, {2324, -1, 1, 1}, {2324, 0, -1, 1}// 8	9	10	
			, {2324, 0, 0, 1}, {2324, 0, 1, 1}, {2324, 1, -1, 1}// 11	12	13	
			, {2324, 1, 0, 1}, {2324, 1, 1, 1}, {2324, 2, -1, 1}// 14	15	16	
			, {2324, 2, 0, 1}, {2324, 2, 1, 1}, {6018, 0, 1, 1}// 17	18	19	
			, {6018, 1, 1, 1}, {6020, 0, -1, 1}, {6020, 1, -1, 1}// 20	21	22	
			, {6022, -1, 1, 1}, {6023, -1, -1, 1}, {6024, 2, -1, 1}// 23	24	25	
			, {6025, 2, 1, 1}, {6019, 2, 0, 1}, {6018, -1, -2, 0}// 26	27	28	
			, {6018, 0, -2, 0}, {6018, 1, -2, 0}, {6018, 2, -2, 0}// 29	30	31	
			, {6019, -2, -1, 0}, {6019, -2, 0, 0}, {6019, -2, 1, 0}// 32	33	34	
			, {6020, -1, 2, 0}, {6020, 0, 2, 0}, {6020, 1, 2, 0}// 35	36	37	
			, {6020, 2, 2, 0}, {6021, 3, -1, 0}, {6021, 3, 0, 0}// 38	39	40	
			, {6021, 3, 1, 0}// 41	
		};

        [Constructable]
        public BananaPatch()
        {

            for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
                AddComponent(new AddonComponent(m_AddOnSimpleComponents[i, 0]), m_AddOnSimpleComponents[i, 1], m_AddOnSimpleComponents[i, 2], m_AddOnSimpleComponents[i, 3]);


            AddComplexComponent((BaseAddon)this, 3240, 0, 0, 2, 0, -1, "", 1);// 1
            AddComplexComponent((BaseAddon)this, 3240, 1, 0, 2, 0, -1, "", 1);// 2
            AddComplexComponent((BaseAddon)this, 3242, 2, 0, 2, 0, -1, "", 1);// 3
            AddComplexComponent((BaseAddon)this, 3240, -1, 0, 2, 0, -1, "", 1);// 4

        }

        public BananaPatch(Serial serial) : base(serial)
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
                ac.Light = (LightType)lightsource;
            addon.AddComponent(ac, xoffset, yoffset, zoffset);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0); // Version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }

    public class BananaPatchDeed : BaseAddonDeed
    {
        public override BaseAddon Addon { get { return new BananaPatch(); } }

        [Constructable]
        public BananaPatchDeed() : base()
        {
            Name = "Patch of Bananas";
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);
            list.Add("Grows Bananas");
        }

        public BananaPatchDeed(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();
        }
    }
    public class CoconutPatch : BaseHarvestPatchAddon
    {
        public override BaseAddonDeed Deed { get { return new CoconutPatchDeed(); } }
        public override Item Crop { get { return new Coconut(); } }

        private static int[,] m_AddOnSimpleComponents = new int[,] {
              {2324, -1, -1, 1}// 5	6	7	
			, {2324, -1, 0, 1}, {2324, -1, 1, 1}, {2324, 0, -1, 1}// 8	9	10	
			, {2324, 0, 0, 1}, {2324, 0, 1, 1}, {2324, 1, -1, 1}// 11	12	13	
			, {2324, 1, 0, 1}, {2324, 1, 1, 1}, {2324, 2, -1, 1}// 14	15	16	
			, {2324, 2, 0, 1}, {2324, 2, 1, 1}, {6018, 0, 1, 1}// 17	18	19	
			, {6018, 1, 1, 1}, {6020, 0, -1, 1}, {6020, 1, -1, 1}// 20	21	22	
			, {6022, -1, 1, 1}, {6023, -1, -1, 1}, {6024, 2, -1, 1}// 23	24	25	
			, {6025, 2, 1, 1}, {6019, 2, 0, 1}, {6018, -1, -2, 0}// 26	27	28	
			, {6018, 0, -2, 0}, {6018, 1, -2, 0}, {6018, 2, -2, 0}// 29	30	31	
			, {6019, -2, -1, 0}, {6019, -2, 0, 0}, {6019, -2, 1, 0}// 32	33	34	
			, {6020, -1, 2, 0}, {6020, 0, 2, 0}, {6020, 1, 2, 0}// 35	36	37	
			, {6020, 2, 2, 0}, {6021, 3, -1, 0}, {6021, 3, 0, 0}// 38	39	40	
			, {6021, 3, 1, 0}// 41	
		};

        [Constructable]
        public CoconutPatch()
        {

            for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
                AddComponent(new AddonComponent(m_AddOnSimpleComponents[i, 0]), m_AddOnSimpleComponents[i, 1], m_AddOnSimpleComponents[i, 2], m_AddOnSimpleComponents[i, 3]);


            AddComplexComponent((BaseAddon)this, 3221, 0, 0, 2, 0, -1, "", 1);// 1
            AddComplexComponent((BaseAddon)this, 3221, 1, 0, 2, 0, -1, "", 1);// 2
            AddComplexComponent((BaseAddon)this, 3221, 2, 0, 2, 0, -1, "", 1);// 3
            AddComplexComponent((BaseAddon)this, 3221, -1, 0, 2, 0, -1, "", 1);// 4

        }

        public CoconutPatch(Serial serial) : base(serial)
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
                ac.Light = (LightType)lightsource;
            addon.AddComponent(ac, xoffset, yoffset, zoffset);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0); // Version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }

    public class CoconutPatchDeed : BaseAddonDeed
    {
        public override BaseAddon Addon { get { return new CoconutPatch(); } }

        [Constructable]
        public CoconutPatchDeed() : base()
        {
            Name = "Patch of Coconuts";
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);
            list.Add("Grows Coconuts");
        }

        public CoconutPatchDeed(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();
        }
    }
    public class DatePatch : BaseHarvestPatchAddon
    {
        public override BaseAddonDeed Deed { get { return new DatePatchDeed(); } }
        public override Item Crop { get { return new Dates(); } }

        private static int[,] m_AddOnSimpleComponents = new int[,] {
              {2324, -1, -1, 1}// 5	6	7	
			, {2324, -1, 0, 1}, {2324, -1, 1, 1}, {2324, 0, -1, 1}// 8	9	10	
			, {2324, 0, 0, 1}, {2324, 0, 1, 1}, {2324, 1, -1, 1}// 11	12	13	
			, {2324, 1, 0, 1}, {2324, 1, 1, 1}, {2324, 2, -1, 1}// 14	15	16	
			, {2324, 2, 0, 1}, {2324, 2, 1, 1}, {6018, 0, 1, 1}// 17	18	19	
			, {6018, 1, 1, 1}, {6020, 0, -1, 1}, {6020, 1, -1, 1}// 20	21	22	
			, {6022, -1, 1, 1}, {6023, -1, -1, 1}, {6024, 2, -1, 1}// 23	24	25	
			, {6025, 2, 1, 1}, {6019, 2, 0, 1}, {6018, -1, -2, 0}// 26	27	28	
			, {6018, 0, -2, 0}, {6018, 1, -2, 0}, {6018, 2, -2, 0}// 29	30	31	
			, {6019, -2, -1, 0}, {6019, -2, 0, 0}, {6019, -2, 1, 0}// 32	33	34	
			, {6020, -1, 2, 0}, {6020, 0, 2, 0}, {6020, 1, 2, 0}// 35	36	37	
			, {6020, 2, 2, 0}, {6021, 3, -1, 0}, {6021, 3, 0, 0}// 38	39	40	
			, {6021, 3, 1, 0}// 41	
		};

        [Constructable]
        public DatePatch()
        {

            for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
                AddComponent(new AddonComponent(m_AddOnSimpleComponents[i, 0]), m_AddOnSimpleComponents[i, 1], m_AddOnSimpleComponents[i, 2], m_AddOnSimpleComponents[i, 3]);


            AddComplexComponent((BaseAddon)this, 3222, 0, 0, 2, 0, -1, "", 1);// 1
            AddComplexComponent((BaseAddon)this, 3222, 1, 0, 2, 0, -1, "", 1);// 2
            AddComplexComponent((BaseAddon)this, 3222, 2, 0, 2, 0, -1, "", 1);// 3
            AddComplexComponent((BaseAddon)this, 3222, -1, 0, 2, 0, -1, "", 1);// 4

        }

        public DatePatch(Serial serial) : base(serial)
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
                ac.Light = (LightType)lightsource;
            addon.AddComponent(ac, xoffset, yoffset, zoffset);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0); // Version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }

    public class DatePatchDeed : BaseAddonDeed
    {
        public override BaseAddon Addon { get { return new DatePatch(); } }

        [Constructable]
        public DatePatchDeed() : base()
        {
            Name = "Patch of Dates";
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);
            list.Add("Grows Dates");
        }

        public DatePatchDeed(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();
        }
    }
    public class GarlicPatch : BaseHarvestPatchAddon
    {
        public override BaseAddonDeed Deed { get { return new GarlicPatchDeed(); } }
        public override Item Crop { get { return new Garlic(); } }

        private static int[,] m_AddOnSimpleComponents = new int[,] {
              {2324, -1, -1, 1}// 5	6	7	
			, {2324, -1, 0, 1}, {2324, -1, 1, 1}, {2324, 0, -1, 1}// 8	9	10	
			, {2324, 0, 0, 1}, {2324, 0, 1, 1}, {2324, 1, -1, 1}// 11	12	13	
			, {2324, 1, 0, 1}, {2324, 1, 1, 1}, {2324, 2, -1, 1}// 14	15	16	
			, {2324, 2, 0, 1}, {2324, 2, 1, 1}, {6018, 0, 1, 1}// 17	18	19	
			, {6018, 1, 1, 1}, {6020, 0, -1, 1}, {6020, 1, -1, 1}// 20	21	22	
			, {6022, -1, 1, 1}, {6023, -1, -1, 1}, {6024, 2, -1, 1}// 23	24	25	
			, {6025, 2, 1, 1}, {6019, 2, 0, 1}, {6018, -1, -2, 0}// 26	27	28	
			, {6018, 0, -2, 0}, {6018, 1, -2, 0}, {6018, 2, -2, 0}// 29	30	31	
			, {6019, -2, -1, 0}, {6019, -2, 0, 0}, {6019, -2, 1, 0}// 32	33	34	
			, {6020, -1, 2, 0}, {6020, 0, 2, 0}, {6020, 1, 2, 0}// 35	36	37	
			, {6020, 2, 2, 0}, {6021, 3, -1, 0}, {6021, 3, 0, 0}// 38	39	40	
			, {6021, 3, 1, 0}// 41	
		};

        [Constructable]
        public GarlicPatch()
        {

            for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
                AddComponent(new AddonComponent(m_AddOnSimpleComponents[i, 0]), m_AddOnSimpleComponents[i, 1], m_AddOnSimpleComponents[i, 2], m_AddOnSimpleComponents[i, 3]);


            AddComplexComponent((BaseAddon)this, 6369, 0, 0, 2, 0, -1, "", 1);// 1
            AddComplexComponent((BaseAddon)this, 6370, 1, 0, 2, 0, -1, "", 1);// 2
            AddComplexComponent((BaseAddon)this, 6370, 2, 0, 2, 0, -1, "", 1);// 3
            AddComplexComponent((BaseAddon)this, 6369, -1, 0, 2, 0, -1, "", 1);// 4

        }

        public GarlicPatch(Serial serial) : base(serial)
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
                ac.Light = (LightType)lightsource;
            addon.AddComponent(ac, xoffset, yoffset, zoffset);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0); // Version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }

    public class GarlicPatchDeed : BaseAddonDeed
    {
        public override BaseAddon Addon { get { return new GarlicPatch(); } }

        [Constructable]
        public GarlicPatchDeed() : base()
        {
            Name = "Patch of Garlic";
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);
            list.Add("Grows Garlic");
        }

        public GarlicPatchDeed(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();
        }
    }
    public class NightshadePatch : BaseHarvestPatchAddon
    {
        public override BaseAddonDeed Deed { get { return new NightshadePatchDeed(); } }
        public override Item Crop { get { return new Nightshade(); } }

        private static int[,] m_AddOnSimpleComponents = new int[,] {
              {2324, -1, -1, 1}// 5	6	7	
			, {2324, -1, 0, 1}, {2324, -1, 1, 1}, {2324, 0, -1, 1}// 8	9	10	
			, {2324, 0, 0, 1}, {2324, 0, 1, 1}, {2324, 1, -1, 1}// 11	12	13	
			, {2324, 1, 0, 1}, {2324, 1, 1, 1}, {2324, 2, -1, 1}// 14	15	16	
			, {2324, 2, 0, 1}, {2324, 2, 1, 1}, {6018, 0, 1, 1}// 17	18	19	
			, {6018, 1, 1, 1}, {6020, 0, -1, 1}, {6020, 1, -1, 1}// 20	21	22	
			, {6022, -1, 1, 1}, {6023, -1, -1, 1}, {6024, 2, -1, 1}// 23	24	25	
			, {6025, 2, 1, 1}, {6019, 2, 0, 1}, {6018, -1, -2, 0}// 26	27	28	
			, {6018, 0, -2, 0}, {6018, 1, -2, 0}, {6018, 2, -2, 0}// 29	30	31	
			, {6019, -2, -1, 0}, {6019, -2, 0, 0}, {6019, -2, 1, 0}// 32	33	34	
			, {6020, -1, 2, 0}, {6020, 0, 2, 0}, {6020, 1, 2, 0}// 35	36	37	
			, {6020, 2, 2, 0}, {6021, 3, -1, 0}, {6021, 3, 0, 0}// 38	39	40	
			, {6021, 3, 1, 0}// 41	
		};

        [Constructable]
        public NightshadePatch()
        {

            for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
                AddComponent(new AddonComponent(m_AddOnSimpleComponents[i, 0]), m_AddOnSimpleComponents[i, 1], m_AddOnSimpleComponents[i, 2], m_AddOnSimpleComponents[i, 3]);


            AddComplexComponent((BaseAddon)this, 6373, 0, 0, 2, 0, -1, "", 1);// 1
            AddComplexComponent((BaseAddon)this, 6374, 1, 0, 2, 0, -1, "", 1);// 2
            AddComplexComponent((BaseAddon)this, 6373, 2, 0, 2, 0, -1, "", 1);// 3
            AddComplexComponent((BaseAddon)this, 6374, -1, 0, 2, 0, -1, "", 1);// 4

        }

        public NightshadePatch(Serial serial) : base(serial)
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
                ac.Light = (LightType)lightsource;
            addon.AddComponent(ac, xoffset, yoffset, zoffset);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0); // Version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }

    public class NightshadePatchDeed : BaseAddonDeed
    {
        public override BaseAddon Addon { get { return new NightshadePatch(); } }

        [Constructable]
        public NightshadePatchDeed() : base()
        {
            Name = "Patch of Nightshade";
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);
            list.Add("Grows Nightshade");
        }

        public NightshadePatchDeed(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();
        }
    }
    public class GinsengPatch : BaseHarvestPatchAddon
    {
        public override BaseAddonDeed Deed { get { return new GinsengPatchDeed(); } }
        public override Item Crop { get { return new Ginseng(); } }

        private static int[,] m_AddOnSimpleComponents = new int[,] {
              {2324, -1, -1, 1}// 5	6	7	
			, {2324, -1, 0, 1}, {2324, -1, 1, 1}, {2324, 0, -1, 1}// 8	9	10	
			, {2324, 0, 0, 1}, {2324, 0, 1, 1}, {2324, 1, -1, 1}// 11	12	13	
			, {2324, 1, 0, 1}, {2324, 1, 1, 1}, {2324, 2, -1, 1}// 14	15	16	
			, {2324, 2, 0, 1}, {2324, 2, 1, 1}, {6018, 0, 1, 1}// 17	18	19	
			, {6018, 1, 1, 1}, {6020, 0, -1, 1}, {6020, 1, -1, 1}// 20	21	22	
			, {6022, -1, 1, 1}, {6023, -1, -1, 1}, {6024, 2, -1, 1}// 23	24	25	
			, {6025, 2, 1, 1}, {6019, 2, 0, 1}, {6018, -1, -2, 0}// 26	27	28	
			, {6018, 0, -2, 0}, {6018, 1, -2, 0}, {6018, 2, -2, 0}// 29	30	31	
			, {6019, -2, -1, 0}, {6019, -2, 0, 0}, {6019, -2, 1, 0}// 32	33	34	
			, {6020, -1, 2, 0}, {6020, 0, 2, 0}, {6020, 1, 2, 0}// 35	36	37	
			, {6020, 2, 2, 0}, {6021, 3, -1, 0}, {6021, 3, 0, 0}// 38	39	40	
			, {6021, 3, 1, 0}// 41	
		};

        [Constructable]
        public GinsengPatch()
        {

            for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
                AddComponent(new AddonComponent(m_AddOnSimpleComponents[i, 0]), m_AddOnSimpleComponents[i, 1], m_AddOnSimpleComponents[i, 2], m_AddOnSimpleComponents[i, 3]);


            AddComplexComponent((BaseAddon)this, 6377, 0, 0, 2, 0, -1, "", 1);// 1
            AddComplexComponent((BaseAddon)this, 6378, 1, 0, 2, 0, -1, "", 1);// 2
            AddComplexComponent((BaseAddon)this, 6377, 2, 0, 2, 0, -1, "", 1);// 3
            AddComplexComponent((BaseAddon)this, 6378, -1, 0, 2, 0, -1, "", 1);// 4

        }

        public GinsengPatch(Serial serial) : base(serial)
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
                ac.Light = (LightType)lightsource;
            addon.AddComponent(ac, xoffset, yoffset, zoffset);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0); // Version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }

    public class GinsengPatchDeed : BaseAddonDeed
    {
        public override BaseAddon Addon { get { return new GinsengPatch(); } }

        [Constructable]
        public GinsengPatchDeed() : base()
        {
            Name = "Patch of Ginseng";
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);
            list.Add("Grows Ginseng");
        }

        public GinsengPatchDeed(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();
        }
    }
    public class MandrakePatch : BaseHarvestPatchAddon
    {
        public override BaseAddonDeed Deed { get { return new MandrakePatchDeed(); } }
        public override Item Crop { get { return new MandrakeRoot(); } }

        private static int[,] m_AddOnSimpleComponents = new int[,] {
              {2324, -1, -1, 1}// 5	6	7	
			, {2324, -1, 0, 1}, {2324, -1, 1, 1}, {2324, 0, -1, 1}// 8	9	10	
			, {2324, 0, 0, 1}, {2324, 0, 1, 1}, {2324, 1, -1, 1}// 11	12	13	
			, {2324, 1, 0, 1}, {2324, 1, 1, 1}, {2324, 2, -1, 1}// 14	15	16	
			, {2324, 2, 0, 1}, {2324, 2, 1, 1}, {6018, 0, 1, 1}// 17	18	19	
			, {6018, 1, 1, 1}, {6020, 0, -1, 1}, {6020, 1, -1, 1}// 20	21	22	
			, {6022, -1, 1, 1}, {6023, -1, -1, 1}, {6024, 2, -1, 1}// 23	24	25	
			, {6025, 2, 1, 1}, {6019, 2, 0, 1}, {6018, -1, -2, 0}// 26	27	28	
			, {6018, 0, -2, 0}, {6018, 1, -2, 0}, {6018, 2, -2, 0}// 29	30	31	
			, {6019, -2, -1, 0}, {6019, -2, 0, 0}, {6019, -2, 1, 0}// 32	33	34	
			, {6020, -1, 2, 0}, {6020, 0, 2, 0}, {6020, 1, 2, 0}// 35	36	37	
			, {6020, 2, 2, 0}, {6021, 3, -1, 0}, {6021, 3, 0, 0}// 38	39	40	
			, {6021, 3, 1, 0}// 41	
		};

        [Constructable]
        public MandrakePatch()
        {

            for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
                AddComponent(new AddonComponent(m_AddOnSimpleComponents[i, 0]), m_AddOnSimpleComponents[i, 1], m_AddOnSimpleComponents[i, 2], m_AddOnSimpleComponents[i, 3]);


            AddComplexComponent((BaseAddon)this, 6367, 0, 0, 2, 0, -1, "", 1);// 1
            AddComplexComponent((BaseAddon)this, 6368, 1, 0, 2, 0, -1, "", 1);// 2
            AddComplexComponent((BaseAddon)this, 6367, 2, 0, 2, 0, -1, "", 1);// 3
            AddComplexComponent((BaseAddon)this, 6368, -1, 0, 2, 0, -1, "", 1);// 4

        }

        public MandrakePatch(Serial serial) : base(serial)
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
                ac.Light = (LightType)lightsource;
            addon.AddComponent(ac, xoffset, yoffset, zoffset);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0); // Version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }

    public class MandrakePatchDeed : BaseAddonDeed
    {
        public override BaseAddon Addon { get { return new MandrakePatch(); } }

        [Constructable]
        public MandrakePatchDeed() : base()
        {
            Name = "Patch of Mandrake";
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);
            list.Add("Grows Mandrake");
        }

        public MandrakePatchDeed(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();
        }
    }
    public class FlaxPatch : BaseHarvestPatchAddon
    {
        public override BaseAddonDeed Deed { get { return new FlaxPatchDeed(); } }
        public override Item Crop { get { return new Flax(); } }

        private static int[,] m_AddOnSimpleComponents = new int[,] {
              {2324, -1, -1, 1}// 5	6	7	
			, {2324, -1, 0, 1}, {2324, -1, 1, 1}, {2324, 0, -1, 1}// 8	9	10	
			, {2324, 0, 0, 1}, {2324, 0, 1, 1}, {2324, 1, -1, 1}// 11	12	13	
			, {2324, 1, 0, 1}, {2324, 1, 1, 1}, {2324, 2, -1, 1}// 14	15	16	
			, {2324, 2, 0, 1}, {2324, 2, 1, 1}, {6018, 0, 1, 1}// 17	18	19	
			, {6018, 1, 1, 1}, {6020, 0, -1, 1}, {6020, 1, -1, 1}// 20	21	22	
			, {6022, -1, 1, 1}, {6023, -1, -1, 1}, {6024, 2, -1, 1}// 23	24	25	
			, {6025, 2, 1, 1}, {6019, 2, 0, 1}, {6018, -1, -2, 0}// 26	27	28	
			, {6018, 0, -2, 0}, {6018, 1, -2, 0}, {6018, 2, -2, 0}// 29	30	31	
			, {6019, -2, -1, 0}, {6019, -2, 0, 0}, {6019, -2, 1, 0}// 32	33	34	
			, {6020, -1, 2, 0}, {6020, 0, 2, 0}, {6020, 1, 2, 0}// 35	36	37	
			, {6020, 2, 2, 0}, {6021, 3, -1, 0}, {6021, 3, 0, 0}// 38	39	40	
			, {6021, 3, 1, 0}// 41	
		};

        [Constructable]
        public FlaxPatch()
        {

            for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
                AddComponent(new AddonComponent(m_AddOnSimpleComponents[i, 0]), m_AddOnSimpleComponents[i, 1], m_AddOnSimpleComponents[i, 2], m_AddOnSimpleComponents[i, 3]);


            AddComplexComponent((BaseAddon)this, 6809, 0, 0, 2, 0, -1, "", 1);// 1
            AddComplexComponent((BaseAddon)this, 6811, 1, 0, 2, 0, -1, "", 1);// 2
            AddComplexComponent((BaseAddon)this, 6811, 2, 0, 2, 0, -1, "", 1);// 3
            AddComplexComponent((BaseAddon)this, 6810, -1, 0, 2, 0, -1, "", 1);// 4

        }

        public FlaxPatch(Serial serial) : base(serial)
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
                ac.Light = (LightType)lightsource;
            addon.AddComponent(ac, xoffset, yoffset, zoffset);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0); // Version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }

    public class FlaxPatchDeed : BaseAddonDeed
    {
        public override BaseAddon Addon { get { return new FlaxPatch(); } }

        [Constructable]
        public FlaxPatchDeed() : base()
        {
            Name = "Patch of Flax";
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);
            list.Add("Grows Flax");
        }

        public FlaxPatchDeed(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();
        }
    }


    public class VinePatchAddon : BaseHarvestPatchAddon
	{
		public override BaseAddonDeed Deed { get { return new VinePatchAddonDeed(); } }
		public override Item Crop { get { return new Grapes(); } }

        private static int[,] m_AddOnSimpleComponents = new int[,] {
			  {2324, -1, -1, 0}, {2324, -1, 0, 0}, {2324, -1, 1, 0}// 1	2	3	
			, {2324, -1, 2, 0}, {2324, 0, -1, 0}, {2324, 0, 0, 0}// 4	5	6	
			, {2324, 0, 1, 0}, {2324, 0, 2, 0}, {2324, 1, -1, 0}// 7	8	9	
			, {2324, 1, 0, 1}, {2324, 1, 1, 0}, {2324, 1, 2, 0}// 10	11	12	
			, {6022, -1, 2, 1}, {6024, 1, -1, 1}, {6025, 1, 2, 1}// 13	14	15	
			, {6023, -1, -1, 1}, {6019, 1, 1, 1}, {6019, 1, 0, 1}// 16	17	18	
			, {6021, -1, 0, 1}, {6021, -1, 1, 1}// 19	20	21	
			, {6018, 0, 2, 1}, {6019, -2, -1, 0}// 22	23	28	
			, {6019, -2, 0, 0}, {6019, -2, 1, 0}, {6019, -2, 2, 0}// 29	30	31	
			, {6020, -1, 3, 0}, {6020, 0, 3, 0}, {6020, 1, 3, 0}// 32	33	34	
			, {6021, 2, -1, 0}, {6021, 2, 0, 0}, {6021, 2, 1, 0}// 35	36	37	
			, {6021, 2, 2, 0}, {6018, -1, -2, 0}, {6018, 0, -2, 0}// 38	39	40	
			, {6018, 1, -2, 0}// 41	
		};

		[ Constructable ]
		public VinePatchAddon()
		{

            for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
                AddComponent( new AddonComponent( m_AddOnSimpleComponents[i,0] ), m_AddOnSimpleComponents[i,1], m_AddOnSimpleComponents[i,2], m_AddOnSimpleComponents[i,3] );


			AddComplexComponent( (BaseAddon) this, 3358, 0, 0, 2, 0, -1, "", 1);// 24
			AddComplexComponent( (BaseAddon) this, 3358, 0, 1, 2, 0, -1, "", 1);// 25
			AddComplexComponent( (BaseAddon) this, 3355, 0, 2, 2, 0, -1, "", 1);// 26
			AddComplexComponent( (BaseAddon) this, 3357, 0, -1, 0, 0, -1, "", 1);// 27

		}

		public VinePatchAddon( Serial serial ) : base( serial )
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

	public class VinePatchAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon { get { return new VinePatchAddon(); } }

		[Constructable]
		public VinePatchAddonDeed() : base()
		{
			Name = "Patch of Grape Vines";
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			list.Add( "Grows Grapes" );
		}

		public VinePatchAddonDeed( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}

	public class SmokeweedPatchAddon : BaseHarvestPatchAddon
	{
		public override BaseAddonDeed Deed { get { return new SmokeweedPatchAddonDeed(); } }
		public override Item Crop { get { return new SmokeweedLeaves(); } }

        	private static int[,] m_AddOnSimpleComponents = new int[,] {
			  {2324, -1, -1, 0}, {2324, -1, 0, 0}, {2324, -1, 1, 0}// 1	2	3	
			, {2324, -1, 2, 0}, {2324, 0, -1, 0}, {2324, 0, 0, 0}// 4	5	6	
			, {2324, 0, 1, 0}, {2324, 0, 2, 0}, {2324, 1, -1, 0}// 7	8	9	
			, {2324, 1, 0, 1}, {2324, 1, 1, 0}, {2324, 1, 2, 0}// 10	11	12	
			, {6022, -1, 2, 1}, {6024, 1, -1, 1}, {6025, 1, 2, 1}// 13	14	15	
			, {6023, -1, -1, 1}, {6019, 1, 1, 1}, {6019, 1, 0, 1}// 16	17	18	
			, {6021, -1, 0, 1}, {6021, -1, 1, 1}// 19	20	21	
			, {6018, 0, 2, 1}, {6019, -2, -1, 0}// 22	23	28	
			, {6019, -2, 0, 0}, {6019, -2, 1, 0}, {6019, -2, 2, 0}// 29	30	31	
			, {6020, -1, 3, 0}, {6020, 0, 3, 0}, {6020, 1, 3, 0}// 32	33	34	
			, {6021, 2, -1, 0}, {6021, 2, 0, 0}, {6021, 2, 1, 0}// 35	36	37	
			, {6021, 2, 2, 0}, {6018, -1, -2, 0}, {6018, 0, -2, 0}// 38	39	40	
			, {6018, 1, -2, 0}// 41	
		};

		[ Constructable ]
		public SmokeweedPatchAddon()
		{

           	for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
                AddComponent( new AddonComponent( m_AddOnSimpleComponents[i,0] ), m_AddOnSimpleComponents[i,1], m_AddOnSimpleComponents[i,2], m_AddOnSimpleComponents[i,3] );


			AddComplexComponent( (BaseAddon) this, 0x4794, 0, 0, 2, 0, -1, "", 1);// 24
			AddComplexComponent( (BaseAddon) this, 0x4792, 0, 1, 2, 0, -1, "", 1);// 25
			AddComplexComponent( (BaseAddon) this, 0x4794, 0, 2, 2, 0, -1, "", 1);// 26
			AddComplexComponent( (BaseAddon) this, 0x4792, 0, -1, 0, 0, -1, "", 1);// 27

		}

		public SmokeweedPatchAddon( Serial serial ) : base( serial )
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

	public class SmokeweedPatchAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon { get { return new SmokeweedPatchAddon(); } }

		[Constructable]
		public SmokeweedPatchAddonDeed() : base()
		{
			Name = "Patch of Smokeweed";
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			list.Add( "Grows Smokeweed" );
		}

		public SmokeweedPatchAddonDeed( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}

}
