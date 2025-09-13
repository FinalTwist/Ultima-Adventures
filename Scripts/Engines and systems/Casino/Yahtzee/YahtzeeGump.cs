using System;
using Server;
using Server.Gumps;
using Server.Network;

namespace Capt.MiniGames
{
	public class YahtzeeGump : Gump
	{
        private int m_currentRoll;// = 1; // 3 rolls max
        private int[] ar_Dice;// = new int[5]{0, 0, 0, 0, 0};
        private bool[] ar_Holds;// = new int[5] { 0, 0, 0, 0, 0 };
        private int[] ar_Scores;// = new int[13] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 };
        private int[] ar_Totals;// = new int[4] { 0, 0, 0, 0 };
        private Mobile m_From;
        private Yahtzee yahtzeeCore;
        private bool hasUsedRoll;

        private const int baseDiceValue = 11279; // 1

        public YahtzeeGump(Mobile from)
            : this(
                    from, 
                    0, 
                    new int[5] { 1, 1, 1, 1, 1 },
                    new bool[5] { false, false, false, false, false },
                    new int[13] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 },
                    new int[4] { 0, 0, 0, 0 },
                    new Yahtzee(),
                    true
                    )
        {
        }

		public YahtzeeGump( Mobile from, int roll, int[] dice, bool[] holds, int[] scores, int[] totals, Yahtzee core, bool usedRoll )
			: base( 0, 0 )
		{
            m_From = from;
            m_currentRoll = roll;
            ar_Dice = dice;
            ar_Holds = holds;
            ar_Scores = scores;
            ar_Totals = totals;
            yahtzeeCore = core;
            hasUsedRoll = usedRoll;


			this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;
			this.AddPage(0);
			
            // background
            this.AddBackground(275, 50, 265, 500, 9270);
			this.AddImageTiled(285, 70, 245, 16, 3607);
            this.AddImageTiled(285, 60, 245, 20, 3004);
            this.AddLabel(385, 60, 31, @"Yahtzee");

            // dice
            int diceValue;
            int xOffset = 25;
            int xBase = 350;

            for (int diceDex = 0; diceDex <= 4; diceDex++)
            {
                diceValue = baseDiceValue;
                diceValue += (int)ar_Dice[diceDex];
                this.AddImage((xBase + (diceDex * xOffset)), 110, diceValue);
            }
            /*
            this.AddImage(350, 110, diceValue);
			this.AddImage(375, 110, 11281);
			this.AddImage(400, 110, 11282);
			this.AddImage(425, 110, 11283);
			this.AddImage(450, 110, 11284);
            */

            this.AddLabel(295, 110, 1149, @"Roll");
            this.AddLabel(305, 130, 1149, m_currentRoll.ToString());
            if (m_currentRoll < 3)
            {
                this.AddButton(325, 112, 5601, 5605, (int)Buttons.bt_Roll, GumpButtonType.Reply, 0);
            }
			
			
			this.AddLabel(295, 165, 1149, @"Ones");
            this.AddLabel(295, 185, 1149, @"Twos");
            this.AddLabel(295, 205, 1149, @"Threes");
            this.AddLabel(295, 225, 1149, @"Fours");
            this.AddLabel(295, 245, 1149, @"Fives");
            this.AddLabel(295, 265, 1149, @"Sixes");
			
            this.AddLabel(340, 285, 1152, @"Upper Total:");
			this.AddLabel(375, 305, 1152, @"Bonus:");

            this.AddLabel(295, 345, 1149, @"Three of a kind");
            this.AddLabel(295, 365, 1149, @"Four of a kind");
            this.AddLabel(295, 385, 1149, @"Full House");
            this.AddLabel(295, 405, 1149, @"Short Straight");
            this.AddLabel(295, 425, 1149, @"Long Straight");
            this.AddLabel(295, 445, 1149, @"Yahtzee");
            this.AddLabel(295, 465, 1149, @"Chance");
            
            this.AddLabel(338, 485, 1152, @"Lower Total:");

            this.AddLabel(340, 505, 1152, @"Grand Total:");

            if (!(m_currentRoll == 3))
            {
                this.AddLabel(480, 135, 1149, @"Hold");
                this.AddCheck(351, 135, 2715, 2714, (bool)ar_Holds[0], (int)Buttons.chk_Hold1);
                this.AddCheck(376, 135, 2715, 2714, (bool)ar_Holds[1], (int)Buttons.chk_Hold2);
                this.AddCheck(401, 135, 2715, 2714, (bool)ar_Holds[2], (int)Buttons.chk_Hold3);
                this.AddCheck(426, 135, 2715, 2714, (bool)ar_Holds[3], (int)Buttons.chk_Hold4);
                this.AddCheck(451, 135, 2715, 2714, (bool)ar_Holds[4], (int)Buttons.chk_Hold5);
            }

            if (!(hasUsedRoll))
            {
                this.AddButton(400, 167, 5601, 5605, (int)Buttons.bt_Ones, GumpButtonType.Reply, 0);
                this.AddButton(400, 187, 5601, 5605, (int)Buttons.bt_Twos, GumpButtonType.Reply, 0);
                this.AddButton(400, 207, 5601, 5605, (int)Buttons.bt_Threes, GumpButtonType.Reply, 0);
                this.AddButton(400, 227, 5601, 5605, (int)Buttons.bt_Fours, GumpButtonType.Reply, 0);
                this.AddButton(400, 247, 5601, 5605, (int)Buttons.bt_Fives, GumpButtonType.Reply, 0);
                this.AddButton(400, 267, 5601, 5605, (int)Buttons.bt_Sixes, GumpButtonType.Reply, 0);

                this.AddButton(400, 348, 5601, 5605, (int)Buttons.bt_ThreeOfAKind, GumpButtonType.Reply, 0);
                this.AddButton(400, 368, 5601, 5605, (int)Buttons.bt_FourOfAKind, GumpButtonType.Reply, 0);
                this.AddButton(400, 388, 5601, 5605, (int)Buttons.bt_FullHouse, GumpButtonType.Reply, 0);
                this.AddButton(400, 408, 5601, 5605, (int)Buttons.bt_ShortStraight, GumpButtonType.Reply, 0);
                this.AddButton(400, 428, 5601, 5605, (int)Buttons.bt_LongStraight, GumpButtonType.Reply, 0);
                this.AddButton(400, 448, 5601, 5605, (int)Buttons.bt_Yahtzee, GumpButtonType.Reply, 0);
                this.AddButton(400, 468, 5601, 5605, (int)Buttons.bt_Chance, GumpButtonType.Reply, 0);
            }

            
            // upper

            int yOffset = 20;
            int yBase = 165;
            string scoreText;
            int currentScore;
            int scoreDex;

            for (scoreDex = 0; scoreDex < 6; scoreDex++)
            {
                scoreText = "-";
                currentScore = ar_Scores[scoreDex];
                if (currentScore > -1)
                    scoreText = currentScore.ToString();
                this.AddLabel(460, (yBase + (yOffset * scoreDex)), 1149, scoreText); 

            }
            /*
            this.AddLabel(460, 185, 0, @"-"); // ones 1
			this.AddLabel(460, 205, 0, @"-"); // twos 2
			this.AddLabel(460, 225, 0, @"-"); // threes 3
			this.AddLabel(460, 245, 0, @"-"); // fours 4
			this.AddLabel(460, 265, 0, @"-"); // fives 5
			this.AddLabel(460, 285, 0, @"-"); // sixes 6
            */


            // lower
            yBase = 345; // adjust y starting value
            
            for (; scoreDex < 13; scoreDex++)
            {
                scoreText = "-";
                currentScore = ar_Scores[scoreDex];
                if (currentScore > -1)
                    scoreText = currentScore.ToString();
                this.AddLabel(460, (yBase + (yOffset * (scoreDex - 6))), 1149, scoreText);

            }
            /*
            this.AddLabel(460, 345, 0, @"-"); // three of a kind 7
			this.AddLabel(460, 365, 0, @"-"); // four of a kind 8
			this.AddLabel(460, 385, 0, @"-"); // full house 9
			this.AddLabel(460, 405, 0, @"-"); // short straight 10
			this.AddLabel(460, 425, 0, @"-"); // long straight 11
			this.AddLabel(460, 445, 0, @"-"); // yahtzee 12
			this.AddLabel(460, 465, 0, @"-"); // chance 13
            */

            // totals
            this.AddLabel(460, 285, 1152, ar_Totals[0].ToString()); // upper total 1.1
            this.AddLabel(460, 305, 1152, ar_Totals[1].ToString()); // bonus 1.2
            this.AddLabel(460, 485, 1152, ar_Totals[2].ToString()); // lower total 1.3
            this.AddLabel(460, 505, 1152, ar_Totals[3].ToString()); // grand total 1.4

		}
		
		public enum Buttons
		{
            None,
			chk_Hold1,
			chk_Hold2,
			chk_Hold3,
			chk_Hold4,
			chk_Hold5,
			bt_Roll,
			bt_Ones,
			bt_Twos,
			bt_Threes,
			bt_Fours,
			bt_Fives,
			bt_Sixes,
			bt_ThreeOfAKind,
			bt_FourOfAKind,
			bt_FullHouse,
			bt_ShortStraight,
			bt_LongStraight,
			bt_Yahtzee,
			bt_Chance,
		}

        public void CheckHold(RelayInfo info)
        {
            bool chk = false;

            for (int i = 1; i <= 5; i++)
            {
                chk = info.IsSwitched(i);
                ar_Holds[i - 1] = chk;
            }
        }

        public void ResetHold()
        {
            bool chk = false;
            for (int i = 1; i <= 5; i++)
            {
                ar_Holds[i - 1] = chk;
            }
            hasUsedRoll = true;
        }

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            int buttonID = info.ButtonID;
            
            CheckHold(info);

            switch (buttonID)
            {
                case 0:
                    {
                        return;
                        //break;
                    }

                case (int)Buttons.bt_Roll:
                    {
                        if (m_currentRoll < 3)
                        {
                            int roll = -1;
                            for (int i = 0; i < 5; i++)
                            {
                                if (!((bool)ar_Holds[i]) || m_currentRoll == 0)
                                {
                                    roll = Utility.RandomMinMax(1, 6);
                                    ar_Dice[i] = roll;
                                }
                            }
                            m_currentRoll++;
                            hasUsedRoll = false;
                        }
                        break;
                    }
                case (int)Buttons.bt_Ones:
                    {
                        if (ar_Scores[0] == -1)
                        {
                            int score = yahtzeeCore.AddDice(ar_Dice, 1);
                            ar_Scores[0] = score;
                            m_currentRoll = 0;
                            ResetHold();
                        }
                        break;
                    }
                case (int)Buttons.bt_Twos:
                    {
                        if (ar_Scores[1] == -1)
                        {
                            int score = yahtzeeCore.AddDice(ar_Dice, 2);
                            ar_Scores[1] = score;
                            m_currentRoll = 0;
                            ResetHold();
                        }
                        break;
                    }
                case (int)Buttons.bt_Threes:
                    {
                        if (ar_Scores[2] == -1)
                        {
                            int score = yahtzeeCore.AddDice(ar_Dice, 3);
                            ar_Scores[2] = score;
                            m_currentRoll = 0;
                            ResetHold();
                        }
                        break;
                    }
                case (int)Buttons.bt_Fours:
                    {
                        if (ar_Scores[3] == -1)
                        {
                            int score = yahtzeeCore.AddDice(ar_Dice, 4);
                            ar_Scores[3] = score;
                            m_currentRoll = 0;
                            ResetHold();
                        }
                        break;
                    }
                case (int)Buttons.bt_Fives:
                    {
                        if (ar_Scores[4] == -1)
                        {
                            int score = yahtzeeCore.AddDice(ar_Dice, 5);
                            ar_Scores[4] = score;
                            m_currentRoll = 0;
                            ResetHold();
                        }
                        break;
                    }
                case (int)Buttons.bt_Sixes:
                    {
                        if (ar_Scores[5] == -1)
                        {
                            int score = yahtzeeCore.AddDice(ar_Dice, 6);
                            ar_Scores[5] = score;
                            m_currentRoll = 0;
                            ResetHold();
                        }
                        break;
                    }
                case (int)Buttons.bt_ThreeOfAKind:
                    {
                        if (ar_Scores[6] == -1)
                        {
                            int score = yahtzeeCore.CalculateMultiples(ar_Dice, 3);
                            ar_Scores[6] = score;
                            m_currentRoll = 0;
                            ResetHold();
                        }
                        break;
                    }
                case (int)Buttons.bt_FourOfAKind:
                    {
                        if (ar_Scores[7] == -1)
                        {
                            int score = yahtzeeCore.CalculateMultiples(ar_Dice, 4);
                            ar_Scores[7] = score;
                            m_currentRoll = 0;
                            ResetHold();
                        }
                        break;
                    }
                case (int)Buttons.bt_FullHouse:
                    {
                        if (ar_Scores[8] == -1)
                        {
                            int score = yahtzeeCore.CalculateFullHouse(ar_Dice);
                            ar_Scores[8] = score;
                            m_currentRoll = 0;
                            ResetHold();
                        }
                        break;
                    }
                case (int)Buttons.bt_ShortStraight:
                    {
                        if (ar_Scores[9] == -1)
                        {
                            int score = yahtzeeCore.CalculateShortStraight(ar_Dice);
                            ar_Scores[9] = score;
                            m_currentRoll = 0;
                            ResetHold();
                        }
                        break;
                    }
                case (int)Buttons.bt_LongStraight:
                    {
                        if (ar_Scores[10] == -1)
                        {
                            int score = yahtzeeCore.CalculateLongStraight(ar_Dice);
                            ar_Scores[10] = score;
                            m_currentRoll = 0;
                            ResetHold();
                        }
                        break;
                    }
                case (int)Buttons.bt_Yahtzee:
                    {
                        if (ar_Scores[11] == -1 || ar_Scores[11] >= 50)
                        {
                            int score = yahtzeeCore.CalculateYahtzee(ar_Dice);
                            if (ar_Scores[11] == -1)
                            {
                                ar_Scores[11] = score;
                                m_currentRoll = 0;
                                ResetHold();
                            }
                            if (ar_Scores[11] >= 50)
                            {
                                if (score == 50)
                                {
                                    ar_Scores[11] += score;
                                    m_currentRoll = 0;
                                    ResetHold();
                                }
                            }
                        }
                        break;
                    }
                case (int)Buttons.bt_Chance:
                    {
                        if (ar_Scores[12] == -1)
                        {
                            int score = yahtzeeCore.CalculateChance(ar_Dice);
                            ar_Scores[12] = score;
                            m_currentRoll = 0;
                            ResetHold();
                        }
                        break;
                    }
            }

            ar_Totals = yahtzeeCore.CalculateTotals(ar_Scores, ar_Totals);
            bool isEnd = yahtzeeCore.CalculateMoves(ar_Scores);

            if (isEnd)
            {
                yahtzeeCore.GiveReward(m_From, ar_Totals[3]);
                m_From.SendGump(new YahtzeeGump(m_From, 3, ar_Dice, ar_Holds, ar_Scores, ar_Totals, yahtzeeCore, true));
                return;
            }
            m_From.SendGump(new YahtzeeGump(m_From, m_currentRoll, ar_Dice, ar_Holds, ar_Scores, ar_Totals, yahtzeeCore, hasUsedRoll));
        }
	}
}