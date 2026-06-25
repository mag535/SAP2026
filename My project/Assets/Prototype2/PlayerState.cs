using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public enum PlayerStates {
        GAME,
        DIALOGUE,
        DESCRIPTION,
    }

    public PlayerStates initialPlayerState;
    public PlayerStates playerState = PlayerStates.GAME;

    void Awake() {
        playerState = initialPlayerState;
    }

    public PlayerStates GetCurrentState() {
        return playerState;
    }

    public void ChangeCurrentState(PlayerStates newState) {
        playerState = newState;
    }
}
