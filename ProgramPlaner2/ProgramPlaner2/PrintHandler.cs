using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.Drawing;

namespace ProgramPlaner2
{
    static class PrintHandler
    {
        private static Bitmap memoryImage;
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern long BitBlt(IntPtr hdcDest, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, int dwRop);

        public static void Print(Form board)
        {
            PrintPreviewDialog pPreview = new PrintPreviewDialog();
            PrintDocument pDoc = new PrintDocument();

            pPreview.Document = pDoc;
            captureScreen(board);

            pDoc.DefaultPageSettings.PaperSize = new PaperSize("Paper", board.ClientRectangle.Width, board.ClientRectangle.Height);

            pDoc.PrintPage += new PrintPageEventHandler(pDoc_PrintPage);

            pPreview.Show(board);
        }

        static void  pDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(memoryImage, 0, 0);
        }

        private static void captureScreen(Form board)
        {
            Graphics mygraphics = board.CreateGraphics();
            Size s = board.Size;
            memoryImage = new Bitmap(s.Width, s.Height, mygraphics);
            Graphics memoryGraphics = Graphics.FromImage(memoryImage);
            IntPtr dc1 = mygraphics.GetHdc();
            IntPtr dc2 = memoryGraphics.GetHdc();
            BitBlt(dc2, 0, 0, board.ClientRectangle.Width, board.ClientRectangle.Height, dc1, 0, 24, 13369376);
            mygraphics.ReleaseHdc(dc1);
            memoryGraphics.ReleaseHdc(dc2);
        }
    }
}
