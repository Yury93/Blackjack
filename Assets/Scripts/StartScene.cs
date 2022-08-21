using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ������ ����� ����������� ������, ����� �������� � web ���� �������
/// </summary>
public class StartScene : MonoBehaviour
{
    [SerializeField] private string sceneName;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.01f);
        SceneManager.LoadSceneAsync(sceneName);
    }
}
