using UnityEngine;

public static class Palette
{
    public static readonly Color Red = new Color32(0xef, 0x9a, 0x9a, 0xff);
    public static readonly Color Yellow = new Color32(0xff, 0xfe, 0xac, 0xff);
    public static readonly Color Green = new Color32(0xa5, 0xd6, 0xa7, 0xff);
    public static readonly Color Button = new Color32(0xd0, 0xd3, 0xd4, 0xff);

    public static readonly Color PrimaryText = new(1f, 1f, 1f, 0.87f);
    public static readonly Color SecondaryText = new(1f, 1f, 1f, 0.6f);
    public static readonly Color Transparent = new(1f, 1f, 1f, 0.5f);

    public static readonly Color ValidPlacement = new(130 / 255f, 224 / 255f, 170 / 255f, .75f);
    public static readonly Color InvalidPlacement = new(241 / 255f, 148 / 255f, 138 / 255f, .75f);
}
