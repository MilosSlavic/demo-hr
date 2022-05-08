using Grpc.Core;
using static Employee.API.EmployeeGrpcService;
using static Knowledge.API.Certificate.CertificateGrpcService;
using static Knowledge.API.Skill.SkillGrpcService;

namespace EmployeeSearchBff.API.Grpc
{
    public class SearchGrpcServiceImpl : SearchGrpcService.SearchGrpcServiceBase
    {
        private readonly EmployeeGrpcServiceClient _employeeGrpcServiceClient;
        private readonly CertificateGrpcServiceClient _certificateGrpcServiceClient;
        private readonly SkillGrpcServiceClient _skillGrpcServiceClient;

        public SearchGrpcServiceImpl(
            EmployeeGrpcServiceClient employeeGrpcServiceClient,
            CertificateGrpcServiceClient certificateGrpcServiceClient,
            SkillGrpcServiceClient skillGrpcServiceClient)
        {
            _employeeGrpcServiceClient = employeeGrpcServiceClient;
            _certificateGrpcServiceClient = certificateGrpcServiceClient;
            _skillGrpcServiceClient = skillGrpcServiceClient;
        }

        public override async Task<SearchReply> Search(SearchMessage request, ServerCallContext context)
        {
            var employeeSearchReply = await _employeeGrpcServiceClient.SearchAsync(new Employee.API.SearchMessage
            {
                SearchTerm = request.SearchTerm
            });

            var tasks = employeeSearchReply.Items
                .Select(async x =>
                {
                    var hasSkillsTask = _skillGrpcServiceClient.HasSkillsAsync(new Knowledge.API.Skill.HasSkillsMessage
                    {
                        EmployeeId = x.Id
                    }).ResponseAsync;
                    var hasCertificateTask = _certificateGrpcServiceClient.HasCertificateAsync(new Knowledge.API.Certificate.HasCertificateMessage
                    {
                        EmployeeId = x.Id
                    }).ResponseAsync;

                    await Task.WhenAll(hasSkillsTask, hasCertificateTask);

                    return new EmployeeModel
                    {
                        Id = x.Id,
                        Email = x.Email,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        HasSkills = hasSkillsTask.Result.Yes,
                        HasCertificates = hasSkillsTask.Result.Yes
                    };
                });

            await Task.WhenAll(tasks);

            return new SearchReply
            {
                Items =
                {
                    tasks.Select(x=>x.Result).ToList()
                }
            };
        }
    }
}