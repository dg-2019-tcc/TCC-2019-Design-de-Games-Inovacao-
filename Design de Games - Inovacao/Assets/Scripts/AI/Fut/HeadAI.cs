using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Complete
{
    public class HeadAI : MonoBehaviour
    {
        public float headForceX;
        public float headForceY;

        public Rigidbody2D ballrb;
        private AITriggerController triggerController;
        public StateController controller;
        public bool isFut;
        public bool isVolei;

        #region Unity Function

        private void Start()
        {
            triggerController = GetComponent<AITriggerController>();
            ballrb = triggerController.rbBola;
        }

        private void Update()
        {
            if (isFut)
            {
                if (triggerController.triggerCollision.touchBall && triggerController.triggerCollision.naArea == false)
                {
                    Chuta();
                    triggerController.triggerCollision.touchBall = false;
                }

                else if (triggerController.triggerCollision.chutouBall && triggerController.triggerCollision.naArea == false)
                {
                    ChutaForte();
                    triggerController.triggerCollision.chutouBall = false;
                }

                else if (triggerController.triggerCollision.naArea && triggerController.triggerCollision.chutouBall)
                {
                    NaArea();
                    triggerController.triggerCollision.chutouBall = false;
                }
            }

            if (isVolei)
            {
                if (triggerController.triggerCollision.touchBall)
                {
                    Corta();
                }
            }
        }

        #endregion

        #region Public Functions

        #endregion

        #region Private Functions

        private void Corta()
        {
            if (ballrb == null) { ballrb = triggerController.rbBola; }
            ballrb.velocity = new Vector2(0, 0);

            ballrb.AddForce(new Vector2(headForceX * 5, headForceY * 5), ForceMode2D.Impulse);
        }

        private void Chuta()
        {
            if (ballrb == null) { ballrb = triggerController.rbBola; }
            ballrb.velocity = new Vector2(0, 0);

            ballrb.AddForce(new Vector2(Random.Range(-5, -10), Random.Range(5, 10)), ForceMode2D.Impulse);
        }

        private void ChutaForte()
        {
            if (ballrb == null) { ballrb = triggerController.rbBola; }
            ballrb.AddForce(new Vector2(Random.Range(-10, -15), Random.Range(-3, 3)), ForceMode2D.Impulse);
        }

        private void NaArea()
        {
            if (ballrb == null) { ballrb = triggerController.rbBola; }
            ballrb.AddForce(new Vector2(headForceX, 0), ForceMode2D.Impulse);
        }

        #endregion
    }
}
