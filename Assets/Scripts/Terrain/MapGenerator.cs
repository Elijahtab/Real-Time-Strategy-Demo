﻿using UnityEngine;
using System.Collections;
using System.Threading;
using System.Collections.Generic;
using System;

public class MapGenerator : MonoBehaviour {

	public enum DrawMode {NoiseMap, ColourMap, Mesh, Collider};
	public DrawMode drawMode;

	public TerrainData terrainData;
	public NoiseData noiseData;
	public int seed;


	const int mapChunkSize = 111;
	[Range(0,2)]
	public int levelOfDetail;

	public AnimationCurve meshHeightCurve;

	public bool autoUpdate;

	public TerrainType[] regions;

	void OnValuesUpdated(){
		if (!Application.isPlaying){
			GenerateMapData();
		}
	}

	public MapData GenerateMapData() {
		float[,] noiseMap = Noise.GenerateNoiseMap (mapChunkSize, mapChunkSize, seed, noiseData.noiseScale, noiseData.octaves, noiseData.persistance, noiseData.lacunarity, noiseData.offset);

		Color[] colourMap = new Color[mapChunkSize * mapChunkSize];
		for (int y = 0; y < mapChunkSize; y++) {
			for (int x = 0; x < mapChunkSize; x++) {
				float currentHeight = noiseMap [x, y];
				for (int i = 0; i < regions.Length; i++) {
					if (currentHeight <= regions [i].height) {
						colourMap [y * mapChunkSize + x] = regions [i].colour;
						break;
					}
				}
			}
		}

		MapDisplay display = FindObjectOfType<MapDisplay> ();
		MeshData meshData = MeshGenerator.GenerateTerrainMesh (noiseMap, terrainData.meshHeightMultiplier, meshHeightCurve, levelOfDetail);
		if (drawMode == DrawMode.NoiseMap) {
			display.DrawTexture (TextureGenerator.TextureFromHeightMap (noiseMap));
		} else if (drawMode == DrawMode.ColourMap) {
			display.DrawTexture (TextureGenerator.TextureFromColourMap (colourMap, mapChunkSize, mapChunkSize));
		} else if (drawMode == DrawMode.Mesh) {
			MeshData visualMeshData = MeshGenerator.GenerateTerrainMesh(noiseMap, terrainData.meshHeightMultiplier, meshHeightCurve, levelOfDetail);
			display.DrawMesh(visualMeshData, TextureGenerator.TextureFromColourMap(colourMap, mapChunkSize, mapChunkSize));			
    		display.GenerateMeshCollider(visualMeshData);		
		} 
		return new MapData(noiseMap, colourMap);
	}

	void OnValidate() {
		if (terrainData != null){
			terrainData.OnValuesUpdated -= OnValuesUpdated;
			terrainData.OnValuesUpdated += OnValuesUpdated;
			
		}
		if (noiseData != null){
			noiseData.OnValuesUpdated -= OnValuesUpdated;
			noiseData.OnValuesUpdated += OnValuesUpdated;
			
		}
	}

}

[System.Serializable]
public struct TerrainType {
	public string name;
	public float height;
	public Color colour;
}

public struct MapData{
	public readonly float[,] heightMap;
	public readonly Color[] colourMap;
	public MapData (float[,] heightMap, Color[] colourMap){
		this.heightMap = heightMap;
		this.colourMap = colourMap;
	}
}