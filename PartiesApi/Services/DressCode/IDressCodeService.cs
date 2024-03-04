using PartiesApi.DTO;
using PartiesApi.DTO.DressCode;

namespace PartiesApi.Services.DressCode;

public interface IDressCodeService
{
    Task<MethodResult<IEnumerable<DressCodeResponse>>> GetDressCodesAsync();
    Task<MethodResult<DressCodeResponse>> CreateDressCodeAsync(DressCodeRequest dressCodeRequest);
    Task<MethodResult> EditDressCodeAsync(DressCodeRequest dressCodeRequest);
}