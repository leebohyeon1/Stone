using UnityEngine;
using UnityEngine.UIElements;

public class PlayerCamera : MonoBehaviour
{
    public GameObject A;
    Transform AT; 
    
        // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AT = A.transform;
    }

    // Update is called once per frame
    void LateUpdate() //Update 함수를 그대로 쓰면 프레임이 어긋남. (버벅거림)
    {
        transform.position = new Vector3(AT.position.x, transform.position.y, transform.position.z);
        // transform.position.z -> 인스펙터창의 포지션 값 설정해둔 거 말하는 것 같음.
        // + 0할 거면 그냥 y좌표 자리에 0해도 되는 듯.
    }
}
