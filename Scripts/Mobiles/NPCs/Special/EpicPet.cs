using System;
using System.Collections;
using Server.Items;
using Server.ContextMenus;
using Server.Misc;
using Server.Network;

namespace Server.Mobiles
{
	public class EpicPet : BasePerson
	{
		public override bool InitialInnocent{ get{ return true; } }
		[Constructable]
		public EpicPet () : base( )
		{
			Name = "an enslaved demon";
			Body = 9;
			BaseSoundID = 357;
			CantWalk = true;
			Direction = Direction.East;
			NameHue = 1154;
			AI = AIType.AI_Citizen;
			FightMode = FightMode.None;
		}

		public override void OnAfterSpawn()
		{
			base.OnAfterSpawn();
			Server.Misc.MorphingTime.CheckMorph( this );
			Name = "an enslaved demon";
			Body = 9;
			BaseSoundID = 357;
			CantWalk = true;
			Direction = Direction.East;
		}

		public override bool OnBeforeDeath()
		{
			Say("In Vas Mani");
			this.Hits = this.HitsMax;
			this.FixedParticles( 0x376A, 9, 32, 5030, EffectLayer.Waist );
			this.PlaySound( 0x202 );
			return false;
		}

		public override bool IsEnemy( Mobile m )
		{
			return false;
		}

		public EpicPet( Serial serial ) : base( serial )
		{
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
			Blessed = true;
			Body = 9;
			BaseSoundID = 357;
			CantWalk = true;
			Direction = Direction.East;
		}
	}
}