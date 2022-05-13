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
        //
        // GET: /Upload/

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
