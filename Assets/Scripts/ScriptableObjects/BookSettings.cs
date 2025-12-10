using UnityEngine;

[CreateAssetMenu(fileName = "Player Stats", menuName = "ScriptableObjects/BookSettings", order = 2)]
public class BookSettings : ScriptableObject
{
    public float MoveAnimationSpeed;
    public int FirstGameplayPageIndex;
    public int LastGameplayPageIndex;
    public AudioClip PageFlipAudioClip;
}