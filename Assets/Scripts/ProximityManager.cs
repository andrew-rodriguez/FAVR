using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProximityManager : MonoBehaviour
{
    [SerializeField] private GameObject _spawnTarget;
    [SerializeField] private GameObject _offer;
    [SerializeField] private GameObject _placenote;


    public GameObject SpawnDeal(string dealName)
    {   
        GameObject prefab = Instantiate(_offer,
            _spawnTarget.transform.position,
            _spawnTarget.transform.rotation);

        Texture2D texture = Resources.Load<Texture2D>(dealName);
        Debug.Log(dealName + " Resource: " + texture);

        prefab.GetComponentsInChildren<Image>()[0].material.mainTexture = texture;
        
        return prefab;
    }


    public GameObject SpawnPlacenote(string dealName)
    {
        GameObject prefab = Instantiate(_placenote,
            _spawnTarget.transform.position + new Vector3(3.5f, 0.5f),
            _spawnTarget.transform.rotation);

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
