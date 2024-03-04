using AutoMapper;
using PartiesApi.DTO;
using PartiesApi.DTO.DressCode;
using PartiesApi.Repositories.DressCode;

namespace PartiesApi.Services.DressCode;

internal class DressCodeService(IDressCodeRepository dressCodeRepository, IMapper mapper) : IDressCodeService
{
    public async Task<MethodResult<IEnumerable<DressCodeResponse>>> GetDressCodesAsync()
    {
        const string methodName = "GetDressCodes";

        try
        {
            var dressCodes = await dressCodeRepository.GetDressCodesAsync();

            var dressCodeResponses = dressCodes.Select(mapper.Map<Models.DressCode, DressCodeResponse>).ToList();

            return new MethodResult<IEnumerable<DressCodeResponse>>(methodName, true, string.Empty, dressCodeResponses);
        }
        catch (Exception ex)
        {
            return new MethodResult<IEnumerable<DressCodeResponse>>(methodName, false, $"Unknown error",
                new List<DressCodeResponse>());
        }
    }

    public async Task<MethodResult<DressCodeResponse>> CreateDressCodeAsync(DressCodeRequest dressCodeRequest)
    {
        const string methodName = "CreateDressCode";

        try
        {
            var dressCode = mapper.Map<DressCodeRequest, Models.DressCode>(dressCodeRequest);
            
            var createdDressCode = await dressCodeRepository.AddDressCodeAsync(dressCode);
            var isDressCodeCreated = createdDressCode != null;
            if (!isDressCodeCreated)
                return new MethodResult<DressCodeResponse>(methodName, isDressCodeCreated, string.Empty);

            var dressCodeResponse = mapper.Map<Models.DressCode, DressCodeResponse>(createdDressCode);
            return new MethodResult<DressCodeResponse>(methodName, isDressCodeCreated, string.Empty, dressCodeResponse);
        }
        catch (Exception ex)
        {
            return new MethodResult<DressCodeResponse>(methodName, false, $"Unknown error");
        }
    }

    public async Task<MethodResult> EditDressCodeAsync(DressCodeRequest dressCodeRequest)
    {
        const string methodName = "EditDressCode";

        try
        {
            if (dressCodeRequest.Id == null)
                return new MethodResult(methodName, false, "No dress code Id in request");

            var existingDressCode = await dressCodeRepository.GetDressCodeOrDefaultAsync((Guid)dressCodeRequest.Id);
            if (existingDressCode == null)
                return new MethodResult(methodName, false, $"Dress code with Id {dressCodeRequest.Id} does not exist");

            var dressCode = mapper.Map<DressCodeRequest, Models.DressCode>(dressCodeRequest);
            var isDressCodeEdited = await dressCodeRepository.EditDressCodeAsync(dressCode);
            return new MethodResult(methodName, isDressCodeEdited, string.Empty);
        }
        catch (Exception ex)
        {
            return new MethodResult(methodName, false, $"Unknown error");
        }
    }
}