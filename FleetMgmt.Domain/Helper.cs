using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using FleetMgmt.Dto;
using Newtonsoft.Json;

namespace FleetMgmt.Domain
{
    public static class Helper
    {
         public static string QueryMapper(List<KeyValuePair<string, string>> sourceList, string fileName, bool vesselCallFilterForVoyage = false)
        {

            string result = string.Empty;
            try
            {
                var location = System.Reflection.Assembly.GetEntryAssembly()?.Location;
                var directory = System.IO.Path.GetDirectoryName(location);
                var requestSearchMapper = System.IO.Path.Combine(directory + "//Mapper/", fileName);
                var readDataFile = File.ReadAllText(requestSearchMapper);
                var requestMappedLst = JsonConvert.DeserializeObject<List<FilterMapper>>(readDataFile);

                string[] operators = new string[] { ">", "<", "=" };
                foreach (var item in sourceList)
                {
                    var data = requestMappedLst.FirstOrDefault(x => x.Key.ToLower() == item.Key.ToLower());

                    if (data?.Key != null)
                    {
                        if (vesselCallFilterForVoyage)
                        {
                            if (!data.Key.Contains("voyage."))
                            {
                                continue;
                            }
                            data.Key = data.Key.Replace("voyage.", "");
                        }
                        string value = "";
                        if (data.DataType.Trim() == "string")
                        {
                            value = "\"" + item.Value.Trim() + "\"";
                        }
                        else if (data.DataType.Trim() == "DateTime")
                        {
                            string op = item.Value[0].ToString();
                            value = op + " DateTime.ParseExact(\"" + item.Value.Substring(1) + "\", \"yyyy-MM-ddTHH:mm:ss\", null)";
                        }
                        else
                        {
                            value = item.Value;
                        }

                        if (!string.IsNullOrEmpty(result))
                        {
                            result += " and ";
                        }

                        string t = "";
                        if (data.DataType.Trim() == "string")
                        {
                            t = "\"" + item.Value.Trim() + "\"";
                        }
                        else if (data.DataType.Trim() == "DateTime")
                        {
                            string op = item.Value[0].ToString();
                            value = op + "DateTime.ParseExact(\"" + item.Value.Substring(1) + "\", \"yyyy-MM-ddTHH:mm:ss\", null)";
                        }
                        else
                        {
                            t = "\"" + item.Value.Trim() + "\"";
                        }

                        if (operators.All(x => x != item.Value[0].ToString()))
                        {
                            if (data.DataType.Trim() == "string")
                            {
                                value = ".Contains(" + t + ")";
                            }
                            else if (data.DataType.Trim() == "DateTime")
                            {
                                value = " = DateTime.ParseExact(\"" + item.Value.Substring(1) + "\", \"yyyy-MM-ddTHH:mm:ss\", null)";
                            }
                            else
                            {
                                value = " = " + t;
                            }
                        }

                        result += data.Value.Replace(data.Key, data.Value) + value;
                    }
                }
            }
            catch (Exception)
            {

                return result;
            }

            return result;
        }
    }
}
