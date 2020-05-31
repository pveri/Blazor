using BlazorMovies.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorMovies.Client.Helpers.Interfaces
{
    public interface IUserRepository
    {
        Task AssignRole(EditRoleDTO roleDTO);
        Task RemoveRole(EditRoleDTO roleDTO);
        Task<List<RoleDTO>> GetRoles();
        Task<PaginatedResponse<List<UserDTO>>> GetUsers(PaginationDTO dto);
    }
}
