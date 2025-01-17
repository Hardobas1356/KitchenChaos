using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounterVisual : MonoBehaviour
{
    [SerializeField] private PlateCounter plateCounter;
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private Transform plateVisual;

    private List<GameObject> plateVisualGameObjectList;

    public void Awake()
    {
        plateVisualGameObjectList = new List<GameObject>();
    }

    public void Start()
    {
        plateCounter.OnPlateSpawned += PlateCounter_OnPlateSpawned;
        plateCounter.OnPlateRemoved += PlateCounter_OnPlateRemoved;
    }

    private void PlateCounter_OnPlateRemoved(object sender, System.EventArgs e)
    {
        GameObject plateGameObject = plateVisualGameObjectList[plateVisualGameObjectList.Count - 1];
        plateVisualGameObjectList.RemoveAt(plateVisualGameObjectList.Count-1);

        Destroy(plateGameObject);
    }

    private void PlateCounter_OnPlateSpawned(object sender, System.EventArgs e)
    {
        Transform plateVisualTransform = Instantiate(plateVisual,counterTopPoint);

        float plateOffsetY = 0.1f;
        plateVisualTransform.localPosition = new Vector3(0,plateOffsetY*plateVisualGameObjectList.Count,0);

        plateVisualGameObjectList.Add(plateVisualTransform.gameObject);
    }
}
