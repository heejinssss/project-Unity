using UnityEngine;

public static class ItemDatabase5
{
    public static Item5[] Items { get; private set; }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)] private static void Intialize() => Items = Resources.LoadAll<Item5>("Items/");
}
