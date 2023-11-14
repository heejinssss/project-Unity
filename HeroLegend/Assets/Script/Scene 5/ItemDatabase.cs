using UnityEngine;

public static class ItemDatabase
{
    public static Item[] Items { get; private set; }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)] private static void Intialize() => Items = Resources.LoadAll<Item>("Items/");
}
