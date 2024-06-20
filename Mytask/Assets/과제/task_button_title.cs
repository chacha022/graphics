using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class task_button_title : MonoBehaviour
{
    [SerializeField] Button btn_title;
    // Start is called before the first frame update

    public void LoadingNewScene(){
        SceneManager.LoadScene("title");
    }

    void Start()
    {
        btn_title.onClick.AddListener(()=>LoadingNewScene());
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
