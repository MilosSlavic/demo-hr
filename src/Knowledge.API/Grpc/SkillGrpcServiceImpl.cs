using Grpc.Core;
using Knowledge.API.Skill;
using Microsoft.EntityFrameworkCore;

namespace Knowledge.API.Grpc
{
    public class SkillGrpcServiceImpl : SkillGrpcService.SkillGrpcServiceBase
    {
        private readonly KnowledgeDbContext _dbContext;

        public SkillGrpcServiceImpl(KnowledgeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override async Task<GetByEmployeeIdReply> GetByEmployeeId(GetByEmployeeIdMessage request, ServerCallContext context)
        {
            var skills = await _dbContext.Skills.Where(x => x.EmployeeId == request.EmployeeId).ToListAsync();
            return new GetByEmployeeIdReply
            {
                Items =
                {
                    skills.Select(x => new SkillModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        EmployeeId = x.EmployeeId,
                        Grade = x.Grade
                    })
                }
            };
        }

        public override async Task<CreateReply> Create(CreateMessage request, ServerCallContext context)
        {
            var skill = new Entities.Skill
            {
                Name = request.Name,
                EmployeeId = request.EmployeeId,
                Grade = (byte)request.Grade
            };
            _dbContext.Skills.Add(skill);
            await _dbContext.SaveChangesAsync();
            return new CreateReply
            {
                Id = skill.Id
            };
        }

        public override async Task<HasSkillsReply> HasSkills(HasSkillsMessage request, ServerCallContext context)
        {
            var hasAny = await _dbContext.Skills.AnyAsync(x => x.EmployeeId == request.EmployeeId);
            return new HasSkillsReply
            {
                Yes = hasAny
            };
        }
    }
}
