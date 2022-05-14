using Navyblue.Consul.ACL.Model;

namespace Navyblue.Consul.ACL;

/// <summary>
///
/// </summary>
public interface IAclClient
{
    ConsulResponse<string> CreateAcl(CreateAcl newAcl, string? token);

    ConsulResponse UpdateAcl(UpdateAcl updateAcl, string? token);

    ConsulResponse DeleteAcl(string aclId, string? token);

    ConsulResponse<Acl> GetAcl(string id);

    ConsulResponse<string> CloneAcl(string aclId, string? token);

    ConsulResponse<IList<Acl>> GetAclList(string? token);
}