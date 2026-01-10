using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class RiverGenerator
{
    public void GenerateRivers(TerrainMap _terrainMap)
    {
        List<Vector3Int> mainPoints = GenerateRiverControlPoints(_terrainMap);
        GenerateRiver(mainPoints, _terrainMap);
    }
    private Vector3Int RandomBorderPosYLeft(Vector2Int size)
    {
        List<Vector3Int> borderPosY = new List<Vector3Int>();

        for(int y = 1; y < size.y; y++)
        {
            Vector3Int currentPos = new Vector3Int(0, y + 1, 0);

            borderPosY.Add(currentPos);
        }

        return borderPosY[Random.Range(0, borderPosY.Count)];
    }

    private Vector3Int RandomBorderPosYRight(Vector2Int size)
    {
        List<Vector3Int> borderPosY = new List<Vector3Int>();

        for(int y = 1; y < size.y; y++)
        {
            Vector3Int currentPos = new Vector3Int(size.x - 1, y - 1, 0);

            borderPosY.Add(currentPos);
        }

        return borderPosY[Random.Range(0, borderPosY.Count)];
    }

    List<Vector3Int> GenerateRiverControlPoints(TerrainMap _terrainMap)
    {
        List<Vector3Int> points = new List<Vector3Int>();

        Vector3Int p0 = RandomBorderPosYLeft(new Vector2Int(_terrainMap.Width, _terrainMap.Height));
        Vector3Int p1 = new Vector3Int(Random.Range(20, 53), Random.Range(0, _terrainMap.Height - 1));
        Vector3Int p2 = new Vector3Int(Random.Range(p1.x, 53), Random.Range(0, _terrainMap.Height - 1));
        Vector3Int p3 = RandomBorderPosYRight(new Vector2Int(_terrainMap.Width, _terrainMap.Height));

        points.Add(p0);
        points.Add(p1);
        points.Add(p2);
        points.Add(p3);

        return points;
    }

    public void GenerateRiver(List<Vector3Int> mainPoints, TerrainMap _terrainMap)
    {
        if (mainPoints.Count < 4) return;
        
        for (int i = 0; i < mainPoints.Count - 3; i++)
        {
            Vector3Int p0 = mainPoints[i];
            Vector3Int p1 = mainPoints[i + 1];
            Vector3Int p2 = mainPoints[i + 2];
            Vector3Int p3 = mainPoints[i + 3];
            
            List<Vector3Int> curvePoints = GetCurveSegment(p0, p1, p2, p3, 10);

            DrawLineBetweenPoints(p0, p1, _terrainMap);
            DrawLineBetweenPoints(p2, p3, _terrainMap);
            DrawRiverThroughPoints(curvePoints, _terrainMap);
        }
    }
    
    private List<Vector3Int> GetCurveSegment(
        Vector3Int p0, Vector3Int p1, 
        Vector3Int p2, Vector3Int p3, 
        int segments)
    {
        List<Vector3Int> points = new List<Vector3Int>();

        float step = 1f / segments;
        for(int i = 0; i <= segments; i++)
        {
            float t = (float)i / segments;

            float t2 = t * t;
            float t3 = t2 * t;
            
            float x = 0.5f * (
                (2f * p1.x) +
                (-p0.x + p2.x) * t +
                (2f*p0.x - 5f*p1.x + 4f*p2.x - p3.x) * t2 +
                (-p0.x + 3f*p1.x - 3f*p2.x + p3.x) * t3
            );
            
            float y = 0.5f * (
                (2f * p1.y) +
                (-p0.y + p2.y) * t +
                (2f*p0.y - 5f*p1.y + 4f*p2.y - p3.y) * t2 +
                (-p0.y + 3f*p1.y - 3f*p2.y + p3.y) * t3
            );

            Vector2 currentPoint = new Vector2(x, y);
            Vector3Int currentPointInt = new Vector3Int(Mathf.RoundToInt(currentPoint.x), Mathf.RoundToInt(currentPoint.y), 0);

            points.Add(currentPointInt);
        }
        
        return points;
    }

    private void DrawRiverThroughPoints(List<Vector3Int> points, TerrainMap _terrainMap)
    {
        for (int i = 0; i < points.Count - 1; i++)
        {
            DrawLineBetweenPoints(points[i], points[i + 1], _terrainMap);
        }
    }

    private void DrawLineBetweenPoints(Vector3Int start, Vector3Int end, TerrainMap _terrainMap)
    {
        Vector3Int delta = end - start;
        Vector3Int absDelta = new Vector3Int(Mathf.Abs(delta.x), Mathf.Abs(delta.y), 0);
        
        bool xIsMajor = absDelta.x > absDelta.y;
        int majorStep = xIsMajor ? absDelta.x : absDelta.y;
        int minorStep = xIsMajor ? absDelta.y : absDelta.x;
        
        int stepX = (int)Mathf.Sign(delta.x);
        int stepY = (int)Mathf.Sign(delta.y);
        
        Vector3Int currentPos = start;
        float error = 0;
        float errorStep = (float)minorStep / majorStep;
        
        int thickness = 4;
        Vector3Int thicknessDir = xIsMajor ? new Vector3Int(0, 1, 0) : new Vector3Int(1, 0, 0);
        
        for(int i = 0; i <= majorStep; i++)
        {
            for(int t = -(thickness/2); t <= (thickness/2); t++)
            {
                Vector3Int waterPoint = currentPos + thicknessDir * t;

                TileData waterTile = new TileData(
                    TerrainType.Water,
                    Resource.None
                );

                if(waterPoint.x >= 0 && waterPoint.x < _terrainMap.Width && waterPoint.y >= 0 && waterPoint.y < _terrainMap.Height)
                {
                    _terrainMap.SetTile(waterPoint.x, waterPoint.y, waterTile);
                }
            }
            
            if(xIsMajor) currentPos.x += stepX;
            else currentPos.y += stepY;
            
            error += errorStep;
            if(error >= 0.5f)
            {
                if(xIsMajor) currentPos.y += stepY;
                else currentPos.x += stepX;
                error -= 1f;
            }
        }
    }
}
