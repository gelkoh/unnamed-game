using UnityEngine;

public enum PageID
{
    Cover,
    Endpaper,
    FrontisPieceAndTitlepage,
    Chapter1Introduction,
    Chapter1Level1Gameplay,
    Chapter1Level2Gameplay
}

[CreateAssetMenu(fileName = "BookPageConfig", menuName = "ScriptableObjects/BookPageConfig")]
public class BookPageConfig : ScriptableObject
{
    public PageID ID;
    public bool ShowPageBorder = true;
    
    public bool ShowNextPageButton = true;
    public bool ShowPreviousPageButton = true;
    public bool ShowPageNumberLeft = true;
    public bool ShowPageNumberRight = true;
}