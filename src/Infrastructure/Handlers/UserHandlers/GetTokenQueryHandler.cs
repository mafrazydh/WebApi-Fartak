using Application.Models.Identity;
using Application.Queries;
using Application.Wrappers;
using Infrastructure.Identity;
using MediatR;

namespace Infrastructure.Handlers.UserHandlers
{
    internal class GetTokenQueryHandler : IRequestHandler<GetTokenQuery, Result<JwtResultDto>>
    {
        private readonly JwtTokenService _jwtTokenService;

        public GetTokenQueryHandler(JwtTokenService jwtTokenService)
        {
            _jwtTokenService = jwtTokenService;
        }

        public async Task<Result<JwtResultDto>> Handle(GetTokenQuery request, CancellationToken cancellationToken)
        {
            var jwtToken = await _jwtTokenService.GenerateTokenAsync(request.Dto);
            return new Result<JwtResultDto> { Value = jwtToken };
        }
    }
}
