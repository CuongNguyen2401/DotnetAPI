
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApiNetCore8.Data;
using MyApiNetCore8.DTO.Request;
using MyApiNetCore8.DTO.Response;
using MyApiNetCore8.Model;
using MyApiNetCore8.Repository;

namespace MyApiNetCore8.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PermissionsController : ControllerBase
    {

        private readonly IPermissionRepository _permissionRepository;

        public PermissionsController(MyContext context, IPermissionRepository permissionRepository)
        {

            _permissionRepository = permissionRepository;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<PermissionResponse>>>> GetPermission()
        {
            return new ApiResponse<List<PermissionResponse>>(200, "Success", await _permissionRepository.GetAllPermissionsAsync());

        }

        [HttpPost]
        public async Task<ActionResult<Permission>> PostPermisison(PermissionRequest permission)
        {
            var permissionResponse = await _permissionRepository.CreatePermissionAsync(permission);
            return CreatedAtAction("GetPermission", new ApiResponse<PermissionResponse>(200, "Success", permissionResponse));

        }

        [HttpDelete("{name}")]
        public void DeletePermission(string name)
        {
            _permissionRepository.DeletePermission(name);
        }
    }
}
