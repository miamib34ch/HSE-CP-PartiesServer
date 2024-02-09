using Microsoft.EntityFrameworkCore;
using PartiesApi.Database;
using PartiesApi.Models;

namespace PartiesApi.Repositories.DressCodeRepository;

internal class DressCodeRepository(ApplicationDbContext dbContext) : IDressCodeRepository
{
    public async Task<DressCode?> GetDressCodeOrDefaultAsync(Guid dressCodeId)
    {
        try
        {
            var dressCode = await dbContext.DressCodes.FirstOrDefaultAsync(dressCode => dressCode.Id == dressCodeId);

            return dressCode;
        }
        catch (Exception e)
        {
            return null;
        }
    }
}