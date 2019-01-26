using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MattressDyer : MonoBehaviour
{
    [SerializeField] private MeshCollider _mattresMeshCollider;
    [SerializeField] private LayerMask _mattressLayerMask = 1 << 9;

    private void Awake()
    {
        if (_mattresMeshCollider == null)
        {
            _mattresMeshCollider = GetComponent<MeshCollider>();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.GetContact(0).otherCollider == _mattresMeshCollider || collision.GetContact(0).thisCollider == _mattresMeshCollider)
        {
            Vector3 rayDir = _mattresMeshCollider.transform.position - collision.GetContact(0).point;
            RaycastHit mattressHitInfo;
            if (Physics.Raycast(_mattresMeshCollider.transform.position - rayDir.normalized * 10f, rayDir, out mattressHitInfo, _mattressLayerMask))
            {
                Debug.Log(mattressHitInfo.transform.name);
                Debug.Log(mattressHitInfo.textureCoord);
                Debug.Log(mattressHitInfo.triangleIndex);
            }
        }
    }
}
