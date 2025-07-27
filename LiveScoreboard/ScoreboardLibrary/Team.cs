using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveScoreboard.ScoreboardLibrary
{
    public class Team
    {
        private string name;

        public string Name { get => name; }

        public Team()
        {
        }

        public Team(string name)
        {
            if (name == null)
            {
                throw new ArgumentException("Team name cannot be null.");
            }
            if (string.IsNullOrEmpty(name.Trim()))
            {
                throw new ArgumentException("Team name cannot be empty or whitespace.");
            }
            this.name = name.Trim();
        }
    }
}
