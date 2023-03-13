using System;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Items;

namespace Capt.MiniGames
{
    public class Yahtzee
    {
        public int AddDice(int[] dice, int diceValue)
        {
            int Sum = 0;
            for (int i = 0; i < 5; i++)
            {
                if (dice[i] == diceValue)
                {
                    Sum += diceValue;
                }
            }
            return Sum;
        }

        public int[] CalculateTotals( int[] scores, int[] totals)
        {
            int score;
            int scoreTotal = 0;
            int grandTotal = 0;
            int i;
            // calculate upper
            for (i = 0; i < 6; i++)
            {
                score = scores[i];
                if (score != -1)
                    scoreTotal += score;
            }
            totals[0] = scoreTotal;

            // check for bonus
            if (scoreTotal >= 63)
                totals[1] = 35;

            // reset counter
            scoreTotal = 0;

            // calculate lower
            for (; i < 13; i++)
            {
                score = scores[i];
                if (score != -1)
                    scoreTotal += score;
            }

            totals[2] = scoreTotal;

            // calculate grand total
            grandTotal = totals[0] + totals[1] + totals[2];
            totals[3] = grandTotal;

            return totals;
        }

        public int CalculateMultiples(int[] dice, int amount)
        {
            int Sum = 0;
            bool multiples = false;
            for (int i = 1; i <= 6; i++)
            {
                int Count = 0;
                for (int ii = 0; ii < 5; ii++)
                {
                    if (dice[ii] == i)
                        Count++;

                    if (Count >= amount)
                    {
                        multiples = true;
                    }

                }
                if (multiples)
                {
                    for (int iii = 0; iii < 5; iii++)
                    {
                        Sum += dice[iii];
                    }
                    return Sum;
                }
            }
            return Sum;
        }

        public int CalculateChance(int[] dice)
        {
            int Sum = 0;
            for (int i = 0; i < 5; i++)
            {
                    Sum += dice[i];
            }
            return Sum;
        }

        public int CalculateFullHouse(int[] dice)
        {
            int Sum = 0;
            Array.Sort(dice);

            if ((((dice[0] == dice[1]) && (dice[1] == dice[2])) && //3
                 (dice[3] == dice[4]) &&  //2
                 (dice[2] != dice[3])) || //or
                ((dice[0] == dice[1]) &&  //2 
                 ((dice[2] == dice[3]) && (dice[3] == dice[4])) && //3
                 (dice[1] != dice[2])))
            {
                Sum = 25;
            }

            return Sum;
        }

        public int CalculateShortStraight(int[] dice)
        {
            int Sum = 0;
            // move duplicates
            Array.Sort(dice);

            for (int i = 0; i < 4; i++)
            {
                int temp = 0;
                if (dice[i] == dice[i + 1])
                {
                    temp = dice[i];

                    for (int ii = i; ii < 4; ii++)
                    {
                        dice[ii] = dice[ii + 1];
                    }

                    dice[4] = temp;
                }
            }

            if (((dice[0] == 1) && (dice[1] == 2) && (dice[2] == 3) && (dice[3] == 4)) ||
                ((dice[0] == 2) && (dice[1] == 3) && (dice[2] == 4) && (dice[3] == 5)) ||
                ((dice[0] == 3) && (dice[1] == 4) && (dice[2] == 5) && (dice[3] == 6)) ||
                ((dice[1] == 1) && (dice[2] == 2) && (dice[3] == 3) && (dice[4] == 4)) ||
                ((dice[1] == 2) && (dice[2] == 3) && (dice[3] == 4) && (dice[4] == 5)) ||
                ((dice[1] == 3) && (dice[2] == 4) && (dice[3] == 5) && (dice[4] == 6)))
            {
                Sum = 30;
            }

            return Sum;
        }
        public int CalculateLongStraight(int[] dice)
        {
            int Sum = 0;

            Array.Sort(dice);

            if (((dice[0] == 1) && (dice[1] == 2) && (dice[2] == 3) && (dice[3] == 4) && (dice[4] == 5)) ||
                ((dice[0] == 2) && (dice[1] == 3) && (dice[2] == 4) && (dice[3] == 5) && (dice[4] == 6)))
            {
                Sum = 40;
            }

            return Sum;
        }
        public int CalculateYahtzee(int[] dice)
        {
            int Sum = 0;

            if (dice[0] == dice[1] && dice[1] == dice[2] && dice[2] == dice[3] && dice[3] == dice[4])
                Sum = 50;

            return Sum;
        }
        public bool CalculateMoves(int[] scores)
        {
            for (int i = 0; i < 13; i++)
            {
                if (scores[i] == -1)
                    return false;
            }
            return true;
        }

        public void GiveReward(Mobile from, int total)
        {
            int toGive = (total - 200) * 10;
            if (toGive < 0)
                toGive = 0;

            try 
            {
                string message = string.Format("Congratulations! You have scored a grand total of {0} points. You win {1} gold.", total, toGive);
                string overheadMessage = string.Format("Yahtzee! {0} Points", total.ToString());
                if (!(toGive == 0))
                    from.AddToBackpack(new Gold(toGive));
                from.SendMessage(50, message);
                from.PublicOverheadMessage(MessageType.Regular, 50, true, overheadMessage, true );
            }
            catch { }
            

        }
    }
}