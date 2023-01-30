using KlementWeb.Business.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Drawing;
using System.IO;

namespace KlementWeb.Business.Managers
{
    public class PictureManager : IPictureManager
    {
        public enum PictureExtension { Bmp, Gif, Jpeg, Png }

        public string OutputDirectoryPath { get; private set; }

        public PictureManager(string outputDirectoryPath) => OutputDirectoryPath = outputDirectoryPath;

        public void ResizePicture(string path, int width = 0, int height = 0)
        {
            Image picture = Image.FromFile(path);
            ResizePicture(picture, width, height);
        }

        /// <summary>
        /// A wrapper method for saving an image from an input.
        /// </summary>
        /// <param name="file">Raw image file to save</param>
        /// <param name="fileName">File name after saving (without extension).</param>
        /// <param name="extension">The format in which to save the image.</param>
        /// <param name="width">Image width after saving (optional parameter).</param>
        /// <param name="height">Image height after saving (optional parameter)</param>
        public void SavePicture(IFormFile file, string fileName, PictureExtension extension, int width = 0, int height = 0)
        {
            using (var stream = new MemoryStream())
            {
                file.CopyTo(stream);
                Image image = Image.FromStream(stream);

                // By default, System.Drawing will keep the aspect ratio if any dimension == 0
                Image newImage = ResizePicture(image, width, height);

                switch (extension)
                {
                    case PictureExtension.Bmp:
                        fileName += ".bmp";
                        break;

                    case PictureExtension.Gif:
                        fileName += ".gif";
                        break;

                    case PictureExtension.Jpeg:
                        fileName += ".jpeg";
                        break;

                    case PictureExtension.Png:
                        fileName += ".png";
                        break;

                    default:
                        return;
                }

                newImage.Save(OutputDirectoryPath + fileName);
            }
        }

        private Image ResizePicture(Image picture, int width = 0, int height = 0)
        {
            // By default, System.Drawing will keep the aspect ratio if any dimension == 0
            if (width > 0 || height > 0)
            {
                Image newImage = new Bitmap(picture, new Size(width, height));

                return newImage;
            }

            return picture;
        }  
    }
}
