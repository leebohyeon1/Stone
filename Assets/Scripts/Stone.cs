using UnityEngine;

public class Stone : MonoBehaviour
{
    public float deg; //각도
    public float turretSpeed; // 포탑? 대물렌즈 설치부? 스피드
    public GameObject turret;  // 화살표 arrow
    public GameObject bullet; //게임오브젝트 생성

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow)) //위쪽 방향키가 눌릴 때, GetKey : 해당되는 키를 누르고 있을 경우 Turn 반환함. -> 이걸 마우스로 어케 바꿈 ㅁㄹ
        {
            deg = deg + Time.deltaTime * turretSpeed; // Time.deltaTime -> 시간개념으로, 각 프레임 레더링하는 데 걸리는 시간을 계산하는 함수
            float rad = deg * Mathf.Deg2Rad;
            turret.transform.localPosition = new Vector2(Mathf.Cos(rad),Mathf.Sin(rad));
            turret.transform.eulerAngles = new Vector3(0, 0, deg);
        }
        else if (Input.GetKey(KeyCode.DownArrow)) // 아래 방향키 눌릴 때?
        {
            deg = deg - Time.deltaTime * turretSpeed;
            float rad = deg * Mathf.Deg2Rad;
            turret.transform.localPosition = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
            turret.transform.eulerAngles = new Vector3(0, 0, deg);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject go = Instantiate(bullet);
            go.transform.position = turret.transform.position;

            // 물체 던지는 거 stone이 던져저야 돼... 던져 던져 던져 던져 
        }
    }
}
