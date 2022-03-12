using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchMenu : MonoBehaviour
{
    [SerializeField] private GameObject _center;
    [SerializeField] private float _orbitSpeed;


    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(_center.transform.position, Vector3.up, _orbitSpeed * Time.deltaTime);
    }
}
