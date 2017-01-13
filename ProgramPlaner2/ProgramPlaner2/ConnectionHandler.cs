using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace ProgramPlaner2
{
    static  class ConnectionHandler
    {
        public static void DrawConnection(SectionClass s, Connection c, Graphics g)
        {
            Direction arrowDirection;
            Point[] points = calculatePoints(s, c, out arrowDirection);
            using (Pen pen = createPen(c.ConnectionType))
            {
                g.DrawLine(pen, points[0], points[1]);
            }
        }

        private static Pen createPen(ConnectionType connectionType)
        {
            Pen pen = new Pen(Color.Black,2);
            pen.CustomEndCap = new AdjustableArrowCap(5, 5); 
            
            switch (connectionType)
            {
                case ConnectionType.Normal:
                    pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                    break;
                case ConnectionType.Erweitert:
                    pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                    break;
                case ConnectionType.Implementiert:
                    pen.DashStyle = DashStyle.Dot;
                    break;
            }
            return pen;
        }

        private static Point[] calculatePoints(SectionClass root, Connection target, out Direction arrowDirection)
        {
            Point p1 = new Point(0, 0);
            Point p2 = new Point(target.ConnectionTarget.Rectangle.X + (target.ConnectionTarget.Rectangle.Width / 2), target.ConnectionTarget.Rectangle.Y);
            arrowDirection = Direction.Up;
            Direction actualDirection = Direction.Up;

            Point save1 = new System.Drawing.Point(0, 0);
            Point save2 = new System.Drawing.Point(0, 0);

            double distance = 0;
            double checkVal;

            for (int i = 0; i <= 3; i++)
            {

                p1 = new System.Drawing.Point(root.Rectangle.X +(root.Rectangle.Width / 2), root.Rectangle.Y);
                checkVal = getDistanceBetweenPoints(p1, p2);

                if (i == 0)
                {
                    distance = checkVal;
                    save1 = p1;
                    save2 = p2;
                    arrowDirection = actualDirection;
                }
                else if (checkVal < distance)
                {
                    distance = checkVal;
                    save1 = p1;
                    save2 = p2;
                    arrowDirection = actualDirection;
                }

                p1 = new System.Drawing.Point(root.Rectangle.X + root.Rectangle.Width, root.Rectangle.Y + (root.Rectangle.Height / 2));
                checkVal = getDistanceBetweenPoints(p1, p2);

                if (checkVal < distance)
                {
                    distance = checkVal;
                    save1 = p1;
                    save2 = p2;
                    arrowDirection = actualDirection;
                }

                p1 = new System.Drawing.Point(root.Rectangle.X, root.Rectangle.Y + (root.Rectangle.Height / 2));

                checkVal = getDistanceBetweenPoints(p1, p2);
                if (checkVal < distance)
                {
                    distance = checkVal;
                    save1 = p1;
                    save2 = p2;
                    arrowDirection = actualDirection;
                }

                p1 = new System.Drawing.Point(root.Rectangle.X + (root.Rectangle.Width / 2), root.Rectangle.Y + root.Rectangle.Height);
                checkVal = getDistanceBetweenPoints(p1, p2);
                if (checkVal < distance)
                {
                    distance = checkVal;
                    save1 = p1;
                    save2 = p2;
                    arrowDirection = actualDirection;
                }

                if (i == 0)
                {
                    p2 = new System.Drawing.Point(target.ConnectionTarget.Rectangle.X + target.ConnectionTarget.Rectangle.Width, target.ConnectionTarget.Rectangle.Y + (target.ConnectionTarget.Rectangle.Height / 2));
                    actualDirection = Direction.Right;
                }
                else if (i == 1)
                {
                    p2 = new System.Drawing.Point(target.ConnectionTarget.Rectangle.X, target.ConnectionTarget.Rectangle.Y + (target.ConnectionTarget.Rectangle.Height / 2));
                    actualDirection = Direction.Left;
                }
                else if (i == 2)
                {
                    p2 = new System.Drawing.Point(target.ConnectionTarget.Rectangle.X + (target.ConnectionTarget.Rectangle.Width / 2), target.ConnectionTarget.Rectangle.Y + target.ConnectionTarget.Rectangle.Height);
                    actualDirection = Direction.Down;
                }
            }
            return new Point[2] { save1, save2};
        }

        private static double getDistanceBetweenPoints(Point p, Point q)
        {
            double a = p.X - q.X;
            double b = p.Y - q.Y;
            double distance = Math.Sqrt(a * a + b * b);
            return distance;
        }
    }
}
