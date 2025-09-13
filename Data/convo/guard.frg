// Soldier/Guard fragment for Britannia
//
// Soldiers and Guards will need separate pools as well for the stuff that
// is more specific to their jobs--i.e. saying that their job is to protect
// Lord British, the castle, or the city.
//
// Keyword list:
// guard, soldier
// weapon, sword, axe, mace, pike, spear, bow, shield, armor, armour
//
// -Raph Koster
//

#Fragment Britannia, Job, Britannia_Guard {
    #Sophistication High {
	#Key "*job*", "*work*", "*what*do*do*", "*profession*", "*occupation*" {
		"I am a soldier, $milord/milady$.",
		"I work to keep this place free of the ruffians that seem to be drawn to it., $milord/milady$.",
		"I am a guard, $milord/milady$.",
		"I try to keep the streets safe."
                }
        #Key "*guard*", "*soldier*" {
            #Notoriety Infamous, Outlaw {
                #Attitude Wicked, Belligerent {
                    "Pah, the life of a guard is none too pleasant, and oft have I thought of relaxing my vigilance to lead the life thou dost.",
                    "Aye, we slave away maintaining a peace we do not have the chance to enjoy.",
                    "'Tisn't a grand life, all in all--thou hast all the adventure."
                }
                #Attitude Neutral, Kindly {
                    "Aye, I am a guard, and I have mine eye on thee!",
                    "The guards shall surely be watching thee closely.",
                    "Take care that we not run thee out of here--we are well aware of thy reputation and tolerate not the likes of thee."
                }
            }
            #Notoriety Anonymous, Known, Famous {
                "The guards are here to serve.",
                "Do not worry, we shall keep thee safe within the bounds of our patrol.",
                "We the guards protect the citizenry from those that seek to do harm."
            }
        }
        #Key "*weapon*", "*sword*", "*axe*", "*mace*", "*pike*", "*spear*", "*club*", "*dagger*" {
            #Notoriety Infamous, Outlaw {
                "Indeed, that would be the last thing we need, to give thee more %0s with which to harm people.",
                "$Scum/Wenches$ like thee should not be allowed to carry arms, for thou dost naught but cause trouble.",
                "Heh, thou dost need more %0s with which to increase thy infamy!"
            }
            #Notoriety Anonymous, Known, Famous {
                "'Tis best to carry a weapon if thou venturest outside the cities here in Britannia. There are indeed many nefarious creatures and wild beasts outside civilized lands.",
                "Thou shouldst visit a weaponsmith or a blacksmith if thou lookest for %0s. Take care to find a weapon of good quality as well, for a shoddy %0 could cost thee thy life.",
                "Hast thou tried a weaponsmith in thy search for %0s? Sometimes even the blacksmiths carry such in their stock."
            }
        }
        #Key "*crossbow*", "*bow*", "* arrow*", "*quarrel*", "*bolt*" {
            #Notoriety Infamous, Outlaw {
                "Thou dost need not another thing with which to shoot innocent people in the back!",
                "I think that thou hast made evil use of the weapons thou hast carried up to now!",
                "Mind thee! If thou dost obtain bows or crossbows or even dost poke some poor citizen too hard with thy finger, the guards shall be after thee!"
            }
            #Notoriety Anonymous, Known, Famous {
                "Hast thou sought out a bowyer or fletcher in thy quest for %0s? Mind thee, it must be an artisan of fair repute or else thou mayst find thyself with a useless missile at the wrong time.",
                "Bowyers can supply thee with %0s and much else. Ofttimes even provisioners have such in their stock.",
                "'Tis an excellent notion to be skilled at firing missiles 'pon the hapless pates of enemies, for the wilds of Britannia are not all hospitable."
            }
        }
        #Key "*shield*", "*armour*", "*armor*" {
            #Notoriety Infamous, Outlaw {
                "I imagine the likes of thee do often need stout metal at thy back and at thy side, for many wish to see thee dead for thy deeds.",
                "No amount of armour would protect thee from the guards if we caught thee doing wrong!",
                "Frankly, $scoundrel/wench$, I prefer to see thee without protection, so that if thou dost something wrong, I can slay thee without a second thought."
            }
            #Notoriety Anonymous, Known, Famous {
                "An armourer is naturally the best place to obtain a shield or armour.",
                "Hast thou tried an armourer? Surely there must be one that can help thee.",
                "'Tis always an excellent idea to be well-armoured, for there be many dangers in this land of Britannia."
            }
        }
    }
    #Sophistication Medium {
#Key "*job*", "*work*", "*what*do*do*", "*profession*", "*occupation*" {
		"I am a soldier, $milord/milady$.",
		"I work to keep this place free of the ruffians that seem to be drawn to it., $milord/milady$.",
		"I am a guard, $milord/milady$.",
		"I try to keep the streets safe."
                }
        #Key "*guard*", "*soldier*" {
            #Notoriety Infamous, Outlaw {
                #Attitude Wicked, Belligerent {
                    "I've been thinking that perhaps the life of a guard is not for me. Thy life seems much better than mine right now.",
                    "Thou'rt the lucky ones--thou ruffians. Thou canst enjoy life, while I am stuck here on duty.",
                    "Aye, I am a guard, which means I stand at my post or walk the same path for hours on end, and head home feeling I have accomplished nothing."
                }
                #Attitude Neutral, Kindly {
                    "Aye, I am a guard. So don't break the law.",
                    "I've got my eye on thee--the guards shall not let thee get away with anything.",
                    "Thou'rt scum, don't make me lock thee up."
                }
            }
            #Notoriety Anonymous, Known, Famous {
                "The guards are here to serve.",
                "Don't worry, we'll keep thee safe.",
                "Guards protect the citizens."
            }
        }
        #Key "*weapon*", "*sword*", "*axe*", "*mace*", "*pike*", "*spear*", "*club*", "*dagger*" {
            #Notoriety Infamous, Outlaw {
                "Now, why would I tell thee where to find %0s? Thou'rt a criminal!",
                "If I told thee where %0s were for sale thou wouldst probably go kill someone.",
                "I wouldn't trust thee with %0s."
            }
            #Notoriety Anonymous, Known, Famous {
                "The wilderness is dangerous, and %0s are good to have.",
                "Weaponsmiths sell %0s. Make sure to get a good one though.",
                "Art thou looking for %0s? Hast thou tried a blacksmith's shop?"
            }
        }
        #Key "*crossbow*", "*bow*", "* arrow*", "*quarrel*", "*bolt*" {
            #Notoriety Infamous, Outlaw {
                "Thou wouldst probably use a bow to shoot people in the back.",
                "With thy reputation, I don't think a guard should tell thee how to get a weapon.",
                "If thou usest a bow to shoot anyone, I will have to kill thee."
            }
            #Notoriety Anonymous, Known, Famous {
                "Bowyers or fletchers can sell thee %0s. Make sure thou gettest a good one though.",
                "%0s can be bought at the shops of bowyers.",
                "Bows are good things to have in the wilderness."
            }
        }
        #Key "*shield*", "*armour*", "*armor*" {
            #Notoriety Infamous, Outlaw {
                "So many people dislike thee that I bet thy %0 is quite dented!",
                "Thou wilt need good armour if I catch thee doing wrong.",
                "If thou dost not wear armour, then i can cut off thy thieving head more easily."
            }
            #Notoriety Anonymous, Known, Famous {
                "If thou wantest %0s, try an armourer.",
                "Hast thou tried an armourer?",
                "In the wildernesses of Britannia, good armour is essential."
            }
        }
    }
    #Sophistication Low {
#Key "*job*", "*work*", "*what*do*do*", "*profession*", "*occupation*" {
		"I'm a soldier, $milord/milady$.",
		"I beat up the ruffians., $milord/milady$.",
		"I'm a guard, $milord/milady$.",
		"I try to keep the streets safe."
                }
        #Key "*guard*", "*soldier*" {
                #Attitude Wicked, Belligerent {
                    "I hate bein' a guard.",
                    "I wish I were an adventurer like thee.",
                    "Guardin' is BORING."
                }
                #Attitude Neutral, Kindly {
                    "I's a guard!",
                    "Don' go doin' nothin', mind thee.",
                    "Thou'rt a likely lookin' $lad/lass$--behave yerself."
                    "I's a guard!",
                    "I'll protect thee.",
                    "We protect people."
                 }
        }
        #Key "*weapon*", "*sword*", "*axe*", "*mace*", "*pike*", "*spear*", "*club*", "*dagger*" {
                "%0s are good to have.",
                "I like MY weapon, it's mine.",
                "Thou kin find $0s at a weaponsmith shop, methinks."
        }
        #Key "*crossbow*", "*bow*", "* arrow*", "*quarrel*", "*bolt*" {
                "Thou kin find bows and such at the bow person's shop.",
                "Huh, I never thought 'bout where arrows come from.",
                "Like bows? They's sorta funny lookin'."
        }
        #Key "*shield*", "*armour*", "*armor*" {
            #Notoriety Infamous, Outlaw {
                "I kin put a dent in yer %0 if thou wants.",
                "If thou do anythin' wrong, thou'lt need good armour!",
                "Awww! If thou wears armour, I can't kill thee as easy."
            }
            #Notoriety Anonymous, Known, Famous {
                "An armourer can sell thee thy $0.",
                "Have thou tried an armourer?",
                "I like me armour."
            }
        }
    }
}

// End of fragment
