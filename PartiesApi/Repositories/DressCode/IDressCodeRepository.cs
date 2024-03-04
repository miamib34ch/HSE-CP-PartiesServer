namespace PartiesApi.Repositories.DressCode;

internal interface IDressCodeRepository
{
    Task<Models.DressCode?> GetDressCodeOrDefaultAsync(Guid dressCodeId);
    Task<Models.DressCode?> AddDressCodeAsync(Models.DressCode dressCode);
    Task<bool> EditDressCodeAsync(Models.DressCode dressCode);
    Task<IEnumerable<Models.DressCode>> GetDressCodesAsync();
}