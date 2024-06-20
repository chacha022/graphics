using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class task_button_start : MonoBehaviour
{
    [SerializeField] Button btn_start;
    // Start is called before the first frame update

    public void LoadingNewScene(){
        
        SceneManager.LoadScene("task");
        
    }

    void Start()
    {
        btn_start.onClick.AddListener(()=>LoadingNewScene());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
