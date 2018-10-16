using UnityEngine;

namespace Play
{
    public class BackgroundScroller : MonoBehaviour
    {
        public float ScrollSpeed;
        public float TileSizeX;

        private Vector3 _startPosition;

        // Use this for initialization
        private void Start ()
        {
            _startPosition = transform.position;
        }
	
        // Update is called once per frame
        private void Update ()
        {
            var newPosition = Mathf.Repeat(Time.time * ScrollSpeed, TileSizeX);
            transform.position = _startPosition + Vector3.left * newPosition;
        }
    }
}
