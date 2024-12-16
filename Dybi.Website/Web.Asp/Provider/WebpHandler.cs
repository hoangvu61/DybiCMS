using Imazen.WebP;
using System.Drawing;
using System.IO;
using System.Web;
using System;
using log4net;
using Library;
using System.Drawing.Imaging;
using System.Linq;

namespace Web.Asp.Provider
{
    public class WebpHandler : IHttpHandler
    {
        protected static readonly ILog log = LogManager.GetLogger(typeof(WebpHandler));
        public void ProcessRequest(HttpContext context)
        {
            try
            {
                string webpFileName = context.Server.MapPath(context.Request.FilePath);
                if (!File.Exists(webpFileName))
                {
                    var fileName = webpFileName.Replace(".webp", "");
                    if (File.Exists(fileName))
                    {
                        using (Bitmap bitmap = new Bitmap(fileName))
                        {
                            using (var saveImageStream = File.Open(webpFileName, FileMode.Create))
                            {
                                var encoder = new SimpleEncoder();
                                encoder.Encode(bitmap, saveImageStream, 20);
                            }
                        }
                    }
                }

                if (File.Exists(webpFileName))
                {
                    if (new FileInfo(webpFileName).Length == 0)
                    {
                        var fileName = webpFileName.Replace(".webp", "");
                        using (Image image = Image.FromFile(fileName))
                        {
                            using (Bitmap bitmap = new Bitmap(image))
                            {
                                var codec = GetEncoderInfo("image/webp");
                                var myEncoderParameters = new EncoderParameters(1);
                                var myEncoderParameter = new EncoderParameter(Encoder.Quality, 25L);
                                myEncoderParameters.Param[0] = myEncoderParameter;
                                bitmap.Save(webpFileName, codec, myEncoderParameters);
                            }
                        }
                    }

                    context.Response.ContentType = "image/webp";
                    context.Response.Clear();
                    context.Response.BufferOutput = true;

                    Byte[] b = System.IO.File.ReadAllBytes(webpFileName);
                    context.Response.BinaryWrite(b);
                }
            }
            catch(Exception ex)
            {
                log.Error(ex.TraceInformation());
            }
        }

        private ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            for (int i = 0; i < codecs.Length; i++)
                if (codecs[i].MimeType == mimeType)
                    return codecs[i];
            return ImageCodecInfo.GetImageDecoders().First(codec => codec.FormatID == ImageFormat.Jpeg.Guid);
        }

        public bool IsReusable
        {
            get
            { return true; }
        }
    }
}
