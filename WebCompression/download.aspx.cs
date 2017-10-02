using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebCompression
{
    public partial class download : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            String FileName = null;
            switch((string)Session["filetype"])
            {
                case ".txt":
                    FileName = "CompressedText.cmx";
                    break;
                case ".JPG":
                case ".jpg":
                    FileName = "CompressedImage.cmi";

                    break;
                case ".cmi":
                    FileName = "ExtractedImage.jpg";
                    break;
          
                case ".cmx":
                    FileName = "ExtractedText.txt";
                    break;

                   
            }
         
            string fn = Server.MapPath("resources/" + FileName);
            System.Web.HttpResponse responses = System.Web.HttpContext.Current.Response;
            responses.ClearContent();
            responses.Clear();
            responses.ContentType = "text/plain";
            responses.AddHeader("Content-Disposition", "attachment; filename=" + FileName + ";");
            responses.TransmitFile(fn);
            responses.Flush();
            responses.End();
        }
    }
}