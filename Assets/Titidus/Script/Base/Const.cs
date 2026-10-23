using UnityEngine;

namespace Titidus
{

    public enum GamePhase
    {
        Preparation,
        Morning,
        Discussion,
        Voting,
        Resolution,

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
        AvatarIndex,
        Language
    }

    public enum Party
    {
        Village,
        Wolf,
        Neutral,
    }
    public enum CardId
    {
        Villager,
        Werewolf,
        Seer,
        Guard
    }
}
