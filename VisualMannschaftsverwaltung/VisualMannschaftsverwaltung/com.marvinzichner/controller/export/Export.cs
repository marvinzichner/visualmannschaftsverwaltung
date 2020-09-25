//Name          Marvin Zichner
//Datum         06.02.2020
//Datei         Person.cs
//Aenderungen   Initales Erzeugen und erste Eigenschaften

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace VisualMannschaftsverwaltung
{
    public class Export
    {
        #region Eigenschaften
        private XmlSerializer xmlSerializer;
        private StreamWriter streamWriter;
        private string _path;
        private string _filename;
        #endregion

        #region Accessoren / Modifier
        public XmlSerializer XmlSerializer { get => xmlSerializer; set => xmlSerializer = value; }
        public StreamWriter StreamWriter { get => streamWriter; set => streamWriter = value; }
        public string Path { get => _path; set => _path = value; }
        public string Filename { get => _filename; set => _filename = value; }
        #endregion

        #region Konstruktoren
        public Export()
        {
            
        }
        #endregion

        #region Worker
        public void configure(string path, string filename)
        {
            this.Path = path;
            this.Filename = filename;
        }
        public void doXmlExport(object exportTarget, Type[] types)
        {
            try { 
                XmlSerializer = new XmlSerializer(exportTarget.GetType(), types);
                StreamWriter = new StreamWriter(
                    new FileStream(Path, FileMode.Create), System.Text.Encoding.UTF8);
                XmlSerializer.Serialize(StreamWriter, exportTarget);
                StreamWriter.Close();
            } 
            catch (Exception)
            {

            }
        }

        public void doDownload()
        {
            try { 
                System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
                byte[] Content = File.ReadAllBytes(Path);
                response.ContentType = "text/xml";
                response.AddHeader("content-disposition", "attachment; filename=" + Filename);
                response.BufferOutput = true;
                response.OutputStream.Write(Content, 0, Content.Length);
                response.End();
            }
            catch (Exception)
            {

            }
        }
        #endregion
    }
}
