using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using FITKMS_business.Data;

namespace FITKMS.Users
{
    /// <summary>
    /// Summary description for ImageHandler
    /// </summary>
    public class ImageHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            int userId = 0;
            KorisniciDataSet.korisniciRow user = null;
            if (context.Request["userId"] != null)
            {
                userId = Convert.ToInt32(context.Request["userId"]);
                user = DAKorisnici.SelectById(userId);

                if (user.IsSlikaNull())
                {
                    System.Drawing.Image image = System.Drawing.Image.FromFile(context.Server.MapPath(context.Request.ApplicationPath)
                                                    + "\\Content\\img\\profile.png");
                    MemoryStream ms = new MemoryStream();
                    image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    context.Response.BinaryWrite(ms.ToArray());
                }
                else
                {
                    context.Response.BinaryWrite(user.Slika);
                    context.Response.ContentType = user.SlikaType;
                }
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}