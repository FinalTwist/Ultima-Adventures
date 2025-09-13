using System;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.ContextMenus;
using Server.Gumps;
using Server.Misc;
using Server.Network;
using Server.Spells;
using System.Collections.Generic;

namespace Server.Mobiles
{
	[CorpseName( "kraki the weaponsmith corpse" )]
	public class VikingWeaponSmith : Mobile
	{
                public virtual bool IsInvulnerable{ get{ return true; } }
		[Constructable]
		public VikingWeaponSmith()
		{
			Name = "Kraki The Weaponsmith";
                        Title = "*Forges Strong Weapons*";
			Body = 400;
			Hue = Utility.RandomSkinHue();
                   Blessed = true;


			      Item Boots  = new Item(5899);
                        Boots.Hue = 1311;
                        Boots.Name = "Viking Weaponsmith Boots";
                        Boots.LootType = LootType.Blessed;
                        Boots.Layer = Layer.Shoes;
                        AddItem(Boots);

                        Item FancyShirt = new Item(7933);
                        FancyShirt.Hue = 2213;
                        FancyShirt.Name = "Viking Weaponsmith Shirt";
                        FancyShirt.Layer = Layer.Shirt;
                        FancyShirt.LootType = LootType.Blessed;
                        AddItem(FancyShirt);

                        LongPants p = new LongPants();
                        p.Hue = 1311;
                        p.Name = "Viking Weaponsmith Pants";
                        AddItem( p );

                       this.FacialHairItemID = 0x204B;
                       this.FacialHairHue = 1281;

                       
	               this.HairItemID = 0x203D;
                     this.HairHue = 1281;

                        
                       

                        SmithHammer weapon = new SmithHammer();
                        weapon.Hue = 2101;
                        weapon.Movable = false;
                        AddItem(weapon);
			
		}

		public VikingWeaponSmith( Serial serial ) : base( serial )
		{
		}

        public override void GetContextMenuEntries(Mobile from, List<ContextMenuEntry> list) 
	        { 
	                base.GetContextMenuEntries( from, list ); 
        	        list.Add( new VikingWeaponsmithEntry( from, this ) ); 
	        } 

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		public class VikingWeaponsmithEntry : ContextMenuEntry
		{
			private Mobile m_Mobile;
			private Mobile m_Giver;
			
			public VikingWeaponsmithEntry( Mobile from, Mobile giver ) : base( 6146, 3 )
			{
				m_Mobile = from;
				m_Giver = giver;
			}

			public override void OnClick()
			{
				

                          if( !( m_Mobile is PlayerMobile ) )
					return;
				
				PlayerMobile mobile = (PlayerMobile) m_Mobile;

				{
					if ( ! mobile.HasGump( typeof( VikingWeaponsmithGump ) ) )
					{
						mobile.SendGump( new VikingWeaponsmithGump( mobile ));
//
						
					} 
				}
			}
		}

		public override bool OnDragDrop( Mobile from, Item dropped )
		{          		
         	        Mobile m = from;
			PlayerMobile mobile = m as PlayerMobile;

			if ( mobile != null)
			{
				if( dropped is Steel )
         		{
         			if(dropped.Amount!=1)
         			{
					this.PrivateOverheadMessage( MessageType.Regular, 1153, false, "my great steel!", mobile.NetState );
         				return false;
         			}

					dropped.Delete(); 
										
					mobile.AddToBackpack( new WarmWool() );
                    
					mobile.SendMessage( "The Jacket is now yours,bring it back to Viking Raider!" );
					

				
					return true;
         		}
				else if ( dropped is Steel )
				{
				this.PrivateOverheadMessage( MessageType.Regular, 1153, 1054071, mobile.NetState );
         			return false;
				}
         		else
         		{
					this.PrivateOverheadMessage( MessageType.Regular, 1153, false, "Why on earth would I want to have that?", mobile.NetState );
     			}
			}
			return false;
		}
	}
}
