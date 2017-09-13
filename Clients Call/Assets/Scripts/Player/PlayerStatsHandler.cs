using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerStatsHandler : Singleton<PlayerStatsHandler> {
    private Dictionary<string, PlayerStats> _playerData = new Dictionary<string, PlayerStats>();

    public Dictionary<string, PlayerStats> PlayerData {
        get { return _playerData; }
    }

    private void Awake() {
        // Add player and PlayerStat class to dictionary.
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < players.Length; i++) {
            GameObject player = players[i];
            _playerData.Add(player.name, new PlayerStats());
        }
    }
}
