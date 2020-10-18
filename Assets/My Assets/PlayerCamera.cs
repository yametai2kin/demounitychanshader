using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private GameObject camera = null;
    public GameObject owner = null;
    public Vector3 offset = new Vector3( 0, 1, 10 );
    public float angle = 0;
    public float speed = 15;

    // Start is called before the first frame update
    void Start()
    {
        this.camera = Camera.main.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if( this.owner == null )
        {
            return;
        }

        // 右クリックを押している時だけカメラを回転させる
        if( Input.GetMouseButton( 1 ) )
        {
            this.angle += Input.GetAxis( "Mouse X" ) * this.speed;
        }

        // ホイールによるカメラの前後移動
        this.offset.z -= Input.mouseScrollDelta.y;
        if( this.offset.z < 0.1f )
        {
            this.offset.z = 0.1f;
        }

        // プレイヤーを視点にカメラの位置を決定
        Vector3 rotOffset = Quaternion.Euler( 0, this.angle, 0 ) * this.offset;
        this.camera.transform.position = this.owner.transform.position + rotOffset;
        this.camera.transform.LookAt( this.owner.transform );
    }
}
