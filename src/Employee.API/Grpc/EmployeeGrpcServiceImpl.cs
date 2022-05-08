using Employee.API.Enums;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;

namespace Employee.API.Grpc
{
    public class EmployeeGrpcServiceImpl : EmployeeGrpcService.EmployeeGrpcServiceBase
    {
        private readonly EmployeeDbContext _dbContext;

        public EmployeeGrpcServiceImpl(EmployeeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override async Task<CreateReply> Create(CreateMessage request, ServerCallContext context)
        {
            var employee = new Employee
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                City = request.City,
                Country = request.Country,
                DateOfBirth = DateTime.ParseExact(request.DateOfBirth, "yyyy-MM-dd", null),
                Email = request.Email,
                PersonalIdentificationNumber = request.PersonalIdentificationNumber,
                State = request.State,
                Title = (TitleOption)request.Title
            };

            _dbContext.Employees.Add(employee);
            await _dbContext.SaveChangesAsync();
            return new CreateReply
            {
                Id = employee.Id
            };
        }

        public override async Task<GetByIdReply> GetById(GetByIdMessage request, ServerCallContext context)
        {
            var employee = await _dbContext.Employees.SingleOrDefaultAsync(
                x => x.Id == request.Id);

            return new GetByIdReply
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                City = employee.City,
                Country = employee.Country,
                DateOfBirth = employee.DateOfBirth.ToString("yyyy-MM-dd"),
                Email = employee.Email,
                PersonalIdentificationNumber = employee.PersonalIdentificationNumber,
                State = employee.State,
                Title = (EmployeeTitle)employee.Title
            };
        }

        public override async Task<UpdateReply> Update(UpdateMessage request, ServerCallContext context)
        {
            var employee = new Employee
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                City = request.City,
                Country = request.Country,
                DateOfBirth = DateTime.ParseExact(request.DateOfBirth, "yyyy-MM-dd", null),
                Email = request.Email,
                PersonalIdentificationNumber = request.PersonalIdentificationNumber,
                State = request.State,
                Title = (TitleOption)request.Title
            };

            _dbContext.Employees.Update(employee);
            var numberOfAffectedRows = await _dbContext.SaveChangesAsync();
            return new UpdateReply
            {
                Success = numberOfAffectedRows == 1
            };
        }

        public override async Task<SearchReply> Search(SearchMessage request, ServerCallContext context)
        {
            var items = await _dbContext.Employees
                .Where(x =>
                    EF.Functions.Like(x.FirstName, $"%{request.SearchTerm}%")
                    || EF.Functions.Like(x.LastName, $"%{request.SearchTerm}%")
                    || EF.Functions.Like(x.Email, $"%{request.SearchTerm}%"))
                .ToListAsync();

            return new SearchReply
            {
                Items =
                {
                    items.Select(x=> new EmployeeSearchModel
                    {
                        Id = x.Id,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        Email = x.Email
                    })
                }
            };
        }
    }
}