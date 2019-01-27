﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MattressDyer : MonoBehaviour
{
    [SerializeField] private MeshCollider _mattresMeshCollider;
    [SerializeField] private Texture2D[] _dirtTextures;

    private MeshFilter _mattressMeshFilter;
    private Mesh _mattressMesh;
    private List<Color> _mattressVertexColor;
    private Texture2D _mattressTextureInstance;
    private Material _mattressMaterial;

    private void Awake()
    {
        if (_mattresMeshCollider == null)
        {
            _mattresMeshCollider = GetComponent<MeshCollider>();
        }
        /*
        _mattressMeshFilter = _mattresMeshCollider.GetComponent<MeshFilter>();
        _mattressMeshFilter.mesh = _mattressMeshFilter.sharedMesh;
        _mattressVertexColor = new List<Color>(new Color[_mattressMeshFilter.mesh.vertexCount]);
        */
        _mattressMaterial = _mattresMeshCollider.GetComponent<Renderer>().material;
        _mattressTextureInstance = Instantiate(_mattressMaterial.mainTexture) as Texture2D;
        _mattressMaterial.mainTexture = _mattressTextureInstance;
    }

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint contactPoint = collision.GetContact(0);
        if (contactPoint.otherCollider != PlayerMovement.Player.BaseCollider && contactPoint.thisCollider != PlayerMovement.Player.BaseCollider)
        {
            Vector3 rayDir = _mattresMeshCollider.transform.position - collision.GetContact(0).point;
            _mattresMeshCollider.convex = false;
            RaycastHit mattressHitInfo;
            Ray ray = new Ray(_mattresMeshCollider.transform.position - rayDir.normalized * 10f, rayDir);
            if (_mattresMeshCollider.Raycast(ray, out mattressHitInfo, 100f))
            {
                Paint(mattressHitInfo);
            }
        }
    }

    public void Paint (RaycastHit hit)
    {

        int textureXCoord = (int) (hit.textureCoord.x * _mattressTextureInstance.width);
        int textureYCoord = (int) (hit.textureCoord.y * _mattressTextureInstance.height);

        print(hit.textureCoord);
        print(textureXCoord.ToString() + "    ,   " + textureYCoord.ToString());

        Texture2D dirtTexture = _dirtTextures[Random.Range(0, _dirtTextures.Length)];

        Color dirtTextureColor;
        Vector2 paintVector;
        for(int x = 0; x < dirtTexture.width; x++)
        {
            for(int y = 0; y < dirtTexture.height; y++)
            {
                dirtTextureColor = dirtTexture.GetPixel(x, y);
                if (dirtTextureColor.a > 0.8f)
                {
                    paintVector.x = textureXCoord - dirtTexture.width / 2 + x;
                    paintVector.y = textureYCoord - dirtTexture.height / 2 + y;
                    if (paintVector.x >= 0 && paintVector.y >= 0)
                    {
                        _mattressTextureInstance.SetPixel((int) paintVector.x, (int) paintVector.y, Color.yellow);
                    }
                }
            }
        }
        _mattressTextureInstance.Apply();
    }
}