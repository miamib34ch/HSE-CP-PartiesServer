using PartiesApi.Models;

namespace PartiesApi.Repositories.DressCodeRepository;

internal interface IDressCodeRepository
{
    Task<DressCode?> GetDressCodeOrDefaultAsync(Guid dressCodeId);
}