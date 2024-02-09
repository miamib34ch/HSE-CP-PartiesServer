using PartiesApi.Repositories.DressCodeRepository;

namespace PartiesApi.Services.DressCode;

internal class DressCodeService(IDressCodeRepository dressCodeRepository) : IDressCodeService
{
    public async Task<Models.DressCode?> GetDressCodeOrDefaultAsync(Guid dressCodeId)
    {
        var dressCode = await dressCodeRepository.GetDressCodeOrDefaultAsync(dressCodeId);

        return dressCode;
    }
}