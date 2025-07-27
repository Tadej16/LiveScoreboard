using LiveScoreboard.ScoreboardLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreboardLibrary.Tests
{
    public class MatchTests
    {
        [Fact]
        public void Match_CanBeCreatedWithTeamsAndStartTime()
        {
            // Arrange
            var homeTeam = new Team("Slovenia");
            var awayTeam = new Team("Croatia");
            DateTime startTime = DateTime.Now.AddMinutes(1);
            // Act
            Match match = new Match(homeTeam, awayTeam, startTime);
            // Assert
            Assert.Equal(homeTeam, match.HomeTeam);
            Assert.Equal(awayTeam, match.AwayTeam);
            Assert.Equal(startTime, match.StartTime);
            Assert.False(match.IsOngoing); // Match is not ongoing at creation
        }

        [Fact]
        public void Match_CannotBeCreatedWithNullTeams()
        {
            // Arrange
            Team nullTeam = null;
            var awayTeam = new Team("Croatia");
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new Match(nullTeam, awayTeam, DateTime.Now));
            Assert.Throws<ArgumentNullException>(() => new Match(awayTeam, nullTeam, DateTime.Now));
        }

        [Fact]
        public void Match_CannotBeCreatedWithSameTeams()
        {
            // Arrange
            var homeTeam = new Team("Slovenia");
            var awayTeam = new Team("Slovenia");
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Match(homeTeam, awayTeam, DateTime.Now));
        }

        [Fact]
        public void Match_CannotBeCreatedWithSameTeamsWithDifferentCases()
        {
            // Arrange
            var homeTeam = new Team("Slovenia");
            var awayTeam = new Team("sLOVEnia");
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Match(homeTeam, awayTeam, DateTime.Now));
        }

        [Fact]
        public void Match_CannotBeCreatedWithSameTeamsWithWhitespaces()
        {
            // Arrange
            var homeTeam = new Team("Slovenia");
            var awayTeam = new Team("   Slovenia   ");
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Match(homeTeam, awayTeam, DateTime.Now));
        }

    }
}
