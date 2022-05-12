using System.Security;

namespace Navyblue.Consul.transport
{
    /// <summary>
	/// Default HTTPS client This class is thread safe
	/// </summary>
	public sealed class DefaultHttpsTransport : AbstractHttpTransport
    {
        private readonly HttpClient httpClient;

        public DefaultHttpsTransport(TLSConfig tlsConfig)
        {
            try
            {
                KeyStore clientStore = KeyStore.getInstance(tlsConfig.getKeyStoreInstanceType().ToString());
                clientStore.load(new FileStream(tlsConfig.CertificatePath, FileMode.Open, FileAccess.Read), tlsConfig.CertificatePassword.ToCharArray());

                KeyManagerFactory kmf = KeyManagerFactory.getInstance(KeyManagerFactory.DefaultAlgorithm);
                kmf.init(clientStore, tlsConfig.CertificatePassword.ToCharArray());
                KeyManager[] kms = kmf.KeyManagers;

                KeyStore trustStore = KeyStore.getInstance(TLSConfig.KeyStoreInstanceType.JKS.ToString());
                trustStore.load(new FileStream(tlsConfig.KeyStorePath, FileMode.Open, FileAccess.Read), tlsConfig.KeyStorePassword.ToCharArray());

                TrustManagerFactory tmf = TrustManagerFactory.getInstance(TrustManagerFactory.DefaultAlgorithm);
                tmf.init(trustStore);
                TrustManager[] tms = tmf.TrustManagers;

                SSLContext sslContext = SSLContexts.custom().loadTrustMaterial(new TrustSelfSignedStrategy()).build();
                sslContext.init(kms, tms, new SecureRandom());
                SSLConnectionSocketFactory factory = new SSLConnectionSocketFactory(sslContext);

                Registry<ConnectionSocketFactory> registry = RegistryBuilder.create<ConnectionSocketFactory>().register("https", factory).build();

                PoolingHttpClientConnectionManager connectionManager = new PoolingHttpClientConnectionManager(registry);
                connectionManager.MaxTotal = DEFAULT_MAX_CONNECTIONS;
                connectionManager.DefaultMaxPerRoute = DEFAULT_MAX_PER_ROUTE_CONNECTIONS;

                RequestConfig requestConfig = RequestConfig.custom().setConnectTimeout(DEFAULT_CONNECTION_TIMEOUT).setConnectionRequestTimeout(DEFAULT_CONNECTION_TIMEOUT).setSocketTimeout(DEFAULT_READ_TIMEOUT).build();

                HttpClientBuilder httpClientBuilder = HttpClientBuilder.create().setConnectionManager(connectionManager).setDefaultRequestConfig(requestConfig);

                httpClient = httpClientBuilder.build();
            }
            catch (SecurityException e)
            {
                throw new TransportException(e);
            }
            catch (IOException e)
            {
                throw new TransportException(e);
            }
        }

        public DefaultHttpsTransport(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        //protected internal override HttpClient HttpClient
        //{
        //	get
        //	{
        //		return httpClient;
        //	}
        //}
    }
}