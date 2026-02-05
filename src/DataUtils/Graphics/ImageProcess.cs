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

    public class ImageProcess
    { 
        public static Bitmap Gaussian5_5(Bitmap source )
        {
            Bitmap bmp = new Bitmap(source.Width, source.Height);
            Color[,] colors = new Color[5, 5];
            short[,] weight = new short[5, 5] {
                { 1, 1, 2, 1, 1 },
                { 1, 3, 4, 3, 1 },
                { 2, 4, 8, 4, 2 },
                { 1, 3, 4, 3, 1 },
                { 1, 1, 2, 1, 1 }
            };
            for (int j = 0; j < bmp.Height; j++)
            {
                for (int i = 0; i < bmp.Width; i++)
                {
                    Color color = source.GetPixel(i, j);

                    colors[2, 1] = color;
                }
            }
            return bmp;
        }
        public static Bitmap Gaussian5_5_Row1(Bitmap source)
        {
            Bitmap bmp = new Bitmap(source.Width, source.Height);
      

            return bmp;
        }


        public static Bitmap CopyBitmap(Bitmap source, Point pt, Size size)
        {
            Rectangle rect = new Rectangle(pt, size);
            return CopyBitmap(source, rect);
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

        /// <summary>
        /// 对图像进行平滑处理（利用高斯平滑Gaussian Blur）
        /// </summary>
        /// <param name="bitmap">要处理的位图</param>
        /// <returns>返回平滑处理后的位图</returns>
        public Bitmap Smooth(Bitmap bitmap)
        {
            int[,,] InputPicture = new int[3, bitmap.Width, bitmap.Height];//以GRB以及位图的长宽建立整数输入的位图的数组

            Color color = new Color();//储存某一像素的颜色
            //循环使得InputPicture数组得到位图的RGB
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    color = bitmap.GetPixel(i, j);
                    InputPicture[0, i, j] = color.R;
                    InputPicture[1, i, j] = color.G;
                    InputPicture[2, i, j] = color.B;
                }
            }

            int[,,] OutputPicture = new int[3, bitmap.Width, bitmap.Height];//以GRB以及位图的长宽建立整数输出的位图的数组
            Bitmap smooth = new Bitmap(bitmap.Width, bitmap.Height);//创建新位图
            //循环计算使得OutputPicture数组得到计算后位图的RGB
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    int R = 0;
                    int G = 0;
                    int B = 0;

                    //每一个像素计算使用高斯模糊卷积核进行计算
                    for (int r = 0; r < 5; r++)//循环卷积核的每一行
                    {
                        for (int f = 0; f < 5; f++)//循环卷积核的每一列
                        {
                            //控制与卷积核相乘的元素
                            int row = i - 2 + r;
                            int index = j - 2 + f;

                            //当超出位图的大小范围时，选择最边缘的像素值作为该点的像素值
                            row = row < 0 ? 0 : row;
                            index = index < 0 ? 0 : index;
                            row = row >= bitmap.Width ? bitmap.Width - 1 : row;
                            index = index >= bitmap.Height ? bitmap.Height - 1 : index;

                            //输出得到像素的RGB值
                            R += (int)(GaussianBlur[r, f] * InputPicture[0, row, index]);
                            G += (int)(GaussianBlur[r, f] * InputPicture[1, row, index]);
                            B += (int)(GaussianBlur[r, f] * InputPicture[2, row, index]);
                        }
                    }
                    color = Color.FromArgb(R, G, B);//颜色结构储存该点RGB
                    smooth.SetPixel(i, j, color);//位图存储该点像素值
                }
            }
            return smooth;
        }
        private double[,] GaussianBlur;//声明私有的高斯模糊卷积核函数

        /// <summary>
        /// 构造卷积（Convolution）类函数
        /// </summary>
        public ImageProcess()
        {
            //初始化高斯模糊卷积核
            int k = 273;
            //GaussianBlur = new double[5, 5]{{(double)1/k,(double)4/k,(double)7/k,(double)4/k,(double)1/k},
            //                                {(double)4/k,(double)16/k,(double)26/k,(double)16/k,(double)4/k},
            //                                {(double)7/k,(double)26/k,(double)41/k,(double)26/k,(double)7/k},
            //                                {(double)4/k,(double)16/k,(double)26/k,(double)16/k,(double)4/k},
            //                                {(double)1/k,(double)4/k,(double)7/k,(double)4/k,(double)1/k}};

            GaussianBlur = new double[5, 5]{{1,1,2,1,1},
                                            {1,3,4,3,1},
                                            {2,4,8,4,2},
                                            {1,3,4,3,1},
                                            {1,1,2,1,1}};
        } 
    }
}
