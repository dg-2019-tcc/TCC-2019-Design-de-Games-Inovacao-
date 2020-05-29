using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Complete
{
    public class TimePartidaManager : MonoBehaviour
    {
        public TextMeshProUGUI recordTimeText;
        public TextMeshProUGUI atualTimeText;

        public BoolVariable jaGanhou;
        public BoolVariable playerGanhou;
        public FloatVariable recordTime;
        public float time;
        public float seconds;
        public int minutes;

        float deltaTime;

        public BoolVariable faseComeca;

        int recordMinutes;
        float recordSeconds;


        // Start is called before the first frame update
        void Start()
        {
            faseComeca.Value = false;
            playerGanhou = Resources.Load<BoolVariable>("PlayerGanhou");

            if (recordTime.Value != 0)
            {
                recordTimeText.enabled = true;
                float record = Mathf.Round(recordTime.Value * 100f) / 100f;

                if (record >= 60)
                {
                    recordMinutes = Mathf.RoundToInt(record / 60);
                    recordSeconds = record - (recordMinutes * 60);
                }

                else
                {
                    recordMinutes = 0;
                    recordSeconds = record;
                }

                recordTimeText.text = "Melhor Tempo:" + recordMinutes + "." + recordSeconds; 
            }

            else
            {
                recordTimeText.enabled = false;
            }

        }

        // Update is called once per frame
        void Update()
        {
            if (faseComeca.Value && playerGanhou.Value == false)
            {
                time += Time.deltaTime;
                minutes = Mathf.RoundToInt(time) / 60;
                seconds = (Mathf.Round(time * 100f) / 100f) - (minutes *60);

                if(seconds < 10)
                {
                    atualTimeText.text = "Tempo:" + minutes + "." + "0" + seconds;
                }

                else
                {
                    atualTimeText.text = "Tempo:" + minutes + "." +  seconds;
                }
            }

            if (playerGanhou.Value)
            {
                if(recordTime.Value > time || recordTime.Value == 0)
                {
                    recordTime.Value = time;
                }
            }

        }
    }
}
