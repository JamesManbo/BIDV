using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BIDV.Common;

namespace BIDV.Controllers
{
    public class UploadController : Controller
    {
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
    // GET: /Upload/ mvc5
         [Route("UpFileCV")]
        [HttpPost]
        [Authorize]
        public IHttpActionResult UploadFileCV()
        {
            // Lấy thông tin đăng nhập của user hiện tại
            var identity = HttpContext.Current.User.Identity as ClaimsIdentity;

            // Check user đăng nhập
            if (identity == null)
            {
                return Unauthorized();
            }

            // Get User Id
            string userId = identity.FindFirst("Id").Value;
            const int MAX_SIZE_FILE = 3 * 1024 * 1024; // 2 MB
            var result = new ApiResult<ErrorObject>();
            var allKeys = HttpContext.Current.Request.Form.AllKeys;
            string uploadedfilelist = "";
            string[] fileTypes = new string[] { FileType.WORD_DOC, FileType.WORD_DOCX, FileType.XLS,
                FileType.XLSX, FileType.PDF,FileType.JPEG, FileType.JPG, FileType.PNG };

            var files = HttpContext.Current.Request.Files.Count > 0 ? HttpContext.Current.Request.Files : null;
            result = ValidateFiles(files, fileTypes, MAX_SIZE_FILE);
            if (!result.Succeeded)
            {
                LogUtil.Info("UploadFileCV fileName = " + files[0].FileName);
                LogUtil.Info("UploadFileCV error mesage = " + result.ReturnMesage);
                return Ok(result);
            }
            for (int i = 0; i < files.Count; i++)
            {
                if (files[i].ContentLength > 0)
                {
                    try
                    {
                        string hashPath = Libs.GetMd5(userId.ToString() + EncryptCore.PassKey).Substring(0, 10);
                        var dicPath = "~" + ConfigUtil.SYLLDir + "/" + hashPath;
                        var physicalPath = System.Web.Hosting.HostingEnvironment.MapPath(dicPath);
                        var directory = new DirectoryInfo(physicalPath);
                        if (!directory.Exists)
                        {
                            directory.Create();
                        }
                        files[i].SaveAs(Path.Combine(physicalPath, files[i].FileName));
                        uploadedfilelist += files[i].FileName + ";";
                        result.ErrCode = 0;
                        result.ReturnMesage = string.IsNullOrEmpty(uploadedfilelist) ? "" : uploadedfilelist.Substring(0, uploadedfilelist.Length - 1);
                    }
                    catch (Exception e)
                    {
                        result.ErrCode = -5;
                        result.ReturnMesage = "Có lỗi xảy ra khi uploaf file: " + e.ToString();
                        LogUtil.Error(e.Message, e);
                    }

                }
            }
            return Ok((result));
        }
        //
        // GET: /Upload/ mvc5

        [HttpPost]
        public JsonResult UploadFiles(string folder)
        {
            var now = DateTime.Now;
            var files = Request.Files;
            if (Request.Files.Count <= 0) return Json(new { status = false, msg = "Bạn chưa chọn file." });
            var lstFiles = new List<string>();
            var virtualPath = string.Format("/Content/FrontEnd/_img_server/{0}/{1}/{2}/{3}/", folder, now.Year,
                now.Month, now.Day);
            var path = Server.MapPath(virtualPath);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            for (var i = 0; i < files.Count; i++)
            {
                var fileData = files[i];
                var filename = files[i].FileName;
                if (System.IO.File.Exists(Path.Combine(path, files[i].FileName)))
                {
                    filename = string.Format("copy({0})_{1}",HelperDateTime.Convert2TimeStamp(now), files[i].FileName);
                }
                fileData.SaveAs(string.Format("{0}/{1}", path, filename));
                lstFiles.Add(string.Format("{0}/{1}", virtualPath, filename));
            }
            return Json(new { status = true, msg = "Upload thành công", files = lstFiles });
        }
[AcceptVerbs("GET", "POST")]
        public IHttpActionResult ImageUpload(int id) //Up ảnh cho khi thêm mới campaign
        {
            using (OnfluencerEntities db = new OnfluencerEntities())
            {
                var path = "";
                string result = "";
                try
                {
                    //for tệp ảnh mới
                    for (int i = 0; i < HttpContext.Current.Request.Files.Count; i++)
                    {
                        var file = HttpContext.Current.Request.Files[i];
                        var isAvatar = file.FileName.Split('.')[0] == "avatar";
                        var filenameImage = DateTime.Now.Ticks + ".jpg";
                        //check xem có thư mục imageCampaign chưa, chưa có thì tạo mới
                        if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/imageCampaign")))
                        {
                            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/imageCampaign"));
                        }
                        Bitmap bmp = new Bitmap(HttpContext.Current.Request.Files[i].InputStream);
                        OrientImage(bmp);

                        // tạo chuối file path
                        path = Path.Combine(HttpContext.Current.Server.MapPath("~/imageCampaign"), filenameImage);
                        //file.SaveAs(path);
                        path = path.ToLower();
                        bmp.Save(path);
                        var pathNewFile = path.Replace(".jpeg", "_m.jpg").Replace(".jpg", "_m.jpg").Replace(".png", "_m.jpg").Replace(".svg", "_m.jpg").Replace(".gif", "_m.jpg");

                        using (Image image = Image.FromFile(path))
                        {
                            using (var resized = ImageUtilities.ScaleImage(image, 800, 800))
                            {
                                //save the resized image as a jpeg with a quality of 90
                                ImageUtilities.SaveJpeg(pathNewFile, resized, 90);
                            }
                        }
                        result = "/imageCampaign/" + filenameImage;
                        var ImageUrlThumbnail = result.Replace(".jpg", "_m.jpg");

                        CampaignImage objImage = new CampaignImage();
                        objImage.CampaignId = id;
                        objImage.ImageUrl = result;
                        objImage.ImageUrlThumbnail = ImageUrlThumbnail;
                        objImage.IsAvatar = isAvatar;
                        db.CampaignImages.Add(objImage);
                        //Add db table ImageCampaign

                    }
                    // for tệp ảnh cũ
                    foreach (var key in HttpContext.Current.Request.Form.AllKeys)
                    {
                        var isAvatar = key == "avatar";
                        var imgId = int.Parse(HttpContext.Current.Request.Form[key]);
                        var objImg = db.CampaignImages.FirstOrDefault(t => t.Id == imgId);
                        if (objImg != null)
                        {
                            CampaignImage objImage = new CampaignImage();
                            objImage.CampaignId = id;
                            objImage.ImageUrl = objImg.ImageUrl;
                            objImage.ImageUrlThumbnail = objImg.ImageUrlThumbnail;
                            objImage.IsAvatar = isAvatar;
                            db.CampaignImages.Add(objImage);
                        }
                    }
                    db.SaveChanges();
                    //for (int i = 0; i < HttpContext.Current.Request.Form.Count; i++)
                    //{
                    //    var file = HttpContext.Current.Request.Form[i];
                    //    var debug = file.ToString();
                    //var isAvatar = file..FileName.Split('.')[0] == "avatar";
                    //var filenameImage = file.FileName.Split('.')[0] + DateTime.Now.ToString("yyyyMMddTHHmmss") + ".jpg";
                    //DateTime time = DateTime.Now;
                    ////check xem có thư mục imageCampaign chưa, chưa có thì tạo mới
                    //if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/imageCampaign")))
                    //{
                    //    Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/imageCampaign"));
                    //}
                    //// tạo chuối file path
                    //path = Path.Combine(HttpContext.Current.Server.MapPath("~/imageCampaign"), filenameImage);
                    //file.SaveAs(path);
                    //result = "/imageCampaign/" + filenameImage;
                    ////Add db table ImageCampaign
                    //CampaignImage objImage = new CampaignImage();
                    //objImage.CampaignId = id;
                    //objImage.ImageUrl = result;
                    //objImage.IsAvatar = isAvatar;
                    //db.CampaignImages.Add(objImage);
                    //db.SaveChanges();
                    //}
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, result));
                }
                catch (Exception ex)
                {
                    result = ex.ToString();
                    return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, result));
                }
            }
        }
    // GET: /Upload/ mvc5
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult ImageUploadForEdit(int id) //Up ảnh cho campaign
        {
            using (OnfluencerEntities db = new OnfluencerEntities())
            {
                var path = "";
                string result = "";
                try
                {
                    // xóa ảnh cũ không sử dụng
                    var lstImageId = new List<int>();
                    foreach (var key in HttpContext.Current.Request.Form.AllKeys)
                    {
                        var isAvatar = key == "avatar";
                        var imgId = int.Parse(HttpContext.Current.Request.Form[key]);
                        CampaignImage objImg = db.CampaignImages.Find(imgId);
                        objImg.IsAvatar = isAvatar;
                        lstImageId.Add(imgId);
                    }

                    var lstCampaignImgRemove = db.CampaignImages.Where(t => t.CampaignId == id && !lstImageId.Contains(t.Id)).ToList();
                    foreach (var objRemove in lstCampaignImgRemove)
                    {
                        db.CampaignImages.Remove(objRemove);
                    }

                    //for tệp ảnh mới
                    for (int i = 0; i < HttpContext.Current.Request.Files.Count; i++)
                    {
                        var file = HttpContext.Current.Request.Files[i];
                        var isAvatar = file.FileName.Split('.')[0] == "avatar";
                        var filenameImage = DateTime.Now.Ticks + ".jpg";
                        DateTime time = DateTime.Now;
                        //check xem có thư mục imageCampaign chưa, chưa có thì tạo mới
                        if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/imageCampaign")))
                        {
                            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/imageCampaign"));
                        }

                        Bitmap bmp = new Bitmap(HttpContext.Current.Request.Files[i].InputStream);
                        OrientImage(bmp);

                        // tạo chuối file path
                        path = Path.Combine(HttpContext.Current.Server.MapPath("~/imageCampaign"), filenameImage);
                        //file.SaveAs(path);
                        path = path.ToLower();
                        bmp.Save(path);
                        var pathNewFile = path.Replace(".jpeg", "_m.jpg").Replace(".jpg", "_m.jpg").Replace(".png", "_m.jpg").Replace(".svg", "_m.jpg").Replace(".gif", "_m.jpg");

                        // tạo chuối file path
                        //path = Path.Combine(HttpContext.Current.Server.MapPath("~/imageCampaign"), filenameImage);
                        //file.SaveAs(path);
                        //var pathNewFile = path.Replace(".jpg", "_m.jpg").Replace(".png", "_m.jpg");

                        using (Image image = Image.FromFile(path))
                        {
                            using (var resized = ImageUtilities.ScaleImage(image, 800, 800))
                            {
                                //save the resized image as a jpeg with a quality of 90
                                ImageUtilities.SaveJpeg(pathNewFile, resized, 90);
                            }
                        }
                        result = "/imageCampaign/" + filenameImage;
                        var ImageUrlThumbnail = result.Replace(".jpg", "_m.jpg");
                        //Add db table ImageCampaign
                        CampaignImage objImage = new CampaignImage();
                        objImage.CampaignId = id;
                        objImage.ImageUrl = result;
                        objImage.ImageUrlThumbnail = ImageUrlThumbnail;
                        objImage.IsAvatar = isAvatar;
                        db.CampaignImages.Add(objImage);
                    }
                    db.SaveChanges();


                    //foreach (var key in HttpContext.Current.Request.Form.AllKeys)
                    //{
                    //    var isAvatar = key == "avatar";
                    //    var imgId = int.Parse(HttpContext.Current.Request.Form[key]);
                    //    var objImg = db.CampaignImages.FirstOrDefault(t => t.Id == imgId);
                    //    if (objImg != null)
                    //    {
                    //        CampaignImage objImage = new CampaignImage();
                    //        objImage.CampaignId = id;
                    //        objImage.ImageUrl = objImg.ImageUrl;
                    //        objImage.IsAvatar = isAvatar;
                    //        db.CampaignImages.Add(objImage);
                    //        db.SaveChanges();
                    //    }

                    //}

                    //for (int i = 0; i < HttpContext.Current.Request.Form.Count; i++)
                    //{
                    //    var file = HttpContext.Current.Request.Form[i];
                    //    var debug = file.ToString();
                    //var isAvatar = file..FileName.Split('.')[0] == "avatar";
                    //var filenameImage = file.FileName.Split('.')[0] + DateTime.Now.ToString("yyyyMMddTHHmmss") + ".jpg";
                    //DateTime time = DateTime.Now;
                    ////check xem có thư mục imageCampaign chưa, chưa có thì tạo mới
                    //if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/imageCampaign")))
                    //{
                    //    Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/imageCampaign"));
                    //}
                    //// tạo chuối file path
                    //path = Path.Combine(HttpContext.Current.Server.MapPath("~/imageCampaign"), filenameImage);
                    //file.SaveAs(path);
                    //result = "/imageCampaign/" + filenameImage;
                    ////Add db table ImageCampaign
                    //CampaignImage objImage = new CampaignImage();
                    //objImage.CampaignId = id;
                    //objImage.ImageUrl = result;
                    //objImage.IsAvatar = isAvatar;
                    //db.CampaignImages.Add(objImage);
                    //db.SaveChanges();
                    //}
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, result));
                }
                catch (Exception ex)
                {
                    result = ex.ToString();
                    return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, result));
                }
            }
            
        }
        
    }
}
