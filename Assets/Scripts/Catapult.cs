using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

public class Catapult : MonoBehaviour
{
    [SerializeField]
    private  int _number;//количество точек через инспектор
    [SerializeField]
    private float forceFactor;//задаем силу через инспектор 
    [SerializeField]
    private  GameObject _trajectoryDotPrefab;//префаб точки с помощью которой будет строится луч 
    private Vector2 _startPosition;// векторные переменные, котороя будет использоваться для хранения стартовая позиция череп
    private Vector2 _endPosition; // векторные переменные, котороя будет использоваться для хранения конечной позиций черепа
    private Vector2 _initPossition;// векторные переменные, котороя будет использоваться для хранения начальная позиция 
    private Vector2 forceAtPlayer;//вектора для хранения силы, направленной на персонажа.
    private Rigidbody2D _rigidbody;//ссылки на компоненты Rigidbody2D
    private GameObject[] trajectoryDots;//массив точек из которых строится луч
   
    
    
    private void Start()
    {
        
        // присваеваем переменной _rigidbody компонента Rigidbody2D, который находится на том же игровом объекте, что и данный скрипт.
        _rigidbody = GetComponent<Rigidbody2D>();
        // инициализирует массив, размер которого задается переменной _number.
        trajectoryDots = new GameObject[_number];
    }
  

    private void Update()
    {
    
        Click();
        DragProjectile();
        LowerTheButton();
   
    }
    
    private void DragProjectile()
    {
        if(Input.GetMouseButton(0))
        {
          
                // Определяем конечную позицию мыши на экране и сохраняют ее в глобальную переменную
                _endPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10);

                
                    // Перемещаем череп на позицию конечной точки
                    gameObject.transform.position = _endPosition;
                    // Вычисляем и сохраняем силу, приложенную к объекту для дальнейших вычислений
                    forceAtPlayer = _endPosition - _startPosition;
                    // Цикл вычисляет новые позиции элементов, изображающих траекторию полета объекта
                    for (int i = 0; i < _number; i++)
                    {
                        trajectoryDots[i].transform.position = calculatePosition(i * 0.1f);
                    }
      
        }
       
    }
    
   
    private void LowerTheButton()
    {
        if(Input.GetMouseButtonUp(0))
        {
            
            StartCoroutine(LetGo() );
            //при отпускание кнопки устанавливаем гравитацию на 1 придовае вес
            _rigidbody.gravityScale = 1;
            //определяем вектор скорости 
            _rigidbody.velocity = new Vector2(-forceAtPlayer.x * forceFactor, -forceAtPlayer.y * forceFactor);
            for (int i = 0; i < _number; i++)// цикл для уничтожения всех точек траектории в массиве trajectoryDots.
            {
                Destroy(trajectoryDots[i]);
            }
        }
    }

   
    private void Click()
    {
        if(Input.GetMouseButtonDown(0)) 
        {
          

            _startPosition = gameObject.transform.position;// записываем позицию черепа в _startPosition
                //создаются точки траектории движения объекта в количестве, заданном переменной _number.
                for (int i = 0; i < _number; i++)
                {
                    trajectoryDots[i] = Instantiate(_trajectoryDotPrefab, gameObject.transform);
                }
            
        }
    }

    // Метод рассчитывает позицию черепа на основе падения под влиянием силы, заданной вектором forceAtPlayer
    private Vector2 calculatePosition(float elapsedTime)
    {
        return new Vector2(_endPosition.x, _endPosition.y) + 
                new Vector2(-forceAtPlayer.x * forceFactor, -forceAtPlayer.y * forceFactor) * elapsedTime + 
                0.5f * Physics2D.gravity * elapsedTime * elapsedTime ;
    }

    private IEnumerator LetGo()// при отпускание левой кнопки мыши объект выключается через 0.1 секунды
    {
       yield return new WaitForSeconds(0.1f);
        enabled = false;
      
    }
}
