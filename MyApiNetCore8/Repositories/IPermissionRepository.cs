

using MyApiNetCore8.DTO.Response;
using MyApiNetCore8.DTO.Request;

namespace MyApiNetCore8.Repository
{
    public interface IPermissionRepository
    {
        Task<List<PermissionResponse>> GetAllPermissionsAsync();
        Task<PermissionResponse> CreatePermissionAsync(PermissionRequest permission);
        void DeletePermission(string name);

    }
}
