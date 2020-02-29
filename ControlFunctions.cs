using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GK_Lab_4_v2
{
    public partial class Form1 : Form
    {
        private void FOVTrackBar_ValueChanged(object sender, EventArgs e)
        {
            FOVLabel.Text = FOVTrackBar.Value.ToString();
            Refresh();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Refresh();
        }

        private void pictureBox1_Scroll(object sender, MouseEventArgs e)
        {
            int delta = -e.Delta / 40;
            int sum = FOVTrackBar.Value + delta;
            if (sum < FOVTrackBar.Minimum)
            {
                FOVTrackBar.Value = FOVTrackBar.Minimum;
            }
            else if (sum > FOVTrackBar.Maximum)
            {
                FOVTrackBar.Value = FOVTrackBar.Maximum;
            }
            else
            {
                FOVTrackBar.Value = sum;
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            grabPoint = new Point(e.X, e.Y);
            dragging = true;
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.SizeAll;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                int dx = e.X - grabPoint.X;
                int dy = e.Y - grabPoint.Y;
                Vector3 forward = cameraTarget - cameraPosition;
                forward = Vector3.Normalize(forward);
                Vector3 right = Vector3.Cross(forward, cameraUpVector);
                right = Vector3.Normalize(right);
                Vector3 up = Vector3.Cross(right, forward);
                up = Vector3.Normalize(up);
                Vector3 tmp = cameraPosition - cameraTarget;
                Accord.Math.Vector3 focusVector = new Accord.Math.Vector3(tmp.X, tmp.Y, tmp.Z);

                //yaw
                double yaw = 2 * Math.PI * (dx / 2000.0);
                Accord.Math.Matrix3x3 yawMatrix = new Accord.Math.Matrix3x3()
                {
                    V00 = (float)Math.Cos(yaw),
                    V01 = 0,
                    V02 = (float)Math.Sin(yaw),
                    V10 = 0,
                    V11 = 1,
                    V12 = 0,
                    V20 = -(float)Math.Sin(yaw),
                    V21 = 0,
                    V22 = (float)Math.Cos(yaw)
                };
                Accord.Math.Matrix3x3 yawMatrix1 = new Accord.Math.Matrix3x3()
                {
                    V00 = (float)Math.Cos(yaw),
                    V01 = -(float)Math.Sin(yaw),
                    V02 = 0,
                    V10 = (float)Math.Sin(yaw),
                    V11 = (float)Math.Cos(yaw),
                    V12 = 0,
                    V20 = 0,
                    V21 = 0,
                    V22 = 1
                };
                Accord.Math.Matrix3x3 W = new Accord.Math.Matrix3x3()
                {
                    V00 = 0,
                    V01 = -up.Z,
                    V02 = up.Y,
                    V10 = up.Z,
                    V11 = 0,
                    V12 = -up.X,
                    V20 = -up.Y,
                    V21 = up.X,
                    V22 = 0
                };
                float sin = (float)Math.Sin(yaw);
                float sin2 = (float)Math.Sin(yaw / 2);
                var yawMatrix2 = Accord.Math.Matrix3x3.Identity + Accord.Math.Matrix3x3.Multiply(W, sin) + Accord.Math.Matrix3x3.Multiply(Accord.Math.Matrix3x3.Multiply(W, W), 2 * sin2 * sin2);
                focusVector = Accord.Math.Matrix3x3.Multiply(yawMatrix2, focusVector);

                //pitch
                double pitch = -2 * Math.PI * (dy / 2000.0);
                W = new Accord.Math.Matrix3x3()
                {
                    V00 = 0,
                    V01 = -right.Z,
                    V02 = right.Y,
                    V10 = right.Z,
                    V11 = 0,
                    V12 = -right.X,
                    V20 = -right.Y,
                    V21 = right.X,
                    V22 = 0
                };
                sin = (float)Math.Sin(pitch);
                sin2 = (float)Math.Sin(pitch / 2);
                var pitchMatrix = Accord.Math.Matrix3x3.Identity + Accord.Math.Matrix3x3.Multiply(W, sin) + Accord.Math.Matrix3x3.Multiply(Accord.Math.Matrix3x3.Multiply(W, W), 2 * sin2 * sin2);
                focusVector = Accord.Math.Matrix3x3.Multiply(pitchMatrix, focusVector);

                cameraPosition = new Vector3(focusVector.X + cameraTarget.X, focusVector.Y + cameraTarget.Y, focusVector.Z + cameraTarget.Z);
                grabPoint = new Point(e.X, e.Y);
                Refresh();
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void lightTimer_Tick(object sender, EventArgs e)
        {
            lightSource.X += 0.04f;
            lightSource.Y += 0.04f;
            if (lightSource.X >= 2.0f)
            {
                lightMoveButton.Text = "Start";
                lightTimer.Stop();
                lightSource = new Vector3(3, 3, 5);
                Refresh();
                return;

            }
            var distSqr = lightSource.X * lightSource.X + lightSource.Y * lightSource.Y;
            lightSource.Z = (float)Math.Sqrt(9 - distSqr);
            Refresh();
        }

        private void lightMoveButton_Click(object sender, EventArgs e)
        {
            if (lightMoveButton.Text == "Start")
            {
                lightMoveButton.Text = "Stop";
                lightSource = new Vector3(-2, -2, 1);
                lightTimer.Start();
                lightTimer_Tick(sender, e);
            }
            else
            {
                lightMoveButton.Text = "Start";
                lightTimer.Stop();
                lightSource = new Vector3(3, 3, 5);
                Refresh();
            }
        }

        private void flatShading_CheckedChanged(object sender, EventArgs e)
        {
            Refresh();
        }

        private void rotationTimer_Tick(object sender, EventArgs e)
        {
            if (followingRadioButton.Checked)
            {
                cameraTarget = piecePosition / 10;
            }
            else if (movingRadioButton.Checked)
            {
                switch (direction)
                {
                    case 0:
                        cameraPosition = new Vector3(piecePosition.X / 2, piecePosition.Y / 2, 0.4f);
                        cameraTarget = cameraPosition;
                        cameraTarget.X += 10f;
                        cameraTarget = new Vector3(0, 0, 0);
                        break;
                    case 1:
                        cameraPosition = new Vector3(piecePosition.X / 2, piecePosition.Y / 2, 0.4f);
                        cameraTarget = cameraPosition;
                        cameraTarget.Y += 10f;
                        cameraTarget = new Vector3(0, 0, 0);
                        break;
                    case 2:
                        cameraPosition = new Vector3(piecePosition.X / 2, piecePosition.Y / 2, 0.4f);
                        cameraTarget = cameraPosition;
                        cameraTarget.X -= 0.2f;
                        cameraTarget = new Vector3(0, 0, 0);
                        break;
                    case 3:
                        cameraPosition = new Vector3(piecePosition.X / 2, piecePosition.Y / 2, 0.4f);
                        cameraTarget = cameraPosition;
                        cameraTarget.Y -= 0.2f;
                        cameraTarget = new Vector3(0, 0, 0);

                        break;
                }
            }
            else
            {
                cameraTarget = new Vector3(0, 0, 0);
            }
            angle += 0.1;
            if (piecePosition.X > 7.0f) direction = 1;
            if (piecePosition.Y > 7.0f) direction = 2;
            if (piecePosition.X < -7.0f) direction = 3;
            if (piecePosition.Y < -7.0f) direction = 0;
            switch (direction)
            {
                case 0:
                    piecePosition.X += 0.1f;
                    piecePosition.Y = -7.0f;
                    break;
                case 1:
                    piecePosition.Y += 0.1f;
                    piecePosition.X = 7.0f;
                    break;
                case 2:
                    piecePosition.X -= 0.1f;
                    piecePosition.Y = 7.0f;
                    break;
                case 3:
                    piecePosition.Y -= 0.1f;
                    piecePosition.X = -7.0f;
                    break;
            }
            Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sphere = creator.Create((int)numericUpDown.Value);
            Refresh();
        }

        private static int mod(int x, int m)
        {
            return (x % m + m) % m;
        }
    }
}
