namespace Typing_App_API
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, GetUserDto>();
            CreateMap<AddUserDto, User>();
            CreateMap<Record, GetRecordDto>();
            CreateMap<AddRecordDto, Record>();
            CreateMap<User, GetUserDtoSafe>();
        }
    }
}
