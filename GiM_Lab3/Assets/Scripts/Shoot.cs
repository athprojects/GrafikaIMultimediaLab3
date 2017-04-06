using UnityEngine;
using UnityEngine.SceneManagement;

public class Shoot : MonoBehaviour
{
    public GameObject Riffle;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var boolet = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            boolet.transform.position = new Vector3(Riffle.transform.position.x, Riffle.transform.position.y, Riffle.transform.position.z);
            boolet.transform.rotation = new Quaternion(Riffle.transform.rotation.x, Riffle.transform.rotation.y, Riffle.transform.rotation.z, Riffle.transform.rotation.w);
            boolet.GetComponent<Renderer>().material = Resources.Load("Flower1", typeof(Material)) as Material;
            var rigidBody = boolet.AddComponent<Rigidbody>();
            rigidBody.AddForce(boolet.transform.forward*5000, ForceMode.Force);
        }
        else if (Input.GetMouseButton(1))
        {
            SceneManager.LoadScene("scene1");
        }
    }
}
