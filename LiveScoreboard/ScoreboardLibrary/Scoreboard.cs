using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveScoreboard.ScoreboardLibrary
{
    public class Scoreboard
    {
        private readonly List<Match> _matches = new();

        public Match StartMatch(Team homeTeam, Team awayTeam, DateTime? startTime = null)
        {
            if(_matches.Any(m => m.HomeTeam.Name == homeTeam.Name && m.AwayTeam.Name == awayTeam.Name && m.IsOngoing))
            {
                throw new ArgumentException("Error starting match: Match between these teams is already ongoing");
            }

            Match match = new Match(homeTeam, awayTeam, startTime);
            _matches.Add(match);
            return match;
        }

        public void UpdateScore(Guid ID, int homeScore, int awayScore)
        {
            Match match = _matches.Any(m => m.ID == ID) ? _matches.First(m => m.ID == ID) : throw new Exception("Error updating score: Match does not exist");
            if (match.IsOngoing)
            {
                match.UpdateScore(homeScore, awayScore);
            }
            else
            {
                throw new Exception("Error updating score: Match is not ongoing, cannot update score.");
            }
        }

        public List<Match> GetSummary()
        {
            return _matches.Where(m => m.IsOngoing)
                .OrderByDescending(x=>x.TotalScore)
                .ThenByDescending(x=>x.StartTime)
                .ToList();
        }

        public void EndMatch(Guid ID)
        {
            Match match = _matches.Any(m => m.ID == ID) ? _matches.First(m => m.ID == ID) : throw new Exception("Error ending match: Match does not exist");
            match.EndMatch();
        }
    }
}
