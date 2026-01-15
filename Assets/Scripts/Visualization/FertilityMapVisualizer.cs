using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class FertilityMapVisualizer : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _fertilityOverlay;

    public void ColoringMap(FertilityMap fertilityMap)
    {
        int width = fertilityMap.Width;
        int height = fertilityMap.Height;

        Texture2D fertilityTexture = MakeFertilityTexture(fertilityMap);

        Sprite fertilitySprite = Sprite.Create(
            fertilityTexture,
            new Rect(0, 0, width, height),
            new Vector2(0.5f, 0.5f),
            pixelsPerUnit: 1
        );

        _fertilityOverlay.sprite = fertilitySprite;
        _fertilityOverlay.transform.position = new Vector3(width/2, height/2, 0);
    }

    private Texture2D MakeFertilityTexture(FertilityMap fertilityMap)
    {
        int width = fertilityMap.Width;
        int height = fertilityMap.Height;

        Texture2D fertilityTexture = new Texture2D(width, height);
        fertilityTexture.filterMode = FilterMode.Point;

        FertilityColorMapper colMapper = new FertilityColorMapper();

        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                if(fertilityMap.FertilityData[x, y] != 0)
                {
                    Color color = Color.green;
                    float colorA = colMapper.GetColorFloat(fertilityMap.FertilityData[x, y]);
                    color.a = colorA;

                    fertilityTexture.SetPixel(x, y, color);
                }
                else
                {
                    Color color = Color.green;
                    color.a = 0;
                    fertilityTexture.SetPixel(x, y, color);
                }
            }
        }

        fertilityTexture.Apply();

        return fertilityTexture;
    }
}
