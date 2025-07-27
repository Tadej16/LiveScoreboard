using LiveScoreboard.ScoreboardLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreboardLibrary.Tests
{
    public class TeamTests
    {
        [Fact]
        public void Team_CanBeCreatedWithValidName()
        {
            // Arrange
            string teamName = "Slovenia";
            // Act
            Team team = new Team(teamName);
            // Assert
            Assert.Equal(teamName, team.Name);
        }
        [Fact]
        public void Team_CannotBeCreatedWithNullOrEmptyName()
        {
            // Arrange
            string nullName = null;
            string emptyName = "";
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Team(nullName));
            Assert.Throws<ArgumentException>(() => new Team(emptyName));
        }
        [Fact]
        public void Team_CannotBeCreatedWithWhitespaceName()
        {
            // Arrange
            string whitespaceName = "   ";
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Team(whitespaceName));
        }
    }
}
