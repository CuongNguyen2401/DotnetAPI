using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyApiNetCore8.DTO.Response;
using MyApiNetCore8.DTO.Request;
using MyApiNetCore8.Model;
using MyApiNetCore8.Data;

namespace MyApiNetCore8.Repository.impl
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly MyContext _context;
        private readonly IMapper _mapper;

        public PermissionRepository(MyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PermissionResponse> CreatePermissionAsync(PermissionRequest permission)
        {
            var permissionEntity = _mapper.Map<Permission>(permission);
            _context.Permission.Add(permissionEntity);
            await _context.SaveChangesAsync();
            return _mapper.Map<PermissionResponse>(permissionEntity);
        }

        public void DeletePermission(string name)
        {
            var permission = _context.Permission.FirstOrDefault(p => p.name == name);
            if (permission != null)
            {
                _context.Permission.Remove(permission);
                _context.SaveChanges();
            }
        }

        public async Task<List<PermissionResponse>> GetAllPermissionsAsync()
        {
            var permissions = await _context.Permission.ToListAsync();
            return _mapper.Map<List<PermissionResponse>>(permissions);
        }
    }
}
