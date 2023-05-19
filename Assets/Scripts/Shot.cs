using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D _rigidbody; // для полета используем RigidBody2D;
    [SerializeField]
    private Rigidbody2D _shootRigit; //Максимальный радиус отягивания рогатки
    [SerializeField]
    private GameObject _newSkullPrefab; //Добавляем новые черепа
    [SerializeField]
    private Transform _newSkullSpawner; // Позиция где будут новые черепа
    [SerializeField] 
    private float _maxDictance = 2f;// максимально дистанция от точки
    
    public bool _isPressed; //нажатие кнопки мыши

    public float launchForce;

   public GameObject point;
   private GameObject[] points;
   public int numberOfPoints;
   public float spaceBetweenPoints;
   private Vector2 _direction;
  
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>(); //Получаем RigitBody2D
        points = new GameObject[numberOfPoints];
        for (int i = 0; i < numberOfPoints; i++)
        {
            points[i] = Instantiate(point, _newSkullSpawner.position, Quaternion.identity);
        }
    }

    private void Update()
    {
        if (_isPressed == true)
        {
            //позиция церепа равна позиции мыши которой будем управлять
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
           // если дистанция от нашего объекта до точки рогатки больше чем 2
           if (Vector2.Distance(mousePosition, _shootRigit.position) > _maxDictance)
           {
               //как только курсор вышел за пределы Unite(mousePosition - _shootRigit.position)
               //анулирует растояние а курсор остоетс за это отвечает normalized
               _rigidbody.position = _shootRigit.position + (mousePosition - _shootRigit.position).normalized * _maxDictance;
           }
           else
           {
               _rigidbody.position = mousePosition; // свободно передвигается по окружности если не выходит за ее пределы
           }

           Vector2 bowSkull = _newSkullSpawner.position;
           _direction = mousePosition - bowSkull;
           for (int i = 0; i < numberOfPoints; i++)
           {
               points[i].transform.position = PointPosition(i * spaceBetweenPoints);
           }
           
           
        }
    }

    private Vector2 PointPosition(float t)
    {
        Vector2 position = (Vector2)_newSkullSpawner.position + (_direction.normalized * launchForce * t) +
                           0.5f * Physics2D.gravity * (t * t);
        return position;
    }

    private void OnMouseDown() //когда нажали левой кнопкой мыши
     {
         _isPressed = true; // если нажали кнопку становится True
         _rigidbody.isKinematic = true; // Если нажали на кнопку череп становится Kinematic(не дергается)
         //Shoot();
         
     }

     private void OnMouseUp() //когда отпустили левую кнопку мыши 
     {
         _isPressed = false; // если отпустили становится False 
         _rigidbody.isKinematic = false;// Если отпустили  кнопку череп полетит
         StartCoroutine(LetGo());// включаем корутину
     }

   //  private void Shoot()
   //  {
   //    GameObject newSkull  = Instantiate(_newSkullPrefab, _newSkullSpawner.position, _newSkullSpawner.rotation);
   //    newSkull.GetComponent<Rigidbody2D>().velocity = transform.right * launchForce;
   //  }

   


     private IEnumerator LetGo() //Чтобы объект улетел нужна корутина
     {
         yield return new WaitForSeconds(0.1f); // через 0.1 на черепи отключается Spring Jint2D(череп становится снарядом)

         gameObject.GetComponent<SpringJoint2D>().enabled = false;
         enabled = false;// отклячаем скрипт чтобы нельзя было тыкать в череп

       //  yield return new WaitForSeconds(2f); //Появление нового снаряда будет происходить через 2 секунды после тпуска кнопки

        // if (_newSkullPrefab != null) //если черепа не закончились
        // {
        //     _newSkullPrefab.transform.position = _newSkullSpawner.transform.position;//если не закончились позиция будет ровна нашой переменной
        //     //Instantiate(_newSkullPrefab);
        // }
     }
}
