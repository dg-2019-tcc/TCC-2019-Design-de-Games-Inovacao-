using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class ActionsAI : MonoBehaviour
    {
        #region Variables
        private Rigidbody2D rbBall;

        private AIController2D aiController2D;
        #endregion

        #region Unity Function
        void Start()
        {
            aiController2D = GetComponent<AIController2D>();
        }
        #endregion

        #region Public Functions
        public void KickAction(Rigidbody2D _rb , int _type)
        {
            if (rbBall == null) rbBall = _rb;
            switch (_type)
            {
                case 0:
                    TouchBall();
                    break;
                case 1:
                    if (aiController2D.collisions.below) { NormalKickBall(); }
                    else { SuperKickBall(); }
                    break;
            }
        }

        public void KickPlayer(NewPlayerMovent _playerMove)
        {
            _playerMove.KickedByOther();
            Debug.Log("[ActionsAI] KickPlayer()");
        }
        #endregion

        #region Private Functions
        void TouchBall()
        {
            rbBall.AddForce(new Vector2(Random.Range(-3,-5), Random.Range(3,10)), ForceMode2D.Impulse);
        }

        void NormalKickBall()
        {
            rbBall.AddForce(new Vector2(Random.Range(-5, -10), Random.Range(5, 10)), ForceMode2D.Impulse);
        }

        void SuperKickBall()
        {
            rbBall.AddForce(new Vector2(Random.Range(-10, -15), Random.Range(-3, 3)), ForceMode2D.Impulse);
        }

        #endregion
    }
}
