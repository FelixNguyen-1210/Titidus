using System;
using System.Collections.Generic;
using Titidus;
using UnityEngine;


namespace Titidus
{
    public class GameSimulation
    {
        public readonly Dictionary<ulong, GamePlayer> Players = new Dictionary<ulong, GamePlayer>();


        public GamePlayer GetPlayer(ulong playerId)
        {
            return Players.TryGetValue(playerId, out var player)
                ? player
                : null;
        }

        public void AddPlayer(GamePlayer player)
        {
            if (!Players.TryAdd(player.Id, player))
                throw new InvalidOperationException(
                    $"Player {player.Id} already exists.");
        }

        public void Reset()
        {
            Players.Clear();
        }
    }
}