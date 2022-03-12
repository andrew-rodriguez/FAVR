using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProximityManager : MonoBehaviour
{
    [SerializeField] private GameObject _spawnTarget;
    [SerializeField] private GameObject _offer;


    public GameObject SpawnDeal(string dealName)
    {
        GameObject dealPrefab =  _offer;
        
        GameObject prefab = Instantiate(dealPrefab,
            _spawnTarget.transform.position,
            _spawnTarget.transform.rotation);

        Texture2D texture = Resources.Load<Texture2D>(dealName);
        Debug.Log(dealName + " Resource: " + texture);

        prefab.GetComponentsInChildren<Image>()[0].material.mainTexture = texture;

        return prefab;
    }


    public void DestroyDeal(GameObject prefab)
    {
        if (prefab != null)
        {
            Destroy(prefab);
        }
    }
}
