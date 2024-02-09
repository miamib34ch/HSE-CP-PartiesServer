using PartiesApi.Models;

namespace PartiesApi.Repositories.DressCodeRepository;

public interface IDressCodeRepository
{
    Task<DressCode?> GetDressCodeOrDefaultAsync(Guid dressCodeId);
}