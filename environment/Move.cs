using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Move : MonoBehaviour
{
    public Transform target;
    public Vector3 direction;   //이동방향
    public float velocity;  //속도
    public float default_velocity;  //초기속도
  //  public float accelaration;  //가속도
    public Vector3 default_direction;   //초기방향

    void Start()
    {
        // 자동으로 움직일 방향 벡터랜덤으로생성
        default_direction.x = Random.Range(-1.0f, 1.0f);
        default_direction.z = Random.Range(-1.0f, 1.0f);
        // 가속도 지정 (추후 힘과 질량, 거리 등 계산해서 수정할 것)
        velocity = 0.2f;    //속도지정
      //  accelaration = 0.3f;
        default_velocity = 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        // Player의 현재 위치를 받아오는 Object
        target = GameObject.Find("FPSController").transform;
        // Player와 객체 간의 거리 계산
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= 8.0f)  //일정거리가 가까워지면
        {
        //    MoveToTarget();
            // 일정 거리안에 있다가 다시 멀어졌을 때, 일정거리안에 있었던 player의 방향으로 움직임
            default_direction = direction;
            velocity = 0.0f;
        }
        // 일정거리 밖에 있을 시, 속도 초기화하고 해당 방향으로 무빙 
        else
        {
            velocity = 0.1f;
            this.transform.position = new Vector3(transform.position.x + (default_direction.x * default_velocity),
                                                  transform.position.y,
                                                  transform.position.z + (default_direction.y * default_velocity));
        }
    }

    public void MoveToTarget()
    {
        // Player의 위치와 이 객체의 위치를 빼고 단위 벡터화 한다.
        direction = (target.position - transform.position).normalized;
        // 초가 아닌 한 프레임으로 가속도 계산하여 속도 증가
       // velocity = (velocity + accelaration * Time.deltaTime);
        // 해당 방향으로 무빙
        this.transform.position = new Vector3(transform.position.x + (direction.x * velocity),
                                              transform.position.y,
                                                  transform.position.z + (direction.y * velocity));

    }
}

/*  회전
 private void Start()
{
    var axis = new Vector3(0, 1, 0);
transform.Rotate(axis, Time.deltaTime*100);
}
*/