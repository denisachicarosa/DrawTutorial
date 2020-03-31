using GrapeCity.Documents.Imaging;
using GrapeCity.Documents.Text;
using System.Drawing;

namespace FromTextToImage
{
    class Program
    {
        public static string FontName { get; set; } = "Autography";
        public static int FontSize { get; set; } = 100;

        //A4 size
        public static int ImageWidth { get; set; } = 1240;
        public static int ImageHeigth { get; set; } = 1754;

        static void Main(string[] args)
        {
            string text = "O PROPOZITIE FRUMOASA";
            
            using (var img = GrapeCity.Documents.Drawing.Image.FromFile("transparent.png"))
            using (var bmp = new GcBitmap(ImageWidth, ImageHeigth, false))
            using (var g = bmp.CreateGraphics(Color.White))
            {
                var tf = new TextFormat()
                {
                    Font = GrapeCity.Documents.Text.Font.FromFile($"Fonts\\{FontName}.ttf"),
                    FontSize = FontSize
                };

                g.DrawString(text, tf, new RectangleF(0, 0, ImageWidth, ImageHeigth),
                             TextAlignment.Justified, ParagraphAlignment.Near, wordWrap: true);

                bmp.SaveAsPng(@"result.png");
            }

            
            Bitmap bitmap = new Bitmap("result.png");
            bitmap.MakeTransparent(Color.White);
            bitmap.Save("result.png");
        }
    }
}
