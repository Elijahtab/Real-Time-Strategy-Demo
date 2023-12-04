﻿﻿using UnityEngine;
using System.Collections;
using System.Threading;
using System.Collections.Generic;
using System;

public class MapGenerator : MonoBehaviour {

	public enum DrawMode {NoiseMap, Mesh, Collider};
	public DrawMode drawMode;

	public TerrainData terrainData;
	public NoiseData noiseData;
	public TextureData textureData;
	public Material terrainMaterial;
	public int seed;


	const int mapChunkSize = 111;
	[Range(0,2)]
	public int levelOfDetail;

	public bool autoUpdate;

	void OnValuesUpdated(){
		if (!Application.isPlaying){
			GenerateMapData();
		}
	}
	void OnTextureValuesUpdated(){
		textureData.ApplyToMaterial (terrainMaterial);
	}

	public MapData GenerateMapData() {
		float[,] noiseMap = Noise.GenerateNoiseMap (mapChunkSize, mapChunkSize, seed, noiseData.noiseScale, noiseData.octaves, noiseData.persistance, noiseData.lacunarity, noiseData.offset);

		for (int y = 0; y < mapChunkSize; y++) {
			for (int x = 0; x < mapChunkSize; x++) {
				float currentHeight = noiseMap [x, y];
			}
		}

		MapDisplay display = FindObjectOfType<MapDisplay> ();
		MeshData meshData = MeshGenerator.GenerateTerrainMesh (noiseMap, terrainData.meshHeightMultiplier, terrainData.meshHeightCurve, levelOfDetail);
		if (drawMode == DrawMode.NoiseMap) {
			display.DrawTexture (TextureGenerator.TextureFromHeightMap (noiseMap));
		} else if (drawMode == DrawMode.Mesh) {
			MeshData visualMeshData = MeshGenerator.GenerateTerrainMesh(noiseMap, terrainData.meshHeightMultiplier, terrainData.meshHeightCurve, levelOfDetail);
			display.DrawMesh(visualMeshData);			
    		display.GenerateMeshCollider(visualMeshData);		
		} 
		textureData.UpdateMeshHeights(terrainMaterial, terrainData.minHeight, terrainData.maxHeight);
		return new MapData(noiseMap);
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
		if (textureData != null){
			textureData.OnValuesUpdated -= OnTextureValuesUpdated;
			textureData.OnValuesUpdated += OnTextureValuesUpdated;
		}
	}

}


public struct MapData{
	public readonly float[,] heightMap;
	public MapData (float[,] heightMap){
		this.heightMap = heightMap;
	}
}