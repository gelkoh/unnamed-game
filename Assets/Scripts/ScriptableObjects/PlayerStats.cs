using UnityEngine;

[CreateAssetMenu(fileName = "Player Stats", menuName = "ScriptableObjects/PlayerStats", order = 1)]
public class PlayerStats : ScriptableObject
{
    public float MovementSpeed;
    public float MaxJumpHeight;
    public float MaxJumpTime;
}