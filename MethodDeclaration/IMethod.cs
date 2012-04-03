using System.Drawing;

namespace MethodDeclaration
{
    public interface IMethod
    {
        double DetectOrient(Bitmap img);
        double DetectOrient(string file);
    }
}
