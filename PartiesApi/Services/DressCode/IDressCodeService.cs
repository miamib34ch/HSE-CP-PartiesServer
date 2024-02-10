using PartiesApi.DTO;
using PartiesApi.DTO.DressCode;

namespace PartiesApi.Services.DressCode;

public interface IDressCodeService
{
    Task<MethodResult<IEnumerable<DressCodeResponse>>> GetDressCodesAsync();
    Task<MethodResult> CreateDressCodeAsync(DressCodeRequest dressCodeRequest);
    Task<MethodResult> EditDressCodeAsync(DressCodeRequest dressCodeRequest);
}