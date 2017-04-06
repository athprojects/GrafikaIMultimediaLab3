using System.Globalization;
using UnityEngine;

public class PiramideBuilder : MonoBehaviour {
    public int LevelCount = 10;
    public float BasementSpring = 10;
    public string SpheresTagName = "PiramideBall";

	// Use this for initialization
	void Start ()
	{
	    var offset = 0 - (LevelCount / 2.0f);
        RenderLevel(LevelCount, new Vector3(offset,0,offset), true);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void RenderLevel(int width, Vector3 position, bool joinWithSprings)
    {
        while (true)
        {
            Rigidbody[] lastLine = null;
            var currentLine = new Rigidbody[width];

            var level = new GameObject();       
            level.transform.parent = this.transform;
            level.transform.localPosition = position;
            for (var z = 0; z < width; z++)
            {
                var line = new GameObject();
                line.transform.parent = level.transform;
                line.transform.localPosition = new Vector3(0, 0, z);
                for (var x = 0; x < width; x++)
                {
                    var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    sphere.tag = SpheresTagName;
                    sphere.GetComponent<Renderer>().material = Resources.Load("Sphere", typeof(Material)) as Material;
                    sphere.transform.parent = line.transform;
                    sphere.transform.localPosition = new Vector3(x, 0, 0);                                     
                    currentLine[x] = sphere.AddComponent<Rigidbody>();

                    if (joinWithSprings)
                        JoinObjToPrevObjs(sphere, x, z, lastLine, currentLine);                      
                }
                lastLine = currentLine;
                currentLine = new Rigidbody[width];
            }
            if (--width > 0)
            {
                position = new Vector3(position.x + 0.5f, position.y + 0.70f, position.z + 0.5f);
                joinWithSprings = false;
                continue;
            }
            break;
        }
    }

    private void JoinObjToPrevObjs(GameObject obj, int x, int z, Rigidbody[] lastLine, Rigidbody[] currentLine)
    {
        if (x > 0)
            JoinBySpring(obj, currentLine[x - 1]);
        if (z > 0 && lastLine != null)
            JoinBySpring(obj, lastLine[x]);
    }

    private void JoinBySpring(GameObject baseObj, Rigidbody connectedRigidBody)
    {
        var spring = baseObj.AddComponent<SpringJoint>();
        spring.connectedBody = connectedRigidBody;
        spring.spring = BasementSpring;
    }
}
