using System;
using System.Collections;
using Server;
using Server.Items;
using System.Collections.Generic;
using Server.Gumps;
using Server.Mobiles;
using System.Linq;

namespace Server.Engines.BulkOrders
{
	[TypeAlias( "Scripts.Engines.BulkOrders.LargeBOD" )]
	public abstract class LargeBOD : Item
	{
		private int m_AmountMax;
		private bool m_RequireExceptional;
		private BulkMaterialType m_Material;
		private LargeBulkEntry[] m_Entries;

		[CommandProperty( AccessLevel.GameMaster )]
		public int AmountMax{ get{ return m_AmountMax; } set{ m_AmountMax = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public bool RequireExceptional{ get{ return m_RequireExceptional; } set{ m_RequireExceptional = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public BulkMaterialType Material{ get{ return m_Material; } set{ m_Material = value; InvalidateProperties(); } }

		public LargeBulkEntry[] Entries{ get{ return m_Entries; } set{ m_Entries = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public bool Complete
		{
			get
			{
				for ( int i = 0; i < m_Entries.Length; ++i )
				{
					if ( m_Entries[i].Amount < m_AmountMax )
						return false;
				}

				return true;
			}
		}

		public abstract List<Item> ComputeRewards( bool full );
		public abstract int ComputeGold();
		public abstract int ComputeFame();

		public virtual void GetRewards( out Item reward, out int gold, out int fame )
		{
			reward = null;
			gold = ComputeGold();
			fame = ComputeFame();

			List<Item> rewards = ComputeRewards( false );

			if ( rewards.Count > 0 )
			{
				reward = rewards[Utility.Random( rewards.Count )];

				for ( int i = 0; i < rewards.Count; ++i )
				{
					if ( rewards[i] != reward )
						rewards[i].Delete();
				}
			}
		}

		public static BulkMaterialType GetRandomMaterial( BulkMaterialType startType, BulkMaterialType endType )
		{
			if (startType == endType) { return startType; }

			int start = (int)startType;
			int end = (int)endType;
			
			return (BulkMaterialType)(start + Utility.Random(1 + end - start));
		}

		public override int LabelNumber{ get{ return 1045151; } } // a bulk order deed

		public LargeBOD( int hue, int amountMax, bool requireExeptional, BulkMaterialType material, LargeBulkEntry[] entries ) : base( Core.AOS ? 0x2258 : 0x14EF )
		{
			Weight = 1.0;
			Hue = hue; // Blacksmith: 0x44E; Tailoring: 0x483
			//LootType = LootType.Blessed;

			m_AmountMax = amountMax;
			m_RequireExceptional = requireExeptional;
			m_Material = material;
			m_Entries = entries;
		}

		public LargeBOD() : base( Core.AOS ? 0x2258 : 0x14EF )
		{
			Weight = 1.0;
			//LootType = LootType.Blessed;
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			list.Add( 1060655 ); // large bulk order

			if ( m_RequireExceptional )
				list.Add( 1045141 ); // All items must be exceptional.

			if ( m_Material != BulkMaterialType.None )
                list.Add("All items must be crafted with " + SmallBODGump.GetMaterialStringFor(m_Material)); // All items must be made with x material.

			list.Add( 1060656, m_AmountMax.ToString() ); // amount to make: ~1_val~

			for ( int i = 0; i < m_Entries.Length; ++i )
				list.Add( 1060658 + i, "#{0}\t{1}", m_Entries[i].Details.Number, m_Entries[i].Amount ); // ~1_val~: ~2_val~
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( IsChildOf( from.Backpack ) )
			{
				
					from.SendGump( new LargeBODGump( from, this ) );
				

			}
			else
				from.SendLocalizedMessage( 1045156 ); // You must have the deed in your backpack to use it.
		}

		public void BeginCombine( Mobile from )
		{
			if ( !Complete )
				from.Target = new LargeBODTarget( this );
			else
				from.SendLocalizedMessage( 1045166 ); // The maximum amount of requested items have already been combined to this deed.
		}

		public void EndCombine( Mobile from, object o )
		{
			if ( o is Item && ((Item)o).IsChildOf( from.Backpack ) )
			{
                Type objectType = o.GetType();
                LargeBulkEntry entry = m_Entries.FirstOrDefault(e => e.Details.Type == objectType);
                if (entry != null)
                {
                    if (entry.Amount + 1 > m_AmountMax)
                    {
                        from.SendLocalizedMessage(1045166); // The maximum amount of requested items have already been combined to this deed.
                    }
                    else
                    {
                        BulkMaterialType material = BulkMaterialType.None;

                        if (o is BaseArmor)
                            material = SmallBOD.GetMaterial(((BaseArmor)o).Resource);
                        else if (o is BaseClothing)
                            material = SmallBOD.GetMaterial(((BaseClothing)o).Resource);
						else if (o is BaseWeapon)
							material = SmallBOD.GetMaterial(((BaseWeapon)o).Resource);
						else if (o is BaseInstrument)
							material = SmallBOD.GetMaterial(((BaseInstrument)o).Resource);

                        if (m_Material >= BulkMaterialType.DullCopper && m_Material <= BulkMaterialType.Dwarven && material != m_Material)
                        {
                            from.SendLocalizedMessage(1045168); // The item is not made from the requested ore.
                        }
                        else if (m_Material >= BulkMaterialType.Horned && m_Material <= BulkMaterialType.Alien && material != m_Material)
                        {
                            from.SendLocalizedMessage(1049352); // The item is not made from the requested leather type.
                        }
                        else if (m_Material >= BulkMaterialType.Ash && m_Material <= BulkMaterialType.Elven && material != m_Material)
                        {
                            from.SendMessage("The item is not made from the requested wood type.");
                        }
                        else
                        {
                            bool isExceptional = false;

                            if (o is BaseWeapon)
                                isExceptional = (((BaseWeapon)o).Quality == WeaponQuality.Exceptional);
                            else if (o is BaseArmor)
                                isExceptional = (((BaseArmor)o).Quality == ArmorQuality.Exceptional);
                            else if (o is BaseClothing)
                                isExceptional = (((BaseClothing)o).Quality == ClothingQuality.Exceptional);
                            else if (o is BaseInstrument)
                                isExceptional = (((BaseInstrument)o).Quality == InstrumentQuality.Exceptional);

                            if (m_RequireExceptional && !isExceptional)
                            {
                                from.SendLocalizedMessage(1045167); // The item must be exceptional.
                            }
                            else
                            {
                                ((Item)o).Delete();
                                entry.Amount++;

                                from.SendLocalizedMessage(1045170); // The item has been combined with the deed.

                                from.SendGump(new LargeBODGump(from, this));

                                if (m_Entries.Any(e => e.Amount < m_AmountMax))
                                    BeginCombine(from);
                            }
                        }
                    }
                }
                else
                {
                    from.SendLocalizedMessage(1045169); // The item is not in the request.
                }
            }
			else
			{
				from.SendLocalizedMessage( 1045158 ); // You must have the item in your backpack to target it.
			}
		}

		public LargeBOD( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write( m_AmountMax );
			writer.Write( m_RequireExceptional );
			writer.Write( (int) m_Material );

			writer.Write( (int) m_Entries.Length );

			for ( int i = 0; i < m_Entries.Length; ++i )
				m_Entries[i].Serialize( writer );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 0:
				{
					m_AmountMax = reader.ReadInt();
					m_RequireExceptional = reader.ReadBool();
					m_Material = (BulkMaterialType)reader.ReadInt();

					m_Entries = new LargeBulkEntry[reader.ReadInt()];

					for ( int i = 0; i < m_Entries.Length; ++i )
						m_Entries[i] = new LargeBulkEntry( this, reader );

					break;
				}
			}

			if ( Weight == 0.0 )
				Weight = 1.0;

			if ( Core.AOS && ItemID == 0x14EF )
				ItemID = 0x2258;

			if ( Parent == null && Map == Map.Internal && Location == Point3D.Zero )
				Delete();
		}
	}
}