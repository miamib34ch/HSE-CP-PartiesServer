using Microsoft.EntityFrameworkCore;
using PartiesApi.Database;

namespace PartiesApi.Repositories.DressCode;

internal class DressCodeRepository(ApplicationDbContext dbContext) : IDressCodeRepository
{
    public async Task<Models.DressCode?> GetDressCodeOrDefaultAsync(Guid dressCodeId)
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

    public async Task<bool> AddDressCodeAsync(Models.DressCode dressCode)
    {
        try
        {
            var createdDressCode = await dbContext.DressCodes.AddAsync(dressCode);

            return createdDressCode.State == EntityState.Added;
        }
        catch (Exception e)
        {
            return false;
        }
        finally
        {
            await dbContext.SaveChangesAsync();
        }
    }

    public async Task<bool> EditDressCodeAsync(Models.DressCode newDressCode)
    {
        try
        {
            var existingDressCode = await dbContext.DressCodes
                .FirstOrDefaultAsync(dressCode => dressCode.Id == newDressCode.Id);

            if (existingDressCode != null)
            {
                existingDressCode.Name = newDressCode.Name;
                existingDressCode.Description = newDressCode.Description;

                var updatedDressCode = dbContext.DressCodes.Update(existingDressCode);

                return updatedDressCode.State == EntityState.Modified;
            }
        }
        catch (Exception e)
        {
            return false;
        }
        finally
        {
            await dbContext.SaveChangesAsync();
        }

        return false;
    }

    public async Task<IEnumerable<Models.DressCode>> GetDressCodesAsync()
    {
        try
        {
            var dressCodes = await dbContext.DressCodes.ToListAsync();

            return dressCodes;
        }
        catch (Exception e)
        {
            return new List<Models.DressCode>();
        }
    }
}