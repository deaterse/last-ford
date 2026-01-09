using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GenerateMap : MonoBehaviour
{
    [Header("Configs")]
    [SerializeField] private MapConfig _mapConfig;
    [SerializeField] private NoiseConfig _noiseConfig;
    [SerializeField] private TilesConfig _tilesConfig;

    [Header("Renderer Components")]
    [SerializeField] private TerrainRenderer _terrainRenderer;

    private void Start()
    {
        TerrainGenerate();
    }

    public void StartGenerating()
    {
        SceneManager.LoadScene("SampleScene");
    }

    private void TerrainGenerate()
    {
        //Generate Grass 64x64
        _terrainRenderer.GenerateTerrain(_mapConfig.MapSize);
        //

        //Generate Heights
        HeightGenerator heightGenerator = new HeightGenerator(_noiseConfig);
        HeightMap heightMap = heightGenerator.GenerateHeightMap(_mapConfig.MapSize.x, _mapConfig.MapSize.y);

        _terrainRenderer.VisualizeHeightMap(heightMap);

        Debug.Log("Terrain succesfully generated.");
        //

        //Make a Terrain Map
        TerrainMap terrainMap = new TerrainMap(_mapConfig.MapSize.x, _mapConfig.MapSize.y);
        //

        //Generate River
        CatmullRomRiver riverGenerator = new CatmullRomRiver(terrainMap);
        terrainMap = riverGenerator.GenerateRivers();

        _terrainRenderer.VisualizeRiver(terrainMap);
        //
    }
}
