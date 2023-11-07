using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassGrower : MonoBehaviour
{
    public GameObject grass; 
    public int maxGrassNumber = 30;
    public int nowGrassNumber = 0;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 scale = new Vector3(1.0f,0.1f,1.0f);
        grass.transform.localScale = scale;
    }

    void Update(){
        if(Input.GetKeyDown("space")){
            GrassGrow();
        }
    }

    public void GrassGrow()
    {
        //Debug.Log("GrassGrow");
            if(maxGrassNumber>nowGrassNumber){
            //float rndX = UnityEngine.Random.Range(-20, 20);//ランダム生成
            //float rndZ = UnityEngine.Random.Range(-20, 20);//ランダム生成
            float rndX = 2;
            float rndZ = 2;
            // 生成位置
            Vector3 pos = new Vector3(rndX, 0.0f, rndZ);
            // プレハブを指定位置に生成
            Instantiate(grass, pos, Quaternion.identity);
            nowGrassNumber++;
            }
        
    }
}
