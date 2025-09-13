using System;
using Server;
using Server.Spells.Second;
using Server.Spells.Third;
using Server.Spells.Fourth;
using Server.Spells.Sixth;
using Server.Spells.Seventh;


namespace Server.Items
{
	public class PrismaticShard : SoulShard
	{
		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		public override int LabelNumber{ get{ return 1061208; } } // prismatic shard	

		[Constructable]
		public PrismaticShard() : this( 1 )
		{
		}

		[Constructable]
		public PrismaticShard( int amount ) : base(amount)
		{
			
			SuccessfulCast = false;
			MaxCharges = 10;
			Stackable = false;
			Amount = amount;
			Light = LightType.Circle150;
			ItemID = 0x023B;
			Hue = 1992;
		}

		public PrismaticShard( Serial serial ) : base( serial )
		{
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			list.Add( 1060658, "{0}\t{1}", "Spell charges", Charges + "/" + MaxCharges  );  // ~1_val~: ~2_val~
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( IsChildOf( from.Backpack ))
			{
				if (HasCharges(from)) {
					Random random = new Random();
					int spellControl = random.Next(1,5);
					bool spellCompleted = false;
					switch(spellControl) {
						case 1:
							FlameStrikeSpell flameStrikeSpell = new FlameStrikeSpell( from, this );
							spellCompleted = flameStrikeSpell.Cast();
						break;
						case 2:
							EnergyBoltSpell energyBoltSpell = new EnergyBoltSpell( from, this );
							spellCompleted = energyBoltSpell.Cast();
						break;
						case 3:
							LightningSpell lightningSpell = new LightningSpell( from, this );
							spellCompleted = lightningSpell.Cast();
						break;
						case 4:
							PoisonSpell poisonSpell = new PoisonSpell( from, this );
							spellCompleted = poisonSpell.Cast();
						break;
						case 5:
							HarmSpell HarmSpell = new HarmSpell( from, this );
							spellCompleted = HarmSpell.Cast();
						break;
					}
					if (spellCompleted && SuccessfulCast) {
						RemoveCharge();
						SuccessfulCast = false;
					}	
				}
			}
			else {
				from.SendLocalizedMessage( 1045158 ); //  You must have the item in your backpack to target it.
			}
		}
	}
}