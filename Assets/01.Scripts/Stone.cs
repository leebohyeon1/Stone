using UnityEngine;

public class Stone : MonoBehaviour
{
    public float deg; //����
    public float turretSpeed; // ��ž? �빰���� ��ġ��? ���ǵ�
    public GameObject turret;  // ȭ��ǥ arrow
    public GameObject bullet; //���ӿ�����Ʈ ����

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow)) //���� ����Ű�� ���� ��, GetKey : �ش�Ǵ� Ű�� ������ ���� ��� Turn ��ȯ��. -> �̰� ���콺�� ���� �ٲ� ����
        {
            deg = deg + Time.deltaTime * turretSpeed; // Time.deltaTime -> �ð���������, �� ������ �������ϴ� �� �ɸ��� �ð��� ����ϴ� �Լ�
            float rad = deg * Mathf.Deg2Rad;
            turret.transform.localPosition = new Vector2(Mathf.Cos(rad),Mathf.Sin(rad));
            turret.transform.eulerAngles = new Vector3(0, 0, deg);
        }
        else if (Input.GetKey(KeyCode.DownArrow)) // �Ʒ� ����Ű ���� ��?
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

            // ��ü ������ �� stone�� �������� ��... ���� ���� ���� ���� 
        }
    }
}
