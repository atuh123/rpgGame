using UnityEngine;

public class BattleCamera : MonoBehaviour
{
    private Vector3 startPosition;
    private Quaternion startRotation;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ResetPosition()
    {
        transform.position = startPosition;
        transform.rotation = startRotation;
    }

    public Vector3 GetStartPosition()
    {
        return startPosition;
    }

    public Quaternion GetStartRotation()
    {
        return startRotation;
    }
}
