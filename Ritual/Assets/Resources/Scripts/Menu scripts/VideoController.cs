using UnityEngine;
using System.Collections;

public class VideoController : MonoBehaviour {

	private MovieTexture movie;
	void Start() {
		Renderer r = GetComponent<Renderer>();
		MovieTexture movie = (MovieTexture)r.material.mainTexture;

		movie.Play();
		movie.loop=true;

	}

}
