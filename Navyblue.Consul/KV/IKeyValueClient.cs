using Navyblue.Consul.KV.Model;

namespace Navyblue.Consul.KV;

/// <summary>
///
/// </summary>
public interface IKeyValueClient
{
    ConsulResponse<GetValue> GetKvValue(string key);

    ConsulResponse<GetValue> GetKvValue(string key, string? token);

    ConsulResponse<GetValue> GetKvValue(string key, QueryParams? queryParams);

    ConsulResponse<GetValue> GetKvValue(string key, string? token, QueryParams? queryParams);

    ConsulResponse<GetBinaryValue> GetKvBinaryValue(string key);

    ConsulResponse<GetBinaryValue> GetKvBinaryValue(string key, string? token);

    ConsulResponse<GetBinaryValue> GetKvBinaryValue(string key, QueryParams? queryParams);

    ConsulResponse<GetBinaryValue> GetKvBinaryValue(string key, string? token, QueryParams? queryParams);

    ConsulResponse<IList<GetValue>> GetKvValues(string keyPrefix);

    ConsulResponse<IList<GetValue>> GetKvValues(string keyPrefix, string? token);

    ConsulResponse<IList<GetValue>> GetKvValues(string keyPrefix, QueryParams? queryParams);

    ConsulResponse<IList<GetValue>> GetKvValues(string keyPrefix, string? token, QueryParams? queryParams);

    ConsulResponse<IList<GetBinaryValue>> GetKvBinaryValues(string keyPrefix);

    ConsulResponse<IList<GetBinaryValue>> GetKvBinaryValues(string keyPrefix, string? token);

    ConsulResponse<IList<GetBinaryValue>> GetKvBinaryValues(string keyPrefix, QueryParams? queryParams);

    ConsulResponse<IList<GetBinaryValue>> GetKvBinaryValues(string keyPrefix, string? token, QueryParams? queryParams);

    ConsulResponse<IList<string>> getKVKeysOnly(string keyPrefix);

    ConsulResponse<IList<string>> getKVKeysOnly(string keyPrefix, string? separator, string? token);

    ConsulResponse<IList<string>> getKVKeysOnly(string keyPrefix, QueryParams? queryParams);

    ConsulResponse<IList<string>> getKVKeysOnly(string keyPrefix, string? separator, string? token, QueryParams? queryParams);

    ConsulResponse<bool?> SetKvValue(string key, string value);

    ConsulResponse<bool?> SetKvValue(string key, string value, PutParams? putParams);

    ConsulResponse<bool?> SetKvValue(string key, string value, string? token, PutParams? putParams);

    ConsulResponse<bool?> SetKvValue(string key, string value, QueryParams? queryParams);

    ConsulResponse<bool?> SetKvValue(string key, string value, PutParams? putParams, QueryParams? queryParams);

    ConsulResponse<bool?> SetKvValue(string key, string value, string? token, PutParams? putParams, QueryParams? queryParams);

    ConsulResponse<bool?> SetKvBinaryValue(string key, sbyte[] value);

    ConsulResponse<bool?> SetKvBinaryValue(string key, sbyte[] value, PutParams? putParams);

    ConsulResponse<bool?> SetKvBinaryValue(string key, sbyte[] value, string? token, PutParams? putParams);

    ConsulResponse<bool?> SetKvBinaryValue(string key, sbyte[] value, QueryParams? queryParams);

    ConsulResponse<bool?> SetKvBinaryValue(string key, sbyte[] value, PutParams? putParams, QueryParams? queryParams);

    ConsulResponse<bool?> SetKvBinaryValue(string key, sbyte[] value, string? token, PutParams? putParams, QueryParams? queryParams);

    ConsulResponse DeleteKvValue(string key);

    ConsulResponse DeleteKvValue(string key, string? token);

    ConsulResponse DeleteKvValue(string key, QueryParams? queryParams);

    ConsulResponse DeleteKvValue(string key, string? token, QueryParams? queryParams);

    ConsulResponse DeleteKvValues(string key);

    ConsulResponse DeleteKvValues(string key, string? token);

    ConsulResponse DeleteKvValues(string key, QueryParams? queryParams);

    ConsulResponse DeleteKvValues(string key, string? token, QueryParams? queryParams);
}