using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GK_Lab_4_v2
{
    public partial class Form1 : Form
    {
        public Matrix4x4 LookAt()
        {
            return Matrix4x4.CreateLookAt(cameraPosition, cameraTarget, cameraUpVector);
        }

        public Vector4 GetPointVector(Vector4 v, Matrix4x4 model)
        {
            Matrix4x4 viewMatrix = LookAt();
            Matrix4x4 projectionMatrix = GetProjectionMatrix();
            var v1 = Vector4.Transform(v, model);
            var v2 = Vector4.Transform(v1, viewMatrix);
            var v3 = Vector4.Transform(v2, projectionMatrix);
            return v3;
        }

        public Matrix4x4 GetModelForStillPiece(int x, int y)
        {
            var m = new Matrix4x4(1, 0, 0, 0,
                                0, 1, 0, 0,
                                0, 0, 1, 0,
                                -0.9f + 0.2f * x, -0.9f + 0.2f * y, 0, 1);
            var m1 = Matrix4x4.Identity;
            m1 = Matrix4x4.Multiply(m1, 0.1f);
            m1.M44 = 1;
            return Matrix4x4.Multiply(m1, m);
        }

        public Matrix4x4 GetModelForMovingPiece()
        {
            var x = piecePosition.X;
            var y = piecePosition.Y;
            var m = new Matrix4x4(1, 0, 0, 0,
                                0, 1, 0, 0,
                                0, 0, 1, 0,
                                (float)x, (float)y, 0, 1);

            var m1 = GetRotationMatrix();
            m = Matrix4x4.Multiply(m1, m);
            m1 = Matrix4x4.Identity;
            m1 = Matrix4x4.Multiply(m1, 0.1f);
            m1.M44 = 1;
            return Matrix4x4.Multiply(m, m1);
        }

        public Matrix4x4 GetModelForBoard()
        {
            return Matrix4x4.Identity;
        }

        public Matrix4x4 GetModelForSphere()
        {
            var m = new Matrix4x4(1, 0, 0, 0,
                                0, 1, 0, 0,
                                0, 0, 1, 0,
                                0, 0, 1, 1);
            var m1 = Matrix4x4.Identity;
            m1 = Matrix4x4.Multiply(m1, 0.2f);
            m1.M44 = 1;
            return Matrix4x4.Multiply(m1, m);
        }
        public Matrix4x4 GetRotationMatrix()
        {
            return new Matrix4x4((float)Math.Cos(angle), (float)Math.Sin(angle), 0, 0,
                                   -(float)Math.Sin(angle), (float)Math.Cos(angle), 0, 0,
                                   0, 0, 1, 0,
                                   0, 0, 0, 1);
        }
        public Matrix4x4 GetModelMatrix2(double angle)
        {
            return new Matrix4x4((float)Math.Cos(angle), 0, (float)Math.Sin(angle), 0,
                                 0, 1, 0, 0,
                                 -(float)Math.Sin(angle), 0, (float)Math.Cos(angle), 0,
                                 0.3f, 0.2f, 0.1f, 1);
        }

        public Matrix4x4 GetProjectionMatrix()
        {
            double e = 1.0 / Math.Tan((double)FOVTrackBar.Value * (Math.PI / 180.0) / 2);
            double a = (double)pictureBox1.Height / (double)pictureBox1.Width;
            return new Matrix4x4((float)e, 0, 0, 0,
                                 0, (float)(e / a), 0, 0,
                                 0, 0, (f + n) / (n - f), (2 * f * n) / (f - n),
                                 0, 0, -1, 0);
        }
        public void NormalizeVector(ref Vector4 v)
        {
            v.X /= v.W;
            v.X = (v.X + 1) * (pictureBox1.Width / 2);
            v.Y /= v.W;
            v.Y = (v.Y + 1) * (pictureBox1.Height / 2);
            v.Z /= v.W;
            v.W = 1;
        }
    }
}
