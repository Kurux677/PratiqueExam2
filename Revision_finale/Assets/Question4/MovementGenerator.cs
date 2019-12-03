using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MovementGenerator : MonoBehaviour {

    public int mapWidth = 256;
    public float noiseScale;
    public int octaves;
    [Range(0, 1)]
    public float persistance;
    public float lacunarity;
    public bool autoUpdate;
    public int seed;
    public Vector2 offset;

    public int maxNumberOfFishes = 100;
    private float timeLastUpdate;

    private float[,] noiseMapX;
    private float[,] noiseMapY;
    private void Start() {
        noiseMapX = Noise.GenerateNoiseMap(mapWidth, mapWidth, seed, noiseScale, octaves, persistance, lacunarity, offset);
        noiseMapY = Noise.GenerateNoiseMap(mapWidth, mapWidth, seed, noiseScale, octaves, persistance, lacunarity, offset);
        timeLastUpdate = Time.time;
    }

    private void Update() {
        foreach (Fish fish in FindObjectsOfType<Fish>()) {
            //Code pour le mouvement des poissons
            float forceX = noiseMapX[(int)Math.Abs(fish.transform.position.x)>100? 100:(int)Math.Abs(fish.transform.position.x), (int)Math.Abs(fish.transform.position.y) > 100 ? 100 : (int)Math.Abs(fish.transform.position.y)];
            float forceY = noiseMapY[(int)Math.Abs(fish.transform.position.x) > 100 ? 100 : (int)Math.Abs(fish.transform.position.x), (int)Math.Abs(fish.transform.position.y) > 100 ? 100 : (int)Math.Abs(fish.transform.position.y)];
            fish.AddForceX(forceX-0.5f);
            fish.AddForceY(forceY - 0.5f);
        }
        if (Time.time - timeLastUpdate > 5) {
            //Code pour nouveau mouvement
            noiseMapX = Noise.GenerateNoiseMap(mapWidth, mapWidth, seed, noiseScale, octaves, persistance, lacunarity, offset);
            noiseMapY = Noise.GenerateNoiseMap(mapWidth, mapWidth, seed, noiseScale, octaves, persistance, lacunarity, offset);

            timeLastUpdate = Time.time;
        }
       
    }
    


    private void OnValidate() {
        if (mapWidth < 1) {
            mapWidth = 1;
        }
        if (lacunarity < 1) {
            lacunarity = 1;
        }
        if (octaves < 0) {
            octaves = 0;
        }

    }


}

