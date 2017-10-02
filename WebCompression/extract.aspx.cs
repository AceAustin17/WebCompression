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
                string SaveLocation = Server.MapPath("uploadedFiles") + "\\" + fn;
                try
                {
                    File1.PostedFile.SaveAs(SaveLocation);

                    btnExtract.Visible = true;
                    string[] filepaths = Directory.GetFiles(Server.MapPath("~/uploadedFiles"));
                    String filepath = filepaths[0].Replace(Request.ServerVariables["APPL_PHYSICAL_PATH"], String.Empty);

                    string fileType = Path.GetExtension(filepath);

                    Session["filetype"] = fileType;

                    switch (fileType)
                    {
                        case ".cmi":
                            CImage cimage = DecompressAndDeserialize<CImage>(File.ReadAllBytes(filepaths[0]));
                            ExtractImage extI = new ExtractImage(cimage);

                            ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);

                            //Create an Encoder object based on the GUID
                            // for the Quality parameter category.  
                            System.Drawing.Imaging.Encoder myEncoder =
                            System.Drawing.Imaging.Encoder.Quality;

                            // Create an EncoderParameters object.  
                            // An EncoderParameters object has an array of EncoderParameter  
                            // objects. In this case, there is only one  
                            // EncoderParameter object in the array.  
                            EncoderParameters myEncoderParameters = new EncoderParameters(1);
                            EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 100L);
                            myEncoderParameters.Param[0] = myEncoderParameter;

                            string filename = Server.MapPath("resources/ExtractedImage.jpg");
                            extI.extract().Save(filename, jpgEncoder, myEncoderParameters);

                            OpenWindow();
                            break;
                        case ".cmx":
                            ExtractText ext = new ExtractText(File.ReadAllBytes(filepaths[0]));


                            string filenam = Server.MapPath("resources/ExtractedText.txt");
                            File.WriteAllText(filenam, ext.extract());

                            OpenWindow();

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


        
       
