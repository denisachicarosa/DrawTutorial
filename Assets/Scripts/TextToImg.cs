using GrapeCity.Documents.Imaging;
using GrapeCity.Documents.Text;
using System.Drawing;
using UnityEngine;
using Color = System.Drawing.Color;
using TextAlignment = GrapeCity.Documents.Text.TextAlignment;

public class TextToImg
{
    public static int FontSize { get; set; } = 100;

    //A4 size
    public static int ImageWidth { get; set; } = 1240;
    public static int ImageHeigth { get; set; } = 1754;

    public static void _CreateImageFromText(string FontName, string text)
    {
        string path = "Fonts\\" + FontName;
        
        using (var img = GrapeCity.Documents.Drawing.Image.FromFile("transparent.png"))
        using (var bmp = new GcBitmap(ImageWidth, ImageHeigth, false))
        using (var g = bmp.CreateGraphics(Color.White))
        {
            var tf = new TextFormat()
            {
                
                Font = GrapeCity.Documents.Text.Font.FromFile(path),
                FontSize = FontSize
            };

            g.DrawString(text, tf, new RectangleF(0, 0, ImageWidth, ImageHeigth),
                         TextAlignment.Justified, ParagraphAlignment.Near, wordWrap: true);

            bmp.SaveAsPng(@"result.png");
        }
    }
}

