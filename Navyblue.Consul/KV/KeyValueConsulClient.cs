using Navyblue.BaseLibrary;
using Navyblue.Consul.KV.Model;
using Navyblue.Consul.Transport;

namespace Navyblue.Consul.KV;

/// <summary>
///
/// </summary>
public sealed class KeyValueConsulClient : IKeyValueClient
{
    private readonly IConsulRawClient _consulRawClient;

    public KeyValueConsulClient(IConsulRawClient consulRawClient)
    {
        _consulRawClient = consulRawClient;
    }

    public ConsulResponse<GetValue> GetKvValue(string key)
    {
        return GetKvValue(key, QueryParams.DEFAULT);
    }

    public ConsulResponse<GetValue> GetKvValue(string key, string? token)
    {
        return GetKvValue(key, token, QueryParams.DEFAULT);
    }

    public ConsulResponse<GetValue> GetKvValue(string key, QueryParams? queryParams)
    {
        return GetKvValue(key, null, queryParams);
    }

    public ConsulResponse<GetValue> GetKvValue(string key, string? token, QueryParams? queryParams)
    {
        IUrlParameters? tokenParams = !string.IsNullOrWhiteSpace(token) ? new SingleUrlParameters("token", token) : null;
        ConsulHttpResponse httpResponse = _consulRawClient.MakeGetRequest("/v1/kv/" + key, tokenParams, queryParams);

        if (httpResponse.StatusCode == 200)
        {
            IList<GetValue> value = httpResponse.Content.FromJson<List<GetValue>>();

            if (value.Count == 0)
            {
                return new ConsulResponse<GetValue>(null, httpResponse);
            }

            if (value.Count == 1)
            {
                return new ConsulResponse<GetValue>(value[0], httpResponse);
            }
            throw new ConsulException("Strange response (list size=" + value.Count + ")");
        }

        if (httpResponse.StatusCode == 404)
        {
            return new ConsulResponse<GetValue>(null, httpResponse);
        }
        throw new OperationException(httpResponse);
    }

    public ConsulResponse<GetBinaryValue> GetKvBinaryValue(string key)
    {
        return GetKvBinaryValue(key, QueryParams.DEFAULT);
    }

    public ConsulResponse<GetBinaryValue> GetKvBinaryValue(string key, string? token)
    {
        return GetKvBinaryValue(key, token, QueryParams.DEFAULT);
    }

    public ConsulResponse<GetBinaryValue> GetKvBinaryValue(string key, QueryParams? queryParams)
    {
        return GetKvBinaryValue(key, null, queryParams);
    }

    public ConsulResponse<GetBinaryValue> GetKvBinaryValue(string key, string? token, QueryParams? queryParams)
    {
        IUrlParameters? tokenParams = !string.IsNullOrWhiteSpace(token) ? new SingleUrlParameters("token", token) : null;
        ConsulHttpResponse httpResponse = _consulRawClient.MakeGetRequest("/v1/kv/" + key, tokenParams!, queryParams);

        if (httpResponse.StatusCode == 200)
        {
            IList<GetBinaryValue> value = httpResponse.Content.FromJson<List<GetBinaryValue>>();

            if (value.Count == 0)
            {
                return new ConsulResponse<GetBinaryValue>(null, httpResponse);
            }

            if (value.Count == 1)
            {
                return new ConsulResponse<GetBinaryValue>(value[0], httpResponse);
            }
            throw new ConsulException("Strange response (list size=" + value.Count + ")");
        }

        if (httpResponse.StatusCode == 404)
        {
            return new ConsulResponse<GetBinaryValue>(null, httpResponse);
        }
        throw new OperationException(httpResponse);
    }

    public ConsulResponse<IList<GetValue>> GetKvValues(string keyPrefix)
    {
        return GetKvValues(keyPrefix, QueryParams.DEFAULT);
    }

    public ConsulResponse<IList<GetValue>> GetKvValues(string keyPrefix, string? token)
    {
        return GetKvValues(keyPrefix, token, QueryParams.DEFAULT);
    }

    public ConsulResponse<IList<GetValue>> GetKvValues(string keyPrefix, QueryParams? queryParams)
    {
        return GetKvValues(keyPrefix, null, queryParams);
    }

    public ConsulResponse<IList<GetValue>> GetKvValues(string keyPrefix, string? token, QueryParams? queryParams)
    {
        IUrlParameters? recurseParam = new SingleUrlParameters("recurse");
        IUrlParameters? tokenParam = !string.IsNullOrWhiteSpace(token) ? new SingleUrlParameters("token", token) : null;
        ConsulHttpResponse httpResponse = _consulRawClient.MakeGetRequest("/v1/kv/" + keyPrefix, recurseParam, tokenParam!, queryParams);

        if (httpResponse.StatusCode == 200)
        {
            IList<GetValue> value = httpResponse.Content.FromJson<List<GetValue>>();
            return new ConsulResponse<IList<GetValue>>(value, httpResponse);
        }

        if (httpResponse.StatusCode == 404)
        {
            return new ConsulResponse<IList<GetValue>>(null, httpResponse);
        }
        throw new OperationException(httpResponse);
    }

    public ConsulResponse<IList<GetBinaryValue>> GetKvBinaryValues(string keyPrefix)
    {
        return GetKvBinaryValues(keyPrefix, QueryParams.DEFAULT);
    }

    public ConsulResponse<IList<GetBinaryValue>> GetKvBinaryValues(string keyPrefix, string? token)
    {
        return GetKvBinaryValues(keyPrefix, token, QueryParams.DEFAULT);
    }

    public ConsulResponse<IList<GetBinaryValue>> GetKvBinaryValues(string keyPrefix, QueryParams? queryParams)
    {
        return GetKvBinaryValues(keyPrefix, null, queryParams);
    }

    public ConsulResponse<IList<GetBinaryValue>> GetKvBinaryValues(string keyPrefix, string? token, QueryParams? queryParams)
    {
        IUrlParameters? recurseParam = new SingleUrlParameters("recurse");
        IUrlParameters? tokenParam = !string.IsNullOrWhiteSpace(token) ? new SingleUrlParameters("token", token) : null;
        ConsulHttpResponse httpResponse = _consulRawClient.MakeGetRequest("/v1/kv/" + keyPrefix, recurseParam, tokenParam!, queryParams);

        if (httpResponse.StatusCode == 200)
        {
            IList<GetBinaryValue> value = httpResponse.Content.FromJson<List<GetBinaryValue>>();
            return new ConsulResponse<IList<GetBinaryValue>>(value, httpResponse);
        }

        if (httpResponse.StatusCode == 404)
        {
            return new ConsulResponse<IList<GetBinaryValue>>(null, httpResponse);
        }
        throw new OperationException(httpResponse);
    }

    public ConsulResponse<IList<string>> getKVKeysOnly(string keyPrefix)
    {
        return getKVKeysOnly(keyPrefix, QueryParams.DEFAULT);
    }

    public ConsulResponse<IList<string>> getKVKeysOnly(string keyPrefix, string? separator, string? token)
    {
        return getKVKeysOnly(keyPrefix, separator, token, QueryParams.DEFAULT);
    }

    public ConsulResponse<IList<string>> getKVKeysOnly(string keyPrefix, QueryParams? queryParams)
    {
        return getKVKeysOnly(keyPrefix, null, null, queryParams);
    }

    public ConsulResponse<IList<string>> getKVKeysOnly(string keyPrefix, string? separator, string? token, QueryParams? queryParams)
    {
        IUrlParameters? keysParam = new SingleUrlParameters("keys");
        IUrlParameters? separatorParam = !string.IsNullOrWhiteSpace(separator) ? new SingleUrlParameters("separator", separator) : null;
        IUrlParameters? tokenParam = !string.IsNullOrWhiteSpace(token) ? new SingleUrlParameters("token", token) : null;
        ConsulHttpResponse httpResponse = _consulRawClient.MakeGetRequest("/v1/kv/" + keyPrefix, keysParam, separatorParam!, tokenParam!, queryParams);

        if (httpResponse.StatusCode == 200)
        {
            IList<string> value = httpResponse.Content.FromJson<List<string>>();
            return new ConsulResponse<IList<string>>(value, httpResponse);
        }

        if (httpResponse.StatusCode == 404)
        {
            return new ConsulResponse<IList<string>>(null, httpResponse);
        }
        throw new OperationException(httpResponse);
    }

    public ConsulResponse<bool?> SetKvValue(string key, string value)
    {
        return SetKvValue(key, value, QueryParams.DEFAULT);
    }

    public ConsulResponse<bool?> SetKvValue(string key, string value, PutParams? putParams)
    {
        return SetKvValue(key, value, putParams, QueryParams.DEFAULT);
    }

    public ConsulResponse<bool?> SetKvValue(string key, string value, string? token, PutParams? putParams)
    {
        return SetKvValue(key, value, token, putParams, QueryParams.DEFAULT);
    }

    public ConsulResponse<bool?> SetKvValue(string key, string value, QueryParams? queryParams)
    {
        return SetKvValue(key, value, null, null, queryParams);
    }

    public ConsulResponse<bool?> SetKvValue(string key, string value, PutParams? putParams, QueryParams? queryParams)
    {
        return SetKvValue(key, value, null, putParams, queryParams);
    }

    public ConsulResponse<bool?> SetKvValue(string key, string value, string? token, PutParams? putParams, QueryParams? queryParams)
    {
        IUrlParameters? tokenParam = !string.IsNullOrWhiteSpace(token) ? new SingleUrlParameters("token", token) : null;
        ConsulHttpResponse httpResponse = _consulRawClient.MakePutRequest("/v1/kv/" + key, value, putParams, tokenParam!, queryParams);

        if (httpResponse.StatusCode == 200)
        {
            bool result = httpResponse.Content.FromJson<bool>(); ;
            return new ConsulResponse<bool?>(result, httpResponse);
        }

        throw new OperationException(httpResponse);
    }

    public ConsulResponse<bool?> SetKvBinaryValue(string key, sbyte[] value)
    {
        return SetKvBinaryValue(key, value, QueryParams.DEFAULT);
    }

    public ConsulResponse<bool?> SetKvBinaryValue(string key, sbyte[] value, PutParams? putParams)
    {
        return SetKvBinaryValue(key, value, putParams, QueryParams.DEFAULT);
    }

    public ConsulResponse<bool?> SetKvBinaryValue(string key, sbyte[] value, string? token, PutParams? putParams)
    {
        return SetKvBinaryValue(key, value, token, putParams, QueryParams.DEFAULT);
    }

    public ConsulResponse<bool?> SetKvBinaryValue(string key, sbyte[] value, QueryParams? queryParams)
    {
        return SetKvBinaryValue(key, value, null, null, queryParams);
    }

    public ConsulResponse<bool?> SetKvBinaryValue(string key, sbyte[] value, PutParams? putParams, QueryParams? queryParams)
    {
        return SetKvBinaryValue(key, value, null, putParams, queryParams);
    }

    public ConsulResponse<bool?> SetKvBinaryValue(string key, sbyte[] value, string? token, PutParams? putParams, QueryParams? queryParams)
    {
        Request request = new Request
        {
            Endpoint = "/v1/kv/" + key,
            Token = token,
            UrlParameters = new List<IUrlParameters?> { queryParams, putParams },
            BinaryContent = value
        };

        ConsulHttpResponse httpResponse = _consulRawClient.MakePutRequest(request);

        if (httpResponse.StatusCode == 200)
        {
            bool result = httpResponse.Content.FromJson<bool>(); ;
            return new ConsulResponse<bool?>(result, httpResponse);
        }

        throw new OperationException(httpResponse);
    }

    public ConsulResponse DeleteKvValue(string key)
    {
        return DeleteKvValue(key, QueryParams.DEFAULT);
    }

    public ConsulResponse DeleteKvValue(string key, string? token)
    {
        return DeleteKvValue(key, token, QueryParams.DEFAULT);
    }

    public ConsulResponse DeleteKvValue(string key, QueryParams? queryParams)
    {
        return DeleteKvValue(key, null, queryParams);
    }

    public ConsulResponse DeleteKvValue(string key, string? token, QueryParams? queryParams)
    {
        Request request = new Request
        {
            Endpoint = "/v1/kv/" + key,
            Token = token,
            UrlParameters = new List<IUrlParameters?> { queryParams },
        };

        ConsulHttpResponse httpResponse = _consulRawClient.MakeDeleteRequest(request);

        if (httpResponse.StatusCode == 200)
        {
            return new ConsulResponse(httpResponse);
        }

        throw new OperationException(httpResponse);
    }

    public ConsulResponse DeleteKvValues(string key)
    {
        return DeleteKvValues(key, QueryParams.DEFAULT);
    }

    public ConsulResponse DeleteKvValues(string key, string? token)
    {
        return DeleteKvValues(key, token, QueryParams.DEFAULT);
    }

    public ConsulResponse DeleteKvValues(string key, QueryParams? queryParams)
    {
        return DeleteKvValues(key, null, queryParams);
    }

    public ConsulResponse DeleteKvValues(string key, string? token, QueryParams? queryParams)
    {
        IUrlParameters? recurseParam = new SingleUrlParameters("recurse");

        Request request = new Request
        {
            Endpoint = "/v1/kv/" + key,
            Token = token,
            UrlParameters = new List<IUrlParameters?> { recurseParam, queryParams },
        };

        ConsulHttpResponse httpResponse = _consulRawClient.MakeDeleteRequest(request);

        if (httpResponse.StatusCode == 200)
        {
            return new ConsulResponse(httpResponse);
        }

        throw new OperationException(httpResponse);
    }
}