using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class HeightMapVisualizer : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _heightOverlay;

    public void ColoringMap(HeightMap heightMap)
    {
        int width = heightMap.Width;
        int height = heightMap.Height;

        Texture2D heightTexture = MakeHeightTexture(heightMap);
        Sprite heightSprite = Sprite.Create(
            heightTexture,
            new Rect(0, 0, width, height),
            new Vector2(0.5f, 0.5f),
            pixelsPerUnit: 1
        );

        _heightOverlay.sprite = heightSprite;
        _heightOverlay.transform.position = new Vector3(width/2, height/2, 0);
    }

    private Texture2D MakeHeightTexture(HeightMap heightMap)
    {
        int width = heightMap.Width;
        int height = heightMap.Height;

        Texture2D heightTexture = new Texture2D(width, height);
        heightTexture.filterMode = FilterMode.Point;

        HeightColorMapper colMapper = new HeightColorMapper();

        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                Color color = Color.black;
                color.a = colMapper.GetColorFloat(heightMap.HeightData[x, y]);

                heightTexture.SetPixel(x, y, color);
            }
        }

        heightTexture.Apply();

        return heightTexture;
    }
}
