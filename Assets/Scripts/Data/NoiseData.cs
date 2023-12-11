using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [CreateAssetMenu()]
public class NoiseData : UpdatableData
{
    //public NoiseData.NormalizedMode normalizedMode;
    public float noiseScale;

	public int octaves;
	[Range(0,1)]
	public float persistance;
	public float lacunarity;
	public Vector2 offset;


	public float treeNoiseScale;
	public int treeOctaves;
	[Range(0,1)]
	public float treePersistance;
	public float treeLacunarity;
	public Vector2 treeOffset;
	
    protected override void OnValidate(){
        if (lacunarity < 1) {
			lacunarity = 1;
		}
		if (octaves < 0) {
			octaves = 0;
		}
		if (treeLacunarity < 1) {
			treeLacunarity = 1;
		}
		if (treeOctaves < 0) {
			treeOctaves = 0;
		}
        base.OnValidate ();
    }
}
