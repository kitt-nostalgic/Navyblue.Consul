using System;
using System.Net.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Navyblue.Consul;
using NUnit.Framework.Internal;

namespace TestProject1;

[TestClass]
public class ConsulRawClientTest
{
    private const String ENDPOINT = "/v1/agent/host";
    private readonly QueryParams? EMPTY_QUERY_PARAMS = QueryParams.Builder.builder().build();
    private const String HOST = "localhost";
    private const int PORT = 8500;
    private const String PATH = "path";
    private readonly String EXPECTED_AGENT_ADDRESS_NO_PATH = "http://" + HOST + ":" + PORT + ENDPOINT;
    private readonly String EXPECTED_AGENT_ADDRESS = "http://" + HOST + ":" + PORT + "/" + PATH + ENDPOINT;

    [TestMethod]
    public void VerifyDefaultUrl()
    {
        //// Given
        //ConsulRawClient client = new ConsulRawClient(HOST,PORT);

        //// When
        //var result =  client.MakeGetRequest(ENDPOINT, EMPTY_QUERY_PARAMS);

        //// Then
        //Assert.AreEqual(200,result.StatusCode);
        //Assert.AreNotEqual("", result.StatusMessage);
    }
}