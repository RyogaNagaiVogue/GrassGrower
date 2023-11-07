using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicVolumeSample : MonoBehaviour
{
    [SerializeField, Range(0f, 10f)] float m_gain = 1f; // 音量に掛ける倍率
    float m_volumeRate; // 音量(0-1)
    public string target;
    public string devName;
    public float startTime, nowTime;
    public GameObject grass;
    public int maxGrassNumber = 30;
    public int nowGrassNumber = 0;
    public float deltaTime = 0.5f;
    //   [Header("Debug Mode")]
    public enum Mode
    {
        Normal,//笑い検出で草生やす
        Volume,//音量で草生やす
        Button,//キーで草生やす
    }
    public Mode mode = Mode.Normal;

    void Start()
    {
        AudioSource aud = GetComponent<AudioSource>();
        if ((aud != null) && (Microphone.devices.Length > 0)) // オーディオソースとマイクがある
        {
            string[] micName = Microphone.devices; // 複数見つかってもとりあえず0番目のマイクを使用

            foreach (string mic in micName)
            {
                if (mic.Contains(target))
                {
                    devName = mic;
                    break;
                }
            }

            int minFreq, maxFreq;
            Microphone.GetDeviceCaps(devName, out minFreq, out maxFreq); // 最大最小サンプリング数を得る
            aud.clip = Microphone.Start(devName, true, 100, minFreq); // 音の大きさを取るだけなので最小サンプリングで十分
            aud.Play(); //マイクをオーディオソースとして実行(Play)開始
        }
        startTime = 0;

        Vector3 scale = new Vector3(1.0f, 0.1f, 1.0f);
        grass.transform.localScale = scale;
    }

    // Update is called once per frame
    void Update()
    {
        if (mode == Mode.Volume)
        {
            Debug.Log(m_volumeRate);
            if (m_volumeRate > 0.005f)
            {
                nowTime = Time.time;
                float delta = nowTime - startTime;
                if (delta > deltaTime)
                {
                    GrassGrow();
                    startTime = Time.time;
                }
            }
        }

        else if (mode == Mode.Button)
        {
            if ((Input.GetKeyDown("g")))
            {
                nowTime = Time.time;
                float delta = nowTime - startTime;
                if (delta > deltaTime)
                {
                    GrassGrow();
                    startTime = Time.time;
                }
            }
        }
    }

    // オーディオが読まれるたびに実行される
    void OnAudioFilterRead(float[] data, int channels)
    {
        float sum = 0f;
        for (int i = 0; i < data.Length; ++i)
        {
            sum += Mathf.Abs(data[i]); // データ（波形）の絶対値を足す
        }
        // データ数で割ったものに倍率をかけて音量とする
        m_volumeRate = Mathf.Clamp01(sum * m_gain / (float)data.Length);
    }

    //====================
    //どっかに草を生やす関数
    public void GrassGrow()
    {
        //Debug.Log("GrassGrow");
        if (maxGrassNumber > nowGrassNumber)
        {
            float rndX = UnityEngine.Random.Range(-10, 10);//ランダム生成
            float rndZ = UnityEngine.Random.Range(-10, 10);//ランダム生成
            // 生成位置
            Vector3 pos = new Vector3(rndX, 0.0f, rndZ);
            // プレハブを指定位置に生成
            Instantiate(grass, pos, Quaternion.identity);
            nowGrassNumber++;
        }

    }
    //===================
}