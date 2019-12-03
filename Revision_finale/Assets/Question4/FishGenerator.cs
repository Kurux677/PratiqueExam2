using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FishGenerator : MonoBehaviour {

    public int mapWidth = 256;
    public float noiseScale;
    public int octaves;
    [Range(0,1)]
    public float persistance;
    public float lacunarity;
    public bool autoUpdate;
    public int seed;
    public Vector2 offset;

    public int maxNumberOfFishes = 100;
    public GameObject fishPrefab;

    private void Start() {
       
    }

    public void GenerateFishes() {
        DeleteFishes();
        CreateFishes();
        
        
        
    }

    private void CreateFishes()
    {
        float[,] text = Noise.GenerateNoiseMap(mapWidth, mapWidth, seed, noiseScale, octaves, persistance, lacunarity, offset);
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapWidth; y++)
            {
                if (text[x,y] >=0.7)
                {
                    if (FindObjectsOfType<Fish>().Length< maxNumberOfFishes)
                    {
                        AddFishInFishes(x,y);
                    }
                }
            }
        }
    }

    private void AddFishInFishes(int x, int y)
    {
        GameObject fish = Instantiate(fishPrefab, new Vector3(x, y), transform.rotation);
    }

    public void DeleteFishes() {
        foreach (Fish fish in FindObjectsOfType<Fish>()) {
            DestroyImmediate(fish.gameObject);
        }
    }
    
  
    private void OnValidate() {
        if(mapWidth< 1) {
            mapWidth = 1;
        }
        if (lacunarity < 1) {
            lacunarity = 1;
        }
        if(octaves < 0) {
            octaves = 0;
        }

    }
    
    
}

