using UnityEngine;

public class VehicleMoveHandler : MonoBehaviour
{
    private float cruiseSpeed, currentSpeed;
    private Transform despawnPoint;
    private Vector3 spawnPos;
    private int laneIndex;
    private VehicleSpawner spawner;
    private Rigidbody rb;
    private VehicleAudioHandler vehicleAudio;
    private bool isBlocking;
    private float graceTimer;

    private const float BrakeRate = 5f;
    private const float AccelRate = 3.5f;
    private const float LookAhead = 8f;
    private const float CastRadius = 0.75f;
    private const float GraceDuration = 1.8f;

    private LayerMask vehicleLayer;

    public void Init(float speed, Transform despawn, Vector3 spawn, int lane, VehicleSpawner sp)
    {
        cruiseSpeed = speed; currentSpeed = 0f;
        despawnPoint = despawn; spawnPos = spawn;
        laneIndex = lane; spawner = sp;

        rb = gameObject.GetComponent<Rigidbody>() ?? gameObject.AddComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.useGravity = false;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.constraints = RigidbodyConstraints.FreezePositionY
                       | RigidbodyConstraints.FreezeRotationX
                       | RigidbodyConstraints.FreezeRotationY
                       | RigidbodyConstraints.FreezeRotationZ;

        vehicleLayer = 1 << gameObject.layer;
        vehicleAudio = GetComponent<VehicleAudioHandler>();
        vehicleAudio?.StartEngine();
    }

    private void FixedUpdate()
    {
        if (rb == null) return;
        graceTimer += Time.fixedDeltaTime;

        bool blocked = graceTimer > GraceDuration && IsCarAhead();

        // if blocked — stop INSTANTLY, not gradually
        // gradual braking causes chain push
        if (blocked)
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, BrakeRate * 2f * Time.fixedDeltaTime);
        }
        else
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, cruiseSpeed, AccelRate * Time.fixedDeltaTime);
        }

        // hard stop when very slow to prevent micro-creep pushing
        if (currentSpeed < 0.5f && blocked)
            currentSpeed = 0f;

        rb.linearVelocity = new Vector3(
            transform.forward.x * currentSpeed,
            rb.linearVelocity.y,
            transform.forward.z * currentSpeed);

        vehicleAudio?.SetEngineIdle(currentSpeed < cruiseSpeed * 0.3f);

        bool stopped = currentSpeed < 0.1f;
        if (stopped && !isBlocking) { isBlocking = true; spawner.AddBlock(laneIndex); }
        if (!stopped && isBlocking) { isBlocking = false; spawner.RemoveBlock(laneIndex); }

        CheckDespawn();
    }

    private bool IsCarAhead()
    {
        // multiple raycasts for wider detection
        Vector3[] origins = {
        transform.position + transform.forward * 1.5f,
        transform.position + transform.forward * 1.5f + transform.right * 0.4f,
        transform.position + transform.forward * 1.5f - transform.right * 0.4f,
    };

        foreach (Vector3 origin in origins)
        {
            if (Physics.SphereCast(origin, CastRadius, transform.forward,
                out RaycastHit hit, LookAhead, vehicleLayer))
            {
                if (hit.transform.root != transform.root)
                    return true;
            }
        }
        return false;
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.layer == gameObject.layer)
            vehicleAudio?.TryHonkOnBump();
    }

    private void CheckDespawn()
    {
        bool done = despawnPoint == null
            ? Vector3.Distance(transform.position, spawnPos) > 80f
            : Vector3.Dot(transform.position - spawnPos,
                          despawnPoint.position - spawnPos)
              >= (despawnPoint.position - spawnPos).sqrMagnitude;
        if (done) SelfDestroy();
    }

    private void SelfDestroy()
    {
        if (isBlocking) spawner?.RemoveBlock(laneIndex);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (isBlocking) spawner?.RemoveBlock(laneIndex);
    }
}