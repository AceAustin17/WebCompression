using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;

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
            ResultsText.Visible = false;
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

                    File1.Visible = false;
                    btnUpload.Visible = false;
                    btnCompress.Visible = true;
                    string[] filepaths = Directory.GetFiles(Server.MapPath("~/uploadedFiles"));
                    String filepath = filepaths[0].Replace(Request.ServerVariables["APPL_PHYSICAL_PATH"], String.Empty);
                    
                    fileType = Path.GetExtension(filepath);

                    Session["filetype"] = fileType;

                    switch(fileType)
                    {
                        case".txt":
                        text.Visible = true;
                        using (StreamReader sr = new StreamReader(filepaths[0]))
                        {
                            while (sr.Peek() >= 0)
                            {
                                string line = sr.ReadToEnd();
                                text.InnerText += line;
                            }
                        }
                            norma = new NormaliseText(filepaths[0]);
                            norma.saveToXML();

                            Session["norm"] = norma;
                            break;
                        case ".jpg":
                        case ".JPG":
                            uploadimage.Visible = true;
                            uploadimage.Src =filepath;
                            normaImage = new NormaliseImage(filepaths[0]);
                            normaImage.saveToXML();

                            Session["norm"] = normaImage;
                            break;
                    }
                    orignaLength = new System.IO.FileInfo(filepaths[0]).Length;

                    Session["originalLen"] = orignaLength;

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

            string[] filepaths = Directory.GetFiles(Server.MapPath("~/uploadedFiles"));
            String filepath = filepaths[0].Replace(Request.ServerVariables["APPL_PHYSICAL_PATH"], String.Empty);
            fileType = Path.GetExtension(filepath);
            switch (fileType)
            {
                case ".txt":                   
                        CompressText cmp = new CompressText();
                        cmp.compressFile((NormaliseText)Session["norm"]);

                        byte[] compressedfile = SmazSharp.Smaz.Compress(cmp._compressedString);

                        string filename = Server.MapPath("resources/CompressedText.cmx");
                        File.WriteAllBytes(filename, compressedfile);

                        compressedLength = new System.IO.FileInfo(filename).Length;

                        Session["compressedLen"] = compressedLength;

                        calcResults();

                    OpenWindow();
                    break;                   
                case ".jpg":
                case ".JPG":
                    
               ImageCompress  icm = new ImageCompress();                      
           
               icm.compressFile((NormaliseImage)Session["norm"]);     

                byte[] compressedImagefile = SerializeAndCompress(icm._ci);

                string fn = Server.MapPath("resources/CompressedImage.cmi");

                File.WriteAllBytes(fn, compressedImagefile);
                                 
                compressedLength = new System.IO.FileInfo(fn).Length;

                Session["compressedLen"] = compressedLength;
                calcResults();
                OpenWindow();
                break;
            }
        }

        protected void OpenWindow()
        {
            head1.InnerText = "Results";
            head2.Visible = false;
            string redirect = "<script>window.open('download.aspx');</script>";
            Response.Write(redirect);
        }

        private void calcResults()
        {
            long ol = (long)Session["originalLen"];
            long cl = (long)Session["compressedLen"];
            Results r = new Results(ol, cl);
            btnCompress.Visible = false;
            ResultsText.Visible = true;

            ResultsText.InnerHtml = "The results of compresssion is:" + "<br/>" + r.ShowRatio() +"<br/>" + r.ShowSavedData();

        }
       

        //code adapted from https://stackoverflow.com/questions/23651650/is-there-a-way-to-compress-an-object-in-memory-and-use-it-transparently
        private static byte[] SerializeAndCompress(object obj)
        {
            MemoryStream ms = new MemoryStream();
            using (GZipStream zs = new GZipStream(ms, CompressionMode.Compress, true))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(zs, obj);
            }
            return ms.ToArray();
        }
       
    }
}