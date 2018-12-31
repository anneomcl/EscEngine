using System.Diagnostics;

namespace Esckie.Test.TestActions
{
    public class CameraEscAction : EscAction
    {
        public static bool CameraToObject(string obj)
        {
            Debug.WriteLine($"Camera to: {obj}");
            return true;
        }
    }
}
