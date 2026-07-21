using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

public class SpriteCombiner
{
    [MenuItem("Tools/Combine Selected Sprites")]
    static void Combine()
    {
        SpriteRenderer[] renderers = Selection.GetFiltered<SpriteRenderer>(SelectionMode.Deep);

        if (renderers.Length == 0)
        {
            Debug.LogError("Selecione um ou mais SpriteRenderers.");
            return;
        }

        float minX = float.MaxValue;
        float minY = float.MaxValue;
        float maxX = float.MinValue;
        float maxY = float.MinValue;

        foreach (var sr in renderers)
        {
            Bounds b = sr.bounds;

            minX = Mathf.Min(minX, b.min.x);
            minY = Mathf.Min(minY, b.min.y);

            maxX = Mathf.Max(maxX, b.max.x);
            maxY = Mathf.Max(maxY, b.max.y);
        }

        float ppu = renderers[0].sprite.pixelsPerUnit;

        int width = Mathf.CeilToInt((maxX - minX) * ppu);
        int height = Mathf.CeilToInt((maxY - minY) * ppu);

        Texture2D output = new Texture2D(width, height, TextureFormat.RGBA32, false);

        Color[] clear = new Color[width * height];
        for (int i = 0; i < clear.Length; i++)
            clear[i] = new Color(0,0,0,0);

        output.SetPixels(clear);

        foreach (var sr in renderers)
        {
            Sprite sprite = sr.sprite;

            Texture2D tex = sprite.texture;

            Rect rect = sprite.textureRect;

            Color[] pixels = tex.GetPixels(
                (int)rect.x,
                (int)rect.y,
                (int)rect.width,
                (int)rect.height);

            int sw = (int)rect.width;
            int sh = (int)rect.height;

            Texture2D spriteTex = new Texture2D(sw, sh, TextureFormat.RGBA32, false);
            spriteTex.SetPixels(pixels);
            spriteTex.Apply();

            if (sr.flipX)
                FlipHorizontal(spriteTex);

            if (sr.flipY)
                FlipVertical(spriteTex);

            int angle = Mathf.RoundToInt(sr.transform.eulerAngles.z);

            if (angle == 90)
                spriteTex = Rotate90(spriteTex);

            else if (angle == 180)
                spriteTex = Rotate180(spriteTex);

            else if (angle == 270)
                spriteTex = Rotate270(spriteTex);

            Vector2 pivot = sprite.pivot;

            int drawX =
                Mathf.RoundToInt((sr.transform.position.x - minX) * ppu - pivot.x);

            int drawY =
                Mathf.RoundToInt((sr.transform.position.y - minY) * ppu - pivot.y);

            for (int x = 0; x < spriteTex.width; x++)
            {
                for (int y = 0; y < spriteTex.height; y++)
                {
                    int px = drawX + x;
                    int py = drawY + y;

                    if (px < 0 || py < 0 || px >= width || py >= height)
                        continue;

                    Color c = spriteTex.GetPixel(x, y);

                    if (c.a > 0)
                        output.SetPixel(px, py, c);
                }
            }
        }

        output.Apply();

        string path = "Assets/CombinedSprites.png";

        File.WriteAllBytes(path, output.EncodeToPNG());

        AssetDatabase.Refresh();

        Debug.Log("PNG criado em: " + path);
    }

    static void FlipHorizontal(Texture2D tex)
    {
        int w = tex.width;
        int h = tex.height;

        for (int y = 0; y < h; y++)
        {
            for (int x = 0; x < w / 2; x++)
            {
                Color a = tex.GetPixel(x, y);
                Color b = tex.GetPixel(w - x - 1, y);

                tex.SetPixel(x, y, b);
                tex.SetPixel(w - x - 1, y, a);
            }
        }

        tex.Apply();
    }

    static void FlipVertical(Texture2D tex)
    {
        int w = tex.width;
        int h = tex.height;

        for (int y = 0; y < h / 2; y++)
        {
            for (int x = 0; x < w; x++)
            {
                Color a = tex.GetPixel(x, y);
                Color b = tex.GetPixel(x, h - y - 1);

                tex.SetPixel(x, y, b);
                tex.SetPixel(x, h - y - 1, a);
            }
        }

        tex.Apply();
    }

    static Texture2D Rotate90(Texture2D tex)
    {
        Texture2D n = new Texture2D(tex.height, tex.width);

        for (int x = 0; x < tex.width; x++)
            for (int y = 0; y < tex.height; y++)
                n.SetPixel(tex.height - y - 1, x, tex.GetPixel(x, y));

        n.Apply();
        return n;
    }

    static Texture2D Rotate180(Texture2D tex)
    {
        Texture2D n = new Texture2D(tex.width, tex.height);

        for (int x = 0; x < tex.width; x++)
            for (int y = 0; y < tex.height; y++)
                n.SetPixel(tex.width - x - 1, tex.height - y - 1, tex.GetPixel(x, y));

        n.Apply();
        return n;
    }

    static Texture2D Rotate270(Texture2D tex)
    {
        Texture2D n = new Texture2D(tex.height, tex.width);

        for (int x = 0; x < tex.width; x++)
            for (int y = 0; y < tex.height; y++)
                n.SetPixel(y, tex.width - x - 1, tex.GetPixel(x, y));

        n.Apply();
        return n;
    }
}