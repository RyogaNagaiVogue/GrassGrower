using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassDeleter : MonoBehaviour
{
    //草が消える判定を行う　そのうち消す
    public MicVolumeSample micVolumeSample;

    void Start()
    {
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Grass")
        {
            Destroy(other.gameObject);
            if (micVolumeSample.nowGrassNumber > 0)
                micVolumeSample.nowGrassNumber--;
        }


    }
}
