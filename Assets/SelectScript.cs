using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SelectScript : MonoBehaviour
{
    [Header("References")]
    public Camera camera;

    [Header("Tag Data")]
    [NaughtyAttributes.Tag] public string doorTag;
    [NaughtyAttributes.Tag] public string notebookTag;

    [Header("Selection Variables")]
    public float defaultDistance;
    public float doorDistance;
    public float itemDistance;

    List<float> distances;
    // Start is called before the first frame update
    void Start()
    {
        distances = new List<float>()
        {
            defaultDistance,
            doorDistance,
            itemDistance,
        };
    }

    // Update is called once per frame
    void Update()
    {
        ReticleManager.current.reticleSelect = false;
        bool held = Input.GetMouseButtonDown(0);

        float maxDistance = distances.Max();
        RaycastHit selectCast;
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out selectCast, maxDistance))
        {
            if (selectCast.collider.CompareTag(doorTag))
            {
                if (doorDistance == -1f || Vector3.Distance(camera.transform.position, selectCast.point) <= doorDistance)
                {
                    DoorScript door = selectCast.collider.GetComponent<DoorScript>();
                    if (door.openTime <= 0f && door.lockTime <= 0f)
                    {
                        ReticleManager.current.reticleSelect = true;
                        if (held)
                        {
                            if (door.openTime <= 0f)
                                door.PlaySound(door.openSound);
                            door.openTime = 3f;
                        }
                    }
                    return;
                }
            }

            if (selectCast.collider.CompareTag(notebookTag))
            {
                if (itemDistance == -1f || Vector3.Distance(camera.transform.position, selectCast.point) <= itemDistance)
                {
                    ReticleManager.current.reticleSelect = true;
                    if (held)
                    {
                        NotebookScript notebook = selectCast.collider.GetComponent<NotebookScript>();
                        notebook.Collect();
                    }
                }
            }
        }
    }


}
