using System;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
    private const float OFFSET = .1f;

    [SerializeField] private PlatesCounter _platesCounter;
    [SerializeField] private Transform _counterTopPoint;
    [SerializeField] private Transform _plateVisualPrefab;

    private List<GameObject> _plateGameObjectsList;

    private void Awake()
    {
        this._plateGameObjectsList = new List<GameObject>();
    }

    private void Start()
    {
        this._platesCounter.OnPlateSpawned += PlatesCounterOnOnPlateSpawned;
        this._platesCounter.OnPlateRemoved += PlatesCounterOnOnPlateRemoved;
    }

    private void PlatesCounterOnOnPlateRemoved(object sender, EventArgs e)
    {
        GameObject plate = this._plateGameObjectsList[this._plateGameObjectsList.Count - 1];
        this._plateGameObjectsList.Remove(plate);
        Destroy(plate);
    }

    private void PlatesCounterOnOnPlateSpawned(object sender, EventArgs e)
    {
        Transform plateVisualTransform = Instantiate(this._plateVisualPrefab, this._counterTopPoint);
        plateVisualTransform.localPosition = new Vector3(0, OFFSET * this._plateGameObjectsList.Count, 0);
        this._plateGameObjectsList.Add(plateVisualTransform.gameObject);
    }
}