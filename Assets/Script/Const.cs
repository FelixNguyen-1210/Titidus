using UnityEngine;

namespace Titidus
{

    public enum GamePhase
    {
        Prepare,
        Morning,
        Discussion,
        Voting,
        Night,
        End
    }
    public enum RoleType
    {
        Villager,
        Werewolf,
        Seer,
        Guard,
        Witch
    }
    public enum PlayerData
    {
        Name,
        Avatar,
        Language
    }
}
