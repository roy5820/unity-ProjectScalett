
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class placeThingsController : MonoBehaviour
{
    int isPlaceTingsSatus = 0; //���� ���� ���� 0: �̿Ϸ� 1: �Ϸ�

    [SerializeField]
    GameObject PlaceObj;//���� ���� ������Ʈ

    [SerializeField]
    int PlaceItemID = 101;//���� ���� ������Ʈ

    [SerializeField]
    private string dataName = "placeNewsPaper"; //�����ͺ��̽��� ����� �����͸�

    // Start is called before the first frame update
    void Start()
    {
        //�ش� ���� ���� ���� ������ ���̽����� ��������.
        if (dataName != null)
        {
            int getObjStatus = GameManager.instance.ControllObjStatusData(0, dataName, 0);//���� �޴������� ������ ��������
            if (getObjStatus >= 0)
                isPlaceTingsSatus = getObjStatus;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //������ �����ִ����� ���� �̺�Ʈ ó��
        if (isPlaceTingsSatus == 1)
        {
            GameManager.instance.DelItem(0, PlaceItemID);
            PlaceObj.SetActive(true);
            Destroy(this.gameObject);
        }
    }

    //Ŭ���� �������� ĭ�� ���� �������� ���� �� PlaceObj Ȱ��ȭ
    public void OnPlaceObj()
    {
        if(GameManager.instance.SelectItemId == PlaceItemID)
        {
            isPlaceTingsSatus = 1;
        }
    }

    private void OnDestroy()
    {
        GameManager.instance.ControllObjStatusData(1, dataName, isPlaceTingsSatus);
    }
}
