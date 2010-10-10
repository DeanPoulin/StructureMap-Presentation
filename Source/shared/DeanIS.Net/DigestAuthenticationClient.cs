using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using DeanIS.Net.Utilities;

namespace DeanIS.Net
{
    public class DigestAuthenticationClient : IDigestAuthenticationClient
    {
        private string _userName;
        private string _password;
        private string _realm;
        private string _nonce;
        private string _algorithm;
        private string _opaque;
        private string _qop;
        private string _cnonce;
        private int _nonceCount;
        private bool _authenticated;

        private HttpWebRequest _httpWebRequest;

        public string GetResponseString(string username, string password, Uri uri)
        {
            _userName = username;
            _password = password;

            _httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);

            if (_authenticated)
            {
                _nonceCount++;
                _cnonce = GenerateNewCNonce();

                _httpWebRequest.Headers[HttpRequestHeader.Authorization] = GetAuthorizationHeader(uri);
            }
            HttpWebResponse response = null;

            try
            {
                response = _httpWebRequest.GetResponse() as HttpWebResponse;
            }
            catch (WebException ex)
            {
                var unauthorizedResponse = ((HttpWebResponse) ex.Response);
                if (unauthorizedResponse.StatusCode == HttpStatusCode.Unauthorized)
                {
                    _authenticated = false;

                    var wwwAuthValue = (from object key in unauthorizedResponse.Headers
                                        where (string)key == "WWW-Authenticate"
                                        select unauthorizedResponse.Headers[(string)key]).FirstOrDefault();

                    if (string.IsNullOrEmpty(wwwAuthValue))
                    {
                        return string.Empty;
                    }

                    var authKeys = BuildParameterDictionary(wwwAuthValue);

                    _realm = authKeys["realm"];
                    _nonce = authKeys["nonce"];
                    _opaque = authKeys["opaque"];
                    _algorithm = authKeys["algorithm"];
                    _qop = authKeys["qop"];
                    _cnonce = GenerateNewCNonce();
                    _nonceCount = 1;

                    var authenticatedRequest = WebRequest.Create(_httpWebRequest.RequestUri) as HttpWebRequest;

                    if (authenticatedRequest == null) return string.Empty;

                    authenticatedRequest.Headers[HttpRequestHeader.Authorization] =
                        GetAuthorizationHeader(_httpWebRequest.RequestUri);

                    try
                    {
                        var authenticatedResponse = authenticatedRequest.GetResponse() as HttpWebResponse;
                        return GetResponse(authenticatedResponse);
                    }
                    catch (Exception ex2)
                    {
                        return string.Empty;
                    }
                }
            }

            if (response == null) return string.Empty;

            return response.StatusCode == HttpStatusCode.OK ? GetResponse(response) : string.Empty;

        }

        private string GetResponse(HttpWebResponse response)
        {
            if (response == null) throw new ArgumentNullException("response");

            var responseStream = response.GetResponseStream();

            if (responseStream == null) return string.Empty;

            _authenticated = true;

            using (var streamReader = new StreamReader(responseStream))
            {
                return streamReader.ReadToEnd();
            }
        }

        private string GetAuthorizationHeader(Uri uri)
        {
            var localPathAndQuery = GetLocalPathAndQuery(uri);

            var ha1 = string.Format("{0}:{1}:{2}", _userName, _realm, _password).ToMD5Hash();
            var ha2 = string.Format("{0}:{1}", "GET", localPathAndQuery).ToMD5Hash();
            var authResponse = string.Format("{0}:{1}:{2:D8}:{3}:{4}:{5}", ha1, _nonce, _nonceCount, _cnonce, _qop, ha2).ToMD5Hash();

            return "Digest username=\"" + _userName + "\"," +
                " realm=\"" + _realm + "\"," +
                " nonce=\"" + _nonce + "\"," +
                " uri=\"" + localPathAndQuery + "\"," +
                " algorithm=" + _algorithm + "," +
                " qop=" + _qop + "," +
                " nc=" + _nonceCount.ToString("D8") + "," +
                " cnonce=\"" + _cnonce + "\"," +
                " response=\"" + authResponse + "\"," +
                " opaque=\"" + _opaque + "\"";
        }

        private static string GetLocalPathAndQuery(Uri uri)
        {
            return uri.LocalPath + uri.Query;
        }

        private static readonly Regex RegexNameValuePair = new Regex(@"(?<key>\w+)\s*=\s*((?<quote>\"")(?<value>[^\""]*)(\k<quote>)|(?<value>[^,]*))((,\s*)?)",
                                       RegexOptions.Compiled | RegexOptions.ExplicitCapture | RegexOptions.Singleline | RegexOptions.IgnoreCase);
        private static Dictionary<string, string> BuildParameterDictionary(string header)
        {
            header = header.Substring(7);

            var matches = RegexNameValuePair.Matches(header);

            return matches.Cast<Match>().ToDictionary(m => m.Groups["key"].Value, m => m.Groups["value"].Value);
        }

        private static string GenerateNewCNonce()
        {
            return Guid.NewGuid().ToString().Replace("-", string.Empty);
        }
    }
}
