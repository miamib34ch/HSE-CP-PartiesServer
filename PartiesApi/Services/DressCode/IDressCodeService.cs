namespace PartiesApi.Services.DressCode;

public interface IDressCodeService
{
    Task<Models.DressCode?> GetDressCodeOrDefaultAsync(Guid dressCodeId);
}