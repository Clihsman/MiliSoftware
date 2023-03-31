﻿using MiliSoftware.Model.WebServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MiliSoftware.WebServices
{
    public class WebPostService
    {
        private HttpWebRequest webRequest;
        private readonly string uri;

        public WebPostService(string uri)
        {
            this.uri = uri;
            webRequest = (HttpWebRequest)WebRequest.Create(new Uri(uri));
            webRequest.Method = "post";
        }

        public void SetCredentials(string username, string password)
        {
            byte[] data = Encoding.GetEncoding("ISO-8859-1").GetBytes(username + ":" + password);
            string encoded = Convert.ToBase64String(data);
            webRequest.Headers.Add("Authorization", "Basic " + encoded);
        }

        public string PostString(string data)
        {
            string result = "";

            using (StreamWriter writer = new StreamWriter(webRequest.GetRequestStream()))
            {
                writer.Write(data);
                writer.Flush();
                writer.Close();
            }

            WebResponse webResponse = webRequest.GetResponse();

            using (StreamReader reader = new StreamReader(webResponse.GetResponseStream()))
            {
                result = reader.ReadToEnd().Trim();
            }

            return result;
        }

        public string PostJson(string json)
        {
            string message = null;
            int statusCode = 404;
            webRequest.ContentType = "application/json;charset=UTF-8";
            HttpWebResponse webResponse = null;

            try
            {
                using (StreamWriter writer = new StreamWriter(webRequest.GetRequestStream()))
                {
                    writer.Write(json);
                    writer.Flush();
                    writer.Close();
                }

                try
                {
                    webResponse = (HttpWebResponse)webRequest.GetResponse();
                    statusCode = (int)webResponse.StatusCode;
                    message = GetString(webResponse);
                }
                catch (WebException ex)
                {
                    if (ex.Status == WebExceptionStatus.ProtocolError)
                    {
                        webResponse = ex.Response as HttpWebResponse;
                        if (webResponse != null)
                        {
                            message = GetString(webResponse);
                            statusCode = (int)webResponse.StatusCode;
                        }
                        else
                        {
                            statusCode = 404;
                        }
                    }
                    else
                    {
                        statusCode = 404;
                    }
                }
                catch (ProtocolViolationException ex)
                {
                    message = ex.ToString();
                }
            }
            catch {

            }

            if (webResponse != null) webResponse.Dispose();

            return GetJsonToObject(new {
                statusCode,
                message
            });
        }

        public string PostJson(object data)
        {
            return PostJson(Newtonsoft.Json.JsonConvert.SerializeObject(data));
        }

        public string PostJson(string[] data)
        {
            string result = "";
            webRequest.ContentType = "application/json;charset=UTF-8";

            using (StreamWriter writer = new StreamWriter(webRequest.GetRequestStream()))
            {
                writer.Write(data);
                writer.Flush();
                writer.Close();
            }

            WebResponse webResponse = webRequest.GetResponse();

            using (StreamReader reader = new StreamReader(webResponse.GetResponseStream()))
            {
                result = reader.ReadToEnd().Trim();
            }

            return result;
        }

        public string PostStream(Stream stream,string name, WebFileFormat format)
        {
            string result = "";

            SetFormat(format);
            webRequest.Headers.Add("FileName", name);

            using (Stream requestStream = webRequest.GetRequestStream())
            {
                stream.CopyTo(requestStream);
                requestStream.Flush();
                requestStream.Close();
            }

            WebResponse webResponse = webRequest.GetResponse();

            using (StreamReader reader = new StreamReader(webResponse.GetResponseStream()))
            {
                result = reader.ReadToEnd().Trim();
            }

            return result;
        }

        public string PostStream(Stream stream, string name, string format)
        {
            string result = "";
            SetFormat(WebFileFormat.OTHER);

            using (Stream requestStream = webRequest.GetRequestStream())
            {
                stream.CopyTo(requestStream);
                requestStream.Flush();
                requestStream.Close();
            }

            WebResponse webResponse = webRequest.GetResponse();

            using (StreamReader reader = new StreamReader(webResponse.GetResponseStream()))
            {
                result = reader.ReadToEnd().Trim();
            }

            return result;
        }

        public string PostData(byte[] buffer, string name, WebFileFormat format)
        {
            string result = "";
            SetFormat(format);

            using (Stream requestStream = webRequest.GetRequestStream())
            {
                requestStream.Write(buffer, 0, buffer.Length);
                requestStream.Flush();
                requestStream.Close();
            }

            WebResponse webResponse = webRequest.GetResponse();

            using (StreamReader reader = new StreamReader(webResponse.GetResponseStream()))
            {
                result = reader.ReadToEnd().Trim();
            }

            return result;
        }

        public string PostData(byte[] buffer, string name, string format)
        {
            string result = "";
            SetFormat(WebFileFormat.OTHER);

            using (Stream requestStream = webRequest.GetRequestStream())
            {
                requestStream.Write(buffer,0, buffer.Length);
                requestStream.Flush();
                requestStream.Close();
            }

            WebResponse webResponse = webRequest.GetResponse();

            using (StreamReader reader = new StreamReader(webResponse.GetResponseStream()))
            {
                result = reader.ReadToEnd().Trim();
            }

            return result;
        }

        private void SetFormat(WebFileFormat format)
        {
            switch (format){
                case WebFileFormat.PNG:
                    webRequest.ContentType = "image/png";
                    break;
                case WebFileFormat.PDF:
                    webRequest.ContentType = "application/pdf";
                    break;
                case WebFileFormat.XML:
                    webRequest.ContentType = "application/xml";
                    break;
                case WebFileFormat.JSON:
                    webRequest.ContentType = "application/json;charset=UTF-8";
                    break;
                case WebFileFormat.DOCX:
                    webRequest.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    break;
                case WebFileFormat.XLSX:
                    webRequest.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    break;
                case WebFileFormat.OTHER:
                    webRequest.ContentType = "";
                    break;
            }
        }

        public void Dispose() {
          
        }

        #region Utils

        private string GetString(WebResponse response) {
            string message = "";

            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                message = reader.ReadToEnd().Trim();
            }

            return message;
        }

        private string GetJsonToObject(object obj) {
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }

        #endregion
    }
}