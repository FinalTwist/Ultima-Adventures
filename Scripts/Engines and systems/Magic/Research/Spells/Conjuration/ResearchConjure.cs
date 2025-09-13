using System;
using Server.Targeting;
using Server.Network;
using Server;
using Server.Items;

namespace Server.Spells.Research
{
	public class ResearchConjure : ResearchSpell
	{
		public override int spellIndex { get { return 1; } }
		public int CirclePower = 1;
		public static int spellID = 1;
		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 2.0 ); } }
		public override double RequiredSkill{ get{ return (double)(Int32.Parse( Server.Misc.Research.SpellInformation( spellIndex, 8 ))); } }
		public override int RequiredMana{ get{ return Int32.Parse( Server.Misc.Research.SpellInformation( spellIndex, 7 )); } }

		private static SpellInfo m_Info = new SpellInfo(
				Server.Misc.Research.SpellInformation( spellID, 2 ),
				Server.Misc.Research.CapsCast( Server.Misc.Research.SpellInformation( spellID, 4 ) ),
				203,
				9041
			);

		public ResearchConjure( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
			if ( CheckSequence() )
			{
				Item item = new Dagger();
				string msg = "You conjure a dagger.";

				switch( Utility.RandomMinMax( 1, 28 ) )
				{
					case 1: item = new Apple(); item.Amount = Utility.RandomMinMax( 1, 5 ); msg = "You conjure some apples."; break;
					case 2: item = new Arrow(); item.Amount = Utility.RandomMinMax( 1, 10 ); msg = "You conjure some arrows."; break;
					case 3: item = new Backpack(); msg = "You conjure a backpack."; break;
					case 4: item = new Bag(); msg = "You conjure a bag."; break;
					case 5: item = new Bandage(); item.Amount = Utility.RandomMinMax( 1, 10 ); msg = "You conjure some bandages."; break;
					case 6: item = new Bedroll(); msg = "You conjure a bedroll."; break;
					case 7: item = new Beeswax(); msg = "You conjure some beeswax."; break;
					case 8: item = new BlueBook(); msg = "You conjure a book."; break;
					case 9: item = new Bolt(); item.Amount = Utility.RandomMinMax( 1, 10 ); msg = "You conjure some crossbow bolts."; break;
					case 10: item = new Bottle(); msg = "You conjure a bottle."; break;
					case 11: item = new BreadLoaf(); item.Amount = Utility.RandomMinMax( 1, 5 ); msg = "You conjure some bread."; break;
					case 12: item = new Candle(); msg = "You conjure a candle."; break;
					case 13: item = new Club(); msg = "You conjure a club."; break;
					case 14: item = new Dagger(); msg = "You conjure a dagger."; break;
					case 15: item = new DarkYarn(); item.Amount = Utility.RandomMinMax( 1, 5 ); msg = "You conjure some yarn."; break;
					case 16: item = new FloppyHat(); msg = "You conjure a hat."; break;
					case 17: item = new Jar(); msg = "You conjure a jar."; break;
					case 18: item = new Kindling(); item.Amount = Utility.RandomMinMax( 1, 5 ); msg = "You conjure some kindling."; break;
					case 19: item = new Lantern(); msg = "You conjure a lantern."; break;
					case 20: item = new Lockpick(); msg = "You conjure a lockpick."; break;
					case 21: item = new OilCloth(); msg = "You conjure an oil cloth."; break;
					case 22: item = new Pouch(); msg = "You conjure a pouch."; break;
					case 23: item = new Robe(); msg = "You conjure a robe."; break;
					case 24: item = new Shoes(); msg = "You conjure some shoes."; break;
					case 25: item = new SpoolOfThread(); item.Amount = Utility.RandomMinMax( 1, 5 ); msg = "You conjure some thread."; break;
					case 26: item = new TenFootPole(); msg = "You conjure a ten foot pole."; break;
					case 27: item = new Torch(); msg = "You conjure a torch."; break;
					case 28: item = new Waterskin(); msg = "You conjure a waterskin."; break;
				}

				Caster.SendMessage( msg );

				Caster.AddToBackpack( item );

				Caster.FixedParticles( 0, 10, 5, 2003, Server.Items.CharacterDatabase.GetMySpellHue( Caster, 0 ), 0, EffectLayer.RightHand );
				Caster.PlaySound( 0x1E2 );

				Server.Misc.Research.ConsumeScroll( Caster, true, spellIndex, true );
			}

			FinishSequence();
		}
	}
}
