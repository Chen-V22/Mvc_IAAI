using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;

namespace MVC_IA.Models
{
    public class Utility
    {
        #region "密碼加密"
        public const int DefaultSaltSize = 5;
        /// <summary>
        /// 產生Salt
        /// </summary>
        /// <returns>Salt</returns>
        public static string CreateSalt()
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buffer = new byte[DefaultSaltSize];
            rng.GetBytes(buffer);
            return Convert.ToBase64String(buffer);
        }
        ///// <summary>
        ///// 密碼加密
        ///// </summary>
        ///// <param name="password">密碼明碼</param>
        ///// <returns>Hash後密碼</returns>
        //public static string CreateHash(string password)
        //{
        //    string salt = CreateSalt();
        //    string saltAndPassword = String.Concat(password, salt);
        //    string hashedPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(saltAndPassword, "SHA1");
        //    hashedPassword = string.Concat(hashedPassword, salt);
        //    return hashedPassword;
        //}

        /// <summary>
        /// Computes a salted hash of the password and salt provided and returns as a base64 encoded string.
        /// </summary>
        /// <param name="password">The password to hash.</param>
        /// <param name="salt">The salt to use in the hash.</param>
        public static string GenerateHashWithSalt(string password, string salt)
        {
            // merge password and salt together
            string sHashWithSalt = password + salt;
            // convert this merged value to a byte array
            byte[] saltedHashBytes = Encoding.UTF8.GetBytes(sHashWithSalt);
            // use hash algorithm to compute the hash
            HashAlgorithm algorithm = new SHA256Managed();
            // convert merged bytes to a hash as byte array
            byte[] hash = algorithm.ComputeHash(saltedHashBytes);
            // return the has as a base 64 encoded string
            return Convert.ToBase64String(hash);
        }

        #endregion
        #region "將使用者資料寫入cookie,產生AuthenTicket"
        /// <summary>
        /// 將使用者資料寫入cookie,產生AuthenTicket
        /// </summary>
        /// <param name="userData">使用者資料</param>
        /// <param name="userId">UserAccount</param>
        static public void SetAuthenTicket(string userData, string userId)
        {
            //宣告一個驗證票
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, userId, DateTime.Now, DateTime.Now.AddHours(3), false, userData);
            //加密驗證票
            string encryptedTicket = FormsAuthentication.Encrypt(ticket);
            //建立Cookie
            HttpCookie authenticationcookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            //將Cookie寫入回應

            HttpContext.Current.Response.Cookies.Add(authenticationcookie);

        }
        #endregion

        #region"儲存上傳檔案"
        /// <summary>
        /// 儲存上傳檔案
        /// </summary>
        /// <param name="upfile">HttpPostedFile 物件</param>
        /// <returns>儲存檔名</returns>
        static public string SaveUpFile(HttpPostedFileBase upfile)
        {
            //取得副檔名
            string extension = upfile.FileName.Split('.')[upfile.FileName.Split('.').Length - 1];
            string fileName = upfile.FileName.Substring(0, upfile.FileName.Length - extension.Length - 1);
            string fileNameTemp = fileName;
            int i = 1;
            while (System.IO.File.Exists(Path.Combine(HttpContext.Current.Server.MapPath("~/upfiles"), string.Format("{0}.{1}", fileNameTemp, extension))))
            {
                fileNameTemp = fileName + "(" + i.ToString(CultureInfo.InvariantCulture) + ")";
                i++;
            }

            //新檔案名稱
            fileName = string.Format("{0}.{1}", fileNameTemp, extension);
            string savedName = Path.Combine(HttpContext.Current.Server.MapPath("~/upfiles"), fileName);
            upfile.SaveAs(savedName);
            return fileName;
        }
        #endregion

        #region"儲存上傳圖片"
        /// <summary>
        /// 儲存上傳圖片
        /// </summary>
        /// <param name="upfile">HttpPostedFile 物件</param>
        /// <returns>儲存檔名</returns>
        static public string SaveUpImage(HttpPostedFileBase upfile)
        {

            //取得副檔名
            string extension = upfile.FileName.Split('.')[upfile.FileName.Split('.').Length - 1];
            //新檔案名稱
            string fileName = String.Format("{0:yyyyMMddhhmmsss}.{1}", DateTime.Now, extension);
            string savedName = Path.Combine(HttpContext.Current.Server.MapPath("~/upfile/images"), fileName);
            upfile.SaveAs(savedName);
            return fileName;
        }
        #endregion

        #region "舉世無敵縮圖程式"
        /// <summary>
        /// 舉世無敵縮圖程式(多載)
        /// 1.會自動判斷是比較高還是比較寬，以比較大的那一方決定要縮的尺寸
        /// 2.指定寬度，等比例縮小
        /// 3.指定高度，等比例縮小
        /// </summary>
        /// <param name="name">原檔檔名</param>
        /// <param name="source">來源檔案的Stream,可接受上傳檔案</param>
        /// <param name="target">目的路徑</param>
        /// <param name="suffix">縮圖辯識符號</param>
        /// <param name="MaxWidth">指定要縮的寬度</param>
        /// <param name="MaxHight">指定要縮的高度</param>
        /// <remarks></remarks>
        static public void GenerateThumbnailImage(string name, System.IO.Stream source, string target, string suffix, int MaxWidth, int MaxHight)
        {
            System.Drawing.Image baseImage = System.Drawing.Image.FromStream(source);
            Single ratio = 0.0F; //存放縮圖比例
            Single h = baseImage.Height; //圖像原尺寸高度
            Single w = baseImage.Width;  //圖像原尺寸寬度
            int ht; //圖像縮圖後高度
            int wt;//圖像縮圖後寬度
            //寬固定算高
            ratio = MaxWidth / w; //計算寬度縮圖比例
            if (MaxWidth < w)
            {
                ht = Convert.ToInt32(ratio * h);
                wt = MaxWidth;

            }
            else
            {
                ht = Convert.ToInt32(baseImage.Height);
                wt = Convert.ToInt32(baseImage.Width);

            }
            if (MaxHight > ht || MaxWidth >= w)
            {
                ratio = MaxHight / h; //計算寬度縮圖比例
                if (MaxHight < h)
                {
                    ht = MaxHight;
                    wt = Convert.ToInt32(ratio * w);
                }
                else
                {
                    ht = Convert.ToInt32(baseImage.Height);
                    wt = Convert.ToInt32(baseImage.Width);
                }
            }
            string Newname = target + "\\" + suffix + name;
            System.Drawing.Bitmap img = new System.Drawing.Bitmap(wt, ht);
            System.Drawing.Graphics graphic = Graphics.FromImage(img);
            graphic.CompositingQuality = CompositingQuality.HighQuality;
            graphic.SmoothingMode = SmoothingMode.HighQuality;
            graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphic.DrawImage(baseImage, 0, 0, wt, ht);
            img.Save(Newname);

            img.Dispose();
            graphic.Dispose();
            baseImage.Dispose();

        }
        #endregion

    }
}