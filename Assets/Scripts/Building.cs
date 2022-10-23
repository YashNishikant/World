using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{

    RaycastHit hit;
    public float dist;

    public GameObject WallSlab;
    public GameObject FloorSlab;
    public GameObject Beam;
    public GameObject Cube;

    int blockChoice = 0;

    List<GameObject> BuildMaterial = new List<GameObject>();

    private void Start()
    {
        BuildMaterial.Add(WallSlab);
        BuildMaterial.Add(FloorSlab);
        BuildMaterial.Add(Beam);
        BuildMaterial.Add(Cube);
    }

    void Update()
    {
        raycasting();
        changeBuild();
    }

    void changeBuild() {

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            blockChoice++;
        }

        if (blockChoice > BuildMaterial.Count-1) {
            blockChoice = 0;
        }

    }

    void raycasting()
    {

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, dist))
            {
                if (hit.collider.gameObject.layer == 8)
                {

                    Destroy(hit.transform.gameObject);

                }
            }
        }
        
        if (Input.GetMouseButtonDown(1)) {
            
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, dist)){

                
                GameObject floor = Instantiate(BuildMaterial[blockChoice]) as GameObject;
                
                floor.transform.position = hit.point + hit.normal;

                if (Physics.Raycast(floor.transform.position, Vector3.left, out hit, floor.transform.localScale.x))
                {

                    if (hit.collider.gameObject.layer == 8)
                    {
                        floor.transform.position = hit.transform.position + new Vector3(hit.transform.localScale.x, 0, 0);

                    }
                    return;
                }

                if (Physics.Raycast(floor.transform.position, Vector3.right, out hit, floor.transform.localScale.x))
                {
                    if (hit.collider.gameObject.layer == 8)
                    {
                        floor.transform.position = hit.transform.position + new Vector3(-hit.transform.localScale.x, 0, 0);

                    }
                    return;
                }

                if (Physics.Raycast(floor.transform.position, Vector3.forward, out hit, floor.transform.localScale.z))
                {
                    if (hit.collider.gameObject.layer == 8)
                    {
                        floor.transform.position = hit.transform.position + new Vector3(0, 0, -hit.transform.localScale.z);

                    }
                    return;
                }

                if (Physics.Raycast(floor.transform.position, Vector3.back, out hit, floor.transform.localScale.z))
                {
                    if (hit.collider.gameObject.layer == 8)
                    {
                        floor.transform.position = hit.transform.position + new Vector3(0, 0, hit.transform.localScale.z);

                    }
                    return;
                }

                if (Physics.Raycast(floor.transform.position, Vector3.down, out hit, floor.transform.localScale.y))
                {
                    if (hit.collider.gameObject.layer == 8)
                    {
                        floor.transform.position = hit.transform.position + new Vector3(0, hit.transform.localScale.y, 0);
                    }
                    return;
                }

                if (Physics.Raycast(floor.transform.position, Vector3.up, out hit, floor.transform.localScale.y))
                {
                    if (hit.collider.gameObject.layer == 8)
                    {
                        floor.transform.position = hit.transform.position + new Vector3(0, -hit.transform.localScale.y, 0);
                    }
                    return;
                }
            }

            }
        }
    }
