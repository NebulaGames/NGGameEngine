using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace NebulaGames.RPGWorld.Graphics
{
    public static class ImageMethods
    {

        public static void SaveImageAsJpg(Image Img, string FileName, long Quality)
        {
            //System.Drawing.Imaging.Encoder _JpgEncoder = System.Drawing.Imaging.Encoder.Quality;
            //System.Drawing.Imaging.ImageCodecInfo jgpEncoder = GetEncoder(System.Drawing.Imaging.ImageFormat.Jpeg);

            //System.Drawing.Imaging.EncoderParameters _Params = new System.Drawing.Imaging.EncoderParameters(1);
            //System.Drawing.Imaging.EncoderParameter _Param = new System.Drawing.Imaging.EncoderParameter(_JpgEncoder, 95L);
            //_Params.Param[0] = _Param;
            Img.Save(FileName);//, jgpEncoder, _Params);

        }

        private static System.Drawing.Imaging.ImageCodecInfo GetEncoder(System.Drawing.Imaging.ImageFormat format)
        {

            System.Drawing.Imaging.ImageCodecInfo[] codecs = System.Drawing.Imaging.ImageCodecInfo.GetImageDecoders();

            foreach (System.Drawing.Imaging.ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }
    }
}
