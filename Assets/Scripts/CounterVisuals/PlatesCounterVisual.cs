using System;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
    [SerializeField] private PlatesCounter platesCounter;
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private Transform plateVisualPrefab;
    private List<GameObject> plateVisualGameObjectList;
    private void Awake() {
        plateVisualGameObjectList = new List<GameObject>();
    }

    private void Start() {
        platesCounter.OnplateSpwaned += PlatesCounter_OnplateSpwaned;
        platesCounter.OnplateRemoved += PlatesCounter_OnplateRemoved;
        platesCounter.OnPlatesStackCleared += PlatesCounter_OnPlatesStackCleared;
    }

    private void OnDestroy()
    {
        if (platesCounter == null) return;
        platesCounter.OnplateSpwaned -= PlatesCounter_OnplateSpwaned;
        platesCounter.OnplateRemoved -= PlatesCounter_OnplateRemoved;
        platesCounter.OnPlatesStackCleared -= PlatesCounter_OnPlatesStackCleared;
    }

    private void PlatesCounter_OnPlatesStackCleared(object sender, EventArgs e)
    {
        foreach (GameObject plateGameObject in plateVisualGameObjectList)
        {
            if (plateGameObject != null)
            {
                Destroy(plateGameObject);
            }
        }
        plateVisualGameObjectList.Clear();
    }

    private void PlatesCounter_OnplateRemoved(object sender, EventArgs e)
    {
        GameObject plateGameObject = plateVisualGameObjectList[plateVisualGameObjectList.Count-1];
        plateVisualGameObjectList.Remove(plateGameObject);
        Destroy(plateGameObject);
    }

    private void PlatesCounter_OnplateSpwaned(object sender, EventArgs e)
    {
        Transform plateVisualTransform = Instantiate(plateVisualPrefab , counterTopPoint);

        float plateOffsetY = 0.15f;
        plateVisualTransform.localPosition = new Vector3(0 , plateOffsetY*plateVisualGameObjectList.Count , 0);
        plateVisualGameObjectList.Add(plateVisualTransform.gameObject);
    }
}
