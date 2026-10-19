// Copyright (c) 2026 CODE Éxitos. All rights reserved.
// Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Runtime.Serialization;
using System.IO;
using System.Net;

using Microsoft.Http;
using FltRestService.Model;

namespace FltRestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            PostNewFlightXML();
        }

        private static void GetAllPrograms()
        {
            using (HttpClient client = new HttpClient("http://localhost:8080/api/flt/"))
            {
                SetAuthorizationHeader(client);

                Console.WriteLine("All programs in FLT");

                using (HttpResponseMessage responseMessage = client.Get("programs"))
                {
                    responseMessage.EnsureStatusIsSuccessful();
                    Console.WriteLine(responseMessage.Content.ReadAsXElement());
                }
                Console.ReadKey();
            }
        }

        private static void SetAuthorizationHeader(HttpClient client)
        {
            client.DefaultHeaders.Authorization = Microsoft.Http.Headers.Credential.CreateBasic("test", "REDACTED");
        }

        private static void GetAllLocationsForProgram13()
        {
            using (HttpClient client = new HttpClient("http://localhost:8080/api/flt/"))
            {
                SetAuthorizationHeader(client);

                Console.WriteLine("All locations for Program 13");

                using (HttpResponseMessage responseMessage = client.Get("programs/13/locations"))
                {
                    responseMessage.EnsureStatusIsSuccessful();
                    Console.WriteLine(responseMessage.Content.ReadAsXElement());
                }
                Console.ReadKey();
            }
        }

        private static void GetAllIssueCodesForProgram14()
        {
            using (HttpClient client = new HttpClient("http://localhost:8080/api/flt/"))
            {
                SetAuthorizationHeader(client);

                Console.WriteLine("All issue codes for Program 14");

                using (HttpResponseMessage responseMessage = client.Get("programs/14/commentissuecodes"))
                {
                    responseMessage.EnsureStatusIsSuccessful();
                    Console.WriteLine(responseMessage.Content.ReadAsXElement());
                }
                Console.ReadKey();
            }
        }

        private static void PostNewFlightXML()
        {
            Console.WriteLine("POST new Flight XML");
            Console.WriteLine();


            string xmlString = "";
            using (StreamReader sr = new StreamReader("C:\\FltRestService\\FLTRestClient\\flight.xml"))
            {
                xmlString = sr.ReadToEnd();
            }

            ASCIIEncoding asciiEncoding = new ASCIIEncoding();
            WebRequest webRequest = WebRequest.Create("http://localhost:8080/api/flt/programs/14/locations/49/fleet/225/flights");
            webRequest.Method = "POST";
            webRequest.Headers.Add(HttpRequestHeader.Authorization, "Basic " + Convert.ToBase64String(asciiEncoding.GetBytes("test:REDACTED")));
            Byte[] byteArray = Encoding.UTF8.GetBytes(xmlString);
            webRequest.ContentType = "application/xml;charset=UTF-8";
            webRequest.ContentLength = byteArray.Length;
            Stream webRequestStream = webRequest.GetRequestStream();
            webRequestStream.Write(byteArray, 0, byteArray.Length);
            webRequestStream.Close();

            String responseFromServer = "";
            try
            {
                using (WebResponse webResponse = webRequest.GetResponse())
                {
                    HttpWebResponse httpWebResponse = webResponse as HttpWebResponse;
                    string responseCode = httpWebResponse.StatusCode.ToString();
                    string responseDescription = httpWebResponse.StatusDescription;
                    switch (responseCode)
                    {
                        case "OK":
                            Console.WriteLine("Call Succeeded");
                            Console.WriteLine();
                            Stream responseStream = webResponse.GetResponseStream();
                            StreamReader sr = new StreamReader(responseStream);
                            responseFromServer = sr.ReadToEnd();
                            responseStream.Close();

                            break;
                        default:
                            Console.WriteLine("Call Failed - Status = " + responseCode);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }

        private static void PostTest()
        {
            Console.WriteLine("POST Test");
            Console.WriteLine();

            string xmlString = "<TestString xmlns=\"http://schemas.datacontract.org/2004/07/FltRestService.Model\"><Value1>This is my test value</Value1><bogus>this is my bogus string</bogus><SubValues><SubValue1>sub value 1</SubValue1></SubValues></TestString>";

            ASCIIEncoding asciiEncoding = new ASCIIEncoding();
            WebRequest webRequest = WebRequest.Create("http://localhost:8080/api/flt/posttest");
            webRequest.Method = "POST";
            webRequest.Headers.Add(HttpRequestHeader.Authorization, "Basic " + Convert.ToBase64String(asciiEncoding.GetBytes("test:REDACTED")));
            Byte[] byteArray = Encoding.UTF8.GetBytes(xmlString);
            webRequest.ContentType = "application/xml;charset=UTF-8";
            webRequest.ContentLength = byteArray.Length;
            Stream webRequestStream = webRequest.GetRequestStream();
            webRequestStream.Write(byteArray, 0, byteArray.Length);
            webRequestStream.Close();

            String responseFromServer = "";
            try
            {
                using (WebResponse webResponse = webRequest.GetResponse())
                {
                    HttpWebResponse httpWebResponse = webResponse as HttpWebResponse;
                    string responseCode = httpWebResponse.StatusCode.ToString();
                    string responseDescription = httpWebResponse.StatusDescription;
                    switch (responseCode)
                    {
                        case "OK":
                            Console.WriteLine("Call Succeeded");
                            Console.WriteLine();
                            Stream responseStream = webResponse.GetResponseStream();
                            StreamReader sr = new StreamReader(responseStream);
                            responseFromServer = sr.ReadToEnd();
                            responseStream.Close();
                            Console.WriteLine(responseFromServer);
                            break;
                        default:
                            Console.WriteLine("Call Failed - Status = " + responseCode);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }

        private static void PostNewFlight()
        {
            using (HttpClient client = new HttpClient("http://localhost:8080/api/flt/"))
            {
                SetAuthorizationHeader(client);

                Console.WriteLine("POST new Flight");
                Console.WriteLine();
                string updateUri = "programs/13/locations/44/fleet/226/flights";
                FltRestService.Model.Flight flight = new Flight();

                flight.HobbsEnd = 394155.8m;
                flight.HobbsStart = 391955.8m;
                flight.DepartureTime = "8:00 AM";
                flight.BallastId = 4;
                flight.FlownRouteId = 211;
                flight.EngineHoursEnd = 0;
                flight.EngineHoursStart = 0;
                flight.EvaluatorId = 4;
                flight.NacelleCheckBuildUp = 1;
                flight.NacelleCheckDiscoloration = 1;
                flight.LocationId = 44;
                flight.MeterUnitId = 25;
                flight.OilLevelIndicatorStart = 4;
                flight.PostFlightFtiOK = 1;
                flight.PostFlightTelemetryCheckComplete = 1;
                flight.PreFlightFtiOK = 1;
                flight.PreFlightTelemetryCheckComplete = 1;
                flight.PreFlightTelemetryOK = 1;
                flight.ReturnTime = "11:00 AM";
                flight.SchedRouteId = 211;
                flight.AirframeId = 74;
                flight.Cargo1Id = 1;
                flight.Cargo2Id = 2;
                flight.FlightDate = "7/1/2012";
                flight.HydraulicLevelIndicatorStart = 4;
                flight.AircraftId = 187;
                flight.WorkShiftIndicatorId = 14;

                FltRestService.Model.RegionTime regionTime = new RegionTime();
                regionTime.RegionId = 1;
                regionTime.Hours = 500m;

                flight.RegionTimes.Add(regionTime);

                HttpContent content = HttpContentExtensions.CreateDataContract<FltRestService.Model.Flight>(flight);

                using (HttpResponseMessage response = client.Post(updateUri, content))
                {
                    response.EnsureStatusIsSuccessful();
                    Console.Write("Web Service Response StatusCode = {0}", response.StatusCode.ToString());
                }
                Console.ReadKey();
            }
        }
    }
}
