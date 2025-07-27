# Live Football World Cup Scoreboard

A simple, in-memory C# library for managing live football (soccer) matches and scores. Designed for clarity, testability, and adherence to SOLID principles.

## Features

- Start a new match with home and away teams (initial score 0-0)
- Update the score of an ongoing match
- Finish a match (removes it from the scoreboard)
- Get a summary of ongoing matches, ordered by total score and recency
- Comprehensive validation and error handling

## Installation

Add the project to your solution or reference the compiled DLL in your .NET 8+ application.

## Usage
```
using LiveScoreboard.ScoreboardLibrary;

// Create teams
var homeTeam = new Team("Argentina");
var awayTeam = new Team("Brazil");

// Create scoreboard
var scoreboard = new Scoreboard();

// Start a match
var match = scoreboard.StartMatch(homeTeam, awayTeam);

// Update score
scoreboard.UpdateScore(match.ID, 2, 1);

// Get summary
var summary = scoreboard.GetSummary();

// Finish match
scoreboard.EndMatch(match.ID);
```


## API Overview

### Team

- Properties
  - `Name`
    - Name must be non-empty and non-whitespace.
    - Name cannot be chnaged after creation

### Match

- Created via `Scoreboard.StartMatch`
- Properties:
  - `HomeTeam`,
  - `AwayTeam`,
  - `HomeScore`,
  - `AwayScore`,
  - `StartTime`,
  - `EndTime`,
  - `IsOngoing`,
  - `TotalScore`,
  - `ID`

### Scoreboard

- Methods
  - `StartMatch(Team home, Team away, DateTime? startTime = null)`
  - `UpdateScore(Guid matchId, int homeScore, int awayScore)`
  - `EndMatch(Guid matchId)`
  - `GetSummary()`

## Validation & Error Handling

- Teams must not be null, empty, or the same (case- and whitespace-insensitive)
- Cannot start a duplicate ongoing match
- Cannot update or end a non-existent or finished match
- Scores must be non-negative

## Running Tests

This project uses xUnit for unit testing. 
