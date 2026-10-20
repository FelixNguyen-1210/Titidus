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
        AvatarIndex,
        Language
    }

    public enum Party
    {
        Human,
        Wolf,
        Couple,
    }
    public struct PlayerProfile
    {
        public string name;
        public string avatarIndex;
        
    }
}
