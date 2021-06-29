using UnityEditor;
using UnityEngine;

namespace ScaleMonk.Ads
{
    public class ExportPackage : MonoBehaviour
    {
        [MenuItem("ScaleMonk/Export")]
        static void Export()
        {
            ExportScaleMonk.Export();
        }
    }
}