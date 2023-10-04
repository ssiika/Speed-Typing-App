﻿namespace Typing_App_API.Services.UserService
{
    public interface IUserService
    {
        Task<ServiceResponse<List<GetUserDto>>> GetAll();
        Task<ServiceResponse<GetUserDto>> GetSingle(int id);
        Task<ServiceResponse<List<GetUserDto>>> AddUser(AddUserDto newUser);

    }
}
