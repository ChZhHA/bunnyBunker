using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class CarrotController : MonoBehaviour
    {
        public static CarrotController Select;
        private SpriteRenderer sprite;
        private Animator animator;
        private float digProgress = 0.752f;
        // Use this for initialization
        void Start()
        {
            sprite = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            bool highlight = false;
            var distance = Vector2.Distance(mousePosition, transform.position);
            if (distance < 1.5f)
            {
                if (Select != this && ((Select != null && Vector2.Distance(mousePosition, Select.transform.position) > distance) || Select == null))
                {
                    //高亮
                    Select = this;
                }
            }
            else if (Select == this)
            {
                Select = null;
            }
            if (Select == this)
            {
                highlight = true;
            }
            sprite.material.SetInt("_Enable", highlight ? 1 : 0);
            sprite.material.SetFloat("_Progress", digProgress);

        }

        public void Dig()
        {
            digProgress -= 0.34f * 0.4f;
            animator.SetTrigger("dig");
            if (digProgress <= 0.3f)
            {
                //TODO 获得萝卜
                Select = null;
                GameManager.carrotCounter+=3;
                Destroy(gameObject);
            }
        }

        private void OnDestroy()
        {
            if (Select == this)
            {
                Select = null;
            }
        }

    }
}