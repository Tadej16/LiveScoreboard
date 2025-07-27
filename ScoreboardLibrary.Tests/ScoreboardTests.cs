using LiveScoreboard.ScoreboardLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreboardLibrary.Tests
{
    public class ScoreboardTests
    {

        #region StartMatch Tests

        [Fact]
        public void StartMatch_AddMatchToScoreboard()
        {
            // Arrange
            var scoreboard = new Scoreboard();
            var homeTeam = new Team("Slovenia");
            var awayTeam = new Team("Croatia");

            // Act
            Match match = scoreboard.StartMatch(homeTeam, awayTeam);

            // Assert
            var matches = scoreboard.GetSummary();
            Assert.Single(matches);
            Assert.Equal(homeTeam.Name, matches[0].HomeTeam.Name);
            Assert.Equal(awayTeam.Name, matches[0].AwayTeam.Name);

            Assert.Equal(0, matches[0].HomeScore);
            Assert.Equal(0, matches[0].AwayScore);
        }

        [Fact]
        public void StartMatch_TeamsCannotBeNull()
        {
            // Arrange
            Scoreboard scoreboard = new Scoreboard();
            Team nullTeam = null;
            Team fullTeam = new Team("Croatia");
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => scoreboard.StartMatch(nullTeam, fullTeam));
            Assert.Throws<ArgumentNullException>(() => scoreboard.StartMatch(fullTeam, nullTeam));
        }

        [Fact]
        public void StartMatch_TeamsCannotBeSame()
        {
            // Arrange
            var scoreboard = new Scoreboard();
            var homeTeam = new Team("Slovenia");
            var awayTeam = new Team("Slovenia");
            // Act & Assert
            Assert.Throws<ArgumentException>(() => scoreboard.StartMatch(homeTeam, awayTeam));
        }

        [Fact]
        public void StartMatch_MatchIsOngoing()
        {
            // Arrange
            var scoreboard = new Scoreboard();
            var homeTeam = new Team("Slovenia");
            var awayTeam = new Team("Croatia");
            Match match1 = scoreboard.StartMatch(homeTeam, awayTeam);
            
            // Act & Assert
            Assert.Throws<ArgumentException>(() => scoreboard.StartMatch(homeTeam, awayTeam));
        }
        #endregion

        #region UpdateScore Tests

        [Fact]
        public void UpdateScore_UpdateMatchScore()
        {
            // Arrange
            var scoreboard = new Scoreboard();
            var homeTeam = new Team("Slovenia");
            var awayTeam = new Team("Croatia");
            Match match = scoreboard.StartMatch(homeTeam, awayTeam);
            
            // Act
            scoreboard.UpdateScore(match.ID, 2, 3);
            var matches = scoreboard.GetSummary();

            // Assert
            Assert.Single(matches);
            Assert.Equal(2, matches[0].HomeScore);
            Assert.Equal(3, matches[0].AwayScore);
        }

        [Fact]
        public void UpdateScore_MatchCannotBeNull()
        {
            // Arrange
            var scoreboard = new Scoreboard();
            var homeTeam = new Team("Slovenia");
            var awayTeam = new Team("Croatia");
            Match match = scoreboard.StartMatch(homeTeam, awayTeam);
            
            // Act & Assert
            Assert.Throws<Exception>(() => scoreboard.UpdateScore(Guid.NewGuid(), 2, 3));
        }

        [Fact]
        public void UpdateScore_ScoreCannotBeNegative()
        {
            // Arrange
            var scoreboard = new Scoreboard();
            var homeTeam = new Team("Slovenia");
            var awayTeam = new Team("Croatia");
            Match match = scoreboard.StartMatch(homeTeam, awayTeam);
            
            // Act & Assert
            Assert.Throws<ArgumentException>(() => scoreboard.UpdateScore(match.ID, -1, 3));
            Assert.Throws<ArgumentException>(() => scoreboard.UpdateScore(match.ID, 2, -3));
        }

        [Fact]
        public void UpdateScore_ScoreCannotBeUpdatedAfterMatchEnded()
        {
            // Arrange
            var scoreboard = new Scoreboard();
            var homeTeam = new Team("Slovenia");
            var awayTeam = new Team("Croatia");
            Match match = scoreboard.StartMatch(homeTeam, awayTeam);
            
            // Act
            scoreboard.UpdateScore(match.ID, 2, 3);
            scoreboard.EndMatch(match.ID);
            // Assert
            Assert.Throws<Exception>(() => scoreboard.UpdateScore(match.ID, 4, 5));
        }

        #endregion

        #region GetSummary Tests

        [Theory]
        [InlineData(
            "Mexico", "Canada", 
            "Spain", "Brazil", 
            "Germany", "France", 
            "Uruguay", "Italy", 
            "Argentina", "Australia"
            )]
        public void GetSummary_GetsCurrentlyActiveMathesInCorrectOrder(string c1, string c2, string c3, string c4, string c5, string c6, string c7, string c8, string c9, string c10)
        {
            // Arrange
            var scoreboard = new Scoreboard();

            var homeTeam1 = new Team(c1);
            var awayTeam1 = new Team(c2);
            
            var homeTeam2 = new Team(c3);
            var awayTeam2 = new Team(c4);
            
            var homeTeam3 = new Team(c5);
            var awayTeam3 = new Team(c6);
            
            var homeTeam4 = new Team(c7);
            var awayTeam4 = new Team(c8);

            var homeTeam5 = new Team(c9);
            var awayTeam5 = new Team(c10);

            // Act
            Match match1 = scoreboard.StartMatch(homeTeam1, awayTeam1);
            Match match2 = scoreboard.StartMatch(homeTeam2, awayTeam2);
            Match match3 = scoreboard.StartMatch(homeTeam3, awayTeam3);
            Match match4 = scoreboard.StartMatch(homeTeam4, awayTeam4);
            Match match5 = scoreboard.StartMatch(homeTeam5, awayTeam5);

            scoreboard.UpdateScore(match1.ID, 0, 5);
            scoreboard.UpdateScore(match2.ID, 10 ,2);
            scoreboard.UpdateScore(match3.ID, 2, 2);
            scoreboard.UpdateScore(match4.ID, 6, 6);
            scoreboard.UpdateScore(match5.ID, 3, 1);
            var matches = scoreboard.GetSummary();

            // Assert
            Assert.Equal(match4.ID, matches[0].ID);
            Assert.Equal(match2.ID, matches[1].ID);
            Assert.Equal(match1.ID, matches[2].ID);
            Assert.Equal(match5.ID, matches[3].ID);
            Assert.Equal(match3.ID, matches[4].ID);

        }

        [Theory]
        [InlineData("Mexico", "Canada", "Spain", "Brazil", "Germany", "France")]
        public void GetSummary_GetCorrectNumberOfMatchesInProgress(string c1, string c2, string c3, string c4, string c5, string c6)
        {
            // Arrange
            var scoreboard = new Scoreboard();
            var homeTeam1 = new Team(c1);
            var awayTeam1 = new Team(c2);
            
            var homeTeam2 = new Team(c3);
            var awayTeam2 = new Team(c4);
            
            var homeTeam3 = new Team(c5);
            var awayTeam3 = new Team(c6);
            
            // Act
            Match match1 = scoreboard.StartMatch(homeTeam1, awayTeam1);
            Match match2 = scoreboard.StartMatch(homeTeam2, awayTeam2);
            Match match3 = scoreboard.StartMatch(homeTeam3, awayTeam3);

            scoreboard.EndMatch(match1.ID);
            // Assert
            Assert.Equal(2, scoreboard.GetSummary().Count);
        }

        #endregion

        #region EndMatch Tests

        [Fact]
        public void EndMatch_MatchIsEnded()
        {
            // Arrange
            var scoreboard = new Scoreboard();
            var homeTeam = new Team("Slovenia");
            var awayTeam = new Team("Croatia");
            Match match = scoreboard.StartMatch(homeTeam, awayTeam);
            
            // Act
            scoreboard.EndMatch(match.ID);
            var matches = scoreboard.GetSummary();
            // Assert
            Assert.Empty(matches);
        }

        [Fact]
        public void EndMatch_MatchCannotBeNull()
        {
            // Arrange
            var scoreboard = new Scoreboard();
            var homeTeam = new Team("Slovenia");
            var awayTeam = new Team("Croatia");
            Match match = scoreboard.StartMatch(homeTeam, awayTeam);
            
            // Act & Assert
            Assert.Throws<Exception>(() => scoreboard.EndMatch(Guid.NewGuid()));
        }

        #endregion

    }
}
