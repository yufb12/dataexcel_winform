using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;

namespace Feng.IO
{
    public class ZipTools
    {
        public static void Test()
        {
            try
            {
                // 要压缩的文件路径
                string sourceFilePath = "test.txt";
                string sourceFilePath1 = "test1.txt";
                // 压缩后的 ZIP 文件路径
                string zipFilePath = "test.zip";
                // 解压缩后的文件保存路径
                string extractPath = "extracted";

                // 创建一个文本文件用于测试
                System.IO.File.WriteAllText(sourceFilePath, "This is a test file.");
                System.IO.File.WriteAllText(sourceFilePath1, "This is a test file 1.");

                // 压缩文件
                ZipFiles(new string[] { sourceFilePath, sourceFilePath1 }, zipFilePath, Application.StartupPath);

                // 解压缩文件
                UnzipFiles(zipFilePath, extractPath);

                // 压缩后的 ZIP 文件路径
                string zipDirctoryPath = "mnist160";
                // 压缩文件
                ZipDirctory(zipDirctoryPath, zipFilePath);

                // 解压缩文件
                UnzipFiles(zipFilePath, extractPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public static void ZipDirctory(string directory, string zipFilePath)
        {
            List<string> list = new List<string>();
            GetFiles(list, directory);
            string baseDirectory = Path.GetDirectoryName(Path.GetFullPath(directory));
            ZipFiles(list.ToArray(), zipFilePath, baseDirectory);
        }

        public static void ZipFiles(string[] filePaths, string zipFilePath, string baseDirectory)
        {
            using (FileStream zipFileStream = new FileStream(zipFilePath, FileMode.Create))
            {
                List<long> localFileHeaderOffsets = new List<long>();

                foreach (string filePath in filePaths)
                {
                    localFileHeaderOffsets.Add(zipFileStream.Position);
                    string relativePath = GetRelativePath(baseDirectory, filePath);
                    byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
                    uint crc32 = CalculateCrc32(fileBytes);

                    // 写入本地文件头
                    WriteLocalFileHeader(zipFileStream, relativePath, fileBytes.Length, crc32, 0);

                    // 压缩文件内容
                    using (MemoryStream compressedStream = new MemoryStream())
                    {
                        using (DeflateStream deflateStream = new DeflateStream(compressedStream, CompressionMode.Compress))
                        {
                            deflateStream.Write(fileBytes, 0, fileBytes.Length);
                        }
                        byte[] compressedBytes = compressedStream.ToArray();
                        int compressedSize = compressedBytes.Length;

                        // 更新本地文件头中的压缩大小
                        UpdateLocalFileHeaderCompressedSize(zipFileStream, localFileHeaderOffsets[localFileHeaderOffsets.Count - 1], compressedSize);

                        // 写入压缩后的文件内容
                        zipFileStream.Write(compressedBytes, 0, compressedBytes.Length);
                    }
                }
                // 写入中央目录
                WriteCentralDirectory(zipFileStream, filePaths, localFileHeaderOffsets, baseDirectory);
            }
        }

        public static void UnzipFiles(string zipFilePath, string extractPath)
        {
            using (FileStream zipFileStream = new FileStream(zipFilePath, FileMode.Open))
            {
                while (zipFileStream.Position < zipFileStream.Length)
                {
                    // 读取本地文件头
                    if (ReadLocalFileHeader(zipFileStream, out string relativePath, out int compressedSize, out int uncompressedSize, out uint crc32))
                    {
                        string extractFilePath = Path.Combine(extractPath, relativePath);
                        Directory.CreateDirectory(Path.GetDirectoryName(extractFilePath));

                        using (FileStream extractFileStream = new FileStream(extractFilePath, FileMode.Create))
                        {
                            // 注意：这里使用一个临时的 MemoryStream 来读取压缩数据
                            using (MemoryStream tempStream = new MemoryStream())
                            {
                                byte[] buffer = new byte[4096];
                                int totalBytesRead = 0;
                                while (totalBytesRead < compressedSize)
                                {
                                    int bytesRead = zipFileStream.Read(buffer, 0, Math.Min(buffer.Length, compressedSize - totalBytesRead));
                                    if (bytesRead == 0)
                                    {
                                        break;
                                    }
                                    tempStream.Write(buffer, 0, bytesRead);
                                    totalBytesRead += bytesRead;
                                }
                                tempStream.Position = 0;

                                using (DeflateStream deflateStream = new DeflateStream(tempStream, CompressionMode.Decompress))
                                {
                                    byte[] decompressedBuffer = new byte[4096];
                                    int decompressedBytesRead;
                                    MemoryStream ms = new MemoryStream();
                                    while ((decompressedBytesRead = deflateStream.Read(decompressedBuffer, 0, decompressedBuffer.Length)) > 0)
                                    {
                                        extractFileStream.Write(decompressedBuffer, 0, decompressedBytesRead);
                                        ms.Write(decompressedBuffer, 0, decompressedBytesRead);
                                    }
                                    ms.Position = 0;
                                    byte[] extractedBytes = ms.ToArray();
                                    uint extractedCrc32 = CalculateCrc32(extractedBytes);
                                    if (extractedCrc32 != crc32)
                                    {
                                        throw new InvalidDataException($"CRC - 32 check failed for {relativePath}.");
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        private static void WriteLocalFileHeader(FileStream zipFileStream, string relativePath, int uncompressedSize, uint crc32, int compressedSize)
        {
            byte[] header = new byte[30 + relativePath.Length];
            Buffer.BlockCopy(BitConverter.GetBytes(0x04034b50), 0, header, 0, 4); // 本地文件头签名
            Buffer.BlockCopy(BitConverter.GetBytes((ushort)20), 0, header, 4, 2); // 版本需要
            Buffer.BlockCopy(BitConverter.GetBytes((ushort)0), 0, header, 6, 2); // 通用位标志
            Buffer.BlockCopy(BitConverter.GetBytes((ushort)8), 0, header, 8, 2); // 压缩方法（Deflate）
            Buffer.BlockCopy(BitConverter.GetBytes((ushort)0), 0, header, 10, 2); // 最后修改时间
            Buffer.BlockCopy(BitConverter.GetBytes((ushort)0), 0, header, 12, 2); // 最后修改日期
            Buffer.BlockCopy(BitConverter.GetBytes(crc32), 0, header, 14, 4); // CRC - 32
            Buffer.BlockCopy(BitConverter.GetBytes((uint)compressedSize), 0, header, 18, 4); // 压缩大小
            Buffer.BlockCopy(BitConverter.GetBytes((uint)uncompressedSize), 0, header, 22, 4); // 未压缩大小
            Buffer.BlockCopy(BitConverter.GetBytes((ushort)relativePath.Length), 0, header, 26, 2); // 文件名长度
            Buffer.BlockCopy(BitConverter.GetBytes((ushort)0), 0, header, 28, 2); // 额外字段长度
            Buffer.BlockCopy(System.Text.Encoding.UTF8.GetBytes(relativePath), 0, header, 30, relativePath.Length);

            zipFileStream.Write(header, 0, header.Length);
        }

        private static void UpdateLocalFileHeaderCompressedSize(FileStream zipFileStream, long localFileHeaderOffset, int compressedSize)
        {
            long currentPosition = zipFileStream.Position;
            zipFileStream.Position = localFileHeaderOffset + 18;
            byte[] compressedSizeBytes = BitConverter.GetBytes((uint)compressedSize);
            zipFileStream.Write(compressedSizeBytes, 0, compressedSizeBytes.Length);
            zipFileStream.Position = currentPosition;
        }

        private static bool ReadLocalFileHeader(FileStream zipFileStream, out string relativePath, out int compressedSize, out int uncompressedSize, out uint crc32)
        {
            byte[] header = new byte[30];
            if (zipFileStream.Read(header, 0, header.Length) != header.Length)
            {
                relativePath = null;
                compressedSize = 0;
                uncompressedSize = 0;
                crc32 = 0;
                return false;
            }

            if (BitConverter.ToUInt32(header, 0) != 0x04034b50)
            {
                relativePath = null;
                compressedSize = 0;
                uncompressedSize = 0;
                crc32 = 0;
                return false;
            }

            ushort fileNameLength = BitConverter.ToUInt16(header, 26);
            byte[] fileNameBytes = new byte[fileNameLength];
            zipFileStream.Read(fileNameBytes, 0, fileNameLength);
            relativePath = System.Text.Encoding.UTF8.GetString(fileNameBytes);

            compressedSize = (int)BitConverter.ToUInt32(header, 18);
            uncompressedSize = (int)BitConverter.ToUInt32(header, 22);
            crc32 = BitConverter.ToUInt32(header, 14);

            return true;
        }

        private static void WriteCentralDirectory(FileStream zipFileStream, string[] filePaths, List<long> localFileHeaderOffsets, string baseDirectory)
        {
            long centralDirectoryOffset = zipFileStream.Position;

            for (int i = 0; i < filePaths.Length; i++)
            {
                string filePath = filePaths[i];
                string relativePath = GetRelativePath(baseDirectory, filePath);
                byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
                uint crc32 = CalculateCrc32(fileBytes);

                // 获取压缩大小
                long currentPosition = zipFileStream.Position;
                zipFileStream.Position = localFileHeaderOffsets[i] + 18;
                byte[] compressedSizeBytes = new byte[4];
                zipFileStream.Read(compressedSizeBytes, 0, 4);
                uint compressedSize = BitConverter.ToUInt32(compressedSizeBytes, 0);
                zipFileStream.Position = currentPosition;

                byte[] centralDirectoryRecord = new byte[46 + relativePath.Length];
                Buffer.BlockCopy(BitConverter.GetBytes(0x02014b50), 0, centralDirectoryRecord, 0, 4); // 中央目录文件头签名
                Buffer.BlockCopy(BitConverter.GetBytes((ushort)20), 0, centralDirectoryRecord, 4, 2); // 版本制作
                Buffer.BlockCopy(BitConverter.GetBytes((ushort)20), 0, centralDirectoryRecord, 6, 2); // 版本需要
                Buffer.BlockCopy(BitConverter.GetBytes((ushort)0), 0, centralDirectoryRecord, 8, 2); // 通用位标志
                Buffer.BlockCopy(BitConverter.GetBytes((ushort)8), 0, centralDirectoryRecord, 10, 2); // 压缩方法（Deflate）
                Buffer.BlockCopy(BitConverter.GetBytes((ushort)0), 0, centralDirectoryRecord, 12, 2); // 最后修改时间
                Buffer.BlockCopy(BitConverter.GetBytes((ushort)0), 0, centralDirectoryRecord, 14, 2); // 最后修改日期
                Buffer.BlockCopy(BitConverter.GetBytes(crc32), 0, centralDirectoryRecord, 16, 4); // CRC - 32
                Buffer.BlockCopy(BitConverter.GetBytes(compressedSize), 0, centralDirectoryRecord, 20, 4); // 压缩大小
                Buffer.BlockCopy(BitConverter.GetBytes((uint)fileBytes.Length), 0, centralDirectoryRecord, 24, 4); // 未压缩大小
                Buffer.BlockCopy(BitConverter.GetBytes((ushort)relativePath.Length), 0, centralDirectoryRecord, 28, 2); // 文件名长度
                Buffer.BlockCopy(BitConverter.GetBytes((ushort)0), 0, centralDirectoryRecord, 30, 2); // 额外字段长度
                Buffer.BlockCopy(BitConverter.GetBytes((ushort)0), 0, centralDirectoryRecord, 32, 2); // 文件注释长度
                Buffer.BlockCopy(BitConverter.GetBytes((ushort)0), 0, centralDirectoryRecord, 34, 2); // 磁盘编号开始
                Buffer.BlockCopy(BitConverter.GetBytes((ushort)0), 0, centralDirectoryRecord, 36, 2); // 内部文件属性
                Buffer.BlockCopy(BitConverter.GetBytes((uint)0), 0, centralDirectoryRecord, 38, 4); // 外部文件属性
                Buffer.BlockCopy(BitConverter.GetBytes((uint)localFileHeaderOffsets[i]), 0, centralDirectoryRecord, 42, 4); // 本地文件头偏移

                Buffer.BlockCopy(System.Text.Encoding.UTF8.GetBytes(relativePath), 0, centralDirectoryRecord, 46, relativePath.Length);

                zipFileStream.Write(centralDirectoryRecord, 0, centralDirectoryRecord.Length);
            }

            long centralDirectorySize = zipFileStream.Position - centralDirectoryOffset;

            // 写入中央目录结尾记录
            byte[] endOfCentralDirectoryRecord = new byte[22];
            Buffer.BlockCopy(BitConverter.GetBytes(0x06054b50), 0, endOfCentralDirectoryRecord, 0, 4); // 中央目录结尾记录签名
            Buffer.BlockCopy(BitConverter.GetBytes((ushort)0), 0, endOfCentralDirectoryRecord, 4, 2); // 磁盘编号
            Buffer.BlockCopy(BitConverter.GetBytes((ushort)0), 0, endOfCentralDirectoryRecord, 6, 2); // 中央目录开始磁盘编号
            Buffer.BlockCopy(BitConverter.GetBytes((ushort)filePaths.Length), 0, endOfCentralDirectoryRecord, 8, 2); // 该磁盘上的中央目录记录数
            Buffer.BlockCopy(BitConverter.GetBytes((ushort)filePaths.Length), 0, endOfCentralDirectoryRecord, 10, 2); // 中央目录记录总数
            Buffer.BlockCopy(BitConverter.GetBytes((uint)centralDirectorySize), 0, endOfCentralDirectoryRecord, 12, 4); // 中央目录大小
            Buffer.BlockCopy(BitConverter.GetBytes((uint)centralDirectoryOffset), 0, endOfCentralDirectoryRecord, 16, 4); // 中央目录开始偏移
            Buffer.BlockCopy(BitConverter.GetBytes((ushort)0), 0, endOfCentralDirectoryRecord, 20, 2); // 注释长度

            zipFileStream.Write(endOfCentralDirectoryRecord, 0, endOfCentralDirectoryRecord.Length);
        }

        private static uint CalculateCrc32(byte[] data)
        {
            const uint polynomial = 0xedb88320;
            uint[] table = new uint[256];

            for (uint i = 0; i < table.Length; ++i)
            {
                uint temp = i;
                for (int j = 8; j > 0; --j)
                {
                    if ((temp & 1) == 1)
                    {
                        temp = (temp >> 1) ^ polynomial;
                    }
                    else
                    {
                        temp >>= 1;
                    }
                }
                table[i] = temp;
            }

            uint crc = 0xffffffff;
            for (int i = 0; i < data.Length; ++i)
            {
                byte index = (byte)(((crc) & 0xff) ^ data[i]);
                crc = (crc >> 8) ^ table[index];
            }
            return ~crc;
        }

        private static void GetFiles(List<string> list, string directory)
        {
            string[] files = System.IO.Directory.GetFiles(directory);
            list.AddRange(files);
            string[] directories = System.IO.Directory.GetDirectories(directory);
            foreach (string item in directories)
            {
                GetFiles(list, item);
            }
        }

        private static string GetRelativePath(string basePath, string fullPath)
        {
            if (string.IsNullOrEmpty(basePath))
                throw new ArgumentNullException(nameof(basePath));
            if (string.IsNullOrEmpty(fullPath))
                throw new ArgumentNullException(nameof(fullPath));

            basePath = Path.GetFullPath(basePath).TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
            fullPath = Path.GetFullPath(fullPath);

            if (basePath == fullPath)
                return string.Empty;

            string[] baseParts = basePath.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
            string[] fullParts = fullPath.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);

            int commonLength = 0;
            while (commonLength < baseParts.Length && commonLength < fullParts.Length && baseParts[commonLength].Equals(fullParts[commonLength], StringComparison.OrdinalIgnoreCase))
            {
                commonLength++;
            }

            List<string> relativeParts = new List<string>();
            for (int i = commonLength; i < baseParts.Length; i++)
            {
                relativeParts.Add("..");
            }
            for (int i = commonLength; i < fullParts.Length; i++)
            {
                relativeParts.Add(fullParts[i]);
            }

            return string.Join(Path.DirectorySeparatorChar.ToString(), relativeParts);
        }
    }
}