using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GK_Lab_4_v2
{
    public class Piece
    {
        public List<Triangle> triangles;
        public int x;
        public int y;
        public Piece(int x, int y, List<Triangle> tris)
        {
            this.x = x;
            this.y = y;
            triangles = new List<Triangle>();
            foreach(var t in tris)
            {
                triangles.Add(t);
            }
        }
        public Piece(int x, int y, Vector4[] p)
        {
            this.x = x;
            this.y = y;
            triangles = new List<Triangle>();
            Color color = Color.AntiqueWhite;

            triangles.Add(new Triangle(p[0], p[3], p[1], color));
            triangles.Add(new Triangle(p[1], p[3], p[2], color));
            triangles.Add(new Triangle(p[1], p[5], p[4], color));
            triangles.Add(new Triangle(p[0], p[1], p[4], color));
            triangles.Add(new Triangle(p[5], p[2], p[6], color));
            triangles.Add(new Triangle(p[1], p[2], p[5], color));
            triangles.Add(new Triangle(p[2], p[3], p[6], color));
            triangles.Add(new Triangle(p[3], p[7], p[6], color));
            triangles.Add(new Triangle(p[0], p[7], p[3], color));
            triangles.Add(new Triangle(p[0], p[4], p[7], color));
            triangles.Add(new Triangle(p[4], p[5], p[6], color));
            triangles.Add(new Triangle(p[4], p[6], p[7], color));
        }
        public void MoveTo(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public void UpdateNormals(Matrix4x4 model)
        {
            foreach (Triangle t in this.triangles)
            {
                t.UpdateNormal(model);
            }
        }
    }
    public class Triangle
    {
        public Vector4[] points;
        public Color color;
        public Vector3 normal;
        public Triangle(Color _color)
        {
            points = new Vector4[3];
            color = _color;
        }
        public Triangle(Vector4 p1, Vector4 p2, Vector4 p3, Color _color)
        {
            points = new Vector4[3];
            points[0] = p1;
            points[1] = p2;
            points[2] = p3;
            color = _color;
            UpdateNormal(Matrix4x4.Identity);
        }
        public void UpdateNormal(Matrix4x4 model)
        {
            Vector4 p1 = Vector4.Transform(points[0], model);
            Vector4 p2 = Vector4.Transform(points[1], model);
            Vector4 p3 = Vector4.Transform(points[2], model);
            Vector4 v1 = p3 - p1;
            Vector4 v2 = p2 - p1;
            Vector3 cross = Vector3.Cross(new Vector3(v1.X, v1.Y, v1.Z), new Vector3(v2.X, v2.Y, v2.Z));
            normal = Vector3.Normalize(cross);
        }
    }

    public class Edge
    {
        public int minIndex;
        public int maxIndex;
        public int ymax;
        public int ymin;
        public double x;
        public double m;

        public Edge(Point p1, Point p2, int ind1, int ind2)
        {
            if (p1.Y > p2.Y)
            {
                ymin = p2.Y;
                minIndex = ind2;
                ymax = p1.Y;
                maxIndex = ind1;
                x = p2.X;
            }
            else
            {
                ymin = p1.Y;
                minIndex = ind1;
                ymax = p2.Y;
                maxIndex = ind2;
                x = p1.X;
            }
            m = (double)(p2.X - p1.X) / (double)(p2.Y - p1.Y);
        }
    }
}
