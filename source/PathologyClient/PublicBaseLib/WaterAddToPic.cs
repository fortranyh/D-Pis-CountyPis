using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace PublicBaseLib
{
    public enum WaterPositionMode
    {
        LeftTop,
        LeftBottom,
        RightTop,
        RightBottom,
        Center
    }
    public class WaterAddToPic
    {
        public static Image GetImage(string path)
        {
            FileStream fs = new System.IO.FileStream(path, FileMode.Open, FileAccess.Read);
            Image result = System.Drawing.Image.FromStream(fs);
            fs.Close();
            return result;
        }
        public static void AddWaterText(string oldpath, string savepath, string watertext, WaterPositionMode position, string color, int alpha)
        {
            Image image = GetImage(oldpath);
            Bitmap bitmap = new Bitmap(image.Width, image.Height);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.White);
            graphics.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);
            Font font = new Font("arial", 12);
            SizeF ziSizeF = new SizeF();
            ziSizeF = graphics.MeasureString(watertext, font);
            float x = 0f;
            float y = 0f;
            switch (position)
            {
                case WaterPositionMode.LeftTop:
                    x = ziSizeF.Width / 2f;
                    y = 8f;
                    break;
                case WaterPositionMode.LeftBottom:
                    x = ziSizeF.Width / 2f;
                    y = image.Height - ziSizeF.Height;
                    break;
                case WaterPositionMode.RightTop:
                    x = image.Width * 1f - ziSizeF.Width / 2f;
                    y = 8f;
                    break;
                case WaterPositionMode.RightBottom:
                    x = image.Width - ziSizeF.Width;
                    y = image.Height - ziSizeF.Height;
                    break;
                case WaterPositionMode.Center:
                    x = image.Width / 2;
                    y = image.Height / 2 - ziSizeF.Height / 2;
                    break;
            }
            try
            {
                StringFormat stringFormat = new StringFormat { Alignment = StringAlignment.Center };
                SolidBrush solidBrush = new SolidBrush(Color.FromArgb(alpha, 0, 0, 0));
                graphics.DrawString(watertext, font, solidBrush, x + 1f, y + 1f, stringFormat);
                SolidBrush brush = new SolidBrush(Color.FromArgb(alpha, ColorTranslator.FromHtml(color)));
                graphics.DrawString(watertext, font, brush, x, y, stringFormat);
                solidBrush.Dispose();
                brush.Dispose();
                bitmap.Save(savepath, ImageFormat.Jpeg);
            }
            catch
            {


            }
            finally
            {
                bitmap.Dispose();
                image.Dispose();
            }

        }
    }
}
