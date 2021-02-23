using log4net;
using System;
using System.Collections.Generic;
using System.Text;

namespace JEAPI_WS
{
    public class HTTPConnection
    {

        public static DateTimeFormatter FMT = DateTimeFormat.forPattern("yyyyMMdd'T'HHmmss").withZoneUTC();
        private static ILog logger = LogManager.GetLogger(typeof(HTTPConnection));
        private static long RETRY_DELAY_MS = 5000;
        private static int RETRIES = 3;

        /**
         *
         */
        public static String API_PATH_V1 = "JEWebService/v1/";
        public static String RESOURCE_OBJECTS = "objects";
        public static String RESOURCE_CLASSES = "classes";
        public static String RESOURCE_ATTRIBUTES = "attributes";
        public static String RESOURCE_TYPES = "types";
        public static String RESOURCE_I18N = "i18n";
        private String baseURL;
        private String username;
        private String password;
        private int readTimeout = 120000;//millis

        enum Trust
        {
            ALWAYS, SYSTEM
        }

        private Trust trustmode = Trust.ALWAYS;

        public HTTPConnection(String baseurl, String username, String password, Trust trustMode)
        {
            this.baseURL = baseurl;
            this.username = username;
            this.password = password;
            this.trustmode = trustMode;
            if (trustMode == Trust.ALWAYS)
            {
                logger.Error("Enable trust for self signed certificates");
                HTTPConnection.trustAllCertificates();
            }
        }

        public static StringBuffer getPayload(HttpURLConnection conn)
        {
            BufferedReader in = new BufferedReader(
                new InputStreamReader(conn.getInputStream()));
        String inputLine;
        StringBuffer response = new StringBuffer();

        while ((inputLine : in.readLine()) != null) {
            response.append(inputLine);
        }
        in.close();
//            logger.trace("Payload: {}", response);

        return response;
    }

/**
 * Its not save to trust all ssl certificats. Better use trusted keys but for now its better than simple http
 */
public static void trustAllCertificates()
//{
//    try
//    {
//        TrustManager[] trustAllCerts = new TrustManager[]{
//                    new X509TrustManager() {
//                        @Override
//                        public X509Certificate[] getAcceptedIssuers()
//        {
//            return new X509Certificate[0];
//        }

//        @Override
//                        public void checkClientTrusted(X509Certificate[] certs, String authType)
//        {
//        }

//        @Override
//                        public void checkServerTrusted(X509Certificate[] certs, String authType)
//        {
//        }
//    }
//            };

//SSLContext sc = SSLContext.getInstance("SSL");
//sc.init(null, trustAllCerts, new SecureRandom());
//HttpsURLConnection.setDefaultSSLSocketFactory(sc.getSocketFactory());
//HttpsURLConnection.setDefaultHostnameVerifier(new HostnameVerifier() {
//                @Override
//                public boolean verify(String arg0, SSLSession arg1)
//{
//    return true;
//}
//            });
//        } catch (Exception e)
//{
//}
    }

    private void addAuth(Http conn, String username, String password)
{
    String auth = new String(Base64.encodeBase64((username + ":" + password).getBytes()));

    //        logger.info("Using auth: 'Authorization Basic " + auth);
    conn.setRequestProperty("Authorization", "Basic " + auth);
}

/**
 * TODO: this function need an rework. The error handling a retry function are suboptimal
 *
 * @param resource
 * @return
 * @throws IOException
 * @throws InterruptedException
 */
public InputStream getInputStreamRequest(String resource) {
        int retry = 0;
boolean delay = false;
do
{
    if (delay)
    {
        Thread.sleep(RETRY_DELAY_MS);
    }
    //        Date start = new Date();
    //replace spaces
    resource = resource.replaceAll("\\s+", "%20");
    //        logger.trace("after replcae: {}", resource);
    URL url = new URL(this.baseURL + "/" + resource);

    HttpURLConnection conn = (HttpURLConnection)url.openConnection();
    conn.setRequestMethod("GET");
    conn.setRequestProperty("Content-Type", "application/json; charset=UTF-8");
    conn.setRequestProperty("Accept-Charset", "UTF-8");
    conn.setRequestProperty("Accept-Encoding", "gzip");
    conn.setReadTimeout(this.readTimeout);
    addAuth(conn, this.username, this.password);

    conn.setRequestProperty("User-Agent", "JEAPI-WS");

    logger.debug("HTTP request {}", conn.getURL());

    switch (conn.getResponseCode())
    {
        case HttpURLConnection.HTTP_NOT_FOUND:
        case HttpURLConnection.HTTP_FORBIDDEN:
            return null;
        case HttpURLConnection.HTTP_OK:
            if ("gzip".equals(conn.getContentEncoding()))
            {
                return new GZIPInputStream(conn.getInputStream());
            }
            else
            {
                return conn.getInputStream();
            }
        case HttpURLConnection.HTTP_GATEWAY_TIMEOUT:
            logger.warn(url + " **gateway timeout**");
            break;
        case HttpURLConnection.HTTP_UNAVAILABLE:
            logger.warn(url + "**unavailable**");
            break;
        default:
            logger.warn(url + " **{} : unknown response code**.", conn.getResponseCode());
            break;
    }

    conn.disconnect();

    retry++;
    logger.error("Failed retry {} for '{}'", retry, resource);
    delay = true;

} while (retry < RETRIES);

logger.fatal("Aborting download of input stream. '{}'", resource);
return null;
    }

    public BufferedImage getIconRequest(String resource)
{
    Date start = new Date();
//replace spaces
resource = resource.replaceAll("\\s+", "%20");
//        logger.trace("after replcae: {}", resource);
URL url = new URL(this.baseURL + "/" + resource);

HttpURLConnection conn = (HttpURLConnection)url.openConnection();
conn.setRequestMethod("GET");
addAuth(conn, this.username, this.password);

conn.setRequestProperty("User-Agent", "JEAPI-WS");

logger.debug("HTTP request {}", conn.getURL());

int responseCode = conn.getResponseCode();

//        Gson gson2 = new GsonBuilder().setPrettyPrinting().create();
//        logger.trace("resonseCode {}", responseCode);
if (responseCode == HttpURLConnection.HTTP_OK)
{

    BufferedInputStream in = new BufferedInputStream(conn.getInputStream());

    BufferedImage imBuff = ImageIO.read(conn.getInputStream());

    conn.disconnect();
            in.close();


    logger.trace("HTTP request closed after: " + ((new Date()).getTime() - start.getTime()) + " msec");
    return imBuff;
    //            return new BufferedImage(10, 10, BufferedImage.TYPE_BYTE_GRAY);
}
else
{
    return null;
}

    }

    public byte[] getByteRequest(String resource)
{
    //replace spaces
    resource = resource.replaceAll("\\s+", "%20");
    //        logger.trace("after replcae: {}", resource);
    URL url = new URL(this.baseURL + "/" + resource);

HttpURLConnection conn = (HttpURLConnection)url.openConnection();
conn.setRequestMethod("GET");
addAuth(conn, this.username, this.password);

conn.setRequestProperty("User-Agent", "JEAPI-WS");

logger.debug("HTTP request {}", conn.getURL());

int responseCode = conn.getResponseCode();


//        Gson gson2 = new GsonBuilder().setPrettyPrinting().create();
logger.trace("responseCode {}", responseCode);

if (responseCode == HttpURLConnection.HTTP_OK)
{

    //            JEVisFile jf = new JEVisFileImp("tmp.file", bytes);//filename comes from the samples
    InputStream inputStream = conn.getInputStream();
    byte[] response = IOUtils.toByteArray(inputStream);
    inputStream.close();

    return response;

}
else
{
    return null;
}

    }

    public StringBuffer postRequest(String resource, String json), JEVisException {
        Date start = new Date();
//replace spaces
resource = resource.replaceAll("\\s+", "%20");
//        logger.trace("after replcae: {}", resource);
URL url = new URL(this.baseURL + "/" + resource);

HttpURLConnection con = (HttpURLConnection)url.openConnection();
con.setRequestMethod("POST");
con.setRequestProperty("Content-Type", "application/json; charset=UTF-8");
//        con.setRequestProperty("Content-Type", "application/json");
con.setRequestProperty("Accept", "application/json");
con.setRequestProperty("User-Agent", "JEAPI-WS");
con.setRequestProperty("Accept-Charset", "UTF-8");
con.setRequestProperty("Accept-Encoding", "gzip");
con.setDoOutput(true);
con.setDoInput(true);
addAuth(con, this.username, this.password);

logger.debug("HTTP POST request {}", con.getURL());
con.connect();

DataOutputStream wr = new DataOutputStream(con.getOutputStream());
BufferedWriter writer = new BufferedWriter(new OutputStreamWriter(wr, StandardCharsets.UTF_8));
writer.write(json);
writer.close();
wr.close();

int responseCode = con.getResponseCode();
logger.debug("Post status: {}", responseCode);

//        Gson gson2 = new GsonBuilder().setPrettyPrinting().create();
//        logger.trace("resonseCode {}", responseCode);
if (responseCode == HttpURLConnection.HTTP_OK || responseCode == HttpURLConnection.HTTP_CREATED)
{

    InputStreamReader streamReader = null;
    if ("gzip".equals(con.getContentEncoding()))
    {
        streamReader = new InputStreamReader(new GZIPInputStream(con.getInputStream()), StandardCharsets.UTF_8);
    }
    else
    {
        streamReader = new InputStreamReader(con.getInputStream(), StandardCharsets.UTF_8);
    }

    BufferedReader in = new BufferedReader(streamReader);
    // BufferedReader in = new BufferedReader(
    //         new InputStreamReader(con.getInputStream(), StandardCharsets.UTF_8));
    String inputLine;
    StringBuffer response = new StringBuffer();

    while ((inputLine = in.readLine()) != null) {
        response.append(inputLine);
    }
            in.close();
    logger.trace("response.Payload: {}", response);

    //            try (PrintWriter out = new PrintWriter("/tmp/" + resource.replaceAll("\\/", "") + ".json")) {
    //                out.println(response.toString());
    //            }
    logger.trace("HTTP request closed after: " + ((new Date()).getTime() - start.getTime()) + " msec");
    return response;
}
else
{
    logger.error("Error getResponseCode: {} for '{}'", responseCode, resource);
    throw new JEVisException("[" + responseCode + "] ", responseCode);

    //            return null;
}

    }

    public StringBuffer getRequest(String resource)
{
    Date start = new Date();
//replace spaces
resource = resource.replaceAll("\\s+", "%20");


URL url = new URL(this.baseURL + resource);
HttpURLConnection con = (HttpURLConnection)url.openConnection();
con.setRequestMethod("GET");
con.setDoOutput(true);

if (con.getResponseCode() != HttpURLConnection.HTTP_OK)
{
    logger.debug("Code is not OK return null");
    return null;
}
else
{
    BufferedReader in = new BufferedReader(
            new InputStreamReader(con.getInputStream(), StandardCharsets.UTF_8));
    String inputLine;
    StringBuffer response = new StringBuffer();

    /**
     * this is producing a out of memory exception in some cases
     */
    while ((inputLine = in.readLine()) != null) {
        response.append(inputLine);
    }
            in.close();
    logger.trace("HTTP request closed after: " + ((new Date()).getTime() - start.getTime()) + " msec");

    return response;
}
    }

    /**
     * @param resource
     * @return
     * @throws MalformedURLException
     * @throws ProtocolException
     * @throws IOException
     * @TODO this is not a generic post Connection like the name implies
     */
    public HttpURLConnection getPostFileConnection(String resource) throws MalformedURLException, ProtocolException, IOException {
        Date start = new Date();
//replace spaces
resource = resource.replaceAll("\\s+", "%20");
//        logger.trace("after replcae: {}", resource);
URL url = new URL(this.baseURL + "/" + resource);

HttpURLConnection conn = (HttpURLConnection)url.openConnection();
conn.setRequestMethod("POST");
conn.setRequestProperty("Content-Type", "application/octet-stream");
conn.setRequestProperty("User-Agent", "JEAPI-WS");

//        con.setRequestProperty("Accept-Encoding", "gzip");
conn.setDoOutput(true);
//        conn.setDoInput(true);
addAuth(conn, this.username, this.password);

conn.setRequestProperty("User-Agent", "JEAPI-WS");

logger.debug("HTTP request {}", conn.getURL());

//        int responseCode = conn.getResponseCode();
//        logger.trace("resonseCode {}", responseCode);
return conn;

    }

    /**
     * @param resource
     * @return
     * @throws MalformedURLException
     * @throws ProtocolException
     * @throws IOException
     * @TODO this is not a generic post Connection like the name implies
     */
    public HttpURLConnection getPostIconConnection(String resource) throws MalformedURLException, ProtocolException, IOException {
        Date start = new Date();
//replace spaces
resource = resource.replaceAll("\\s+", "%20");
//        logger.trace("after replcae: {}", resource);
URL url = new URL(this.baseURL + "/" + resource);

HttpURLConnection conn = (HttpURLConnection)url.openConnection();
conn.setRequestMethod("POST");
conn.setRequestProperty("Content-Type", "image/png");
conn.setRequestProperty("User-Agent", "JEAPI-WS");

//        con.setRequestProperty("Accept-Encoding", "gzip");
conn.setDoOutput(true);
//        conn.setDoInput(true);
addAuth(conn, this.username, this.password);

conn.setRequestProperty("User-Agent", "JEAPI-WS");

logger.debug("HTTP request {}", conn.getURL());

//        int responseCode = conn.getResponseCode();
//        logger.trace("resonseCode {}", responseCode);
return conn;

    }

    public HttpURLConnection getGetConnection(String resource)
{
    Date start = new Date();
//replace spaces
resource = resource.replaceAll("\\s+", "%20");
//        logger.trace("after replcae: {}", resource);
URL url = new URL(this.baseURL + "/" + resource);

HttpURLConnection conn = (HttpURLConnection)url.openConnection();
conn.setRequestMethod("GET");
addAuth(conn, this.username, this.password);

conn.setRequestProperty("User-Agent", "JEAPI-WS");
conn.setRequestProperty("Accept-Encoding", "gzip");

logger.debug("HTTP request {}", conn.getURL());

int responseCode = conn.getResponseCode();
logger.trace("responseCode {}", responseCode);
return conn;

    }

    public HttpURLConnection getDeleteConnection(String resource)
{
    Date start = new Date();
//replace spaces
resource = resource.replaceAll("\\s+", "%20");
//        logger.trace("after replcae: {}", resource);
URL url = new URL(this.baseURL + "/" + resource);

HttpURLConnection conn = (HttpURLConnection)url.openConnection();
conn.setRequestMethod("DELETE");
addAuth(conn, this.username, this.password);

conn.setRequestProperty("User-Agent", "JEAPI-WS");

logger.debug("HTTP DELETE request {}", conn.getURL());

int responseCode = conn.getResponseCode();
logger.trace("resonseCode {}", responseCode);
return conn;

    }
}
