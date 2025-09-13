
using System;
using Server;
using Server.Network;
using Server.Items;
using Server.Mobiles;

namespace Server.Items
{

	public class PhilosophersStone : MagicTalisman
    {
        private int mSymbiosisPoints;//Stone will only evolve to 50%
        [CommandProperty(AccessLevel.GameMaster)]
        public int SymbiosisPoints { get { return mSymbiosisPoints; } set { mSymbiosisPoints = value; } }

		public int BoundToSoul = 0;// Start binding value as zero.

		[Constructable]
		public PhilosophersStone()
		{
            ItemID = 0x2C96;
            Weight = 7.0;
			Name = "Philosopher's Stone";
			Resource = CraftResource.None;//Resource None so the Stones name shows correct once Bound.
			
			BoundToSoul = 0; 
			// Create item with value at zero. Will show in [props as ParentEntity and RootParentEntitty as null.
			
		}
		
		public override bool OnEquip( Mobile from ) 
		{ 
			if(BoundToSoul == 0) //Check to see if bound to a serial.
			{ 
      			BoundToSoul = from.Serial; //Bind to a serial on first time equiped.
                this.Name = from.Name.ToString() + "'s Philosopher's Stone";//Change item name and add who it is bound to. "Player's Soul Stone"
      			from.Emote( "*" + from.Name + " feels the stone expanding their understanding...is it...alive?*" ); 
				base.OnEquip( from ); 
				return true;//Allow it to bind to the first player to equip it after creation.
							//Will show in [props as ParentEntity and RootParentEntitty as [m] Serial, "Player Name"
      		} 
           	else if(BoundToSoul == from.Serial) //Check to see if Stone is bound to who is equiping it.
      		{
				base.OnEquip( from );
				return true; //Allow player who had bound to Stone to equip it.
      		} 
      		else 
      		{ 
      			from.SendMessage( "The Stone refuses your soul" ); 
				return false; //Disallow any one else from equiping the Stone.
			} 
		}
		
		public override void AddNameProperty(ObjectPropertyList list)
        {
			base.AddNameProperty( list );
			if(BoundToSoul == 0) //Check to see if bound to a serial.
			{ 
				list.Add( "<BASEFONT COLOR=#669966>"/*Green*/ + "[Awaiting Host]" + "<BASEFONT COLOR=#FFFFFF>"/*Back to White*/ );
      		}
			else if (BoundToSoul >= 0)//Once the Stone is bound it will show the Symbiosis Points.
			{// \n puts the stuff after it on a new line
				list.Add( "<BASEFONT COLOR=#669966>"/*Green*/ + "[Bonded to Host]\n" + "Symbiosis Points: " + mSymbiosisPoints.ToString() + "<BASEFONT COLOR=#FFFFFF>"/*Back to White*/ );
			}
        }

        //Points are awarded in AOS.cs on dealing any damage
        public void ApplyGain()
        {
            int expr;
            if (mSymbiosisPoints < 5001)//5001 restricts evolving to 50% and 10001 is 100%
            {
                mSymbiosisPoints++;

                if ((mSymbiosisPoints / 100) > 0)
                {
                    expr = mSymbiosisPoints / 100;

                    this.Attributes.EnhancePotions = expr;
                    this.Attributes.LowerRegCost = expr;
                }

                if ((mSymbiosisPoints / 200) > 0)
                {
                    expr = mSymbiosisPoints / 200;

                  this.Attributes.AttackChance = expr;
                  this.Attributes.DefendChance = expr;
                }


                if ((mSymbiosisPoints / 2000) > 0)
                {
                    expr = mSymbiosisPoints / 1000;

                    this.Attributes.RegenHits = expr;
                    this.Attributes.RegenStam = expr;
                }
                InvalidateProperties();

            }
        }
		
        public override bool CanEquip( Mobile from )
        {
            if ( from.Skills[SkillName.Alchemy].Base <= 90.0 )
			{
				from.SendMessage( "You are not skilled enough to equip that." );
                return false;
			}
            else
            {
                return base.CanEquip( from );
            }
        }	
		
		public PhilosophersStone( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			writer.Write( (int) mSymbiosisPoints );//Serialize(Save) how many points the Stone has.
         	writer.Write( (int) BoundToSoul );//Serialize who it is bound to.
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			mSymbiosisPoints = reader.ReadInt();//Read on startup how many Stone the Stone has.
         	BoundToSoul = reader.ReadInt();//Read on startup who it is bound to.
		}
	}
}