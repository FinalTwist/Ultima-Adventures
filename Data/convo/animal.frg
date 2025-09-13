// Animal Trainer function
//
// Keywords:
// what*do*do, job, train, animal, cat, dog, horse
//
// whip saddle trainer 
// 
// - cwm, (jls 4.7.97)


#Fragment Britannia, Job, Britannia_Animaltrainer {
#Sophistication High {
#KEY "*job*", "*what*do*do*", "*trainer*"   {
#Attitude Wicked {
	"I train animals.",
	"I break horses, and train cats and dogs.",
        "Hast thou a dog, horse or cat, that earns not its keep? I can train it to better serve thee, or sell thee a better one.",
        "Shouldst thou need a hard-working horse, dog or cat, I can provide thee with one."
	}
#Attitude Neutral {
	"I train animals.",
	"I train and sell horses, cats and dogs.",
        "Hast thou a dog, horse or cat, that earns not its keep? I can teach this animal to better serve thee.",
        "If thou shouldst need a hardworking horse, dog or cat, I can provide thee with one."
	}
#Attitude Goodhearted {
	"I educate horses, cats and dogs.",
	"I turn beasts into useful companions.",
        "If thy dog, cat or horse is not quite the companion thou mightst wish, I can teach it better and more useful behavior.",
        "If thou shouldst need a smart, well-behaved animal companion, I can provide thee with one."
	}
	}
#KEY "*train*", "*animal*" {
#Attitude Wicked {
	"I teach beasts to respect the whip!",
	"I make sure animals know who's the boss.",
        "In my experience as a trainer, I have seen that the best way to spoil an animal is to spare the rod.",
        "Any trainer will tell thee that one cannot reason with a horse or dog. They understand pain and pleasure, but little else."
	}
#Attitude Neutral {
	"I can break horses to saddle, train guard dogs, and even turn cats into better mousers.",
	"I have found that animals need a firm but fair hand.",
	"In my experience as a trainer, I have seen that animals respond best to a combination of strict discipline and affectionate praise.",
        "Methinks there is naught worse than an untrained beast. An animal should be respectful and useful to its master."
}
#Attitude Goodhearted {
	"Animals learn like children, through affectionate discipline.",
	"Train a beast with fear and it'll flee if it can. Train it with love and it will follow thee into hell itself.",
        "The best way to train an animal is to befriend it. Once it is thy friend, it will desire to please thee in any way it can.",
        "Those that make heavy use of the rod in training animals, are inferior to the poor beasts they abuse."
}
}
#KEY "*saddle*" {
#Attitude Wicked {
	"The horses of Britannia don't require saddles for riding. Some still use them, but it's more out of habit than need.",
	"Don't bother with a saddle! Britannian horses are as belligerent with as without them.",
        "Saddles will get thee nowhere with these animals."
	}
#Attitude Neutral {
	"I train my horses so that they prefer to be ridden bareback. 'Tis less trouble in the long run.",
	"A well-trained horse should have no trouble being ridden saddle-less. In fact, they seem to prefer it.",
        "A saddle would be a waste of thy money in Britannia."
	}
#Attitude Goodhearted {
	"We have bred horses here in Britannia of such intelligence that they're willing to work with thee, not against. The saddle has been rendered almost useless.",
	"There is no need to use a saddle on these fine steeds.",
        "Waste not thy money on a saddle. Thou dost not need it."
}
}

#KEY "*whip*" {
#Attitude Wicked {
	"Ah! Get thine own whip! No one makes or sells them here.  I had to braid mine myself.",
	"There doesn't seem to be enough of a market for whips to support a craftsman. I had to make mine own.",
        "Thou art a man after my own heart, if thou art looking for a whip. Sorry I can't help thee, though. No one seems to find it worth the time to sell 'em."
	}
#Attitude Neutral {
	"Oh, thou dost not need a whip for thine animals. 'Tis too harsh a punishment!",
	"A well-used whip will cause thy horse or dog to turn on thee. Mark my words. 'Tis lucky thou can no longer get one.",
        "Thou should not need a whip for a well-trained animal."
	}
#Attitude Goodhearted {
	"Only a heartless animal would think to use a whip! Be glad that none can be bought in Britannia!",
	"There is no need to use a whip on these fine steeds! Don't think on it!",
        "'Tis a good thing none sell those evil instruments any longer! Only a monster would think to use one!"
}
}


#KEY "*dog*" {
#Attitude Wicked {
	"I show my dogs who's boss.",
	"My guard dogs will rip an intruder's throat right out.",
        "An unmannered dog is a curse on its master.",
        "All of my dogs know to fear their masters."
	}
#Attitude Neutral {
	"I train strong working dogs, not little house pets.",
	"Thou must be firm with a dog, but cruelty will ruin them.",
        "I find that the more well-behaved the dog is, the happier it is.",
        "All of my dogs respect their masters."
	}
#Attitude Goodhearted {
	"A dog is like a good soldier. All he asks of thee is wise, fair leadership.",
	"There is no human friendship can equal the bond between man and dog.",
        "A dog is happiest when it knows exactly what its human expects of it.",
        "All of my dogs love their masters."
}
}
#KEY "*cat*" {
#Attitude Wicked {
        "Even a cat can learn to fear a boot.",
	"I don't stand for any nonsense, even from a cat.",
        "I don't know why people waste their time with the arrogant creatures. If my cats don't prove good mousers, they end up fish food.",
        "The world's too small for a useless cat.",
        "Cats? I keep 'em only because people seem to want them. I'faith, I cannot see why."
	}
#Attitude Neutral {
	"One can't really train a cat. One can simply try to convince it that it wants to do things in a certain way.",
        "Cats haven't any masters, but they do have friends, or even partners.",
        "A cat cannot be trained. But its skills can be put to work catching mice.",
        "I think many people keep a cat for the same reason they might buy a painting - they are beautiful, graceful animals."
	}
#Attitude Goodhearted {
	"Many misguided folk distrust cats. They find them much too human for comfort.",
        "A well-behaved cat is a blessing to any house. The other kind is a curse.",
        "Cats need naught from humans. They are companion only to those who they truly care for.",
        "Windowsills and hearth rugs look painfully empty without a cat lying on them."
}
} 
#KEY "*horse*" {
#Attitude Wicked {
        "The first thing one must understand is that the horse is a stupid creature.",
	"Until a horse has a bit in its mouth, the only way to get its attention is to strike it between the eyes with a fist.",
        "A horse is worth its oats only so long as it can work. Otherwise, it's worth little more than shoe leather.",
        "I have yet to meet a horse I couldn't break."
	}
#Attitude Neutral {
	"A well-trained horse can be a fine animal. There is truly no creature more useful to man.",
        "I cannot abide a belligerent or willful horse.",
        "A horse is only as good as its rider.",
        "All of my horses are well-schooled, hard-working animals."
	}
#Attitude Goodhearted {
	"Horses have been glorified in legend since man first learned to ride them. Surely there is no more noble a beast.",
        "When both horse and rider are experienced, the signals that pass between them are invisible to an observer.",
        "Methinks there exists no creature more beautiful and more willing to serve man than the horse.",
        "I have yet to meet a horse that could not be ridden. Training them requires only a modicum of patience and gentleness." 
}
}
}
#Sophistication Medium {
#KEY "*job*", "*what*do*do*", "*trainer*"   {
#Attitude Wicked {
	"I train animals.",
	"I break horses, and train cats and dogs.",
        "Hast thou a dog, horse or cat, that earns not its keep? I can train it to better serve thee, or sell thee a better one.",
        "Shouldst thou need a hard-working horse, dog or cat, I can provide thee with one."
	}
#Attitude Neutral {
	"I train animals.",
	"I train and sell horses, cats and dogs.",
        "Hast thou a dog, horse or cat, that earns not its keep? I can teach this animal to better serve thee.",
        "If thou shouldst need a hardworking horse, dog or cat, I can provide thee with one."
	}
#Attitude Goodhearted {
	"I educate horses, cats and dogs.",
	"I turn beasts into useful companions.",
        "If thy dog, cat or horse is not quite the companion thou mightst wish, I can teach it better and more useful behavior.",
        "If thou shouldst need a smart, well-behaved animal companion, I can provide thee with one."
	}
	}
#KEY "*train*", "*animal*" {
#Attitude Wicked {
	"I teach beasts to respect the whip!",
	"I make sure animals know who's the boss.",
        "In my experience as a trainer, I have seen that the best way to spoil an animal is to spare the rod.",
        "Any trainer will tell thee that one cannot reason with a horse or dog. They understand pain and pleasure, and little else."
	}
#Attitude Neutral {
	"I can break horses to saddle, train guard dogs, and even turn cats into better mousers.",
	"I have found that animals need a firm but fair hand.",
	"In my experience as a trainer, I have seen that animals respond best to a combination of strict discipline and affectionate praise.",
        "Methinks there is naught worse than an untrained beast. An animal should be respectful and useful to its master."
}
#Attitude Goodhearted {
	"Animals learn like children, through affectionate discipline.",
	"Train a beast with fear and it'll flee if it can. Train it with love and it will follow thee into hell itself.",
        "The best way to train an animal is to befriend it. Once it is thy friend, it will desire to please thee in any way it can.",
        "Those that make heavy use of the rod in training animals, are inferior to the poor beasts they abuse."
}
}

#KEY "*saddle*" {
#Attitude Wicked {
	"The horses of Britannia don't require saddles for riding. Some still use them, but it's more out of habit than need.",
	"Don't bother with a saddle! Britannian horses are as belligerent with as without them.",
        "Saddles will get thee nowhere with these animals."
	}
#Attitude Neutral {
	"I train my horses so that they prefer to be ridden bareback. 'Tis less trouble in the long run.",
	"A well-trained horse should have no trouble being ridden saddle-less. In fact, they seem to prefer it.",
        "A saddle would be a waste of thy money in Britannia."
	}
#Attitude Goodhearted {
	"We have bred horses here in Britannia of such intelligence that they're willing to work with thee, not against. The saddle has been rendered almost useless.",
	"There is no need to use a saddle on these fine steeds.",
        "Waste not thy money on a saddle. Thou dost not need it."
}
}

#KEY "*whip*" {
#Attitude Wicked {
	"Ah! Get thine own whip! No one makes or sells them here.  I had to braid mine myself.",
	"There doesn't seem to be enough of a market for whips to support a craftsman. I had to make mine own.",
        "Thou art a man after my own heart, if thou art looking for a whip. Sorry I can't help thee, though. No one seems to find it worth the time to sell 'em."
	}
#Attitude Neutral {
	"Oh, thou dost not need a whip for thine animals. 'Tis too harsh a punishment!",
	"A well-used whip will cause thy horse or dog to turn on thee. Mark my words. 'Tis lucky thou can no longer get one.",
        "Thou should not need a whip for a well-trained animal."
	}
#Attitude Goodhearted {
	"Only a heartless animal would think to use a whip! Be glad that none can be bought in Britannia!",
	"There is no need to use a whip on these fine steeds! Don't think on it!",
        "'Tis a good thing none sell those evil instruments any longer! Only a monster would think to use one!"
}
}

#KEY "*dog*" {
#Attitude Wicked {
	"I show my curs who's boss.",
	"My guard dogs will rip an intruder's throat right out.",
        "Marry, an unmannered dog is a curse on its master.",
        "All of my dogs know to fear their masters.",
	}
#Attitude Neutral {
	"I train strong working dogs, not little house pets.",
	"Thou must be firm with a dog, but cruelty will ruin them.",
        "I find that the more well-behaved the dog is, the happier it is.",
        "All of my dogs respect their masters."
	}
#Attitude Goodhearted {
	"A dog is like a good soldier. All he asks of thee is wise, fair leadership.",
	"There is no human friendship can equal the bond between man and dog.",
        "A dog is happiest when it knows exactly what its human expects of it.",
        "All of my dogs love their masters."
}
}
#KEY "*cat*" {
#Attitude Wicked {
        "Even a cat can learn to fear a boot.",
	"I don't stand for any nonsense, even from a cat.",
        "I don't know why people waste their time with the arrogant creatures. If my cat don't prove good mousers, they end up fish food.",
        "The world's too small for a useless cat.",
        "Cats? I keep 'em only because people seem to want them. I'faith, I cannot see why."
	}
#Attitude Neutral {
	"One can't really train a cat. One can simply try to convince it that it wants to do things in a certain way.",
        "Cats haven't any masters, but they do have friends, or even partners.",
        "A cat cannot be trained. But its skills can be put to work catching mice.",
        "I think many people keep a cat for the same reason they might buy a painting - they are beautiful, graceful animals."
	}
#Attitude Goodhearted {
	"Many misguided folk distrust cats. They find them much too human for comfort.",
        "A well-behaved cat is a blessing to any house. The other kind is a curse.",
        "Cats need naught from humans. They are companion only to those who they truly care for.",
        "Windowsills and hearth rugs look painfully empty without a cat lying on them."
}
} 
#KEY "*horse*" {
#Attitude Wicked {
        "The first thing one must understand is that the horse is a stupid creature.",
	"Until a horse has a bit in its mouth, the only way to get its attention is to strike it between the eyes with a fist.",
        "A horse is worth its oats only so long as it can work. Otherwise, it's worth little more than shoe leather.",
        "I have yet to meet a horse I couldn't break."
	}
#Attitude Neutral {
	"If well-trained, horses can be fine animals. There is truly no creature more useful to man.",
        "I cannot abide a belligerent or willful horse.",
        "A horse is only as good as its rider.",
        "All of my horses are well-schooled, hard-working animals."
	}
#Attitude Goodhearted {
	"Horses have been glorified in legend since man first learned to ride them. Surely there is no more noble a beast.",
        "When both horse and rider are experienced, the signals that pass between them are invisible to an observer.",
        "Methinks there exists no creature more beautiful and more willing to serve man than the horse.",
        "I have yet to meet a horse that could not be ridden. Training them requires only a modicum of patience and gentleness." 
}
}
}
#Sophistication Low {
#KEY "*job*", "*what*do*do*", "*trainer*"   {
#Attitude Wicked {
	"I train animals, $milord/milady$.",
	"I break horses, and train cats and dogs, $milord/milady$.",
        "If thou got a useless dog, horse or cat, I'll train it to obey, or sell thee a better one.",
        "Need a hard-workin' horse, dog or cat? I'll sell thee one or train thine."
	}
#Attitude Neutral {
	"I train animals, $milord/milady$.",
	"I train and sell horses, cats and dogs, $milord/milady$.",
        "Got a dog, horse or cat, that don't earn its keep? I can teach it to be better.",
        "Lookin' for a hardworking horse, dog or cat? I can sell thee one, $milord/milady$."
	}
#Attitude Goodhearted {
	"I tame horses, cats and dogs.",
	"I like to say that I turn beasts into friends.",
        "If thou hast a brutish dog, cat or horse, $milord/milady$, I can teach it better and more useful behavior.",
        "I'm an animal trainer. I can find thee a smart, well-behaved animal if thou would like one."
	}
	}
#KEY "*train*" "*animal*"{
#Attitude Wicked {
	"I teach beasts to respect the whip!",
	"I make sure animals know who's the boss.",
	"It's my experience that the best way to spoil an animal is to spare the rod.",
        "Any trainer will vouch - thou can't reason with a horse or dog. They understand pain and pleasure, but that's it."
               }
#Attitude Neutral {
	"I break horses, train guard dogs, and even turn cats into better mousers.",
	"Animals are needin' a firm but fair hand.",
	"'Tis my experience that animals seem to like strict rules and loving praise.",
        "I think there ain't nothin' worse than an untrained beast. Animals should be useful and obedient."
}
#Attitude Goodhearted {
	"Animals learn like children, through kindness.",
	"Train a beast with fear and it'll run off if it can. Train it with love and it'll follow thee to hell itself.",
        "The best way to train an animal is be a friend to it. Then it'll do anything thou ask.",
        "Those that beat the animals they train are lower than the poor brutes they injure."
}
}

#KEY "*saddle*" {
#Attitude Wicked {
	"Britannian horses don't need saddles. Some folks still use 'em, but you ain't gonna need 'em.",
	"Don't bother with saddles! Britannian horses are as mean with as without 'em.",
        "Saddles will get thee nowhere with these beasts."
	}
#Attitude Neutral {
	"My horses like to be ridden bareback. Less trouble for the rider, too.",
	"My horses ain't got no trouble being ridden without no saddle. In fact, they like it.",
        "Aw! A saddle is a waste of money in Britannia."
	}
#Attitude Goodhearted {
	"Our horses here in Britannia are smart. They're willing to work with thee, not against. The saddle's almost useless.",
	"There ain't no need for a saddle on these creatures.",
        "Don't waste money on a saddle. No need for it."
}
}

#KEY "*whip*" {
#Attitude Wicked {
	"Ah! No one makes or sells them whips, anymore.  I had to braid my own.",
	"There don't seem to be enough interest for whips for a man to make a living makin' 'em. I made my own.",
        "A whip's a useful thing, I'm tellin' thee. Sorry I can't help, though. No one seems to want to sell 'em."
	}
#Attitude Neutral {
	"A whip ain't very useful for most animals. Makes 'em mean!",
	"Use a whip too much and that animal will turn on thee. Mark my words. It's lucky thou can no longer get one.",
        "Thou shouldn't need a whip for one of my animals."
	}
#Attitude Goodhearted {
	"Now thou wouldn't think to use a whip, would thou? 'Tis a good thing that none can be bought in Britannia!",
	"There ain't no need to use a whip on these steeds! Don't think on it!",
        "'Tis a good thing none sell those evil instruments any longer! Only a monster would think to use one!"
}
}

#KEY "*dog*" {
#Attitude Wicked {
	"I show my curs who's the boss.",
	"My guard dogs will rip an intruder's throat right out.",
        "Marry, a brutish dog's a curse on its master.",
        "All my dogs know to fear their masters."
	}
#Attitude Neutral {
	"I train strong working dogs, not little house pets.",
	"Thou must be firm with a dog, but beatings'll ruin 'em.",
        "I find that the more trained a dog is, the happier it is.",
        "All my dogs respect their masters."
}
#Attitude Goodhearted {
	"A dog is like a good soldier. All he asks is wise, fair leadership.",
	"There ain't no human friendship can touch a dog's love for its master.",
        "A dog is happiest when it knows exactly what's expected of it.",
        "All my dogs love their masters."
}
}
#KEY "*cat*" {
#Attitude Wicked {
        "Even a cat can learn what a boot's for.",
	"I don't stand for no nonsense, not even from a cat.",
	"Can't understand what people see in cats. If mine don't prove good mousers, they end up fish food.",
        "The world's too small for a useless cat.",
        "Cats? I keep 'em only because people keep askin' me for 'em. Dunno what they want 'em for ..."
}
#Attitude Neutral {
	"'Tis impossible to train a cats. I jus' try and convince 'em they wants to do things my way.",
        "Cats got no masters. Thou can sometimes make friends with 'em, though."
	}
#Attitude Goodhearted {
	"Many folks ain't trustin' cats; think they're much too human for comfort.",
        "Well-behaved cats are a blessing. Wildcats are a curse.",
        "Cats need nothin' from us. They stick around only 'cause they like the company.",
        "A house seems mighty empty without a cat."
}
}
#KEY "*horse*" {
#Attitude Wicked {
        "The first thing thou needs to know is that a horse is a stupid brute.",
	"'Til thou hast a bit in its mouth, the only way to talk to a horse is to pound it between the eyes with a fist.",
        "A horse is only worth its oats if it can work. Past that, it's worth little more 'n shoe leather.",
        "I haven't met a horse I couldn't break."
	}
#Attitude Neutral {
	"Nothin' more useful than a saddle-broke horse.",
        "I can't stand a mean or stubborn horse.",
        "A horse is only as good as its rider.",
        "All my horses are well-taught, hard-working animals."
	}
#Attitude Goodhearted {
	"Ahh, $milord/milady$, I think there're few animals more noble than the horse.",
        "If thou'rt looking at a good horse and a good rider, 'tis impossible to see when the rider corrects the horse.",
        "I think there ain't no creature more willin' to serve man than the horse.",
        "I've never known a horse that couldn't be ridden. It just takes a bit of patience."
}
}
}
}
