using System.Collections;
using Extension;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Preloader
{
    public class Preloader : MonoBehaviour
    {
        [SerializeField] private Image _fillingCircle;
        
        [Header("UI scene"), Scene]
        [SerializeField] private string _scene;
        
        void Start()
        {
           StartCoroutine(LoadMain());
        }

        private IEnumerator LoadMain()
        {
            float current = 0;
            while (true)
            {
                current += Random.Range(0f, 0.1f);
                if (current > 1)
                    current = 1;

                if (_fillingCircle != null)
                {
                    _fillingCircle.fillAmount = current;
                }
                yield return new WaitForSeconds(0.1f);
                if (current == 1)
                    break;
            }
            
            SceneManager.LoadScene(_scene, LoadSceneMode.Additive);
            Destroy(gameObject);
        }
    }
}