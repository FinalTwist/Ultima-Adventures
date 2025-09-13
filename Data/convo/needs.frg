// Needs function
//
// Keywords:
// need, want, require, lacking, needing, how... fare, 
// How are you..., Are you well..., How art thou..., Art thou well...
//
//  Detail for 3 attitude and 3 notoriety levels included here. 
// - Dab


#Fragment Britannia, General, Britannia_Needs {

#Sophistication High {	

	
	#KEY "*you need*", "*you want*", "*you require*",  "*thou need*", "*thou want*", "*thou require*", "*you lacking*", "*you needing*", "*What's wrong*", "*What is wrong*"  {
	#Attitude Wicked {
	#Notoriety Infamous {
		"I need [GetNeed]!",
		"Get thee away from me![Leave]",
		"Leave me be![Leave]",
		"I lack [GetNeed]!",
		"I don't have [GetNeed]!",
		"I require [GetNeed]!",
		"If thou dost want to help, bring me [GetNeed].",
		"I am in most dire need of [GetNeed].",
		"Bring me [GetNeed] or leave me be!"
		}

#Notoriety Anonymous {
		"I need [GetNeed].  If thou dost happen upon some, please bring it here.",
		"My [GetNeed] supply is severely lacking!",
		"I don't have any [GetNeed]!",
		"I require more [GetNeed]!",
		"If thou dost want to help, bring me [GetNeed].",
		"We seem to be short of [GetNeed].",
		"Bring me [GetNeed] if thou art able."
		}

#Notoriety Famous {
		"I have found myself in short supply of [GetNeed].  If thou dost happen upon some, I ask thee to please bring it here.",
		"My [GetNeed] supply is lacking!  If thou wouldst help, I would deeply appreciate it!",
		"I don't have [GetNeed]!  I need [GetNeed]!",
		"I require more [GetNeed]!  We ALL require more [GetNeed]!",
		"If thou dost bring me [GetNeed] $milord/milady$, it would be very helpful!",
		"We are short of [GetNeed].  It is important that this is remedied",
		"Bring me [GetNeed] if thou art able.  And willing.  'Twould help greatly!"
		}
		}

#Attitude Neutral {
#Notoriety Infamous {
		"Nothing, thanks!  [Leave]",
		"I'm doing well enough, thank thee! Don't bother thyself with my needs.",
		"Well, I don't have as much [GetNeed] as I'd like.  But I'm not suffering for it.",
		"I've no idea what thou dost mean.",
		"I am fine with what I currently have, thank thee.",
		"Do not bother me![Leave].",
		"Get thee away![Leave]"
		}

	
#Notoriety Anonymous {
		"I am doing well, just now.  But if thou dost happen upon some [GetNeed], I could use more.",
		"My [GetNeed] supply is getting low.",
		"I'm doing well enough, now.",
		"I require nothing.",
		"I don't know.  I guess I could use some more [GetNeed].",
		"Things seem to be going well.",
		"I'm doing well enough."
		}

#Notoriety Famous {
		"I seem to be doing well enough, now, thank thee $milord/milady$.",
		"I want for nothing!",
		"I seem to have all that I need.  Thank thee, though.",
		"I am well, $milord/milady$.  Thank thee for thy concern.",
		"Well, a little more [GetNeed] couldn't hurt.  But I do not suffer for lack of it.",
		"I'm doing well enough. Don't worry thyself with my needs.",
		"I'm fine.  Do not bother thyself with my petty life, $milord/milady$"
		}
		}
	
#Attitude Goodhearted {
#Notoriety Infamous {
		"Nothing, thank thee.",
		"I'm doing great! I am in need of absolutely nothing!",
		"I want for nothing.",
		"I'm doing well enough with what I have. Now please leave me alone.  [Leave]",
		"I am just fine as I am, thank thee. I must attend to other things, now.  [Leave]",
		"'Tis none of thy concern! [Leave]"
		}

#Notoriety Anonymous {
		"I am doing particularly well, just now, with what I have.",
		"I'm doing very well, thank thee. I need nothing.",
		"I'm doing quite well with what I have on hand.",
		"I require nothing, really.",
		"I'm as happy as could be expected. I need nothing.",
		"Things seem to be going quite well, thank thee. I don't need anything that I'm aware of.",
		"I'm doing quite well enough! Thy help is unnecessary. But thank thee."
		}

#Notoriety Famous {
		"I am doing particularly well, just now, $milord/milady$. I need nothing.",
		"I am doing very well just now, thank thee. I require nothing.",
		"I'm doing quite well, $milord/milady$. I need nothing at all.",
		"I require nothing at all.",
		"I'm as happy as could be, $milord/milady$. I'm in need of nothing.",
		"Things seem to be going quite well with what I have.  Quite well indeed!",
		"I'm faring quite well with what I have, thank thee!"
		}
		}
		}

	#KEY "*How*you*", "*Are you*well*", "*How art thou*", "*Art thou*well*", "*you fare*", "*thou fare*" {
	#Attitude Wicked {
	#Notoriety Infamous {
		"Horrible!  I am in dire need of [GetNeed].",
		"Miserable!  My supply of [GetNeed] has run out!",
		"Art thou blind?  I am not well at all!",
		"I am in need of [GetNeed]!",
		"I need [GetNeed].",
		"Not well!  There is a shortage of [GetNeed]."
		}
	
	#Notoriety Anonymous {
		"I am not well, unfortunately!  I am out of [GetNeed].",
		"I am not well at all!",
		"We are in need of [GetNeed]!",
		"I could use [GetNeed].",
		"Not well at all!  [GetNeed] is in short supply."
		}
	
	#Notoriety Famous {
		"I am not well at all, I fear!  My [GetNeed] is gone.",
"I am not well, $milord/milady$!",
"We are in need of [GetNeed], $milord/milady$!",
"I could use [GetNeed] $sir/milady$.  Thy help would be wonderful!",
		"Not well at all!  [GetNeed] is in very short supply.  With thy help, this problem might be remedied."
		}
		}
	
	#Attitude Neutral {
	#Notoriety Infamous {
		"I am well enough, thank thee.",
		"I have no complaints.",
		"I am fine.",
		"'Tis none of thy business.",
		"Doing well.",
		"Just fine.",
		"I could use some more [GetNeed], but I'm not sorely wanting for anything.",
		"I'm well."
		}
		
	#Notoriety Anonymous {
		"I am well, fortunately.",
		"Doing just fine.",
		"We are in need of [GetNeed]!",
		"I could use some more [GetNeed].  'Tis not a crucial thing, however.",
		"Good.  Good.  Doing good."
		}
	
	#Notoriety Famous {
		"I am well.  Nothing plagues me at the moment.",
"I am well enough, $milord/milady$!",
"We seem to be fine here, $milord/milady$!",
"Only half so well as thee!",
		"Doing just fine, $milord/milady$."
		}
		}

	#Attitude Goodhearted {
	#Notoriety Infamous {
		"I am well enough, thank thee.",
		"I have no complaints.",
		"I am fine.",
		"'Tis none of thy business!  Begone!  [Leave]",
		"Doing well.",
		"I cannot complain.",
		"I'm well."
		}

	#Notoriety Anonymous {
		"I am very well, fortunately.",
"I am doing just fine.",
"Couldn't be better!",
"Just dandy!",
		"Very good.  Doing good."			
		}

	#Notoriety Famous {
		"I am very well, fortunately, $milord/milady$.",
"I am doing just fine, $milord/milady$.",
"Couldn't be better!  Not even a little!",
"Just dandy!",
		"Very good.  Wonderful, in fact!  Just great!"
		}

			}
				}
					}
#Sophistication Medium {
	
	#KEY "*you need*", "*you want*", "*you require*",  "*thou need*", "*thou want*", "*thou require*", "*you lacking*", "*you needing*", "*What's wrong*", "*What is wrong*"  {
	#Attitude Wicked {
	#Notoriety Infamous {
		"I need [GetNeed]!",
		"Get thee away from me![Leave]",
		"Leave me be![Leave]",
		"I lack [GetNeed]!",
		"I don't have [GetNeed]!",
		"I require [GetNeed]!",
		"If thou dost want to help, bring me [GetNeed].",
		"I am in most dire need of [GetNeed].",
		"Bring me [GetNeed] or leave me be!"
		}

#Notoriety Anonymous {
		"I need [GetNeed].  If thou dost happen upon some, please bring it here.",
		"My [GetNeed] supply is severely lacking!",
		"I don't have any [GetNeed]!",
		"I require more [GetNeed]!",
		"If thou dost want to help, bring me [GetNeed].",
		"We seem to be short of [GetNeed].",
		"Bring me [GetNeed] if thou art able."
		}

#Notoriety Famous {
		"I have found myself in short supply of [GetNeed].  If thou dost happen upon some, I ask thee to please bring it here.",
"My [GetNeed] supply is lacking!  If thou wouldst help, I would deeply appreciate it!",
"I don't have [GetNeed]!  I need [GetNeed]!",
"I require more [GetNeed]!  We ALL require more [GetNeed]!",
"If thou dost bring me [GetNeed] $milord/milady$, it would be very helpful!",
"We are short of [GetNeed].  It is important that this is remedied",
		"Bring me [GetNeed] if thou art able.  And willing.  'Twould help greatly!"
		}
		}

#Attitude Neutral {
#Notoriety Infamous {
		"Nothing, thanks!  [Leave]",
		"I'm doing well enough, thank thee!",
		"Well, I don't have as much [GetNeed] as I'd like.  But I'm not suffering for it.",
		"I've no idea what thou dost mean.",
		"I am fine, thank thee.",
		"Do not bother me![Leave].",
		"Get thee away![Leave]"
		}

	
#Notoriety Anonymous {
		"I am doing well, just now.  But if thou dost happen upon some [GetNeed], I could use more.",
		"My [GetNeed] supply is getting low.",
		"I'm doing well enough, now.",
		"I require nothing.",
		"I don't know.  I guess I could use some more [GetNeed].",
		"Things seem to be going well.",
		"I'm doing well enough."
		}

#Notoriety Famous {
		"I seem to be doing well enough, now, thank thee $milord/milady$.",
"I want for nothing!",
"I seem to have all that I need.  Thank thee, though.",
"I am well, $milord/milady$.  Thank thee for thy concern.",
"Well, a little more [GetNeed] couldn't hurt.  But I do not suffer for lack of it.",
"I'm doing well enough.",
		"I'm fine.  Do not bother thyself with my petty life, $milord/milady$"
		}
		}
	
#Attitude Goodhearted {
#Notoriety Infamous {
		"Nothing, thank thee.",
		"I'm doing great! Don't need a thing!",
		"I want for nothing.",
		"I'm doing well enough with what I have. Now please leave me alone.  [Leave]",
		"I am just fine as I am, thank thee. I must attend to other things, now.  [Leave]",
		"'Tis none of thy concern! [Leave]"
		}

#Notoriety Anonymous {
		"I am doing particularly well, just now. Don't need a thing.",
		"I'm doing very well with what I have, thank thee.",
		"I'm doing quite well with what I have.",
		"I require nothing, really.",
		"I'm as happy as could be expected. I don't need anything.",
		"Things seem to be going quite well right now, thank thee. I don't need a thing.",
		"I'm doing quite well enough with what I have!"
		}

#Notoriety Famous {
		"I am doing particularly well, just now, $milord/milady$",
		"I am doing very well just now, thank thee. I need nothing.",
		"I'm doing quite well, $milord/milady$. I don't need a thing.",
		"I require nothing at all.",
		"I'm as happy as could be with what I have, $milord/milady$",
		"Things seem to be going quite well. Quite well indeed! I need nothing.",
		"I'm faring quite well, thank thee!"
		}
		}
		}

	#KEY "*How*you*", "*Are you*well*", "*How art thou*", "*Art thou*well*", "*you fare*", "*thou fare*" {
	#Attitude Wicked {
	#Notoriety Infamous {
		"Horrible!  I am in dire need of [GetNeed].",
		"Miserable!  My supply of [GetNeed] has run out!",
		"Art thou blind?  I am not well at all!",
		"I am in need of [GetNeed]!",
		"I need [GetNeed].",
		"Not well!  There is a shortage of [GetNeed]."
		}
	
	#Notoriety Anonymous {
		"I am not well, unfortunately!  I am out of [GetNeed].",
		"I am not well at all!",
		"We are in need of [GetNeed]!",
		"I could use [GetNeed].",
		"Not well at all!  [GetNeed] is in short supply."
		}
	
	#Notoriety Famous {
		"I am not well at all, I fear!  My [GetNeed] is gone.",
"I am not well, $milord/milady$!",
"We are in need of [GetNeed], $milord/milady$!",
"I could use [GetNeed] $sir/milady$.  Thy help would be wonderful!",
		"Not well at all!  [GetNeed] is in very short supply.  With thy help, this problem might be remedied."
		}
		}
	
	#Attitude Neutral {
	#Notoriety Infamous {
		"I am well enough, thank thee.",
		"I have no complaints.",
		"I am fine.",
		"'Tis none of thy business.",
		"Doing well.",
		"Just fine.",
		"I could use some more [GetNeed], but I'm not sorely wanting for anything.",
		"I'm well."
		}
		
	#Notoriety Anonymous {
		"I am well, fortunately.",
		"Doing just fine.",
		"We are in need of [GetNeed]!",
		"I could use some more [GetNeed].  'Tis not a crucial thing, however.",
		"Good.  Good.  Doing good."
		}
	
	#Notoriety Famous {
		"I am well.  Nothing plagues me at the moment.",
"I am well enough, $milord/milady$!",
"We seem to be fine here, $milord/milady$!",
"Only half so well as thee!",
		"Doing just fine, $milord/milady$."
		}
		}

	#Attitude Goodhearted {
	#Notoriety Infamous {
		"I am well enough, thank thee.",
		"I have no complaints.",
		"I am fine.",
		"'Tis none of thy business!  Begone!  [Leave]",
		"Doing well.",
		"I cannot complain.",
		"I'm well."
		}

	#Notoriety Anonymous {
		"I am very well, fortunately.",
"I am doing just fine.",
"Couldn't be better!",
"Just dandy!",
		"Very good.  Doing good."			
		}

	#Notoriety Famous {
		"I am very well, fortunately, $milord/milady$.",
"I am doing just fine, $milord/milady$.",
"Couldn't be better!  Not even a little!",
"Just dandy!",
		"Very good.  Wonderful, in fact!  Just great!"
		}

			}
				}
					}
#Sophistication Low {
	
		#KEY "*you need*", "*you want*", "*you require*",  "*thou need*", "*thou want*", "*thou require*", "*you lacking*", "*you needing*", "*What's wrong*", "*What is wrong*"  {
	#Attitude Wicked {
	#Notoriety Infamous {
		"I need [GetNeed]!",
		"Get away from me![Leave]",
		"Leave me be![Leave]",
		"I got no [GetNeed]!",
		"I don't got [GetNeed]!",
		"I gotta get [GetNeed]!",
		"If thou wants to help, bring me [GetNeed].",
		"I'm in need of [GetNeed].",
		"Get me [GetNeed] or leave me alone!"
		}

#Notoriety Anonymous {
		"I need [GetNeed].  If thou'rt havin' it, bring it here.",
		"My [GetNeed] is gone!",
		"I don't got no [GetNeed]!",
		"I need more [GetNeed]!",
		"If thou'rt wantin' to help, bring me [GetNeed].",
		"We're short o' [GetNeed].",
		"Bring me [GetNeed] if thou'rt able."
		}

#Notoriety Famous {
		"I got no [GetNeed].  If thou have some, I'd ask thee to bring it here.",
		"My [GetNeed] is gone!  Please help, if thou'rt able!",
		"I don't got [GetNeed]!  I need [GetNeed]!",
		"I need some more [GetNeed]!  We ALL need s'more [GetNeed]!",
		"If thou'lt bring me [GetNeed] $milord/milady$, t'would be helpful!",
		"We're low on [GetNeed].  'Tis important that this is fixed.",
		"Bring me [GetNeed]!  'Twould help me lots!"
		}
		}

#Attitude Neutral {
#Notoriety Infamous {
		"I don't need nothin', thanks!  [Leave]",
		"I'm doing good, thanks!",
		"Well, I ain't got as much [GetNeed] as I'd like.  But I ain't dyin' for it.",
		"I ain't got no idea what thou'rt meanin'.",
		"I'm fine, thank thee.",
		"Don't bother me![Leave].",
		"Get away![Leave]"
		}

	
#Notoriety Anonymous {
		"I'm doing good, now.  But if thou find some [GetNeed], I could use it.",
		"My [GetNeed] is gettin' low.",
		"I'm doing good, now.",
		"I don't need nothin'.",
		"I don't know.  I could use some more [GetNeed].",
		"Things seem to be goin' well.",
		"I'm doin' well enough."
		}

#Notoriety Famous {
		"I seem to be doin' well enough, now, thanks $milord/milady$.",
		"I need nothin'!",
		"I got all I need.  Thanks, though.",
		"I'm good, $milord/milady$.  Thanks for the concern.",
		"Well, a bit more [GetNeed] couldn't hurt.  But I ain't sufferin' for it.",
		"I'm doing good enough.",
		"I'm fine.  Don't bother with my petty life, $milord/milady$"
		}
		}
	
#Attitude Goodhearted {
#Notoriety Infamous {
		"Nothin', thank thee.",
		"I'm doin' great! Don't need nothin'.",
		"I don't need nothin'.",
		"I'm doin' well enough. Now please leave me alone.  [Leave]",
		"I'm just fine with what I got, thanks. I must look to other things, now.  [Leave]",
		"'Tis none of thy concern!  [Leave]"
		}

#Notoriety Anonymous {
		"I'm doin' fine, just now.",
		"I'm fine now, thank thee.",
		"I'm doin' good enough. Don't need nothin'.",
		"I need nothin', really.",
		"I'm happy as could be with what I got.",
		"Things seem to be goin' good, thank thee. Don't need a thing.",
		"I'm doin' good enough with what I got!"
		}

#Notoriety Famous {
		"I am doin' pretty good, just now, $milord/milady$",
		"I'm doin' just fine now, thanks. I don't need nothin'",
		"I'm doin' well as I am, $milord/milady$. Don't need a thing.",
		"I don't need nothin' at all.",
		"I'm as happy as could be with what I got, $milord/milady$",
		"Things seem to be goin' good! I don't need nothin'",
		"I'm doin' well with what I got, thank thee!"
		}
		}
		}

	#KEY "*How*you*", "*Are you*well*", "*How art thou*", "*Art thou*well*", "*you fare*", "*thou fare*" {
	#Attitude Wicked {
	#Notoriety Infamous {
		"Bad!  I need some [GetNeed].",
		"Miserable!  My [GetNeed] has run out!",
		"Art thou blind?  I ain't good at all!",
		"I need [GetNeed]!",
		"I want [GetNeed].",
		"Not good!  We're short o' [GetNeed]."
		}
	
	#Notoriety Anonymous {
		"I ain't good, blast it!  I'm out o' [GetNeed].",
		"I ain't good at all!",
		"We're needin' [GetNeed]!",
		"I could use [GetNeed].",
		"Not good at all!  [GetNeed] is in short supply."
		}
	
	#Notoriety Famous {
		"I ain't well at all, I fear!  My [GetNeed] is gone.",
"I ain't doin' very good, $milord/milady$!",
"We're needin' [GetNeed], $milord/milady$!",
"I could use [GetNeed] $sir/milady$.  Thy help would be 'preciated!",
		"Not doin' very good at all!  [GetNeed] is in real short supply.  With thy help, this problem might
be fixed."
		}
		}
	
	#Attitude Neutral {
	#Notoriety Infamous {
		"I'm fine, thank thee.",
		"I ain't got complaints.",
		"I'm fine.",
		"'Tis none of thy business.",
		"Doing good.",
		"Just fine.",
		"I could use some more [GetNeed], but I'm not sorely needin' nothin'.",
		"I'm good."
		}
		
	#Notoriety Anonymous {
		"I'm doin' fine.",
		"Doing just fine.",
		"We're needing [GetNeed]!",
		"I could use some more [GetNeed].  'Tis not a big thing, though.",
		"Good.  Good.  Doin' good."
		}
	
	#Notoriety Famous {
		"I'm well.  Nothing plagues me at the moment.",
"I'm well enough, $milord/milady$!",
"We seem to be fine here, $milord/milady$!",
"Only half so well as thee!",
		"Doing just fine, $milord/milady$."
		}
		}

	#Attitude Goodhearted {
	#Notoriety Infamous {
		"I'm well enough, thank thee.",
		"I ain't got no complaints.",
		"I'm fine.",
		"'Tis none of thy business!  Begone!  [Leave]",
		"Doin' well.",
		"I can't complain.",
		"I'm well."
		}

	#Notoriety Anonymous {
		"I am very good, fortunately.",
"I'm doing just fine.",
"Couldn't be better!",
"Just dandy!",
		"Very good.  Doing good."			
		}

	#Notoriety Famous {
		"I'm real good, $milord/milady$.",
"I'm doing just fine, $milord/milady$.",
"Couldn't be better!  Not even a little!",
"Just dandy!",
		"Very good.  Wonderful, in fact!  Just great!"
		}

			}
				}
					}
						}
