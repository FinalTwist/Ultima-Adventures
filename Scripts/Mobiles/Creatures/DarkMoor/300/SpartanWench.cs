using System;
using Server;
using System.Collections;
using Server.Items;
using Server.ContextMenus;
using Server.Misc;
using Server.Network;
using System.Collections.Generic;

namespace Server.Mobiles
{
	[CorpseName( "a spartan corpse" )] 
	public class SpartanWench : BaseCreature
	{
        private DateTime m_NextPickup;
        public override bool ShowFameTitle{ get{ return false; } }

		[Constructable]
		public SpartanWench() : base( AIType.AI_Melee, FightMode.Aggressor, 10, 1, 0.2, 0.2 )
        {
			SpeechHue = Utility.RandomDyedHue();
			Name = NameList.RandomName( "female" );
			Hue = Utility.RandomSkinHue();
            Title = "the Spartan Wench";

			Body = 0x193;

			SetStr( 86, 100 );
			SetDex( 81, 100 );
			SetInt( 72, 85 );

			SetDamage( 18, 26 );
			
			SetHits ( 100, 152);

			SetSkill( SkillName.Fencing, 95.0, 100.0 );
			SetSkill( SkillName.Swords, 92.0, 100.0 );
			SetSkill( SkillName.Tactics, 90.0, 100.0 );
			SetSkill( SkillName.Wrestling, 94.3, 100.0 );
            SetSkill(SkillName.Chivalry, 94.3, 100.0);

            SetResistance( ResistanceType.Physical, 70, 80 );
			SetResistance( ResistanceType.Fire, 40, 50 );
			SetResistance( ResistanceType.Cold, 80, 90 );
			SetResistance( ResistanceType.Poison, 50, 60 );
			SetResistance( ResistanceType.Energy, 25, 35 );

			Fame = 10000;
			Karma = 10000;

			AddItem( new Cloak( 33 ) );
			AddItem( new BoneArms() );
			AddItem( new BoneLegs() );
			AddItem( new Cleaver() );
			AddItem( new BronzeShield() );
			AddItem( new Sandals() );
            AddItem( new FemaleLeatherChest() );
            


            Utility.AssignRandomHair( this );
		}
        protected override BaseAI ForcedAI
        {
            get
            {
                return new OmniAI(this);
            }
        }

        public override void OnThink()
        {
            base.OnThink();
            if (DateTime.UtcNow < m_NextPickup)
                return;

            m_NextPickup = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(5, 10));

            switch (Utility.RandomMinMax(0, 7))
            {
             
                case 1: Undress(Combatant); break; 
            }
        }

        #region Undress
        private DateTime m_NextUndress;

        public void Undress(Mobile target)
        {
            if (target == null || Deleted || !Alive || m_NextUndress > DateTime.UtcNow || 0.25 < Utility.RandomDouble())
                return;

            if (target.Player && !target.Hidden && CanBeHarmful(target))
            {

                UndressItem(target, Layer.Pants);
                

                target.SendMessage("Her ravishing looks makes your blood race. Your trousers are too confining.");
            }

            m_NextUndress = DateTime.UtcNow + TimeSpan.FromMinutes(1);
        }

        public void UndressItem(Mobile m, Layer layer)
        {
            Item item = m.FindItemOnLayer(layer);

            if (item != null && item.Movable)
                m.PlaceInBackpack(item);
        }
        #endregion
        public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
		}

		public override bool AlwaysMurderer{ get{ return true; } }

		public SpartanWench( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}