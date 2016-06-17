using UnityEngine;
using System.Collections;


[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class PolyLensCamera : MonoBehaviour {

	public Camera MyCamera;
	public PolyLensOrientation Orientation;
	public Material PostBlitShader;

	void Start()
	{
		MyCamera = GetComponent<Camera> ();
	}

	private Vector3 OldPosition;
	private Quaternion OldRotation;

	void OnPreRender() {
		OldPosition = MyCamera.transform.localPosition;
		OldRotation = MyCamera.transform.localRotation;
	
		//	move according to gyro/tracker

		//	orientate for poly lens
		//	gr: this should be fixed, then orientation is tracker/gyro
		if ( Orientation != null )
		{
			MyCamera.transform.localPosition += Orientation.transform.position;
			MyCamera.transform.localRotation *= Orientation.transform.rotation;
		}
	}

	void OnRenderImage(RenderTexture src, RenderTexture dest) 
	{
		if ( PostBlitShader != null )
			Graphics.Blit(src, dest, PostBlitShader );
		else
			Graphics.Blit(src, dest );
	}

	void OnPostRender() {
		//	revert settings
		MyCamera.transform.localPosition = OldPosition;
		MyCamera.transform.localRotation = OldRotation;
	}


}

