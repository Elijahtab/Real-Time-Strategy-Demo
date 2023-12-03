using UnityEngine;
using System.Collections;

public class MapDisplay : MonoBehaviour {

	public Renderer textureRender;
	public MeshFilter meshFilter;
	public MeshRenderer meshRenderer;

	public void DrawTexture(Texture2D texture) {
		textureRender.sharedMaterial.mainTexture = texture;
		textureRender.transform.localScale = new Vector3 (texture.width, 1, texture.height);
	}

	
	public void DrawMesh(MeshData meshData, Texture2D texture) {
		meshFilter.sharedMesh = meshData.CreateMesh();
		meshRenderer.sharedMaterial.mainTexture = texture;    	
	}

	
	public void GenerateMeshCollider(MeshData meshData){
		MeshCollider meshCollider;
		meshCollider = gameObject.GetComponent<MeshCollider>();
		if (meshCollider == null) {
        	meshCollider = gameObject.AddComponent<MeshCollider>();
    	}
		// Create a new mesh for collider
		Mesh colliderMesh = meshData.CreateMesh();

		// Assign the mesh to the collider
		meshCollider.sharedMesh = colliderMesh;
		meshCollider.transform.position = meshFilter.transform.position;

	}
	

}