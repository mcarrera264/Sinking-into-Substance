using UnityEngine;
using TMPro;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "NewSpeaker", menuName = "Data/New Speaker")]
[System.Serializable]
public class Speaker : ScriptableObject
{
    public string speakerName;
    public Color nameColor;
    public Color textColor;
    public bool bold;
    public bool italic;
    public AudioMixerGroup voiceMixerGroup;

}
