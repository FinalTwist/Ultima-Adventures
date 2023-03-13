using System;
using System.Collections.Generic;
using System.Text;
using Server.Mobiles;
using Server.ContextMenus;
using Server;
using Custom.Jerbal.Jako.Gumps;
using Server.Gumps;
using System.Collections;
using Custom.Jerbal.Jako.Breeding;
using Server.Misc;


namespace Custom.Jerbal.Jako.Breeding
{
    class JakoBreeder : AnimalTrainer
    {

        private static ArrayList m_PendingOffers = new ArrayList();
        public static ArrayList PendingOffers { get { return m_PendingOffers; } set { m_PendingOffers = value; } }

        [Constructable]
        public JakoBreeder()
        {
            Title = "the animal breeder";
            CantWalk = true;
        }

        public override void AddCustomContextEntries(Server.Mobile from, List<Server.ContextMenus.ContextMenuEntry> list)
        {
            list.Add(new BreedEntry(this,from));
            base.AddCustomContextEntries(from, list);
        }


        private class BreedEntry : ContextMenuEntry
        {
            private JakoBreeder m_Trainer;
            private PlayerMobile m_From;

            public BreedEntry(JakoBreeder trainer, Mobile from)
                : base(6120, 10)
            {
                if (from is PlayerMobile)
                    m_From = (PlayerMobile)from;
                m_Trainer = trainer;               
            }

            public override void OnClick()
            {
                if (m_From == null)
                    return;
                JakoBreeder.BreedClick(m_From, m_Trainer);
                
            }
        }

        public static void DoBreeding(Mobile from, int DictID)
        {
            BreedingRequest req = (BreedingRequest)m_PendingOffers[DictID];
            if (!req.Accepted && (((BaseCreature)req.Creature1).ControlMaster != ((BaseCreature)req.Creature2).ControlMaster))
            {
                req.Accepted = true;
                from.SendMessage("They have been notified you accepted the offer.");
                if (((BaseCreature)req.Creature2).ControlMaster == from)
                    ((BaseCreature)req.Creature1).ControlMaster.SendMessage("They have accepted the breeding offer.");
                else
                    ((BaseCreature)req.Creature2).ControlMaster.SendMessage("They have accepted the breeding offer.");
                return;
            }
            JakoBreeder.EndBreeding(req);
            m_PendingOffers.RemoveAt(DictID);

        }

        public static void EndBreeding(BreedingRequest request)
        {
            BaseCreature c1 = ((BaseCreature)request.Creature1);
            BaseCreature c2 = ((BaseCreature)request.Creature2);
            PlayerMobile pm1 = (PlayerMobile)c1.ControlMaster;
            PlayerMobile pm2 = (PlayerMobile)c2.ControlMaster;
            int gp1 = JakoBreeder.GoldPrice(request.Creature1, request.Creature2);
            int gp2 = JakoBreeder.GoldPrice(request.Creature1, request.Creature2);

            if (Banker.GetBalance(pm1) < gp1 || Banker.GetBalance(pm2) < gp2)
            {
                pm1.SendMessage("The breeding has been canceled due to insufficient funds.");
                if (pm1 != pm2)
                    pm2.SendMessage("The breeding has been canceled due to insufficient funds.");
                return;
            }

            Banker.Withdraw((Mobile)pm1, gp1);
            Banker.Withdraw((Mobile)pm2, gp2);

            ReadyPetForBreed(c1);
            ReadyPetForBreed(c2);
            
            pm1.AddToBackpack(new BreedingParentTicket(pm1, pm2, c1, c2, DateTime.UtcNow + c1.NextMateIn));
            pm2.AddToBackpack(new BreedingParentTicket(pm2, pm1, c2, c1, DateTime.UtcNow + c2.NextMateIn));

        }

        private static void ReadyPetForBreed(BaseCreature pet)
        {
            pet.ControlTarget = null;
            pet.ControlOrder = OrderType.Stay;
            pet.Internalize();
            pet.SetControlMaster(null);
            pet.Breeding = true;
        }

        private static void ReadyPetForReturn(Mobile owner, Mobile pet)
        {
            if (!(pet is BaseCreature) || pet == null)
            {
                owner.SendMessage("null pet error");
                return;
            }

            BaseCreature bc = pet as BaseCreature;
            bc.NextMate = DateTime.UtcNow + bc.NextMateIn;
            bc.MoveToWorld( owner.Location, owner.Map );
            int slots = bc.ControlSlots;//making sure slots don't change for old pets
            double mintaming = bc.MinTameSkill;
	        bc.SetControlMaster(owner);
            bc.OnAfterSpawn();
            bc.ControlSlots = slots;//restoring
            bc.MinTameSkill = mintaming;//restoring
            bc.ControlOrder = OrderType.Stay;
            bc.Breeding = false;
        }

        public static void CancelBreeding(int DictID)
        {
            BreedingRequest req = (BreedingRequest)m_PendingOffers[DictID];
            m_PendingOffers.RemoveAt(DictID);
            ((BaseCreature)req.Creature1).ControlMaster.SendMessage("The Breeding has been canceled.");
            ((BaseCreature)req.Creature1).ControlMaster.CloseGump(typeof(JakoBreederAcceptGump));

            if (((BaseCreature)req.Creature1).ControlMaster == ((BaseCreature)req.Creature2).ControlMaster)
                return;
			
            ((BaseCreature)req.Creature2).ControlMaster.SendMessage("The Breeding has been canceled.");
            ((BaseCreature)req.Creature2).ControlMaster.CloseGump(typeof(JakoBreederAcceptGump));
        }

        public static void SendMasterOkayGumps(Mobile targeter, BaseCreature bc1, BaseCreature bc2)
        {
            CleanUpPendingOffers();
            int dictID = PendingOffers.Add(new BreedingRequest(bc1,bc2));
            if (bc1.ControlMaster == null && bc2.ControlMaster != null) // one of the pets is stabled likely
                bc2.ControlMaster.SendGump(new JakoBreederAcceptGump(targeter,bc1,bc2,dictID));
            else if (bc1.ControlMaster != null)
                bc1.ControlMaster.SendGump(new JakoBreederAcceptGump(targeter,bc1,bc2,dictID));

            if (bc1.ControlMaster != bc2.ControlMaster && bc2.ControlMaster != null && bc1.ControlMaster != null)
                bc2.ControlMaster.SendGump(new JakoBreederAcceptGump(targeter,bc2,bc1,dictID));
        }

        private static void CleanUpPendingOffers()
        {
            PlayerMobile pm1;
            PlayerMobile pm2;
            foreach (BreedingRequest br in m_PendingOffers)
            {
                pm1 = (PlayerMobile)((BaseCreature)br.Creature1).ControlMaster;
                pm2 = (PlayerMobile)((BaseCreature)br.Creature2).ControlMaster;
                if (pm1.NetState == null || pm2.NetState == null || (!br.Accepted && (!pm1.HasGump(typeof(JakoBreederAcceptGump)) || !pm2.HasGump(typeof(JakoBreederAcceptGump)))))
                    m_PendingOffers.Remove(br);
            }
        }

        public static void BreedClick(PlayerMobile pm, Mobile breeder)
        {

            if (pm.Skills[SkillName.AnimalTaming].Value < 55.0)
            {
                breeder.SayTo(pm, "I refuse to waste my time with someone who is not skilled in taming.");
                return;
            }
            pm.SendGump(new JakoBreederTalkGump(pm, breeder));
        }

        public static string CanBreed(Mobile animal)
        {
            if ( !(animal is BaseCreature) )
                return "what kind of thing is this? Does it have a male and female version?";

            BaseCreature bc;

            if (animal.Deleted)
                return "That has been deleted";

            if (animal is PlayerMobile)
                return String.Format("I don't specialize in that but I know a wench who might be able to help.");

            bc = animal as BaseCreature;
            if (bc == null || !bc.JakoIsEnabled)
                return "You can't breed that!";

            if ( bc.IsParagon)
                return "Nice try, Everyone knows paragons are impotent!";
                
            if (bc is JakoBreeder)
            {
                bc.Emote("scoffs");
                return String.Format("I should {0} you...",(bc.Female?"slap":"hit"));
            }
            if (bc.Body.IsHuman)
                return "You'll need to find an innkeeper for that.";
            if (!bc.Tamable)
                return "I specialize only in tameable pets.";
            if (bc.ControlMaster == null)
                return "That's not tame to anyone!"; //502674
            if (bc.NextMate > DateTime.UtcNow)
                return "That animal is still recovering from breeding.";
            if (bc.IsDeadBondedPet)
                return "Living pets only, please."; //1049668
            if (bc.RealLevel < bc.MatingLevel)
                return "That pet is too young to mate.";
            return null;
        }

        public static string CanBreed(Mobile animal1, Mobile animal2)
        {
            BaseCreature bc1, bc2;
            if (animal1.Deleted || animal2.Deleted)
                return "One of those has been deleted.";
            bc1 = animal1 as BaseCreature;
            bc2 = animal2 as BaseCreature;
            if (bc1 == null || bc2 == null)
                return "Unexpected error.  One of these isn't an animal!"; //shouldn't happen.  Caught before called
            if (bc1 == bc2)
                return (bc1.Female ? "I'm all out of magic juice and haven't been to THAT bank for awhile, sorry." : "I don't think it's long enough, and won't produce the results you want anyways.");
            if (bc1.GetType() != bc2.GetType())
			{
				if ( Insensitive.Contains(bc1.Title, "primeval dragon") && Insensitive.Contains(bc2.Title, " primeval dragon") )
					return null;
				else
					return "That seems complicated.  Would that even be possible? *think*";
			}
            if (bc1.Female == bc2.Female)
                return "I think it would be best to not push your beliefs on these creatures.";
            if (Math.Abs((int)(bc1.RealLevel - bc2.RealLevel)) > 5)
                return "These creatures are to different in level to mate them safely.";
            return null;
        }

        public static int GoldPrice(Mobile creature1, Mobile creature2)
        {
            if (creature1.Deleted || creature2.Deleted)
                return 0;
            double mult =1;
            if (((BaseCreature)creature1).ControlMaster == ((BaseCreature)creature2).ControlMaster)
                mult = 2;
            return (int)(((BaseCreature)creature1).Level * ((BaseCreature)creature2).Level * 50*mult);
        }


        public override bool OnDragDrop(Mobile from, Item dropped)
        {
            if (from is PlayerMobile && from != null && dropped is BreedingParentTicket)
            {
				
                BreedingParentTicket bt = (BreedingParentTicket)dropped;
                if (bt.Creature == null || bt.Creature.Deleted )
                {
                    from.SendMessage("Your pet has been lost forever.  I have added to your bank a level 50 rabbit deed and turned this one worthless.  Please page a GM with details of this error.");
                    bt.NullifyDeed();
                    BaseCreature rab = new Rabbit();
                    rab.MaxLevel = 50;
                    rab.Level = 50;
                    from.BankBox.AddItem(new BreedingParentTicket(bt.Owner, bt.OtherParent, rab, rab, DateTime.MinValue));
                    dropped.Delete();
                    return false;
                }
				if (bt.OtherCreature == null)
					bt.OtherCreature = bt.Creature;
				
                if (bt.Owner != from && (bt.DoneReal == DateTime.MinValue && bt.OtherParent != from))
                {
                    from.SendMessage("This ticket is not yours! You can not claim it!");
                    return false;
                }
                if ( (bt.DoneReal >= DateTime.UtcNow) && (((Mobile)from).AccessLevel == AccessLevel.Player) )
                {
						from.SendMessage("Your pet is not ready yet.");
						return false;
                }

                if (((BaseCreature)bt.Creature).ControlSlots > (from.FollowersMax - from.Followers))
                {
                    from.SendMessage("You have to many followers to collect this creature");
                    return false;
                }


                ReadyPetForReturn(from, bt.Creature);
                if (bt.DoneReal == DateTime.MinValue)
                    return true;
                    
                if (bt.Failed)
                {
                    bt.Owner.SendMessage("The breeding has failed! Some of your money has been returned.");
                    bt.OtherParent.SendMessage("The breeding has failed! Some of your money has been returned.");	
					Banker.Deposit(bt.Owner, (int)(JakoBreeder.GoldPrice(bt.Creature, bt.OtherCreature) * .75));
                    Banker.Deposit(bt.OtherParent, (int)(JakoBreeder.GoldPrice(bt.OtherCreature, bt.Creature) * .75));
                    return true;
                }

                if (bt.Creature.Female)
                {
                    BaseCreature baby1 = Activator.CreateInstance(bt.Creature.GetType()) as BaseCreature;	

			if (baby1.Tamable && !(baby1 is Zombiex) && !(baby1 is BaseUndead) && !baby1.Controlled)
			{


					int rarity = 3; //randomize new pets on world generation
					int chance = Utility.RandomMinMax(1, 200);
					if (chance == 69)  // 0.5% chance
					    rarity = 6;
					else if (chance <= 10) // 5% chance
					    rarity = 5;
					else if (chance <= 30) // 15% chance
					    rarity = 4;
					else if (chance >= 190) // 5% chance
					    rarity = 1;
					else if (chance >= 170)  // 15% chance
					    rarity = 2;
					 

					if (rarity > 3) // 85% chance of being normal or stronger
					{
					    baby1.RawStr = (int)((double)baby1.RawStr*( ((double)(Utility.RandomMinMax(3, rarity) * 0.33))));
					    baby1.RawDex = (int)((double)baby1.RawDex*( ((double)(Utility.RandomMinMax(3, rarity) * 0.33))));
					    baby1.RawInt = (int)((double)baby1.RawInt*( ((double)(Utility.RandomMinMax(3, rarity) * 0.33))));
					    baby1.HitsMaxSeed = (int)((double)baby1.HitsMaxSeed*( ((double)(Utility.RandomMinMax(3, rarity) * 0.33))));

					    baby1.Hits = baby1.HitsMaxSeed;
					    baby1.DamageMax = (int)((double)baby1.DamageMax*( ((double)(Utility.RandomMinMax(3, rarity) * 0.33))));
					    baby1.DamageMin = (int)((double)baby1.DamageMin*( ((double)(Utility.RandomMinMax(3, rarity) * 0.33))));
					    if (baby1.DamageMin > baby1.DamageMax)
						baby1.DamageMin = baby1.DamageMax -1;
					}
					else if (rarity < 3) // 15% chance of being weaker
					{
					    baby1.RawStr = (int)((double)baby1.RawStr*( ((double)(Utility.RandomMinMax(1, rarity) * 0.33))));
					    baby1.RawDex = (int)((double)baby1.RawDex*( ((double)(Utility.RandomMinMax(1, rarity) * 0.33))));
					    baby1.RawInt = (int)((double)baby1.RawInt*( ((double)(Utility.RandomMinMax(1, rarity) * 0.33))));
					    baby1.HitsMaxSeed = (int)((double)baby1.HitsMaxSeed*( ((double)(Utility.RandomMinMax(1, rarity) * 0.33))));

					    baby1.Hits = baby1.HitsMaxSeed;
					    baby1.DamageMax = (int)((double)baby1.DamageMax*( ((double)(Utility.RandomMinMax(1, rarity) * 0.33))));
					    baby1.DamageMin = (int)((double)baby1.DamageMin*( ((double)(Utility.RandomMinMax(1, rarity) * 0.33))));
					    if (baby1.DamageMin > baby1.DamageMax)
						baby1.DamageMin = baby1.DamageMax -1;
					}
					else if (rarity == 3) // 70% chance of small change only
					{
					    baby1.RawStr += (int)((double)baby1.RawStr*( ((double)(Utility.RandomMinMax(1, rarity) * 0.05))));
					    baby1.RawDex += (int)((double)baby1.RawDex*( ((double)(Utility.RandomMinMax(1, rarity) * 0.05))));
					    baby1.RawInt += (int)((double)baby1.RawInt*( ((double)(Utility.RandomMinMax(1, rarity) * 0.05))));
					    baby1.HitsMaxSeed += (int)((double)baby1.HitsMaxSeed*( ((double)(Utility.RandomMinMax(1, rarity) * 0.05))));

					    baby1.Hits = baby1.HitsMaxSeed;
					    baby1.DamageMax += (int)((double)baby1.DamageMax*( ((double)(Utility.RandomMinMax(1, rarity) * 0.05))));
					    baby1.DamageMin += (int)((double)baby1.DamageMin*( ((double)(Utility.RandomMinMax(1, rarity) * 0.05))));
					    if (baby1.DamageMin > baby1.DamageMax)
						baby1.DamageMin = baby1.DamageMax -1;
					}

			}

                    baby1.MaxLevel = Convert.ToUInt32(Math.Ceiling((double)(((BaseCreature)bt.Creature).Level + ((BaseCreature)bt.OtherCreature).Level) / 1.25) + 1);

                    SetStats( baby1, bt.Creature as BaseCreature, bt.OtherCreature as BaseCreature );    

					LoggingFunctions.LogBreed( from, baby1, baby1.MaxLevel );
					from.SendMessage("Here is a ticket for the baby");
                    from.AddToBackpack(new BreedingParentTicket(bt.Owner, bt.OtherParent, baby1, DateTime.MinValue));
                    if (bt.Twins)
                    {
                        BaseCreature baby2 = Activator.CreateInstance(bt.Creature.GetType()) as BaseCreature;
                        baby2.Female = baby1.Female;
                        baby2.MaxLevel = baby1.MaxLevel;
			

                        if (baby2.Tamable && !(baby2 is Zombiex) && !(baby2 is BaseUndead))
                        {
    					int rarity = 3; //randomize new pets on world generation
					int chance = Utility.RandomMinMax(1, 200);
					if (chance == 69)  // 0.5% chance
					    rarity = 6;
					else if (chance <= 10) // 5% chance
					    rarity = 5;
					else if (chance <= 30) // 15% chance
					    rarity = 4;
					else if (chance >= 190) // 5% chance
					    rarity = 1;
					else if (chance >= 170)  // 15% chance
					    rarity = 2;
					 

					if (rarity > 3) // 85% chance of being normal or stronger
					{
					    baby2.RawStr = (int)((double)baby2.RawStr*( ((double)(Utility.RandomMinMax(3, rarity) * 0.33))));
					    baby2.RawDex = (int)((double)baby2.RawDex*( ((double)(Utility.RandomMinMax(3, rarity) * 0.33))));
					    baby2.RawInt = (int)((double)baby2.RawInt*( ((double)(Utility.RandomMinMax(3, rarity) * 0.33))));
					    baby2.HitsMaxSeed = (int)((double)baby2.HitsMaxSeed*( ((double)(Utility.RandomMinMax(3, rarity) * 0.33))));

					    baby2.Hits = baby2.HitsMaxSeed;
					    baby2.DamageMax = (int)((double)baby2.DamageMax*( ((double)(Utility.RandomMinMax(3, rarity) * 0.33))));
					    baby2.DamageMin = (int)((double)baby2.DamageMin*( ((double)(Utility.RandomMinMax(3, rarity) * 0.33))));
					    if (baby2.DamageMin > baby2.DamageMax)
						baby2.DamageMin = baby2.DamageMax -1;
					}
					else if (rarity < 3) // 15% chance of being weaker
					{
					    baby2.RawStr = (int)((double)baby2.RawStr*( ((double)(Utility.RandomMinMax(1, rarity) * 0.33))));
					    baby2.RawDex = (int)((double)baby2.RawDex*( ((double)(Utility.RandomMinMax(1, rarity) * 0.33))));
					    baby2.RawInt = (int)((double)baby2.RawInt*( ((double)(Utility.RandomMinMax(1, rarity) * 0.33))));
					    baby2.HitsMaxSeed = (int)((double)baby2.HitsMaxSeed*( ((double)(Utility.RandomMinMax(1, rarity) * 0.33))));

					    baby2.Hits = baby1.HitsMaxSeed;
					    baby2.DamageMax = (int)((double)baby2.DamageMax*( ((double)(Utility.RandomMinMax(1, rarity) * 0.33))));
					    baby2.DamageMin = (int)((double)baby2.DamageMin*( ((double)(Utility.RandomMinMax(1, rarity) * 0.33))));
					    if (baby2.DamageMin > baby2.DamageMax)
						baby2.DamageMin = baby2.DamageMax -1;
					}
					else if (rarity == 3) // 70% chance of small change only
					{
					    baby2.RawStr += (int)((double)baby2.RawStr*( ((double)(Utility.RandomMinMax(1, rarity) * 0.05))));
					    baby2.RawDex += (int)((double)baby2.RawDex*( ((double)(Utility.RandomMinMax(1, rarity) * 0.05))));
					    baby2.RawInt += (int)((double)baby2.RawInt*( ((double)(Utility.RandomMinMax(1, rarity) * 0.05))));
					    baby2.HitsMaxSeed += (int)((double)baby2.HitsMaxSeed*( ((double)(Utility.RandomMinMax(1, rarity) * 0.05))));

					    baby2.Hits = baby1.HitsMaxSeed;
					    baby2.DamageMax += (int)((double)baby2.DamageMax*( ((double)(Utility.RandomMinMax(1, rarity) * 0.05))));
					    baby2.DamageMin += (int)((double)baby2.DamageMin*( ((double)(Utility.RandomMinMax(1, rarity) * 0.05))));
					    if (baby2.DamageMin > baby2.DamageMax)
						baby2.DamageMin = baby2.DamageMax -1;
					}

                            SetStats( baby2, bt.Creature as BaseCreature, bt.OtherCreature as BaseCreature );  

                            bt.Owner.SendMessage("Congradulations, your pet has just had twins! The father of the child has received the additional ticket.");
                            if (bt.Owner != bt.OtherParent)
                                bt.OtherParent.SendMessage(" Good news, it's twins! {0} has just recieved the second baby. Your ticket to redeem has been placed in your bank.", bt.Owner.Name);
                            bt.OtherParent.BankBox.AddItem(new BreedingParentTicket(bt.OtherParent, bt.Owner, baby2, DateTime.MinValue));
                        }
                    }
                }
                return true;
            }
            return false;
        }

        public void SetStats( Mobile baby, BaseCreature parent1, BaseCreature parent2 )
        {
            if (baby == null || parent1 == null || parent2 == null)
                return;

            BaseCreature statser = parent1;
            BaseCreature babe = (BaseCreature)baby;

            if (Utility.RandomDouble() > 0.33)
            {
                if (Utility.RandomBool())
                    statser = parent1;
                else
                        statser = parent2;
                babe.HitsMaxSeed = statser.HitsMaxSeed;
                babe.Hits = babe.HitsMaxSeed;
            }

            if (Utility.RandomDouble() > 0.50)
            {
                if (Utility.RandomBool())
                    statser = parent1;
                else
                        statser = parent2;

                if (statser.m_oldstr != 0)
                    babe.RawStr = statser.m_oldstr;
                else
                    babe.RawStr = statser.RawStr;
            }

            if (Utility.RandomDouble() > 0.85 )
            {
                if (statser.m_oldmin != 0)
                    babe.DamageMin = statser.m_oldmin;
                else
                    babe.DamageMin = statser.DamageMin;

                if (statser.m_oldmax != 0)
                    babe.DamageMax = statser.m_oldmax;
                else
                    babe.DamageMax = statser.DamageMax;
            }

            if (Utility.RandomDouble() > 0.50)
            {
                if (Utility.RandomBool())
                    statser = parent1;
                else
                        statser = parent2;

                if (statser.m_olddex != 0)
                    babe.RawDex = statser.m_olddex;
                else
                    babe.RawDex = statser.RawDex;
            }

            if (Utility.RandomDouble() > 0.50)
            {
                if (Utility.RandomBool())
                    statser = parent1;
                else
                        statser = parent2;

                if (statser.m_oldint != 0)
                    babe.RawInt = statser.m_oldint;
                else
                    babe.RawInt = statser.RawInt;
            }

            if (Utility.RandomDouble() > 0.33)
            {
                if (Utility.RandomBool())
                    statser = parent1;
                else
                        statser = parent2;
                
                if (statser.m_oldpois != 0)
                    babe.PoisonResistSeed = statser.m_oldpois;
                else
                    babe.PoisonResistSeed = statser.PoisonResistSeed;
            }

            if (Utility.RandomDouble() > 0.33)
            {
                if (Utility.RandomBool())
                    statser = parent1;
                else
                        statser = parent2;
                
                if (statser.m_oldcold != 0)
                    babe.ColdResistSeed = statser.m_oldcold;
                else
                    babe.ColdResistSeed = statser.ColdResistSeed;
            }

            if (Utility.RandomDouble() > 0.33)
            {
                if (Utility.RandomBool())
                    statser = parent1;
                else
                        statser = parent2;

                if (statser.m_oldphys != 0)
                    babe.PhysicalResistanceSeed = statser.m_oldphys;
                else
                    babe.PhysicalResistanceSeed = statser.PhysicalResistanceSeed;
            }

            if (Utility.RandomDouble() > 0.33)
            {
                if (Utility.RandomBool())
                    statser = parent1;
                else
                        statser = parent2;
                
                if (statser.m_oldfire != 0)
                    babe.FireResistSeed = statser.m_oldfire;
                else
                    babe.FireResistSeed = statser.FireResistSeed;    
            }

            if (Utility.RandomDouble() > 0.33)
            {
                if (Utility.RandomBool())
                    statser = parent1;
                else
                        statser = parent2;

                if (statser.m_oldener != 0)
                    babe.EnergyResistSeed = statser.m_oldener;
                else
                    babe.EnergyResistSeed = statser.EnergyResistSeed;    
            }     


        }

        public override void OnSpeech(SpeechEventArgs e)
        {
            if (e.Speech.ToLower().Equals("i wish to breed"))
                SayTo(e.Mobile,String.Format("I'm not that type of {0}.",(Female?"girl":"guy")));
            else if (e.Speech.ToLower().Contains("i wish to breed animals") || e.Speech.ToLower().Contains("mate"))
                JakoBreeder.BreedClick((PlayerMobile)e.Mobile, this);
            base.OnSpeech(e);
        }

		public JakoBreeder( Serial serial ) : base( serial )
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


        public class BreedingRequest
        {
            public Mobile Creature1;
            public Mobile Creature2;
            public bool Accepted;

            public BreedingRequest(Mobile c1, Mobile c2)
            {
                Creature1 = c1;
                Creature2 = c2;
                Accepted = false;
            }
        }
    }
}
