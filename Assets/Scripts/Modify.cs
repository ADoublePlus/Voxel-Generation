using UnityEngine;

namespace VoxelGeneration
{
    public class Modify : MonoBehaviour
    {
        public Camera cam;

        Ray ray;
        
        // Update is called once per frame
        void Update ()
        {
            ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Input.GetMouseButton(0))
            {
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100))
                {
                    EditTerrain.SetBlock(hit, new BlockAir());
                }
            }
        }
    }
}