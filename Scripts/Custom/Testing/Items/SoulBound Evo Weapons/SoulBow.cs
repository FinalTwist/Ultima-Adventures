/*----------------*/
/*--- Scripted ---*/
/*--- By: JBob ---*/
/*----------------*/
using System;
using Server;
using Server.Network;
using Server.Items;
using Server.Mobiles;

namespace Server.Items
{
	[FlipableAttribute( 0x13B2, 0x13B1 )]
	public class SoulBow : BaseRanged
	{
        private int mEvolutionPoints;//Bow will only evolve to 50%
        [CommandProperty(AccessLevel.GameMaster)]
        public int EvolutionPoints { get { return mEvolutionPoints; } set { mEvolutionPoints = value; } }

		public int BoundToSoul = 0;// Start binding value as zero.
		
		public override int EffectID{ get{ return 0xF42; } }
		public override Type AmmoType{ get{ return typeof( Arrow ); } }
		public override Item Ammo{ get{ return new Arrow(); } }

		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.ParalyzingBlow; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.MortalStrike; } }

		public override int AosStrengthReq{ get{ return 30; } }
		public override int AosMinDamage{ get{ return Core.ML ? 15 : 16; } }
		public override int AosMaxDamage{ get{ return Core.ML ? 19 : 18; } }
		public override int AosSpeed{ get{ return 25; } }
		public override float MlSpeed{ get{ return 4.25f; } }

		public override int OldStrengthReq{ get{ return 20; } }
		public override int OldMinDamage{ get{ return 9; } }
		public override int OldMaxDamage{ get{ return 41; } }
		public override int OldSpeed{ get{ return 20; } }

		public override int DefMaxRange{ get{ return 10; } }

		public override WeaponAnimation DefAnimation{ get{ return WeaponAnimation.ShootBow; } }

		public override int InitMinHits{ get{ return Utility.RandomMinMax(100, 125); } }
		public override int InitMaxHits{ get{ return Utility.RandomMinMax(126, 150); } }

		[Constructable]
		public SoulBow() : base( 0x13B2 )
		{
			Weight = 7.0;
			Name = "Soul Bow";
			Resource = CraftResource.None;//Resource None so the Bows name shows correct once Bound.
			Layer = Layer.TwoHanded;
			
			BoundToSoul = 0; 
			// Create item with value at zero. Will show in [props as ParentEntity and RootParentEntitty as null.
			
            Attributes.Luck = 100;
            WeaponAttributes.SelfRepair = 100;

            Attributes.WeaponDamage = 1;
            Attributes.WeaponSpeed = 10;
            Attributes.SpellChanneling = 1;
		}
		
		public override bool OnEquip( Mobile from ) 
		{ 
			if(BoundToSoul == 0) //Check to see if bound to a serial.
			{ 
      			BoundToSoul = from.Serial; //Bind to a serial on first time equiped.
                this.Name = from.Name.ToString() + "'s Soul Bow";//Change item name and add who it is bound to. "Player's Soul Bow"
      			from.Emote( "*" + from.Name + " feels a weird energy overwhelming their body*" ); 
				base.OnEquip( from ); 
				return true;//Allow it to bind to the first player to equip it after creation.
							//Will show in [props as ParentEntity and RootParentEntitty as [m] Serial, "Player Name"
      		} 
           	else if(BoundToSoul == from.Serial) //Check to see if Bow is bound to who is equiping it.
      		{
				base.OnEquip( from );
				return true; //Allow player who had bound to Bow to equip it.
      		} 
      		else 
      		{ 
      			from.SendMessage( "The Bow refuses your soul" ); 
				return false; //Disallow any one else from equiping the Bow.
			} 
		}
		
		public override void AddNameProperty(ObjectPropertyList list)
        {
			base.AddNameProperty( list );
			if(BoundToSoul == 0) //Check to see if bound to a serial.
			{ 
				list.Add( "<BASEFONT COLOR=#669966>"/*Green*/ + "[Un-Bound]" + "<BASEFONT COLOR=#FFFFFF>"/*Back to White*/ );
      		}
			else if (BoundToSoul >= 0)//Once the Bow is bound it will show the Evolution Points.
			{// \n puts the stuff after it on a new line
				list.Add( "<BASEFONT COLOR=#669966>"/*Green*/ + "[Soulbound]\n" + "Evolution Points: " + mEvolutionPoints.ToString() + "<BASEFONT COLOR=#FFFFFF>"/*Back to White*/ );
			}
        }
		/*When weapon hits this gives a chance to gain Evolution Points*/
        public override void OnHit(Mobile attacker, Mobile defender, double Damagebonus)
        {
            if (Utility.Random(2) == 1)
            {
                ApplyGain();
            }
            base.OnHit(attacker, defender,Damagebonus);
        }

        public void ApplyGain()
        {
            int expr;
            if (mEvolutionPoints < 5001)//5001 restricts evolving to 50% and 10001 is 100%
            {
                mEvolutionPoints++;

                if ((mEvolutionPoints / 100) > 0)
                {
                    expr = mEvolutionPoints / 100;

                    this.WeaponAttributes.HitHarm = expr;
                    this.WeaponAttributes.HitMagicArrow = expr;
                }

                if ((mEvolutionPoints / 200) > 0)
                {
                    expr = mEvolutionPoints / 100;

                    this.WeaponAttributes.HitLightning = expr;
                    this.WeaponAttributes.HitFireball = expr;
                    this.Attributes.WeaponDamage = expr;
                }

                if ((25 + (mEvolutionPoints / 200)) > 0) this.Attributes.WeaponSpeed = (25 + (mEvolutionPoints / 200));

                if ((mEvolutionPoints / 2000) > 0)
                {
                    expr = mEvolutionPoints / 2000;

                    this.Attributes.CastRecovery = expr;
                    this.Attributes.CastSpeed = expr;
                }
                InvalidateProperties();

            }
        }
		
        public override bool CanEquip( Mobile from )
        {
            if ( from.Skills[SkillName.Archery].Base <= 75.0 )
			{
				from.SendMessage( "You are not skilled enough to equip that." );
                return false;
			}
            else
            {
                return base.CanEquip( from );
            }
        }	
		
		public SoulBow( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			writer.Write( (int) mEvolutionPoints );//Serialize(Save) how many points the Bow has.
         	writer.Write( (int) BoundToSoul );//Serialize who it is bound to.
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			mEvolutionPoints = reader.ReadInt();//Read on startup how many points the Bow has.
         	BoundToSoul = reader.ReadInt();//Read on startup who it is bound to.
		}
	}
}