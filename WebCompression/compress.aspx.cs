using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace WebCompression
{
    public partial class compress : System.Web.UI.Page
    {
        NormaliseText norma;
        NormaliseImage normaImage;
        long compressedLength = 0;
        long orignaLength = 0;
        string fileType;

        protected void Page_Load(object sender, EventArgs e)
        {
            uploadimage.Visible = false;
            text.Visible = false;
        }
        public void upload(object sender, EventArgs e)
        {
            if ((File1.PostedFile != null) && (File1.PostedFile.ContentLength > 0))
            {
                string fn = System.IO.Path.GetFileName(File1.PostedFile.FileName);
                string SaveLocation = Server.MapPath("uploadedFiles") + "\\" + fn;
                try
                {
                    File1.PostedFile.SaveAs(SaveLocation);
                    Response.Write("The file has been uploaded.");

                    File1.Visible = false;
                    btnUpload.Visible = false;
                    btnCompress.Visible = true;
                    string[] filepaths = Directory.GetFiles(Server.MapPath("~/uploadedFiles"));
                    String filepath = filepaths[0].Replace(Request.ServerVariables["APPL_PHYSICAL_PATH"], String.Empty);
                    
                    fileType = Path.GetExtension(filepath);
                    switch(fileType)
                    {
                        case".txt":
                        text.Visible = true;
                        using (StreamReader sr = new StreamReader(filepath))
                        {
                            while (sr.Peek() >= 0)
                            {
                                string line = sr.ReadToEnd();
                                text.InnerText += line;
                            }
                        }
                            break;
                        case ".jpg":
                        case ".JPG":
                            uploadimage.Visible = true;
                            uploadimage.Src =filepath;
                            break;
                    }
                    
                }
                catch (Exception ex)
                {
                    Response.Write("Error: ");
                }
            }
            else
            {
                Response.Write("Please select a file to upload.");
            }

           
        }

        public void compressfile(object sender, EventArgs e)
        {

        }
    }
}