using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebCompression
{
    public partial class extract : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void extractfile(object sender, EventArgs e)
        {
            if ((File1.PostedFile != null) && (File1.PostedFile.ContentLength > 0))
            {
                string fn = System.IO.Path.GetFileName(File1.PostedFile.FileName);
                Session["filename"] = fn;
                string SaveLocation = Server.MapPath("uploadedFiles") + "\\" + fn;
                try
                {
                    File1.PostedFile.SaveAs(SaveLocation);

                    btnExtract.Visible = true;
                    String filepath = findFilePath((String)Session["filename"]);
                    String actualFile = findFile((String)Session["filename"]);
                    string fileType = Path.GetExtension(filepath);

                    Session["filetype"] = fileType;

                    switch (fileType)
                    {
                        case ".cmi":
                            CImage cimage = DecompressAndDeserialize<CImage>(File.ReadAllBytes(actualFile));
                            ExtractImage extI = new ExtractImage(cimage);

                            ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);
                            
                            System.Drawing.Imaging.Encoder myEncoder =
                            System.Drawing.Imaging.Encoder.Quality;
                            EncoderParameters myEncoderParameters = new EncoderParameters(1);
                            EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 100L);
                            myEncoderParameters.Param[0] = myEncoderParameter;

                            string filename = Server.MapPath("resources/ExtractedImage.jpg");
                            extI.extract().Save(filename, jpgEncoder, myEncoderParameters);

                            OpenWindow();
                            break;
                        case ".cmx":
                            ExtractText ext = new ExtractText(File.ReadAllBytes(actualFile));


                            string filenam = Server.MapPath("resources/ExtractedText.txt");
                            File.WriteAllText(filenam, ext.extract());

                            OpenWindow();

                            break;
                        default:

                            btnExtract.Visible = true;
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Upload .cmx or.cmi files only')", true);

                            break;
                    }


                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Unkown error occured')", true);

                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please upload a file')", true);
                            }
        }
        private string findFilePath(String filename)
        {
            string[] filepaths = Directory.GetFiles(Server.MapPath("~/uploadedFiles"));
            for (int i = 0; i < filepaths.Length; i++)
            {
                if (Path.GetFileName(filepaths[i]).Equals(filename))
                {
                    return filepaths[i].Replace(Request.ServerVariables["APPL_PHYSICAL_PATH"], String.Empty);
                }
            }
            return null;
        }
        private string findFile(String filename)
        {
            string[] filepaths = Directory.GetFiles(Server.MapPath("~/uploadedFiles"));
            for (int i = 0; i < filepaths.Length; i++)
            {
                if (Path.GetFileName(filepaths[i]).Equals(filename))
                {
                    return filepaths[i];
                }
            }
            return null;
        }
        protected void OpenWindow()
        {
            
            string redirect = "<script>window.open('download.aspx');</script>";
            Response.Write(redirect);
        }
        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            var codecs = ImageCodecInfo.GetImageDecoders();
            foreach (var codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }
        public static T DecompressAndDeserialize<T>(byte[] data)
        {
            using (MemoryStream ms = new MemoryStream(data))
            using (GZipStream zs = new GZipStream(ms, CompressionMode.Decompress, true))
            {
                BinaryFormatter bf = new BinaryFormatter();
                ms.Position = 0;
                return (T)bf.Deserialize(zs);
            }
        }
    }
}


        
       
