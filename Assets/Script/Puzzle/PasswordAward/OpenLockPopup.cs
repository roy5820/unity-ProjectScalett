using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenLockPopup : MonoBehaviour
{
    int isPasswordAwardSatus = 0; //Lock ���� �Ϸ� ���� 0: �̿Ϸ� 1: �Ϸ�
    [SerializeField]
    private string dataName = "puzzle_passwordPuzzle"; //�����ͺ��̽��� ����� �����͸�
    [SerializeField]
    private int EmptyMessage = 5003;//Lock������ �̹� Ŭ���� ���� �� ��� �Ǵ� �޽���
    [SerializeField]
    private GameObject LockPuzzleObj;//Lock���� ������Ʈ
    private void Start()
    {
        LoadData();
    }

    public void LoadData()
    {
        //�ش� ���� ���� ���� ������ ���̽����� ��������.
        if (dataName != null)
        {
            int getObjStatus = GameManager.instance.ControllObjStatusData(0, dataName, 0);//���� �޴������� ������ ��������
            if (getObjStatus >= 0)
                isPasswordAwardSatus = getObjStatus;
        }
    }

    //Lock���� ȣ���ϴ� �Լ�
    public void OnOpenLockPuzzlePopup()
    {
        LoadData();
        if (isPasswordAwardSatus > 0)
            PlayerUIController.instance.OpenChatWindow(EmptyMessage);
        else
            LockPuzzleObj.SetActive(true);
            
    }
}
