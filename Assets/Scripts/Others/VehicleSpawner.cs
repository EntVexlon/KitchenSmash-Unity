using UnityEngine;
using System.Collections;

public class VehicleSpawner : MonoBehaviour
{
    [System.Serializable]
    public class LaneConfig
    {
        public Transform SpawnPoint;
        public Transform DespawnPoint;
        public bool FlipRotation;
    }

    [Header("Lanes")]
    [SerializeField] private LaneConfig[] Lanes;
    [SerializeField] private GameObject[] VehiclePrefabs;
    [SerializeField] private LayerMask VehicleLayer;

    [Header("Speed")]
    [SerializeField] private float MinSpeed = 8f;
    [SerializeField] private float MaxSpeed = 14f;

    [Header("Spawn Tuning")]
    [SerializeField] private float IntraClusterGap = 1.8f;
    [SerializeField] private float SpawnClearRadius = 5f;

    private int[] laneBlocks;

    public void AddBlock(int i) { if (Valid(i)) laneBlocks[i]++; }
    public void RemoveBlock(int i) { if (Valid(i)) laneBlocks[i] = Mathf.Max(0, laneBlocks[i] - 1); }
    public bool IsBlocked(int i) => Valid(i) && laneBlocks[i] > 0;
    private bool Valid(int i) => i >= 0 && i < laneBlocks.Length;

    private void Start()
    {
        laneBlocks = new int[Lanes.Length];
        for (int i = 0; i < Lanes.Length; i++)
            StartCoroutine(SpawnLoop(i));
    }

    private IEnumerator SpawnLoop(int lane)
    {
        // very wide stagger — lanes feel completely unrelated
        yield return new WaitForSeconds(Random.Range(1f, 20f));

        while (true)
        {
            // random "mood" for this burst — calm, normal, or busy
            SpawnMood mood = PickMood();

            switch (mood)
            {
                case SpawnMood.Calm:
                    // one car, then long rest
                    yield return SpawnWithWait(lane, 1);
                    yield return new WaitForSeconds(Random.Range(12f, 22f));
                    break;

                case SpawnMood.Normal:
                    // 1-2 cars, medium gap
                    yield return SpawnWithWait(lane, Random.Range(1, 3));
                    yield return new WaitForSeconds(Random.Range(5f, 12f));
                    break;

                case SpawnMood.Busy:
                    // 2-4 cars close together, short gap
                    yield return SpawnWithWait(lane, Random.Range(2, 5));
                    yield return new WaitForSeconds(Random.Range(2f, 6f));
                    break;

                case SpawnMood.Silent:
                    // no cars for a while — calm period
                    yield return new WaitForSeconds(Random.Range(15f, 30f));
                    break;
            }
        }
    }

    private IEnumerator SpawnWithWait(int lane, int count)
    {
        for (int i = 0; i < count; i++)
        {
            yield return new WaitUntil(() =>
                !IsBlocked(lane) && IsSpawnClear(Lanes[lane].SpawnPoint.position));

            SpawnVehicle(lane);

            if (i < count - 1)
                yield return new WaitForSeconds(IntraClusterGap + Random.Range(-0.2f, 0.4f));
        }
    }

    private enum SpawnMood { Calm, Normal, Busy, Silent }

    private SpawnMood PickMood()
    {
        float r = Random.value;
        if (r < 0.30f) return SpawnMood.Calm;    // 30% — single car
        if (r < 0.55f) return SpawnMood.Normal;  // 25% — normal flow
        if (r < 0.70f) return SpawnMood.Busy;    // 15% — burst of cars
        return SpawnMood.Silent;                  // 30% — quiet period
    }

    // Weighted: more 1s and 2s than 3s and 4s
    private int PickClusterSize()
    {
        float r = Random.value;
        if (r < 0.45f) return 1;
        if (r < 0.75f) return 2;
        if (r < 0.92f) return 3;
        return 4;
    }

    private bool IsSpawnClear(Vector3 pos)
        => Physics.OverlapSphere(pos, SpawnClearRadius, VehicleLayer).Length == 0;

    private void SpawnVehicle(int lane)
    {
        if (VehiclePrefabs.Length == 0) return;
        LaneConfig l = Lanes[lane];
        Quaternion rot = l.FlipRotation ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;
        GameObject v = Instantiate(VehiclePrefabs[Random.Range(0, VehiclePrefabs.Length)],
                                   l.SpawnPoint.position, rot);
        int layerIndex = (int)Mathf.Log(VehicleLayer.value, 2);
        SetLayer(v, layerIndex);
        v.AddComponent<VehicleMoveHandler>().Init(
            Random.Range(MinSpeed, MaxSpeed), l.DespawnPoint,
            l.SpawnPoint.position, lane, this);
    }

    private static void SetLayer(GameObject obj, int layer)
    {
        obj.layer = layer;
        foreach (Transform c in obj.transform) SetLayer(c.gameObject, layer);
    }
}