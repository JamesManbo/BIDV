using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web.Helpers;


namespace BIDV.Common
{
    public static class HelperImages
    {
        /// <summary>  
        /// Save image as jpeg  
        /// </summary>  
        /// <param name="path">path where to save</param>  
        /// <param name="img">image to save</param>  
        public static void SaveJpeg(string path, Image img)
        {
            var qualityParam = new EncoderParameter(Encoder.Quality, 100L);
            var jpegCodec = GetEncoderInfo("image/jpeg");

            var encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = qualityParam;
            img.Save(path, jpegCodec, encoderParams);
        }

        /// <summary>  
        /// Save image  
        /// </summary>  
        /// <param name="path">path where to save</param>  
        /// <param name="img">image to save</param>  
        /// <param name="imageCodecInfo">codec info</param>  
        public static void Save(string path, Image img, ImageCodecInfo imageCodecInfo)
        {
            var qualityParam = new EncoderParameter(Encoder.Quality, 100L);

            var encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = qualityParam;
            img.Save(path, imageCodecInfo, encoderParams);
        }

        /// <summary>  
        /// get codec info by mime type  
        /// </summary>  
        /// <param name="mimeType"></param>  
        /// <returns></returns>  
        public static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            return ImageCodecInfo.GetImageEncoders().FirstOrDefault(t => t.MimeType == mimeType);
        }

        /// <summary>  
        /// the image remains the same size, and it is placed in the middle of the new canvas  
        /// </summary>  
        /// <param name="image">image to put on canvas</param>  
        /// <param name="width">canvas width</param>  
        /// <param name="height">canvas height</param>  
        /// <param name="canvasColor">canvas color</param>  
        /// <returns></returns>  
        public static Image PutOnCanvas(Image image, int width, int height, Color canvasColor)
        {
            var res = new Bitmap(width, height);
            using (var g = Graphics.FromImage(res))
            {
                g.Clear(canvasColor);
                var x = (width - image.Width) / 2;
                var y = (height - image.Height) / 2;
                g.DrawImageUnscaled(image, x, y, image.Width, image.Height);
            }

            return res;
        }

        /// <summary>  
        /// the image remains the same size, and it is placed in the middle of the new canvas  
        /// </summary>  
        /// <param name="image">image to put on canvas</param>  
        /// <param name="width">canvas width</param>  
        /// <param name="height">canvas height</param>  
        /// <returns></returns>  
        public static Image PutOnWhiteCanvas(Image image, int width, int height)
        {
            return PutOnCanvas(image, width, height, Color.White);
        }

        /// <summary>  
        /// resize an image and maintain aspect ratio  
        /// </summary>  
        /// <param name="image">image to resize</param>  
        /// <param name="newWidth">desired width</param>  
        /// <param name="maxHeight">max height</param>  
        /// <param name="onlyResizeIfWider">if image width is smaller than newWidth use image width</param>  
        /// <returns>resized image</returns>  
        public static Image Resize(Image image, int newWidth, int maxHeight, bool onlyResizeIfWider)
        {
            if (onlyResizeIfWider && image.Width <= newWidth) newWidth = image.Width;

            var newHeight = image.Height * newWidth / image.Width;
            if (newHeight > maxHeight)
            {
                // Resize with height instead  
                newWidth = image.Width * maxHeight / image.Height;
                newHeight = maxHeight;
            }

            var res = new Bitmap(newWidth, newHeight);

            using (var graphic = Graphics.FromImage(res))
            {
                graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphic.SmoothingMode = SmoothingMode.HighQuality;
                graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphic.CompositingQuality = CompositingQuality.HighQuality;
                graphic.DrawImage(image, 0, 0, newWidth, newHeight);
            }

            return res;
        }
        /// <summary>
        /// resize image by width, height is auto
        /// </summary>
        /// <param name="image"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        public static Image Resize(Image image, int width)
        {
            int sourceWidth = image.Width;
            int sourceHeight = image.Height;

            float nPercent = ((float)width / (float)sourceWidth);

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.DrawImage(image, 0, 0, destWidth, destHeight);
            g.Dispose();

            return b;
        }
        /// <summary>  
        /// Crop an image   
        /// </summary>  
        /// <param name="img">image to crop</param>  
        /// <param name="cropArea">rectangle to crop</param>  
        /// <returns>resulting image</returns>  
        public static Image Crop(Image img, Rectangle cropArea)
        {
            var bmpImage = new Bitmap(img);
            var bmpCrop = bmpImage.Clone(cropArea, bmpImage.PixelFormat);
            return bmpCrop;
        }

        public static byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }

        public static Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

        //The actual converting function  
        public static string GetImage(object img)
        {
            return "data:image/jpg;base64," + Convert.ToBase64String((byte[])img);
        }


        public static void PerformImageResizeAndPutOnCanvas(string pFilePath, string pFileName, int pWidth, int pHeight, string pOutputFileName)
        {

            System.Drawing.Image imgBef;
            imgBef = System.Drawing.Image.FromFile(pFilePath + pFileName);


            System.Drawing.Image _imgR;
            _imgR = Resize(imgBef, pWidth, pHeight, true);


            System.Drawing.Image _img2;
            _img2 = PutOnCanvas(_imgR, pWidth, pHeight, System.Drawing.Color.White);

            //Save JPEG  
            SaveJpeg(pFilePath + pOutputFileName, _img2);
        }

        public static void SaveAndResize(WebImage image, int width, int height, string filename, string filepath)
        {
            image.Resize(width, height);
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            string pathsave;
            if (System.IO.File.Exists(Path.Combine(filepath, filename)))
            {
                pathsave = Path.Combine(filepath, filename);
            }
            else
            {
                pathsave = Path.Combine(filepath, filename);
            }
            image.Save(pathsave);
        }

        public static void SaveAndResizeImage(WebImage image, int width, string filename, string filepath)
        {            
            int sourceWidth = image.Width;
            int sourceHeight = image.Height;
            float nPercent = ((float)sourceWidth / (float)width);
            int destHeight = (int)(sourceHeight / nPercent);
            image.Resize(width, destHeight);
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            string pathsave;
            if (System.IO.File.Exists(Path.Combine(filepath, filename)))
            {
                pathsave = Path.Combine(filepath, filename);
            }
            else
            {
                pathsave = Path.Combine(filepath, filename);
            }
            image.Save(pathsave);
        }
        public static void SaveImage(WebImage image, string filename, string filepath)
        {
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            string pathsave;
            if (System.IO.File.Exists(Path.Combine(filepath, filename)))
            {
                pathsave = Path.Combine(filepath, filename);
            }
            else
            {
                pathsave = Path.Combine(filepath, filename);
            }
            image.Save(pathsave);
        }
        //validate file image
        public static string Validate(HttpPostedFileBase file,bool isRequire)
        {
            if(isRequire && file==null)
            {
                return MessageUtils.Err("Chưa chọn ảnh");
            }
            
            if(file!=null)
            {
                string filetype = "jpg,jpeg,png,gif,";
                double maxfilesize = 3;
                maxfilesize = maxfilesize * 1024;
                string fileext = System.IO.Path.GetExtension(file.FileName).Replace(".", "").ToLower();
                if (("," + filetype).IndexOf("," + fileext + ",") < 0)
                {
                    return MessageUtils.Err("File không được phép");
                }

                int length = file.ContentLength;
                if (((length / 1024)) > maxfilesize)
                {
                    return MessageUtils.Err("Dung lượng file lớn hơn cho phép: " + Math.Round(maxfilesize, 2) + " KB");
                }
            }
            
            return string.Empty;
        }
        /// <summary>  
        /// Crop an image   
        /// </summary>  
        /// <param name="img">C</param>  
        /// <param name="cropArea">ValidateFiles</param>  
        /// <returns>resulting image</returns>  
        ///var files = HttpContext.Current.Request.Files.Count > 0 ? HttpContext.Current.Request.Files : null;
        ///    if (null != files)
        ///    {
        ///        LogUtil.Info("UploadFileAvatar fileCount = " + files.Count);
          ///  }
         ///   result = ValidateFiles(files, fileTypes, MAX_SIZE_FILE);
        private ApiResult<ErrorObject> ValidateFiles(HttpFileCollection files, string[] fileTypes, int maxFileSize)
        {
            // Kiểm tra file not found
            ApiResult<ErrorObject> result = new ApiResult<ErrorObject>();
            if (files == null || files.Count <= 0)
            {
                result.Failed();
                result.ErrCode = -4;
                result.ReturnMesage = "Không có file";
                return result;
            }

            for (int i = 0; i < files.Count; i++)
            {
                var arrTypeName = files[i].FileName.Split('.');
                if (arrTypeName.Length <= 1)
                {
                    result.Failed();
                    result.ErrCode = -3;
                    result.ReturnMesage = "Tên file không hợp lệ";
                    return result;
                }
                string fileType = arrTypeName[arrTypeName.Length - 1];
                if (!Array.Exists(fileTypes, x => x.ToLower() == "." + fileType.ToLower()))
                {
                    result.Failed();
                    result.ErrCode = -1;
                    result.ReturnMesage = "Định dạng file không hỗ trợ. Vui lòng upload file có các định dạng(" + string.Join(",", fileTypes) + ")";
                    return result;
                }
                if (files[i].ContentLength > maxFileSize)
                {
                    result.Failed();
                    result.ErrCode = -2;
                    result.ReturnMesage = "File quá lớn. Xin vui lòng upload file dưới " + ((int)maxFileSize / 1024 / 1024) + "MB";
                    return result;
                }
            }
            return result;
        }
    }
}
