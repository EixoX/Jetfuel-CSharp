using System;
using System.Collections.Generic;

using System.Text;

namespace EixoX.Text
{
    public class ImageAdapter
        : AbstractTextAdapter<System.Drawing.Image>
    {
        private System.Drawing.Imaging.ImageFormat _imageFormat = System.Drawing.Imaging.ImageFormat.Jpeg;


        protected override System.Drawing.Image Parse(string text, IFormatProvider formatProvider)
        {
            byte[] array = Convert.FromBase64String(text);
            System.IO.MemoryStream ms = new System.IO.MemoryStream(array);
            return System.Drawing.Image.FromStream(ms);
        }

        public System.Drawing.Imaging.ImageFormat ImageFormat
        {
            get { return this._imageFormat; }
            set { this._imageFormat = value; }
        }

        protected override string Format(System.Drawing.Image value, IFormatProvider formatProvider)
        {
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream((int)(
                value.PhysicalDimension.Width * value.PhysicalDimension.Height)))
            {
                try
                {
                    value.Save(ms, _imageFormat);
                    byte[] bytes = ms.ToArray();
                    return Convert.ToBase64String(bytes);
                }
                finally
                {
                    ms.Close();
                }
            }
        }
    }
}
