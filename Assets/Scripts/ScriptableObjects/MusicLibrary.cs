using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewMusicLibrary", menuName = "ScriptableObjects/MusicLibrary")]
public class MusicLibrary : ScriptableObject
{
    public List<MusicTrack> musicTracks;

    public MusicTrack GetMusicTrackForContext(MusicContext context)
    {
        return musicTracks.Find(t => t.context == context);
    }
}