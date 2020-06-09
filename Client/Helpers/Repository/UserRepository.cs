using BlazorMovies.Client.Helpers.Interfaces;
using BlazorMovies.Shared.DTO;
using BlazorMovies.Shared.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorMovies.Client.Helpers.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IHttpService httpService;
        private string url = "api/user";
        public UserRepository(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        public async Task<PaginatedResponse<List<UserDTO>>> GetUsers(PaginationDTO dto)
        {
            var response = await httpService.GetHelper<List<UserDTO>>(url, dto);
            return response;
        }

        public async Task<List<RoleDTO>> GetRoles()
        {
            var response = await httpService.GetHelper<List<RoleDTO>>($"{url}/roles");
            return response;
        }

        public async Task AssignRole (EditRoleDTO roleDTO)
        {
            var response = await httpService.Post($"{url}/AssignRoles", roleDTO);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
        }

        public async Task RemoveRole(EditRoleDTO roleDTO)
        {
            var response = await httpService.Post($"{url}/AssignRoles", roleDTO);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
        }
    }
}
