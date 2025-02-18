using Sample.Test.Domain.Entity.UserAggregate;
using Sample.Test.Domain.Service.Queries.GetUserById;

namespace Sample.Test.Service.Queries.GetUserById
{
    public class GetUserByIdFactory
    {
        public static GetUserByIdResponse MapResponse(User user)
        {
            return new GetUserByIdResponse
            {
                Id = user.Id,
                MobileNumber = user.MobileNumber,
                Fullname = user.Fullname,
                NationalCode = user.NationalCode ?? string.Empty,
                Status = user.Status,
                BirthDate = user.BirthDate,
                Email = user.Email,
                SubmitTime = user.CreatedAt,
            };
        }
    }
}