using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "BookLibraryConfig", menuName = "ScriptableObjects/BookLibraryConfig")]
public class BookLibraryConfig : ScriptableObject
{
    public List<BookPageConfig> BookPageConfigs;
}