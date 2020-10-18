using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShaderManager : MonoBehaviour
{
    public GameObject rootMesh = null;
    public GameObject[] childMeshs = null;
    public string[] shaderNames = null; 

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnGUI()
    {
        GUILayout.Box( "Replace Shader", GUILayout.Width( 170 ), GUILayout.Height( 100 ) );
        Rect screenRect = new Rect( 10, 25, 150, 100 );
        GUILayout.BeginArea( screenRect );

        foreach( var animation in shaderNames )
        {
            if( GUILayout.RepeatButton( animation ) )
            {
                ReplaceShader( animation );
            }
        }
        GUILayout.EndArea();
    }

    void ReplaceShader( string name )
    {
        Shader shader = Shader.Find( name );
        if( shader == null )
        {
            Debug.LogError( "undefined " + name );
            return;
        }

        ReplaceShader( this.rootMesh, shader );
        foreach( var child in this.childMeshs )
        {
            ReplaceShader( child, shader );
        }
    }

    void ReplaceShader( GameObject root, Shader shader )
    {
        Transform[] transforms = root.GetComponentsInChildren<Transform>( true );
        foreach( Transform transform in transforms )
        {
            Renderer renderer = transform.GetComponent<Renderer>();
            if( renderer != null )
            {
                foreach( Material mtrl in renderer.sharedMaterials )
                {
                    mtrl.shader = shader;
                }
            }
        }

    }
}
