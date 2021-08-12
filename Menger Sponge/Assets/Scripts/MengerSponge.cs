using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MengerSponge : MonoBehaviour
{
    [SerializeField] private GameObject cubePrefab;
    [SerializeField] private float size = 300f;
    [SerializeField] private int iterations = 3;
    [SerializeField] private UIManager uiManager;
    private List<GameObject> cubes = new List<GameObject>();
    private void Start()
    {
        //create once each start
        CreateMengerBox(iterations, false);
    }

    private List<GameObject> Split(List<GameObject> cubes, bool inverse)
    {
        //for the first split, there is just once cube
        List<GameObject> tempCubes = new List<GameObject>();
        foreach (var cube in cubes)
        {
            //take the size of the cube, then cut the cube in 27 equal big cubes (example Vector3(-1,-1,-1), Vector3(1,0,0).... up to Vector3(1,1,1)
            float size = cube.GetComponent<MengerBox>().size;

            for (int x = -1; x < 2; x++)
            {
                for (int y = -1; y < 2; y++)
                {
                    for (int z = -1; z < 2; z++)
                    {
                        float xx = x * (size / 3f);
                        float yy = y * (size / 3f);
                        float zz = z * (size / 3f);

                        //set the new position of the new cubes
                        Vector3 cubePos = new Vector3(xx, yy, zz) + cube.transform.position;

                        //check if the cube is in the middle of the the bigger cube (example (Abs(-1)+Abs(-1)+Abs(-1) = 3 good, abs(0)+abs(0)+abs(-1) = 1 bad)
                        int sum = Mathf.Abs(x) + Mathf.Abs(y) + Mathf.Abs(z);
                        //check if menger sponge is wanted or (kinda) inverse
                        if (inverse)
                        {

                            if ((x == 0 && (y == 0 || z == 0)) || y == 0 && (x == 0 || z == 0) || (x == 0 && y == 0 && z == 0)) 
                            {
                                //intantiate the good new cubes
                                GameObject copy = Instantiate(cube, cubePos, Quaternion.identity);
                                //set the new size (/3 because each of the 27 smaller cube from the big one is 1/3 of its size
                                copy.GetComponent<MengerBox>().size = size / 3f;
                                copy.transform.localScale = new Vector3(size / 3f, size / 3f, size / 3f);

                                tempCubes.Add(copy);
                            }
                        }
                        else
                        {
                            if (sum > 1)  
                            {
                                //intantiate the good new cubes
                                GameObject copy = Instantiate(cube, cubePos, Quaternion.identity);
                                //set the new size (/3 because each of the 27 smaller cube from the big one is 1/3 of its size
                                copy.GetComponent<MengerBox>().size = size / 3f;
                                copy.transform.localScale = new Vector3(size / 3f, size / 3f, size / 3f);

                                tempCubes.Add(copy);
                            }
                        }

                    }
                }
            }
        }
        return tempCubes;
    }


    public void CreateMengerBox(int iterations, bool inverse)
{
    if (cubes.Count > 0)
    {
        foreach (var OldCube in cubes)
        {
            Destroy(OldCube);
        }
        cubes.Clear();
    }
    GameObject cube = Instantiate(cubePrefab, Vector3.zero, Quaternion.identity);
    cube.transform.localScale = new Vector3(size, size, size);
    cube.GetComponent<MengerBox>().size = size;
    cubes.Add(cube);
    for (int i = 0; i < iterations; i++)
    {
        List<GameObject> newCubes = Split(cubes, inverse);
        //destroy all old cubes
        foreach (GameObject oldCube in cubes)
        {
            Destroy(oldCube);
        }

        cubes = newCubes;
    }
    uiManager.CubeCount = cubes.Count;
}
}

