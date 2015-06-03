using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using web_app.common;

namespace web_app.admin
{
    public partial class CImage2 : System.Web.UI.Page
    {
        // Genereate random number
        private Random random = new Random();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
                this.Session["CaptchaImageText"] = GenerateRandomCode();

            // Create a CAPTCHA image using the text stored in the Session object.
            CaptchaImage ci =
                new CaptchaImage(
                    this.Session["CaptchaImageText"].ToString(), 225, 40);

            // Change the response headers to output a JPEG image.
            this.Response.Clear();
            this.Response.ContentType = "image/jpeg";

            // Write the image to the response stream in JPEG format.
            ci.Image.Save(this.Response.OutputStream, ImageFormat.Jpeg);

            // Dispose of the CAPTCHA image object.
            ci.Dispose();

        }

        //
        // Returns a string of six random digits.
        //
        private string GenerateRandomCode()
        {
            string s = "";
            s = String.Concat(s, this.random.Next(100000, 999999).ToString());  
            return s;
        }
    }
}