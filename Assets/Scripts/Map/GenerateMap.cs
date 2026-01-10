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
    [SerializeField] private ResourcesRenderer _resourcesRenderer;

    private HeightMap _heightMap;
    private TerrainMap _terrainMap;

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

        //Generate Heights
        HeightGenerator heightGenerator = new HeightGenerator(_noiseConfig);
        _heightMap = heightGenerator.GenerateHeightMap(_mapConfig.MapSize.x, _mapConfig.MapSize.y);

        _terrainRenderer.VisualizeHeightMap(_heightMap);

        Debug.Log("Terrain succesfully generated.");

        //Make a Terrain Map
        _terrainMap = new TerrainMap(_mapConfig.MapSize.x, _mapConfig.MapSize.y);

        //Generate River
        CatmullRomRiver riverGenerator = new CatmullRomRiver(_terrainMap);
        _terrainMap = riverGenerator.GenerateRivers();

        _terrainRenderer.VisualizeRiver(_terrainMap);

        Debug.Log("River succesfully generated.");

        //Generate Forests
        ForestsGenerator forestsGenerator = new ForestsGenerator(_terrainMap);
        forestsGenerator.GenerateForests();

        _resourcesRenderer.VisualizeForests(_terrainMap);

        Debug.Log("Forests succesfully generated.");
    }
}
