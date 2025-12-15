using UnityEngine;

[CreateAssetMenu(fileName = "BookUIConfig", menuName = "ScriptableObjects/BookUIConfig")]
public class BookUIConfig : ScriptableObject
{
    public GameObject PagesUICanvasPrefab;

    public GameObject PageBorderPrefab;
    
    public GameObject PreviousPageButtonPrefab;
    public GameObject NextPageButtonPrefab;
    
    public GameObject PageNumberLeftPrefab;
    public GameObject PageNumberRightPrefab;
    
    public Vector3 PreviousPageButtonOffset = new Vector3(-860f, 0f, 0f);
    public Vector3 NextPageButtonOffset = new Vector3(860f, 0f, 0f);
    public Vector3 PageNumberLeftOffset = new Vector3(-860f, -500f, 0f);
    public Vector3 PageNumberRightOffset = new Vector3(860f, -500f, 0f);
}