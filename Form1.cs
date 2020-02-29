using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GK_Lab_4_v2
{
    public partial class Form1 : Form
    {
        public int n = 1, f = 100;
        public List<Triangle> boardUnderside;
        public List<Triangle> board;
        public List<Triangle> sphere;
        public List<Piece> pieces = new List<Piece>();
        public Piece movingPiece;
        public double angle = 0;
        public Vector3 cameraPosition = new Vector3(0, 3, 1);
        public Vector3 cameraTarget = new Vector3(0, 0, 0);
        public Vector3 cameraUpVector = new Vector3(0, 0, 1);
        public Vector3 lightSource = new Vector3(3, 3, 5);
        public double[,] zBuffer;
        public Point grabPoint;
        public bool dragging = false;
        public Vector3 piecePosition = new Vector3(-7.0f, -7.0f, 0);
        int direction = 0;
        Bitmap bmp;
        SphereCreator creator = new SphereCreator();
        readonly double spotlightAngle = Math.Cos(Math.PI / 30);
        readonly double spotlightFadeAngle = Math.Cos(Math.PI / 24);

        BitmapData bitmapData;
        byte[] pixels;

        public Form1()
        {
            board = new List<Triangle>();
            InitializeComponent();
            this.pictureBox1.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_Scroll);
            Random r = new Random();
            zBuffer = new double[pictureBox1.Width, pictureBox1.Height];
            Vector4[] pieceTris = new Vector4[8] {new Vector4(-0.5f,-0.5f,0,1), new Vector4(0.5f, -0.5f, 0, 1), new Vector4(0.5f, 0.5f, 0, 1), new Vector4(-0.5f, 0.5f, 0, 1),
                                               new Vector4(-0.5f,-0.5f,1,1), new Vector4(0.5f,-0.5f,1,1), new Vector4(0.5f,0.5f,1,1), new Vector4(-0.5f,0.5f,1,1),};

            Vector4[] boardUndersidePoints = new Vector4[8] {new Vector4(-1, -1, -0.2f, 1), new Vector4(1, -1, -0.2f, 1), new Vector4(1, 1, -0.2f, 1), new Vector4(-1, 1, -0.2f, 1),
                                               new Vector4(-1, -1, 0, 1), new Vector4(1, -1, 0, 1), new Vector4(1, 1, 0, 1), new Vector4(-1, 1, 0, 1)};
            Color color = Color.SaddleBrown;
            board.Add(new Triangle(boardUndersidePoints[0], boardUndersidePoints[3], boardUndersidePoints[1], color));
            board.Add(new Triangle(boardUndersidePoints[1], boardUndersidePoints[3], boardUndersidePoints[2], color));
            board.Add(new Triangle(boardUndersidePoints[1], boardUndersidePoints[5], boardUndersidePoints[4], color));
            board.Add(new Triangle(boardUndersidePoints[0], boardUndersidePoints[1], boardUndersidePoints[4], color));
            board.Add(new Triangle(boardUndersidePoints[5], boardUndersidePoints[2], boardUndersidePoints[6], color));
            board.Add(new Triangle(boardUndersidePoints[1], boardUndersidePoints[2], boardUndersidePoints[5], color));
            board.Add(new Triangle(boardUndersidePoints[2], boardUndersidePoints[3], boardUndersidePoints[6], color));
            board.Add(new Triangle(boardUndersidePoints[3], boardUndersidePoints[7], boardUndersidePoints[6], color));
            board.Add(new Triangle(boardUndersidePoints[0], boardUndersidePoints[7], boardUndersidePoints[3], color));
            board.Add(new Triangle(boardUndersidePoints[0], boardUndersidePoints[4], boardUndersidePoints[7], color));

            Vector4[,] boardPoints = new Vector4[11, 11];
            for (int i = 0; i <= 10; i++)
            {
                for (int j = 0; j <= 10; j++)
                {
                    boardPoints[i, j] = new Vector4(-1.0f + i * 0.2f, -1.0f + j * 0.2f, 0, 1);
                }
            }

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    color = Color.Black;
                    if (i == 0 || i == 9 || j == 0 || j == 9)
                    {
                        color = Color.SaddleBrown;
                    }
                    else if ((i % 2 == 0 && j % 2 == 0) || (i % 2 == 1 && j % 2 == 1))
                    {
                        color = Color.White;
                    }
                    board.Add(new Triangle(boardPoints[i, j], boardPoints[i + 1, j + 1], boardPoints[i, j + 1], color));
                    board.Add(new Triangle(boardPoints[i, j], boardPoints[i + 1, j], boardPoints[i + 1, j + 1], color));
                }
            }

            pieces.Add(new Piece(6, 2, pieceTris));
            pieces.Add(new Piece(3, 5, pieceTris));
            pieces.Add(new Piece(7, 4, pieceTris));
            pieces.Add(new Piece(5, 6, pieceTris));
            movingPiece = new Piece(1, 1, pieceTris);

            sphere = creator.Create(2);
            rotationTimer.Start();

            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.MouseWheel += new System.Windows.Forms.MouseEventHandler(pictureBox1_Scroll);
            pictureBox1.BackColor = Color.SkyBlue;
            Refresh();
        }

        private new void Refresh()
        {
            pictureBox1.Refresh();
            pictureBox1.Image = bmp;
            pictureBox1.Invalidate();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            zBuffer = new double[pictureBox1.Width, pictureBox1.Height];
            for (int i = 0; i < pictureBox1.Width; ++i)
            {
                for (int j = 0; j < pictureBox1.Height; ++j)
                {
                    zBuffer[i, j] = double.MinValue;
                }
            }

            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            bitmapData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, bmp.PixelFormat);
            int byteCount = bitmapData.Stride * bmp.Height;
            pixels = new byte[byteCount];
            IntPtr ptrFirstPixel = bitmapData.Scan0;

            Matrix4x4 modelMatrix = GetModelForBoard();
            foreach (Triangle t in board)
            {
                DrawFace(t, modelMatrix, board);
            }
            foreach (Piece p in pieces)
            {
                modelMatrix = GetModelForStillPiece(p.x, p.y);
                foreach (var t in p.triangles)
                {
                    DrawFace(t, modelMatrix, p.triangles);
                }
            }
            modelMatrix = GetModelForSphere();
            foreach(Triangle t in sphere)
            {
                DrawFace(t, modelMatrix, sphere);
            }

            modelMatrix = GetModelForMovingPiece();
            movingPiece.UpdateNormals(modelMatrix);
            foreach (Triangle t in movingPiece.triangles)
            {
                DrawFace(t, modelMatrix, movingPiece.triangles);
            }
            Marshal.Copy(pixels, 0, ptrFirstPixel, pixels.Length);
            bmp.UnlockBits(bitmapData);
        }

        private void AmbientLight(ref byte r, ref byte g, ref byte b)
        {
            int change = 20;
            r = (byte)(r + change > 255 ? 255 : r + change);
            g = (byte)(g + change > 255 ? 255 : g + change);
            b = (byte)(b + change > 255 ? 255 : b + change);
        }

        public void DrawFace(Triangle t, Matrix4x4 modelMatrix, List<Triangle> triangles)
        {
            var p1 = GetPointVector(t.points[0], modelMatrix);
            var p2 = GetPointVector(t.points[1], modelMatrix);
            var p3 = GetPointVector(t.points[2], modelMatrix);
            NormalizeVector(ref p1);
            NormalizeVector(ref p2);
            NormalizeVector(ref p3);

            Vector3[] points3d = new Vector3[3];
            for (int i = 0; i < 3; ++i)
            {
                var vec = Vector4.Transform(t.points[i], modelMatrix);
                points3d[i] = new Vector3(vec.X, vec.Y, vec.Z);
            }

            Vector3 spotlightPosition = piecePosition / 10;

            //flat shading
            Vector3 spotlightVector = Vector3.Normalize(new Vector3(0, 0, 1.0f) - spotlightPosition);
            Vector3 lightVector = Vector3.Normalize(new Vector3(0, 0, 0) - lightSource);

            double cos = Vector3.Dot(t.normal, lightVector);
            if (cos < 0 || double.IsNaN(cos)) cos = 0;

            //check if object inside spotlight cone
            Vector3 triMiddle = (points3d[0] + points3d[1] + points3d[2]) / 3;
            Vector3 spotlightDir = Vector3.Normalize(triMiddle - spotlightPosition);
            double theta = Vector3.Dot(spotlightDir, spotlightVector);
            if (theta > spotlightFadeAngle)
            {
                double cos2 = Vector3.Dot(t.normal, spotlightVector);
                if (theta < spotlightAngle)
                {
                    cos2 *= (theta - spotlightFadeAngle) / (spotlightAngle - spotlightFadeAngle);
                }
                if (cos2 < 0 || double.IsNaN(cos2)) cos2 = 0;
                cos += cos2;
                if (cos > 1) cos = 1;
            }

            byte r = (byte)((double)(t.color.R) * cos);
            byte g = (byte)((double)(t.color.G) * cos);
            byte b = (byte)((double)(t.color.B) * cos);
            AmbientLight(ref r, ref g, ref b);

            Vector3[] normals = new Vector3[3];
            double[] intensities = new double[3];
            if (!flatShading.Checked)
            {
                for (int i = 0; i < 3; i++)
                {
                    Vector3 normal = new Vector3();
                    int count = 0;
                    foreach (Triangle triangle in triangles)
                    {
                        if (triangle.points.Contains(t.points[i]))
                        {
                            normal += triangle.normal;
                            count++;
                        }
                    }
                    normal /= count;
                    normal = Vector3.Normalize(normal);
                    normals[i] = normal;

                    cos = Vector3.Dot(normal, lightVector);
                    if (cos < 0 || double.IsNaN(cos)) cos = 0;

                    //check if object inside spotlight cone
                    Vector3 p = points3d[i];
                    spotlightDir = Vector3.Normalize(p - spotlightPosition);
                    theta = Vector3.Dot(spotlightDir, spotlightVector);
                    if (theta > spotlightFadeAngle)
                    {
                        double cos2 = Vector3.Dot(normal, spotlightVector);
                        if (theta < spotlightAngle)
                        {
                            cos2 *= (theta - spotlightFadeAngle) / (spotlightAngle - spotlightFadeAngle);
                        }
                        if (cos2 < 0 || double.IsNaN(cos2)) cos2 = 0;
                        cos += cos2;
                        if (cos > 1) cos = 1;
                    }

                    intensities[i] = cos;
                }
            }

            List<Point> points = new List<Point>() { new Point((int)p1.X, (int)p1.Y), new Point((int)p2.X, (int)p2.Y), new Point((int)p3.X, (int)p3.Y) };
            int n = 3;
            int[] keys = new int[n];
            int[] ind = new int[n];
            for (int i = 0; i < n; i++)
            {
                keys[i] = points[i].Y;
                ind[i] = i;
            }
            Array.Sort(keys, ind);
            int ymin = points[ind[0]].Y;
            int ymax = points[ind[n - 1]].Y;
            List<Edge> activeEdges = new List<Edge>();
            int k = 0;

            for (int y = ymin; y <= ymax; y++)
            {
                while (points[ind[k]].Y == y - 1)
                {
                    int indP = ind[k];
                    int indPrev = mod((ind[k] - 1), n);
                    Point p = points[indP];
                    Point prevP = points[indPrev];
                    if (prevP.Y > p.Y)
                    {
                        activeEdges.Add(new Edge(prevP, p, indPrev, indP));
                    }
                    else if (prevP.Y < p.Y)
                    {
                        activeEdges.RemoveAll(e => e.ymin == prevP.Y && e.ymax == p.Y);
                    }
                    int indNext = mod((ind[k] + 1), n);
                    Point nextP = points[indNext];
                    if (nextP.Y > p.Y)
                    {
                        activeEdges.Add(new Edge(nextP, p, indNext, indP));
                    }
                    else if (nextP.Y < p.Y)
                    {
                        activeEdges.RemoveAll(e => e.ymin == nextP.Y && e.ymax == p.Y);
                    }
                    k++;
                }

                activeEdges.Sort(Comparer<Edge>.Create((e1, e2) => e1.x.CompareTo(e2.x)));
                if (activeEdges.Count >= 2)
                {
                    double dist, x1 = 0, x2 = 0, Ia = 0, Ib = 0;
                    Vector3 n1 = new Vector3();
                    Vector3 n2 = new Vector3();
                    Vector3 pt1 = new Vector3();
                    Vector3 pt2 = new Vector3();
                    if (gouraudShading.Checked)
                    {
                        int ind1 = activeEdges[0].minIndex;
                        int ind2 = activeEdges[0].maxIndex;
                        Point pMin = points[ind1];
                        Point pMax = points[ind2];
                        double pX = activeEdges[0].x;
                        dist = Math.Sqrt((pX - pMin.X) * (pX - pMin.X) + (y - pMin.Y) * (y - pMin.Y)) /
                                      Math.Sqrt((pMax.X - pMin.X) * (pMax.X - pMin.X) + (pMax.Y - pMin.Y) * (pMax.Y - pMin.Y));
                        Ia = dist * intensities[ind2] + (1 - dist) * intensities[ind1];

                        ind1 = activeEdges[1].minIndex;
                        ind2 = activeEdges[1].maxIndex;
                        pMin = points[ind1];
                        pMax = points[ind2];
                        pX = activeEdges[1].x;
                        dist = Math.Sqrt((pX - pMin.X) * (pX - pMin.X) + (y - pMin.Y) * (y - pMin.Y)) /
                                      Math.Sqrt((pMax.X - pMin.X) * (pMax.X - pMin.X) + (pMax.Y - pMin.Y) * (pMax.Y - pMin.Y));
                        Ib = dist * intensities[ind2] + (1 - dist) * intensities[ind1];

                        x1 = activeEdges[0].x;
                        x2 = activeEdges[1].x;
                    }
                    else if (phongShading.Checked)
                    {
                        int ind1 = activeEdges[0].minIndex;
                        int ind2 = activeEdges[0].maxIndex;
                        Point pMin = points[ind1];
                        Point pMax = points[ind2];
                        double pX = activeEdges[0].x;
                        dist = Math.Sqrt((pX - pMin.X) * (pX - pMin.X) + (y - pMin.Y) * (y - pMin.Y)) /
                                      Math.Sqrt((pMax.X - pMin.X) * (pMax.X - pMin.X) + (pMax.Y - pMin.Y) * (pMax.Y - pMin.Y));
                        n1 = new Vector3((float)(normals[ind2].X * dist + normals[ind1].X * (1 - dist)), (float)(normals[ind2].Y * dist + normals[ind1].Y * (1 - dist)), (float)(normals[ind2].Z * dist + normals[ind1].Z * (1 - dist)));
                        pt1 = new Vector3((float)(points3d[ind2].X * dist + points3d[ind1].X * (1 - dist)), (float)(points3d[ind2].Y * dist + points3d[ind1].Y * (1 - dist)), (float)(points3d[ind2].Z * dist + points3d[ind1].Z * (1 - dist)));

                        ind1 = activeEdges[1].minIndex;
                        ind2 = activeEdges[1].maxIndex;
                        pMin = points[ind1];
                        pMax = points[ind2];
                        pX = activeEdges[1].x;
                        dist = Math.Sqrt((pX - pMin.X) * (pX - pMin.X) + (y - pMin.Y) * (y - pMin.Y)) /
                                      Math.Sqrt((pMax.X - pMin.X) * (pMax.X - pMin.X) + (pMax.Y - pMin.Y) * (pMax.Y - pMin.Y));
                        n2 = new Vector3((float)(normals[ind2].X * dist + normals[ind1].X * (1 - dist)), (float)(normals[ind2].Y * dist + normals[ind1].Y * (1 - dist)), (float)(normals[ind2].Z * dist + normals[ind1].Z * (1 - dist)));
                        pt2 = new Vector3((float)(points3d[ind2].X * dist + points3d[ind1].X * (1 - dist)), (float)(points3d[ind2].Y * dist + points3d[ind1].Y * (1 - dist)), (float)(points3d[ind2].Z * dist + points3d[ind1].Z * (1 - dist)));

                        x1 = activeEdges[0].x;
                        x2 = activeEdges[1].x;
                    }

                    for (int pair = 0; pair < activeEdges.Count; pair += 2)
                    {
                        for (int x = (int)Math.Round(activeEdges[pair].x); x < (int)Math.Round(activeEdges[pair + 1].x); ++x)
                        {
                            double w1 = ((p2.Y - p3.Y) * (x - p3.X) + (p3.X - p2.X) * (y - p3.Y)) /
                                        ((p2.Y - p3.Y) * (p1.X - p3.X) + (p3.X - p2.X) * (p1.Y - p3.Y));

                            double w2 = ((p3.Y - p1.Y) * (x - p3.X) + (p1.X - p3.X) * (y - p3.Y)) /
                                        ((p2.Y - p3.Y) * (p1.X - p3.X) + (p3.X - p2.X) * (p1.Y - p3.Y));

                            double w3 = 1 - w1 - w2;
                            double z = (w1 * p1.Z + w2 * p2.Z + w3 * p3.Z) / (w1 + w2 + w3);
                            if (x >= pictureBox1.Width || x < 0 || y >= pictureBox1.Height || y < 0) continue;
                            if (z > zBuffer[x, y])
                            {
                                zBuffer[x, y] = z;

                                int currentLine = y * bitmapData.Stride;
                                int bx = x * 4;
                                if (currentLine + bx < pixels.Length)
                                {
                                    if (!flatShading.Checked)
                                    {
                                        double intensity = 0;
                                        dist = (x - x1) / (x2 - x1);
                                        if (gouraudShading.Checked)
                                        {
                                            intensity = dist * Ib + (1 - dist) * Ia;
                                        }
                                        else if (phongShading.Checked)
                                        {
                                            Vector3 normal = new Vector3((float)(n2.X * dist + n1.X * (1 - dist)), (float)(n2.Y * dist + n1.Y * (1 - dist)), (float)(n2.Z * dist + n1.Z * (1 - dist)));
                                            intensity = Vector3.Dot(normal, lightVector);
                                            if (intensity < 0 || double.IsNaN(intensity)) intensity = 0;

                                            Vector3 p = new Vector3((float)(pt2.X * dist + pt1.X * (1 - dist)), (float)(pt2.Y * dist + pt1.Y * (1 - dist)), (float)(pt2.Z * dist + pt1.Z * (1 - dist)));
                                            spotlightDir = Vector3.Normalize(p - spotlightPosition);
                                            theta = Vector3.Dot(spotlightDir, spotlightVector);
                                            if (theta > spotlightFadeAngle)
                                            {
                                                double intensity2 = Vector3.Dot(normal, spotlightVector);
                                                if (theta < spotlightAngle)
                                                {
                                                    intensity2 *= (theta - spotlightFadeAngle) / (spotlightAngle - spotlightFadeAngle);
                                                }
                                                if (intensity2 < 0 || double.IsNaN(intensity2)) intensity2 = 0;
                                                intensity += intensity2;
                                                if (intensity > 1) intensity = 1;
                                            }
                                        }
                                        r = (byte)(intensity * t.color.R);
                                        g = (byte)(intensity * t.color.G);
                                        b = (byte)(intensity * t.color.B);
                                        AmbientLight(ref r, ref g, ref b);
                                    }
                                    pixels[currentLine + bx] = b;
                                    pixels[currentLine + bx + 1] = g;
                                    pixels[currentLine + bx + 2] = r;
                                    pixels[currentLine + bx + 3] = 255;
                                }
                            }
                        }
                    }
                }
                foreach (Edge e in activeEdges)
                {
                    e.x += e.m;
                }
            }
        }
    }
}
