using UnityEngine;

public class AddColliders : MonoBehaviour
{
    [ContextMenu("Add Mesh Colliders to All Children")]
    void AddCollidersMethod()
    {
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();

        int count = 0;
        foreach (var mf in meshFilters)
        {
            if (mf.GetComponent<Collider>() == null)
            {
                MeshCollider mc = mf.gameObject.AddComponent<MeshCollider>();
                mc.sharedMesh = mf.sharedMesh;
                count++;
            }
        }

        Debug.Log($"Successfully added MeshCollider to {count} objects!");
    }
}