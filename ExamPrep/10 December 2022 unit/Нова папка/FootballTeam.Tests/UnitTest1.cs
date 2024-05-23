using NUnit.Framework;
using System.Collections.Generic;
using System;

namespace FootballTeam.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CreateFootballTeamWithValidParameters()
        {
            FootballTeam team = new FootballTeam("Real Madrid", 21);


            Assert.IsNotNull(team);
            Assert.AreEqual("Real Madrid", team.Name);
            Assert.AreEqual(21, team.Capacity);


            Type t = team.Players.GetType();
            Type expectedType = typeof(List<FootballPlayer>);

            Assert.AreEqual(t, expectedType);
        }
        [Test]
        public void CreateFootballTeamWithInvalidName()
        {
            FootballTeam team ;
           Assert.Throws<ArgumentException>(()=>team = new FootballTeam("", 15));
        }
        [Test]
        public void CreateFootballTeamWithInvalidCapacity()
        {
            FootballTeam team;
            Assert.Throws<ArgumentException>(() => team = new FootballTeam("real", 13));
        }
        [Test]
        public void AddPlayerInTheTeam()
        {
            FootballPlayer player = new FootballPlayer("kostov", 9, "Goalkeeper");
            FootballTeam team = new FootballTeam("Levski",21);
            var actualResult = team.AddNewPlayer(player);
            var expectedResult = "Added player kostov in position Goalkeeper with number 9";
            Assert.AreEqual(expectedResult,actualResult);
        }
        [Test]
        public void AddPlayerInTheTeamFullCapacity()
        {
            FootballPlayer player6 = new FootballPlayer("kostov", 1, "Goalkeeper");
            FootballPlayer player1 = new FootballPlayer("iliev", 2, "Goalkeeper");
            FootballPlayer player2 = new FootballPlayer("ilkow", 3, "Goalkeeper");
            FootballPlayer player3 = new FootballPlayer("kolew", 4, "Goalkeeper");
            FootballPlayer player4 = new FootballPlayer("popow", 5, "Goalkeeper");
            FootballPlayer player5 = new FootballPlayer("lolow", 6, "Goalkeeper");
            FootballPlayer player7 = new FootballPlayer("Player7Name", 7, "Midfielder");
            FootballPlayer player8 = new FootballPlayer("Player8Name", 8, "Midfielder");
            FootballPlayer player9 = new FootballPlayer("Player9Name", 9, "Midfielder");
            FootballPlayer player10 = new FootballPlayer("Player10Name", 10, "Midfielder");
            FootballPlayer player11 = new FootballPlayer("Player11Name", 11, "Midfielder");
            FootballPlayer player12 = new FootballPlayer("Player12Name", 12, "Forward");
            FootballPlayer player13 = new FootballPlayer("Player13Name", 13, "Forward");
            FootballPlayer player14 = new FootballPlayer("Player14Name", 14, "Forward");
            FootballPlayer player15 = new FootballPlayer("Player15Name", 15, "Forward");
            FootballPlayer player16 = new FootballPlayer("Player16Name", 16, "Forward");

            FootballTeam team = new FootballTeam("Levski", 15);
             team.AddNewPlayer(player6);
             team.AddNewPlayer(player1);
             team.AddNewPlayer(player2);
             team.AddNewPlayer(player3);
             team.AddNewPlayer(player4);
             team.AddNewPlayer(player5);
             team.AddNewPlayer(player7);
             team.AddNewPlayer(player8);
             team.AddNewPlayer(player9);
             team.AddNewPlayer(player10);
             team.AddNewPlayer(player11);
             team.AddNewPlayer(player12);
             team.AddNewPlayer(player13);
             team.AddNewPlayer(player14);
             team.AddNewPlayer(player15);
             var actualResult = team.AddNewPlayer(player16);
            var expectedResult = "No more positions available!";

            Assert.AreEqual(expectedResult, actualResult);
        }
        [Test]
        public void PickPlayer_ValidParameters()
        {
            FootballPlayer player = new FootballPlayer("PlayerName", 8, "Forward");
            FootballPlayer player2 = new FootballPlayer("PlayerName2", 8, "Forward");

            FootballTeam team = new FootballTeam("Chicago Bool", 20);
            team.AddNewPlayer(player);
            team.AddNewPlayer(player2);

            var expectedPlayer = team.PickPlayer("PlayerName");
            Assert.AreSame(expectedPlayer, player);
        }
        [Test]
        public void PlayerScore_IncreasesStatistics()
        {
            FootballPlayer player = new FootballPlayer("PlayerName", 8, "Forward");
            FootballPlayer player2 = new FootballPlayer("PlayerName2", 9, "Forward");
            FootballTeam team = new FootballTeam("Chicago bool", 20);
            team.AddNewPlayer(player);
            team.AddNewPlayer(player2);

            string actualOutput = team.PlayerScore(8);

            var expectedOutput = "PlayerName scored and now has 1 for this season!";

            Assert.AreEqual(actualOutput, expectedOutput);
        }

    }
}