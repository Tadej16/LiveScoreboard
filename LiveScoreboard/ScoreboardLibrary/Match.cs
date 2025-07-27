using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace LiveScoreboard.ScoreboardLibrary
{
    public class Match
    {
        public readonly Guid ID = Guid.NewGuid();

        public Team HomeTeam { get; set; }
        public int HomeScore { get; private set; }
        
        public Team AwayTeam { get; set; }
        public int AwayScore { get; private set; }
        
        public int TotalScore 
        {
            get
            {
                return HomeScore + AwayScore;
            }
        }

        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; set; }
        public bool IsOngoing { 
            get 
            {
                //Match is ongoing if the EndTime is not set or is in the future
                bool hasStarted = StartTime <= DateTime.Now;
                bool hasEnded = EndTime != default(DateTime) && EndTime <= DateTime.Now;
                return hasStarted && !hasEnded;
            } 
        }

        public Match()
        {
        }
        public Match(Team homeTeam, Team awayTeam, DateTime? startTime)
        {
            if (homeTeam == null || awayTeam == null)
            {
                throw new ArgumentNullException("Teams cannot be null");
            }
            if (homeTeam.Name.Trim().ToUpper() == awayTeam.Name.Trim().ToUpper())
            {
                throw new ArgumentException("Home and away teams cannot be the same");
            }

            HomeTeam = homeTeam;
            AwayTeam = awayTeam;

            if (startTime == null || startTime == default(DateTime))
            {
                this.StartTime = DateTime.Now;
            }
            else
            {
                this.StartTime = startTime.Value;
            }
            EndTime = default(DateTime);
        }

        public void UpdateScore(int homeScore, int awayScore)
        {
            if (homeScore < 0 || awayScore < 0)
            {
                throw new ArgumentException("Error updating score: Scores cannot be negative.");
            }
            HomeScore = homeScore;
            AwayScore = awayScore;
        }

        public void EndMatch()
        {
            if(DateTime.Now < StartTime)
            {
                throw new InvalidOperationException("Error ending match: Match cannot end before it starts.");
            }
            EndTime = DateTime.Now;
        }
    }
}
