using System;
using Server.Targeting;
using Server.Network;
using Server.Regions;
using Server.Items;

namespace Server.Spells.Syth
{
    public class Psychokinesis : SythSpell
	{
		public override int spellIndex { get { return 270; } }
		public int CirclePower = 1;
		public static int spellID = 270;
		public override int RequiredTithing{ get{ return Int32.Parse(  Server.Spells.Syth.SythSpell.SpellInfo( spellIndex, 10 )); } }
		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 0.5 ); } }
		public override double RequiredSkill{ get{ return (double)(Int32.Parse( Server.Spells.Syth.SythSpell.SpellInfo( spellIndex, 2 ))); } }
		public override int RequiredMana{ get{ return Int32.Parse(  Server.Spells.Syth.SythSpell.SpellInfo( spellIndex, 3 )); } }

		private static SpellInfo m_Info = new SpellInfo(
				Server.Spells.Syth.SythSpell.SpellInfo( spellID, 1 ),
				Server.Misc.Research.CapsCast( Server.Spells.Syth.SythSpell.SpellInfo( spellID, 4 ) ),
				203,
				0
			);

        public Psychokinesis(Mobile caster, Item scroll) : base(caster, scroll, m_Info)
        {
        }

        public override void OnCast()
        {
            this.Caster.Target = new InternalTarget(this);
        }

        public void Target(ITelekinesisable obj)
        {
            if (this.CheckSequence())
            {
                SpellHelper.Turn(this.Caster, obj);

                obj.OnTelekinesis(this.Caster);
            }

            this.FinishSequence();
        }

        public void Target(Container item)
        {
            if (this.CheckSequence())
            {
                SpellHelper.Turn(this.Caster, item);

                object root = item.RootParent;

                if (!item.IsAccessibleTo(this.Caster))
                {
                    item.OnDoubleClickNotAccessible(this.Caster);
                }
                else if (!item.CheckItemUse(this.Caster, item))
                {
                }
                else if (root != null && root is Mobile && root != this.Caster)
                {
                    item.OnSnoop(this.Caster);
                }
                else if (item is Corpse && !((Corpse)item).CheckLoot(this.Caster, null))
                {
                }
                else if ( this.Caster.Region.OnDoubleClick(this.Caster, item) && CheckFizzle() )
                {
                    Effects.SendLocationParticles(EffectItem.Create(item.Location, item.Map, EffectItem.DefaultDuration), 0x376A, 9, 32, 0, 0, 5022, 0);
                    Effects.PlaySound(item.Location, item.Map, 0x1F5);

                    item.OnItemUsed(this.Caster, item);
                }
            }

            this.FinishSequence();
        }

#region Grab
        public void Target(Item item)
        {
            if (this.CheckSequence())
            {
                SpellHelper.Turn(this.Caster, item);
                object root = item.RootParent;

				if (item.Movable == false){ Caster.SendMessage( "That item does not seem to move." ); }
				else if (item.Amount > 1){ Caster.SendMessage( "There are too many items stacked here to move." ); }
				else if (item.Weight > (Caster.Int / 20)){ Caster.SendMessage( "That is to heavy to move." ); }
				else if (item.RootParentEntity != null){ Caster.SendMessage( "You can not move objects that are inside of other objects or being worn." ); }
				else
				{
					Effects.SendLocationParticles(EffectItem.Create(item.Location, item.Map, EffectItem.DefaultDuration), 0x376A, 9, 32, 0, 0, 5022, 0);
					Effects.PlaySound(item.Location, item.Map, 0x1F5);
					Caster.AddToBackpack( item );
					Caster.SendMessage( "You move the object to within your grasp and place it in your backpack."); 
				}
			}
            this.FinishSequence();
        }
#endregion

        public class InternalTarget : Target
        {
            private readonly Psychokinesis m_Owner;
            public InternalTarget(Psychokinesis owner) : base(Core.ML ? 10 : 12, false, TargetFlags.None)
            {
                this.m_Owner = owner;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is ITelekinesisable)
                    this.m_Owner.Target((ITelekinesisable)o);
                else if (o is Container)
                    this.m_Owner.Target((Container)o);
                    else if (o is Item)
                    this.m_Owner.Target((Item)o);
                else
					from.SendMessage( "This power will not work on that!"); 
            }

            protected override void OnTargetFinish(Mobile from)
            {
                this.m_Owner.FinishSequence();
            }
        }
    }
}