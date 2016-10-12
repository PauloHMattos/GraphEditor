using UnityEngine;

public class DestroyOnTimer : MonoBehaviour
{
    public float createTime;
    public float destroyDelay;

    void Start()
    {
        createTime = Time.time;
    }

    void Update()
    {
        if(createTime + destroyDelay > Time.time)
        {
            Destroy(gameObject);
        }
    }
}

public class ScrollBackground : MonoBehaviour
{
    public float speed = 0.5f;
    private Renderer _renderer;

	// Use this for initialization
	void Start () {
        _renderer = GetComponent<Renderer>();

    }
	
	// Update is called once per frame
	void Update () {
        var offset = new Vector2(Time.deltaTime * speed, 0);
        _renderer.material.mainTextureOffset += offset;

    }
}
