using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {
    public float rot_speed = 100.0f;

    public GameObject Player;
    public GameObject MainCamera;

    private float camera_dist = 0f;
    public float camera_width = -10f;
    public float camera_height = 4f;
    public float camera_fix = 3f;
    Vector3 dir;
	// Use this for initialization
	void Start () {
        Player = GameObject.FindGameObjectsWithTag("Player");
        MainCamera = GameObject.FindGameObjectsWithTag("MainCamera");

        //카메라리그에서 카메라까지의 길이
        camera_dist = Mathf.Sqrt(camera_width * camera_width + camera_height * camera_height);

        //카메라 리그에서 카메라 위치까지의 방향벡터
        dir = new Vector3(0, camera_height, camera_width).normalized;
	}
	
	// Update is called once per frame
	void Update () {
        //y축 기준 회전 
        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * Time.deltaTime * rot_speed, Space.World);
        //X축 기준 회전
        transform.Rotate(Vector3.left * Input.GetAxis("Mouse Y") * Time.deltaTime * rot_speed, Space.Self);

        transform.position = Player.transform.position;

        //레이캐스트할 벡터값

        Vector3 ray_target = transform.up * camera_height + transform.forward * camera_width;

        RaycastHit hitinfo;
        Physics.Raycast(transform.position, ray_target, out hitinfo, camera_dist);

        if (hitinfo.point != Vector3.zero)
        {
            //point로 옮김
            MainCamera.transform.position = hitinfo.point;
            //카메라 보정
            MainCamera.transform.Translate(dir * -1 * camera_fix);
        }
        else
        {
            //로컬좌표를 0으로 맞춤 (카메라리그로 옮김
            MainCamera.transform.localPosition = Vector3.zero;
            //카메라위치까지의 방향벡터 * 카메라 최대거리로 옮김
            MainCamera.transform.Translate(dir * camera_dist);
            //카메라 보정
            MainCamera.transform.Translate(dir * -1 * camera_fix);
        }
	}
}
