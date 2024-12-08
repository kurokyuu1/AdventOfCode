namespace AdventOfCode._2022.Models.DayTwo;

internal sealed record GameHandResult(PlayerHand Opponent, PlayerHand Player);

internal sealed record GameOutcomeResult(PlayerHand Hand, Outcome Outcome);
