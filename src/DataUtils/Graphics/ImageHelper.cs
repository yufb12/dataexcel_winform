using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace Feng.Drawing
{

    public class ImageHelper
    {

        public static bool IsImage(string fileextis)
        {
            string imageextents = ".bmp.jpg.jpeg.png.gif";
            return imageextents.Contains(fileextis.ToLower());
        }

        public static Bitmap GetScreenImage()
        {
            System.Drawing.Size screen = new System.Drawing.Size(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width,
                System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height);
            System.Drawing.Bitmap memoryImage = new System.Drawing.Bitmap(screen.Width, screen.Height);
            System.Drawing.Graphics memoryGraphics = System.Drawing.Graphics.FromImage(memoryImage);
            memoryGraphics.CopyFromScreen(0, 0, 0, 0, screen);
            return memoryImage;
        }
        public static Bitmap GetScreenImage(Rectangle rect)
        {
            System.Drawing.Bitmap memoryImage = new System.Drawing.Bitmap(rect.Width, rect.Height);
            System.Drawing.Graphics memoryGraphics = System.Drawing.Graphics.FromImage(memoryImage);
            memoryGraphics.CopyFromScreen(rect.Location, Point.Empty, rect.Size);
            return memoryImage;
        }
        public static Bitmap GetScreenImage(Point pt,Size size)
        { 
            System.Drawing.Bitmap memoryImage = new System.Drawing.Bitmap(size.Width, size.Height);
            System.Drawing.Graphics memoryGraphics = System.Drawing.Graphics.FromImage(memoryImage);
            memoryGraphics.CopyFromScreen(pt, Point.Empty, size);
            return memoryImage;
        }

        public static Rectangle ImageRectangleFromSizeMode(ImageLayout mode, Image image, RectangleF rect)
        {
            return ImageRectangleFromSizeMode(mode, image, Rectangle.Round(rect));
        }
        public static Rectangle ImageRectangleFromSizeMode(ImageLayout mode, Image image, Rectangle rect)
        {
            Rectangle rectangle = rect;
            if (image != null)
            {
                switch (mode)
                {
                    case ImageLayout.None: 
                        rectangle.Size = image.Size;
                        return rectangle; 
                    case ImageLayout.Stretch:
                        return rectangle; 
                    case ImageLayout.Center:
                        rectangle.X += (rectangle.Width - image.Width) / 2;
                        rectangle.Y += (rectangle.Height - image.Height) / 2;
                        rectangle.Size = image.Size;
                        return rectangle; 
                    case ImageLayout.Zoom:
                        {
                            Size size = image.Size;
                            float num = Math.Min((float)((rectangle.Width) / ((float)size.Width)), (float)((rectangle.Height) / ((float)size.Height)));
                            rectangle.Width = (int)(size.Width * num);
                            rectangle.Height = (int)(size.Height * num);
                            rectangle.X += (rect.Width - rectangle.Width) / 2;
                            rectangle.Y += (rect.Height - rectangle.Height) / 2;
                            return rectangle;
                        }
                }
            }
            return rectangle;
        }

        public static string GetFileExtenIcon(ImageList imagelist, string file)
        {
            try
            { 
                string exten = System.IO.Path.GetExtension(file);
                if (imagelist.Images.ContainsKey(exten))
                {
                    return exten;
                }
                Bitmap bmp = null;
                try
                {
                    bmp = Feng.Utils.UnsafeNativeMethods.GetBitMapFromFileExten(exten);
                }
                catch (Exception)
                { 
                }
                imagelist.Images.Add(exten, bmp);
                return exten;
            }
            catch (Exception)
            { 
            }
            return string.Empty;
        }

        public static Icon GetIconFromFile(string file)
        {
            return Feng.Utils.UnsafeNativeMethods.GetLargeFileIcon(file);
        }

        public static Bitmap GetImageFromIcon(Icon icon)
        {
            return icon.ToBitmap();
        }

        public static void SaveBitmapPng(Bitmap bitmap, string file)
        {
            bitmap.Save(file, System.Drawing.Imaging.ImageFormat.Png);
        }
        public static byte[] GetData(Bitmap img)
        {
            byte[] data = null;
            if (img != null)
            {
                using (Bitmap bitmap = new Bitmap(img))
                {
                    using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                    {
                        bitmap.Save(ms, ImageFormat.Png);
                        data = ms.ToArray();
                    }
                }
            }
            return data;
        }
        //public static byte[] GetData(Image img)
        //{
        //    byte[] data = null;
        //    if (img != null)
        //    { 
        //        using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
        //        {
        //            img.Save(ms,img.RawFormat);
        //            ms.Flush();
        //            data = ms.ToArray();
        //        }
        //    }
        //    else
        //    {
        //        data = new byte[] { };
        //    }
        //    return data;
        //}
        public static Bitmap ReadFileBitmap(string file)
        {
            byte[] data = System.IO.File.ReadAllBytes(file);
            Bitmap bmp = Feng.Drawing.ImageHelper.GetBitmap(data);
            return bmp;
        }
        public static Bitmap GetBitmap(byte[] data)
        {
            if (data == null)
                return null;
            if (data.Length < 1)
                return null;
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream(data))
            {
                return Bitmap.FromStream(ms) as Bitmap;
            }
        }

        public static Image GetImageByCache(byte[] data)
        {
            if (data == null)
                return null;
            if (data.Length < 1)
                return null;
            System.IO.MemoryStream memoryStream = new System.IO.MemoryStream();
            memoryStream.Write(data, 0, data.Length);
            return Image.FromStream(memoryStream, true, true); 
        }

        //public static byte[] GetBitmapData(Bitmap bmp1)
        //{ 
        //    System.Drawing.Imaging.BitmapData bmpData = bmp1.LockBits(new Rectangle() { X = 0, Y = 0, Width = bmp1.Width, Height = bmp1.Height },
        //        System.Drawing.Imaging.ImageLockMode.ReadWrite,
        //        bmp1.PixelFormat);

        //    IntPtr ptr = bmpData.Scan0;

        //    int bytes = Math.Abs(bmpData.Stride) * bmp1.Height;
        //    byte[] rgbValues = new byte[bytes];

        //    System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

        //    for (int counter = 0; counter < rgbValues.Length - 2; counter += 3)
        //    {
        //        byte value = Feng.Utils.ConvertHelper.AVG(rgbValues[counter], rgbValues[counter + 1], rgbValues[counter + 2]);
        //        rgbValues[counter] = value;
        //        rgbValues[counter + 1] = value;
        //        rgbValues[counter + 2] = value;
        //    }
        //    System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);

        //    bmp1.UnlockBits(bmpData);
        //    return rgbValues;
        //}
        public static Point GetImagePoint(Bitmap source, Bitmap destination)
        {
            int row = 0;
            int column = 0;
            bool result = CompareImage(source, destination, ref row, ref column);
            if (result)
            {
                return new Point(column, row);
            }
            return Point.Empty;
        }
        public static bool CompareImage(Bitmap source, Bitmap destination)
        {
            int row = 0;
            int column = 0;
            return CompareImage(source, destination, ref row, ref column);
        }
        public static bool CompareImage(Bitmap source, Bitmap destination, ref int row, ref int column)
        {

            if (source.PixelFormat != destination.PixelFormat)
            {
                return false;
            }

            int bitdepth_per_pixel = Bitmap.GetPixelFormatSize(source.PixelFormat) / 8;

            if (bitdepth_per_pixel != 1 && bitdepth_per_pixel != 3 && bitdepth_per_pixel != 4)
            {
                return false;
            }

            BitmapData source_bitmapdata = null;
            BitmapData destination_bitmapdata = null;
            bool resutl = false;
            source_bitmapdata = source.LockBits(new Rectangle(0, 0, source.Width, source.Height), ImageLockMode.ReadWrite,
                                            source.PixelFormat);
            destination_bitmapdata = destination.LockBits(new Rectangle(0, 0, destination.Width, destination.Height), ImageLockMode.ReadWrite,
                                            destination.PixelFormat);

            int source_bitmapdata_bitdepth_width = source_bitmapdata.Width * bitdepth_per_pixel;
            int source_bitmapdata_height = source_bitmapdata.Height;
            int source_bitmapdata_bitdepth_stride = source_bitmapdata.Stride;

            int destination_bitmapdata_bitdepth_width = destination_bitmapdata.Width * bitdepth_per_pixel;
            int destination_bitmapdata_height = destination_bitmapdata.Height;
            int destination_bitmapdata_bitdepth_stride = destination_bitmapdata.Stride;
 
            unsafe
            {
                byte* source_ptr = (byte*)source_bitmapdata.Scan0;
                byte* destination_ptr = (byte*)destination_bitmapdata.Scan0;

                resutl = Contains(source_ptr, source_bitmapdata_bitdepth_stride, source_bitmapdata_height, destination_ptr,
                    destination_bitmapdata_bitdepth_stride, destination_bitmapdata_height, ref row, ref column);
            }
            column = column / bitdepth_per_pixel;
            source.UnlockBits(source_bitmapdata);
            destination.UnlockBits(destination_bitmapdata);
            return resutl;
        }

        public static unsafe bool Contains(byte[] psource, int p1width, int p1height, byte[] des, int p2width, int p2height)
        {
            int row = 0; int column = 0;
            bool result = Contains(psource, p1width, p1height, des, p2width, p2height, ref   row, ref   column);
            return result;
        }

        public static unsafe bool Contains(byte[] psource, int p1width, int p1height, byte[] des, int p2width, int p2height, ref int row, ref int column)
        {

            fixed (byte* p1 = psource)
            {
                fixed (byte* p2 = des)
                {
                    return Contains(p1, p1width, p1height, p2, p2width, p2height, ref row, ref column);
                }
            }

        }
        public static unsafe bool Contains(byte* psource, int p1width, int p1height, byte* des, int p2width, int p2height, ref int row, ref int column)
        {
            if (p2height > p1height)
                return false; 
            byte* p1 = psource;
            byte* p2 = des;
            bool result = true;
            for (int i = 0; i < p1height; i++)
            {
                p1 = psource + p1width * i;
                p2 = des;
                result = Contains(p1, p1width, p2, p2width, ref column);
                if (result)
                {
                    row = i;
                    int column2 = 0;
                    result = Contains(psource, p1width, p1height, i, des, p2width, p2height, ref column2);
                    if (result)
                    {
                        break;
                    }
                }
            }
            return result;
        }
        public static unsafe bool Contains(byte* psource, int p1width, int p1height, int p1start, byte* des, int p2width, int p2height, ref int column)
        { 
            if (p2height > p1height)
                return false;
            if (p1start + p2height > p1height)
            {
                return false;
            }
            byte* p1 = psource;
            byte* p2 = des;
            bool result = true ;
            for (int i = 0; i < p2height; i++)
            {
                p1 = psource + p1width * (i + p1start);
                p2 = des + p2width * i; 
                result = Contains(p1, p1width, p2, p2width, ref column);
                if (!result)
                {
                    return false;
                }
            }
            return result;
        }

        public static unsafe bool Contains(byte[] psource, byte[] des, ref int column)
        {

            fixed (byte* p1 = psource)
            {
                fixed (byte* p2 = des)
                {
                    return Contains(p1, psource.Length, p2, des.Length, ref column);
                }
            }

        }
        public static unsafe bool Contains(byte* psource, int p1width, byte* des, int p2width, ref int column)
        {
            bool result = false;
            if (p2width > p1width)
                return false;
            byte* p1 = psource;
            byte* p2 = des;
            for (int i = 0; i < p1width; i++)
            {
                byte a = *(psource + i);
                byte b = *des;
                if (b == a)
                {
                    result = Contains(psource, p1width, i, des, p2width);
                    if (result)
                    {
                        column = i;
                        break;
                    }
                }
            }
            return result;

        }
        public static unsafe bool Contains(byte* psource, int p1width, int p1start, byte* des, int p2width)
        {
            if (p1width < p1start + p2width)
            {
                return false;
            }
            bool result = true;
            for (int i = 0; i < p2width; i++)
            {
                byte b = *(des + i);
                byte a = *(psource + p1start + i);
                if (b != a)
                {
                    result = false;
                    break;
                }
            }
            return result;

        }

        public static byte[] GetBitmapData(Bitmap source)
        { 
            int bitdepth_per_pixel = Bitmap.GetPixelFormatSize(source.PixelFormat) / 8;

            if (bitdepth_per_pixel != 1 && bitdepth_per_pixel != 3 && bitdepth_per_pixel != 4)
            {
                return null;
            }

            BitmapData source_bitmapdata = null; 
             
            source_bitmapdata = source.LockBits(new Rectangle(0, 0, source.Width, source.Height), ImageLockMode.ReadWrite,
                                            source.PixelFormat);
 
            int source_bitmapdata_bitdepth_width = source_bitmapdata.Width * bitdepth_per_pixel;
            int source_bitmapdata_height = source_bitmapdata.Height;
            int source_bitmapdata_bitdepth_stride = source_bitmapdata.Stride;
            byte[] data = new byte[source_bitmapdata_height * source_bitmapdata_bitdepth_stride];
            unsafe
            {
                fixed (byte* p1 = data)
                {
                    byte* destination_ptr = p1;
                    byte* source_ptr = (byte*)source_bitmapdata.Scan0;
                    int offset = source_bitmapdata_bitdepth_stride - source_bitmapdata_bitdepth_width;
                    StringBuilder sb = new StringBuilder();

                    sb.Append(string.Format("{0:000},", 0));
                    for (int j = 1; j <= source_bitmapdata_bitdepth_width; j++)
                    {
                        sb.Append(string.Format("{0:000},", j));
                    }
                    for (int i = 0; i < source_bitmapdata_height; i++)
                    {
                        sb.AppendLine();
                        sb.Append(string.Format("{0:000}:", i));
                        for (int j = 0; j < source_bitmapdata_bitdepth_width; j++, source_ptr++, destination_ptr++)
                        {
                            *destination_ptr = *source_ptr;

                            sb.Append(string.Format("{0:000},", *destination_ptr));
                        }
                        source_ptr += offset;
                        destination_ptr += offset;
                    }
                    System.IO.File.WriteAllText("aaaa.txt", sb.ToString());
                }
            }

            source.UnlockBits(source_bitmapdata);

            return data;
    
        }

        public static bool CopyBitmap(Bitmap source, Bitmap destination)
        {
            if ((source.Width != destination.Width) || (source.Height != destination.Height) || (source.PixelFormat != destination.PixelFormat))
            {
                return false;
            }

            int bitdepth_per_pixel = Bitmap.GetPixelFormatSize(source.PixelFormat) / 8;

            if (bitdepth_per_pixel != 1 && bitdepth_per_pixel != 3 && bitdepth_per_pixel != 4)
            {
                return false;
            }

            BitmapData source_bitmapdata = null;
            BitmapData destination_bitmapdata = null;

            try
            {
                source_bitmapdata = source.LockBits(new Rectangle(0, 0, source.Width, source.Height), ImageLockMode.ReadWrite,
                                                source.PixelFormat);
                destination_bitmapdata = destination.LockBits(new Rectangle(0, 0, destination.Width, destination.Height), ImageLockMode.ReadWrite,
                                                destination.PixelFormat);

                int source_bitmapdata_bitdepth_width = source_bitmapdata.Width * bitdepth_per_pixel;
                int source_bitmapdata_height = source_bitmapdata.Height;
                int source_bitmapdata_bitdepth_stride = source_bitmapdata.Stride;

                unsafe
                {
                    byte* source_ptr = (byte*)source_bitmapdata.Scan0;
                    byte* destination_ptr = (byte*)destination_bitmapdata.Scan0;

                    int offset = source_bitmapdata_bitdepth_stride - source_bitmapdata_bitdepth_width;

                    for (int i = 0; i < source_bitmapdata_height; i++)
                    {
                        for (int j = 0; j < source_bitmapdata_bitdepth_width; j++, source_ptr++, destination_ptr++)
                        {
                            *destination_ptr = *source_ptr;
                        }

                        source_ptr += offset;
                        destination_ptr += offset;
                    }
                }

                source.UnlockBits(source_bitmapdata);
                destination.UnlockBits(destination_bitmapdata);

                return true;
            }
            catch { return false; }
        }
        public static Bitmap CopyBitmap(Bitmap source, int x, int y, int width, int height)
        {
            Rectangle rect = new Rectangle(x, y, width, height);
            return CopyBitmap(source, rect);
        }
        public static Bitmap CopyBitmap(Bitmap source, Point pt, Size size)
        {
            Rectangle rect = new Rectangle(pt, size);
            return CopyBitmap(source, rect);
        }
        public static Bitmap CloneEmptyBitmap(Bitmap source)
        {
            Bitmap bmp = new Bitmap(source.Width, source.Height, source.PixelFormat);
            return bmp;
        }
        public static void SetBitmap(Bitmap source, byte[] data)
        {
            int bitdepth_per_pixel = Bitmap.GetPixelFormatSize(source.PixelFormat) / 8;
 
            BitmapData source_bitmapdata = null;
            source_bitmapdata = source.LockBits(new Rectangle(0, 0, source.Width, source.Height), ImageLockMode.ReadWrite,
                                            source.PixelFormat);

            int source_bitmapdata_bitdepth_width = source_bitmapdata.Width * bitdepth_per_pixel;
            int source_bitmapdata_height = source_bitmapdata.Height;
            int source_bitmapdata_bitdepth_stride = source_bitmapdata.Stride;

            unsafe
            {
                fixed (byte* p1 = data)
                {
                    byte* destination_ptr = p1;
                    byte* source_ptr = (byte*)source_bitmapdata.Scan0;
                    int offset = source_bitmapdata_bitdepth_stride - source_bitmapdata_bitdepth_width;

                    for (int i = 0; i < source_bitmapdata_height; i++)
                    {
                        for (int j = 0; j < source_bitmapdata_bitdepth_width; j++, source_ptr++, destination_ptr++)
                        {
                            *source_ptr = *destination_ptr;
                        }
                        source_ptr += offset;
                        destination_ptr += offset;
                    }
                }
            }

            source.UnlockBits(source_bitmapdata);

        }
        public static Bitmap CopyBitmap(Bitmap source, Rectangle rect)
        {
            if (rect.Width < 1 || rect.Height < 1)
                return null;
            Bitmap destination = new Bitmap(rect.Width, rect.Height, source.PixelFormat);
 
            int bitdepth_per_pixel = Bitmap.GetPixelFormatSize(source.PixelFormat) / 8;

            if (bitdepth_per_pixel != 1 && bitdepth_per_pixel != 3 && bitdepth_per_pixel != 4)
            {
                return null;
            }

            BitmapData source_bitmapdata1 = null;
            BitmapData destination_bitmapdata = null;

            try
            {
                source_bitmapdata1 = source.LockBits(new Rectangle(0, 0, source.Width, source.Height), ImageLockMode.ReadWrite,
                                                source.PixelFormat);
                destination_bitmapdata = destination.LockBits(new Rectangle(0, 0, destination.Width, destination.Height), ImageLockMode.ReadWrite,
                                                destination.PixelFormat);

                int destination_bitmapdata_bitdepth_width = destination_bitmapdata.Width * bitdepth_per_pixel;
                int destination_bitmapdata_height = destination_bitmapdata.Height;
                int destination_bitmapdata_bitdepth_stride = destination_bitmapdata.Stride;

                unsafe
                {
                    byte* source_ptr = (byte*)source_bitmapdata1.Scan0;
                    byte* destination_ptr = (byte*)destination_bitmapdata.Scan0;

                    int offset = destination_bitmapdata_bitdepth_stride - destination_bitmapdata_bitdepth_width;
                    source_ptr = source_ptr + rect.Y * source_bitmapdata1.Stride;
                    for (int i = 0; i < destination_bitmapdata_height; i++)
                    {
                        byte* source_ptr_row = source_ptr + (rect.X * bitdepth_per_pixel);
                        for (int j = 0; j < destination_bitmapdata_bitdepth_width; j++, source_ptr_row++, destination_ptr++)
                        {
                            *destination_ptr = *source_ptr_row;
                        }

                        destination_ptr += offset;
                        source_ptr = source_ptr + source_bitmapdata1.Stride;
                    }
                }

                source.UnlockBits(source_bitmapdata1);
                destination.UnlockBits(destination_bitmapdata);

                return destination;
            }
            catch { return null; }
        }
        public static bool CompareImage(byte[] data1, byte[] data2, ref int row)
        {
            bool result = false;
            if (data2.Length > data1.Length)
            {
                return false;
            }
            for (int i = 0; i < data1.Length; i++)
            {

                if (data1[i] == data2[0])
                {
                    result = true;
                    if (i + data2.Length > data1.Length)
                    {
                        return false;
                    }
                    for (int j = 0; j < data2.Length; j++)
                    {
                        if (data1[i + j] != data2[j])
                        {
                            result = false;
                            break;
                        }
                    }
                    if (result)
                    {
                        row = i;
                        return result;
                    }
                }
                 
            }
            return result;
        }
    }
     

}
