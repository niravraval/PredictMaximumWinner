using System;
using System.Collections.Generic;
using System.Linq;

namespace PredictMaximumWinner
{
    class Program
    {

        static void Main(string[] args)
        {

            Console.WriteLine("Hello World!");

            Console.WriteLine("Input No of Test (Between 1 to 1,00,000) :");

            int cases = Convert.ToInt32(Console.ReadLine());
            if (cases >= 1 && cases <= 100000)
            {
                List<TestCompetition> tests = new List<TestCompetition>(cases);

                for (int j = 0; j < cases; j++)
                {
                    int players = GetPlayerCount();
                    if (players >= 1 && players <= 100000)
                    {
                        TestCompetition test = new TestCompetition
                        {
                            TeamA = new List<Player>(),
                            TeamB = new List<Player>()
                        };

                        test.TeamA = GetTeamPower("INDIA", players);
                        test.TeamB = GetTeamPower("All Starz", players);

                        test.TeamA = test.TeamA.OrderBy(o => o.Power).ToList();
                        test.TeamB = test.TeamB.OrderBy(o => o.Power).ToList();

                        for (int i = 0; i < players; i++)
                        {
                            Player player = test.TeamA[i];
                            player.IsPlayed = true;

                            int BTeamPlayer = -1;

                            for (int b = players - 1; b >= 0; b--)
                            {
                                if (test.TeamB[b].IsPlayed == false)
                                {
                                    if (player.Power > test.TeamB[b].Power)
                                    {
                                        player.HasWon = true;
                                        test.TeamB[b].IsPlayed = true;
                                        break;
                                    }
                                    if (BTeamPlayer == -1 && player.HasWon == false)
                                    {
                                        BTeamPlayer = b;
                                    }
                                }
                            }

                            if (player.HasWon == false && BTeamPlayer > -1)
                            {
                                test.TeamB[BTeamPlayer].IsPlayed = true;
                            }
                            
                        }
                        int MatchedWon = test.TeamA.Count(x => x.HasWon == true);
                        
                        Console.WriteLine(string.Format("\nTotal Match Won : {0}\n", MatchedWon));
                    }
                    else
                    {
                        Console.WriteLine(string.Format("Invalid No of Players for Test Case : ", players));
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid Input for Number of Cases!");
                Console.ReadKey();
            }
        }

        private static List<Player> GetTeamPower(string TeamName, int Players)
        {
            List<Player> Team = new List<Player>();
            try
            {
                Console.WriteLine(string.Format("Input Power - Space Seperated - for Team {0} ({1} Members) :", TeamName, Players));
                string taStrength = Console.ReadLine();
                string[] TeamAStrength = taStrength.Split(' ');
                foreach (string item in TeamAStrength)
                {
                    Team.Add(new Player() { Power = Convert.ToInt64(item), IsPlayed = false, HasWon = false }); ;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("While getting Team {0} Players Power :\n", TeamName) + ex.Message);
            }
            return Team;
        }

        private static int GetPlayerCount()
        {
            try
            {
                Console.WriteLine("Input No of Team Member(s) (Between 1 to 1,00,000) :");
                int players = Convert.ToInt32(Console.ReadLine());
                return players;
            }
            catch (Exception ex)
            {
                Console.WriteLine("While Getting Players Count : " + ex.Message);
                return 0;
            }

        }
    }

    class TestCompetition
    {
        public List<Player> TeamA { get; set; }
        public List<Player> TeamB { get; set; }
    }

    class Player
    {
        public long Power { get; set; }
        public bool IsPlayed { get; set; }
        public bool HasWon { get; set; }
    }

}
