using UnityEngine;

[System.Serializable]
public class NPC
{
    public string NPCName;
    public string NPCHierarchie;
    [TextArea(3,10)] public string[] NPCDilogue;
}